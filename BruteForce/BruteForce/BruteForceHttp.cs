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

namespace BruteForce
{
    class BruteForceHTTP
    {

        //Dictionnaire
        private string[] dictionnaryData;
        private string dictionnaryPath;

        //Checkboxes
        private bool majuscules = false;
        private bool minuscules = false;
        private bool numbers = false;
        private bool symbols = false;

        //Methode
        private string method;//1 = post, 2 = get
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

        public BruteForceHTTP(string _url, string _login, string _password, string _path, bool _majuscules, bool _minuscules, bool _numbers, bool _symbols, int _method, int _maxChar, int _minChar, string _username)
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
            methodNum = _method;
            ReadDictionnary();
        }

        private void ReadDictionnary()
        {
            try
            {
                dictionnaryData = System.IO.File.ReadAllLines(dictionnaryPath);
            }
            catch
            {
                MessageBox.Show("Le dictionnaire est introuvable...");
                dictionnaryData = null;
            }
            foreach (string s in dictionnaryData)
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
