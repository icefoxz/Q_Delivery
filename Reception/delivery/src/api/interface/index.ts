// 请求响应参数（不包含data）
export interface Result {
  code: string;
  msg: string;
}

// 请求响应参数（包含data）
export interface ResultData<T = any> extends Result {
  data: T;
}

// 分页响应参数
export interface ResPage<T> {
  list: T[];
  pageNum: number;
  pageSize: number;
  total: number;
}

// 分页请求参数
export interface ReqPage {
  pageNum: number;
  pageSize: number;
}

// 文件上传模块
export namespace Upload {
  export interface ResFileUrl {
    fileUrl: string;
  }
}

// 登录模块
export namespace Login {
  export interface ReqLoginForm {
    username: string;
    password: string;
  }
  export interface ResLogin {
    access_token: string;
  }
  export interface ResAuthButtons {
    [key: string]: string[];
  }
}

// 用户管理模块（模板）
// export namespace User {
//   // 请求参数
//   export interface ReqUserParams extends ReqPage {
//     username: string;
//     gender: number;
//     idCard: string;
//     email: string;
//     address: string;
//     createTime: string[];
//     status: number;
//   }

//   // table
//   export interface ResUserList {
//     id: string;
//     username: string;
//     gender: number;
//     user: { detail: { age: number } };
//     idCard: string;
//     email: string;
//     address: string;
//     createTime: string;
//     status: number;
//     avatar: string;
//     photo: any[];
//     children?: ResUserList[];
//   }
//   export interface ResStatus {
//     userLabel: string;
//     userValue: number;
//   }
//   export interface ResGender {
//     genderLabel: string;
//     genderValue: number;
//   }
//   export interface ResDepartment {
//     id: string;
//     name: string;
//     children?: ResDepartment[];
//   }
//   export interface ResRole {
//     id: string;
//     name: string;
//     children?: ResDepartment[];
//   }
// }

// 单位管理
export namespace Dept {
  // 请求参数
  export interface ReqDeptParams extends ReqPage {
    dept_Name: string;
    dept_ParentId: string;
    dept_ParentName: string;
    dept_Address: string;
    dept_Phone: string;
    dept_PostCode: string;
  }

  // table
  export interface ResDeptList {
    // id: string;
    // username: string;
    // gender: number;
    // user: { detail: { age: number } };
    // idCard: string;
    // email: string;
    // address: string;
    // createTime: string;
    // status: number;
    // avatar: string;
    // photo: any[];
    // children?: ResDeptList[];
    id: string;
    dept_Name: string;
    dept_ParentId: string;
    dept_ParentName: string;
    dept_Address: string;
    dept_Phone: string;
    dept_PostCode: string;
    create_Time: string;
    create_User: string;
    childList?: ResDeptList[];
  }
}

// 人员管理
export namespace Person {
  // 请求参数
  export interface ReqPersonParams extends ReqPage {
    per_Name: string;
    per_DeptId: string;
    per_JobId: string;
    per_JobStatus: string;
    per_Phone: string;
    per_IdNo: string;
    per_Birthday: string;
    per_Address: string;
    per_Palifcs: string;
    per_Type: string;
    per_UserName: string;
    per_UserPwd: string;
    per_NormalizedPhone: string;
  }

  // table
  export interface ResPersonList {
    id: string;
    per_Name: string;
    per_DeptId: string;
    per_JobId: string;
    per_JobStatus: string;
    per_Phone: string;
    per_IdNo: string;
    per_Birthday: string;
    per_Address: string;
    per_Palifcs: string;
    per_Type: string;
    per_UserName: string;
    per_UserPwd: string;
    per_NormailizedPhone: string;
    create_Time: string;
    create_User: string;
    [prop: string]: any;
  }
}

// 用户管理
export namespace User {
  // 请求参数
  export interface ReqUserParams extends ReqPage {
    user_LoginName: string;
    user_LoginPwd: string;
    user_LoginPwdCipher: string;
    user_DeptId: string;
    user_Limit: string;
    user_Person: string;
  }

  // table
  export interface ResUserList {
    id: string;
    user_LoginName: string;
    user_LoginPwd: string;
    user_LoginPwdCipher: string;
    user_DeptId: string;
    user_Limit: string;
    user_Person: string;
    create_Time: string;
    create_User: string;
    [prop: string]: any;
  }
}

// 职位管理
export namespace Job {
  // 请求参数
  export interface ReqJobParams extends ReqPage {
    job_Name: string;
    job_DeptId: string;
  }

  // table
  export interface ResJobList {
    id: string;
    job_Name: string;
    job_DeptId: string;
    create_Time: string;
    create_User: string;
  }
}

// 菜单管理
export namespace Menu {
  // 请求参数
  export interface ReqMenuParams extends ReqPage {
    path: string;
    component?: string;
    meta: {
      title: string;
    };
  }

  // table
  export interface ResMenuList {
    [x: string]: any;
    id: string;
    path: string;
    component?: string;
    redirect?: string;
    meta: {
      icon: string;
      title: string;
      isAffix: string;
      isLink?: string;
      isHide?: boolean;
      isFull?: boolean;
      isKeepAlive?: boolean;
    };
    children?: ResMenuList[];
  }
}

// 订单管理
export namespace Order {
  // 请求参数
  export interface ReqOrderParams extends ReqPage {
    order_Name: string;
    order_RiderName: string;
    order_RiderPhone: string;
    order_JobStatus: string;
    order_ReceiverName: string;
    order_ReceiverPhone: string;
    order_GoodsName: string;
    order_GoodsType: string;
    order_GoodsWeight: string;
    order_GoddsNums: number;
    order_GoodsLong: number;
    order_GoodsWidth: number;
    order_GoodsHight: number;
    order_GoodsPrice: number;
    order_GoodsDelivery: string;
    order_PathDistance: number;
    order_PayIdentity: string;
    order_Status: string;
  }

  // table
  export interface ResOrderList {
    id: string;
    order_Name: string;
    order_RiderName: string;
    order_RiderPhone: string;
    order_JobStatus: string;
    order_ReceiverName: string;
    order_ReceiverPhone: string;
    order_GoodsName: string;
    order_GoodsType: string;
    order_GoodsWeight: string;
    order_GoddsNums: number;
    order_GoodsLong: number;
    order_GoodsWidth: number;
    order_GoodsHight: number;
    order_GoodsPrice: number;
    order_GoodsDelivery: string;
    order_PathDistance: number;
    order_PayIdentity: string;
    order_Status: string;
    create_Time: string;
    create_User: string;
  }
}

// 订单管理
export namespace OrderFollow {
  // 请求参数
  export interface ReqOrderFollowParams extends ReqPage {
    order_Name: string;
    tag_Id: string;
    tag_Name: string;
    expand_desc: string;
    create_Time: string;
    create_User: string;
  }

  // table
  export interface ResOrderFollowList {
    id: string;
    order_Name: string;
    tag_Id: string;
    tag_Name: string;
    expand_desc: string;
    create_Time: string;
    create_User: string;
  }
}

// 虚拟币管理
export namespace Coin {
  // 请求参数
  export interface ReqCoinParams extends ReqPage {
    lingao_Balance: string;
    person_Id: string;
  }

  // table
  export interface ResCoinList {
    id: string;
    lingao_Balance: string;
    person_Id: string;
  }
}

// 虚拟币操作日志
export namespace CoinLog {
  // 请求参数
  export interface ReqCoinLogParams extends ReqPage {
    log_OptUser: string;
    log_Type: string;
    log_OptTime: string;
    log_BeUser: string;
    log_BeginAmount: number;
    log_EndAmount: number;
  }

  // table
  export interface ResCoinLogList {
    id: string;
    log_OptUser: string;
    log_Type: string;
    log_OptTime: string;
    log_BeUser: string;
    log_BeginAmount: number;
    log_EndAmount: number;
  }
}

// 系统字典
export namespace SysDic {
  // 请求参数
  export interface ReqSysDicParams extends ReqPage {
    dict_parentId: string;
    dict_Key: string;
    dict_Value: string;
    dict_JsonValue: string;
  }

  // table
  export interface ResSysDicList {
    id: string;
    dict_parentId: string;
    dict_Key: string;
    dict_Value: string;
    dict_JsonValue: string;
  }
}
// 系统日志
export namespace SysLog {
  // 请求参数
  export interface ReqSysLogParams extends ReqPage {
    log_OptIp: string;
    log_OptPort: string;
    log_OptUser: string;
    log_OptJob: string;
    log_Type: string;
    log_Message: string;
    log_OptTime: string;
    log_OptResult: string;
  }

  // table
  export interface ResSysLogList {
    id: string;
    log_OptIp: string;
    log_OptPort: string;
    log_OptUser: string;
    log_OptJob: string;
    log_Type: string;
    log_Message: string;
    log_OptTime: string;
    log_OptResult: string;
  }
}
