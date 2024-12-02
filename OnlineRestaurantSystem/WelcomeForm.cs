using System;
using System.Windows.Forms;

namespace OnlineRestaurantSystem
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            // Timer başlatılıyor (Interval: 5000 ms yani 5 saniye)
            timer1.Interval = 5000; // Eğer Designer'da ayarlamadıysanız buradan da ayarlayabilirsiniz
            timer1.Enabled = true; // Timer çalışmaya başlıyor
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Timer süresi dolunca giriş formuna yönlendirme
            timer1.Stop(); // Timer durduruluyor
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Show(); // LoginForm açılıyor
             // WelcomeForm kapanıyor
        }
    }
}
