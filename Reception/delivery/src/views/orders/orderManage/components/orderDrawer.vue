<template>
  <el-drawer v-model="drawerVisible" :destroy-on-close="true" size="600px" :title="`${drawerProps.title}订单`">
    <el-form
      ref="ruleFormRef"
      label-width="120px"
      label-suffix=" :"
      :inline="false"
      :rules="rules"
      :disabled="drawerProps.isView"
      :model="drawerProps.row"
      :hide-required-asterisk="drawerProps.isView"
    >
      <el-row :gutter="35">
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="">
          <el-form-item label="订单名称" prop="order_Name">
            <el-input v-model="drawerProps.row.order_Name" placeholder="请填写订单名称" clearable></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="配送人" prop="order_RiderName">
            <el-select v-model="drawerProps.row!.order_RiderName" placeholder="请选择配送人" clearable filterable>
              <el-option v-for="item in riderList" :key="item.id" :label="item.per_Name" :value="item.per_Name" />
            </el-select>
            <!-- <el-input v-model="drawerProps.row!.order_RiderName" placeholder="请填写配送人" clearable></el-input> -->
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="配送人手机号" prop="order_RiderPhone">
            <el-input v-model="drawerProps.row!.order_RiderPhone" placeholder="请填写配送人" clearable></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="收货人名称" prop="order_ReceiverName">
            <el-input v-model="drawerProps.row!.order_ReceiverName" placeholder="请填写配送人" clearable></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="收货人手机号" prop="order_ReceiverPhone">
            <el-input v-model="drawerProps.row!.order_ReceiverPhone" placeholder="请填写配送人" clearable></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="">
          <el-form-item label="在职状态" prop="order_JobStatus">
            <el-select v-model="drawerProps.row!.order_JobStatus" placeholder="请选择在职状态" clearable>
              <el-option v-for="item in jobStatusList" :label="item.dict_Name" :value="item.dict_Key" :key="item.dict_Key" />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="物品名称" prop="order_GoodsName">
            <el-input
              v-model="drawerProps.row!.order_GoodsName"
              placeholder="请填写物品名称"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="物品类型" prop="order_GoodsType">
            <el-select
              v-model="drawerProps.row!.order_GoodsType"
              placeholder="请选择物品类型"
              clearable
              :disabled="!!drawerProps.row!.id"
            >
              <el-option v-for="item in goodsTypeList" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="物品重量" prop="order_GoodsWeight">
            <el-input
              v-model.number="drawerProps.row!.order_GoodsWeight"
              placeholder="请填写物品重量"
              clearable
              :disabled="!!drawerProps.row!.id"
            >
              <template #append>kg</template>
            </el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="物品件数" prop="order_GoddsNums">
            <el-input
              v-model.number="drawerProps.row!.order_GoddsNums"
              placeholder="请填写物品件数"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="物品长度" prop="order_GoodsLong">
            <el-input
              v-model.number="drawerProps.row!.order_GoodsLong"
              placeholder="请填写物品长度"
              clearable
              :disabled="!!drawerProps.row!.id"
            >
              <template #append>cm</template>
            </el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="物品宽度" prop="order_GoodsWidth">
            <el-input
              v-model.number="drawerProps.row!.order_GoodsWidth"
              placeholder="请填写物品宽度"
              clearable
              :disabled="!!drawerProps.row!.id"
            >
              <template #append>cm</template>
            </el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="物品高度" prop="order_GoodsHight">
            <el-input
              v-model="drawerProps.row!.order_GoodsHight"
              placeholder="请填写物品高度"
              clearable
              :disabled="!!drawerProps.row!.id"
            >
              <template #append>cm</template>
            </el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="商品价格" prop="order_GoodsPrice">
            <el-input
              type="number"
              v-model.number="drawerProps.row!.order_GoodsPrice"
              placeholder="请填写商品价格"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="开始经度" prop="order_BenginLng">
            <el-input
              v-model.number="drawerProps.row!.order_BenginLng"
              placeholder="请填写开始经度"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="结束经度" prop="order_EndLng">
            <el-input
              v-model.number="drawerProps.row!.order_EndLng"
              placeholder="请填写结束经度"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="开始纬度" prop="order_BenginLat">
            <el-input
              v-model.number="drawerProps.row!.order_BenginLat"
              placeholder="请填写开始纬度"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="结束纬度" prop="order_EndLat">
            <el-input
              v-model.number="drawerProps.row!.order_EndLat"
              placeholder="请填写结束纬度"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="开始地理Id" prop="order_BenginPlaceId">
            <el-input
              v-model="drawerProps.row!.order_BenginPlaceId"
              placeholder="请填写开始地理Id"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="结束地理Id" prop="order_EndPlaceId">
            <el-input
              v-model="drawerProps.row!.order_EndPlaceId"
              placeholder="请填写结束地理Id"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="">
          <el-form-item label="舟属" prop="order_StateId">
            <!-- <el-input
              v-model="drawerProps.row!.order_StateId"
              placeholder="请填写舟属"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input> -->
            <el-select v-model="drawerProps.row!.order_StateId" class="m-2" placeholder="请选择舟属" size="large">
              <el-option v-for="item in Naviculus" :label="item.dict_Name" :value="item.dict_Key" :key="item.dict_Key" />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="配送距离" prop="order_PathDistance">
            <el-input
              v-model.number="drawerProps.row!.order_PathDistance"
              placeholder="请填写配送距离"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="运送费" prop="order_GoodsDelivery">
            <el-input
              v-model.number="drawerProps.row!.order_GoodsDelivery"
              placeholder="请填写运送费"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="付费类型" prop="order_PayType">
            <el-select
              v-model="drawerProps.row!.order_PayType"
              placeholder="请选择付费类型"
              clearable
              :disabled="!!drawerProps.row!.id"
            >
              <el-option v-for="item in payTypeList" :key="item.value" :label="item.label" :value="item.value" />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12" class="">
          <el-form-item label="付款标识" prop="order_PayIdentity">
            <el-input
              v-model="drawerProps.row!.order_PayIdentity"
              placeholder="请填写付款标识"
              clearable
              :disabled="!!drawerProps.row!.id"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="">
          <el-form-item label="订单状态" prop="order_StatusKey">
            <el-select v-model="drawerProps.row!.order_StatusKey" placeholder="请选择订单状态" clearable>
              <el-option v-for="item in OrderStatus" :label="item.dict_Name" :value="item.dict_Key" :key="item.dict_Key" />
              <!-- <el-option v-for="item in orderStatusList" :key="item.value" :label="item.label" :value="item.value" /> -->
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="">
          <el-form-item label="附件">
            <el-upload
              v-model:file-list="fileList"
              :action="baseURL + '/api/System/saveFile'"
              list-type="picture-card"
              :on-preview="handlePictureCardPreview"
              :on-remove="handleRemove"
              :http-request="uploadFile"
            >
              <!-- :auto-upload="false" -->
              <el-icon><Plus /></el-icon>
            </el-upload>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="" v-if="!!drawerProps.row!.id">
          <h3>订单报告</h3>
          <el-form-item label="订单报告类型" prop="tagManager_Id">
            <el-select v-model="drawerProps.row!.tagManager_Id" placeholder="请选择" clearable filterable>
              <el-option
                v-for="item in tagList.filter((f: any) => f.tag_Type == 2)"
                :key="item.id"
                :label="item.tag_Name"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="" v-if="!!drawerProps.row!.id">
          <el-form-item label="订单报告说明" prop="order_TagOrReport_Name">
            <el-input
              type="textarea"
              :row="3"
              v-model="drawerProps.row!.order_TagOrReport_Name"
              placeholder="请填写订单报告说明"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <template #footer>
      <el-button @click="drawerVisible = false"> 取消 </el-button>
      <el-button v-show="!drawerProps.isView" type="primary" @click="handleSubmit"> 确定 </el-button>
    </template>
    <el-dialog v-model="dialogVisible">
      <img style="width: 100%" w-full :src="dialogImageUrl" alt="Preview Image" />
    </el-dialog>
  </el-drawer>
</template>

<script setup lang="ts" name="orderDrawer">
import { reactive, ref } from "vue";
// import { genderType } from "@/utils/serviceDict";
import { ElMessage, FormInstance, UploadProps, UploadRequestOptions, UploadUserFile } from "element-plus";
import { $http } from "@/api/testaxios/testaxios";
import { useUserStore } from "@/stores/modules/user";
// import UploadImg from "@/components/Upload/Img.vue";
// import UploadImgs from "@/components/Upload/Imgs.vue";
const props = defineProps<{ tagList: any[] }>();

const Naviculus = useUserStore().findDic("Naviculus");
const OrderStatus = useUserStore().findDic("OrderStatus");
const jobStatusList = useUserStore().findDic("JobStatus");
const rules = reactive({
  order_Name: [{ required: true, message: "请填写订单名称" }],
  order_RiderName: [{ required: true, message: "请填写配送人" }],
  order_RiderPhone: [{ required: true, message: "请填写配送人手机号" }],
  order_JobStatus: [{ required: true, message: "请选择在职状态" }],
  order_ReceiverName: [{ required: true, message: "请填写收货人名称" }],
  order_ReceiverPhone: [{ required: true, message: "请填写收货人手机号" }],
  order_GoodsName: [{ required: true, message: "请填写物品名称" }],
  order_GoodsType: [{ required: true, message: "请填写物品类型" }],
  order_GoodsWeight: [{ required: true, message: "请填写物品重量" }],
  order_GoddsNums: [{ required: true, message: "请填写物品件数" }],
  order_GoodsLong: [{ required: true, message: "请填写物品长度" }],
  order_GoodsWidth: [{ required: true, message: "请填写物品宽度" }],
  order_GoodsHight: [{ required: true, message: "请填写物品高度" }],
  order_GoodsPrice: [{ required: true, message: "请填写商品价格" }],
  order_GoodsDelivery: [{ required: true, message: "请填写运送费" }],
  order_PathDistance: [{ required: true, message: "请填写配送距离" }],
  order_PayType: [{ required: true, message: "请选择付费类型" }],
  order_PayIdentity: [{ required: true, message: "请填写付款标识" }],
  order_StatusKey: [{ required: true, message: "请选择订单状态" }],
  tagManager_Id: [{ required: true, message: "请选择" }],
  order_TagOrReport_Name: [{ required: true, message: "请填写订单报告说明" }]
});
// 物品类型
const goodsTypeList = [
  {
    value: "1",
    label: "默认类型"
  }
];
// 付款类型
const payTypeList = [
  {
    value: "1",
    label: "APP"
  }
];

// 获取配送人列表
let riderList = ref<any>([]);
async function getRiderList() {
  const res = await $http.get("/api/User/getPersonListByPerson", { per_Type: 2 });
  riderList = res.data;
}
getRiderList();

interface DrawerProps {
  title: string;
  isView: boolean;
  row: any;
  api?: (params: any) => Promise<any>;
  getTableList?: () => void;
}

const drawerVisible = ref(false);
const drawerProps = ref<DrawerProps>({
  isView: false,
  title: "",
  row: {}
});
const isEdit = ref(false);

const baseURL = import.meta.env.VITE_API_URL_A as string;
const fileList = ref<UploadUserFile[]>([]);
let fileIdList: any[] = []; // 用来提交
let fileList1: string[] = [];
const dialogImageUrl = ref("");
const dialogVisible = ref(false);
const handleRemove: UploadProps["onRemove"] = (uploadFile, uploadFiles) => {
  const i = fileList1.findIndex(f => f == uploadFile.url);
  console.log(i);
  fileIdList.splice(i, 1);
};
const handlePictureCardPreview: UploadProps["onPreview"] = uploadFile => {
  dialogImageUrl.value = uploadFile.url!;
  dialogVisible.value = true;
};
const uploadFile = async (param: any) => {
  let formdata = new FormData();
  formdata.append("file", param.file);
  if (drawerProps.value.row!.id) formdata.append("data_Id", drawerProps.value.row!.id);
  formdata.append("data_Type", "OrderImg");
  fetch(baseURL + "/api/System/saveFile", {
    method: "POST",
    body: formdata
  })
    .then(res => {
      if (res.ok) {
        return res.json();
      } else {
        param.onError(res as any);
        console.log("error");
      }
    })
    .then(res => {
      fileIdList.push(res.data.data.id);
      fileList1.push(baseURL + res.data.data.filePath);
      param.onSuccess(res);
    });
};

// 接收父组件传过来的参数
const acceptParams = (params: DrawerProps) => {
  console.log("params", params);
  drawerProps.value = params;
  fileIdList = drawerProps.value.row.fileUrlList.map((v: any) => v.fileId);
  fileList1 = drawerProps.value.row.fileUrlList.map((v: any) => baseURL + v.filePath);
  fileList.value = drawerProps.value.row.fileUrlList.map((v: any) => ({ name: v.fileName, url: baseURL + v.filePath }));
  drawerVisible.value = true;
  if (params.title == "编辑") return (isEdit.value = true);
};

// 提交数据（新增/编辑）
const ruleFormRef = ref<FormInstance>();
const handleSubmit = () => {
  ruleFormRef.value!.validate(async valid => {
    if (!valid) return;
    try {
      // return;
      await drawerProps.value.api!({ ...drawerProps.value.row, fileIdList });
      ElMessage.success({ message: `${drawerProps.value.title}成功！` });
      drawerProps.value.getTableList!();
      drawerVisible.value = false;
    } catch (error) {}
  });
};

defineExpose({
  acceptParams
});
</script>
