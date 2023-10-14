import { $http } from "@/api/testaxios/testaxios";
import { defineStore } from "pinia";

type treeMode = {
  /** id标识 */
  id: string; //
  /**  单位名称 */
  dept_Name: string;
  /**  上级单位Id */
  dept_ParentId?: string;
  /**  上级单位名称 */
  dept_ParentName?: string;
  /**  单位地址 */
  dept_Address: string;
  /**  单位电话 */
  dept_Phone: string;
  /**  单位邮编 */
  dept_PostCode: string;
  /**  创建时间 */
  create_Time: string;
  /**  创建用户 */
  create_User: string;
  /** 子级数据 */
  children?: treeMode[];
};

// 树数据转扁平数据
function treeToArray(tree: treeMode[]): treeMode[] {
  return tree.reduce((arr, item) => {
    if (!Array.isArray(item.children)) return arr.concat(item);
    const children = item.children.map(v => ({ ...v, dept_ParentName: item.dept_Name }));
    item = { ...item };
    delete item.children; // 扁平不需要children
    return arr.concat(item, ...treeToArray(children));
  }, [] as treeMode[]);
}

export const useDwStore = defineStore("dwData", {
  state: () => ({
    treeList: [] as treeMode[],
    list: [] as treeMode[]
  }),
  getters: {
    getFilterList(id?: string): treeMode[] {
      if (!id) return this.list;
      // return this.title;
      return this.list.filter(f => f.dept_ParentId == id);
    }
  },
  actions: {
    /** 请求树数据 */
    async getTree() {
      const res: any = await $http.get("/api/Dept/getParentDeptList");
      let obj = {
        dept_Name: "全部",
        id: ""
        // children: [],
        // create_Time: "",
        // create_User: "",
        // dept_Address: "",
        // dept_ParentId: "",
        // dept_ParentName: "",
        // dept_Phone: "",
        // dept_PostCode: ""
      };
      console.log("res.data", res.data);
      this.treeList = [...res.data];
      this.list = treeToArray(this.treeList);
    }
  }
});

// 初始化数据
const { getTree } = useDwStore();
getTree();
// getList();
