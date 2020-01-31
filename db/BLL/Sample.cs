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
    public partial class SampleBLL
    {
	    #region 自定义
        
        #endregion
		
		
		SampleDAL  dal = new SampleDAL();
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ObjModel">模型对象</param>
        /// <returns>产生主键值</returns>
        public void Insert(SampleModel ObjModel )
        {
            dal.InsertSampleModel(ObjModel);
        }
		
		
		public List<SampleModel> GetAllSample()
        {
            return dal.GetAllSample();
        }

        /// <summary>
        /// get Sample list by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<SampleModel> GetSample(SampleModel condition)
        {
            return dal.GetSample(condition);
        }


        public List<SampleModel> GetSample(SampleModel condition,int pageSize,int pageIndex)
        {
            return dal.GetSample(condition,pageSize,pageIndex);
        }
     

        public int GetTotalCount(SampleModel condition)
        {
            return dal.GetTotalCount( condition);
        } 
		
		public void Update(SampleModel ObjModel )
        {
            dal.Update(ObjModel);
        }
    }
}
