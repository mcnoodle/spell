using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Model
{
    ///################################################################################################
    /// <summary>
    /// <para>摘要：SecuriteSiteModel 类，业务模型。</para>
    /// <para>说明：</para>
    /// <para>Programmer： Sean</para>
    /// <para>Email： </para>
	/// <remarks>
    /// 对应数据库表：SecuriteSite
    /// <table class="dtTABLE" cellspacing="0">
    /// <tr valign="top"><th>序号</th><th>列名</th><th>数据类型</th><th>长度</th><th>小数位</th><th>标识</th><th>主键</th><th>允许空</th><th>默认值</th><th>字段说明</th></tr>
    /// <tr valign="top"><td>1</td><td>SecurityCodeUrl</td><td>varchar </td><td>300</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// <tr valign="top"><td>2</td><td>CodeNum</td><td>integer</td><td>8</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// <tr valign="top"><td>3</td><td>Name</td><td>varchar </td><td>300</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// <tr valign="top"><td>4</td><td>Backup1</td><td>varchar </td><td>50</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// <tr valign="top"><td>5</td><td>Backup2</td><td>varchar </td><td>50</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// <tr valign="top"><td>6</td><td>id</td><td>integer</td><td>8</td><td></td><td>√</td><td>√</td><td></td><td></td><td></td></tr>
    /// </table>
    /// </remarks>
    /// </summary>
    ///################################################################################################
    public partial class SecuriteSiteModel
    {
		#region Custom
        
        #endregion
	
        #region -  公共属性  ------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        private string _SecurityCodeUrl;
        /// <summary>
        /// 
        /// </summary>
        public string SecurityCodeUrl
        {
            set { _SecurityCodeUrl = value; }
            get { return _SecurityCodeUrl; }
        }
        /// <summary>
        /// 
        /// </summary>
        private int _CodeNum;
        /// <summary>
        /// 
        /// </summary>
        public int CodeNum
        {
            set { _CodeNum = value; }
            get { return _CodeNum; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _Name;
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _Backup1;
        /// <summary>
        /// 
        /// </summary>
        public string Backup1
        {
            set { _Backup1 = value; }
            get { return _Backup1; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _Backup2;
        /// <summary>
        /// 
        /// </summary>
        public string Backup2
        {
            set { _Backup2 = value; }
            get { return _Backup2; }
        }
        /// <summary>
        /// 
        /// </summary>
        private int _id;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
		
        #endregion ----------------------------------------------------------------------
    } 
}
