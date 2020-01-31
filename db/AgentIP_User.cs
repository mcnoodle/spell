using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using Model;
using DAL;
namespace BLL
{
    public partial class AgentIPBLL
    {
        #region 自定义
        public void SetAgent(AgentIPModel ObjModel, bool active)
        {
            List<AgentIPModel> aList = dal.GetAgentIP(ObjModel);
            if (aList.Count <= 0)
            {
                ObjModel.Active = active;
                ObjModel.LastCheckData = DateTime.Now;
                dal.InsertAgentIPModel(ObjModel);
            }
        }
        public List<AgentIPModel> GetAgentIPByUpdateDate(DateTime lastUp)
        {
            return dal.GetAgentIPByUpdateDate(lastUp);
        }
        public List<AgentIPModel> GetAgentIPByOldDate(DateTime lastUp)
        {
            return dal.GetAgentIPByOldDate(lastUp);
        }
        #endregion
    }
}
