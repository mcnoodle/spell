using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data.SQLite;
using SqliteDAL;
using Model;
namespace DAL
{
    public partial class SecuriteSiteDAL
    {
        const string Insert_SecuriteSite = "Insert into SecuriteSite([SecurityCodeUrl],[CodeNum],[Name],[Backup1],[Backup2]) values('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' )";

    }
}
