using System.Data.SqlClient;
using System.Windows;

namespace QTP_Client
{
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
                    //cb_numberUnit.Items.Add(read.GetValue(1).ToString());
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
            if (!((CheackSurname(tb_surename.Text.Trim())) && (CheackNameOrSecondName(tb_nameMat.Text.Trim())) &&(CheackNameOrSecondName(tb_nameName.Text.Trim()))))
            {
                MessageBox.Show("Введите строго по макету Иванов А.А.");
            }
            else
            {
                Wait form = new Wait(cb_unit.Text, "", cb_zvezda.Text, tb_surename.Text.Trim() + " " + tb_nameName.Text.Trim() + "." + tb_nameMat.Text.Trim() + ".");
                form.Show();
                Close();
            }
        }
        public bool CheackSurname (string surname)
        {
            if (surname.Length < 2)
                return false;

            for (int i = 0; i < surname.Length; i++)
            {
                if ((i == 0) && !((surname[i] >= 'А') && (surname[i] <= 'Я')))
                    return false;
                if ((i != 0) && !((surname[i] >= 'а') && (surname[i] <= 'я')))
                    return false;
            }

            return true;
        }
        public bool CheackNameOrSecondName (string name)
        {
            if (name.Length != 1)
                return false;

            if (!((name[0] >= 'А') && (name[0] <= 'Я')))
                return false;

            return true;
        }
        public string strSQLConnection()
        {
            return "Server=" + Properties.Settings.Default.pathSQL + ";Initial Catalog =QTPDB; User ID = sa; Password = qwerty12";
        }

    }
}
