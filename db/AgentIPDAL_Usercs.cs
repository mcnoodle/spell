using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SQLite;
using SqliteDAL;

namespace DAL
{
    public partial class AgentIPDAL
    {
        #region 自定义
        AgentIPModel getAgentIPInner(SQLiteDataReader sr)
        {
            bool acknowledge;
            AgentIPModel model = new AgentIPModel();

            if (sr.GetValue(0) != DBNull.Value)
                model.ip = sr.GetString(0);
            if (sr.GetValue(1) != DBNull.Value)
                model.port = sr.GetString(1);

            try
            {
                if (sr.GetString(2) != "")
                    model.LastCheckData = Convert.ToDateTime(sr.GetString(2));
            }
            catch (Exception e)
            { }
            if (sr.GetValue(3) != DBNull.Value)
                model.Active = Convert.ToBoolean(sr.GetString(3));

            if (sr.GetValue(4) != DBNull.Value)
                model.LastSuccess =Convert.ToBoolean( sr.GetString(4));

            return model;
        }


        /// <summary>
        /// get event by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<AgentIPModel> GetAgentIPByUpdateDate(DateTime lastUp)
        {
            try
            {
                string condtion = string.Format("  [LastCheckData] >= '{0}' ", lastUp);

                string searchSql = string.Format(Select_AgentIPByCondition, condtion);

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

        public List<AgentIPModel> GetAgentIPByOldDate(DateTime lastUp)
        {
            try
            {
                string condtion = string.Format("  [LastCheckData] <= '{0}' ", lastUp);

                string searchSql = string.Format(Select_AgentIPByCondition, condtion);

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

        #endregion
    }
}
