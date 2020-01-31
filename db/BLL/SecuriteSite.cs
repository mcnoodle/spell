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
    public partial class SecuriteSiteBLL
    {
	    #region 自定义
        
        #endregion
		
		
		SecuriteSiteDAL  dal = new SecuriteSiteDAL();
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ObjModel">模型对象</param>
        /// <returns>产生主键值</returns>
        public void Insert(SecuriteSiteModel ObjModel )
        {
            dal.InsertSecuriteSiteModel(ObjModel);
        }
		
		
		public List<SecuriteSiteModel> GetAllSecuriteSite()
        {
            return dal.GetAllSecuriteSite();
        }

        /// <summary>
        /// get SecuriteSite list by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<SecuriteSiteModel> GetSecuriteSite(SecuriteSiteModel condition)
        {
            return dal.GetSecuriteSite(condition);
        }


        public List<SecuriteSiteModel> GetSecuriteSite(SecuriteSiteModel condition,int pageSize,int pageIndex)
        {
            return dal.GetSecuriteSite(condition,pageSize,pageIndex);
        }
     

        public int GetTotalCount(SecuriteSiteModel condition)
        {
            return dal.GetTotalCount( condition);
        } 
		
		public void Update(SecuriteSiteModel ObjModel )
        {
            dal.Update(ObjModel);
        }
    }
}
