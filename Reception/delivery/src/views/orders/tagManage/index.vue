<template>
  <div class="main-box">
    <!-- {{ setTime() }} -->
    <div class="table-box">
      <ProTable
        ref="proTable"
        title="标签列表"
        row-key="id"
        :indent="20"
        :columns="columns"
        :data="tableData"
        :init-param="initParam"
        :search-col="{ xs: 1, sm: 1, md: 2, lg: 3, xl: 3 }"
        :request-api="handleQuery"
      >
        <!-- 表格 header 按钮 -->
        <template #tableHeader>
          <el-button type="primary" :icon="Plus" @click="openAddSwiper"> 新增 </el-button>
        </template>
        <template #isEnable="{ row }">
          <el-switch v-model="row.isEnable" @change="statusFn(row)" />
        </template>
        <!-- <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
          <el-form :model="queryParams" ref="queryForm" :inline="true">
            <el-form-item label="标签名称">
              <el-input v-model="queryParams.tag_Name" placeholder="请输入菜单名称" clearable />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" :icon="Plus" @click="openAddSwiper"> 新增 </el-button>
            </el-form-item>
          </el-form>
        </el-card> -->
        <!-- 表格操作 -->
        <template #operation="{ row }">
          <el-button :icon="Edit" size="small" text type="primary" @click="openEditSwiper(row)"> 编辑 </el-button>
          <el-button :icon="Delete" size="small" text type="danger" @click="delSwiper(row)"> 删除 </el-button>
        </template>
      </ProTable>
      <editDialog ref="editDialogRef" :title="editSwiperTitle" @reloadTable="handleQuery" />
    </div>
  </div>
</template>

<script lang="ts" setup name="swiper">
import { ElMessage, ElMessageBox } from "element-plus";
import { onMounted, reactive, ref } from "vue";
// import { auth } from '/@/utils/authFunction';
//import { formatDate } from '/@/utils/formatTime';
import editDialog from "./component/editDialog.vue";
import { $http } from "@/api/testaxios/testaxios";
import { Plus, Edit, Delete } from "@element-plus/icons-vue";
import ColSetting from "@/components/ProTable/components/ColSetting.vue";
import { ColumnProps, ProTableInstance } from "@/components/ProTable/interface";
import { setTime } from "@/filters";
import { Order } from "@/api/interface";
import ProTable from "@/components/ProTable/index.vue";
// import { pageSwiper, deleteSwiper } from '/@/api/main/swiper';
const initParam = reactive({ departmentId: "" });
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

// 表格配置项
const columns: ColumnProps<Order.ResOrderList>[] = [
  // { type: "index", label: "#", width: 80 },
  {
    prop: "tag_Name",
    label: "标签名称",
    width: 150,
    // sortable: true,
    // isFilterEnum: false,
    // enum: filterGenderEnum.value,
    search: {
      el: "input",
      props: { placeholder: "请输入标签名称" }
      // props: { placeholder: "请输入订单名称", filterable: true, remote: true, reserveKeyword: true, loading, remoteMethod }
      // reserveKeyword：筛选时，是否在选择选项后保留关键字
    }
    // render: scope => <>{scope.row.gender === 1 ? "男" : "女"}</>
  },
  { prop: "tag_Name", label: "标签名称" },
  { prop: "expand_Desc", label: "标签描述" },
  { prop: "create_User", label: "创建者" },
  { prop: "isEnable", label: "状态" },
  // { prop: "create_Time", label: "创建时间" },
  { prop: "operation", label: "操作", width: 300, fixed: "right" }
];

// 查询操作
const handleQuery = async (e?: any) => {
  loading.value = true;
  var res = await $http.get("/api/TagManager/getTagList", { ...e, isAll: true });

  tableData.value = res.data ?? [];
  loading.value = false;
};

// 修改标签状态
const statusFn = async function (row: any) {
  try {
    await ElMessageBox.confirm(`您确定${row.isEnable ? "启用" : "禁用"}该标签吗?`, "提示", {
      confirmButtonText: "确 定",
      cancelButtonText: "取 消",
      type: "warning"
    });
    await $http.post(`api/TagManager/disableTag?id=${row.id}&isEnable=${row.isEnable}`, { id: row.id, isEnable: row.isEnable });
    await ElMessage.success("操作成功");
  } catch (error) {
    row.isEnable = !row.isEnable;
  }
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
      await $http.delete("/api/TagManager/deleteTag", { id: row.id });
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
