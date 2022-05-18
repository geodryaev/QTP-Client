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
using System.Data.SqlClient;


namespace QTP_Client
{

    public partial class TestingWindow : Window
    {
        string[] nameTems;
        string unit, numberUnit, zvezda, fullName;
        public TestingWindow(string _unit, string _nameUnit, string _zvezda, string _fullName, string[] _nameTems)
        {
            InitializeComponent();
            unit = _unit;
            numberUnit = _nameUnit;
            zvezda = _zvezda;
            fullName = _fullName;
            nameTems = _nameTems;
            using (SqlConnection con = new SqlConnection(strSQLConnection()))
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT * FROM t_tems";

            }
        }
        public bool eqQuestion (string answer, string idQuestion)
        {
            using (SqlConnection con = new SqlConnection(strSQLConnection()))
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "";
            }
        }
        public string strSQLConnection()
        {
            return "Server=" + Properties.Settings.Default.pathSQL + ";Initial Catalog =QTPDB; User ID = sa; Password = qwerty12";
        }

        public struct Disciplines
        {
            public string _name;
            public string 
        }
        public struct Question
        {
            public string _nameQuestrion;
            public string[] _answers;
            public int _countAnser;
            public string answer;
        }

    }
}
