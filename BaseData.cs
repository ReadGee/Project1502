using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v2
{
    public class BaseData
    {
        public static string StringConnected = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Trade;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static bool firstStart = true;
        public static int sumParse = 0;
    }
}
