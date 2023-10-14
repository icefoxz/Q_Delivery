<template>
  <div class="main-box">
    <TreeFilter v-model:id="dwId" label="dept_Name" @change="handleQuery" title="单位搜索" />
    <div class="table-box card">
      <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
        <el-form :model="queryParams" ref="queryForm" :inline="true">
          <el-form-item label="单位名称" prop="per_Name">
            <el-input v-model="queryParams.dept_Name" placeholder="请输入单位名称" clearable />
          </el-form-item>
          <el-form-item label="单位电话" prop="dept_Phone">
            <el-input v-model="queryParams.dept_Phone" placeholder="请输入单位电话" clearable />
          </el-form-item>
          <el-form-item>
            <el-button-group>
              <el-button type="primary" icon="Search" @click="handleQuery()"> 搜索 </el-button>
              <el-button icon="Refresh" @click="queryParams = {}"> 重置 </el-button>
            </el-button-group>
          </el-form-item>
        </el-form>
      </el-card>
      <el-card shadow="hover" style="margin-top: 8px">
        <el-form :model="queryParams" ref="queryForm" :inline="true">
          <el-form-item>
            <el-button type="primary" :icon="Plus" @click="openAddSwiper"> 新增 </el-button>
          </el-form-item>
        </el-form>
        <el-table
          :data="tableData"
          style="width: 100%; height: calc(100vh - 320px)"
          v-loading="loading"
          tooltip-effect="light"
          row-key="id"
          border
        >
          <el-table-column prop="dept_ParentName" label="上级单位名称" />
          <el-table-column prop="dept_Name" label="单位名称" />
          <el-table-column prop="dept_Address" label="单位地址" />
          <el-table-column prop="dept_Phone" label="单位电话" />
          <el-table-column prop="dept_PostCode" label="单位邮编" />
          <el-table-column prop="create_Time" label="创建时间">
            <template #default="{ row }">{{ setTime(row.create_Time) }}</template>
          </el-table-column>

          <el-table-column label="操作" width="200" align="center" fixed="right">
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
import { useDwStore } from "@/stores/modules/dw";
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
// 查询操作
const handleQuery = async (val?: any) => {
  // loading.value = true;
  // var res = await $http.get("/api/Dept/getDeptList");
  // const res = await $http.get("/api/Dept/getDeptList");
  const res1 = await $http.get("/api/Dept/getDeptList", { dept_ParentId: val ?? null, ...queryParams.value });
  tableData.value = res1.data ?? [];
  // loading.value = false;
};

// 打开新增页面
const openAddSwiper = () => {
  editSwiperTitle.value = "添加标签";
  editDialogRef.value.openDialog({});
};

// 打开编辑页面
const openEditSwiper = (row: any) => {
  editSwiperTitle.value = "编辑标签";
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
      await $http.delete("/api/Dept", { id: row.id });
      useDwStore().getTree();
      handleQuery({});
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
