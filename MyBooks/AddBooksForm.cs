using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBooks
{
    public partial class AddBooksForm : Form {
        Form prev_form;
        public AddBooksForm(Form PrevForm) {
            InitializeComponent();
            prev_form = PrevForm;

            OuterDesign.ChangeForm(this);
            this.CancelButton = button3;
            this.AcceptButton = button2;

        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            //Lines
            using (Graphics g = Graphics.FromHwnd(this.Handle)) {
                using (Pen p = new Pen(OuterDesign.TextAndBorderColor, 1)) {
                    g.DrawLine(p, 12, 59, ClientSize.Width - 12, 59);
                    g.DrawLine(p, 12, 330, ClientSize.Width - 12, 330);
                }
            }
        }
        private void AddBooks_Load(object sender, EventArgs e)
        {

        }
        private void NameBookField_TextChanged(object sender, EventArgs e)
        {

        }
        //Кнопка Скасувати
        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }
        //Кнопка Додати
        private void button2_Click(object sender, EventArgs e) 
        {
            if(NameBookField.Text == "")
            {
                MessageBox.Show("Не введено дані");
                return;
            }
            else if (SurnameAuthorField.Text == "")
            {
                MessageBox.Show("Не введено дані");
                return;
            }
            else if (YearCreateField.Text == "")
            {
                MessageBox.Show("Не введено дані");
                return;
            }
            else if (PlaceField.Text == "")
            {
                MessageBox.Show("Не введено дані");
                return;
            }

            string UserName, UserSurname;
            int UserYear, UserPlace;

            UserName = NameBookField.Text;
            UserSurname = SurnameAuthorField.Text;
            UserYear = Convert.ToInt32(YearCreateField.Text);
            UserPlace = Convert.ToInt32(PlaceField.Text);

            if (!isUniqueNameBook(UserName) && !isUniqueSurnameAuthor(UserSurname))
            {
                MessageBox.Show("Автор та назва такої книги уже є в базі даних");
                return;
            }
            else if(!isCorrectInput(UserYear, UserPlace))
            {
                MessageBox.Show("Неправильно введений рік видання або місце розташування книги");
                return;
            }
            else if (!isFreePlace(UserPlace))
            {
                MessageBox.Show("Це місце вже зайнято");
                return;
            }

            MySQL mysql = new MySQL();

            mysql.openConnection();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            //MySqlCommand count = new MySqlCommand("SELECT COUNT(*) as count FROM `bookslibrarytable`", mysql.getConnection());

            //int AmountBooksInLibrary = (int)count.ExecuteScalar();

            //if (AmountBooksInLibrary >= 250)
            //{
            //    MessageBox.Show("В базі даних уже 250 книг, більше не можна");
            //    return;
            //}

            MySqlCommand command = new MySqlCommand("INSERT INTO `bookslibrarytable` (`id`, `surname`, `name`, `year`, `place`) VALUES (NULL, @uS, @uN, @uY, @uP);", mysql.getConnection());
            command.Parameters.Add("@uN", MySqlDbType.VarChar).Value = UserName;
            command.Parameters.Add("@uS", MySqlDbType.VarChar).Value = UserSurname;
            command.Parameters.Add("@uY", MySqlDbType.Int32).Value = UserYear;
            command.Parameters.Add("@uP", MySqlDbType.Int32).Value = UserPlace;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            MessageBox.Show("Дані успішно занесені до бази даних");

            BookLibrary bookLibrary = new BookLibrary();
            //bookLibrary.BooksAtLibraryAtTheMoment = AmountBooksInLibrary + 1;

            ClearTextBox();

            mysql.closeConnection();

        }
        //Кнопка Очистити
        private void button3_Click(object sender, EventArgs e) {
            ClearTextBox();
        }
        //Кнопка показати всі книги
        private void button4_Click(object sender, EventArgs e) {           
            this.Hide();
            BooksOutputForm booksOutputForm = new BooksOutputForm(this);
            booksOutputForm.Show();
        }

        //Проверка на уникальность фамилии автора
        public Boolean isUniqueSurnameAuthor(string _surname)
        {
            MySQL mysql = new MySQL();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `bookslibrarytable` WHERE `surname` = @uS", mysql.getConnection());
            command.Parameters.Add("@uS", MySqlDbType.VarChar).Value = _surname;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Проверка на уникальность название книги
        public Boolean isUniqueNameBook(string _name)
        {
            MySQL mysql = new MySQL();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `bookslibrarytable` WHERE `name` = @uN", mysql.getConnection());
            command.Parameters.Add("@uN", MySqlDbType.VarChar).Value = _name;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Проверка на правильность введения года и места
        public Boolean isCorrectInput(int _year, int _place)
        {
            if(_year > 2020 || _year < 1800)
            {
                return false;
            }
            else if(_place <= 0 || _place > 9999)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Проверка на доступность места в библиотеке
        public Boolean isFreePlace(int _place)
        {
            MySQL mysql = new MySQL();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `bookslibrarytable` WHERE `place` = @uP", mysql.getConnection());
            command.Parameters.Add("@uP", MySqlDbType.Int32).Value = _place;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Очистка полей
        public void ClearTextBox()
        {
            NameBookField.Text = "";
            SurnameAuthorField.Text = "";
            YearCreateField.Text = "";
            PlaceField.Text = "";
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (!prev_form.IsDisposed) prev_form.Show();
            base.OnFormClosing(e);
        }
    }
}
