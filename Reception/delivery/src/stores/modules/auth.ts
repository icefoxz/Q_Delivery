import { defineStore } from "pinia";
import { AuthState } from "@/stores/interface";
import { getAuthButtonListApi } from "@/api/modules/login"; // getAuthMenuListApi
import { getFlatMenuList, getShowMenuList, getAllBreadcrumbList } from "@/utils";

export const useAuthStore = defineStore({
  id: "geeker-auth",
  state: (): AuthState => ({
    // 按钮权限列表
    authButtonList: {},
    // 菜单权限列表
    authMenuList: [],
    // 当前页面的 router name，用来做按钮权限筛选
    routeName: ""
  }),
  getters: {
    // 按钮权限列表
    authButtonListGet: state => state.authButtonList,
    // 菜单权限列表 ==> 这里的菜单没有经过任何处理
    authMenuListGet: state => state.authMenuList,
    // 菜单权限列表 ==> 左侧菜单栏渲染，需要剔除 isHide == true
    showMenuListGet: state => getShowMenuList(state.authMenuList),
    // 菜单权限列表 ==> 扁平化之后的一维数组菜单，主要用来添加动态路由
    flatMenuListGet: state => getFlatMenuList(state.authMenuList),
    // 递归处理后的所有面包屑导航列表
    breadcrumbListGet: state => getAllBreadcrumbList(state.authMenuList)
  },
  actions: {
    // Get AuthButtonList
    async getAuthButtonList() {
      const { data } = await getAuthButtonListApi();
      this.authButtonList = data;
    },
    getAuthMenuList2(data: any[]) {
      this.authMenuList = data;
    },
    // Get AuthMenuList
    async getAuthMenuList() {
      // const { data } = await getAuthMenuListApi();
      let data = [
        {
          path: "/home/index",
          name: "home",
          component: "/home/index",
          meta: {
            icon: "HomeFilled",
            title: "首页",
            isAffix: true,
            // 以下字段不用
            isLink: "",
            isHide: false,
            isFull: false,
            isKeepAlive: false
          }
        },
        {
          name: "userCenter",
          path: "/userCenter",
          redirect: "/userCenter/menuManage",
          meta: {
            icon: "Tickets",
            title: "用户中心",
            isAffix: false,
            // 以下字段不用
            isLink: "",
            isHide: false,
            isFull: false,
            isKeepAlive: true
          },
          children: [
            {
              name: "menuManage",
              path: "/userCenter/menuManage",
              component: "/userCenter/menuManage/index",
              meta: {
                icon: "Menu",
                title: "菜单管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },
            {
              name: "deptManage",
              path: "/userCenter/deptManage",
              component: "/userCenter/deptManage/index",
              meta: {
                icon: "Menu",
                title: "单位管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },
            {
              name: "jobManage",
              path: "/userCenter/jobManage",
              component: "/userCenter/jobManage/index",
              meta: {
                icon: "Menu",
                title: "职位管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },
            {
              name: "authManage",
              path: "/userCenter/authManage",
              component: "/userCenter/authManage/index",
              meta: {
                icon: "Menu",
                title: "权限管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },
            {
              name: "personnelManage",
              path: "/userCenter/personnelManage",
              component: "/userCenter/personnelManage/index",
              meta: {
                icon: "Menu",
                title: "人员管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },
            {
              name: "userManage",
              path: "/userCenter/userManage",
              component: "/userCenter/userManage/index",
              meta: {
                icon: "Menu",
                title: "用户管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            }
          ]
        },
        {
          name: "orders",
          path: "/orders",
          redirect: "/orders/orderManage",
          meta: {
            icon: "Tickets",
            title: "订单管理",
            isAffix: false,
            // 以下字段不用
            isLink: "",
            isHide: false,
            isFull: false,
            isKeepAlive: true
          },
          children: [
            {
              name: "orderManage",
              path: "/orders/orderManage",
              component: "/orders/orderManage/index",
              meta: {
                icon: "Menu",
                title: "订单查询",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },

            {
              name: "tagManage",
              path: "/orders/tagManage",
              component: "/orders/tagManage/index",
              meta: {
                icon: "Menu",
                title: "订单标签",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },
            {
              name: "tagGroupManage",
              path: "/orders/tagGroupManage",
              component: "/orders/tagGroupManage/index",
              meta: {
                icon: "Menu",
                title: "标签类型管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },
            {
              name: "bg",
              path: "/orders/bg",
              component: "/orders/bg/index",
              meta: {
                icon: "Menu",
                title: "标签报告操作管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            }
          ]
        },
        {
          name: "virtualCoin",
          path: "/virtualCoin",
          redirect: "/virtualCoin/coinManage",
          meta: {
            icon: "Tickets",
            title: "虚拟币管理",
            isAffix: false,
            // 以下字段不用
            isLink: "",
            isHide: false,
            isFull: false,
            isKeepAlive: true
          },
          children: [
            {
              name: "coinManage",
              path: "/virtualCoin/coinManage",
              component: "/virtualCoin/coinManage/index",
              meta: {
                icon: "Menu",
                title: "虚拟币",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },
            {
              name: "operateLog",
              path: "/virtualCoin/operateLog",
              component: "/virtualCoin/operateLog/index",
              meta: {
                icon: "Menu",
                title: "虚拟币操作日志",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            }
          ]
        },
        {
          name: "system",
          path: "/system",
          redirect: "/system/logManage",
          meta: {
            icon: "Tickets",
            title: "系统配置",
            isAffix: false,
            // 以下字段不用
            isLink: "",
            isHide: false,
            isFull: false,
            isKeepAlive: true
          },
          children: [
            {
              name: "logManage",
              path: "/system/logManage",
              component: "/system/logManage/index",
              meta: {
                icon: "Menu",
                title: "日志管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            },

            {
              name: "dictionaries",
              path: "/system/dictionaries",
              component: "/system/dictionaries/index",
              meta: {
                icon: "Menu",
                title: "字典管理",
                isAffix: false,
                // 以下字段不用
                isLink: "",
                isHide: false,
                isFull: false,
                isKeepAlive: true
              }
            }
          ]
        }
      ];
      this.authMenuList = data;
    },
    // Set RouteName
    async setRouteName(name: string) {
      this.routeName = name;
    }
  }
});
