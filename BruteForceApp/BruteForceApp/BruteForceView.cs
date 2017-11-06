using System;

namespace BruteForceApp
{
    public partial class BruteForceView : MaterialForm
    {
        public BruteForceView()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void BruteForceView_Load(object sender, EventArgs e)
        {

        }
    }
}
