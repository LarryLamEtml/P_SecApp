using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BruteForce
{
    public partial class BruteForceView : MaterialForm
    {
        public BruteForceView()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.DeepPurple400, TextShade.WHITE);
            
        }

        private void BruteForceView_Load(object sender, EventArgs e)
        {

        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {

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
        private void txMaxChar_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Input chiffres seulement
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
    }
}
