<template>
  <div class="swiper-container">
    <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
      <el-form :model="queryParams" ref="queryForm" :inline="true">
        <el-form-item label="标签名称" prop="tag_Name">
          <el-input v-model="queryParams.tag_Name" placeholder="请输入标签名称" clearable />
        </el-form-item>
        <el-form-item label="标签报告类型" prop="per_Name">
          <el-select v-model="queryParams.tag_Type" placeholder="请选择标签报告类型" clearable>
            <el-option label="标签" value="1" />
            <el-option label="报告" value="2" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button-group>
            <el-button type="primary" icon="Search" @click="handleQuery"> 查询 </el-button>
            <el-button icon="Refresh" @click="queryParams = {}"> 重置 </el-button>
          </el-button-group>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" :icon="Plus" @click="openAddSwiper"> 新增 </el-button>
        </el-form-item>
      </el-form>
    </el-card>
    <el-card shadow="hover" style="margin-top: 8px">
      <el-table
        :data="tableData"
        style="width: 100%; height: calc(100vh - 270px)"
        v-loading="loading"
        tooltip-effect="light"
        row-key="id"
        border
      >
        <!-- <el-table-column prop="id" label="Id" /> -->
        <el-table-column prop="tag_Name" label="标签名称" />
        <el-table-column prop="create_User" label="创建用户" />
        <el-table-column prop="tag_TypeName" label="所属类型" />

        <!-- <el-table-column prop="show" label="隐藏显示">
          <template #default="scope">
            <el-tag type="danger" v-if="!scope.row.isEnabled"> 隐藏</el-tag>
            <el-tag v-else> 显示 </el-tag>
          </template>
        </el-table-column> -->

        <el-table-column prop="expand_Desc" label="标签描述" />
        <el-table-column prop="create_Time" label="创建时间" />
        <el-table-column label="状态">
          <template #default="{ row }">
            <el-tag type="danger" v-if="!row.isEnable"> 禁用 </el-tag>
            <el-tag type="success" v-else> 启用 </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="200" align="center" fixed="right">
          <template #default="{ row }">
            <el-button :icon="Edit" size="small" text type="primary" @click="openEditSwiper(row)"> 编辑 </el-button>
            <el-button :icon="Delete" size="small" text type="danger" @click="delSwiper(row)"> 删除 </el-button>
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
</template>

<script lang="ts" setup name="swiper">
import { ElMessage, ElMessageBox } from "element-plus";
import { ref } from "vue";
// import { auth } from '/@/utils/authFunction';
//import { formatDate } from '/@/utils/formatTime';
import editDialog from "./component/editDialog.vue";
import { $http } from "@/api/testaxios/testaxios";
import { Plus, Edit, Delete } from "@element-plus/icons-vue";
import ColSetting from "@/components/ProTable/components/ColSetting.vue";
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
const handleQuery = async () => {
  loading.value = true;
  var res = await $http.get("/api/TagManager/getTagManagerList", { ...queryParams.value, isAll: true });
  tableData.value = res.data ?? [];
  loading.value = false;
};

// 打开新增页面
const openAddSwiper = () => {
  editSwiperTitle.value = "添加所属标签类型";
  editDialogRef.value.openDialog({});
};

// 打开编辑页面
const openEditSwiper = (row: any) => {
  editSwiperTitle.value = "编辑所属标签类型";
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
      await $http.delete("/api/TagManager/deleteTagManager", { id: row.id }, { params: { id: row.id }, body: { id: row.id } });
      handleQuery();
      ElMessage.success("删除成功");
    })
    .catch(() => {});
};

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
