using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using SqliteDAL;
using Model; 
namespace DAL
{
    ///################################################################################################
    /// <summary>
    /// <para>摘要：AgentIPDAL 类，封装数据表基本应用。</para>
    /// <para>说明：</para>
    /// <para>Programmer： Sean</para>
    /// <para>Email： </para>
    /// </summary>
    ///################################################################################################
    public partial class AgentIPDAL
    {
		#region 自定义
        
        #endregion
	
	 
        #region -  属性集合  ------------------------------------------------------------

        /// <summary>
        /// 数据库连接对象
        /// </summary> 
        SQLiteDataReader  sr;

		const string Select_AllAgentIP = "Select [ip],[port],[LastCheckData],[Active],[LastSuccess] from AgentIP order by ip desc";
        const string Select_AgentIPByCondition = "Select [ip],[port],[LastCheckData],[Active],[LastSuccess] from AgentIP where {0} order by ip desc";
        const string Insert_AgentIP = "Insert into AgentIP([ip],[port],[LastCheckData],[Active],[LastSuccess]) values('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' )";
        const string Select_TotalCount = "Select count(0) from AgentIP where {0}";
        /// <summary>
        /// 0 condition,1 pagesize,2 startindex
        /// </summary>
        const string Select_AgentIPByConditionPage = " Select [ip],[port],[LastCheckData],[Active],[LastSuccess] from AgentIP where {0} order by ip desc limit {1} offset {2} ";

        #endregion ----------------------------------------------------------------------

        #region -  公有方法  ------------------------------------------------------------
		    /// <summary>
        /// get all event
        /// </summary>
        /// <returns></returns>
        public List<AgentIPModel> GetAllAgentIP()
        {
            try
            { 
                sr = Sqlite.ExecuteReader(Select_AllAgentIP);
                List<AgentIPModel> list = new List<AgentIPModel>();
                while (sr.Read())
                { 
                    list.Add(getAgentIPInner(sr));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sr.Close();
            }
        }

        /// <summary>
        /// get total rows count
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount(AgentIPModel condition)
        {
            try
            { 
                 StringBuilder sbCondition = GetConditionString(condition);
                string searchSql = string.Format(Select_TotalCount, sbCondition.ToString());
        
                object count;
                count = Sqlite.ExecuteScalar(searchSql);
                int intcount = 0;
                int.TryParse(count + "", out intcount);

                return intcount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            }
        }
		
		StringBuilder GetConditionString(AgentIPModel condition)
        {
            StringBuilder sbCondition = new StringBuilder(" 1=1 "); 
			
			if (!string.IsNullOrEmpty(condition.ip))
			{
				sbCondition.AppendFormat(" and [ip] = '{0}' ",condition.ip);
			}
			if (!string.IsNullOrEmpty(condition.port))
			{
				sbCondition.AppendFormat(" and [port] = '{0}' ",condition.port);
			}
			if (condition.LastCheckData != null && condition.LastCheckData != DateTime.MinValue)
			{
				sbCondition.AppendFormat(" and [LastCheckData] = '{0}' ",condition.LastCheckData);
			}
            if (condition.LastSuccess != null)
            {
                sbCondition.AppendFormat(" and [LastSuccess] = '{0}' ", condition.LastSuccess);
            }
			
            return sbCondition;
        }
 

        /// <summary>
        /// get event by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<AgentIPModel> GetAgentIP(AgentIPModel condition)
        {
            try
            {   
                StringBuilder sbCondition = GetConditionString(condition);

                string searchSql = string.Format(Select_AgentIPByCondition, sbCondition.ToString()); 

                sr = Sqlite.ExecuteReader(searchSql);
                List<AgentIPModel> AgentIPList = new List<AgentIPModel>();
                while (sr.Read())
                { 
                    AgentIPList.Add(getAgentIPInner(sr));
                }
                return AgentIPList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sr.Close();
            }
        }
       

		public List<AgentIPModel> GetAgentIP(AgentIPModel condition,int pageSize,int pageIndex)
        {
            try
            {

                StringBuilder sbCondition = GetConditionString(condition);
                int startindex = (pageIndex-1)*pageSize;
                string searchSql = string.Format(Select_AgentIPByConditionPage, sbCondition.ToString(),pageSize,startindex);


                
                sr = Sqlite.ExecuteReader(searchSql);
                List<AgentIPModel> AgentIPList = new List<AgentIPModel>();
                while (sr.Read())
                { 
                    AgentIPList.Add(getAgentIPInner(sr));
                }
                return AgentIPList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sr.Close();
                
            }
        }


        public void InsertAgentIPModel(AgentIPModel data)
        {
            try
            {
                string executeInsert = string.Format(Insert_AgentIP,  data.ip , data.port , data.LastCheckData , data.Active , data.LastSuccess );

                int retCount = Sqlite.ExecuteNonQuery(executeInsert);
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            }
        }
		
		/// <summary>
        /// Update
        /// </summary>
        /// <param name="mObj"></param>
        /// <returns></returns>
        public void Update(AgentIPModel mObj)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update AgentIP set "); 
                StringBuilder sbCondition = new StringBuilder();
				
				if (mObj.ip != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" ip = '{0}' ", mObj.ip);
				}	
				if (mObj.port != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" port = '{0}' ", mObj.port);
				}	
				if (mObj.LastCheckData != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" LastCheckData = '{0}' ", mObj.LastCheckData);
				}	
				if (mObj.Active != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" Active = '{0}' ", mObj.Active);
				}	
				if (mObj.LastSuccess != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" LastSuccess = '{0}' ", mObj.LastSuccess);
				}	
				 
                strSql.Append(sbCondition.ToString());
                strSql.AppendFormat(" where [ip]='{0}'",mObj.ip);
                 
                object ret = Sqlite.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            }

        }
 

        #endregion ----------------------------------------------------------------------

     }
}
