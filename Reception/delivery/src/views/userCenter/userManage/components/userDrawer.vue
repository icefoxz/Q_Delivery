<template>
  <el-drawer v-model="drawerVisible" :destroy-on-close="true" size="550px" :title="`${drawerProps.title}人员`">
    <el-form
      ref="ruleFormRef"
      label-width="100px"
      label-suffix=" :"
      :rules="rules"
      :disabled="drawerProps.isView"
      :model="drawerProps.row"
      :hide-required-asterisk="drawerProps.isView"
    >
      <el-form-item label="登录名称" prop="user_LoginName">
        <el-input v-model="drawerProps.row!.user_LoginName" placeholder="请填写登录名称" clearable></el-input>
      </el-form-item>
      <el-form-item label="登录密码" :prop="drawerProps.row.id ? '' : 'user_LoginPwd'">
        <el-input v-model="drawerProps.row!.user_LoginPwd" placeholder="请填写登录密码" clearable></el-input>
      </el-form-item>
      <!-- <el-form-item label="密文密码" prop="user_LoginPwdCipher">
        <el-input v-model="drawerProps.row!.user_LoginPwdCipher" placeholder="请填写密文密码" clearable></el-input>
      </el-form-item> -->
      <el-form-item label="管辖单位" prop="dept_Id">
        <el-select v-model="drawerProps.row!.dept_Id" placeholder="请选择所属单位" clearable filterable>
          <el-option
            v-for="item in list"
            :disabled="item.id == drawerProps.row.id"
            :key="item.id"
            :label="item.dept_Name"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="角色权限" prop="limit_Id">
        <el-select v-model="drawerProps.row!.limit_Id" placeholder="角色权限" clearable filterable>
          <el-option :label="item.limit_Name" :value="item.id" v-for="item in authList" :key="item.id" />
        </el-select>
      </el-form-item>
      <el-form-item label="绑定人员" prop="person_Id">
        <el-select v-model="drawerProps.row!.person_Id" placeholder="绑定人员" clearable filterable>
          <el-option :label="item.per_Name" :value="item.id" v-for="item in perList" :key="item.id" />
        </el-select>
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="drawerVisible = false"> 取消 </el-button>
      <el-button v-show="!drawerProps.isView" type="primary" @click="handleSubmit"> 确定 </el-button>
    </template>
  </el-drawer>
</template>

<script setup lang="ts" name="UserDrawer">
import { ref, reactive } from "vue";
// import { genderType } from "@/utils/serviceDict";
import { ElMessage, FormInstance } from "element-plus";
import { User } from "@/api/interface";
import { $http } from "@/api/testaxios/testaxios";
import md5 from "md5";
import { storeToRefs } from "pinia";
import { useDwStore } from "@/stores/modules/dw";
const { list } = storeToRefs(useDwStore());
// 加密密码
// function encryptPassword(password: string): string {
//   const secretKey = "7A04A20762984749B78E081853618DD5"; // 用于加密的密钥
//   const encryptedPassword = crypto.AES.encrypt(password, secretKey).toString();
//   return encryptedPassword;
// }
// import UploadImg from "@/components/Upload/Img.vue";
// import UploadImgs from "@/components/Upload/Imgs.vue";

const rules = reactive({
  user_LoginName: [{ required: true, message: "请填写人员姓名" }],
  user_LoginPwd: [{ required: true, message: "请选择所属单位" }],
  user_LoginPwdCipher: [{ required: true, message: "请选择工作状态" }],
  dept_Id: [{ required: true, message: "管辖单位" }],
  limit_Id: [{ required: true, message: "请填写身份证号" }],
  person_Id: [{ required: true, message: "请填写身份证号" }]
});

interface DrawerProps {
  title: string;
  isView: boolean;
  row: Partial<User.ResUserList>;
  api?: (params: any) => Promise<any>;
  getTableList?: () => void;
}

const drawerVisible = ref(false);
const drawerProps = ref<DrawerProps>({
  isView: false,
  title: "",
  row: {}
});

// 接收父组件传过来的参数
const acceptParams = (params: DrawerProps) => {
  drawerProps.value = params;
  drawerVisible.value = true;
};

// 提交数据（新增/编辑）
const ruleFormRef = ref<FormInstance>();
const handleSubmit = () => {
  ruleFormRef.value!.validate(async valid => {
    if (!valid) return;
    try {
      await $http.post("/api/User/saveUser", {
        ...drawerProps.value.row,
        ...(drawerProps.value.row.user_LoginPwd ? { user_LoginPwd: md5(drawerProps.value.row.user_LoginPwd).toUpperCase() } : {})

        // user_LoginPwd: drawerProps.value.row.user_LoginPwd ? encryptPassword(drawerProps.value.row.user_LoginPwd!) : null
      });
      ElMessage.success({ message: `${drawerProps.value.title}用户成功！` });
      drawerProps.value.getTableList!();
      drawerVisible.value = false;
    } catch (error) {}
  });
};

const authList = ref<any>([]); // 权限
const perList = ref<any>([]); // 人员
const deptList = ref<any>([]); // 人员

async function init() {
  var res = await $http.get("/api/MenuLimit/getLimitList");
  const res1 = await $http.get("/api/User/getPersonList");
  authList.value = res.data ?? [];
  perList.value = res1.data ?? [];
}
init();
defineExpose({
  acceptParams
});
</script>
