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
            
           
            //var list = br.Query<HjsfSysUser>();
            //Console.WriteLine(list.FirstOrDefault().UserName);
            //HJSFContext c = new HJSFContext();
            //Console.WriteLine(c.HfjsSysUser.FirstOrDefault().UserName);
        }
    }
}
