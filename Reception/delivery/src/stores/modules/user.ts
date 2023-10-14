import { defineStore } from "pinia";
import { UserState } from "@/stores/interface";
import piniaPersistConfig from "@/config/piniaPersist";

export const useUserStore = defineStore({
  id: "geeker-user",
  state: (): UserState => ({
    token: "",
    userInfo: { name: "Geeker" },
    dictList: [] as any[]
  }),
  getters: {
    // findDic(k: string) {
    //   return this.dictList.find((f: any) => f.dict_Key == k).chilren;
    // },
    findDic() {
      // this.dictList;
      return (k: string) => this.dictList.find((f: any) => f.dict_Key == k).chilren;
    }
  },
  actions: {
    // Set Token
    setToken(token: string) {
      this.token = token ? "Bearer " + token : "";
    },
    // Set setUserInfo
    setUserInfo(userInfo: UserState["userInfo"]) {
      this.userInfo = userInfo;
    },
    setDic(arr: any[]) {
      this.dictList = arr;
    }
  },
  persist: piniaPersistConfig("geeker-user")
});
