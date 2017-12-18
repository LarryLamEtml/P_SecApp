/* ETML
 * 
 * Auteur : Larry Lam, Justin Vuffray
 * Date : 06.11.2017
 * Description : Traitement des requetes HTTP 
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
        //propriétés des thread 
        private const int NB_THREAD = 2;
        private const int POST_METHOD = 1;
        private const int GET_METHOD = 2;

        //
        public string getUsername = "";
        public string getPassword = "";

        //
        private string url;
        private string login;
        private string password;
        private string username;

        private FileStream fl;
        private int nbLine;

        private string badRequest = "";
        private string badRequestPost = "";
        public string passwordRight = "";
        private HttpWebResponse reponseGET;
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


        WebClient wb = new WebClient();
        NameValueCollection data = new NameValueCollection();

        public BruteForceHTTP(string _url, string _login, string _password, string _path, bool _majuscules, bool _minuscules, bool _numbers, bool _symbols, int _method, int _mode, int _maxChar, int _minChar, string _username, string _getUsername, string _getPassword)
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
            username = _username;
            getUsername = _getUsername;
            getPassword = _getPassword;
            method = (_method == 1 ? "POST" : "GET");
            mode = _mode;
            methodNum = _method;

            //informaer lutilisateur que le test des mots de passe va commencer
            Thread msgThread = new Thread(showStartMessage);
            msgThread.Start();

            if (mode == 1)
            {
                ReadDictionnary();
            }
            else
            {
                generatePasswords();
            }

        }

        private void showStartMessage()
        {
            MessageBox.Show("Début du test des mot de passe .......\n\nVeuillez attendre\n\nVous pouvez fermer cette MessageBox");
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
                    findPasswordThreadPOST(psw);
                }
                else//GET
                {
                    findPasswordThreadGET(psw);
                }
            } while (true);
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
            //LANCEMENT DES REQUETES EN MULTITHREAD
            List<Thread> threadList = new List<Thread>();
            Thread dividePasswordFinder1 = new Thread(threadStart);
            threadList.Add(dividePasswordFinder1);
            Thread dividePasswordFinder2 = new Thread(threadStart);
            threadList.Add(dividePasswordFinder2);
            int index = 0;
            //lancer les threads
            foreach (Thread th in threadList)
            {
                th.Start(index);
                index++;
            }

            //attendre la fin des threads
            foreach (Thread th in threadList)
            {
                th.Join();
            }

            MessageBox.Show(passwordRight);
        }

        /// <summary>
        /// Lancer le 1er thread
        /// </summary>
        private void threadStart(object obj)
        {
            int index = (int)obj;
            string find = "";
            find = sendPassword(index);

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
        public string sendPassword(int iStart)
        {
            string notFinded = "";

            //diviser le dictionaire et tester si le mot de passe est corrrect
            for (int i = nbLine / NB_THREAD * iStart; i < nbLine / NB_THREAD + nbLine / NB_THREAD * iStart; i++)
            {
                if (methodNum == POST_METHOD)
                {
                    if (findPasswordThreadPOST(allPassword[i]))
                    {
                        return allPassword[i];
                    }
                }
                else if (methodNum == GET_METHOD)
                {
                    if (findPasswordThreadGET(allPassword[i]))
                    {
                        return allPassword[i];
                    }
                }
            }
            //retourner si le mot de passse n'est pas trouvé
            return notFinded;

        }

        private bool findPasswordThreadGET(string password)
        {
            WebRequest request;
            //string de l'url pour la requete
            string _url;
            //reponse de la requete
            StreamReader sr;

            //construction de l'url avec le mot de passe dedans
            _url = constructUrl(url, getUsername, username, getPassword, password);
            //lancer la requete
            request = WebRequest.Create(_url);
            reponseGET = (HttpWebResponse)request.GetResponse();
            //stocker le résultat
            sr = new StreamReader(reponseGET.GetResponseStream());
            finalRequest = sr.ReadToEnd();

            //comparer la page si elle est similaire à celle d'erreur
            if (finalRequest != badRequest)
            {
                return true;
                //retourner true si le mot de passe est trouvé
            }
            return false;
        }



        public bool findPasswordThreadPOST(string password)
        {
            using (WebClient client = new WebClient())
            {
                byte[] response =
                    client.UploadValues(url, new NameValueCollection()
                    {
                                { getUsername, username },
                                { getPassword, password }
                    });

                string result = System.Text.Encoding.UTF8.GetString(response);

                //comparer la page si elle est similaire à celle d'erreur
                if (result != badRequestPost)
                {
                    //retourner true si le mot de passe est trouvé
                    return true;
                }
                return false;
            }
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

