<template>
  <div class="main-box">
    <!-- {{ setTime() }} -->
    <div class="table-box">
      <!-- <ProTable
        ref="proTable"
        title="订单列表"
        row-key="id"
        :indent="20"
        :columns="columns"
        :data="tableData"
        :init-param="initParam"
        :search-col="{ xs: 1, sm: 1, md: 2, lg: 3, xl: 3 }"
        :request-api="getOrderList"
      >
        <template #tableHeader>
          <el-button type="primary" :icon="CirclePlus" @click="openDrawer('新增')"> 新增订单 </el-button>
        </template>
        <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
          <el-form :model="queryParams" ref="queryForm" :inline="true">
            <el-form-item label="标签名称">
              <el-input v-model="queryParams.tag_Name" placeholder="请输入菜单名称" clearable />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" :icon="CirclePlus" @click="openDrawer('新增')"> 新增订单 </el-button>
            </el-form-item>
          </el-form>
        </el-card>
        <template #operation="{ row }">
          <el-button type="primary" link @click="openDrawer('编辑', row)"> 编辑 </el-button>
          <el-button type="primary" link @click="openTag(row)"> 打标签 </el-button>
          <el-button type="primary" link @click="openTag(row)"> 详情 </el-button>
          <el-button type="primary" link @click="openFollow('订单跟踪', row)"> 订单跟踪 </el-button>
        </template>
      </ProTable> -->

      <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
        <div class="pb20">
          <el-form :model="queryParams" ref="queryForm" :inline="true">
            <div>
              <el-form-item label="订单名称" prop="order_Name">
                <el-input v-model="queryParams.order_Name" placeholder="请输入订单名称" clearable />
              </el-form-item>
              <el-form-item label="配送人电话" prop="order_RiderPhone">
                <el-input v-model="queryParams.order_RiderPhone" placeholder="请输入配送人电话" clearable />
              </el-form-item>
              <el-form-item label="收货人" prop="order_ReceiverName">
                <el-input v-model="queryParams.order_ReceiverName" placeholder="请输入收货人" clearable />
              </el-form-item>
              <el-form-item label="收货人电话" prop="order_ReceiverPhone">
                <el-input v-model="queryParams.order_ReceiverPhone" placeholder="请输入收货人电话" clearable />
              </el-form-item>
              <el-form-item label="物品名称" prop="order_GoodsName">
                <el-input v-model="queryParams.order_GoodsName" placeholder="请输入物品名称" clearable />
              </el-form-item>
            </div>
            <div>
              <el-form-item label="订单状态" prop="order_Status">
                <el-select v-model="queryParams.order_Status" placeholder="请选择订单状态" clearable>
                  <el-option v-for="item in OrderStatus" :label="item.dict_Name" :value="item.dict_Key" :key="item.dict_Key" />
                  <!-- <el-option v-for="item in orderStatusList" :key="item.value" :label="item.label" :value="item.value" /> -->
                </el-select>
              </el-form-item>
              <el-form-item label="日期" prop="per_Name">
                <el-date-picker
                  v-model="queryParams.time"
                  type="daterange"
                  range-separator="至"
                  start-placeholder="开始时间"
                  end-placeholder="结束时间"
                />
              </el-form-item>
              <el-form-item label="标签报告类型" prop="per_Name">
                <el-select v-model="queryParams.tag_Type" placeholder="请选择标签报告类型" clearable>
                  <el-option label="标签" value="Tag" />
                  <el-option label="报告" value="TagReport" />
                </el-select>
              </el-form-item>

              <el-form-item label="标签" prop="per_Name" v-if="queryParams.tag_Type == 'Tag'">
                <el-select
                  v-model="queryParams.tagManager_IdList"
                  placeholder="请选择标签"
                  clearable
                  filterable
                  multiple
                  collapse-tags
                >
                  <el-option
                    v-for="item in tagList.filter((f: any) => f.tag_Type == 1)"
                    :key="item.id"
                    :label="item.tag_Name"
                    :value="item.id"
                  />
                </el-select>
              </el-form-item>
              <el-form-item label="报告" prop="per_Name" v-if="queryParams.tag_Type == 'TagReport'">
                <el-select
                  v-model="queryParams.tagManager_IdList"
                  placeholder="请选择报告"
                  clearable
                  filterable
                  multiple
                  collapse-tags
                >
                  <el-option
                    v-for="item in tagList.filter((f: any) => f.tag_Type == 2)"
                    :key="item.id"
                    :label="item.tag_Name"
                    :value="item.id"
                  />
                </el-select>
              </el-form-item>

              <el-form-item>
                <el-button-group>
                  <el-button type="primary" icon="Search" @click="getOrderList"> 查询 </el-button>
                  <el-button icon="Refresh" @click="queryParams = {}"> 重置 </el-button>
                </el-button-group>
              </el-form-item>
              <el-form-item>
                <el-button type="primary" :icon="Plus" @click="openDrawer('新增')"> 新增 </el-button>
              </el-form-item>
            </div>
          </el-form>
        </div>
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
          <el-table-column prop="order_Name" label="订单名称" width="150" />
          <el-table-column prop="order_RiderName" label="配送人" width="150" />
          <el-table-column prop="order_RiderPhone" label="配送人手机号" width="150" />
          <el-table-column prop="order_ReceiverName" label="收货人名称" width="150" />
          <el-table-column prop="order_ReceiverPhone" label="收货人手机号" width="150" />
          <el-table-column prop="order_GoodsName" label="物品名称" width="150" />
          <el-table-column prop="order_GoodsType" label="物品类型" width="150" />
          <el-table-column prop="order_GoodsWeight" label="物品重量" width="150" />
          <el-table-column prop="order_GoddsNums" label="物品件数" width="150" />
          <el-table-column prop="order_GoodsLong" label="物品长度" width="150" />
          <el-table-column prop="order_GoodsWidth" label="物品宽度" width="150" />
          <el-table-column prop="order_GoodsHight" label="物品高度" width="150" />
          <el-table-column prop="order_GoodsPrice" label="商品价格" width="150" />
          <el-table-column prop="order_GoodsDelivery" label="运送费" width="150" />
          <el-table-column prop="order_PathDistance" label="配送距离" width="150" />
          <el-table-column prop="order_PayIdentity" label="付款标识" width="150" />
          <el-table-column prop="order_Status" label="订单状态" width="150" />
          <el-table-column prop="create_Time" label="创建时间" width="150" />
          <el-table-column prop="create_User" label="创建人" width="150" />
          <el-table-column prop="operation" label="操作" width="150" />

          <el-table-column label="操作" width="300" align="center" fixed="right">
            <template #default="{ row }">
              <el-button type="primary" link @click="openDrawer('编辑', row)"> 编辑 </el-button>
              <el-button type="primary" link @click="openTag(row)"> 打标签 </el-button>
              <el-button type="primary" link @click="detailFn(row)"> 详情 </el-button>
              <el-button type="primary" link @click="openFollow('订单跟踪', row)"> 订单跟踪 </el-button>
            </template>
          </el-table-column>
        </el-table>
        <el-pagination
          class="mt20"
          v-model:currentPage="tableParams.current_PageIndex"
          v-model:page-size="tableParams.page_Size"
          :total="tableParams.total"
          small
          background
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          layout="total, prev, pager, next"
        />
      </el-card>

      <orderDrawer ref="drawerRef" :tagList="tagList" />
      <followDialog ref="followRef" :tagList="tagList" />
    </div>

    <el-dialog v-model="isShowDialog" title="打标签" :width="500" draggable>
      <div style="display: flex; justify-content: end">
        <el-button :type="disabled ? 'primary' : ''" size="small" @click="disabled = !disabled">
          {{ disabled ? "编辑" : "取消" }}
        </el-button>
      </div>
      <el-checkbox-group v-model="checkList" :disabled="disabled">
        <div v-for="item in tagList.filter((f: any) => f.tag_Type == 1)" :key="item.id">
          <el-checkbox :label="item.id">{{ item.tag_Name }}</el-checkbox>
        </div>
      </el-checkbox-group>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="isShowDialog = false" size="default">取 消</el-button>
          <el-button type="primary" @click="submit" size="default" :disabled="disabled">确 定</el-button>
        </span>
      </template>
    </el-dialog>

    <el-dialog v-model="isShowDialog1" title="详情" :width="1000" draggable>
      <el-descriptions border>
        <el-descriptions-item label="订单名称">{{ rowCopy.order_Name }}</el-descriptions-item>
        <el-descriptions-item label="配送人">{{ rowCopy.order_RiderName }}</el-descriptions-item>
        <el-descriptions-item label="配送人手机号">{{ rowCopy.order_RiderPhone }}</el-descriptions-item>
        <el-descriptions-item label="收货人名称">{{ rowCopy.order_ReceiverName }}</el-descriptions-item>
        <el-descriptions-item label="收货人手机号">{{ rowCopy.order_ReceiverPhone }}</el-descriptions-item>
        <el-descriptions-item label="在职状态">
          {{ jobStatusList.find((f:any) => f.dict_Key == rowCopy.order_JobStatus).dict_Name  }}
        </el-descriptions-item>
        <el-descriptions-item label="物品名称">{{ rowCopy.order_GoodsName }}</el-descriptions-item>
        <!-- <el-descriptions-item label="物品类型">{{ rowCopy.order_GoodsType }}</el-descriptions-item> -->
        <el-descriptions-item label="物品重量">{{ rowCopy.order_GoodsWeight }}</el-descriptions-item>
        <el-descriptions-item label="物品件数">{{ rowCopy.order_GoddsNums }}</el-descriptions-item>
        <el-descriptions-item label="物品长度">{{ rowCopy.order_GoodsLong }}</el-descriptions-item>
        <el-descriptions-item label="物品宽度">{{ rowCopy.order_GoodsWidth }}</el-descriptions-item>

        <el-descriptions-item label="物品高度">{{ rowCopy.order_GoodsHight }}</el-descriptions-item>
        <el-descriptions-item label="商品价格">{{ rowCopy.order_GoodsPrice }}</el-descriptions-item>
        <el-descriptions-item label="开始经度">{{ rowCopy.order_BenginLng }}</el-descriptions-item>
        <el-descriptions-item label="结束经度">{{ rowCopy.order_EndLng }}</el-descriptions-item>
        <el-descriptions-item label="开始纬度">{{ rowCopy.order_BenginLat }}</el-descriptions-item>
        <el-descriptions-item label="结束纬度">{{ rowCopy.order_EndLat }}</el-descriptions-item>
        <el-descriptions-item label="开始地理Id">{{ rowCopy.order_BenginPlaceId }}</el-descriptions-item>
        <el-descriptions-item label="结束地理Id">{{ rowCopy.order_EndPlaceId }}</el-descriptions-item>
        <el-descriptions-item label="舟属">
          {{ Naviculus.find((f:any) => f.dict_Key == rowCopy.order_StateId).dict_Name  }}
        </el-descriptions-item>
        <el-descriptions-item label="配送距离">{{ rowCopy.order_PathDistance }}</el-descriptions-item>
        <el-descriptions-item label="运送费">{{ rowCopy.order_GoodsDelivery }}</el-descriptions-item>
        <el-descriptions-item label="付费类型">{{ rowCopy.order_PayType }}</el-descriptions-item>
        <el-descriptions-item label="付款标识">{{ rowCopy.order_PayIdentity }}</el-descriptions-item>
        <el-descriptions-item label="订单状态" :span="4">
          {{ OrderStatus.find((f:any) => f.dict_Key == rowCopy.order_Status).dict_Name  }}
        </el-descriptions-item>
        <el-descriptions-item label="附件">
          <el-image
            class="pr10"
            v-for="(url, i) in imgs"
            :key="url"
            style="width: 100px; height: 100px"
            :src="url"
            :zoom-rate="1.2"
            :preview-src-list="imgs"
            :initial-index="i"
            fit="cover"
          />
        </el-descriptions-item>
      </el-descriptions>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="isShowDialog1 = false" size="default">取 消</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="tsx" name="deptManage">
import { Order } from "@/api/interface";
import { ElMessage } from "element-plus";
import { onMounted, ref } from "vue";
// import { genderType } from "@/utils/serviceDict";
import { ProTableInstance } from "@/components/ProTable/interface";
import followDialog from "@/views/orders/orderManage/components/followDialog.vue";
import orderDrawer from "@/views/orders/orderManage/components/orderDrawer.vue";
import { Plus } from "@element-plus/icons-vue";
// import { getOrderList } from "@/api/modules/order";
import { $http } from "@/api/testaxios/testaxios";
import { useUserStore } from "@/stores/modules/user";

const OrderStatus = useUserStore().findDic("OrderStatus");
const jobStatusList = useUserStore().findDic("JobStatus");
const Naviculus = useUserStore().findDic("Naviculus");
const rowCopy = ref<any>({});
const isShowDialog1 = ref(false);
const detailFn = (row: any) => {
  rowCopy.value = row;
  isShowDialog1.value = true;
};
// import { getDeptList, deleteUser, getParentDeptList } from "@/api/modules/user";
const loading = ref(false);
const tableParams = ref({
  current_PageIndex: 1,
  page_Size: 20,
  total: 0
});
// 改变页面容量
const handleSizeChange = (val: number) => {
  tableParams.value.page_Size = val;
  getOrderList();
};

// 改变页码序号
const handleCurrentChange = (val: number) => {
  tableParams.value.current_PageIndex = val;
  getOrderList();
};

onMounted(() => {
  getOrderList();
});
const tableData = ref<any>([]);
// tableData.value = [{ order_Name: "订单" }];
// 获取 ProTable 元素，调用其获取刷新数据方法（还能获取到当前查询参数，方便导出携带参数）
const proTable = ref<ProTableInstance>();
const queryParams = ref<any>({ order_Status: "0" });

// 获取订单数据
const getOrderList = async () => {
  if (queryParams.value.time && queryParams.value.time.length == 2) {
    queryParams.value.begin_Time = queryParams.value.time[0];
    queryParams.value.end_Time = queryParams.value.time[1];
  } else {
    queryParams.value.begin_Time = queryParams.value.end_Time = null;
  }
  try {
    loading.value = true;
    const res: any = await $http.post("/api/Order/getOrderPageList", { ...queryParams.value, ...tableParams.value });
    tableData.value = res.data.currentPageData;
    tableParams.value.total = res.data.total;
    loading.value = false;
  } catch (error) {}
};
const baseURL = import.meta.env.VITE_API_URL_A as string;
const imgs = ref<string[]>([]);
// 打开 drawer(新增、查看、编辑)
const drawerRef = ref<InstanceType<typeof orderDrawer> | null>(null);
const openDrawer = (title: string, row: any = { fileUrlList: [] }) => {
  console.log(row);
  imgs.value = row.fileUrlList.map((v: any) => baseURL + v.filePath);
  const params = {
    title,
    row: { ...row },
    isView: title === "查看",
    api: (params: any): Promise<any> => {
      const obj = { ...params };
      if (!obj.id) {
        delete obj.tagManager_Id;
        delete obj.order_TagOrReport_Name;
      }
      return $http.post("/api/Order/saveOrder", obj);
    },
    getTableList: getOrderList
  };
  drawerRef.value?.acceptParams(params);
};

const isShowDialog = ref(false);
const disabled = ref(true);
const checkList = ref<any>([]);
const tagList = ref<any>([]);
let orderId: any;

async function getTag() {
  const res = await $http.get("/api/TagManager/getTagManagerList");
  tagList.value = res.data;
}
getTag();
// 打开打标签
const openTag = async (row: any) => {
  try {
    orderId = row.id;
    const res2: any = await $http.get("/api/Order/getOrderTagOrReportList", { tag_Type: 1, order_Id: row.id });
    checkList.value = res2.data.map((v: any) => v.tagManager_Id);
    isShowDialog.value = true;
  } catch (error) {}
};
const submit = async (row: any) => {
  try {
    const res: any = await $http.post("api/Order/saveOrderTagOrReport", {
      tag_Type: 1,
      order_Id: orderId,
      tagManager_IdList: checkList.value
    });
    ElMessage.success("保存成功");
    isShowDialog.value = false;
  } catch (error) {}
};

// 打开订单跟踪
const followRef = ref<InstanceType<typeof followDialog> | null>(null);
const openFollow = (title: string, row: Partial<Order.ResOrderList> = {}) => {
  const params = {
    title,
    row: { ...row },
    isView: title === "查看",
    // api: title === "新增" ? addUser : title === "编辑" ? editUser : undefined,
    getTableList: proTable.value?.getTableList
  };
  followRef.value?.acceptParams(params);
};
</script>
