﻿<template>
  <div class="swiper-container">
    <el-dialog v-model="isShowDialog" :title="props.title" :width="700" draggable>
      <el-form :model="ruleForm" ref="ruleFormRef" size="default" label-width="100px" :rules="rules">
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="上级单位">
              <el-select v-model="ruleForm.dept_ParentId" class="m-2" placeholder="请选择上级单位" size="large" filterable>
                <el-option
                  v-for="item in list"
                  :disabled="item.id == ruleForm.id"
                  :key="item.id"
                  :label="item.dept_Name"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="单位名称" prop="dept_Name">
              <el-input v-model="ruleForm.dept_Name" placeholder="请输入单位名称" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="单位地址" prop="dept_Address">
              <el-input v-model="ruleForm.dept_Address" placeholder="请输入单位地址" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="单位电话" prop="dept_Phone">
              <el-input v-model="ruleForm.dept_Phone" placeholder="请输入单位电话" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="单位邮编" prop="dept_PostCode">
              <el-input v-model="ruleForm.dept_PostCode" placeholder="请输入单位邮编" clearable />
            </el-form-item>
          </el-col>
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
import { Plus } from "@element-plus/icons-vue";
import type { FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import { onMounted, ref } from "vue";
import { $http } from "@/api/testaxios/testaxios";
import { storeToRefs } from "pinia";
import { useDwStore } from "@/stores/modules/dw";
const { list } = storeToRefs(useDwStore());
// import { addSwiper, updateSwiper } from '/@/api/main/swiper';
//父级传递来的参数
var props = defineProps({
  title: {
    type: String,
    default: ""
  }
});
//父级传递来的函数，用于回调
const emit = defineEmits(["reloadTable"]);
const ruleFormRef = ref();
const isShowDialog = ref(false);
const ruleForm = ref<any>({});
//自行添加其他规则
const rules = ref<FormRules>({
  dept_ParentId: [{ required: true, message: "请选择上级单位！", trigger: "blur" }],
  dept_Name: [{ required: true, message: "请输入单位名称", trigger: "blur" }],
  dept_Address: [{ required: true, message: "请输入单位地址", trigger: "blur" }],
  dept_Phone: [{ required: true, message: "请输入单位电话", trigger: "blur" }],
  dept_PostCode: [{ required: true, message: "请输入单位邮编", trigger: "blur" }]
});

// 打开弹窗
const openDialog = (row: any) => {
  ruleForm.value = JSON.parse(JSON.stringify(row));
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
      let values = ruleForm.value;
      await $http.post("/api/Dept/saveDept", values);
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
