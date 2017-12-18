/* ETML
 * 
 * Auteur : Larry Lam, Justin Vuffray
 * Date : 06.11.2017
 * Description : Génération de password en fonction des paramètres donnés
 * 
 * 
 * */
using System;
using System.Collections.Generic;

namespace BruteForce
{
    /// <summary>
    /// Génération de password en fonction des paramètres donnés
    /// </summary>
    class RandomPassword
    {
        //Max et min char par default
        private static int DEFAULT_MIN_PASSWORD_LENGTH = 0;
        private static int DEFAULT_MAX_PASSWORD_LENGTH = 100;

        //strint contenant les char
        private static string PASSWORD_CHARS_LCASE;
        private static string PASSWORD_CHARS_UCASE;
        private static string PASSWORD_CHARS_NUMERIC;
        private static string PASSWORD_CHARS_SPECIAL;
        private static List<char> PASSWORD_ALL = new List<char>();

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="minChar"></param>
        /// <param name="maxChar"></param>
        /// <param name="lowerCase"></param>
        /// <param name="upperCase"></param>
        /// <param name="numeric"></param>
        /// <param name="symbols"></param>
        public RandomPassword(int minChar, int maxChar, bool lowerCase, bool upperCase, bool numeric, bool symbols)
        {
            DEFAULT_MIN_PASSWORD_LENGTH = minChar;
            DEFAULT_MAX_PASSWORD_LENGTH = maxChar;
            PASSWORD_CHARS_LCASE = (lowerCase ? "abcdefgijkmnopqrstwxyz" : "");
            PASSWORD_CHARS_UCASE = (upperCase ? "ABCDEFGHJKLMNPQRSTWXYZ" : "");
            PASSWORD_CHARS_NUMERIC = (numeric ? "123456789" : "");
            PASSWORD_CHARS_SPECIAL = (symbols ? "*$-+?_&=!%{}/ " : "");
            setList();
        }

        /// <summary>
        /// Ajoute à la liste tout les caractères choisi avec les options
        /// </summary>
        private void setList()
        {
            foreach (char c in PASSWORD_CHARS_LCASE.ToCharArray())
            {
                PASSWORD_ALL.Add(c);
            }
            foreach (char c in PASSWORD_CHARS_UCASE.ToCharArray())
            {
                PASSWORD_ALL.Add(c);
            }
            foreach (char c in PASSWORD_CHARS_NUMERIC.ToCharArray())
            {
                PASSWORD_ALL.Add(c);
            }
            foreach (char c in PASSWORD_CHARS_SPECIAL.ToCharArray())
            {
                PASSWORD_ALL.Add(c);
            }
        }

        /// <summary>
        /// Génère un mot de passe aléatoire
        /// </summary>
        /// <returns></returns>
        public string GeneratePassword()
        {
            // Make sure that input parameters are valid.
            if (DEFAULT_MIN_PASSWORD_LENGTH <= 0 || DEFAULT_MAX_PASSWORD_LENGTH <= 0 || DEFAULT_MIN_PASSWORD_LENGTH > DEFAULT_MAX_PASSWORD_LENGTH)
                return null;
            //Password
            string psw = "";
            //Random
            Random r = new Random();
            //Définit la taille du password
            int passwordSize = r.Next(DEFAULT_MIN_PASSWORD_LENGTH, DEFAULT_MAX_PASSWORD_LENGTH + 1);

            //Parcours chaque lettre du password
            for (int i = 0; i < passwordSize; i++)
            {
                //ajoute un charactère aléatoire du tableau au mot de passe final
                psw += PASSWORD_ALL[r.Next(0, PASSWORD_ALL.Count)];
            }
            return psw;
        }

    }
}