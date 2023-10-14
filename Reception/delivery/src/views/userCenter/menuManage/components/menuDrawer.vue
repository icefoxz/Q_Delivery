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
      <el-form-item label="菜单名称" prop="title">
        <el-input v-model="drawerProps.row!.meta.title" placeholder="请填写菜单名称" clearable></el-input>
      </el-form-item>
      <el-form-item label="路由path" prop="path">
        <el-input v-model="drawerProps.row!.path" placeholder="请填写路由path" clearable></el-input>
      </el-form-item>
      <el-form-item label="上级菜单" prop="per_JobStatus">
        <el-select v-model="drawerProps.row!.per_JobStatus" placeholder="请选择上级菜单" clearable filterable>
          <!-- <el-option v-for="item in " :key="item.value" :label="item.label" :value="item.value" /> -->
        </el-select>
      </el-form-item>
      <el-form-item label="组件地址" prop="component">
        <el-input v-model="drawerProps.row!.component" placeholder="请填写组件地址" clearable></el-input>
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="drawerVisible = false"> 取消 </el-button>
      <el-button v-show="!drawerProps.isView" type="primary" @click="handleSubmit"> 确定 </el-button>
    </template>
  </el-drawer>
</template>

<script setup lang="ts" name="menuDrawer">
import { ref, reactive } from "vue";
// import { genderType } from "@/utils/serviceDict";
import { ElMessage, FormInstance } from "element-plus";
import { User } from "@/api/interface";
// import UploadImg from "@/components/Upload/Img.vue";
// import UploadImgs from "@/components/Upload/Imgs.vue";

const rules = reactive({
  path: [{ required: true, message: "请填写菜单路径" }],
  component: [{ required: true, message: "请填写组件" }],
  redirect: [{ required: true, message: "请填写菜单姓名" }],
  "meta.title": [{ required: true, message: "请填写菜单名称" }],
  "meta.icon": [{ required: true, message: "请选择菜单icon" }],
  "meta.isAffix": [{ required: true, message: "请选择" }]
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
      await drawerProps.value.api!(drawerProps.value.row);
      ElMessage.success({ message: `${drawerProps.value.title}用户成功！` });
      drawerProps.value.getTableList!();
      drawerVisible.value = false;
    } catch (error) {}
  });
};

defineExpose({
  acceptParams
});
</script>
