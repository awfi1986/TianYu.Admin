using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.FilterAttribute.AuthorizeCode
{
    public class LoginSessionDataHelper
    {
        private static LoginSessionDataHelper helper = new LoginSessionDataHelper();
        private string connctionString = ConfigHelper.GetAppsettingValue(TianYuConsts.ApiAuthorizeDatabase);
        internal LoginSessionDataHelper()
        {
            if (connctionString.IsNullOrWhiteSpace())
            {
                throw new KeyNotFoundException(string.Format("未找到配置项{0}", TianYuConsts.ApiAuthorizeDatabase));
            }
        }
        internal void AddSession(LoginSessionInfoModel model)
        {
            if (model.IsNull())
            {
                return;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into LoginSessinInfo (SessinId,DeviceNo,LoginTime,LashRefresh,Expired,Status) values(@SessinId,@DeviceNo,@LoginTime,@LashRefresh,@Expired,@Status)");

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(){  ParameterName="@SessinId",Value=model.SessinId},
                new SqlParameter(){  ParameterName="@DeviceNo",Value=model.DeviceNo},
                new SqlParameter(){  ParameterName="@LoginTime",Value=model.LoginTime},
                new SqlParameter(){  ParameterName="@LashRefresh",Value=model.LashRefresh},
                new SqlParameter(){  ParameterName="@Expired",Value=model.Expired},
                new SqlParameter(){  ParameterName="@Status",Value=model.Status }
            };
            ExecuteNonQuery(builder.ToString(), parameters);
        }
        /// <summary>
        /// 从db中获取session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static LoginSessionInfoModel GetSessionInfoModel(string sessionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SessinId,DeviceNo,LoginTime,LashRefresh,Expired,Status from LoginSessinInfo where SessinId = @SessinId");

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(){  ParameterName="@SessinId",Value=sessionId}
            };
            DataTable dt = helper.ExecuteQuery(builder.ToString(), parameters);
            LoginSessionInfoModel model = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                model = new LoginSessionInfoModel()
                {
                    SessinId = row["SessinId"].ToString(),
                    DeviceNo = row["DeviceNo"].ToString(),
                    LashRefresh = row["LashRefresh"].ToDateTime(),
                    Expired = row["Expired"].ToInt(),
                    LoginTime = row["LoginTime"].ToDateTime(),
                    Status = row["Status"].ToInt()
                };
            }
            return model;
        }
        /// <summary>
        /// 保存session，若存在则更新session
        /// </summary>
        /// <param name="model"></param>
        public static void SaveSessionInfo(LoginSessionInfoModel model)
        {
            if (model.IsNull())
            {
                return;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("update LoginSessinInfo set DeviceNo=@DeviceNo, LashRefresh = @LashRefresh,Status=1 where SessinId=@SessinId");
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(){  ParameterName="@SessinId",Value=model.SessinId},
                new SqlParameter(){  ParameterName="@DeviceNo",Value=model.DeviceNo},
                new SqlParameter(){  ParameterName="@LashRefresh",Value=model.LashRefresh}
                };
            int rowCount = helper.ExecuteNonQuery(builder.ToString(), parameters);
            if (rowCount == 0)//影响行数
            {
                helper.AddSession(model);
            }
        }
        /// <summary>
        /// 删除session
        /// </summary>
        /// <param name="sessinId"></param>
        public static void Delete(string sessinId)
        {
            if (sessinId.IsNullOrWhiteSpace())
            {
                return;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("delete LoginSessinInfo where SessinId=@SessinId");
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(){  ParameterName="@SessinId",Value=sessinId}
                };
            int rowCount = helper.ExecuteNonQuery(builder.ToString(), parameters);

        }
        private int ExecuteNonQuery(string commandText, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connctionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddRange(parameters);
                    connection.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        private DataTable ExecuteQuery(string commandText, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connctionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, connection))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
