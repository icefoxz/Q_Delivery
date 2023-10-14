<template>
  <div class="swiper-container">
    <el-dialog v-model="isShowDialog" :title="props.title" :width="700" draggable>
      <el-form :model="ruleForm" ref="ruleFormRef" size="default" label-width="100px" :rules="rules">
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="人员" prop="person_Id">
              <el-select v-model="ruleForm.person_Id" class="m-2" placeholder="请选择人员" size="large" filterable>
                <el-option
                  @click.native="ruleForm.person_Name = item.per_Name"
                  :label="item.per_Name"
                  :value="item.id"
                  v-for="item in perList"
                  :key="item.id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="余额" prop="lingau_Balance">
              <el-input-number
                style="width: 200px"
                :row="5"
                v-model="ruleForm.lingau_Balance"
                placeholder="请输入余额"
                clearable
              />
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
// import { addSwiper, updateSwiper } from '/@/api/main/swiper';
//父级传递来的参数
var props = defineProps({
  title: {
    type: String,
    default: ""
  }
});

const perList = ref<any>([]);

const handleQuery = async () => {
  const res1 = await $http.get("/api/User/getPersonList", { per_Type: 2 });
  perList.value = res1.data ?? [];
};

//父级传递来的函数，用于回调
const emit = defineEmits(["reloadTable"]);
const ruleFormRef = ref();
const isShowDialog = ref(false);
const ruleForm = ref<any>({});
//自行添加其他规则
const rules = ref<FormRules>({
  person_Id: [{ required: true, message: "请选择人员！", trigger: "blur" }],
  lingau_Balance: [{ required: true, message: "请输入余额！", trigger: "blur" }]
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
      await $http.post("api/Lingau/saveLingau", values);
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
onMounted(async () => {
  handleQuery();
});

//将属性或者函数暴露给父组件
defineExpose({ openDialog });
</script>
