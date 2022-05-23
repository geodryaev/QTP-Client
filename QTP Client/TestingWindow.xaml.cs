using System.Data.SqlClient;
using System.Windows;


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
        public bool eqQuestion(string answer, string idQuestion)
        {
            using (SqlConnection con = new SqlConnection(strSQLConnection()))
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "";
            }
            return true;
        }



        public class myAll
        {
            public myAll(string[] nameDisciplines, string strSQLconnection)
            {
                _connectionSQL = strSQLconnection;
            }

            private string _connectionSQL;
            public Disciplines[] _arrayQuestion;
            public int _count;
        }


        public class Disciplines
        {
            public Disciplines(string str, string strSQLconnection, int countQuestionTesting)
            {
                _nameDisciplines = str;
                _countQuestionTesting = countQuestionTesting;
                _connection = new SqlConnection(strSQLconnection);
                _connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM t_tems";
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    if (read.GetValue(1).ToString() == _nameDisciplines)
                    {
                        _tKey = read.GetValue(0).ToString();
                    }
                }
                read.Close();
                _connection.Close();
                _arrayQuestion = new Question[_countQuestionTesting];
            }

            private int countQuestion()
            {
                int count = 0;
                _connection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM t_question");
                SqlDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    if (read.GetValue(1).ToString() == _tKey)
                    {
                        count++;
                    }
                }
                read.Close();
                _connection.Close();
                return count;
            }
            private void setQuestion ()
            {
                int count = 0;
                string[] bufferArrayQuestion = new string[countQuestion()];
                _connection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM t_question");
                SqlDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    if (read.GetValue(1).ToString() == _tKey)
                    {
                        bufferArrayQuestion[count] = read.GetValue(0).ToString();
                        count++;
                    }
                }
                read.Close();
            }
            private int _countQuestionTesting;
            private string _tKey ;
            public string _nameDisciplines;
            private SqlConnection _connection;
            public Question[] _arrayQuestion;
        }
        public struct Question
        {
            public string _nameQuestrion, _kQuestion;
            public string[] _kAnswers;
            public int[] _kTrueAnswer;// хранит индекс правильного ответа
        }
        public string strSQLConnection()
        {
            return "Server=" + Properties.Settings.Default.pathSQL + ";Initial Catalog =QTPDB; User ID = sa; Password = qwerty12";
        }
    }
}
