using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=SAADET\\SQLEXPRESS01;Initial Catalog=DbDirectory;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*From TblPeople", connection);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void temizle()
        {
            TxtID.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MTxtTel.Text = "";
            TxtMail.Text = "";
            TxtAd.Focus();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Insert into TblPeople (Name,Surname,Phone,Email) values (@p1,@p2,@p3,@p4)", connection);
            command.Parameters.AddWithValue("@p1", TxtAd.Text);
            command.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            command.Parameters.AddWithValue("@p3", MTxtTel.Text);
            command.Parameters.AddWithValue("@p4", TxtMail.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Kişi sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MTxtTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            connection.Open();
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Kişi Rehberden Silinsin mi?", "UYARI", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes)
            {
                SqlCommand command= new SqlCommand("Delete from TblPeople where ID=" + TxtID.Text,connection);
                command.ExecuteNonQuery();
            }
            else

            {

                MessageBox.Show("Kişi Rehberden Silinmedi");

            }
            connection.Close();
            listele();
            temizle();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update TblPeople set  Name=@p1,Surname=@p2,Phone=@p3,Email=@p4 where ID=@p5",connection);
            command.Parameters.AddWithValue("@p1", TxtAd.Text);
            command.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            command.Parameters.AddWithValue("@p3",MTxtTel.Text);
            command.Parameters.AddWithValue("@p4",TxtMail.Text);
            command.Parameters.AddWithValue("@P5",TxtID.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Kişi Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
