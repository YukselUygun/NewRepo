using System;
using System.Windows.Forms;

namespace OnlineRestaurantSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WelcomeForm()); // İlk açılan form WelcomeForm
        }
    }
}
