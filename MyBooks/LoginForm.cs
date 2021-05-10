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
    public partial class LoginForm : Form {
        private Form prev_form;
        public LoginForm(Form PrevForm) {
            InitializeComponent();
            prev_form = PrevForm;
            OuterDesign.ChangeForm(this);
            this.CancelButton = button1;
            this.AcceptButton = button2;
            textBox2.PasswordChar = '*';
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
        private void Autorization_Load(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //Кнопка скасувати
        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }
        //Кнопка OK
        private void button2_Click(object sender, EventArgs e) {
            string Login, Password;
            if(textBox1.Text == "" || textBox1.Text == "") {
                MessageBox.Show("Не введено дані!");
                return;
            }
            Login = textBox1.Text;
            Password = textBox2.Text;
            //Доделать минус пробелы
            Login autorization = new Login();

            if(!autorization.CheckLoginAndPassword(Login, Password)) {
                MessageBox.Show("Неправильний логін або пароль!!!");
                return;
            }
            else {
                //MessageBox.Show("Успішно ввійшли в систему як адміністратор");              
                StartMenuAfterAutorizationForm f = new StartMenuAfterAutorizationForm();
                f.Show();
                prev_form.Close();
                this.Close();
            }
        }        
        private void textBox1_TextChanged(object sender, EventArgs e) {
             
        }
        private void textBox2_TextChanged(object sender, EventArgs e) {

        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (!prev_form.IsDisposed) prev_form.Show();
            base.OnFormClosing(e);
        }
    }
}
