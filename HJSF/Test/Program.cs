using HJSF.ORM.Models;
using RepositoryServices;
using System;
using System.Data;
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseRepository br = new BaseRepository();
            string msg = "";
            DataTable dt = br.QueryTableSql("select * from hjsf_sysuser", ref msg);
            Console.WriteLine(dt.Rows[0]["UserName"]);
            //var list = br.Query<HjsfSysUser>();
            //Console.WriteLine(list.FirstOrDefault().UserName);
            //HJSFContext c = new HJSFContext();
            //Console.WriteLine(c.HfjsSysUser.FirstOrDefault().UserName);
        }
    }
}
