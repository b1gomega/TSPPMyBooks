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
    public partial class SearchXXVariantForm : Form {
        private Form prev_form;
        private bool ShowPrevForm = true;
        public SearchXXVariantForm(Form PrevForm){
            InitializeComponent();
            prev_form = PrevForm;

            OuterDesign.ChangeForm(this);
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            //Lines
            using (Graphics g = Graphics.FromHwnd(this.Handle)) {
                using (Pen p = new Pen(OuterDesign.TextAndBorderColor, 1)) {
                    g.DrawLine(p, 12, 59, ClientSize.Width - 12, 59);
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SearchXXVariantForm_Load(object sender, EventArgs e)
        {

        }

        //Кнопка скасувати
        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
        //Кнопка знайти
        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text == "") {
                MessageBox.Show("Не введено дані");
                return;
            }
            BookLibrary bookLibrary = new BookLibrary();
            MyBooks.BookLibrary.YearForSearch = Convert.ToInt32(textBox1.Text);
            MyBooks.BookLibrary.VariantForSearch = 2;

            ShowPrevForm = false;
            this.Close();
            SearchResultForm searchResultForm = new SearchResultForm(prev_form);
            searchResultForm.Show();
        }       
        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (!prev_form.IsDisposed && ShowPrevForm) prev_form.Show();
            base.OnFormClosing(e);
        }
    }
}
