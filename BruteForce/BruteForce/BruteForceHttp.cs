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


        /*-------------------------------------------------------*/
      /*  public string findPassword()
        {
            //construire l'url avec des guid car c'est unique donc aucun user aura ce mdp et nom
            String _url = constructUrl(url, getUsername, Guid.NewGuid().ToString(), getPassword, Guid.NewGuid().ToString());
            //lancer la requete
            WebRequest request = WebRequest.Create(_url);
            HttpWebResponse reponse;
            try
            {
                reponse = (HttpWebResponse)request.GetResponse();
            }
            catch
            {
                return @"/!\ Site Web Inaccessible /!\";
            }
            //stocker la page avec connexion échouée
            StreamReader sr = new StreamReader(reponse.GetResponseStream());
            badRequest = sr.ReadToEnd();

            //lANCEMENT DES REQUETES EN MULTITHREAD
            List<Thread> threadList = new List<Thread>();
            Thread dividePasswordFinder1 = new Thread(thread1Start);
            threadList.Add(dividePasswordFinder1);
            Thread dividePasswordFinder2 = new Thread(thread2Start);
            threadList.Add(dividePasswordFinder2);

            //lancer les threads
            foreach (Thread th in threadList)
            {
                th.Start();
            }

            //attendre la fin des threads
            foreach (Thread th in threadList)
            {
                th.Join();
            }

            //retourner le mot de passe trouvé
            return passwordRight;

        }

        /// <summary>
        /// Lancer le 1er thread
        /// </summary>
        private void thread1Start()
        {
            string find = "";
            find = findPasswordThread(0);

            //si le mdp est vide ce n'est pas le bon
            if (find != "")
            {
                //stocker le mot de passe si il est trouvé
                passwordRight = find;
            }
        }

        /// <summary>
        /// Lancer le 2eme thread
        /// </summary>
        private void thread2Start()
        {
            string find = "";
            find = findPasswordThread(1);

            //si le mdp est vide ce n'est pas le bon
            if (find != "")
            {
                //stocker le mot de passe si il est trouvé
                passwordRight = find;
            }
        }


        /// <summary>
        /// Trouve le mot de passe
        /// </summary>
        /// <param name="iStart">numero du thread</param>
        /// <returns>le mot de passe ou rien</returns>
        public string findPasswordThread(int iStart)
        {
            //variable mot de passe
            String passwordThread = "";
            //variable de requete
            HttpWebResponse reponse;
            WebRequest request;
            //string de l'url pour la requete
            string _url;
            //reponse de la requete
            StreamReader sr;

            //diviser le dictionaire et tester si le mot de passe est corrrect
            for (int i = nbLine / NB_THREAD * iStart; i < nbLine / NB_THREAD + nbLine / NB_THREAD * iStart; i++)
            {
                //construction de l'url avec le mot de passe dedans
                _url = constructUrl(url, getUsername, "test", getPassword, allPassword[i]);
                //lancer la requete
                request = WebRequest.Create(_url);
                reponse = (HttpWebResponse)request.GetResponse();
                //stocker le résultat
                sr = new StreamReader(reponse.GetResponseStream());
                finalRequest = sr.ReadToEnd();

                //comparer la page si elle est similaire à celle d'erreur
                if (finalRequest != badRequest)
                {
                    //retourner le mot de passe trouvé
                    return allPassword[i];
                }
            }
            //retourner si le mot de passse n'est pas trouvé
            return passwordThread;

        }*/


        /// <summary>
        /// Construire l'url pour la requete
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_get"></param>
        /// <param name="_getText"></param>
        /// <param name="_get2"></param>
        /// <param name="_get2Text"></param>
        /// <returns></returns>
        private string constructUrl(string _url, string _get, string _getText, string _get2, string _get2Text)
        {
            string toReturn;
            if (_url.Contains("?"))
            {
                toReturn = _url + "&" + _get + "=" + _getText + "&" + _get2 + "=" + _get2Text;
            }
            else
            {
                toReturn = _url + "?" + _get + "=" + _getText + "&" + _get2 + "=" + _get2Text;
            }

            return toReturn;
        }
    }
}

