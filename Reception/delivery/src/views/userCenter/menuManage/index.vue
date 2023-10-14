<template>
  <div class="swiper-container">
    <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
      <el-form :model="queryParams" ref="queryForm" :inline="true">
        <el-form-item label="菜单名称">
          <el-input v-model="queryParams.menu_Name" placeholder="请输入菜单名称" clearable />
        </el-form-item>
        <el-form-item>
          <el-button-group>
            <el-button type="primary" icon="Search" @click="handleQuery()"> 搜索 </el-button>
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
        style="width: 100%; height: calc(100vh - 320px)"
        v-loading="loading"
        tooltip-effect="light"
        row-key="id"
        border
      >
        <!-- <el-table-column type="index" label="序号" width="55" align="center" /> -->
        <!-- <el-table-column type="index" label="序号" /> -->
        <!-- <el-table-column prop="img" label="图片地址">
          <template #default="scope">
            <el-image
              style="width: 60px; height: 60px"
              :src="scope.row.imageUrl"
              :lazy="true"
              :hide-on-click-modal="true"
              :preview-src-list="[scope.row.imageUrl]"
              :initial-index="0"
              fit="scale-down"
              preview-teleported
            />
          </template>
        </el-table-column> -->
        <el-table-column prop="meta.title" label="菜单名称" />
        <el-table-column prop="name" label="菜单 name" />
        <el-table-column prop="path" label="菜单url" />
        <el-table-column label="菜单图标">
          <template #default="scope">
            <el-icon :size="18">
              <component :is="scope.row.meta.icon"></component>
            </el-icon>
          </template>
        </el-table-column>
        <el-table-column prop="component" label="页面路径" />
        <el-table-column prop="expand_Order" label="菜单权重" />
        <el-table-column prop="meta.isLink" label="外链地址" />
        <!-- <el-table-column prop="show" label="隐藏显示">
          <template #default="scope">
            <el-tag type="danger" v-if="!scope.row.isEnabled"> 隐藏</el-tag>
            <el-tag v-else> 显示 </el-tag>
          </template>
        </el-table-column> -->
        <el-table-column label="操作" width="200" align="center" fixed="right">
          <template #default="scope">
            <el-button :icon="Edit" size="small" text type="primary" @click="openEditSwiper(scope.row)"> 编辑 </el-button>
            <el-button :icon="Delete" size="small" text type="danger" @click="delSwiper(scope.row)"> 删除 </el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
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
      />
      <editDialog :menus="menus" ref="editDialogRef" :title="editSwiperTitle" @reloadTable="handleQuery" />
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
const menus = ref<any>([]);

// 树数据转扁平数据
function treeToArray(tree: any[]): any[] {
  return tree.reduce((arr, item) => {
    if (!Array.isArray(item.children)) return arr.concat(item);
    const children = item.children.map((v: any) => ({ ...v }));
    item = { ...item };
    delete item.children; // 扁平不需要children
    return arr.concat(item, ...treeToArray(children));
  }, [] as any[]);
}

// 查询操作
const handleQuery = async () => {
  loading.value = true;
  var res: any = await $http.get("/api/MenuLimit/getMenuListAsync", queryParams.value);

  tableData.value = res.data ?? [];
  if (!menus.value.length) {
    menus.value = treeToArray([...(res.data || [])]);
  }

  loading.value = false;
};

// 打开新增页面
const openAddSwiper = () => {
  editSwiperTitle.value = "添加菜单";
  editDialogRef.value.openDialog({});
};

// 打开编辑页面
const openEditSwiper = (row: any) => {
  editSwiperTitle.value = "编辑菜单";
  editDialogRef.value.openDialog({
    id: row.id,
    menu_Name: row.meta.title,
    menu_Path: row.path,
    menu_Icon: row.meta.icon,
    menu_FileName: row.name,
    menu_ComponentName: row.component,
    menu_ParentId: row.menu_ParentId
  });
};

// 删除
const delSwiper = (row: any) => {
  ElMessageBox.confirm(`确定要删除吗?`, "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      await $http.delete("/api/MenuLimit/deleteMenu", { id: row.id });
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
