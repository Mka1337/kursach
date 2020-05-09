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

namespace WindowsFormsApp1
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Point lastPoint;

        private void Register_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (nameField.Text == "")
            {
                MessageBox.Show("Name is empty");
                return;
            }

            if (surnameField.Text == "")
            {
                MessageBox.Show("Surname is empty");
                return;
            }

            if (loginField.Text == "")
            {
                MessageBox.Show("Login is empty");
                return;
            }

            if (passField.Text == "")
            {
                MessageBox.Show("Password is empty");
                return;
            }

            if (isUserExist())
                return;

            

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`Login`, `Password`, `Name`, `Surname`) VALUES (@login, @password, @Name, @Surname)", db.GetConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = nameField.Text;
            command.Parameters.Add("@Surname", MySqlDbType.VarChar).Value = surnameField.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Account was successfully added");
            else
                MessageBox.Show("Account was not added");

            db.closeConnection();
        }

        public Boolean isUserExist()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `Login` = @uL", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;
            
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("This profile is exist");
                return true;
            }

            else
                return false;
        }

        private void AutorizationButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
