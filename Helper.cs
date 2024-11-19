using demo1111.Context;
using demo1111.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo1111
{
    public static class Helper
    {
        public static readonly MyprojContext mycontext = new MyprojContext();
        public static User currUser;
        public static bool isGuest = false;
        public static int role;
        public static Zakaz zakaz = new Zakaz();
        public static List<Prod> prodsZakaz = new List<Prod>();
        public static List<ZakazProd> zakazPrs = new List<ZakazProd>();
    }
}
