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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Esti
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class DatabaseHelpers
        {
            string connString = "Host=localhost;Username=postgres;Password=1234;Database=pariwisata";
            NpgsqlConnection conn;

            public void connect()
            {
                if (conn == null)
                {
                    conn = new NpgsqlConnection(connString);
                }
                conn.Open();
            }

            public DataTable getData(string sql)
            {
                DataTable table = new DataTable();
                connect();
                try
                {
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    conn.Close();
                }
                return table;
            }

            public void exc(String sql)
            {
                connect();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch
                {

                }
                finally
                {
                    conn.Close();
                }
            }
        }
        DatabaseHelpers db = new DatabaseHelpers();

        private void Form1_Load(object sender, EventArgs e)
        {
            DataPariwisata();
        }

        private void DataPariwisata()
        {
            string sql = "SELECT * FROM pariwisata";
            dataGridView1.DataSource = db.getData(sql);

            dataGridView1.Columns["id_tiket"].HeaderText = "ID Ticket";
            dataGridView1.Columns["nama_pengunjung"].HeaderText = "Nama Pengunjung";
            dataGridView1.Columns["nama_tempat"].HeaderText = "Nama Tempat";
            dataGridView1.Columns["jumlah_tiket"].HeaderText = "Jumlah Ticket";
            dataGridView1.Columns["biaya"].HeaderText = "Biaya";
            dataGridView1.Columns["Edit"].DisplayIndex = 6;
            dataGridView1.Columns["Delete"].DisplayIndex = 6;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("APAKAH ANDA YAKIN?", "INSERT PARIWISATA", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string sql = $"INSERT INTO pariwisata(id_tiket, nama_pengunjung, nama_tempat, jumlah_tiket, biaya) VALUES ({textBox1.Text},'{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}')";
                MessageBox.Show(sql);
                db.exc(sql);
                DataPariwisata();
                button3.PerformClick();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Gagal!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
            {
                button1.Enabled = false;
                textBox1.Enabled = false;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["id_tiket"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["nama_pengunjung"].Value.ToString();
                textBox3.Text= dataGridView1.Rows[e.RowIndex].Cells["nama_tempat"].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["jumlah_tiket"].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["biaya"].Value.ToString();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                var id_ticket = dataGridView1.Rows[e.RowIndex].Cells["id_tiket"].Value.ToString();
                string sql = $"delete from pariwisata where id_tiket = {id_ticket}";

                DialogResult dialogResult = MessageBox.Show("APAKAH ANDA YAKIN?", "DELETE PARIWISATA", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show("Berhasil!");
                    db.exc(sql);
                    DataPariwisata();
                    button3.PerformClick();
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Gagal!");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("APAKAH ANDA YAKIN?", "UPDATE PARIWISATA", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string sql = $"update pariwisata set nama_pengunjung = '{textBox2.Text}', nama_tempat = '{textBox3.Text}', jumlah_tiket = '{textBox4.Text}', biaya = '{textBox5.Text}' where id_tiket = {textBox1.Text}";
                MessageBox.Show("Berhasil!");
                db.exc(sql);
                DataPariwisata();
                button3.PerformClick();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Gagal!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            button1.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            textBox4.Text = "";
            textBox5.Text = "";

        }
    }
}
