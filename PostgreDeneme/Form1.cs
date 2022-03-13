using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace PostgreDeneme
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection connection;
        private int id = -1;
        public Form1()
        {
            InitializeComponent();

            connection = new NpgsqlConnection("server=localhost; port=5432; Database=Deneme; user Id=postgres; password=HARUNCAN67");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GridDoldur();
        }

        private void GridDoldur()
        {
            dataGridView1.DataSource = AdoVerileriCek();
        }

        public List<Tbl_Ogrenci> AdoVerileriCek()
        {
            connection.Open();

            string sorgu = "select * from tbl_ogrenci";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<Tbl_Ogrenci> ogrenciler = new List<Tbl_Ogrenci>();

            foreach (var item in ds.Tables[0].Rows)
            {
                DataRow dr = item as DataRow;
                ogrenciler.Add(new Tbl_Ogrenci
                {
                    Id = int.Parse(dr[0].ToString()),
                    Ad = dr[1].ToString(),
                    Soyad = dr[2].ToString()
                });
            }

            connection.Close();

            return ogrenciler;
        }

        private void BaglantiTercihi(object sender, EventArgs e)
        {
            if (efTercih.Checked == false && adoTercih.Checked == false)
            {
                return;
            }
            else if (efTercih.Checked == true)
            {
                adoTercih.Checked = false;
            }
            else if (adoTercih.Checked == true)
            {
                efTercih.Checked = false;
            }
        }

        private void Vt_Btns_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (adoTercih.Checked)
            {
                switch (btn.Name)
                {
                    case "kaydetBtn":
                        KaydetFonkAdo();
                        break;
                    case "guncelleBtn":
                        GuncelleFonkAdo();
                        break;
                    case "silBtn":
                        SilFonkAdo();
                        break;
                }
            }
            else if (efTercih.Checked)
            {
                switch (btn.Name)
                {
                    case "kaydetBtn":
                        KaydetFonkEf();
                        break;
                    case "guncelleBtn":
                        GuncelleFonkEf();
                        break;
                    case "silBtn":
                        SilFonkEf();
                        break;
                }
            }
        }

        /// <summary>
        ///             ENTITY FRAMEWORK (EntityFramework 6.0.0 ve üstü kabul etmiyor. Sıkıntı şu ki alt                  sürümleri de kabul etmiyor?)
        /// </summary>

        private void SilFonkEf()
        {
            //sürüm hatası
        }

        private void GuncelleFonkEf()
        {
            // sürüm hatası
        }

        private void KaydetFonkEf()
        {
            // sürüm hatası
        }

        /// <summary>
        ///             ADO 
        /// </summary>

        private void SilFonkAdo()
        {
            if ( id > 0 )
            {
                connection.Open();

                string sorgu = $"delete from tbl_ogrenci where id={id}";
                NpgsqlCommand komut = connection.CreateCommand();
                komut.CommandText = sorgu;
                komut.ExecuteNonQuery();

                connection.Close();
                Temizle();
                GridDoldur();
            }
            else
            {
                MessageBox.Show("Listeden seçim yapınız.");
            }
        }

        private void GuncelleFonkAdo()
        {
            if (id>0)
            {
                connection.Open();

                string sorgu = $"update tbl_ogrenci set ad='{adTxt.Text}',soyad='{soyadTxt.Text}' where id={id}";
                NpgsqlCommand komut = connection.CreateCommand();
                komut.CommandText = sorgu;
                komut.ExecuteNonQuery();

                connection.Close();
                Temizle();
                GridDoldur();
            }
            else
            {
                MessageBox.Show("Listeden seçim yapınız.");
            }
        }

        private void KaydetFonkAdo()
        {
            connection.Open();

            string sorgu = $"insert into tbl_ogrenci(ad,soyad) values('{adTxt.Text}','{soyadTxt.Text}')";
            NpgsqlCommand komut = connection.CreateCommand();
            komut.CommandText = sorgu;
            komut.ExecuteNonQuery();

            connection.Close();
            Temizle();
            GridDoldur();
        }

        private void Temizle()
        {
            id = -1;
            adTxt.Text = "";
            soyadTxt.Text = "";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count>0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow vr = dataGridView1.Rows[rowIndex];

                id = int.Parse(vr.Cells["Id"].Value.ToString());
                adTxt.Text = vr.Cells["Ad"].Value.ToString();
                soyadTxt.Text = vr.Cells["Soyad"].Value.ToString();
            }
        }
    }
}
