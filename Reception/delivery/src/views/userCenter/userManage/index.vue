<template>
  <div class="main-box">
    <TreeFilter v-model:id="dwId" label="dept_Name" @change="getList" title="单位搜索" />
    <div class="table-box">
      <ProTable
        ref="proTable"
        title="用户列表"
        row-key="id"
        :indent="20"
        :columns="columns"
        :request-api="getList"
        :request-auto="false"
        :init-param="initParam"
        :data="tableData"
        :search-col="{ xs: 1, sm: 1, md: 2, lg: 3, xl: 3 }"
      >
        <!-- 表格 header 按钮 -->
        <template #tableHeader>
          <el-button type="primary" :disabled="!dwId" :icon="CirclePlus" @click="openDrawer('新增')"> 新增用户 </el-button>
        </template>
        <template #isEnable="{ row }">
          <el-switch v-model="row.isEnable" @change="statusFn(row)" />
        </template>
        <!-- 表格操作 -->
        <template #operation="scope">
          <!-- <el-button type="primary" link :icon="View" @click="openDrawer('查看', scope.row)"> 查看 </el-button> -->
          <el-button type="primary" link :icon="EditPen" @click="openDrawer('编辑', scope.row)"> 编辑 </el-button>
          <el-button type="primary" link :icon="Delete" @click="deleteAccount(scope.row)"> 删除 </el-button>
        </template>
      </ProTable>
      <userDrawer ref="drawerRef" />
      <ImportExcel ref="dialogRef" />
    </div>
  </div>
</template>

<script setup lang="tsx" name="userManage">
import { onMounted, reactive, ref } from "vue";
import { Person } from "@/api/interface";
// import { genderType } from "@/utils/serviceDict";
import { useHandleData } from "@/hooks/useHandleData";
// import { ElMessage } from "element-plus";
import ProTable from "@/components/ProTable/index.vue";
import ImportExcel from "@/components/ImportExcel/index.vue";
import userDrawer from "@/views/userCenter/userManage/components/userDrawer.vue";
import { CirclePlus, Delete, EditPen } from "@element-plus/icons-vue";
import { ColumnProps, ProTableInstance } from "@/components/ProTable/interface";
import { getUserTreeList, deleteUser } from "@/api/modules/user"; // getUserDepartment
import { $http } from "@/api/testaxios/testaxios";
import { useDwStore } from "@/stores/modules/dw";
import TreeFilter from "@/components/TreeFilter/index.vue";
import { ElMessage, ElMessageBox } from "element-plus";

const dwId = ref<any>("");

const { list } = useDwStore();

onMounted(() => {
  getList("");
});

// 获取 ProTable 元素，调用其获取刷新数据方法（还能获取到当前查询参数，方便导出携带参数）
const proTable = ref<ProTableInstance>();

// 如果表格需要初始化请求参数，直接定义传给 ProTable(之后每次请求都会自动带上该参数，此参数更改之后也会一直带上，改变此参数会自动刷新表格数据)
const initParam = reactive({ departmentId: "" });

// 表格配置项
const columns: ColumnProps<Person.ResPersonList>[] = [
  // { type: "index", label: "#", width: 80 },
  // { prop: "id", label: "用户Id" },
  {
    prop: "user_LoginName",
    label: "登录名称",
    // sortable: true,
    // isFilterEnum: false,
    // enum: filterGenderEnum.value,
    search: {
      el: "input",
      props: { placeholder: "请输入登录名称" }
      // props: { placeholder: "请输入登录名称", filterable: true, remote: true, reserveKeyword: true, loading, remoteMethod }
      // reserveKeyword：筛选时，是否在选择选项后保留关键字
    }
    // render: scope => <>{scope.row.gender === 1 ? "男" : "女"}</>
  },
  { prop: "dept_Name", label: "单位名称" },
  { prop: "person_Name", label: "人员名称" },
  { prop: "limit_Name", label: "权限组名称" },
  { prop: "create_User", label: "创建用户" },
  { prop: "create_Time", label: "创建时间" },
  { prop: "isEnable", label: "状态" },
  { prop: "operation", label: "操作", width: 200, fixed: "right" }
];

const loading = ref(false);
const tableData = ref<any>([]);
let dept_Id = ref("");
// 查询操作
const getList = async (val: any) => {
  loading.value = true;
  var res = await $http.get("/api/User/getUserList", { dept_Id: dwId.value, ...val });

  tableData.value = res.data ?? [];
  loading.value = false;
};

// 修改标签状态
const statusFn = async function (row: any) {
  try {
    await ElMessageBox.confirm(`您确定${row.isEnable ? "启用" : "禁用"}该用户吗?`, "提示", {
      confirmButtonText: "确 定",
      cancelButtonText: "取 消",
      type: "warning"
    });
    await $http.post(`api/User/disableUser?id=${row.id}&isEnable=${row.isEnable}`, { id: row.id, isEnable: row.isEnable });
    await ElMessage.success("操作成功");
  } catch (error) {
    row.isEnable = !row.isEnable;
  }
};

// 删除用户信息
const deleteAccount = async (row: any) => {
  ElMessageBox.confirm(`确定要删除吗?`, "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      await $http.delete("/api/User/deleteUser", { id: row.id });
      getList(dept_Id);
      ElMessage.success("删除成功");
    })
    .catch(() => {});
};

// 打开 drawer(新增、查看、编辑)
const drawerRef = ref<InstanceType<typeof userDrawer> | null>(null);
const openDrawer = (title: string, row: Partial<Person.ResPersonList> = {}) => {
  row!.user_LoginPwd = "";
  const params = {
    title,
    row: { dept_Id: dwId.value, ...row },
    isView: title === "查看",
    // api: title === "新增" ? addUser : title === "编辑" ? editUser : undefined,
    getTableList: proTable.value?.getTableList
  };
  drawerRef.value?.acceptParams(params);
};
</script>
