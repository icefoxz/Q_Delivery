<template>
  <div class="swiper-container">
    <el-dialog v-model="isShowDialog" :title="props.title" :width="700" draggable>
      <el-form :model="ruleForm" ref="ruleFormRef" size="default" label-width="100px" :rules="rules">
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="排序" prop="expand_Order">
              <el-slider class="pl10" v-model="ruleForm.expand_Order" show-input />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="上级字典">
              <el-select v-model="ruleForm.parentId" class="m-2" placeholder="请选择上级字典" size="large" filterable>
                <el-option
                  v-for="item in list"
                  :disabled="item.id == ruleForm.id"
                  :key="item.id"
                  :label="item.dict_Name"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="名称" prop="dict_Name">
              <el-input v-model="ruleForm.dict_Name" placeholder="请输入名称" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="字典Key" prop="dict_Key">
              <el-input v-model="ruleForm.dict_Key" placeholder="请输入字典Key" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="字典value" prop="dict_Value">
              <el-input v-model="ruleForm.dict_Value" placeholder="请输入字典value" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="是否内置">
              <el-switch v-model="ruleForm.isSystemBuilt" active-text="是" inactive-text="否" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="描述" prop="expand_Desc">
              <el-input type="textarea" :rows="5" v-model="ruleForm.expand_Desc" placeholder="请输入描述" clearable />
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
import { useDicStore } from "@/stores/modules/dic";
const { list } = storeToRefs(useDicStore());
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
  parentId: [{ required: true, message: "请选择上级字典！", trigger: "blur" }],
  dict_Name: [{ required: true, message: "请输入名称", trigger: "blur" }],
  dict_Key: [{ required: true, message: "请输入字典Key", trigger: "blur" }],
  dict_Value: [{ required: true, message: "请输入字典value", trigger: "blur" }],
  expand_Desc: [{ required: true, message: "请输入描述", trigger: "blur" }]
});

// 打开弹窗
const openDialog = (row: any) => {
  ruleForm.value = JSON.parse(JSON.stringify(row));
  ruleForm.value = ruleForm.value ? ruleForm.value : 0;
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
      values.isSystemBuilt = !!values.isSystemBuilt;
      await $http.post("/api/System/saveSystemDict", values);
      await useDicStore().getTree();
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
