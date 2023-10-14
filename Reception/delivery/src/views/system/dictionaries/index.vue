<template>
  <div class="main-box">
    <TreeFilter label="name" title="字典搜索" v-model:id="dicId" @change="handleQuery" />
    <div class="table-box card">
      <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
        <el-form :model="queryParams" ref="queryForm" :inline="true">
          <el-form-item label="名称" prop="per_Name">
            <el-input v-model="queryParams.dict_Name" placeholder="请输入名称" clearable />
          </el-form-item>
          <el-form-item label="字典key" prop="per_Name">
            <el-input v-model="queryParams.dict_Key" placeholder="请输入字典key" clearable />
          </el-form-item>
          <el-form-item label="字典value" prop="per_Name">
            <el-input v-model="queryParams.dict_Value" placeholder="请输入字典value" clearable />
          </el-form-item>
          <el-form-item>
            <el-button-group>
              <el-button type="primary" icon="Search" @click="handleQuery"> 查询 </el-button>
              <el-button icon="Refresh" @click="queryParams = {}"> 重置 </el-button>
            </el-button-group>
          </el-form-item>
        </el-form>
      </el-card>
      <el-card shadow="hover" style="margin-top: 8px">
        <el-form-item>
          <el-button type="primary" :icon="Plus" @click="openAddSwiper"> 新增 </el-button>
        </el-form-item>
        <el-table
          :data="tableData"
          style="width: 100%; height: calc(100vh - 300px)"
          v-loading="loading"
          tooltip-effect="light"
          row-key="id"
          border
        >
          <el-table-column prop="dict_Name" label="名称" />
          <el-table-column prop="dict_Key" label="字典key" />
          <el-table-column prop="dict_Value" label="字典value" />

          <el-table-column label="是否是内置">
            <template #default="{ row }">
              <el-tag type="success" v-if="row.isSystemBuilt"> 是</el-tag>
              <el-tag type="info" v-else> 否 </el-tag>
            </template>
          </el-table-column>
          <el-table-column prop="create_User" label="创建用户" />
          <el-table-column prop="create_Time" label="创建时间">
            <template #default="{ row }">{{ setTime(row.create_Time) }}</template>
          </el-table-column>

          <el-table-column prop="dict_Value" label="描述" width="200">
            <template #default="{ row }">
              <el-popover :width="200" trigger="hover" :content="row.expand_Desc">
                <template #reference>
                  <div class="text-line-1">{{ row.expand_Desc }}</div>
                </template>
              </el-popover>
            </template>
          </el-table-column>

          <el-table-column prop="expand_Order" label="权重" />
          <el-table-column label="操作" width="200" align="center" fixed="right">
            <template #default="scope">
              <el-button :icon="Edit" size="small" text type="primary" @click="openEditSwiper(scope.row)"> 编辑 </el-button>
              <el-button :icon="Delete" size="small" text type="danger" @click="delSwiper(scope.row)"> 删除 </el-button>
            </template>
          </el-table-column>
        </el-table>
        <editDialog ref="editDialogRef" :title="editSwiperTitle" @reloadTable="handleQuery" />
      </el-card>
    </div>
  </div>
</template>

<script setup lang="tsx" name="deptManage">
import { $http } from "@/api/testaxios/testaxios";
import TreeFilter from "@/components/TreeFilter/index2.vue";
import { setTime } from "@/filters";
import { useDicStore } from "@/stores/modules/dic";
import { Delete, Edit, Plus } from "@element-plus/icons-vue";
import { ElMessage, ElMessageBox } from "element-plus";
import { ref } from "vue";
import editDialog from "./component/editDialog.vue";
const dicId = ref<any>("");
// import { pageSwiper, deleteSwiper } from '/@/api/main/swiper';
const editDialogRef = ref();
const loading = ref(false);
const tableData = ref<any>([]);
const queryParams = ref<any>({});
const editSwiperTitle = ref("");
// 查询操作
const handleQuery = async () => {
  const res1 = await $http.get("/api/System/getSystemDictList", { ParentId: dicId.value, ...queryParams.value });
  tableData.value = res1.data ?? [];
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
      await $http.delete("/api/System/deleteSystemDic", { id: row.id });
      useDicStore().getTree();
      handleQuery();
      ElMessage.success("删除成功");
    })
    .catch(() => {});
};

handleQuery();
</script>
