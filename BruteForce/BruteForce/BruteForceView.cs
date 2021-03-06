﻿/* ETML
 * 
 * Auteur : Larry Lam, Justin Vuffray
 * Date : 06.11.2017
 * Description : Génération de passwords en fonction des paramètres donnés
 * 
 * 
 * */
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BruteForce
{
    /// <summary>
    /// Génération de passwords en fonction des paramètres donnés
    /// </summary>
    public partial class BruteForceView : MaterialForm
    {
        //Dictionnaire
        private string dictionnaryPath = "dictionnary.txt";

        //Checkboxes
        private bool majuscules = false;
        private bool minuscules = false;
        private bool numbers = false;
        private bool symbols = false;

        //Methode
        private int method = 1;//1 = post, 2 = get
        private int mode = 1;//1 = dictionnary, 2 = generated

        //Max & Min char
        private int maxChar = 100;
        private int minChar = 1;

        //variable du website
        private string url;
        private string login;
        private string password;
        private string username;

        //Index pour les types d'erreurs
        private const int ERROR_MINCHAR = 0;
        private const int ERROR_MAXCHAR = 1;
        private const int ERROR_DICTIONNARY = 2;
        private const int ERROR_URL = 3;
        private const int ERROR_LOGIN = 4;
        private const int ERROR_PASSWORD = 5;
        private const int ERROR_USERNAME = 6;
        private const int VALID_FORM = 10;

        /// <summary>
        /// Constructeur
        /// </summary>
        public BruteForceView()
        {
            InitializeComponent();

            //Couleurs et thèmes
            setColors();

            //Récupère le dictionnaire par defaut
            //Definit la methode post par defaut (radio)
            radioPost.Checked = true;
            this.AcceptButton = btnStart;
            //Cache les options
            hideOptions();
        }

        /// <summary>
        /// Cache les options dans le cas ou le dictionnaire est choisi comme option
        /// </summary>
        public void hideOptions()
        {
            cbMajuscule.Hide();
            cbMinuscule.Hide();
            cbNumbers.Hide();
            cbSymbols.Hide();
            txbChar.Hide();
            txbMax.Hide();
            txbMaxChar.Hide();
            txbMin.Hide();
            txbMinChar.Hide();
        }

        /// <summary>
        /// affiche les options dans le cas ou la génération manuelle est choisi comme option
        /// </summary>
        public void showOptions()
        {
            cbMajuscule.Show();
            cbMinuscule.Show();
            cbNumbers.Show();
            cbSymbols.Show();
            txbChar.Show();
            txbMax.Show();
            txbMaxChar.Show();
            txbMin.Show();
            txbMinChar.Show();
        }

        /// <summary>
        /// Définit les couleurs de la form
        /// </summary>
        private void setColors()
        {
            //Défini les couleurs et thèmes
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.DeepPurple400, TextShade.WHITE);
        }

        /// <summary>
        /// Restreint l'input à que des chiffres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbMinChar_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Input chiffres seulement
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            /*
             Taille responsive
             Size size = TextRenderer.MeasureText(txbMinChar.Text, txbMinChar.Font);
             txbMinChar.Width = size.Width;
             txbMinChar.Height = size.Height;*/
        }

        /// <summary>
        /// Restreint l'input à que des chiffres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txMaxChar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Input chiffres seulement
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Ouvre le fileDialog pour importer le dictionnaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            //Filtre des types de fichier
            fileDialogDictionary.Filter = "Text file|*.txt";
            //Ouvre le fileDialog
            fileDialogDictionary.ShowDialog();
        }

        /// <summary>
        /// Lorsque le fichier est choisi, récupérer ses données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileDialogDictionary_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //Stocke le chemin du fichier dans une variable
                dictionnaryPath = fileDialogDictionary.FileName;
                //Affiche le chemin du fichier choisi
                txbDicoFileName.Text = fileDialogDictionary.FileName;
            }
            catch
            {
                //Averti l'utilisateur
                MessageBox.Show("Fichier illisible");

            }
        }

        /// <summary>
        /// Check si tout est bien rempli puis lance le bruteforce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Vérifie que tout les champs sont valide et affiche un message d'erreur pour les champs spécifiques
            switch (validateForm())
            {
                case ERROR_MINCHAR:
                    MessageBox.Show("Le nombre minimum de caractère doit être supérieur à 0");
                    break;
                case ERROR_MAXCHAR:
                    MessageBox.Show("Le nombre maximum de caractère doit être supérieur à " + minChar);
                    break;
                case ERROR_DICTIONNARY:
                    MessageBox.Show("Le dictionnaire de données n'est pas valide");
                    break;
                case ERROR_URL:
                    MessageBox.Show("L'url n'est pas valide veuillez respecter la syntaxe [http(s)://]site.com");
                    break;
                case ERROR_LOGIN:
                    MessageBox.Show("Veuillez remplir la variable login de l'url");
                    break;
                case ERROR_PASSWORD:
                    MessageBox.Show("Veuillez remplir la variable password de l'url");
                    break;
                case ERROR_USERNAME:
                    MessageBox.Show("Veuillez remplir le nom d'utilisateur");
                    break;
                case VALID_FORM:
                    //Commence le bruteforce
                    Thread thread = new Thread(startBruteForce);
                    thread.Start();
                    break;
                default:
                    break;

            }
        }

        /// <summary>
        /// Crée la classe qui va gérer le bruteforce
        /// </summary>
        private void startBruteForce()
        {
            
            BruteForceHTTP bruteF = new BruteForceHTTP(url, login, password, dictionnaryPath, majuscules, minuscules, numbers, symbols, method, mode, maxChar, minChar,username);
        }

        /// <summary>
        ///set le nombre de caractères min
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbMinChar_TextChanged(object sender, EventArgs e)
        {
            //set le nombre de caractères min
            minChar = Convert.ToInt32(txbMinChar.Text);
        }

        /// <summary>
        ///Définit le nombre de caractères max
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbMaxChar_TextChanged(object sender, EventArgs e)
        {
            //Définit le nombre de caractères max
            maxChar = Convert.ToInt32(txbMaxChar.Text);
        }

        /// <summary>
        ///Définit si les minuscules est choisi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMinuscule_CheckedChanged(object sender, EventArgs e)
        {
            //Définit si les minuscules est choisi
            minuscules = cbMinuscule.Checked;
        }

        /// <summary>
        ///Définit si les chiffres sont choisis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNumbers_CheckedChanged(object sender, EventArgs e)
        {
            //Définit si les chiffres sont choisis
            numbers = cbNumbers.Checked;
        }

        /// <summary>
        /// Définit si les majuscules sont choisies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMajuscule_CheckedChanged(object sender, EventArgs e)
        {
            //Définit si les majuscules sont choisies
            majuscules = cbMajuscule.Checked;
        }
        /// <summary>
        /// Définit si les symboles sont choisis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSymbols_CheckedChanged(object sender, EventArgs e)
        {
            //Définit si les symboles sont choisis
            symbols = cbSymbols.Checked;
        }

        /// <summary>
        /// Définit l'url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbUrl_TextChanged(object sender, EventArgs e)
        {
            //Définit l'url
            url = txbUrl.Text;
        }

        /// <summary>
        ///Définit la methode (POST ou GET)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioPost_CheckedChanged(object sender, EventArgs e)
        {
            //Définit la methode (POST ou GET) en fonction du radio button selectionné
            if (radioPost.Checked)
            {
                method = 1;
            }
            else
            {
                method = 2;
            }
        }

        /// <summary>
        ///Définit la methode (POST ou GET)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGet_CheckedChanged(object sender, EventArgs e)
        {
            //Définit la methode (POST ou GET) en fonction du radio button selectionné
            if (radioGet.Checked)
            {
                method = 2;
            }
            else
            {
                method = 1;
            }
        }

        /// <summary>
        /// Valide tout les paramètres
        /// </summary>
        /// <returns></returns>
        private int validateForm()
        {
            //Regex pour url
            string pattern = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
            Regex reg = new Regex(pattern);
            
            //Valide l'url
            if (url == null)
            {
                return ERROR_URL;
            }
            else if (!reg.IsMatch(url))//Check la syntaxe
            {
                return ERROR_URL;
            }
            if (login == null)
            {
                return ERROR_LOGIN;
            }
            if (password == null)
            {
                return ERROR_PASSWORD;
            }
            if (username == null)
            {
                return ERROR_USERNAME;
            }

            if(method == 1)
            {
                //Valide le dictionnaire
                if (dictionnaryPath == null)
                {
                    return ERROR_DICTIONNARY;
                }
            }
            else
            {
                //Valide l'input minChar
                if (minChar <= 0)
                {
                    return ERROR_MINCHAR;
                }

                //Valide l'input maxChar
                if (minChar > maxChar)
                {
                    return ERROR_MAXCHAR;

                }
            }
            //Si tout est valide
            return VALID_FORM;
        }

        /// <summary>
        /// Set la variable login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbLogin_TextChanged(object sender, EventArgs e)
        {
            login = txbLogin.Text;
        }

        /// <summary>
        /// Set la variable password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbPassword_TextChanged(object sender, EventArgs e)
        {
            password = txbPassword.Text;

        }

        /// <summary>
        /// Set le username
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbUsername_TextChanged(object sender, EventArgs e)
        {
            username = txbUsername.Text;
        }

        /// <summary>
        /// Change les propriétés disponibles en fonction du mode dictionnaire ou génération
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOptions_CheckedChanged(object sender, EventArgs e)
        {
            //Si la génération est selectionnée
            if(cbOptions.Checked)
            {
                //Affiche et cache les propriétés en liens
                btnImport.Enabled = false;
                txbDicoFileName.Enabled = false;
                showOptions();
                mode = 2;
            }
            //sinon
            else
            {
                //Affiche et cache les propriétés en liens
                btnImport.Enabled = true;
                txbDicoFileName.Enabled = true;
                hideOptions();
                mode = 1;
            }
        }
    }
}
