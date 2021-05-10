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
    public partial class BooksOutputForm : Form {
        private Form prev_form;
        public BooksOutputForm(Form PrevForm) {
            InitializeComponent();
            OuterDesign.ChangeForm(this);
            
            prev_form = PrevForm;
            this.AcceptButton = button1;
            ShowBooks();
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
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //Кнопка ОК
        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }
        public void ShowBooks() {
            MySQL mysql = new MySQL();
            mysql.openConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT name FROM `bookslibrarytable`", mysql.getConnection());
            MySqlDataReader dr = command.ExecuteReader();
            while (dr.Read()) {
                listBox1.Items.Add(dr["name"]);
            }
            dr.Close();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            mysql.closeConnection();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void BooksOutputForm_Load(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (!prev_form.IsDisposed) prev_form.Show();
            base.OnFormClosing(e);
        }

        private void button2_Click(object sender, EventArgs e) {

        }

    }
}
