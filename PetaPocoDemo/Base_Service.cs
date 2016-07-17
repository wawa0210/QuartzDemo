//------------------------------------------------------------------------------------------------
//
//
//         All rights reserved
//
//		   filename:Base_Service
//		   desciption:
//
//		   created by 张潇 at 2016/5/25 14:03:08
//
//------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPocoDemo
{
    [PetaPoco.TableName("Base_Service")]
    [PetaPoco.PrimaryKey("ServiceCode")]
    public class Base_Service
    {

        public string ServiceCode
        {
            get;
            set;
        }
        public string ServiceName { get; set; }
        public string ServiceShowName { get; set; }
        public string ProviderDll { get; set; }
        public string ProviderClass { get; set; }
        public string Params { get; set; }
        public int Frequence { get; set; }
        public bool Enable { get; set; }
        public string Memo { get; set; }
        public string CreateTime { get; set; }
    }
}
