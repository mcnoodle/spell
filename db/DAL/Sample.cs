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
    /// <para>摘要：SampleDAL 类，封装数据表基本应用。</para>
    /// <para>说明：</para>
    /// <para>Programmer： Sean</para>
    /// <para>Email： </para>
    /// </summary>
    ///################################################################################################
    public partial class SampleDAL
    {
		#region 自定义
        
        #endregion
	
	 
        #region -  属性集合  ------------------------------------------------------------

        /// <summary>
        /// 数据库连接对象
        /// </summary> 
        SQLiteDataReader  sr;

		const string Select_AllSample = "Select [Code],[TrueValue],[SecuritySiteId],[Backup1],[Backup2] from Sample order by Code desc";
        const string Select_SampleByCondition = "Select [Code],[TrueValue],[SecuritySiteId],[Backup1],[Backup2] from Sample where {0} order by Code desc";
        const string Insert_Sample = "Insert into Sample([Code],[TrueValue],[SecuritySiteId],[Backup1],[Backup2]) values('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' )";
        const string Select_TotalCount = "Select count(0) from Sample where {0}";
        /// <summary>
        /// 0 condition,1 pagesize,2 startindex
        /// </summary>
        const string Select_SampleByConditionPage = " Select [Code],[TrueValue],[SecuritySiteId],[Backup1],[Backup2] from Sample where {0} order by Code desc limit {1} offset {2} ";

        #endregion ----------------------------------------------------------------------

        #region -  公有方法  ------------------------------------------------------------
		    /// <summary>
        /// get all event
        /// </summary>
        /// <returns></returns>
        public List<SampleModel> GetAllSample()
        {
            try
            { 
                sr = Sqlite.ExecuteReader(Select_AllSample);
                List<SampleModel> list = new List<SampleModel>();
                while (sr.Read())
                { 
                    list.Add(getSampleInner(sr));
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
        public int GetTotalCount(SampleModel condition)
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
		
		StringBuilder GetConditionString(SampleModel condition)
        {
            StringBuilder sbCondition = new StringBuilder(" 1=1 "); 
			
			if (!string.IsNullOrEmpty(condition.Code))
			{
				sbCondition.AppendFormat(" and [Code] = '{0}' ",condition.Code);
			}
			if (!string.IsNullOrEmpty(condition.TrueValue))
			{
				sbCondition.AppendFormat(" and [TrueValue] = '{0}' ",condition.TrueValue);
			}
			if (0!= condition.SecuritySiteId)
			{
				sbCondition.AppendFormat(" and [SecuritySiteId] = '{0}' ",condition.SecuritySiteId);
			}
			if (!string.IsNullOrEmpty(condition.Backup1))
			{
				sbCondition.AppendFormat(" and [Backup1] = '{0}' ",condition.Backup1);
			}
			if (!string.IsNullOrEmpty(condition.Backup2))
			{
				sbCondition.AppendFormat(" and [Backup2] = '{0}' ",condition.Backup2);
			}
            return sbCondition;
        }
 

        /// <summary>
        /// get event by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<SampleModel> GetSample(SampleModel condition)
        {
            try
            {   
                StringBuilder sbCondition = GetConditionString(condition);

                string searchSql = string.Format(Select_SampleByCondition, sbCondition.ToString()); 

                sr = Sqlite.ExecuteReader(searchSql);
                List<SampleModel> SampleList = new List<SampleModel>();
                while (sr.Read())
                { 
                    SampleList.Add(getSampleInner(sr));
                }
                return SampleList;
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
        SampleModel getSampleInner(SQLiteDataReader sr)
        {
            bool acknowledge;
            SampleModel model = new SampleModel();
            
			if (sr.GetValue(0) != DBNull.Value)
                model.Code = sr.GetString(0);
			if (sr.GetValue(1) != DBNull.Value)
                model.TrueValue = sr.GetString(1);
			if (sr.GetValue(2) != DBNull.Value)
                model.SecuritySiteId = sr.GetInt32(2);
			if (sr.GetValue(3) != DBNull.Value)
                model.Backup1 = sr.GetString(3);
			if (sr.GetValue(4) != DBNull.Value)
                model.Backup2 = sr.GetString(4);
			
			return model;
        }

		public List<SampleModel> GetSample(SampleModel condition,int pageSize,int pageIndex)
        {
            try
            {

                StringBuilder sbCondition = GetConditionString(condition);
                int startindex = (pageIndex-1)*pageSize;
                string searchSql = string.Format(Select_SampleByConditionPage, sbCondition.ToString(),pageSize,startindex);


                
                sr = Sqlite.ExecuteReader(searchSql);
                List<SampleModel> SampleList = new List<SampleModel>();
                while (sr.Read())
                { 
                    SampleList.Add(getSampleInner(sr));
                }
                return SampleList;
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


        public void InsertSampleModel(SampleModel data)
        {
            try
            {
                string executeInsert = string.Format(Insert_Sample,  data.Code , data.TrueValue , data.SecuritySiteId , data.Backup1 , data.Backup2 );

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
        public void Update(SampleModel mObj)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Sample set "); 
                StringBuilder sbCondition = new StringBuilder();
				
				if (mObj.Code != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" Code = '{0}' ", mObj.Code);
				}	
				if (mObj.TrueValue != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" TrueValue = '{0}' ", mObj.TrueValue);
				}	
				if (mObj.SecuritySiteId != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" SecuritySiteId = '{0}' ", mObj.SecuritySiteId);
				}	
				if (mObj.Backup1 != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" Backup1 = '{0}' ", mObj.Backup1);
				}	
				if (mObj.Backup2 != null)
				{
					if (sbCondition.Length > 0)
                        sbCondition.Append(",");
					sbCondition.AppendFormat(" Backup2 = '{0}' ", mObj.Backup2);
				}	
				 
                strSql.Append(sbCondition.ToString());
                strSql.AppendFormat(" where [Code]='{0}'",mObj.Code);
                 
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
