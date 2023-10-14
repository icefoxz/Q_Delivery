import { defineStore } from "pinia";
import { UserState } from "@/stores/interface";
import piniaPersistConfig from "@/config/piniaPersist";
import { $http } from "@/api/testaxios/testaxios";

type preMode = {
  per_JobStatusId: string; // 在职状态
  dept_Name: string; // 所属单位名称
  job_Name: string; // 职位名称
  per_Name: string; // 人员名称
  per_FullName: string; // 名称全拼
  per_SimpleName: string; // 名称简拼
  per_JobStatus: string; // 在职状态
  per_Phone: string; // 手机号
  per_IdNo: string; // 身份证号
  per_Birthday: string; // 生日
  per_Address: string; // 地址string
  per_Politics: string; // 政治面貌
  dept_Id: string; // 所属单位Id
  create_User: string; // 创建人
  create_Time: string; // 创建时间
  id: string;
};

export const useDwStore = defineStore("preData", {
  state: () => ({
    list: [] as preMode[]
  }),
  getters: {
    // getFilterList(id?: string): preMode[] {
    //   if (!id) return this.list;
    //   // return this.title;
    //   return this.list.filter(f => f.dept_ParentId == id);
    // }
  },
  actions: {
    /** 请求树数据 */
    async getPre() {
      const res: any = await $http.get("/api/User/getPersonList");
      this.list = res.data;
    }
  }
});

// 初始化数据
const { getPre } = useDwStore();
getPre();
