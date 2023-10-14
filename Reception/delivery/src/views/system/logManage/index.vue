<template>
  <div class="main-box">
    <div class="table-box">
      <el-form :model="queryParams" ref="queryForm" :inline="true">
        <el-form-item label="IP">
          <el-input v-model="queryParams.log_OptIP" placeholder="请输入IP" clearable />
        </el-form-item>
        <el-form-item label="日志内容">
          <el-input v-model="queryParams.log_Message" placeholder="请输入日志内容" clearable />
        </el-form-item>

        <el-form-item label="方式">
          <el-select v-model="queryParams.log_ApiMethod" placeholder="请选择方式" clearable>
            <el-option label="get" value="get" />
            <el-option label="post" value="post" />
            <el-option label="put" value="put" />
            <el-option label="delete" value="delete" />
          </el-select>
        </el-form-item>

        <el-form-item>
          <el-button-group>
            <el-button type="primary" icon="Search" @click="getList"> 查询 </el-button>
            <el-button icon="Refresh" @click="queryParams = {}"> 重置 </el-button>
          </el-button-group>
        </el-form-item>
      </el-form>
      <ProTable
        ref="proTable"
        title="日志列表"
        row-key="id"
        :indent="20"
        :columns="columns"
        :request-api="getList"
        :request-auto="false"
        :search-col="{ xs: 1, sm: 1, md: 2, lg: 3, xl: 3 }"
        :data="tableData"
      >
        <template #pagination>
          <el-pagination
            class="mt20"
            v-model:currentPage="pageable.current_PageIndex"
            v-model:page-size="pageable.page_Size"
            :total="pageable.total"
            small
            background
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            layout="total, prev, pager, next"
          />
        </template>
      </ProTable>
    </div>
  </div>
</template>

<script setup lang="tsx" name="jobManage">
import { onMounted, ref } from "vue";
import { SysLog } from "@/api/interface";
// import { genderType } from "@/utils/serviceDict";
// import { ElMessage } from "element-plus";
import ProTable from "@/components/ProTable/index.vue";
// import { CirclePlus, Delete, EditPen } from "@element-plus/icons-vue";
import { ColumnProps, ProTableInstance } from "@/components/ProTable/interface";
import { getUserTreeList } from "@/api/modules/user"; // getUserDepartment
import { $http } from "@/api/testaxios/testaxios";

const queryParams = ref<any>({});
onMounted(() => {
  // getTreeFilter();
});
const tableData = ref<any>([]);
const pageable = ref<any>({});
const tableParams = ref({
  current_PageIndex: 1,
  page_Size: 15
  // total: 0
});

const getList = async (e?: any) => {
  try {
    const res: any = await $http.get("/api/System/getLogPageList", { ...queryParams.value, ...e, ...tableParams.value });
    tableData.value = res.data.currentPageData ?? [];
    pageable.value.current_PageIndex = res.data.current_PageIndex;
    pageable.value.page_Size = res.data.page_Size;
    pageable.value.total = res.data.total;
  } catch (error) {}
};
getList();
// 获取 ProTable 元素，调用其获取刷新数据方法（还能获取到当前查询参数，方便导出携带参数）
const proTable = ref<ProTableInstance>();

// 如果表格需要初始化请求参数，直接定义传给 ProTable(之后每次请求都会自动带上该参数，此参数更改之后也会一直带上，改变此参数会自动刷新表格数据)

// 表格配置项
const columns: ColumnProps<SysLog.ResSysLogList>[] = [
  { prop: "log_OptIP", label: "操作IP" },
  { prop: "log_Type", label: "日志类型" },
  { prop: "log_ApiMethod", label: "请求方式" },
  { prop: "log_ApiPath", label: "接口路径" },
  {
    prop: "log_OptIp",
    label: "操作IP"
    // search: {
    //   el: "input",
    //   props: { placeholder: "请输入日志名称" }
    // }
  },
  { prop: "log_ElapsedMilliseconds", label: "请求市场" },
  { prop: "log_Browser", label: "浏览器" },
  { prop: "log_Os", label: "操作系统" },
  { prop: "log_Message", label: "接口请求结果" },
  { prop: "log_OptTime", label: "操作时间" },
  { prop: "log_Params", label: "入参" },
  { prop: "log_Device", label: "设备" },
  { prop: "log_BrowserInfo", label: "浏览器信息" },
  { prop: "create_User", label: "操作用户" }
];
const handleSizeChange = (val: any) => {
  tableParams.value.page_Size = val;
  getList();
};
const handleCurrentChange = (val: any) => {
  tableParams.value.current_PageIndex = val;
  getList();
};
</script>
