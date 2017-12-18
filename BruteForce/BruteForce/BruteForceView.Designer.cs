/* ETML
 * 
 * Author : Larry Lam, Justin Vuffray
 * Date : 06.11.2017
 * Description : Traitement de la vue.
 * 
 * 
 * */
namespace BruteForce
{
    partial class BruteForceView
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnImport = new MaterialSkin.Controls.MaterialFlatButton();
            this.txbDicoFileName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.cbMinuscule = new MaterialSkin.Controls.MaterialCheckBox();
            this.cbMajuscule = new MaterialSkin.Controls.MaterialCheckBox();
            this.cbNumbers = new MaterialSkin.Controls.MaterialCheckBox();
            this.cbSymbols = new MaterialSkin.Controls.MaterialCheckBox();
            this.txbMaxChar = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txbMin = new MaterialSkin.Controls.MaterialLabel();
            this.txbMax = new MaterialSkin.Controls.MaterialLabel();
            this.btnStart = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txbChar = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.txbUrl = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txbMinChar = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.radioPost = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioGet = new MaterialSkin.Controls.MaterialRadioButton();
            this.fileDialogDictionary = new System.Windows.Forms.OpenFileDialog();
            this.txbLogin = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txbPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txbUsername = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.cbOptions = new MaterialSkin.Controls.MaterialCheckBox();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.AutoSize = true;
            this.btnImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnImport.Depth = 0;
            this.btnImport.Location = new System.Drawing.Point(218, 280);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnImport.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnImport.Name = "btnImport";
            this.btnImport.Primary = false;
            this.btnImport.Size = new System.Drawing.Size(79, 36);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "Importer";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txbDicoFileName
            // 
            this.txbDicoFileName.Depth = 0;
            this.txbDicoFileName.Enabled = false;
            this.txbDicoFileName.Hint = "Default";
            this.txbDicoFileName.Location = new System.Drawing.Point(34, 285);
            this.txbDicoFileName.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbDicoFileName.Name = "txbDicoFileName";
            this.txbDicoFileName.PasswordChar = '\0';
            this.txbDicoFileName.SelectedText = "";
            this.txbDicoFileName.SelectionLength = 0;
            this.txbDicoFileName.SelectionStart = 0;
            this.txbDicoFileName.Size = new System.Drawing.Size(177, 23);
            this.txbDicoFileName.TabIndex = 4;
            this.txbDicoFileName.TabStop = false;
            this.txbDicoFileName.Text = "Default";
            this.txbDicoFileName.UseSystemPasswordChar = false;
            // 
            // cbMinuscule
            // 
            this.cbMinuscule.AutoSize = true;
            this.cbMinuscule.Depth = 0;
            this.cbMinuscule.Font = new System.Drawing.Font("Roboto", 10F);
            this.cbMinuscule.Location = new System.Drawing.Point(58, 369);
            this.cbMinuscule.Margin = new System.Windows.Forms.Padding(0);
            this.cbMinuscule.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbMinuscule.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbMinuscule.Name = "cbMinuscule";
            this.cbMinuscule.Ripple = true;
            this.cbMinuscule.Size = new System.Drawing.Size(101, 30);
            this.cbMinuscule.TabIndex = 6;
            this.cbMinuscule.Text = "Minuscules";
            this.cbMinuscule.UseVisualStyleBackColor = true;
            this.cbMinuscule.CheckedChanged += new System.EventHandler(this.cbMinuscule_CheckedChanged);
            // 
            // cbMajuscule
            // 
            this.cbMajuscule.AutoSize = true;
            this.cbMajuscule.Depth = 0;
            this.cbMajuscule.Font = new System.Drawing.Font("Roboto", 10F);
            this.cbMajuscule.Location = new System.Drawing.Point(58, 399);
            this.cbMajuscule.Margin = new System.Windows.Forms.Padding(0);
            this.cbMajuscule.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbMajuscule.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbMajuscule.Name = "cbMajuscule";
            this.cbMajuscule.Ripple = true;
            this.cbMajuscule.Size = new System.Drawing.Size(100, 30);
            this.cbMajuscule.TabIndex = 7;
            this.cbMajuscule.Text = "Majuscules";
            this.cbMajuscule.UseVisualStyleBackColor = true;
            this.cbMajuscule.CheckedChanged += new System.EventHandler(this.cbMajuscule_CheckedChanged);
            // 
            // cbNumbers
            // 
            this.cbNumbers.AutoSize = true;
            this.cbNumbers.Depth = 0;
            this.cbNumbers.Font = new System.Drawing.Font("Roboto", 10F);
            this.cbNumbers.Location = new System.Drawing.Point(215, 369);
            this.cbNumbers.Margin = new System.Windows.Forms.Padding(0);
            this.cbNumbers.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbNumbers.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbNumbers.Name = "cbNumbers";
            this.cbNumbers.Ripple = true;
            this.cbNumbers.Size = new System.Drawing.Size(79, 30);
            this.cbNumbers.TabIndex = 8;
            this.cbNumbers.Text = "Chiffres";
            this.cbNumbers.UseVisualStyleBackColor = true;
            this.cbNumbers.CheckedChanged += new System.EventHandler(this.cbNumbers_CheckedChanged);
            // 
            // cbSymbols
            // 
            this.cbSymbols.AutoSize = true;
            this.cbSymbols.Depth = 0;
            this.cbSymbols.Font = new System.Drawing.Font("Roboto", 10F);
            this.cbSymbols.Location = new System.Drawing.Point(215, 399);
            this.cbSymbols.Margin = new System.Windows.Forms.Padding(0);
            this.cbSymbols.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbSymbols.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbSymbols.Name = "cbSymbols";
            this.cbSymbols.Ripple = true;
            this.cbSymbols.Size = new System.Drawing.Size(90, 30);
            this.cbSymbols.TabIndex = 9;
            this.cbSymbols.Text = "Symboles";
            this.cbSymbols.UseVisualStyleBackColor = true;
            this.cbSymbols.CheckedChanged += new System.EventHandler(this.cbSymbols_CheckedChanged);
            // 
            // txbMaxChar
            // 
            this.txbMaxChar.Depth = 0;
            this.txbMaxChar.Hint = "100";
            this.txbMaxChar.Location = new System.Drawing.Point(235, 479);
            this.txbMaxChar.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbMaxChar.Name = "txbMaxChar";
            this.txbMaxChar.PasswordChar = '\0';
            this.txbMaxChar.SelectedText = "";
            this.txbMaxChar.SelectionLength = 0;
            this.txbMaxChar.SelectionStart = 0;
            this.txbMaxChar.Size = new System.Drawing.Size(76, 23);
            this.txbMaxChar.TabIndex = 11;
            this.txbMaxChar.TabStop = false;
            this.txbMaxChar.UseSystemPasswordChar = false;
            this.txbMaxChar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txMaxChar_KeyPress);
            this.txbMaxChar.TextChanged += new System.EventHandler(this.txbMaxChar_TextChanged);
            // 
            // txbMin
            // 
            this.txbMin.AutoSize = true;
            this.txbMin.Depth = 0;
            this.txbMin.Font = new System.Drawing.Font("Roboto", 11F);
            this.txbMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txbMin.Location = new System.Drawing.Point(34, 478);
            this.txbMin.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbMin.Name = "txbMin";
            this.txbMin.Size = new System.Drawing.Size(34, 19);
            this.txbMin.TabIndex = 10;
            this.txbMin.Text = "Min";
            // 
            // txbMax
            // 
            this.txbMax.AutoSize = true;
            this.txbMax.Depth = 0;
            this.txbMax.Font = new System.Drawing.Font("Roboto", 11F);
            this.txbMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txbMax.Location = new System.Drawing.Point(186, 479);
            this.txbMax.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbMax.Name = "txbMax";
            this.txbMax.Size = new System.Drawing.Size(37, 19);
            this.txbMax.TabIndex = 11;
            this.txbMax.Text = "Max";
            // 
            // btnStart
            // 
            this.btnStart.Depth = 0;
            this.btnStart.Location = new System.Drawing.Point(262, 540);
            this.btnStart.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStart.Name = "btnStart";
            this.btnStart.Primary = true;
            this.btnStart.Size = new System.Drawing.Size(97, 41);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txbChar
            // 
            this.txbChar.AutoSize = true;
            this.txbChar.Depth = 0;
            this.txbChar.Font = new System.Drawing.Font("Roboto", 11F);
            this.txbChar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txbChar.Location = new System.Drawing.Point(30, 443);
            this.txbChar.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbChar.Name = "txbChar";
            this.txbChar.Size = new System.Drawing.Size(100, 19);
            this.txbChar.TabIndex = 14;
            this.txbChar.Text = "Charactère(s)";
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(30, 251);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(125, 19);
            this.materialLabel5.TabIndex = 15;
            this.materialLabel5.Text = "Dictionnaire (.txt)";
            // 
            // txbUrl
            // 
            this.txbUrl.Depth = 0;
            this.txbUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.txbUrl.Hint = "URL";
            this.txbUrl.Location = new System.Drawing.Point(34, 79);
            this.txbUrl.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbUrl.Name = "txbUrl";
            this.txbUrl.PasswordChar = '\0';
            this.txbUrl.SelectedText = "";
            this.txbUrl.SelectionLength = 0;
            this.txbUrl.SelectionStart = 0;
            this.txbUrl.Size = new System.Drawing.Size(301, 23);
            this.txbUrl.TabIndex = 1;
            this.txbUrl.TabStop = false;
            this.txbUrl.UseSystemPasswordChar = false;
            this.txbUrl.TextChanged += new System.EventHandler(this.txbUrl_TextChanged);
            // 
            // txbMinChar
            // 
            this.txbMinChar.Depth = 0;
            this.txbMinChar.Hint = "1";
            this.txbMinChar.Location = new System.Drawing.Point(83, 478);
            this.txbMinChar.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbMinChar.Name = "txbMinChar";
            this.txbMinChar.PasswordChar = '\0';
            this.txbMinChar.SelectedText = "";
            this.txbMinChar.SelectionLength = 0;
            this.txbMinChar.SelectionStart = 0;
            this.txbMinChar.Size = new System.Drawing.Size(76, 23);
            this.txbMinChar.TabIndex = 10;
            this.txbMinChar.TabStop = false;
            this.txbMinChar.UseSystemPasswordChar = false;
            this.txbMinChar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbMinChar_KeyPress);
            this.txbMinChar.TextChanged += new System.EventHandler(this.txbMinChar_TextChanged);
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.Location = new System.Drawing.Point(30, 213);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(68, 19);
            this.materialLabel6.TabIndex = 20;
            this.materialLabel6.Text = "Méthode";
            // 
            // radioPost
            // 
            this.radioPost.AutoSize = true;
            this.radioPost.Depth = 0;
            this.radioPost.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioPost.Location = new System.Drawing.Point(147, 208);
            this.radioPost.Margin = new System.Windows.Forms.Padding(0);
            this.radioPost.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioPost.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioPost.Name = "radioPost";
            this.radioPost.Ripple = true;
            this.radioPost.Size = new System.Drawing.Size(64, 30);
            this.radioPost.TabIndex = 12;
            this.radioPost.TabStop = true;
            this.radioPost.Text = "POST";
            this.radioPost.UseVisualStyleBackColor = true;
            this.radioPost.CheckedChanged += new System.EventHandler(this.radioPost_CheckedChanged);
            // 
            // radioGet
            // 
            this.radioGet.AutoSize = true;
            this.radioGet.Depth = 0;
            this.radioGet.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioGet.Location = new System.Drawing.Point(222, 208);
            this.radioGet.Margin = new System.Windows.Forms.Padding(0);
            this.radioGet.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioGet.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioGet.Name = "radioGet";
            this.radioGet.Ripple = true;
            this.radioGet.Size = new System.Drawing.Size(54, 30);
            this.radioGet.TabIndex = 13;
            this.radioGet.TabStop = true;
            this.radioGet.Text = "GET";
            this.radioGet.UseVisualStyleBackColor = true;
            this.radioGet.CheckedChanged += new System.EventHandler(this.radioGet_CheckedChanged);
            // 
            // fileDialogDictionary
            // 
            this.fileDialogDictionary.FileOk += new System.ComponentModel.CancelEventHandler(this.fileDialogDictionary_FileOk);
            // 
            // txbLogin
            // 
            this.txbLogin.Depth = 0;
            this.txbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.txbLogin.Hint = "Login variable";
            this.txbLogin.Location = new System.Drawing.Point(34, 119);
            this.txbLogin.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbLogin.Name = "txbLogin";
            this.txbLogin.PasswordChar = '\0';
            this.txbLogin.SelectedText = "";
            this.txbLogin.SelectionLength = 0;
            this.txbLogin.SelectionStart = 0;
            this.txbLogin.Size = new System.Drawing.Size(145, 23);
            this.txbLogin.TabIndex = 2;
            this.txbLogin.TabStop = false;
            this.txbLogin.UseSystemPasswordChar = false;
            this.txbLogin.TextChanged += new System.EventHandler(this.txbLogin_TextChanged);
            // 
            // txbPassword
            // 
            this.txbPassword.Depth = 0;
            this.txbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.txbPassword.Hint = "Password variable";
            this.txbPassword.Location = new System.Drawing.Point(190, 119);
            this.txbPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '\0';
            this.txbPassword.SelectedText = "";
            this.txbPassword.SelectionLength = 0;
            this.txbPassword.SelectionStart = 0;
            this.txbPassword.Size = new System.Drawing.Size(145, 23);
            this.txbPassword.TabIndex = 3;
            this.txbPassword.UseSystemPasswordChar = false;
            this.txbPassword.TextChanged += new System.EventHandler(this.txbPassword_TextChanged);
            // 
            // txbUsername
            // 
            this.txbUsername.Depth = 0;
            this.txbUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.txbUsername.Hint = "Nom d\'utilisateur";
            this.txbUsername.Location = new System.Drawing.Point(34, 159);
            this.txbUsername.MouseState = MaterialSkin.MouseState.HOVER;
            this.txbUsername.Name = "txbUsername";
            this.txbUsername.PasswordChar = '\0';
            this.txbUsername.SelectedText = "";
            this.txbUsername.SelectionLength = 0;
            this.txbUsername.SelectionStart = 0;
            this.txbUsername.Size = new System.Drawing.Size(145, 23);
            this.txbUsername.TabIndex = 21;
            this.txbUsername.TabStop = false;
            this.txbUsername.UseSystemPasswordChar = false;
            this.txbUsername.TextChanged += new System.EventHandler(this.txbUsername_TextChanged);
            // 
            // cbOptions
            // 
            this.cbOptions.AutoSize = true;
            this.cbOptions.Depth = 0;
            this.cbOptions.Font = new System.Drawing.Font("Roboto", 10F);
            this.cbOptions.Location = new System.Drawing.Point(34, 327);
            this.cbOptions.Margin = new System.Windows.Forms.Padding(0);
            this.cbOptions.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbOptions.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbOptions.Name = "cbOptions";
            this.cbOptions.Ripple = true;
            this.cbOptions.Size = new System.Drawing.Size(78, 30);
            this.cbOptions.TabIndex = 22;
            this.cbOptions.Text = "Options";
            this.cbOptions.UseVisualStyleBackColor = true;
            this.cbOptions.CheckedChanged += new System.EventHandler(this.cbOptions_CheckedChanged);
            // 
            // BruteForceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 593);
            this.Controls.Add(this.cbOptions);
            this.Controls.Add(this.txbUsername);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.txbLogin);
            this.Controls.Add(this.radioGet);
            this.Controls.Add(this.radioPost);
            this.Controls.Add(this.materialLabel6);
            this.Controls.Add(this.txbMinChar);
            this.Controls.Add(this.txbUrl);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.txbChar);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txbMax);
            this.Controls.Add(this.txbMin);
            this.Controls.Add(this.txbMaxChar);
            this.Controls.Add(this.cbSymbols);
            this.Controls.Add(this.cbNumbers);
            this.Controls.Add(this.cbMajuscule);
            this.Controls.Add(this.cbMinuscule);
            this.Controls.Add(this.txbDicoFileName);
            this.Controls.Add(this.btnImport);
            this.ForeColor = System.Drawing.Color.Red;
            this.MaximizeBox = false;
            this.Name = "BruteForceView";
            this.Sizable = false;
            this.Text = "  ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialFlatButton btnImport;
        private MaterialSkin.Controls.MaterialSingleLineTextField txbDicoFileName;
        private MaterialSkin.Controls.MaterialCheckBox cbMinuscule;
        private MaterialSkin.Controls.MaterialCheckBox cbMajuscule;
        private MaterialSkin.Controls.MaterialCheckBox cbNumbers;
        private MaterialSkin.Controls.MaterialCheckBox cbSymbols;
        private MaterialSkin.Controls.MaterialSingleLineTextField txbMaxChar;
        private MaterialSkin.Controls.MaterialLabel txbMin;
        private MaterialSkin.Controls.MaterialLabel txbMax;
        private MaterialSkin.Controls.MaterialRaisedButton btnStart;
        private MaterialSkin.Controls.MaterialLabel txbChar;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialSingleLineTextField txbUrl;
        private MaterialSkin.Controls.MaterialSingleLineTextField txbMinChar;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialRadioButton radioPost;
        private MaterialSkin.Controls.MaterialRadioButton radioGet;
        private System.Windows.Forms.OpenFileDialog fileDialogDictionary;
        private MaterialSkin.Controls.MaterialSingleLineTextField txbLogin;
        private MaterialSkin.Controls.MaterialSingleLineTextField txbPassword;
        private MaterialSkin.Controls.MaterialSingleLineTextField txbUsername;
        private MaterialSkin.Controls.MaterialCheckBox cbOptions;
    }
}

