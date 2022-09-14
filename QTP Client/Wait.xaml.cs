using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace QTP_Client
{
    public partial class Wait : Window
    {
        int count;
        bool test;
        CheckBox[] checkBoxes = new CheckBox[1000];
        string unit, numberUnit, zvezda, fullName;
        public Wait(string _unit, string _nameUnit, string _zvezda, string _fullName)
        {
            test = false;
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
                    if (read.GetValue(7).ToString() != "0")
                    {
                        checkBoxes[count] = new CheckBox();
                        checkBoxes[count].FontSize = 24;
                        checkBoxes[count].FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
                        checkBoxes[count].Content = read.GetString(1);
                        checkBoxes[count].Margin = new Thickness(30, 10, 0, 10);
                        panel.Children.Add(checkBoxes[count]);
                        count++;
                    }
                }
                read.Close();
                connect.Close();
            }
        }
        public Wait()
        {
            test = true;
            InitializeComponent();
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
                    if (read.GetValue(7).ToString() != "0")
                    {
                        checkBoxes[count] = new CheckBox();
                        checkBoxes[count].FontSize = 24;
                        checkBoxes[count].VerticalAlignment = VerticalAlignment.Center;
                        checkBoxes[count].VerticalContentAlignment = VerticalAlignment.Center;
                        checkBoxes[count].FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
                        checkBoxes[count].Content = read.GetString(1);
                        checkBoxes[count].Margin = new Thickness(30, 10, 0, 10);
                        panel.Children.Add(checkBoxes[count]);
                        count++;
                    }
                   
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
            if (checkBoxes[0] == null)
            {
                MessageBox.Show("Пока что темы для тестирования не добавлены. Пожалуйста обратитесь к адмнистратору.", "Внимаение", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (countArr == 0)
            {
                MessageBox.Show("Пожалуйста выберите темы тестирования", "Внимаение", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
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

            if(!test)
            {
                TestingWindow form = new TestingWindow(unit, numberUnit, zvezda, fullName, buffer);
                try
                {
                    form.Show();

                }
                catch
                {

                }
                Close();
            }
            else
            {
                TestingWindow form = new TestingWindow(buffer);
                try
                {
                    form.Show();

                }
                catch
                {

                }
                Close();
            }

        }
    }

}
