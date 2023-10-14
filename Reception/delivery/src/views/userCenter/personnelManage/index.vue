<template>
  <div class="main-box">
    <TreeFilter v-model:id="dwId" label="dept_Name" @change="handleQuery" title="单位搜索" />
    <div class="table-box card">
      <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
        <el-form :model="queryParams" ref="queryForm" :inline="true">
          <el-form-item label="姓名" prop="per_Name">
            <el-input v-model="queryParams.per_Name" placeholder="请输入姓名或者手机号" clearable />
          </el-form-item>
          <el-form-item label="人员类型" prop="per_Type">
            <el-select v-model="queryParams.per_Type" placeholder="请选择人员类型" clearable>
              <el-option label="内部人员" value="1" />
              <el-option label="骑手" value="2" />
              <el-option label="普通用户" value="3" />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button-group>
              <el-button type="primary" icon="Search" @click="handleQuery"> 查询 </el-button>
              <el-button icon="Refresh" @click="queryParams = {}"> 重置 </el-button>
            </el-button-group>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" :icon="Plus" @click="openAddSwiper" :disabled="!dwId"> 新增 </el-button>
          </el-form-item>
        </el-form>
      </el-card>
      <el-card shadow="hover" style="margin-top: 8px">
        <el-table
          :data="tableData"
          style="width: 100%; height: calc(100vh - 320px)"
          v-loading="loading"
          tooltip-effect="light"
          row-key="id"
          border
        >
          <el-table-column prop="per_Name" label="姓名" width="100" />
          <el-table-column prop="dept_Name" label="单位" width="200" />
          <el-table-column prop="per_FullName" label="名称全拼" width="200" />
          <el-table-column prop="per_SimpleName" label="名称简拼" width="200" />
          <el-table-column prop="per_Type" label="人员类型" width="100" />
          <el-table-column prop="per_Phone" label="手机号" width="200" />
          <el-table-column prop="per_IdNo" label="身份证号" width="200" />
          <el-table-column label="出生日期" width="180">
            <template #default="{ row }">{{ setTime(row.per_Birthday) }}</template>
          </el-table-column>

          <el-table-column prop="per_Address" label="居住地址" width="200" />
          <!-- <el-table-column prop="per_Politics" label="政治面貌" width="100" /> -->
          <el-table-column label="工作状态" width="100">
            <template #default="scope">
              <el-tag type="danger" v-if="scope.row.per_JobStatus == 2"> 离职</el-tag>
              <el-tag v-else> 在职 </el-tag>
            </template>
          </el-table-column>
          <el-table-column label="操作" width="170" align="center" fixed="right">
            <template #default="scope">
              <el-button :icon="Edit" size="small" text type="primary" @click="openEditSwiper(scope.row)"> 编辑 </el-button>
              <el-button :icon="Delete" size="small" text type="danger" @click="delSwiper(scope.row)"> 删除 </el-button>
            </template>
          </el-table-column>
        </el-table>
        <!-- <el-pagination
          class="mt20"
          v-model:currentPage="tableParams.page"
          v-model:page-size="tableParams.pageSize"
          :total="tableParams.total"
          :page-sizes="[10, 20, 50, 100]"
          small
          background
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          layout="total, sizes, prev, pager, next, jumper"
        /> -->
        <editDialog ref="editDialogRef" :title="editSwiperTitle" @reloadTable="handleQuery" />
      </el-card>
    </div>
  </div>
</template>

<script setup lang="tsx" name="deptManage">
import { $http } from "@/api/testaxios/testaxios";
import TreeFilter from "@/components/TreeFilter/index.vue";
import { setTime } from "@/filters";
import { Delete, Edit, Plus } from "@element-plus/icons-vue";
import { ElMessage, ElMessageBox } from "element-plus";
import { ref } from "vue";
import editDialog from "./component/editDialog.vue";

// const { list: tableData } = storeToRefs(useDwStore());
// getTree();

const dwId = ref<any>("");

// import { pageSwiper, deleteSwiper } from '/@/api/main/swiper';
const editDialogRef = ref();
const loading = ref(false);
const tableData = ref<any>([]);
const queryParams = ref<any>({});
const tableParams = ref({
  page: 1,
  pageSize: 10,
  total: 0
});
const editSwiperTitle = ref("");
setTime;
// 查询操作
const handleQuery = async () => {
  const res1 = await $http.get("/api/User/getPersonList", { dept_Id: dwId.value, ...queryParams.value });
  tableData.value = res1.data ?? [];
};

// 打开新增页面
const openAddSwiper = () => {
  editSwiperTitle.value = "添加";
  editDialogRef.value.openDialog({ dept_Id: dwId.value });
};

// 打开编辑页面
const openEditSwiper = (row: any) => {
  editSwiperTitle.value = "编辑";
  editDialogRef.value.openDialog(row);
};

// 删除
const delSwiper = (row: any) => {
  ElMessageBox.confirm(`确定要删除吗?`, "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      await $http.delete("/api/User", { id: row.id });
      handleQuery();
      ElMessage.success("删除成功");
    })
    .catch(() => {});
};

// 选择树
const getPick = (val: any) => {};

// 改变页面容量
const handleSizeChange = (val: number) => {
  tableParams.value.pageSize = val;
  handleQuery();
};

// 改变页码序号
const handleCurrentChange = (val: number) => {
  tableParams.value.page = val;
  handleQuery();
};

handleQuery();
</script>
