﻿using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BruteForce
{
    public partial class BruteForceView : MaterialForm
    {
        //Dictionnaire
        private string[] dictionnaryData;

        //Checkboxes
        private bool majuscules = false;
        private bool minuscules = false;
        private bool numbers = false;
        private bool symbols = false;

        //Methode
        private int method = 1;//1 = post, 2 = get

        //Max & Min char
        private int maxChar = 100;
        private int minChar = 1;

        private string url;

        private const int ERROR_MINCHAR = 0;
        private const int ERROR_MAXCHAR = 1;
        private const int ERROR_DICTIONNARY = 2;
        private const int ERROR_URL = 3;
        private const int VALID_FORM = 4;

        public BruteForceView()
        {
            InitializeComponent();

            //Couleurs et thèmes
            setColors();
            //Récupère le dictionnaire par defaut
            dictionnaryData = System.IO.File.ReadAllLines("dictionnary.txt");
            //Definit la methode post par defaut (radio)           
            radioPost.Checked = true;
        }
        /// <summary>
        /// 
        /// </summary>
        private void setColors()
        {
            //Défini les couleurs et thèmes
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.DeepPurple400, TextShade.WHITE);

        }

        private void txbMinChar_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Input chiffres seulement
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            /* Size size = TextRenderer.MeasureText(txbMinChar.Text, txbMinChar.Font);
             txbMinChar.Width = size.Width;
             txbMinChar.Height = size.Height;*/
        }

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
                //Stocke les données du fichier dans une variable
                dictionnaryData = System.IO.File.ReadAllLines(fileDialogDictionary.FileName);
                //Affiche le chemin du fichier choisi
                txbDicoFileName.Text = fileDialogDictionary.FileName;
            }
            catch
            {
                //Averti l'utilisateur
                MessageBox.Show("Fichier illisible");

            }
        }

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
                case VALID_FORM://Commence le bruteforce
                    startBruteForce();
                    break;
                default:
                    break;

            }
        }

        private void startBruteForce()
        {

        }

        private void txbMinChar_TextChanged(object sender, EventArgs e)
        {
            //Définit le nombre de caractères min
            minChar = Convert.ToInt32(txbMinChar.Text);
        }

        private void txbMaxChar_TextChanged(object sender, EventArgs e)
        {
            //Définit le nombre de caractères max
            maxChar = Convert.ToInt32(txbMaxChar.Text);
        }

        private void cbMinuscule_CheckedChanged(object sender, EventArgs e)
        {
            //Définit si les minuscules est choisi
            minuscules = cbMinuscule.Checked;
        }

        private void cbNumbers_CheckedChanged(object sender, EventArgs e)
        {
            //Définit si les chiffres sont choisis
            numbers = cbNumbers.Checked;
        }

        private void cbMajuscule_CheckedChanged(object sender, EventArgs e)
        {
            //Définit si les majuscules sont choisies
            majuscules = cbMajuscule.Checked;
        }

        private void cbSymbols_CheckedChanged(object sender, EventArgs e)
        {
            //Définit si les symboles sont choisis
            symbols = cbSymbols.Checked;
        }

        private void txbUrl_TextChanged(object sender, EventArgs e)
        {
            //Définit l'url
            url = txbUrl.Text;
        }

        private void radioPost_CheckedChanged(object sender, EventArgs e)
        {
            //Définit la methode
            if (radioPost.Checked)
            {
                method = 1;
            }
            else
            {
                method = 2;
            }
        }

        private void radioGet_CheckedChanged(object sender, EventArgs e)
        {
            //Définit la methode
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
            string pattern = @"^(http://|ftp://|https://)?(.+)+\.[^.]+$";
            Regex reg = new Regex(pattern);

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

            //Valide le dictionnaire
            if (dictionnaryData == null)
            {
                return ERROR_DICTIONNARY;
            }

            //Valide l'url
            if (url == null)
            {
                return ERROR_URL;
            }
            else if (!reg.IsMatch(url))//Check la syntaxe
            {
                return ERROR_URL;
            }
            //Si tout est valide
            return VALID_FORM;
        }
    }
}
