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
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GraphSystemInterface
{
    public partial class Form1 : Form
    {

        private Database DB;
        private DataTable table;
        private MySqlDataAdapter adapter;

        class Database
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost; Database=graph_system_interface; User ID=root; Password=Toyotaipsum1996!");

            public void OpenConnection()
            {
                if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            }

            public void CloseConnection()
            {
                if (connection.State == System.Data.ConnectionState.Open) connection.Close();
            }

            public MySqlConnection GetConnection()
            {
                return connection;
            }
        }

        public Form1()
        {
            InitializeComponent();
            DB = new Database();
            table = new DataTable();
            adapter = new MySqlDataAdapter();
        }

        private void machine_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillHeader();
            string mt_type = machine_type.Text;

            DB.OpenConnection();
            DataTable dt2 = new DataTable();
            MySqlCommand command = new MySqlCommand("select machine_tool_name from machine_tool_name where (id_mt=(select id_mt from machine_tool_type where type='" + mt_type + "'));", DB.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(dt2);
            machine_name.Items.Clear();

            foreach (DataRow row in dt2.Rows)
            {
                machine_name.Items.Add((string)(row.ItemArray[0]));
            }

            DB.CloseConnection();
        }

        private void machine_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_machine_name.Text = (sender as System.Windows.Forms.ComboBox).Text;
            FillHeader();
            string selected = machine_name.Text;
            Image machineImage = (Image)Properties.Resources.ResourceManager.GetObject(selected);
            picture_machine.Image = machineImage;
            picture_machine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        }

        private void FillHeader()
        {
            label_machine_type.Text = machine_type.Text + " " + label_machine_name.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            DB.OpenConnection();

            MySqlCommand commamd = new MySqlCommand("SELECT type FROM machine_tool_type", DB.GetConnection());

            adapter.SelectCommand = commamd;
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                machine_type.Items.Add((string)row.ItemArray[0]);
            }

            DB.CloseConnection();   
        }

        private void button_acticve_messages_Click(object sender, EventArgs e)
        {
            string mt_name = label_machine_name.Text;
            table.Clear();
            DB.OpenConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM machine_tool_state where (id_mtn=(SELECT id_mtn FROM machine_tool_name where machine_tool_name='" + mt_name + "'));", DB.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            dataGridView2.DataSource = table;
            DB.CloseConnection();
        }

        private void button_schedule_Click(object sender, EventArgs e)
        {
            string mt_name = label_machine_name.Text;
            DataTable dt = new DataTable();
            DB.OpenConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM machine_tool_load where (id_mtn=(SELECT id_mtn FROM machine_tool_name where machine_tool_name='" + mt_name +"'))", DB.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dt);
            DB.CloseConnection();

            chart1.Series.Clear();

            //double v;
            //int s_counter = 0;
            //bool MondayLegendVisible = true;
            //bool TuesdayLegendVisible = true;
            //bool WednesdayLegendVisible = true;
            //bool ThursdayLegendVisible = true;
            //bool FridayLegendVisible = true;
            //bool SaturdayLegendVisible = true;
            //bool SundayLegendVisible = true;

            //foreach (DataRow dr in dt.Rows)
            //{
            //    v = (double.Parse((string)(dr["time_mtl"]))) / 60;
            //    string seriesName = "S" + s_counter.ToString();
            //    chart1.Series.Add(seriesName);
            //    chart1.Series[seriesName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.PointAndFigure;
                
            //    if ((string)dr["day"]) == "пн") 
            //    {
            //        chart1.Series[seriesName].Color = Color.FromArgb(0, 192, 0);
            //        chart1.Series[seriesName].LegendText = "пн";
            //        chart1.Series[seriesName].IsVisibleInLegend = MondayLegendVisible;
            //        chart1.Series[seriesName].SetCustomProperty("PixelPointWidth", "150");
            //        MondayLegendVisible = false;
            //    }

            //    if ((string)dr["day"]) == "вт") 
            //    {
            //        chart1.Series[seriesName].Color = Color.FromArgb(0, 192, 0);
            //        chart1.Series[seriesName].LegendText = "вт";
            //        chart1.Series[seriesName].IsVisibleInLegend = TuesdayLegendVisible;
            //        chart1.Series[seriesName].SetCustomProperty("PixelPointWidth", "150");
            //        MondayLegendVisible = false;
            //    }
            //    if ((string)dr["day"]) == "ср") 
            //    {
            //        chart1.Series[seriesName].Color = Color.FromArgb(0, 192, 0);
            //        chart1.Series[seriesName].LegendText = "ср";
            //        chart1.Series[seriesName].IsVisibleInLegend = WednesdayLegendVisible;
            //        chart1.Series[seriesName].SetCustomProperty("PixelPointWidth", "150");
            //        MondayLegendVisible = false;
            //    }
            //    if ((string)dr["day"]) == "чт") 
            //    {
            //        chart1.Series[seriesName].Color = Color.FromArgb(0, 192, 0);
            //        chart1.Series[seriesName].LegendText = "чт";
            //        chart1.Series[seriesName].IsVisibleInLegend = ThursdayLegendVisible;
            //        chart1.Series[seriesName].SetCustomProperty("PixelPointWidth", "150");
            //        MondayLegendVisible = false;
            //    }
            //    if ((string)dr["day"]) == "пт") 
            //    {
            //        chart1.Series[seriesName].Color = Color.FromArgb(0, 192, 0);
            //        chart1.Series[seriesName].LegendText = "пт";
            //        chart1.Series[seriesName].IsVisibleInLegend = FridayLegendVisible;
            //        chart1.Series[seriesName].SetCustomProperty("PixelPointWidth", "150");
            //        MondayLegendVisible = false;
            //    }
            //    if ((string)dr["day"]) == "сб") 
            //    {
            //        chart1.Series[seriesName].Color = Color.FromArgb(0, 192, 0);
            //        chart1.Series[seriesName].LegendText = "сб";
            //        chart1.Series[seriesName].IsVisibleInLegend = SaturdayLegendVisible;
            //        chart1.Series[seriesName].SetCustomProperty("PixelPointWidth", "150");
            //        MondayLegendVisible = false;
            //    }
            //    if ((string)dr["day"]) == "вс") 
            //    {
            //        chart1.Series[seriesName].Color = Color.FromArgb(0, 192, 0);
            //        chart1.Series[seriesName].LegendText = "вс";
            //        chart1.Series[seriesName].IsVisibleInLegend = SundayLegendVisible;
            //        chart1.Series[seriesName].SetCustomProperty("PixelPointWidth", "150");
            //        MondayLegendVisible = false;
            //    }

            //    chart1.Series[seriesName].Points.AddY(v);
            //    s_counter++; 
            //}
        }
    }
}
