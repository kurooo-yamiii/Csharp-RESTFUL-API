using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Try_SQL
{
    public partial class Main : Form
    {
        private MySQLCommand mySQLCommand;
        public Main()
        {
            InitializeComponent();
            string connectionString = "server=localhost; port=1433; uid=your_username; password=your_password";
            mySQLCommand = new MySQLCommand(connectionString);
        }

        public void MyPopulate()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string query = "SELECT * FROM bim0kocn6y1cl68dqubs.item";
            DataTable dataTable = mySQLCommand.MyPopulate(query);

            foreach (DataRow row in dataTable.Rows)
            {
                dataGridView1.Rows.Add(row["code"], row["item"], row["price"], row["stock"], row["redeem"]);
            }
        }


        private void Main_Load(object sender, EventArgs e)
        {
            MyPopulate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyPopulate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT MAX(code) FROM bim0kocn6y1cl68dqubs.item";
            string maxCode = mySQLCommand.SelectSpecific(query);
            int convert = string.IsNullOrEmpty(maxCode) ? 1 : Convert.ToInt32(maxCode) + 1;

            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Please fill up the necessary information",
                "ROX Diamond Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    query = $"INSERT INTO bim0kocn6y1cl68dqubs.item (code, item, price, stock, redeem) VALUES('{convert}', '{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}')";
                    mySQLCommand.NonQuery(query);
                    MessageBox.Show("Added the Item Successfully",
                    "ROX Diamond Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyPopulate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message,
                    "ROX Diamond Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string code = row.Cells[0].Value.ToString();
                string item = row.Cells[1].Value.ToString();
                string price = row.Cells[2].Value.ToString();
                string stock = row.Cells[3].Value.ToString();
                string redeem = row.Cells[4].Value.ToString();

                label5.Text = code;
                textBox1.Text = item;
                textBox2.Text = price;
                textBox3.Text = stock;
                textBox4.Text = redeem;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(label5.Text))
            {
                MessageBox.Show("Please select item that you want to Update",
                "ROX Diamond Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string query = $"UPDATE bim0kocn6y1cl68dqubs.item SET item = '"+textBox1.Text+"', price = '"+textBox2.Text+"', stock = '"+textBox3.Text+"', redeem = '"+textBox4.Text+"' where code = '"+label5.Text+"'";
                    mySQLCommand.NonQuery(query);
                    MessageBox.Show("Item Successfuly Updated",
                    "ROX Diamond Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyPopulate();
                }
                catch (Exception Exception)
                {
                    MessageBox.Show("Error there is something wrong occured" + Exception,
                    "ROX Diamond Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            label5.Text = "";
           
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(label5.Text))
            {
                MessageBox.Show("Please select item that you want to Delete",
                "ROX Diamond Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string query = $"DELETE FROM bim0kocn6y1cl68dqubs.item WHERE code = '" + label5.Text + "'";
                    mySQLCommand.NonQuery(query);
                    MessageBox.Show("Item Successfuly Deleted",
                     "ROX Diamond Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyPopulate();
                }
                catch (Exception Exception)
                {
                    MessageBox.Show("Error there is something wrong occured" + Exception,
                    "ROX Diamond Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            label5.Text = "";
        }
    }
}
