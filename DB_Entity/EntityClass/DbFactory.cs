using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Entity.EntityClass
{
    public class DbFactory
    {
        public static string ConnectionString = "Data Source=WAWA0210\\WAWA0210;Initial Catalog=SunyuCRM_V3;Persist Security Info=True;User ID=sa;Password=ZHANGXIAO1824668;Max Pool Size=200";
        public static DataBaseDataContext CreateContext()
        {
            DataBaseDataContext db = new DataBaseDataContext(ConnectionString);
            db.CommandTimeout = 6000;
            return db;
        }
    }
}
