using HireMockup.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HireMockup
{
    /// <summary>
    /// Interaction logic for CreateHireAsset.xaml
    /// </summary>
    public partial class CreateHireAsset : Window
    {
        public CreateHireAsset()
        {
            InitializeComponent();
        }

        private void bttn_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void bttn_submitHireAsset_Click(object sender, RoutedEventArgs e)
        {
            string hireName = tbx_assetName.Text.ToString();
            string hireType = tbx_assetNumber.Text.ToString();
            decimal hireDailyRate = decimal.Parse(tbx_dailyRate.Text);
            DataAccessLayer.newHireAsset(hireName, hireType, hireDailyRate);
            MessageBox.Show("Asset window closing");
            DialogResult = false;
        }
    }
}
