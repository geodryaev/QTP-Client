using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace QTP_Client
{
    public partial class Wait : Window
    {
        int count;
        CheckBox[] checkBoxes = new CheckBox[1000];
        string unit, numberUnit, zvezda, fullName;
        public Wait(string _unit, string _nameUnit, string _zvezda, string _fullName)
        {
            InitializeComponent();
            unit = _unit;
            numberUnit = _nameUnit;
            zvezda = _zvezda;
            fullName = _fullName;
            using (SqlConnection connect = new SqlConnection(strSQLConnection()))
            {
                connect.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = connect;
                com.CommandText = "SELECT * FROM t_tems";
                SqlDataReader read = com.ExecuteReader();
                count = 0;
                while (read.Read())
                {
                    checkBoxes[count] = new CheckBox();
                    checkBoxes[count].Content = read.GetString(1);
                    checkBoxes[count].Margin = new Thickness(30, 10, 0, 10);
                    panel.Children.Add(checkBoxes[count]);
                    count++;
                }
                read.Close();
                connect.Close();
            }
        }
        public string strSQLConnection()
        {
            return "Server=" + Properties.Settings.Default.pathSQL + ";Initial Catalog =QTPDB; User ID = sa; Password = qwerty12";
        }

        private void b_go_Click(object sender, RoutedEventArgs e)
        {

            int countArr = 0;
            for (int i = 0; i < count; i++)
            {
                if (checkBoxes[i].IsChecked == true)
                {
                    countArr++;
                }
            }
            string[] buffer = new string[countArr];
            countArr = 0;
            for (int i = 0; i < count; i++)
            {
                if (checkBoxes[i].IsChecked == true)
                {
                    buffer[countArr] = checkBoxes[i].Content.ToString();
                    countArr++;
                }
            }

            TestingWindow form = new TestingWindow(unit,numberUnit,zvezda,fullName,buffer);
            form.Show();
            Close();
        }
    }

}
