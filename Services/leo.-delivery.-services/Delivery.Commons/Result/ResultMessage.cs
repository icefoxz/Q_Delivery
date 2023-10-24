using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Commons.Result
{
    public class ResultMessage
    {
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public ResultMessage()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        public ResultMessage(bool success)
        {
            this.Success = success;
            if (success)
                this.Message = "Success";
            else
                this.ErrorMsg = "Failed";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="errorCode"></param>
        public ResultMessage(bool success, HttpCodeEnum errorCode)
        {
            this.Success = success;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        public ResultMessage(bool success, string msg)
        {
            this.Success = success;
            if (success)
                this.Message = msg;
            else
            {
                this.ErrorMsg = msg;
                this.ErrorCode = HttpCodeEnum.OperationError;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="errorCode"></param>
        /// <param name="msg"></param>
        public ResultMessage(bool success, HttpCodeEnum errorCode, string msg)
        {
            this.Success = success;
            this.ErrorCode = errorCode;
            if (success)
                this.Message = msg;
            else
            {
                this.ErrorMsg = msg;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="errorCode"></param>
        /// <param name="obj"></param>
        public ResultMessage(bool success, HttpCodeEnum errorCode, object obj)
        {
            this.Success = success;
            this.ErrorCode = errorCode;
            this.Data = obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="errorCode"></param>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        public ResultMessage(bool success, HttpCodeEnum errorCode, string msg, object obj)
        {
            this.Success = success;
            this.ErrorCode = errorCode;
            this.Data = obj;
            if (success)
                this.Message = msg;
            else
            {
                this.ErrorMsg = msg;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        public ResultMessage(bool success, string msg, object obj)
        {
            this.Success = success;
            this.Data = obj;
            if (success)
                this.Message = msg;
            else
            {
                this.ErrorMsg = msg;
                this.ErrorCode = HttpCodeEnum.OperationError;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        public ResultMessage(bool success, object obj)
        {
            this.Success = success;
            this.Data = obj;
            if (!success) 
            {
                this.ErrorCode = HttpCodeEnum.OperationError;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        /// <param name="count"></param>
        /// <param name="obj"></param>
        public ResultMessage(bool success, string msg, int count, object obj)
        {
            this.Success = success;
            this.DataCount = count;
            this.Data = obj;
            if (success)
                this.Message = msg;
            else
                this.ErrorMsg = msg;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 状态码
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public HttpCodeEnum? ErrorCode { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 数据记录总数
        /// </summary>
        public int? DataCount { get; set; }

        /// <summary>
        /// 响应先锋参数
        /// </summary>
        public string ErrorMsg { get; set; }

        #endregion

    }
}
