<template>
  <el-drawer v-model="drawerVisible" :destroy-on-close="true" size="450px" :title="`${drawerProps.title}单位`">
    <el-form
      ref="ruleFormRef"
      label-width="100px"
      label-suffix=" :"
      :rules="rules"
      :disabled="drawerProps.isView"
      :model="drawerProps.row"
      :hide-required-asterisk="drawerProps.isView"
    >
      <el-form-item label="职位名称" prop="job_Name">
        <el-input v-model="drawerProps.row!.job_Name" placeholder="请填写职位名称" clearable></el-input>
      </el-form-item>
      <el-form-item label="所属单位" prop="dept_Id">
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
import { Job } from "@/api/interface";
import { $http } from "@/api/testaxios/testaxios";
import { storeToRefs } from "pinia";
import { useDwStore } from "@/stores/modules/dw";
const { list } = storeToRefs(useDwStore());
// import UploadImg from "@/components/Upload/Img.vue";
// import UploadImgs from "@/components/Upload/Imgs.vue";

const rules = reactive({
  job_Name: [{ required: true, message: "请填写职位名称" }],
  dept_Id: [{ required: true, message: "请选择所属单位" }]
  // dept_Phone: [{ required: true, message: "请填写联系电话" }],
  // dept_PostCode: [{ required: true, message: "请填写单位邮编" }],
  // dept_Address: [{ required: true, message: "请填写单位地址" }]
});

//父级传递来的函数，用于回调
const emit = defineEmits(["reloadTable"]);
const isShowDialog = ref(false);

interface DrawerProps {
  title: string;
  isView: boolean;
  row: any;
  api?: (params: any) => Promise<any>;
  getTableList?: () => void;
}

const drawerVisible = ref(false);
const drawerProps = ref<DrawerProps>({
  isView: false,
  title: "",
  row: {}
});

// 关闭弹窗
const closeDialog = () => {
  emit("reloadTable");
  drawerVisible.value = false;
};

// 接收父组件传过来的参数
const acceptParams = (params: DrawerProps) => {
  drawerProps.value = params;
  drawerVisible.value = true;
};

// 提交数据（新增/编辑）
const ruleFormRef = ref();
const handleSubmit = () => {
  ruleFormRef.value.validate(async (isValid: boolean, fields?: any) => {
    if (isValid) {
      let values = drawerProps.value.row;
      await $http.post("/api/Job/saveJob", values);
      // await useDwStore().getTree();
      closeDialog();
    } else {
      ElMessage({
        message: `表单有${Object.keys(fields).length}处验证失败，请修改后再提交`,
        type: "error"
      });
    }
  });
};

defineExpose({
  acceptParams
});
</script>
