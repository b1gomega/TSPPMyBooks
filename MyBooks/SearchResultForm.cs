using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace MyBooks {
    public partial class SearchResultForm : Form {
        private Form prev_form;
        public SearchResultForm(Form PrevForm) {
            InitializeComponent();
            prev_form = PrevForm;

            OuterDesign.ChangeForm(this);
            if (MyBooks.BookLibrary.VariantForSearch == 1) {
                ShowResultFirstVariant();
            }
            else {
                ShowResultSecondVariant();
            }
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            //Lines
            using (Graphics g = Graphics.FromHwnd(this.Handle)) {
                using (Pen p = new Pen(OuterDesign.TextAndBorderColor, 1)) {
                    g.DrawLine(p, 12, 59, ClientSize.Width - 12, 59);
                    g.DrawLine(p, 12, 510, ClientSize.Width - 12, 510);
                }
            }
        }
        private void label1_Click(object sender, EventArgs e) {

        }
        private void SearchResultForm_Load(object sender, EventArgs e) {

        }
        //Кнопка Повернутися
        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }
        //Кнопка Для cформування MS Word File
        private void button2_Click(object sender, EventArgs e) {
            CreateWordFile();
            MessageBox.Show("Звіт з ім'ям MyBooksResult.docx збережений в папці проекту");
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }
        //Відображення пошуку принципу ХУ
        public void ShowResultFirstVariant() {
            string UserName = MyBooks.BookLibrary.NameForSearch;
            string UserSurname = MyBooks.BookLibrary.SurnameForSearch;

            MySQL mysql = new MySQL();

            mysql.openConnection();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand commandPlace = new MySqlCommand("SELECT place FROM `bookslibrarytable` WHERE name = @uN AND surname = @uS", mysql.getConnection());
            commandPlace.Parameters.Add("@uN", MySqlDbType.VarChar).Value = UserName;
            commandPlace.Parameters.Add("@uS", MySqlDbType.VarChar).Value = UserSurname;
            MySqlDataReader place = commandPlace.ExecuteReader();

            List<string> books = new List<string>();
            while (place.Read()) {
                string value = "Місце розташування книги автора " + UserSurname + " назви " + UserName + " - " + place[0].ToString();
                books.Add(value);
            }
            if (books.Count > 0) {
                foreach (string book in books) {
                    listBox1.Items.Add(book);
                    MyBooks.BookLibrary.PlaceBookAfterXYSearchVariant += book;
                }
            }
            else {
                listBox1.Items.Add("Книг з такою назвою і таким автором нема");
                MyBooks.BookLibrary.PlaceBookAfterXYSearchVariant += "Книг з такою назвою і таким автором нема";
            }
            place.Close();

            adapter.SelectCommand = commandPlace;
            adapter.Fill(table);
            mysql.closeConnection();
        }
        //Відображення пошуку принципу ХХ
        public void ShowResultSecondVariant() {
            int UserYear = MyBooks.BookLibrary.YearForSearch;
            string d = UserYear.ToString();

            MySQL mysql = new MySQL();
            mysql.openConnection();

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand commandName = new MySqlCommand("SELECT surname, name FROM `bookslibrarytable` WHERE year = @uY;", mysql.getConnection());
            commandName.Parameters.Add("@uY", MySqlDbType.Int32).Value = UserYear;
            MySqlDataReader name = commandName.ExecuteReader();

            MyBooks.BookLibrary.NameBookAfterXXSearchVariant = "Кількість книг " + UserYear + " року видання:\n";

            List<string> books = new List<string>();
            while (name.Read()) {
                string value = name[0].ToString() + " - " + name[1].ToString();
                books.Add(value);
            }
            if (books.Count > 0) {
                listBox1.Items.Add($" Знайдена кількість книг: {books.Count}");
                MyBooks.BookLibrary.NameBookAfterXXSearchVariant = $" Знайдена кількість книг: {books.Count}\n";
            }
            else {
                listBox1.Items.Add("Книг з такою назвою і таким автором нема");
                MyBooks.BookLibrary.NameBookAfterXXSearchVariant += "Книг з такою назвою і таким автором нема";
                MyBooks.BookLibrary.NameBookAfterXXSearchVariant += "\n";
            }
            name.Close();

            adapter.SelectCommand = commandName;
            adapter.Fill(table);

            mysql.closeConnection();
        }
        //Створення ворд-файла
        public void CreateWordFile() {
            string WordDocumentPath = "../../../MyBooksResult.docx";
            DocX document = DocX.Create(WordDocumentPath);

            Paragraph paragraph = document.InsertParagraph();
            if (MyBooks.BookLibrary.VariantForSearch == 1) {
                //   document.RemoveParagraph(paragraph);
                paragraph.AppendLine(MyBooks.BookLibrary.PlaceBookAfterXYSearchVariant).FontSize(18).Alignment = Alignment.center;
                // document.InsertParagraph("Відібрані книги згідно пошуку по року\n");
                // document.InsertParagraph("Відібрані книги згідно пошуку по року\n" + MyBooks.BookLibrary.PlaceBookAfterXYSearchVariant);
                document.Save();
            }
            else {
                //   document.RemoveParagraph(paragraph);
                paragraph.AppendLine(MyBooks.BookLibrary.NameBookAfterXXSearchVariant).FontSize(14).Alignment = Alignment.left;
                document.Save();
            }

        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (!prev_form.IsDisposed) prev_form.Show();
            base.OnFormClosing(e);
        }
    }
}
