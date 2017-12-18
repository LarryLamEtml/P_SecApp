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
    /// <summary>
    /// Traitement des requetes HTTP 
    /// </summary>
    class BruteForceHTTP
    {
        //propriétés des thread 
        private const int NB_THREAD = 2;
        private const int POST_METHOD = 1;
        private const int GET_METHOD = 2;

        //Variable url
        private string getUsername = "";
        private string getPassword = "";
        
        //Url
        private string url;

        //Nom d'utilisateur
        private string username;

        //Fichier
        private FileStream fl;
        
        //Nombre de ligne du password
        private int nbPasswordLine;

        //Code html de la page à comparer. (avec des entrées fausse)
        private string badRequest = "";
        private string badRequestPost = "";

        //Mot de passe trouvé
        private string passwordRight = "";

        //Variable pour http
        private HttpWebResponse reponseGET;

        //Code html de la page retourné après envoi du mdp
        private string finalRequest = "";

        //Dictionnaire
            //tableau des passwords
        private string[] allPassword;
            //chemin
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

        /// <summary>
        /// Constructeur
        /// </summary>
        public BruteForceHTTP(string _url, string _login, string _password, string _path, bool _majuscules, bool _minuscules, bool _numbers, bool _symbols, int _method, int _mode, int _maxChar, int _minChar, string _username)
        {
            url = _url;
            username = _username;
            dictionnaryPath = _path;
            majuscules = _majuscules;
            minuscules = _minuscules;
            numbers = _numbers;
            symbols = _symbols;
            maxChar = _maxChar;
            minChar = _minChar;
            getUsername = _login;
            getPassword = _password;
            //Si la methode est à 1-> post sinon -> get
            method = (_method == 1 ? "POST" : "GET");
            mode = _mode;
            methodNum = _method;

            //informer l'utilisateur que le test des mots de passe va commencer
            Thread msgThread = new Thread(showStartMessage);
            msgThread.Start();

            //Définit la page par défaut à comparer.
            setDefaultRequest();
            //Methode dictionnaire ou génération de mdp

            if (mode == 1)
            {
                //Lance la methode
                DictionnaryAttack();
            }
            else
            {
                //Lance la methode
                generatePasswords();
            }

        }

        /// <summary>
        /// Averti l'utilisateur
        /// </summary>
        private void showStartMessage()
        {
            MessageBox.Show("Début du test des mot de passe .......\n\nVeuillez attendre\n\nVous pouvez fermer cette MessageBox");
        }

        /// <summary>
        /// Génère un mot de passe aléatoire en fonction des paramètres choisi et le teste
        /// </summary>
        private void generatePasswords()
        {
            //Création de la classe avec les paramètres
            RandomPassword password = new RandomPassword(minChar, maxChar, minuscules, majuscules, numbers, symbols);
            do
            {
                //Génère le mot de passe
                string psw = password.GeneratePassword();

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

        private void setDefaultRequest()
        {
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
        }

        /// <summary>
        /// Lit le dictionnaire et stocke ses infos
        /// </summary>
        private void DictionnaryAttack()
        {
            try
            {
                //ouvertur du dictionnaire
                fl = File.OpenRead(dictionnaryPath);
                //stocker dans une string les mots de passes
                allPassword = File.ReadAllLines(dictionnaryPath);
                //nombre de mot de passe
                nbPasswordLine = allPassword.Count();
            }
            catch
            {
                MessageBox.Show("Pas de dictionnaire ici..");
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
        /// teste les mots de passe
        /// </summary>
        /// <param name="iStart">numero du thread</param>
        /// <returns>le mot de passe ou rien</returns>
        public string sendPassword(int iStart)
        {
            string notFinded = "";
            
            //diviser le dictionaire et tester si le mot de passe est corrrect
            for (int i = nbPasswordLine / NB_THREAD * iStart; i < nbPasswordLine / NB_THREAD + nbPasswordLine / NB_THREAD * iStart; i++)
            {
                //La methode POTS
                if (methodNum == POST_METHOD)
                {
                    //Test le mot de passe
                    if (findPasswordThreadPOST(allPassword[i]))
                    {
                        //Retourne le mot de passe trouvé si c'est juste
                        return allPassword[i];
                    }
                }//Sinon si la methode GET
                else if (methodNum == GET_METHOD)
                {
                    //Test le mot de passe
                    if (findPasswordThreadGET(allPassword[i]))
                    {
                        //Retourne le mot de passe trouvé si c'est juste
                        return allPassword[i];
                    }
                }
            }
            //retourner si le mot de passse n'est pas trouvé
            return notFinded;

        }

        /// <summary>
        /// Teste le mot de passe (GET) et return true si trouvé 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
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
                //retourner true si le mot de passe est trouvé
                return true;
            }
            //Retourner false car mdp non trouvé
            return false;
        }



        /// <summary>
        /// Teste le mot de passe (POTS) et return true si trouvé 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool findPasswordThreadPOST(string password)
        {
            using (WebClient client = new WebClient())
            {
                //Récupère la page après avoir envoyé les data
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
                //Retourner false car mdp non trouvé
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

