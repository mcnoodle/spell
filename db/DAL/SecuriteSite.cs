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
    /// <para>摘要：SecuriteSiteDAL 类，封装数据表基本应用。</para>
    /// <para>说明：</para>
    /// <para>Programmer： Sean</para>
    /// <para>Email： </para>
    /// </summary>
    ///################################################################################################
    public partial class SecuriteSiteDAL
    {
        #region 自定义

        #endregion


        #region -  属性集合  ------------------------------------------------------------

        /// <summary>
        /// 数据库连接对象
        /// </summary> 
        SQLiteDataReader sr;

        const string Select_AllSecuriteSite = "Select [SecurityCodeUrl],[CodeNum],[Name],[Backup1],[Backup2],[id] from SecuriteSite order by SecurityCodeUrl desc";
        const string Select_SecuriteSiteByCondition = "Select [SecurityCodeUrl],[CodeNum],[Name],[Backup1],[Backup2],[id] from SecuriteSite where {0} order by SecurityCodeUrl desc";
        const string Select_TotalCount = "Select count(0) from SecuriteSite where {0}";
        /// <summary>
        /// 0 condition,1 pagesize,2 startindex
        /// </summary>
        const string Select_SecuriteSiteByConditionPage = " Select [SecurityCodeUrl],[CodeNum],[Name],[Backup1],[Backup2],[id] from SecuriteSite where {0} order by SecurityCodeUrl desc limit {1} offset {2} ";

        #endregion ----------------------------------------------------------------------

        #region -  公有方法  ------------------------------------------------------------
        /// <summary>
        /// get all event
        /// </summary>
        /// <returns></returns>
        public List<SecuriteSiteModel> GetAllSecuriteSite()
        {
            try
            {
                sr = Sqlite.ExecuteReader(Select_AllSecuriteSite);
                List<SecuriteSiteModel> list = new List<SecuriteSiteModel>();
                while (sr.Read())
                {
                    list.Add(getSecuriteSiteInner(sr));
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
        public int GetTotalCount(SecuriteSiteModel condition)
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

        StringBuilder GetConditionString(SecuriteSiteModel condition)
        {
            StringBuilder sbCondition = new StringBuilder(" 1=1 ");

            if (!string.IsNullOrEmpty(condition.SecurityCodeUrl))
            {
                sbCondition.AppendFormat(" and [SecurityCodeUrl] = '{0}' ", condition.SecurityCodeUrl);
            }
            if (0 != condition.CodeNum)
            {
                sbCondition.AppendFormat(" and [CodeNum] = '{0}' ", condition.CodeNum);
            }
            if (!string.IsNullOrEmpty(condition.Name))
            {
                sbCondition.AppendFormat(" and [Name] = '{0}' ", condition.Name);
            }
            if (!string.IsNullOrEmpty(condition.Backup1))
            {
                sbCondition.AppendFormat(" and [Backup1] = '{0}' ", condition.Backup1);
            }
            if (!string.IsNullOrEmpty(condition.Backup2))
            {
                sbCondition.AppendFormat(" and [Backup2] = '{0}' ", condition.Backup2);
            }
            if (0 != condition.id)
            {
                sbCondition.AppendFormat(" and [id] = '{0}' ", condition.id);
            }
            return sbCondition;
        }


        /// <summary>
        /// get event by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<SecuriteSiteModel> GetSecuriteSite(SecuriteSiteModel condition)
        {
            try
            {
                StringBuilder sbCondition = GetConditionString(condition);

                string searchSql = string.Format(Select_SecuriteSiteByCondition, sbCondition.ToString());

                sr = Sqlite.ExecuteReader(searchSql);
                List<SecuriteSiteModel> SecuriteSiteList = new List<SecuriteSiteModel>();
                while (sr.Read())
                {
                    SecuriteSiteList.Add(getSecuriteSiteInner(sr));
                }
                return SecuriteSiteList;
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
        SecuriteSiteModel getSecuriteSiteInner(SQLiteDataReader sr)
        {
            bool acknowledge;
            SecuriteSiteModel model = new SecuriteSiteModel();

            if (sr.GetValue(0) != DBNull.Value)
                model.SecurityCodeUrl = sr.GetString(0);
            if (sr.GetValue(1) != DBNull.Value)
                model.CodeNum = sr.GetInt32(1);
            if (sr.GetValue(2) != DBNull.Value)
                model.Name = sr.GetString(2);
            if (sr.GetValue(3) != DBNull.Value)
                model.Backup1 = sr.GetString(3);
            if (sr.GetValue(4) != DBNull.Value)
                model.Backup2 = sr.GetString(4);
            if (sr.GetValue(5) != DBNull.Value)
                model.id = sr.GetInt32(5);

            return model;
        }

        public List<SecuriteSiteModel> GetSecuriteSite(SecuriteSiteModel condition, int pageSize, int pageIndex)
        {
            try
            {

                StringBuilder sbCondition = GetConditionString(condition);
                int startindex = (pageIndex - 1) * pageSize;
                string searchSql = string.Format(Select_SecuriteSiteByConditionPage, sbCondition.ToString(), pageSize, startindex);



                sr = Sqlite.ExecuteReader(searchSql);
                List<SecuriteSiteModel> SecuriteSiteList = new List<SecuriteSiteModel>();
                while (sr.Read())
                {
                    SecuriteSiteList.Add(getSecuriteSiteInner(sr));
                }
                return SecuriteSiteList;
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


        public void InsertSecuriteSiteModel(SecuriteSiteModel data)
        {
            try
            {
                string executeInsert = string.Format(Insert_SecuriteSite, data.SecurityCodeUrl, data.CodeNum, data.Name, data.Backup1, data.Backup2);

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
        public void Update(SecuriteSiteModel mObj)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update SecuriteSite set ");
                StringBuilder sbCondition = new StringBuilder();

                if (mObj.SecurityCodeUrl != null)
                {
                    if (sbCondition.Length > 0)
                        sbCondition.Append(",");
                    sbCondition.AppendFormat(" SecurityCodeUrl = '{0}' ", mObj.SecurityCodeUrl);
                }
                if (mObj.CodeNum != null)
                {
                    if (sbCondition.Length > 0)
                        sbCondition.Append(",");
                    sbCondition.AppendFormat(" CodeNum = '{0}' ", mObj.CodeNum);
                }
                if (mObj.Name != null)
                {
                    if (sbCondition.Length > 0)
                        sbCondition.Append(",");
                    sbCondition.AppendFormat(" Name = '{0}' ", mObj.Name);
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
                if (mObj.id != null)
                {
                    if (sbCondition.Length > 0)
                        sbCondition.Append(",");
                    sbCondition.AppendFormat(" id = '{0}' ", mObj.id);
                }

                strSql.Append(sbCondition.ToString());
                strSql.AppendFormat(" where [SecurityCodeUrl]='{0}'", mObj.SecurityCodeUrl);

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
