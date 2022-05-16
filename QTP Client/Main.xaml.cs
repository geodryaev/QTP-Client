using System.Data.SqlClient;
using System.Windows;

namespace QTP_Client
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            using (SqlConnection connect = new SqlConnection(strSQLConnection()))
            {
                connect.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connect;
                comm.CommandText = "SELECT * FROM t_groupe";
                SqlDataReader read = comm.ExecuteReader();
                while (read.Read())
                {
                    cb_unit.Items.Add(read.GetValue(1).ToString());
                }
                read.Close();
                comm.CommandText = "SELECT * FROM t_numberGroupe";
                read = comm.ExecuteReader();
                while (read.Read())
                {
                    cb_numberUnit.Items.Add(read.GetValue(1).ToString());
                }
                read.Close();
                comm.CommandText = "SELECT * FROM t_zvezda";
                read = comm.ExecuteReader();
                while (read.Read())
                {
                    cb_zvezda.Items.Add(read.GetValue(1).ToString());
                }
                read.Close();
                connect.Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_nameMat.Text.Length != 1 || tb_surename.Text.Length < 2 || tb_nameName.Text.Length != 1 || !(tb_surename.Text[0] > 'А' && tb_surename.Text[0] < 'Я') || !(tb_nameMat.Text[0] > 'А' && tb_nameMat.Text[0] < 'Я') || !(tb_nameName.Text[0] > 'А' && tb_nameName.Text[0] < 'Я'))
            {
                MessageBox.Show("Введите строго по макету Иванов А.А.");
            }

        }

        public string strSQLConnection()
        {
            return "Server=" + Properties.Settings.Default.pathSQL + ";Initial Catalog =QTPDB; User ID = sa; Password = qwerty12";
        }
    }
}
