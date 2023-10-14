<template>
  <div class="swiper-container">
    <el-dialog v-model="isShowDialog" :title="props.title" :width="700" draggable>
      <el-form :model="ruleForm" ref="ruleFormRef" size="default" label-width="100px" :rules="rules">
        <el-row :gutter="35">
          <!-- <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="序号" prop="sortOrder">
              <el-input-number v-model="ruleForm.sortOrder" style="width: 200px" placeholder="请输入序号" clearable />
            </el-form-item>
          </el-col> -->
          <!-- <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="图片地址" prop="imageUrl">
              <el-upload list-type="picture-card" :limit="1" :show-file-list="false" :http-request="uploadImgHandle">
                <img v-if="ruleForm.imageUrl" :src="ruleForm.imageUrl" style="width: 100%; height: 100%; object-fit: contain" />
                <el-icon v-else><Plus /></el-icon>
              </el-upload>
            </el-form-item>
          </el-col> -->
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="标签" prop="tag_Id">
              <el-select v-model="ruleForm.tag_Id" class="m-2" placeholder="请选择标签" size="large" filterable>
                <el-option v-for="item in tagList" :key="item.id" :label="item.tag_Name" :value="item.id" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="类型" prop="tag_Type">
              <el-select v-model="ruleForm.tag_Type" class="m-2" placeholder="请选择类型" size="large">
                <el-option label="标签" :value="1" />
                <el-option label="报告" :value="2" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="标签描述" prop="expand_Desc">
              <el-input type="textarea" :rows="5" v-model="ruleForm.expand_Desc" placeholder="请输入标签描述" clearable />
            </el-form-item>
          </el-col>
          <!-- <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="隐藏显示" prop="isEnabled">
              <el-switch v-model="ruleForm.isEnabled" active-text="显示" inactive-text="隐藏" />
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
//父级传递来的函数，用于回调
const emit = defineEmits(["reloadTable"]);
const ruleFormRef = ref();
const isShowDialog = ref(false);
const ruleForm = ref<any>({});
//自行添加其他规则
const rules = ref<FormRules>({
  tag_Id: [{ required: true, message: "请选择标签！", trigger: "blur" }],
  tag_Type: [{ required: true, message: "请选择类型！", trigger: "blur" }],
  expand_Desc: [{ required: true, message: "请输入标签描述！", trigger: "blur" }]
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
      await $http.post("/api/TagManager/saveTagManager", values);
      closeDialog();
    } else {
      ElMessage({
        message: `表单有${Object.keys(fields).length}处验证失败，请修改后再提交`,
        type: "error"
      });
    }
  });
};

const tagList = ref<any>([]);

// 页面加载时
const getTag = async () => {
  var res = await $http.get("/api/TagManager/getTagList");
  tagList.value = res.data ?? [];
};
getTag();
//将属性或者函数暴露给父组件
defineExpose({ openDialog });
</script>
