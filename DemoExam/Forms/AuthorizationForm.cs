using MySql.Data.MySqlClient;
using OTKInformationSystem.Forms;
using OTKInformationSystem.Models;

namespace OTKInformationSystem
{
    public partial class AuthorizationForm : Form
    {
        private MySqlConnection connection;

        public AuthorizationForm()
        {
            InitializeComponent();

            MinimumSize = new Size(450, 200);
            MaximumSize = new Size(500, 250);

            var connectionString = "Server=localhost;Database=uchpr;port=3306;User id=sa;password=root";
            connection = new MySqlConnection(connectionString);
        }

        private void ShowPassword(object sender, EventArgs e)
        {
            if (showPasswordCheckBox.Checked) passwordTextBox.UseSystemPasswordChar = false;
            else passwordTextBox.UseSystemPasswordChar = true;
        }

        /// <summary>
        /// После успешного входа форма авторизации уходит в скрытый режим, избежание утечек памяти реализовано в OrderFormationForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogIn(object sender, EventArgs e)
        {
            connection.Open();
            var query =
                $"SELECT * FROM employee " +
                $"WHERE login = {loginTextBox.Text} " +
                $"AND password = {passwordTextBox.Text}";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            Employee result = null;

            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                result = new Employee
                {
                    Id = dataReader.GetInt16(0),
                    Name = dataReader.GetString(1),
                };
            }

            dataReader.Close();

            if (result != null)
            {
                new OrdersForm(this, result).Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Логин или пароль введены неверно");
            }

            connection.Close();
        }
    }
}
