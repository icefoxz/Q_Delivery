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
            <el-form-item label="上级菜单" prop="menu_ParentId">
              <el-select v-model="ruleForm.menu_ParentId" placeholder="请选择" clearable filterable>
                <el-option v-for="item in menus" :key="item.id" :label="item.meta.title" :value="item.id" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="菜单名称" prop="menu_Name">
              <el-input v-model="ruleForm.menu_Name" placeholder="请输入菜单名称" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="菜单url" prop="menu_Path">
              <el-input v-model="ruleForm.menu_Path" placeholder="请输入菜单路径" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="图标" prop="menu_Icon">
              <!-- <el-input v-model="ruleForm.menu_Icon" placeholder="请输入图标" clearable /> -->
              <ElIconPicker v-model="ruleForm.menu_Icon" text="选择图标"></ElIconPicker>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="路由name" prop="menu_FileName">
              <el-input v-model="ruleForm.menu_FileName" placeholder="请输入路由name" clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="页面路径 " prop="menu_ComponentName">
              <el-input v-model="ruleForm.menu_ComponentName" placeholder="请输入页面路径 " clearable />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="排序" prop="expand_Order">
              <el-slider class="pl10" v-model="ruleForm.expand_Order" show-input />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="链接类型" prop="isOuterJoin">
              <el-radio-group v-model="ruleForm.isOuterJoin">
                <el-radio :label="false">内部</el-radio>
                <el-radio :label="true">外链</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20" v-if="ruleForm.isOuterJoin">
            <el-form-item label="链接">
              <el-input v-model="ruleForm.menu_Link" placeholder="请输入链接 " clearable />
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
import ElIconPicker from "../component/ElIconPicker.vue";
// import { addSwiper, updateSwiper } from '/@/api/main/swiper';
//父级传递来的参数
var props = defineProps({
  title: {
    type: String,
    default: ""
  },
  menus: {
    type: Array as any,
    default: () => []
  }
});
//父级传递来的函数，用于回调
const emit = defineEmits(["reloadTable"]);
const ruleFormRef = ref();
const isShowDialog = ref(false);
const ruleForm = ref<any>({});
//自行添加其他规则
const rules = ref<FormRules>({
  menu_Name: [{ required: true, message: "请输入菜单名称", trigger: "blur" }],
  menu_Path: [{ required: true, message: "请输入菜单url", trigger: "blur" }],
  menu_Icon: [{ required: true, message: "请输入图标", trigger: "blur" }],
  menu_FileName: [{ required: true, message: "请输入路由名称", trigger: "blur" }],
  menu_ComponentName: [{ required: true, message: "请输入页面路径 ", trigger: "blur" }],
  isOuterJoin: [{ required: true, message: "请选择链接类型", trigger: "change" }]
});

// 打开弹窗
const openDialog = (row: any) => {
  ruleForm.value = { isOuterJoin: false, menu_Icon: "", ...JSON.parse(JSON.stringify(row)) };
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
      await $http.post("/api/MenuLimit/saveMenu", values);
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
