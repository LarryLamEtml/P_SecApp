/* ETML
 * 
 * Author : Larry Lam, Justin Vuffray
 * Date : 06.11.2017
 * Description : Traitement de la vue.
 * 
 * 
 * */

using System.Net;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace BruteForce
{
    class BruteForceHTTP
    {
        public string getUsername = "";
        public string getPassword = "";

        private string badRequest = "";
        public string passwordRight = "";

        //Dictionnaire
        private string[] allPassword;
        private string dictionnaryPath;

        private int nbLine;



        //Checkboxes
        private bool majuscules = false;
        private bool minuscules = false;
        private bool numbers = false;
        private bool symbols = false;

        //Methode
        private string method;//1 = post, 2 = get
        private int mode = 1;//1 = dictionnary, 2 = generated
        private int methodNum;

        //Max & Min char
        private int maxChar;
        private int minChar;

        private string url;
        private string login;
        private string password;
        private string username;

        WebClient wb = new WebClient();
        NameValueCollection data = new NameValueCollection();

        public BruteForceHTTP(string _url, string _login, string _password, string _path, bool _majuscules, bool _minuscules, bool _numbers, bool _symbols, int _method, int _mode, int _maxChar, int _minChar, string _username)
        {
            url = _url;
            login = _login;
            password = _password;
            dictionnaryPath = _path;
            majuscules = _majuscules;
            minuscules = _minuscules;
            numbers = _numbers;
            symbols = _symbols;
            maxChar = _maxChar;
            minChar = _minChar;
            url = _url;
            login = _login;
            password = _password;
            username = _username;
            method = (_method == 1 ? "POST" : "GET");
            mode = _mode;
            methodNum = _method;

            if (mode == 1)
            {
                ReadDictionnary();
            }
            else
            {
                generatePasswords();
            }

        }

        private void generatePasswords()
        {
            RandomPassword password =  new RandomPassword(minChar, maxChar, minuscules, majuscules, numbers, symbols);
            do
            {
                string psw = password.Generate();

                //POST
                if (methodNum == 1)
                {
                    POSTRequest(psw);
                }
                else//GET
                {
                    GETRequest(psw);

                }
            } while (true);

            /*
            foreach (string s in allPassword)
            {
                //S'il y a des majuscules
                if (s.Any(char.IsUpper) == majuscules)
                {
                    //S'il y a des minuscules
                    if (s.Any(char.IsLower) == minuscules)
                    {
                        //S'il y a des chiffres
                        if (s.Any(char.IsDigit) == numbers)
                        {
                            //S'il y a des symbols
                            if (s.Any(ch => !Char.IsLetter(ch) == symbols))
                            {
                                //Si le mot respecte la taille min et max définie
                                if (s.Length >= minChar && s.Length <= maxChar)
                                {
                                    //POST
                                    if (methodNum == 1)
                                    {
                                        POSTRequest(s);
                                    }
                                    else//GET
                                    {
                                        GETRequest(s);

                                    }
                                }
                            }
                        }
                    }
                }
            }*/
        }


        private void ReadDictionnary()
        {
            try
            {
                allPassword = System.IO.File.ReadAllLines(dictionnaryPath);
                nbLine = allPassword.Count();
            }
            catch
            {
                MessageBox.Show("Le dictionnaire est introuvable...");
                allPassword = null;
                nbLine = 0;
            }
            foreach (string s in allPassword)
            {
                //POST
                if (methodNum == 1)
                {
                    POSTRequest(s);
                }
                else//GET
                {
                    GETRequest(s);

                }
            }
        }

        private void POSTRequest(string _password)
        {
            data[login] = username;
            data[password] = _password;
            var response = wb.UploadValues(url, method, data);
        }


        private void GETRequest(string _password)
        {
            var response = wb.DownloadString(url);

        }



    }
}

