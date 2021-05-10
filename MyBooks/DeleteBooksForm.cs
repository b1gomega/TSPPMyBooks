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

namespace MyBooks {
    public partial class DeleteBooksForm : Form {
        private Form prev_form;
        public DeleteBooksForm(Form RrevForm) {
            InitializeComponent();
            prev_form = RrevForm;

            OuterDesign.ChangeForm(this);
        }
        private void DeleteBooksForm_Load(object sender, EventArgs e) {

        }
        private void label2_Click(object sender, EventArgs e) {

        }
        //Кнопка видалити книгу
        private void button2_Click(object sender, EventArgs e) {
            if (NameBookField.Text == "") {
                MessageBox.Show("Не введено дані");
                return;
            }

            string UserName;

            UserName = NameBookField.Text;

            if (!isBookInTable(UserName)) {
                MessageBox.Show("Такої книги немає в базі даних");
                ClearTextBox();
                return;
            }

            MySQL mysql = new MySQL();

            mysql.openConnection();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("DELETE FROM `bookslibrarytable` WHERE `bookslibrarytable`.`name` = @uN", mysql.getConnection());
            command.Parameters.AddWithValue("@uN", UserName);

            adapter.SelectCommand = command;
            adapter.Fill(table);

            MessageBox.Show("Книга успішно видалена з бази даних");

            ClearTextBox();

            mysql.closeConnection();
        }

        //Проверка есть ли книга в базе данных
        public Boolean isBookInTable(string _name) {
            MySQL mysql = new MySQL();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `bookslibrarytable` WHERE `name` = @uN", mysql.getConnection());
            command.Parameters.AddWithValue("@uN", _name);

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0) {
                return true;
            }
            else {
                return false;
            }
        }

        public void ClearTextBox() {
            NameBookField.Text = "";
        }
        //Кнопка очистити
        private void button1_Click(object sender, EventArgs e) {
            ClearTextBox();
        }
        //Кнопка показати всі книги
        private void button3_Click(object sender, EventArgs e) {
            this.Hide();
            BooksOutputForm booksOutputForm = new BooksOutputForm(this);
            booksOutputForm.Show();
        }       
        //Кнопка скасувати
        private void button4_Click(object sender, EventArgs e) {
            this.Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (!prev_form.IsDisposed) prev_form.Show();
            base.OnFormClosing(e);
        }
    }
}
