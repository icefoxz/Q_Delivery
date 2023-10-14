<template>
  <div class="swiper-container">
    <el-dialog v-model="isShowDialog" :title="props.title" :width="700" draggable>
      <el-form :model="ruleForm" ref="ruleFormRef" size="default" label-width="100px" :rules="rules">
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="人员类型" prop="per_TypeId">
              <el-select v-model="ruleForm.per_TypeId" class="m-2" placeholder="请选择工作状态" size="large">
                <el-option label="内部人员" value="1" />
                <el-option label="骑手" value="2" />
                <el-option label="普通用户" value="3" />
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20" v-if="ruleForm.per_TypeId == 2">
            <el-form-item label="登录名称" prop="per_UserName">
              <el-input v-model="ruleForm.per_UserName" placeholder="请输入登录名称" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20" v-if="ruleForm.per_TypeId == 2 && !ruleForm.id">
            <el-form-item label="登录密码" :prop="ruleForm.id ? '' : 'per_UserPwd'">
              <el-input v-model="ruleForm.per_UserPwd" placeholder="请输入登录密码" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20" v-if="ruleForm.per_TypeId == 2">
            <el-form-item label="格式化电话号码" prop="per_NormalizedPhone">
              <el-input v-model="ruleForm.per_NormalizedPhone" placeholder="请输入格式化电话号码" clearable />
            </el-form-item>
          </el-col>

          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="姓名" prop="per_Name">
              <el-input v-model="ruleForm.per_Name" placeholder="请输入姓名" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="工作状态" prop="per_JobStatus">
              <el-select v-model="ruleForm.per_JobStatus" class="m-2" placeholder="请选择工作状态" size="large">
                <el-option v-for="item in JobStatus" :label="item.dict_Name" :value="item.dict_Key" :key="item.dict_Key" />
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="手机号" prop="per_Phone">
              <el-input v-model="ruleForm.per_Phone" placeholder="请输入手机号" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="身份证号" prop="per_IdNo">
              <el-input v-model="ruleForm.per_IdNo" placeholder="请输入身份证号" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="出生日期" prop="per_Birthday">
              <el-date-picker v-model="ruleForm.per_Birthday" type="date" placeholder="请选择出生日期" />
              <!-- <el-input v-model="ruleForm.per_Birthday" placeholder="请输入生日" clearable /> -->
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="居住地址" prop="per_Address">
              <el-input v-model="ruleForm.per_Address" placeholder="请输入居住地址" clearable />
            </el-form-item>
          </el-col>
          <!-- <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="政治面貌" prop="per_Politics">
              <el-input v-model="ruleForm.per_Politics" placeholder="请输入政治面貌" clearable />
            </el-form-item>
          </el-col> -->
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="cancel" size="default">取 消</el-button>
          <el-button type="primary" @click="submit" size="default">确 定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup>
import { $http } from "@/api/testaxios/testaxios";
import { useDwStore } from "@/stores/modules/dw";
import { verifyIdCard, verifyPhone } from "@/utils/toolsValidate";
import md5 from "md5";
import type { FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import { storeToRefs } from "pinia";
import { onMounted, ref } from "vue";
import { useUserStore } from "@/stores/modules/user";
const { list } = storeToRefs(useDwStore());
// import { addSwiper, updateSwiper } from '/@/api/main/swiper';
//父级传递来的参数
var props = defineProps({
  title: {
    type: String,
    default: ""
  }
});

const JobStatus = useUserStore().findDic("JobStatus");
//父级传递来的函数，用于回调
const emit = defineEmits(["reloadTable"]);
const ruleFormRef = ref();
const isShowDialog = ref(false);
const ruleForm = ref<any>({});
//自行添加其他规则
const rules = ref<FormRules>({
  per_TypeId: [{ required: true, message: "请选择人员类型", trigger: "blur" }],
  per_UserName: [{ required: true, message: "请输入登录名称", trigger: "blur" }],
  per_UserPwd: [{ required: true, message: "请输入登录密码", trigger: "blur" }],
  per_NormalizedPhone: [
    { required: true, message: "请输入格式化电话号码", trigger: "blur" }
    // {
    //   validator: (rule: any, value: any, callback: any) => (verifyPhone(value) ? callback() : callback("请输入正确的手机号")),
    //   trigger: "blur"
    // }
  ],
  per_Name: [{ required: true, message: "请输入姓名", trigger: "blur" }],
  per_JobStatus: [{ required: true, message: "请选择工作状态", trigger: "blur" }],
  // per_Phone: [
  //   { required: true, message: "请输入手机号", trigger: "blur" },
  //   {
  //     validator: (rule: any, value: any, callback: any) => (verifyPhone(value) ? callback() : callback("请输入正确的手机号")),
  //     trigger: "blur"
  //   }
  // ],
  // per_IdNo: [
  //   { required: true, message: "请输入身份证号码", trigger: "blur" }
  //   // {
  //   //   validator: (rule: any, value: any, callback: any) => (verifyIdCard(value) ? callback() : callback("请输入正确的身份证号")),
  //   //   trigger: "blur"
  //   // }
  // ],
  per_Birthday: [{ required: true, message: "请选择出生日期", trigger: "blur" }],
  per_Address: [{ required: true, message: "请输入居住地址", trigger: "blur" }]
  // per_Politics: [{ required: true, message: "请输入政治面貌", trigger: "blur" }]
});

// 打开弹窗
const openDialog = (row: any) => {
  ruleForm.value = JSON.parse(JSON.stringify(row));
  ruleForm.value.per_UserPwd = "";
  isShowDialog.value = true;
};

// 关闭弹窗
const closeDialog = () => {
  emit("reloadTable");
  isShowDialog.value = false;
};

// 取消
const cancel = () => {
  isShowDialog.value = false;
};
// 提交
const submit = async () => {
  ruleFormRef.value.validate(async (isValid: boolean, fields?: any) => {
    if (isValid) {
      let values = { ...ruleForm.value };
      if (values.per_TypeId != 2) {
        delete values.per_UserName;
        delete values.per_UserPwd;
        delete values.per_NormalizedPhone;
      } else {
        values.per_Type = values.per_TypeId;
      }
      await $http.post("/api/User/savePerson", {
        ...values,
        // per_TypeId: "1",
        ...(values.per_UserPwd ? { per_UserPwd: md5(values.per_UserPwd).toUpperCase() } : {})
      });
      await useDwStore().getTree();
      closeDialog();
    } else {
      ElMessage({
        message: `表单有${Object.keys(fields).length}处验证失败，请修改后再提交`,
        type: "error"
      });
    }
  });
};

// 页面加载时
onMounted(async () => {});

//将属性或者函数暴露给父组件
defineExpose({ openDialog });
</script>
