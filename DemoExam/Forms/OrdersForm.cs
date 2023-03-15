using MySql.Data.MySqlClient;
using OTKInformationSystem.Models;

namespace OTKInformationSystem.Forms
{
    public partial class OrdersForm : Form
    {
        private AuthorizationForm authorizationForm;
        private bool closedByLogOut = false;
        private int discount;
        private Employee employee;
        private MySqlConnection connection;
        private List<Service> services = new List<Service>();
        private List<Client> clients = new List<Client>();
        private List<Order> orders = new List<Order>();

        public OrdersForm(AuthorizationForm authorizationForm, Employee employee)
        {
            this.authorizationForm = authorizationForm;
            this.employee = employee;

            var connectionString = "Server=localhost;Database=uchpr;port=3306;User id=sa;password=root";
            connection = new MySqlConnection(connectionString);

            InitializeComponent();

            MinimumSize = new Size(560, 565);
            MaximumSize = new Size(850, 600);

            GetServices();
            GetClients();
            GetOrders();

            welcomeLabel.Text = $"Добро пожаловать, {employee.Name}";
            logOutButton.Location = new Point(logOutButton.Location.X + welcomeLabel.Width, logOutButton.Location.Y);
        }

        private void GetServices()
        {
            connection.Open();
            var query =
                $"SELECT * FROM service";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            services.Clear();
            while (dataReader.Read())
            {
                services.Add(new Service
                {
                    Id = dataReader.GetInt16("id"),
                    Name = dataReader.GetString("name"),
                    Price = dataReader.GetFloat("price")
                });
            }

            dataReader.Close();
            connection.Close();

            serviceComboBox.DataSource = services;
            serviceComboBox.ValueMember = "Name";
        }

        private void GetClients()
        {
            connection.Open();
            var query =
                $"SELECT * FROM client";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            clients.Clear();
            while (dataReader.Read())
            {
                clients.Add(new Client
                {
                    Id = dataReader.GetInt16("id"),
                    Name = dataReader.GetString("name"),
                    Adress = dataReader.GetString("address"),
                    PhoneNumber = dataReader.GetString("phone")
                });
            }

            dataReader.Close();
            connection.Close();

            clientComboBox.DataSource = clients;
            clientComboBox.ValueMember = "Name";
        }

        private void GetOrders()
        {
            connection.Open();
            var query =
                $"SELECT orders.id, orders.date, orders.discount, orders.state, client.name, employee.name, service.name FROM orders " +
                $"JOIN client on client_code = client.id " +
                $"JOIN employee on employee_code = employee.id " +
                $"JOIN service on service_code = service.id; ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            orders.Clear();
            while (dataReader.Read())
            {
                orders.Add(new Order
                {
                    Id = dataReader.GetInt16(0),
                    Date = dataReader.GetString(1),
                    Discount = dataReader.GetInt16(2),
                    Status = dataReader.GetString(3),
                    ClientName = dataReader.GetString(4),
                    EmployeeName = dataReader.GetString(5),
                    ServiceName = dataReader.GetString(6)
                });
            }

            dataReader.Close();
            connection.Close();

            ordersDataGridView.DataSource = null;
            ordersDataGridView.DataSource = orders;

            ordersDataGridView.Update();
            ordersDataGridView.Refresh();
        }

        /// <summary>
        /// Рассчитывает цену с учётом скидки, здесь же проводит валидацию скидки и реагирует на изменение её значения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountPrice(object sender, EventArgs e)
        {
            try
            {
                discount = int.Parse(discountTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Укажите число в поле скидки");
                discount = 0;
                discountTextBox.Text = "0";
            }
            if (discount > 100 || discount < 0)
            {
                MessageBox.Show("Скидка должны быть числом в диапазоне от 0 до 100");
                discount = 0;
                discountTextBox.Text = "0";
            }
            var price = (serviceComboBox.SelectedItem as Service)?.Price;
            countedPriceLabel.Text = (price - price * discount / 100).ToString();
        }

        private void AddOrder(object sender, EventArgs e)
        {
            CountPrice(sender, e);
            connection.Open();
            var query =
                $"INSERT INTO orders(date, discount, state, client_code, employee_code, service_code) " +
                $"VALUES('{dateTimePicker.Value.ToString("yyyyMMdd")}', {discount}, 'в работе', {((Client)clientComboBox.SelectedItem).Id}, {employee.Id}, {((Service)serviceComboBox.SelectedItem).Id})";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            connection.Close();

            GetOrders();
        }

        private void DeleteOrder(object sender, EventArgs e)
        {
            connection.Open();
            var query = $"INSERT INTO orders_backup(date, discount, state, client_code, employee_code, service_code)" +
                $" SELECT date, discount, 'закрыт', client_code, employee_code, service_code" +
                $" FROM orders WHERE id = {((Order)ordersDataGridView.CurrentRow.DataBoundItem).Id}; " +
                $"DELETE FROM orders WHERE id = {((Order)ordersDataGridView.CurrentRow.DataBoundItem).Id};";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            connection.Close();

            GetOrders();
        }

        private void EditOrder(object sender, EventArgs e)
        {
            connection.Open();
            var query =
                $"UPDATE orders " +
                $"SET date = '{dateTimePicker.Value.ToString("dd.MM.yyyy")}', discount = {discount}, " +
                $"client_code = {((Client)clientComboBox.SelectedItem).Id}, employee_code = {employee.Id}, service_code = {((Service)serviceComboBox.SelectedItem).Id} " +
                $"WHERE id = {((Order)ordersDataGridView.CurrentRow.DataBoundItem).Id}";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            connection.Close();

            GetOrders();
        }

        /// <summary>
        /// Возврат на окно авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOut(object sender, EventArgs e)
        {
            closedByLogOut = true;
            this.Close();
        }

        /// <summary>
        /// Действие при закрытии, необходим для избежания утечек памяти
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClose(object sender, FormClosingEventArgs e)
        {
            if (closedByLogOut) authorizationForm.Show();
            else authorizationForm.Close();
        }
    }
}
