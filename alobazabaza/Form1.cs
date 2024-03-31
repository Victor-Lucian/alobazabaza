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

namespace alobazabaza
{
    public partial class Form1 : Form
    {
        SqlConnection myConnection;
        SqlDataAdapter myAdapter;
        DataTable myTable;
        SqlCommand q;

        public void openConnection()
        {
            string myConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Admitere.mdf;Integrated Security=True;Connect Timeout=30";
            myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
        }

        private void select(string qs)
        {
            myAdapter = new SqlDataAdapter();
            myTable = new DataTable();
            myTable.Clear();
            myTable.Columns.Clear();
            admitereDataGridView.Columns.Clear();
            q = new SqlCommand(qs, myConnection);
            myAdapter.SelectCommand = q;
            myAdapter.Fill(myTable);
            admitereDataGridView.DataSource = myTable;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openConnection();

            q = new SqlCommand("update Admitere set rezultat = ' ', media = 0", myConnection);
            q.ExecuteNonQuery();

            select("select * from Admitere");

        }

        private void button_pb0_Click(object sender, EventArgs e)
        {
            q = new SqlCommand("update Admitere set rezultat = 'RESPINS', media = (proba1 + proba2 - 0.01) / 2", myConnection);
            q.ExecuteNonQuery();

            q = new SqlCommand("update Admitere set rezultat = 'ADMIS' where id in ( select top 20 id from Admitere where (proba1>=5) and (proba2>=5) order by media desc)", myConnection);
            q.ExecuteNonQuery();
            select("select * from Admitere");
        }

        private void button_pb4_1_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, proba1, rezultat from Admitere order by proba1 desc");
        }

        private void button_pb4_2_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, proba2, rezultat from Admitere order by proba2 desc");
        }

        private void button_pb5_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, media, rezultat from Admitere order by nume, prenume");
        }

        private void button_pb6_Click(object sender, EventArgs e)
        {
            string s = ("select count(*) from Admitere");
            q = new SqlCommand(s, myConnection);
            int nrtot = Convert.ToInt32(q.ExecuteScalar());

            s = ("select count(*) from Admitere where media <= 5");
            q = new SqlCommand(s, myConnection);
            int nr5 = Convert.ToInt32(q.ExecuteScalar());
            textBox1.Text = (nr5 * 100) / nrtot + "%";

            s = ("select count(*) from Admitere where media > 5 and media <= 7");
            q = new SqlCommand(s, myConnection);
            int nr7 = Convert.ToInt32(q.ExecuteScalar());
            textBox2.Text = (nr7 * 100) / nrtot + "%";

            s = ("select count(*) from Admitere where media > 7 and media <= 9");
            q = new SqlCommand(s, myConnection);
            int nr9 = Convert.ToInt32(q.ExecuteScalar());
            textBox3.Text = (nr9 * 100) / nrtot + "%";

            s = ("select count(*) from Admitere where media > 9");
            q = new SqlCommand(s, myConnection);
            int nr10 = Convert.ToInt32(q.ExecuteScalar());
            textBox4.Text = (nr10 * 100) / nrtot + "%";
        }

        private void button_pb7_a_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, media, rezultat, oras from Admitere order by oras, nume, prenume");
        }

        private void button_pb9_1_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, sex, proba1, proba2, media, datan, rezultat from Admitere where rezultat = 'ADMIS' and oras = 'CLUJ' order by media");
        }

        private void button_pb10_Click(object sender, EventArgs e)
        {
            q = new SqlCommand("update Admitere set proba1= '" + textBox7.Text + "' where nume= '" + textBox5.Text + "' and prenume= '" + textBox6.Text + "' ", myConnection);
            q.ExecuteNonQuery();

            q = new SqlCommand("update Admitere set proba2= '" + textBox10.Text + "' where nume= '" + textBox8.Text + "' and prenume= '" + textBox9.Text + "' ", myConnection);
            q.ExecuteNonQuery();

            q = new SqlCommand("update Admitere set rezultat = 'ADMIS' where id in (select top 20 id from Admitere where (proba1 >= 5) and (proba2 >= 5) order by media desc)", myConnection);
            q.ExecuteNonQuery();

            select("select * from Admitere");
        }

        private void button_pb11_Click(object sender, EventArgs e)
        {
            select("select top 4 nume, prenume, oras, media from Admitere where oras != 'Brasov' order by media desc, proba1");
        }

        private void button_12_1_Click(object sender, EventArgs e)
        {
            select("select top 2 nume, prenume, oras, media from Admitere where sex = 'M' and oras !='Brasov' and rezultat = 'ADMIS' order by media desc, proba1");
        }

        private void button_12_2_Click(object sender, EventArgs e)
        {
            select("select top 3 nume, prenume, oras, media from Admitere where sex = 'F' and oras !='Brasov' and rezultat = 'ADMIS' order by media desc, proba1");
        }

        private void button_13_1_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, media from Admitere where (media>9.74) order by nume, prenume");
        }

        private void button_13_2_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, media from Admitere where (media>8.49) and (media<9.75) order by nume, prenume");
        }

        private void button_pb1_f_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, rezultat, media from Admitere where sex = 'F' order by media desc");
        }

        private void button_pb1_b_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, rezultat, media from Admitere where sex = 'M' order by media desc");
        }

        private void button_pb2_max_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, media, datan, oras from Admitere where id in (select top 5 id from Admitere where rezultat = 'ADMIS' order by media desc) order by media desc");
        }

        private void button_pb2_min_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, media, datan, oras from Admitere where id in (select top 5 id from Admitere where rezultat = 'ADMIS' order by media) order by media");
        }

        private void button_pb3_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, oras, datan, media from Admitere where rezultat = 'ADMIS' and dateadd(year, 20, datan) >= getdate() order by datan, nume");
        }

        private void button_pb7_b_Click(object sender, EventArgs e)
        {
            select("select rank() OVER(order by Oras,Nume,Prenume) as NR,NUME,PRENUME,MEDIA,REZULTAT from Admitere where Oras='" + textBoxOras.Text + "' order by Nume,Prenume");
        }

        private void button_pb8_Click(object sender, EventArgs e)
        {
            select("select Nume, Prenume, Media, Oras from Admitere where media = (select max(media) from Admitere as Adm where Admitere.Oras = Adm.Oras) order by Oras");
        }

        private void button_pb9_2_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, sex, proba1, proba2, media, datan, rezultat from Admitere where rezultat = 'RESPINS' and oras = 'CLUJ' order by media");
        }

        private void button_pb14_1_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, datan, oras from admitere where rezultat = 'RESPINS' and (" + DateTime.Now.Year + " - Year(datan) >= 20) order by nume, prenume");
        }

        private void button_pb14_2_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, datan, oras from admitere where NOT(rezultat = 'RESPINS' and (" + DateTime.Now.Year + " - Year(datan) >= 20)) order by nume, prenume");
        }

        private void button_pb15_Click(object sender, EventArgs e)
        {
            q = new SqlCommand("select count(*) from admitere where Oras = '" + textBoxOras.Text + "'", myConnection);
            int cnt = Convert.ToInt32(q.ExecuteScalar());
            q = new SqlCommand("select count(*) from admitere where Oras = '" + textBoxOras.Text + "' and Rezultat = 'ADMIS'", myConnection);
            int nr_admisi = Convert.ToInt32(q.ExecuteScalar());
            int procentaj = nr_admisi * 100 / cnt;
            textBoxProcentaj.Text = "Procentaj admisi: " + procentaj + "%, NumarTotalCandidati";
        }

        private void button_pb16_Click(object sender, EventArgs e)
        {
            q = new SqlCommand("select avg(media) from admitere where rezultat = 'ADMIS'");
            double media_tot = Convert.ToDouble(q.ExecuteScalar());

            q = new SqlCommand("select avg(proba1) from admitere where rezultat = 'ADMIS'");
            double media_1 = Convert.ToDouble(q.ExecuteScalar());

            q = new SqlCommand("select avg(proba2) from admitere where rezultat = 'ADMIS'");
            double media_2 = Convert.ToDouble(q.ExecuteScalar());

            /// trebuie sa faci afisarea
        }
    }
}
