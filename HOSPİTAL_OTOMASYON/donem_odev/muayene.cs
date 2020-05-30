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
    public partial class muayene : Form
    {
        public SqlConnection con = new SqlConnection(@"Data Source =DESKTOP-QNLOUJ0; Initial Catalog =donem_odev; Integrated Security = True");
        SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr;
        public SqlDataReader dr1;
        public SqlDataReader dr2;
        public muayene()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void muayene_Load(object sender, EventArgs e)
        {
            ComboBox();
        }
        public void ComboBox()
        {
            DataTable tb = new DataTable("hastalar");
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from hastalar ", con);

            dr = cmd.ExecuteReader();

            tb.Load(dr);

            comboBox1.DisplayMember = "ad";
            comboBox1.ValueMember = "hastaid";
            comboBox1.DataSource = tb;
            comboBox1.Text = "";
            con.Close();

            DataTable tb1 = new DataTable("doktor");

            con.Open();

            SqlCommand cmd1 = new SqlCommand("select * from doktor ", con);

            dr1 = cmd1.ExecuteReader();
            tb1.Load(dr1);
            comboBox2.DisplayMember = "ad";
            comboBox2.ValueMember = "doktorid";
            comboBox2.DataSource = tb1;
            comboBox2.Text = "";
            con.Close();

            DataTable tb2 = new DataTable("muayeneler");

            con.Open();
            SqlCommand cmd2 = new SqlCommand("select * from muayeneler ", con);

            dr2 = cmd2.ExecuteReader();
            tb2.Load(dr2);
            comboBox3.DisplayMember = "muayeneid";
            comboBox3.ValueMember = "muayeneid";
            comboBox3.DataSource = tb2;
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
                string kayit = "insert into muayeneler(Giristarih,sikayet,hastaid,doktorid) values (@gt,@sikayet,@hasta,@dok)";

                SqlCommand komut = new SqlCommand(kayit, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@gt", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@sikayet", textBox1.Text);
                komut.Parameters.AddWithValue("@hasta",comboBox1.SelectedValue);
                komut.Parameters.AddWithValue("@dok", comboBox2.SelectedValue);
              

                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("muayene Kayıt İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into tahliller(tahliladi,tahlilsonuc,teshis,receteler,muayeneid,cikistarih) values (@ad,@sonuc,@teshis,@recete,@muayene,@ct)";

                SqlCommand komut = new SqlCommand(kayit, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@sonuc", textBox3.Text);
                komut.Parameters.AddWithValue("@teshis",textBox4.Text);
                komut.Parameters.AddWithValue("@recete", textBox5.Text);
                komut.Parameters.AddWithValue("@muayene", comboBox3.SelectedValue);
                komut.Parameters.AddWithValue("@ct", dateTimePicker2.Value);

                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("muayene sonuc Kayıt İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

    }
}
    
