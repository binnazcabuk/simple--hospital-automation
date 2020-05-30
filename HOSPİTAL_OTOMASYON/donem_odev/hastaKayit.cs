using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace donem_odev
{
    public partial class hastaKayit : Form
    {
        public SqlConnection con = new SqlConnection(@"Data Source =DESKTOP-QNLOUJ0; Initial Catalog =hastane_otomasyon; Integrated Security = True");
        SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;
        public SqlDataReader dr1;
        public SqlDataReader dr2;
        public hastaKayit()
        {
            InitializeComponent();
        }

        private void hastaKayit_Load(object sender, EventArgs e)
        {
            ComboBox();
        }
        public void ComboBox()
        {
            DataTable tb = new DataTable("iller");
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from iller ORDER BY ilid ASC", con);

            dr = cmd.ExecuteReader();

            tb.Load(dr);

            comboBox2.DisplayMember = "iladi";
            comboBox2.ValueMember = "ilid";
            comboBox2.DataSource = tb;
            comboBox2.Text = "";
            con.Close();

            DataTable tb1 = new DataTable("ilceler");

            con.Open();

            SqlCommand cmd1 = new SqlCommand("select * from ilceler  ", con);

            dr1 = cmd1.ExecuteReader();
            tb1.Load(dr1);
            comboBox3.DisplayMember = "ilceadi";
            comboBox3.ValueMember = "ilceid";
            comboBox3.DataSource = tb1;
            comboBox3.Text = "";
            con.Close();


            DataTable tb2 = new DataTable("kangrubu");
            con.Open();
            SqlCommand cmd2 = new SqlCommand("select kangrubuid,kangrubuadi from kangrubu ", con);
            dr2 = cmd2.ExecuteReader();
            tb2.Load(dr2);
            comboBox1.DisplayMember = "kangrubuadi";
            comboBox1.ValueMember = "kangrubuid";
            comboBox1.DataSource = tb2;
            comboBox3.Text = "";
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into hastalar(ad,soyad,tckimlikno,kangrubuid,kayitilid,kayitilceid) values (@ad,@soyad,@tc,@kan,@il,@ilce)";

                SqlCommand komut = new SqlCommand(kayit, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@ad", textBox1.Text);
                komut.Parameters.AddWithValue("@soyad", textBox2.Text);
                komut.Parameters.AddWithValue("@tc", textBox3.Text);
                komut.Parameters.AddWithValue("@kan", comboBox1.SelectedValue);
                komut.Parameters.AddWithValue("@il", comboBox2.SelectedValue);
                komut.Parameters.AddWithValue("@ilce", comboBox3.SelectedValue);



                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("Hasta Kayıt İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
    }
}