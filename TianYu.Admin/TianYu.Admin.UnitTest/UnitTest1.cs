using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TianYu.Admin.Domain;
using TianYu.Admin.Infrastructure.Constant;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SystemMenuHelper.GetSytemMenuList<SystemMenuItem>();
        }
    }
    [TestClass]
    public class tata
    {
        string connectionString = "server=192.168.253.136;uid=sa;pwd=sa1234;database=TianYu;MultipleActiveResultSets=True;";
        
        [TestMethod]
        public void Test()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string queryTableSql = @"SELECT a.name as tb_name, b.value as tb_desc FROM sys.objects a LEFT JOIN [sys].[extended_properties] b ON a.object_id = b.major_id AND b.minor_id = 0 WHERE a.type = 'U'";
            string queryColunmSql = @"
SELECT 
    表名       = case when a.colorder=1 then d.name else '' end,
    表说明     = case when a.colorder=1 then isnull(f.value,'') else '' end,
    字段序号   = a.colorder,
    字段名     = a.name,
    标识       = case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,
    主键       = case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                     SELECT name FROM sysindexes WHERE indid in( SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) then '√' else '' end,
    类型       = b.name,
    占用字节数 = a.length,
    长度       = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
    小数位数   = isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),
    允许空     = case when a.isnullable=1 then '√'else '' end,
    默认值     = isnull(e.text,''),
    字段说明   = isnull(g.[value],'')
FROM  syscolumns a
left join  systypes b  on   a.xusertype=b.xusertype
inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
left join syscomments e  on  a.cdefault=e.id
left join sys.extended_properties   g  on   a.id=G.major_id and a.colid=g.minor_id  
left join sys.extended_properties f on  d.id=f.major_id and f.minor_id=0
where   d.name= '@tableName'    --如果只查询指定表,加上此where条件，tablename是要查询的表名；去除where条件查询所有的表信息
order by  a.id,a.colorder";
            //string querySql = "SET FMTONLY ON; select * from @tableName; SET FMTONLY OFF;";
            SqlCommand command = new SqlCommand(queryTableSql, conn);
            SqlDataAdapter ad = new SqlDataAdapter(command);
            System.Data.DataSet ds = new DataSet();
            System.Data.DataSet ds2 = new DataSet();
            ad.Fill(ds);
             
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                ds2.Tables.Clear();
                string tb_name = row["tb_name"].ToString();
                string tb_desc = row["tb_desc"].ToString();
                command.CommandText = queryColunmSql.Replace("@tableName", tb_name);
                ad.Fill(ds2); 
            }
            conn.Close();
        }
    }
}
