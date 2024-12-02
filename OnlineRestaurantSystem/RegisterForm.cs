using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OnlineRestaurantSystem
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {

            
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string adres = txtAdres.Text.Trim();
            string rol = cmbRoller.SelectedItem?.ToString();

          
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(telefon) ||
                string.IsNullOrEmpty(adres) || string.IsNullOrEmpty(rol))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

        
            using (SqlConnection con = new SqlConnection(@"Server=YUKSELPC\SQLEXPRESS;Database=OnlineRestaurantOrderTrackingSystem;Integrated Security=True;"))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO Users (name, eMail, password, phone, address, rol) VALUES (@username, @email, @password, @telefon, @adres, @rol)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@telefon", telefon);
                        cmd.Parameters.AddWithValue("@adres", adres);
                        cmd.Parameters.AddWithValue("@rol", rol);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Kayıt başarı ile tamamlandı. Giriş yapabilirsiniz.");

                            btnLogin.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Kayıt sırasında bir hata oluştu. Lütfen tekrar deneyin.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            try
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

       
    }
}
