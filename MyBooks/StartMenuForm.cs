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
    public partial class StartMenuForm : Form {
        public StartMenuForm() {
            InitializeComponent();
            OuterDesign.ChangeForm(this);
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);           
            //Lines
            using (Graphics g = Graphics.FromHwnd(this.Handle)) {
                using (Pen p = new Pen(OuterDesign.TextAndBorderColor, 1)) {
                    g.DrawLine(p, 12, 59, ClientSize.Width / 2 - 25, 59);
                    g.DrawLine(p, ClientSize.Width / 2 + 25, 59, ClientSize.Width - 12, 59);
                    g.DrawLine(p, ClientSize.Width / 2, 9, ClientSize.Width / 2, ClientSize.Height - 9);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e) {

        }

        //Кнопка показати всі книги
        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            BooksOutputForm booksOutputForm = new BooksOutputForm(this);
            booksOutputForm.Show();
        }
        //Кнопка автор Х назва У
        private void button2_Click(object sender, EventArgs e) {
            this.Hide();
            SearchXYVariantForm searchXYVariantForm = new SearchXYVariantForm(this);
            searchXYVariantForm.Show();           
        }
        //Кнопка ХХ рік видання  
        private void button3_Click(object sender, EventArgs e) {
            this.Hide();
            SearchXXVariantForm searchXXVariantForm = new SearchXXVariantForm(this);
            searchXXVariantForm.Show();            
        }
        private void button4_Click(object sender, EventArgs e) {
            this.Hide();
            LoginForm LogForm = new LoginForm(this);
            LogForm.Show();
        }
    }
}
