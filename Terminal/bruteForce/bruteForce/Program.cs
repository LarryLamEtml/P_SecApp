﻿using System;

namespace bruteForce
{
    class Program
    {
        static void Main(string[] args)
        {
            String password;
            DateTime dt = DateTime.Now;
            BruteForceHTTP btForce = new BruteForceHTTP("http://127.0.0.1:8080/edsa-shop/index.php?controller=login&action=login", "login", "password", @"C:\Users\vuffrayju\Downloads\test.txt");
            
            password = btForce.findPassword();
            DateTime dt2 = DateTime.Now;
            Console.Write(password+"\n"+(dt2-dt).TotalSeconds+"s");
            Console.ReadLine();
        }


    }
}
