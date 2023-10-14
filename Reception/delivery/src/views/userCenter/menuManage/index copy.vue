<template>
  <div class="main-box">
    <!-- <div class="table-box">
      <ProTable
        ref="proTable"
        title="菜单列表"
        row-key="id"
        :indent="20"
        :columns="columns"
        :data="menuData"
        :request-auto="false"
        :init-param="initParam"
        :search-col="{ xs: 1, sm: 1, md: 2, lg: 3, xl: 3 }"
      >
        <template #tableHeader>
          <el-button type="primary" :icon="CirclePlus" @click="openDrawer('新增')"> 新增菜单 </el-button>
        </template>
        <template #operation="scope">
          <el-button type="primary" link :icon="EditPen" @click="openDrawer('编辑', scope.row)"> 编辑 </el-button>
          <el-button type="primary" link :icon="Delete" @click="deleteAccount(scope.row)"> 删除 </el-button>
        </template>
      </ProTable>
      <personDrawer ref="drawerRef" />
      <ImportExcel ref="dialogRef" /> 
    </div> -->
    <div class="table-box">
      <ProTable
        ref="proTable"
        title="菜单列表"
        row-key="path"
        :indent="20"
        :columns="columns"
        :data="menuData"
        :pagination="false"
        :init-param="initParam"
      >
        <!-- 表格 header 按钮 -->
        <template #tableHeader>
          <el-button type="primary" :icon="CirclePlus" @click="openDrawer('新增')"> 新增菜单 </el-button>
        </template>
        <!-- 菜单图标 -->
        <template #icon="scope">
          <el-icon :size="18">
            <component :is="scope.row.meta.icon"></component>
          </el-icon>
        </template>
        <!-- 菜单操作 -->
        <template #operation="scope">
          <el-button type="primary" link :icon="EditPen" @click="openDrawer('编辑', scope.row)"> 编辑 </el-button>
          <el-button type="primary" link :icon="Delete" @click="deleteAccount(scope.row)"> 删除 </el-button>
        </template>
      </ProTable>
      <menuDrawer ref="drawerRef" />
      <ImportExcel ref="dialogRef" />
    </div>
  </div>
</template>

<script setup lang="tsx" name="menuManage">
import { onMounted, reactive, ref } from "vue";
import { Menu } from "@/api/interface";
// import { genderType } from "@/utils/serviceDict";
import { useHandleData } from "@/hooks/useHandleData";
// import { ElMessage } from "element-plus";
import ProTable from "@/components/ProTable/index.vue";
import ImportExcel from "@/components/ImportExcel/index.vue";
import menuDrawer from "@/views/userCenter/menuManage/components/menuDrawer.vue";
import { CirclePlus, Delete, EditPen } from "@element-plus/icons-vue";
import { ColumnProps, ProTableInstance } from "@/components/ProTable/interface";
import { deleteUser } from "@/api/modules/user"; // getMenuList getUserDepartment
import authMenuList from "@/assets/json/authMenuList.json";
const menuData = ref(authMenuList.data);

onMounted(() => {
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

// 获取 treeFilter 数据
// 当 proTable 的 requestAuto 属性为 false，不会自动请求表格数据，等待 treeFilter 数据回来之后，更改 initParam.departmentId 的值，才会触发请求 proTable 数据
// const treeFilterData = ref<any>([]);
// const getTreeFilter = async () => {
//   const { data } = await getUserDepartment();
//   treeFilterData.value = data;
//   initParam.departmentId = treeFilterData.value[1].id;
// };

// 模拟远程加载性别搜索框数据
// const loading = ref(false);
// const filterGenderEnum = ref<typeof genderType>([]);

// 远程搜索 输入值发生变化时调用 参数为当前输入值
// const remoteMethod = (query: string) => {
//   filterGenderEnum.value.length = 0;
//   if (!query) return;
//   loading.value = true;
//   setTimeout(() => {
//     loading.value = false;
//     filterGenderEnum.value.push(...genderType.filter(item => item.label.includes(query)));
//   }, 500);
// };

const columns: ColumnProps<Menu.ResMenuList>[] = [
  { type: "index", label: "#", width: 150 },
  { prop: "meta.title", label: "菜单名称", align: "left", search: { el: "input" } },
  { prop: "meta.icon", label: "菜单图标" },
  { prop: "name", label: "菜单 name", search: { el: "input" } },
  { prop: "path", label: "菜单路径", width: 300, search: { el: "input" } },
  { prop: "component", label: "组件路径", width: 300 },
  { prop: "operation", label: "操作", width: 250, fixed: "right" }
];

// 删除用户信息
const deleteAccount = async (params: Menu.ResMenuList) => {
  await useHandleData(deleteUser, { id: [params.id] }, `删除【${params.per_Name}】菜单`);
  proTable.value?.getTableList();
};

// 打开 drawer(新增、查看、编辑)
const drawerRef = ref<InstanceType<typeof menuDrawer> | null>(null);
const openDrawer = (title: string, row: Partial<Menu.ResMenuList> = {}) => {
  const params = {
    title,
    row: { ...row },
    isView: title === "查看",
    // api: title === "新增" ? addUser : title === "编辑" ? editUser : undefined,
    getTableList: proTable.value?.getTableList
  };
  drawerRef.value?.acceptParams(params);
};
// 表格配置项
// const columns: ColumnProps[] = [
//   { type: "index", label: "#", width: 150 },
//   { prop: "meta.title", label: "菜单名称", align: "left", search: { el: "input" } },
//   { prop: "meta.icon", label: "菜单图标" },
//   { prop: "name", label: "菜单 name", search: { el: "input" } },
//   { prop: "path", label: "菜单路径", width: 300, search: { el: "input" } },
//   { prop: "component", label: "组件路径", width: 300 },
//   { prop: "operation", label: "操作", width: 250, fixed: "right" }
// ];
</script>
