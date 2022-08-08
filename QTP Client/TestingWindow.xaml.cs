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
using System.Windows.Threading;

namespace QTP_Client
{

    public partial class TestingWindow : Window
    {
        int selectButton;
        Button[] arrayB;
        Random rand = new Random();
        Question selectQuestion;
        myAll all;
        Disciplines now;
        DoublFuck [] arrayDB;
        DispatcherTimer timer;
        int numberTems, allMin;
        string unit, numberUnit, zvezda, fullName;
        string[] nameTems;
        public static RoutedCommand MyCommand = new RoutedCommand();
        bool globalTime;
     

        public TestingWindow(string _unit, string _nameUnit, string _zvezda, string _fullName, string[] _nameTems)
        {
            InitializeComponent();
            globalTime = false;

            MyCommand.InputGestures.Add(new KeyGesture(Key.Enter));
            arrayDB  =new DoublFuck[6];
            arrayDB[0]._cb = cb1;
            arrayDB[0]._tb = tb1;
            arrayDB[1]._cb = cb2;
            arrayDB[1]._tb = tb2;
            arrayDB[2]._cb = cb3;
            arrayDB[2]._tb = tb3;
            arrayDB[3]._cb = cb4;
            arrayDB[3]._tb = tb4;
            arrayDB[4]._cb = cb5;
            arrayDB[4]._tb = tb5;
            arrayDB[5]._cb = cb6;
            arrayDB[5]._tb = tb6;
            
            for (int i =0; i < 6;i++)
            {
                arrayDB[i]._tb.Visibility = Visibility.Hidden;
                arrayDB[i]._cb.Visibility = Visibility.Hidden;
            }

            unit = _unit;
            numberUnit = _nameUnit;
            zvezda = _zvezda;
            fullName = _fullName;
            nameTems = _nameTems;
            all = new myAll(nameTems, strSQLConnection());
            numberTems = 0;
            now = all._arrayDisciplines[numberTems];
            label_nameTems.Content = now._nameDisciplines;

            arrayB = new Button[now._arrayQuestion.Length];
            for (int i =0; i < now._arrayQuestion.Length; i++)
            {
                arrayB[i] = new Button();
                arrayB[i].Width = 30;
                arrayB[i].Height = 30;
                arrayB[i].Content = i+1;
                arrayB[i].Click += new RoutedEventHandler(this.buttonCkick);
                arrayB[i].HorizontalAlignment = HorizontalAlignment.Left;
                arrayB[i].VerticalAlignment = VerticalAlignment.Top;
                wr_panel.Children.Add(arrayB[i]);
            }
            allMin = getAllMin(now);
            allMin = allMin * 60;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            
            arrayB[0].RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); 
        }
        public TestingWindow(string _unit, string _nameUnit, string _zvezda, string _fullName, string[] _nameTems, int countNext)
        {
            InitializeComponent();
            globalTime = false;

            MyCommand.InputGestures.Add(new KeyGesture(Key.Enter));
            arrayDB = new DoublFuck[6];
            arrayDB[0]._cb = cb1;
            arrayDB[0]._tb = tb1;
            arrayDB[1]._cb = cb2;
            arrayDB[1]._tb = tb2;
            arrayDB[2]._cb = cb3;
            arrayDB[2]._tb = tb3;
            arrayDB[3]._cb = cb4;
            arrayDB[3]._tb = tb4;
            arrayDB[4]._cb = cb5;
            arrayDB[4]._tb = tb5;
            arrayDB[5]._cb = cb6;
            arrayDB[5]._tb = tb6;

            for (int i = 0; i < 6; i++)
            {
                arrayDB[i]._tb.Visibility = Visibility.Hidden;
                arrayDB[i]._cb.Visibility = Visibility.Hidden;
            }

            unit = _unit;
            numberUnit = _nameUnit;
            zvezda = _zvezda;
            fullName = _fullName;
            nameTems = _nameTems;
            all = new myAll(nameTems, strSQLConnection());
            numberTems = countNext;
            now = all._arrayDisciplines[numberTems];
            label_nameTems.Content = now._nameDisciplines;

            arrayB = new Button[now._arrayQuestion.Length];
            for (int i = 0; i < now._arrayQuestion.Length; i++)
            {
                arrayB[i] = new Button();
                arrayB[i].Width = 30;
                arrayB[i].Height = 30;
                arrayB[i].Content = i + 1;
                arrayB[i].Click += new RoutedEventHandler(this.buttonCkick);
                arrayB[i].HorizontalAlignment = HorizontalAlignment.Left;
                arrayB[i].VerticalAlignment = VerticalAlignment.Top;
                wr_panel.Children.Add(arrayB[i]);
            }
            allMin = getAllMin(now);
            allMin = allMin * 60;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            arrayB[0].RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        public void timerTick (object sender, EventArgs e)
        {
            int hour, minute, second;
            allMin--;
            if (allMin == 0)
            {
                globalTime = true;
                complate();
            }
            hour = allMin / 3600;
            minute = (allMin - (hour * 3600) )/ 60;
            second = (allMin - (hour * 3600) - (minute *60));

            tb_timer.Text = "Ч:" + Convert.ToString(hour) + "\tМ:" + Convert.ToString(minute) + "\tC:" + Convert.ToString(second);
        }
        public string getNumberButton (string str)
        {
            string answer = "";
            bool popp = true;
            for (int i = 1; i < str.Length;i++)
            {
                while (str[i-1] != ':' && popp)
                {
                    i++;
                }
                
                popp = false;
                answer += str[i];
            }

            return answer.Trim();
        }
        public void buttonCkick (object sender, RoutedEventArgs e)
        {
            clear();
            labeNumber.Content = getNumberButton(e.Source.ToString());
            int count = Convert.ToInt32(labeNumber.Content);
            tb_Question.Text = now._arrayQuestion[count-1]._nameQuestrion;
            selectButton = count;

            selectQuestion = now._arrayQuestion[count - 1];
            for (int i = 0; i < now._arrayQuestion[count-1]._kAnswers.Length;i++)
            {
                arrayDB[i]._tb.Visibility = Visibility.Visible;
                arrayDB[i]._cb.Visibility = Visibility.Visible;
                setQuestion(getQuestionAnswer(count - 1, i), now._arrayQuestion[count - 1]._kAnswers[i], now._arrayQuestion[count - 1]._kAnswers.Length);
            }
            setIfGreen(selectQuestion);
        }
        public void setIfGreen (Question q)
        {
            if ( q._answerUser != null &&q._answerUser.Length != 0)
            {
                for (int i = 0; i  < q._answerUser.Length;i++)
                {
                    using (SqlConnection c = new SqlConnection(strSQLConnection()))
                    {
                        c.Open();
                        SqlCommand command = new SqlCommand();
                        command.CommandText = "SELECT * FROM t_answer";
                        command.Connection = c;
                        SqlDataReader read = command.ExecuteReader();
                        while (read.Read())
                        {
                            if (read.GetValue(0).ToString() == q._answerUser[i])
                            {
                                setAccessForGreens(read.GetValue(2).ToString());
                            }
                        }
                        read.Close();
                        c.Close();  
                    }
                }
            }
        }
        public void setAccessForGreens(string str)
        {
            for (int i = 0;  i < arrayDB.Length;i++)
            {
                if (arrayDB[i]._tb != null && arrayDB[i]._tb.Text != null && arrayDB[i]._tb.Text == str)
                    arrayDB[i]._cb.IsChecked = true;
            }
        }
        public void clear ()
        {
            for (int i = 0; i < arrayDB.Length;i++)
            {
                arrayDB[i]._tb.Text = null;
                arrayDB[i]._tb.Visibility = Visibility.Hidden;
                arrayDB[i]._cb.IsChecked = false;
                arrayDB[i]._cb.Visibility = Visibility.Hidden;
                arrayDB[i]._keyAnswer = null;
            }
        }
        public void complate()
        {
            bool ch = true;
            for (int i = 0; i < now._arrayQuestion.Length;i++)
            {
                if (now._arrayQuestion[i]._answerUser == null)
                {
                    ch = false;
                }
            }
            if (ch)
            {
                Result form = new Result(now, unit, numberUnit, zvezda, fullName, numberTems, nameTems);
                form.Show();
                Close();
            }
            if (globalTime)
            {
                Result form = new Result(now, unit, numberUnit, zvezda, fullName, numberTems, nameTems);
                form.Show();
                Close();
            }
            
        }
        public void setQuestion(string answer, string keyAnswer, int countAnswer)
        {
            int[] buffer;
            int index = 0;
            for (int i = 0; i < countAnswer; i++) 
            {
                if (arrayDB[i]._keyAnswer == null )
                {
                    index++;
                }
            }
            buffer = new int[index];
            index = 0;
            for (int i = 0; i < countAnswer; i++)
            {
                if (arrayDB[i]._keyAnswer == null)
                {
                    buffer[index] = i;
                    index++;
                }
            }
            int countPole = getRandomNubmerTB(buffer, index);
            arrayDB[countPole]._tb.Text = answer;
            arrayDB[countPole]._keyAnswer = keyAnswer;
        }
        public int getRandomNubmerTB(int [] buufer, int count)
        {
            int num  = rand.Next(0, count);
            return buufer[num];

        }
        public string getQuestionAnswer (int numberQuestion, int numberAnswer)
        {
            using (SqlConnection con = new SqlConnection(strSQLConnection()))
            {
                con.Open();
                SqlCommand comand = new SqlCommand("SELECT * FROM t_answer");
                comand.Connection = con;
                SqlDataReader reader = comand.ExecuteReader();
                while(reader.Read())
                {
                    if (reader.GetValue(0).ToString() == now._arrayQuestion[numberQuestion]._kAnswers[numberAnswer])
                    {
                        return reader.GetString(2);
                    }
                }
                return null;
            }
        }
        private void b_next_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < 6; i++) 
            {
                if (arrayDB[i]._keyAnswer != null && arrayDB[i]._cb.IsChecked == true)
                {
                    count++;
                }
            }
            string [] arrayAnswerUser = new string [count];
            count = 0;
            for (int i =0; i < 6; i ++)
            {
                if (arrayDB[i]._keyAnswer != null && arrayDB[i]._cb.IsChecked == true)
                {
                    arrayAnswerUser[count] = arrayDB[i]._keyAnswer;
                    count++;
                }
            }
            if (arrayAnswerUser.Length >0)
            {
                arrayB[selectButton - 1].Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                selectQuestion._answerUser = arrayAnswerUser;
                now._arrayQuestion[selectButton-1]._answerUser = arrayAnswerUser;
            }
            else
            {
                arrayB[selectButton - 1].Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }
            if (selectButton < now._arrayQuestion.Length)
            {
                arrayB[selectButton].RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            complate();
        }
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
                r = new Random();

                while (read.Read())
                {
                    if (read.GetValue(1).ToString() == _nameDisciplines)
                    {
                        _tKey = read.GetValue(0).ToString();
                        _free = Convert.ToInt32(read.GetValue(2).ToString());
                        _four = Convert.ToInt32(read.GetValue(3).ToString());
                        _five = Convert.ToInt32(read.GetValue(4).ToString());
                        _time = Convert.ToInt32(read.GetValue(6).ToString());
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
                int[] bufNubmerRandom = getArray(bufferArrayQuestion.Length);
                _connection.Close();
                for (int i = 0; i < _arrayQuestion.Length; i++)
                {
                    _arrayQuestion[i]= bufferArrayQuestion[randomNumber(ref bufNubmerRandom)];
                    _arrayQuestion[i]._kAnswers = getArrayKAnswer(_arrayQuestion[i]._kQuestion);
                }
            }
            private int randomNumber(ref int[] arrInt)
            {
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


            public Random r;
            private int _countQuestionTesting;
            public int _five, _four, _free, _time;
            private string _tKey;
            public string _nameDisciplines;
            private SqlConnection _connection;
            public Question[] _arrayQuestion;
        }
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            b_next.RaiseEvent(new RoutedEventArgs(Button.ClickEvent)); 
        }
        private void tb1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cb1.IsChecked == false)
            {
                cb1.IsChecked = true;
            }
            else
            {
                cb1.IsChecked = false;
            }
        }
        private void tb2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cb2.IsChecked == false)
            {
                cb2.IsChecked = true;
            }
            else
            {
                cb2.IsChecked = false;
            }
        }
        private void tb3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cb3.IsChecked == false)
            {
                cb3.IsChecked = true;
            }
            else
            {
                cb3.IsChecked = false;
            }
        }
        private void tb4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cb4.IsChecked == false)
            {
                cb4.IsChecked = true;
            }
            else
            {
                cb4.IsChecked = false;
            }
        }
        private void tb5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cb5.IsChecked == false)
            {
                cb5.IsChecked = true;
            }
            else
            {
                cb5.IsChecked = false;
            }
        }
        private void tb6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cb6.IsChecked == false)
            {
                cb6.IsChecked = true;
            }
            else
            {
                cb6.IsChecked = false;
            }
        }

        

        public struct Question
        {
            public string _nameQuestrion, _kQuestion;
            public string[] _kAnswers;
            public string[] _kTrueAnswer; //хранит индекс правильного ответа
            public string[] _answerUser;
        }
        public struct DoublFuck
        {
            public CheckBox _cb;
            public TextBlock _tb;
            public string _keyAnswer;
        }
        public string strSQLConnection()
        {
            return "Server=" + Properties.Settings.Default.pathSQL + ";Initial Catalog =QTPDB; User ID = sa; Password = qwerty12";
        }
        public int getAllMin(Disciplines a)
        {
            int answer = 0;
            using (SqlConnection c = new SqlConnection(strSQLConnection()))
            {
                c.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM t_tems");
                command.Connection = c;
                SqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    if (read.GetValue(1).ToString() == a._nameDisciplines)
                    {

                        answer = Convert.ToInt32(read.GetValue(5).ToString()) * Convert.ToInt32(read.GetValue(6).ToString());
                    }
                }
                read.Close();
                c.Close();

            }
            return answer;
        }


    }
}
