using System.Data.SqlClient;
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

    public partial class TestingWindow : Window
    {
        myAll all;
        Disciplines now;
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
            all = new myAll(_nameTems, strSQLConnection());
            label_nameTems.Content = all._arrayDisciplines[0]._nameDisciplines;
            Button[] arrayB = new Button[all._arrayDisciplines[0]._arrayQuestion.Length];
            for (int i =0; i < all._arrayDisciplines[0]._arrayQuestion.Length; i++)
            {
                arrayB[i] = new Button();
                arrayB[i].Width = 30;
                arrayB[i].Height = 30;
                arrayB[i].Content = i+1;
                arrayB[i].HorizontalAlignment = HorizontalAlignment.Left;
                arrayB[i].VerticalAlignment = VerticalAlignment.Top;
                wr_panel.Children.Add(arrayB[i]);
            }
            now = all._arrayDisciplines[0];
            labeNumber.Content = "1";
            tb_Question.Text = now._arrayQuestion[0]._nameQuestrion;
        }


        public void createInterface(Question question)
        {
            for (int i = 0; i < question._kAnswers.Length; i++)
            {
                
            }
        }

        public void buttonCkick (int count)
        {
            
            tb_Question.Text = now._nameDisciplines.ToString();
        }
        //public bool eqQuestion(string answer, string idQuestion)
        //{
        //    using (SqlConnection con = new SqlConnection(strSQLConnection()))
        //    {
        //        con.Open();
        //        SqlCommand com = new SqlCommand();
        //        com.Connection = con;
        //        com.CommandText = "";
        //    }
        //    return true;
        //}
        public class myAll
        {
            public myAll(string[] nameDisciplines, string strSQLconnection)
            {
                string n ="0";
                _connectionSQL = strSQLconnection;
                _arrayDisciplines = new Disciplines[nameDisciplines.Length];
                for (int i = 0; i< nameDisciplines.Length; i++)
                {

                    using (SqlConnection con = new SqlConnection(_connectionSQL))
                    {

                        con.Open();
                        SqlCommand com = new SqlCommand("SELECT * FROM t_tems");
                        com.Connection = con;
                        SqlDataReader read = com.ExecuteReader();
                        while(read.Read())
                        {
                            if (nameDisciplines[i] == read.GetString(1).ToString())
                            {
                                n = read.GetString(5).ToString();
                            }
                        }
                    }

                    _arrayDisciplines[i] = new Disciplines(nameDisciplines[i], _connectionSQL, Convert.ToInt32(n));
                }
            }
            private string _connectionSQL;
            public Disciplines[] _arrayDisciplines;
            public int _count;
        }
        public class Disciplines
        {
            public Disciplines(string nameDis, string strSQLconnection, int countQuestionTesting)
            {
                _nameDisciplines = nameDis;
                _countQuestionTesting = countQuestionTesting;
                _connection = new SqlConnection(strSQLconnection);
                _connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM t_tems";
                command.Connection = _connection;
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
                setQuestion();
            }

            private int countQuestion()
            {
                int count = 0;
                _connection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM t_question");
                com.Connection = _connection;
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
            private void setQuestion()
            {
                int count = 0;
                Question[] bufferArrayQuestion = new Question[countQuestion()];
                _connection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM t_question");
                com.Connection= _connection;
                SqlDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    if (read.GetValue(1).ToString() == _tKey)
                    {
                        bufferArrayQuestion[count]._nameQuestrion = read.GetValue(3).ToString();
                        bufferArrayQuestion[count]._kQuestion = read.GetValue(0).ToString();
                        bufferArrayQuestion[count]._kTrueAnswer = getArrayKTrueAnswer(read.GetValue(2).ToString());
                        count++;
                    }
                }
                read.Close();
                int[] bufNubmerRandom = getArray(_countQuestionTesting);
                _connection.Close();
                for (int i = 0; i < _arrayQuestion.Length; i++)
                {
                    _arrayQuestion[i]= bufferArrayQuestion[randomNumber(ref bufNubmerRandom)];
                    _arrayQuestion[i]._kAnswers = getArrayKAnswer(_arrayQuestion[i]._kQuestion);
                }
            }
            private int randomNumber(ref int[] arrInt)
            {
                Random r = new Random();
                int numberRandom = r.Next(0, arrInt.Length), random = arrInt[numberRandom], count = 0;
                int[] newArr = new int[arrInt.Length - 1];
                for (int i = 0; i < arrInt.Length; i++)
                {
                    if (i != numberRandom)
                    {
                        newArr[count] = arrInt[i];
                        count++;
                    }
                }
                arrInt = newArr;
                return random;
            }
            private int[] getArray(int number)
            {
                int[] answer = new int[number];
                for (int i = 0; i < answer.Length; i++)
                {
                    answer[i] = i;
                }

                return answer;
            }
            private string[] getArrayKTrueAnswer(string str)
            {
                if (str.Length > 0)
                {
                    int count = 0, indexArray = 0;
                    string buffer = "";
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '|')
                        {
                            count++;
                        }
                    }
                    string[] answer = new string[count];
                    for (int i = 0; i < str.Length; i++)
                    {
                        buffer = "";
                        while (i < str.Length && str[i] != '|')
                        {
                            buffer += str[i];
                            i++;
                        }
                        if (buffer != "")
                        {
                            answer[indexArray] = buffer;
                            indexArray++;
                        }
                    }
                    return answer;
                }
                return null;
            }
            private string[] getArrayKAnswer(string qKey)
            {
                string[] str;
                int _count = 0; 
                _connection.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM t_answer");
                com.Connection = _connection;
                SqlDataReader read = com.ExecuteReader();
                while(read.Read())
                {
                    if (qKey == read.GetValue(1).ToString())
                        _count++;
                }
                str = new string[_count];
                read.Close();
                read = com.ExecuteReader();
                _count = 0; 
                while (read.Read())
                {
                    if (qKey == read.GetValue(1).ToString())
                    {
                        str[_count] = read.GetValue(0).ToString();
                        _count++;
                    }
                }
                _connection.Close();

                return str;
            }


            private int _countQuestionTesting;
            private string _tKey;
            public string _nameDisciplines;
            private SqlConnection _connection;
            public Question[] _arrayQuestion;
        }
        public struct Question
        {
            public string _nameQuestrion, _kQuestion;
            public string[] _kAnswers;
            public string[] _kTrueAnswer; //хранит индекс правильного ответа
            public string[] _answerUser;
        }
        public string strSQLConnection()
        {
            return "Server=" + Properties.Settings.Default.pathSQL + ";Initial Catalog =QTPDB; User ID = sa; Password = qwerty12";
        }

    }
}
