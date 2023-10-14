<template>
  <div class="swiper-container">
    <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
      <el-form :model="queryParams" ref="queryForm" :inline="true">
        <el-form-item label="名称" prop="per_Name">
          <el-input v-model="queryParams.limit_Name" placeholder="请输入名称" clearable />
        </el-form-item>

        <el-form-item>
          <el-button-group>
            <el-button type="primary" icon="Search" @click="getList"> 查询 </el-button>
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
        <!-- <el-table-column prop="id" label="Id" /> -->
        <el-table-column prop="limit_Name" label="权限名称" />
        <el-table-column prop="create_User" label="创建用户" />
        <el-table-column prop="create_Time" label="创建时间" />
        <el-table-column label="操作" width="280" align="center" fixed="right">
          <template #default="{ row }">
            <el-button size="small" text type="primary" @click="openMenu(row)"> 绑定菜单 </el-button>
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
      <editDialog ref="editDialogRef" :title="editSwiperTitle" @reloadTable="getList" />
    </el-card>

    <el-dialog v-model="show" title="绑定菜单" :width="700" draggable>
      <el-cascader :options="data" :props="{ multiple: true }" clearable v-model="form.menu_List" />
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="show = false" size="default">取 消</el-button>
          <el-button type="primary" @click="submit" size="default">确 定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="swiper">
import { onMounted, reactive, ref } from "vue";
import { ElMessage, ElMessageBox } from "element-plus";
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

// 树数据转扁平数据
function treeToArray(tree: any[]): any[] {
  return tree.reduce((arr, item) => {
    item.label = item.meta.title;
    item.value = item.id;
    if (Array.isArray(item.children)) treeToArray(item.children);
  }, [] as any[]);
}

onMounted(async () => {
  getList();
  try {
    var res: any = await $http.get("/api/MenuLimit/getMenuListAsync");
    treeToArray(res.data);
    data.value = res.data;
  } catch (error) {}
});

const show = ref(false);
const data = ref([]);
const form = reactive({
  id: "",
  menu_List: [] as any
});

async function openMenu(row: any) {
  try {
    const res = await $http.get("/api/MenuLimit/getSelectMenuList", { id: row.id });
    form.id = row.id;
    form.menu_List = res.data;
    show.value = true;
  } catch (error) {}
}

async function submit() {
  try {
    const obj: any = { ...form };
    obj.menu_List = form.menu_List.map((v: any) => ({ menu_IdArray: v, menu_Id: v[0] }));
    const res = await $http.post("/api/MenuLimit/saveLimitRelevancyMenu", obj);
    show.value = false;
  } catch (error) {}
}

// 查询操作
const getList = async () => {
  loading.value = true;
  var res = await $http.get("/api/MenuLimit/getLimitList", { ...queryParams.value });

  tableData.value = res.data ?? [];
  loading.value = false;
};

// 打开新增页面
const openAddSwiper = () => {
  editSwiperTitle.value = "添加权限";
  editDialogRef.value.openDialog({});
};

// 打开编辑页面
const openEditSwiper = (row: any) => {
  editSwiperTitle.value = "编辑权限";
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
      await $http.delete("/api/MenuLimit/deleteLimit", { id: row.id }, { params: { id: row.id }, body: { id: row.id } });
      getList();
      ElMessage.success("删除成功");
    })
    .catch(() => {});
};

// 改变页面容量
const handleSizeChange = (val: number) => {
  tableParams.value.pageSize = val;
  getList();
};

// 改变页码序号
const handleCurrentChange = (val: number) => {
  tableParams.value.page = val;
  getList();
};
</script>
