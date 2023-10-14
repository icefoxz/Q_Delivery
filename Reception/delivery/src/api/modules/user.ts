import { ResPage, User, Dept, Menu } from "@/api/interface/index"; // Menu
import { PORT1 } from "@/api/config/servicePort";
import http from "@/api";
import testaxios from "@/api/testaxios/testaxios";

/**
 * @name 用户中心模块
 */
// 获取用户列表
export const getUserList = (params: User.ReqUserParams) => {
  return http.post<ResPage<User.ResUserList>>(PORT1 + `/user/list`, params);
};

// 获取树形用户列表
export const getUserTreeList = (params: User.ReqUserParams) => {
  return http.post<ResPage<User.ResUserList>>(PORT1 + `/user/tree/list`, params);
};

// 新增用户
export const addUser = (params: { id: string }) => {
  return http.post(PORT1 + `/user/add`, params);
};

// 批量添加用户
export const BatchAddUser = (params: FormData) => {
  return http.post(PORT1 + `/user/import`, params);
};

// 编辑用户
export const editUser = (params: { id: string }) => {
  return http.post(PORT1 + `/user/edit`, params);
};

// 删除用户
export const deleteUser = (params: { id: string[] }) => {
  return http.post(PORT1 + `/user/delete`, params);
};

// 切换用户状态
export const changeUserStatus = (params: { id: string; status: number }) => {
  return http.post(PORT1 + `/user/change`, params);
};

// 重置用户密码
export const resetUserPassWord = (params: { id: string }) => {
  return http.post(PORT1 + `/user/rest_password`, params);
};

// 导出用户数据
export const exportUserInfo = (params: User.ReqUserParams) => {
  return http.download(PORT1 + `/user/export`, params);
};

// // 获取用户状态字典
// export const getUserStatus = () => {
//   return http.get<User.ResStatus[]>(PORT1 + `/user/status`);
// };

// // 获取用户性别字典
// export const getUserGender = () => {
//   return http.get<User.ResGender[]>(PORT1 + `/user/gender`);
// };

// // 获取用户部门列表
// export const getUserDepartment = () => {
//   return http.get<User.ResDepartment[]>(PORT1 + `/user/department`);
// };

// // 获取用户角色字典
// export const getUserRole = () => {
//   return http.get<User.ResRole[]>(PORT1 + `/user/role`);
// };

// --------------------------------------
// 获取菜单列表
export const getMenuList = () => {
  return http.get<Menu.ReqMenuParams[]>(PORT1 + `/User/getParentDeptList`);
};

/**
 * 单位管理
 */
// 获取单位树
export const getParentDeptList = () => {
  return testaxios.get<Dept.ReqDeptParams[]>(`/User/getParentDeptList`);
};
// 获取单位列表
export const getDeptList = (params: Dept.ReqDeptParams) => {
  return testaxios.get<Dept.ReqDeptParams[]>(`/User/getDeptList`, params);
};
