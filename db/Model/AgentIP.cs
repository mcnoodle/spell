using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Model
{
    ///################################################################################################
    /// <summary>
    /// <para>摘要：AgentIPModel 类，业务模型。</para>
    /// <para>说明：</para>
    /// <para>Programmer： Sean</para>
    /// <para>Email： </para>
	/// <remarks>
    /// 对应数据库表：AgentIP
    /// <table class="dtTABLE" cellspacing="0">
    /// <tr valign="top"><th>序号</th><th>列名</th><th>数据类型</th><th>长度</th><th>小数位</th><th>标识</th><th>主键</th><th>允许空</th><th>默认值</th><th>字段说明</th></tr>
    /// <tr valign="top"><td>1</td><td>ip</td><td>varchar</td><td>2147483647</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// <tr valign="top"><td>2</td><td>port</td><td>varchar</td><td>2147483647</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// <tr valign="top"><td>3</td><td>LastCheckData</td><td>date</td><td>8</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// <tr valign="top"><td>4</td><td>Active</td><td>boolean</td><td>1</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// <tr valign="top"><td>5</td><td>LastSuccess</td><td>boolean</td><td>1</td><td></td><td></td><td></td><td>√</td><td></td><td></td></tr>
    /// </table>
    /// </remarks>
    /// </summary>
    ///################################################################################################
    public partial class AgentIPModel
    {
		#region Custom
        
        #endregion
	
        #region -  公共属性  ------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        private string _ip;
        /// <summary>
        /// 
        /// </summary>
        public string ip
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _port;
        /// <summary>
        /// 
        /// </summary>
        public string port
        {
            set { _port = value; }
            get { return _port; }
        }
        /// <summary>
        /// 
        /// </summary>
        private DateTime _LastCheckData;
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastCheckData
        {
            set { _LastCheckData = value; }
            get { return _LastCheckData; }
        }
        /// <summary>
        /// 
        /// </summary>
        private bool _Active;
        /// <summary>
        /// 
        /// </summary>
        public bool Active
        {
            set { _Active = value; }
            get { return _Active; }
        }
        /// <summary>
        /// 
        /// </summary>
        private bool _LastSuccess;
        /// <summary>
        /// 
        /// </summary>
        public bool LastSuccess
        {
            set { _LastSuccess = value; }
            get { return _LastSuccess; }
        }
		
        #endregion ----------------------------------------------------------------------
    } 
}
