using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// </table>
    /// </remarks>
    /// </summary>
    ///################################################################################################
    public partial class AgentIPModel
    {
 
        #region Custom
        /// <summary>
        /// 
        /// </summary>
        private DateTime _LastCheckData_User;
        /// <summary>
        /// 最近一次使用的更新ip日期
        /// </summary>
        public DateTime LastCheckData_User
        {
            set { _LastCheckData_User = value; }
            get { return _LastCheckData_User; }
        }
        #endregion

    }
}
