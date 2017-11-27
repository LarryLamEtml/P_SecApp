using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruteForce
{
    class Program
    {
        static void Main(string[] args)
        {
            String password;
            BruteForceHTTP btForce = new BruteForceHTTP("http://127.0.0.1:8080/edsa-shop/index.php?controller=login&action=login", "login", "password", @"C:\Users\vuffrayju\Downloads\rockyou.txt");
            
            password = btForce.findPassword();
            Console.Write(password);
            Console.ReadLine();
        }


    }
}
