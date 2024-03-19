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

        private void button_pb7_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, media, rezultat, oras from Admitere order by oras, nume, prenume");
        }

        private void button_pb9_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, media, oras from Admitere where media = (select max(media) from Admitere as x where x.oras = Admitere.oras) order by oras");
        }

        private void button_pb10_1_Click(object sender, EventArgs e)
        {
            select("select id, nume, prenume, sex, proba1, proba2, media, datan, rezultat from Admitere where oras = 'Cluj' and rezultat = 'ADMIS' order by media desc, nume, prenume");
        }

        private void button_pb10_2_Click(object sender, EventArgs e)
        {
            select("select id, nume, prenume, sex, proba1, proba2, media, datan, rezultat from Admitere where oras = 'Cluj' and rezultat = 'RESPINS' order by media desc, nume, prenume");
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
            select("select nume, prenume, media from Admitere where media>=9.75 and media<=10 order by nume, prenume");
        }

        private void button_13_2_Click(object sender, EventArgs e)
        {
            select("select nume, prenume, media from Admitere where media>=8.50 and media<=9.74 order by nume, prenume");
        }
    }
}
