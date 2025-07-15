using System;
using System.Data.OleDb;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace TP_MESERP_Core.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string hashed = SHAHelper.ComputeSHA256Hash(password);

            using (var conn = AccessConnectionHelper.GetConnection())
            {
                conn.Open();
                var cmd = new OleDbCommand("SELECT * FROM Users WHERE Username = ? AND PasswordHash = ?", conn);
                cmd.Parameters.AddWithValue("?", username);
                cmd.Parameters.AddWithValue("?", hashed);

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string ip = Dns.GetHostEntry(Dns.GetHostName())
                        .AddressList
                        .FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                        .ToString() ?? "127.0.0.1";

                    string pc = Environment.MachineName;

                    MessageBox.Show($"Güncelle: IP={ip}, PC={pc}, ID={reader["UserID"]}");

                    var update = new OleDbCommand("UPDATE Users SET LastLogin=?, IPAddress=?, ComputerName=? WHERE UserID=?", conn);

                    // Parametreleri açıkça tür belirterek ekliyoruz
                    update.Parameters.Add("LastLogin", OleDbType.Date).Value = DateTime.Now;
                    update.Parameters.Add("IPAddress", OleDbType.VarWChar).Value = ip;
                    update.Parameters.Add("ComputerName", OleDbType.VarWChar).Value = pc;
                    update.Parameters.Add("UserID", OleDbType.Integer).Value = Convert.ToInt32(reader["UserID"]);

                    update.ExecuteNonQuery();


                    MessageBox.Show("Giriş Başarılı!");

                    // Buraya MainView çağırabilirsin
                }
                else
                {
                    MessageBox.Show("Hatalı kullanıcı adı veya şifre.");
                }
            }
        }

        private string GetLocalIP()
        {
            string ip = Dns.GetHostEntry(Dns.GetHostName())
                           .AddressList[0]
                           .ToString();
            return ip;
        }
    }
}
