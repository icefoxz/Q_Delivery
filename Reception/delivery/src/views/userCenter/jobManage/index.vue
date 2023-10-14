<template>
  <div class="main-box">
    <TreeFilter v-model:id="dwId" label="dept_Name" @change="getList" title="单位搜索" />
    <div class="table-box">
      <ProTable
        ref="proTable"
        title="职位列表"
        row-key="id"
        :indent="20"
        :columns="columns"
        :request-api="getList"
        :request-auto="false"
        :init-param="initParam"
        :data="tableData"
        :pagination="false"
        :search-col="{ xs: 1, sm: 1, md: 2, lg: 3, xl: 3 }"
      >
        <!-- 表格 header 按钮 -->
        <template #tableHeader>
          <el-button type="primary" :icon="CirclePlus" @click="openDrawer('新增')"> 新增职位 </el-button>
        </template>
        <!-- 表格操作 -->
        <template #operation="scope">
          <!-- <el-button type="primary" link :icon="View" @click="openDrawer('查看', scope.row)"> 查看 </el-button> -->
          <el-button type="primary" link :icon="EditPen" @click="openDrawer('编辑', scope.row)"> 编辑 </el-button>
          <el-button type="primary" link :icon="Delete" @click="deleteAccount(scope.row)"> 删除 </el-button>
        </template>
      </ProTable>
      <jobDrawer ref="drawerRef" @reloadTable="getList" />
      <ImportExcel ref="dialogRef" />
    </div>
  </div>
</template>

<script setup lang="tsx" name="jobManage">
import { onMounted, reactive, ref } from "vue";
import { Person } from "@/api/interface";
// import { genderType } from "@/utils/serviceDict";
import { useHandleData } from "@/hooks/useHandleData";
// import { ElMessage } from "element-plus";
import ProTable from "@/components/ProTable/index.vue";
import ImportExcel from "@/components/ImportExcel/index.vue";
import jobDrawer from "@/views/userCenter/jobManage/components/jobDrawer.vue";
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
  // getTreeFilter();
  // ElNotification({
  //   title: "温馨提示",
  //   message: "该页面 ProTable 数据不会自动请求，需等待 treeFilter 数据请求完成之后，才会触发表格请求。",
  //   type: "info",
  //   duration: 10000
  // });
  // setTimeout(() => {
  //   ElNotification({
  //     title: "温馨提示",
  //     message: "该页面 ProTable 性别搜索框为远程数据搜索，详情可查看代码。",
  //     type: "info",
  //     duration: 10000
  //   });
  // }, 0);
});

// 获取 ProTable 元素，调用其获取刷新数据方法（还能获取到当前查询参数，方便导出携带参数）
const proTable = ref<ProTableInstance>();

// 如果表格需要初始化请求参数，直接定义传给 ProTable(之后每次请求都会自动带上该参数，此参数更改之后也会一直带上，改变此参数会自动刷新表格数据)
const initParam = reactive({ departmentId: "" });

// 表格配置项
const columns: ColumnProps<Person.ResPersonList>[] = [
  // { type: "index", label: "#", width: 80 },
  {
    prop: "job_Name",
    label: "职位名称",
    // sortable: true,
    // isFilterEnum: false,
    // enum: filterGenderEnum.value,
    search: {
      el: "input",
      props: { placeholder: "请输入职位名称" }
      // props: { placeholder: "请输入职位名称", filterable: true, remote: true, reserveKeyword: true, loading, remoteMethod }
      // reserveKeyword：筛选时，是否在选择选项后保留关键字
    }
    // render: scope => <>{scope.row.gender === 1 ? "男" : "女"}</>
  },
  { prop: "dept_Name", label: "所属单位" },
  // { prop: "create_User", label: "绑定人员" },
  { prop: "create_Time", label: "创建时间", width: 180 },
  { prop: "operation", label: "操作", width: 200, fixed: "right" }
];

const loading = ref(false);
const tableData = ref<any>([]);
let dept_Id = ref("");
// 查询操作
const getList = async (val: any) => {
  dept_Id = val;
  loading.value = true;
  var res = await $http.get("/api/Job/getJobList", { dept_Id: dwId.value || null, ...val });

  tableData.value = res.data ?? [];
  loading.value = false;
};

// 删除职位信息
const deleteAccount = async (row: any) => {
  ElMessageBox.confirm(`确定要删除吗?`, "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning"
  })
    .then(async () => {
      await $http.delete("/api/Job", { id: row.id }, { params: { id: row.id }, body: { id: row.id } });
      getList(dept_Id);
      ElMessage.success("删除成功");
    })
    .catch(() => {});
};

// 打开 drawer(新增、查看、编辑)
const drawerRef = ref<InstanceType<typeof jobDrawer> | null>(null);
const openDrawer = (title: string, row: Partial<Person.ResPersonList> = {}) => {
  const params = {
    title,
    row: { ...row },
    isView: title === "查看",
    // api: title === "新增" ? addUser : title === "编辑" ? editUser : undefined,
    getTableList: proTable.value?.getTableList
  };
  drawerRef.value?.acceptParams(params);
};
</script>
