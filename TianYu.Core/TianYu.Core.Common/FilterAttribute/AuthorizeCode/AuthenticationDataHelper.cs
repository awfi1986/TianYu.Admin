using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.FilterAttribute.AuthorizeCode
{
    public class AuthenticationDataHelper
    {
        private string connctionString = ConfigHelper.GetAppsettingValue(TianYuConsts.ApiAuthorizeDatabase);
        internal AuthenticationDataHelper()
        {
            if (connctionString.IsNullOrWhiteSpace())
            {
                throw new KeyNotFoundException(string.Format("未找到配置项{0}", TianYuConsts.ApiAuthorizeDatabase));
            }
        }
        internal void AddAuthenticationTicketDetails(AuthenticationTicketDetailsModel model)
        {
            if (model.IsNull())
            {
                return;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO AuthenticationTicketDetails(TicketId,Ticket,TicketAppID,TicketSecond,LastRefreshDate,DeviceNo,ClientType,CreateTime)");
            builder.Append("VALUES(@TicketId,@Ticket,@TicketAppID,@TicketSecond,@LastRefreshDate,@DeviceNo,@ClientType,@CreateTime)");
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(){  ParameterName="@TicketId",Value=model.TicketId},
                new SqlParameter(){  ParameterName="@Ticket",Value=model.Ticket},
                new SqlParameter(){  ParameterName="@TicketAppID",Value=model.TicketAppID},
                new SqlParameter(){  ParameterName="@TicketSecond",Value=model.TicketSecond},
                new SqlParameter(){  ParameterName="@LastRefreshDate",Value=model.LastRefreshDate},
                new SqlParameter(){  ParameterName="@DeviceNo",Value=model.DeviceNo},
                new SqlParameter(){  ParameterName="@ClientType",Value=model.ClientType},
                new SqlParameter(){  ParameterName="@CreateTime",Value=model.CreateTime},
                //new SqlParameter(){  ParameterName="AppSecret",Value=model.AppSecret},
            };
            bool flag = ExecuteNonQuery(builder.ToString(), parameters) > 0;
            if (flag)
            {
                string key = TianYuConsts.GetTicketCacheKey(model.Ticket);
                Cache.CacheHelper.Insert(key, model, 7200);
            }
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
        internal void RefreshTicketDate(string token)
        {
            if (token.IsNullOrWhiteSpace())
            {
                return;
            }

            RefreshCache(token);
            Task.Run(() =>
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("UPDATE AuthenticationTicketDetails SET LastRefreshDate=@LastRefreshDate WHERE Ticket=@Ticket");
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter(){  ParameterName="Ticket",Value=token},
                new SqlParameter(){  ParameterName="LastRefreshDate",Value=DateTime.Now}
                };
                ExecuteNonQuery(builder.ToString(), parameters);
            });

        }
        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="token"></param>
        internal void RefreshCache(string token)
        {
            string key = TianYuConsts.GetTicketCacheKey(token);
            var model = Cache.CacheHelper.Get<AuthenticationTicketDetailsModel>(key);
            if (model.IsNull())
            {
                return;
            }
            Cache.CacheHelper.Remove(token);
            model.LastRefreshDate = DateTime.Now;
            Cache.CacheHelper.Insert(key, model, 7200);
        }
        internal AuthenticationTicketDetailsModel GetCheckTicket(string token)
        {
            if (token.IsNullOrWhiteSpace())
            {
                return null;
            }
            string key = TianYuConsts.GetTicketCacheKey(token);
            AuthenticationTicketDetailsModel model = Cache.CacheHelper.Get<AuthenticationTicketDetailsModel>(key);
            if (model.IsNull())
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT * FROM AuthenticationTicketDetails WHERE Ticket=@Ticket AND LastRefreshDate>@LastRefreshDate");
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter(){  ParameterName="@Ticket",Value=token},
                    new SqlParameter(){  ParameterName="@LastRefreshDate",Value=DateTime.Now.AddSeconds(-7200)}
                };
                var dt = ExecuteQuery(builder.ToString(), parameters);
                if (!dt.IsNull() && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    model = new AuthenticationTicketDetailsModel
                    {
                        ClientType = row["ClientType"].ToString(),
                        DeviceNo = row["DeviceNo"].ToString(),
                        LastRefreshDate = row["LastRefreshDate"].ToDateTime(),
                        Ticket = row["Ticket"].ToString(),
                        TicketId = Guid.Parse(row["TicketId"].ToString()),
                        TicketAppID = row["TicketAppID"].ToString(),
                        TicketSecond = row["TicketSecond"].ToInt(),
                    };
                    builder.Clear();
                    builder.Append("SELECT * FROM ApplocationAuthor WHERE AppId=@AppId");
                    parameters = new SqlParameter[]
                      {
                            new SqlParameter(){  ParameterName="@AppId",Value=model.TicketAppID}
                      };
                    dt = ExecuteQuery(builder.ToString(), parameters);
                    if (!dt.IsNull() && dt.Rows.Count > 0)
                    {
                        model.AppSecret = dt.Rows[0]["AppSecret"].ToString();
                    }
                    Cache.CacheHelper.Insert(key, model, 7200);
                }
                else
                {
                    return null;
                }
            }
            RefreshCache(token);
            return model;
        }
        /// <summary>
        /// 获取授权配置对象
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ApplocationAuthorModel GetApplocationAuthorModel(string appId)
        {
            if (appId.IsNullOrWhiteSpace())
            {
                return null;
            }
            var cacheModel = Cache.CacheHelper.Get<ApplocationAuthorModel>(appId);
            if (!cacheModel.IsNull())
            {
                return cacheModel;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT AppId,AppSecret,Status,ClientType,CreateTime FROM ApplocationAuthor WHERE AppId=@AppId AND Status=1");
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(){  ParameterName="AppId",Value=appId}
            };
            var dt = ExecuteQuery(builder.ToString(), parameters);
            if (!dt.IsNull() && dt.Rows.Count > 0)
            {
                ApplocationAuthorModel model = new ApplocationAuthorModel()
                {
                    AppId = dt.Rows[0]["AppId"].ToString(),
                    AppSecret = dt.Rows[0]["AppSecret"].ToString(),
                    ClientType = dt.Rows[0]["ClientType"].ToString(),
                    CreateTime = dt.Rows[0]["CreateTime"].ToDateTime(),
                    Status = dt.Rows[0]["Status"].ToInt(),
                };
                Cache.CacheHelper.Insert(appId, model, DateTime.Now.AddHours(12));
                return model;
            }
            else
            {
                return null;
            }

        }

        //public ApplocationAuthorModel GetApplocationAuthorModelByToken(string token)
        //{
        //    var ticketModel = GetCheckTicket(token);
        //    if (ticketModel.IsNull())
        //    {
        //        return null;
        //    }
        //    return GetApplocationAuthorModel(ticketModel.TicketAppID);
        //}

    }
}
