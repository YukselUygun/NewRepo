using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineRestaurantSystem
{
    public partial class CustomerForm : Form
    {
        private string connectionString = "Server=YUKSELPC\\SQLEXPRESS;Database=OnlineRestaurantOrderTrackingSystem;Trusted_Connection=True;";

        public CustomerForm()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            // Veritabanından kategorileri yükleme
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT categoryID, categoryName FROM Categories";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int categoryId = reader.GetInt32(0);
                    string categoryName = reader.GetString(1);

                    // Kategoriye uygun butonu bul ve etkinleştir
                    foreach (Control control in flowLayoutPanelCategories.Controls)
                    {
                        if (control is Button button && button.Text == categoryName)
                        {
                            button.Tag = categoryId;
                            button.Click += CategoryButton_Click;
                            break;
                        }
                    }
                }
            }
        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int categoryId = (int)clickedButton.Tag;

            // Seçilen kategoriye ait ürünleri yükle
            LoadProducts(categoryId);
        }

        private void LoadProducts(int categoryId)
        {
            // Önceki ürünleri temizle
            ProductPanel.Controls.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT productName, description, price, imageURL FROM Products WHERE categoryID = @categoryID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@categoryID", categoryId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string productName = reader.GetString(0);
                    string description = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);
                    string imageURL = reader.GetString(3);

                    // Ürün paneli oluştur
                    Panel productPanel = new Panel
                    {
                        Width = 200,
                        Height = 300,
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    // Ürün resmi
                    // Ürün resmi
                    PictureBox pictureBox = new PictureBox
                    {
                        Name = "pictureBox",
                        Width = 180,
                        Height = 120,
                        Location = new Point(10, 10),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    try
                    {
                        pictureBox.Load(imageURL); // Veritabanından alınan resim yükleniyor
                    }
                    catch
                    {
                        pictureBox.Image = null; // Varsayılan resim yok
                    }

                    productPanel.Controls.Add(pictureBox);


                    productPanel.Controls.Add(pictureBox);

                    // Ürün ismi
                    Label lblName = new Label
                    {
                        Name = "lblName",
                        Text = productName,
                        AutoSize = false,
                        Width = 180,
                        Location = new Point(10, 140),
                        Font = new Font("Arial", 10, FontStyle.Bold)
                    };
                    productPanel.Controls.Add(lblName);

                    // Açıklama
                    Label descriptionLabel = new Label
                    {
                        Text = description,
                        AutoSize = false,
                        Width = 180,
                        Location = new Point(10, 160),
                        Font = new Font("Arial", 8, FontStyle.Regular)
                    };
                    productPanel.Controls.Add(descriptionLabel);

                    // Fiyat
                    Label lblPrice = new Label
                    {
                        Name = "lblPrice",
                        Text = $"Fiyat: {price:C}",
                        AutoSize = false,
                        Width = 180,
                        Location = new Point(10, 200),
                        Font = new Font("Arial", 9, FontStyle.Regular),
                        ForeColor = Color.Green
                    };
                    productPanel.Controls.Add(lblPrice);

                    // Sepete ekle butonu
                    Button addToCartButton = new Button
                    {
                        Text = "Sepete Ekle",
                        Width = 120,
                        Height = 30,
                        Location = new Point(40, 240)
                    };
                    addToCartButton.Click += (s, e) =>
                    {
                        MessageBox.Show($"{productName} sepete eklendi!");
                    };
                    productPanel.Controls.Add(addToCartButton);

                    // Ürün panelini FlowLayoutPanel'e ekle
                    ProductPanel.Controls.Add(productPanel);
                }
            }
        }

      
    }
}
