using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Commons.Enum
{
    public enum ErrorCodeEnum
    {
        /// <summary>
        /// 接口调用成功
        /// </summary>
        Success = 3000,

        /// <summary>
        /// 服务器异常
        /// </summary>
        ServerException = 4000,

        /// <summary>
        /// 丢失参数
        /// </summary>
        ParameterMissing = 4001,

        /// <summary>
        /// 参数无效
        /// </summary>
        ParameterInvalid = 4002,

        /// <summary>
        /// 参数校验失败
        /// </summary>
        ParameterError = 4003,

        /// <summary>
        /// 当前账套不存在，请重新登陆
        /// </summary>
        TheSetOfBooksNotExist = 4004,

        /// <summary>
        /// 初始化完成
        /// </summary>
        InitializationCompleted = 4005,

        /// <summary>
        /// 初始化未完成
        /// </summary>
        InitializationNoCompleted = 4006,

        /// <summary>
        /// 口令无效
        /// </summary>
        TokenInvalid = 4007,

        /// <summary>
        /// 平台禁用
        /// </summary>
        PlatformDisabled = 4008,

        /// <summary>
        /// 口令丢失
        /// </summary>
        TokenMissing = 4010,

        /// <summary>
        /// 接口状态不正确
        /// </summary>
        InterfaceError = 4011,

        /// <summary>
        /// 没有接口权限
        /// </summary>
        NoAccess = 4012,

        /// <summary>
        /// 未登录
        /// </summary>
        NotLogin = 4013,

        /// <summary>
        /// 操作错误
        /// </summary>
        OperationError = 4014,

        /// <summary>
        /// 时间戳异常
        /// </summary>
        TimeError = 4015,

        /// <summary>
        /// 确认提示
        /// </summary>
        ConfirmPrompt = 4016,

        /// <summary>
        /// 登录注册未审核时
        /// </summary>
        RegistrationNotApproved = 4017,

        /// <summary>
        /// 银行界面判断是否首次登录
        /// </summary>
        IsFirstLogin = 4018,

        /// <summary>
        /// 秘钥登录提示
        /// </summary>
        IsKeyLoginConfim = 4019,
    }
}
