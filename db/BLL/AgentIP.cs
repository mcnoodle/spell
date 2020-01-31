using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using Model;
using DAL;

namespace BLL
{
    ///################################################################################################
    /// <summary>
    /// <para>摘要：业务规则类。</para>
    /// <para>说明：</para>
    /// <para>Programmer： Sean</para>
    /// <para>Email： Ziria013@hotmail.com</para>
    /// </summary>
    ///################################################################################################
    public partial class AgentIPBLL
    {
	    #region 自定义
        
        #endregion
		
		
		AgentIPDAL  dal = new AgentIPDAL();
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ObjModel">模型对象</param>
        /// <returns>产生主键值</returns>
        public void Insert(AgentIPModel ObjModel )
        {
            dal.InsertAgentIPModel(ObjModel);
        }
		
		
		public List<AgentIPModel> GetAllAgentIP()
        {
            return dal.GetAllAgentIP();
        }

        /// <summary>
        /// get AgentIP list by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<AgentIPModel> GetAgentIP(AgentIPModel condition)
        {
            return dal.GetAgentIP(condition);
        }


        public List<AgentIPModel> GetAgentIP(AgentIPModel condition,int pageSize,int pageIndex)
        {
            return dal.GetAgentIP(condition,pageSize,pageIndex);
        }
     

        public int GetTotalCount(AgentIPModel condition)
        {
            return dal.GetTotalCount( condition);
        } 
		
		public void Update(AgentIPModel ObjModel )
        {
            dal.Update(ObjModel);
        }
    }
}
