import { $http } from "@/api/testaxios/testaxios";
import { defineStore } from "pinia";
type treeMode = {
  /** id标识 */
  id: string; //
  /**  上级单位Id */
  parentId?: string;
  /**  key */
  dict_Key: string;
  /**  用于显示名称 */
  dict_Name: string;
  /**  值 */
  dict_Value: string;
  /**  ---- */
  dict_JsonValue: any;
  /**  是否是内置的 */
  isSystemBuilt: boolean;
  /**  创建用户 */
  create_User: string;
  /**  创建时间 */
  create_Time: string;
  /** 子级数据 */
  chilren?: treeMode[];

  /**  上一级用于显示名称 */
  // parentName?: string;
};

// 树数据转扁平数据
function treeToArray(tree: treeMode[]): treeMode[] {
  return tree.reduce((arr, item) => {
    if (!Array.isArray(item.chilren)) return arr.concat(item);
    const chilren = item.chilren.map(v => ({ ...v }));
    item = { ...item };
    delete item.chilren; // 扁平不需要chilren
    return arr.concat(item, ...treeToArray(chilren));
  }, [] as treeMode[]);
}

export const useDicStore = defineStore("dicData", {
  state: () => ({
    treeList: [] as treeMode[],
    list: [] as treeMode[]
  }),
  getters: {
    getFilterList(id?: string): treeMode[] {
      if (!id) return this.list;
      // return this.title;
      return this.list.filter(f => f.parentId == id);
    }
  },
  actions: {
    /** 请求树数据 */
    async getTree() {
      const res: any = await $http.get("/api/System/getSystemDictList", { isTree: true });
      this.treeList = res.data;
      this.list = treeToArray(this.treeList);
    }
  }
});

// 初始化数据
const { getTree } = useDicStore();
getTree();
// getList();
