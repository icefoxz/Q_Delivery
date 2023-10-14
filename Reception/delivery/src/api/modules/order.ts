import { ResPage, Order, Menu } from "@/api/interface/index"; // Menu
import { PORT1 } from "@/api/config/servicePort";
import http from "@/api";

/**
 * @name 订单模块
 */
// 获取订单列表
export const getOrderList = (params: Order.ReqOrderParams) => {
  return http.post<ResPage<Order.ResOrderList>>(PORT1 + `/Order/getOrderList`, params);
};

// 删除用户
// export const deleteOrder = (params: { id: string[] }) => {
//   return http.post(PORT1 + `/Order/deleteOrder`, params);
// };
