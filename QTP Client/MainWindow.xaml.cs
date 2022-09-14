﻿using System.Data.SqlClient;
using System.Windows;

namespace QTP_Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void m_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SQLConnect form = new SQLConnect();
            form.ShowDialog();
        }

        public string strSQLConnection()
        {
            return "Server=" + Properties.Settings.Default.pathSQL + ";Initial Catalog =QTPDB; User ID = sa; Password = qwerty12";
        }

        private void b_conncet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(strSQLConnection()))
                {
                    connect.Open();
                    connect.Close();
                }
                if(true)
                {
                    Main form = new Main();
                    form.Show();
                    Close();
                }
                //недостижимый потому что он вверзу всегда true
                else
                {
                    MessageBox.Show("Введен неверный пароль");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к серверу, провертье путь к серверу" + Properties.Settings.Default.pathSQL); 
            }
        }

        private void bt_test_Click(object sender, RoutedEventArgs e)
        {
            Wait form = new Wait();
            form.Show();
            Close();
        }
    }
}
