<template>
  <el-dialog v-model="dialogVisible" title="订单跟踪" width="60%" :destroy-on-close="false">
    <ProTable
      ref="proTable"
      title="订单列表"
      row-key="id"
      :indent="20"
      :columns="columns"
      :request-api="getList"
      :data="tableData"
      :request-auto="false"
      :init-param="initParam"
      :search-col="{ xs: 1, sm: 1, md: 2, lg: 3, xl: 3 }"
    >
      <!-- 表格操作 -->
      <template #operation="{ row }">
        <el-button type="danger" v-if="row.isEnable" @click="disFn(row)"> 废除 </el-button>
        <el-button type="info" v-else> 废除 </el-button>
      </template>
      <template #isEnable="{ row }">
        <el-button type="primary" v-if="row.isEnable"> 有效 </el-button>
        <el-button type="info" v-else> 无效 </el-button>
      </template>
      <template #pagination>
        <el-pagination
          class="mt20"
          v-model:currentPage="tableParams.current_PageIndex"
          v-model:page-size="tableParams.page_Size"
          :total="tableParams.total"
          small
          background
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          layout="total, prev, pager, next"
        />
      </template>
    </ProTable>
    <el-divider content-position="left" style="margin-top: 50px">添加跟踪信息</el-divider>
    <el-form
      ref="ruleFormRef"
      label-width="120px"
      label-suffix=" :"
      :rules="rules"
      :disabled="dialogProps.isView"
      :model="form"
      :hide-required-asterisk="dialogProps.isView"
      style="margin-bottom: 30px"
      inline
    >
      <el-form-item label="订单报告类型" prop="tagManager_Id">
        <el-select v-model="form!.tagManager_Id" placeholder="请选择" clearable filterable>
          <el-option
            v-for="item in tagList.filter((f: any) => f.tag_Type == 2)"
            :key="item.id"
            :label="item.tag_Name"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="订单报告说明" prop="order_TagOrReport_Name">
        <el-input
          type="textarea"
          :row="3"
          v-model="form!.order_TagOrReport_Name"
          placeholder="请填写订单报告说明"
          clearable
        ></el-input>
      </el-form-item>
      <el-form-item>
        <el-button v-show="!dialogProps.isView" type="primary" @click="handleSubmit"> 保存 </el-button>
        <el-button @click="dialogVisible = false"> 清空 </el-button>
      </el-form-item>
    </el-form>
    <!-- <template #footer>
      <el-button @click="dialogVisible = false"> 关闭 </el-button>
    </template> -->
  </el-dialog>
</template>

<script setup lang="ts" name="orderDrawer">
import { ref, reactive, watch } from "vue";
// import { genderType } from "@/utils/serviceDict";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { OrderFollow } from "@/api/interface";
// import UploadImg from "@/components/Upload/Img.vue";
// import UploadImgs from "@/components/Upload/Imgs.vue";
import ProTable from "@/components/ProTable/index.vue";
import { ColumnProps } from "@/components/ProTable/interface";
import { getUserTreeList } from "@/api/modules/user";
import { $http } from "@/api/testaxios/testaxios";

const tableParams = ref({
  current_PageIndex: 1,
  page_Size: 10,
  total: 0
});
// 改变页面容量
const handleSizeChange = (val: number) => {
  tableParams.value.page_Size = val;
  getList({});
};

// 改变页码序号
const handleCurrentChange = (val: number) => {
  tableParams.value.current_PageIndex = val;
  getList({});
};
const props = defineProps<{ tagList: any[] }>();

const initParam = reactive({ departmentId: "" });

const tableData = ref();

const rules = reactive({
  tagManager_Id: [{ required: true, message: "请选择" }],
  order_TagOrReport_Name: [{ required: true, message: "请填写订单报告说明" }]
});
// 表格配置项
const columns: ColumnProps<OrderFollow.ResOrderFollowList>[] = [
  // { type: "index", label: "#", width: 80 },
  {
    prop: "order_Name",
    label: "订单名称",
    search: {
      el: "input",
      props: { placeholder: "请输入订单名称" }
    }
  },

  { prop: "tag_Name", label: "订单报告类型" },
  { prop: "order_TagOrReport_Name", label: "订单报告说明" },
  { prop: "create_User", label: "创建人" },
  {
    prop: "isEnable",
    label: "报告状态",
    enum: [
      { value: "", label: "全部" },
      { value: "true", label: "有效" },
      { value: "false", label: "无效" }
    ],
    search: {
      el: "select",
      props: { placeholder: "请选择报告状态" },
      defaultValue: "true"
    }
  },
  { prop: "operation", label: "操作", width: 300, fixed: "right" }
  // { prop: "create_Time", label: "创建时间", width: 180 }
];

const disFn = async (row: any) => {
  try {
    await ElMessageBox.confirm(`是否设置无效记录?`, "提示", {
      confirmButtonText: "确 定",
      cancelButtonText: "取 消",
      type: "warning"
    });
    await $http.post("api/Order/disableOrderTagOrReport", { id: row.id, isEnable: false });
    await ElMessage.success("操作成功");
    row.isEnable = false;
  } catch (error) {}

  // try {
  //   await ElMessageBox.confirm(`您确定${row.isEnable ? "启用" : "禁用"}该标签吗?`, "提示", {
  //     confirmButtonText: "确 定",
  //     cancelButtonText: "取 消",
  //     type: "warning"
  //   });
  //   await $http.post(`api/TagManager/disableTag?id=${row.id}&isEnable=${row.isEnable}`, { id: row.id, isEnable: row.isEnable });
  //   await ElMessage.success("操作成功");
  // } catch (error) {
  //   row.isEnable = !row.isEnable;
  // }
};

interface DialogProps {
  title: string;
  isView: boolean;
  row: Partial<OrderFollow.ResOrderFollowList>;
  api?: (params: any) => Promise<any>;
  getTableList?: () => void;
}

const dialogVisible = ref(false);
const dialogProps = ref<DialogProps>({
  isView: false,
  title: "",
  row: {}
});
const getList = async (e: any) => {
  try {
    const res: any = await $http.get("/api/Order/getOrderTagOrReportPageList", {
      order_Id: dialogProps.value.row.id,
      tag_Type: 2,
      ...e,
      ...tableParams.value
    });
    tableParams.value.total = res.data.total;
    tableData.value = res.data.currentPageData;
  } catch (error) {}
};

// 订单跟踪表单
const form = reactive({
  order_Id: "" as any,
  tag_Type: 2,
  tagManager_Id: "",
  order_TagOrReport_Name: ""
});

// 接收父组件传过来的参数
const acceptParams = (params: DialogProps) => {
  form.order_Id = params.row.id;
  dialogProps.value = params;
  dialogVisible.value = true;
  getList({});
};

// 提交数据（新增/编辑）
const ruleFormRef = ref<FormInstance>();
const handleSubmit = () => {
  ruleFormRef.value!.validate(async valid => {
    if (!valid) return;
    try {
      await $http.post("api/Order/saveOrderTagOrReport", form);
      await getList({});
      // await dialogProps.value.api!(dialogProps.value.row);
      ElMessage.success({ message: `添加成功` });
      // dialogProps.value.getTableList!();
      // dialogVisible.value = false;
    } catch (error) {}
  });
};

defineExpose({
  acceptParams
});
</script>
