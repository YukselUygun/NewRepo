using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OnlineRestaurantSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Veritabanı bağlantısı
            SqlConnection con = new SqlConnection(@"Server=YUKSELPC\SQLEXPRESS;Database=OnlineRestaurantOrderTrackingSystem;Integrated Security=True;");
            con.Open();

            // Kullanıcı adı ve şifreyi alıyoruz
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Kullanıcı adı ve şifreyi sorguluyoruz ve rol bilgisini de alıyoruz
            SqlCommand cmd = new SqlCommand("SELECT name, password, rol FROM Users WHERE name=@username AND password=@password", con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Kullanıcı bulunursa
            if (dt.Rows.Count > 0)
            {
                // Kullanıcı rolünü alıyoruz
                string rol = dt.Rows[0]["rol"].ToString();

                // Eğer rol "müşteri" ise CustomerForm'a yönlendir
                if (rol == "Müşteri")
                {
                    MessageBox.Show("Giriş Başarılı! Müşteri formuna yönlendiriliyorsunuz.");
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.Show();
                    this.Hide();  // Login formu gizliyoruz
                }
                else
                {
                    MessageBox.Show("Giriş Başarılı! Ancak şu an müşteri rolünde değilsiniz.");
                    // Eğer müşteri dışında bir rolse, uygun formu açabilirsiniz.
                    // Örneğin: Yönetici ya da Çalışan için yönlendirme yapılabilir.
                }
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre");
            }
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(); // Yeni bir kayıt formu oluştur
            registerForm.Show(); // Kayıt formunu göster
            this.Hide();
        }


    }
}