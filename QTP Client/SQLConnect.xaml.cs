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

namespace QTP_Client
{
    /// <summary>
    /// Логика взаимодействия для SQLConnect.xaml
    /// </summary>
    public partial class SQLConnect : Window
    {
        public SQLConnect()
        {
            InitializeComponent();
            tb_sqlPath.Text = Properties.Settings.Default.pathSQL;
        }

        private void b_go_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.pathSQL = tb_sqlPath.Text;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
