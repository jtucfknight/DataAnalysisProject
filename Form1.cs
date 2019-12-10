using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Team06_DataAnalysis_gui
{
    public partial class Form1 : Form
    {
        static System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set palette.
            this.cht_data.Palette = ChartColorPalette.BrightPastel;
            Title title = cht_data.Titles.Add("Sentiment Analysis");
            title.Font = new System.Drawing.Font("Century Gothic", 14, FontStyle.Bold);
            title.ForeColor = System.Drawing.Color.FromArgb(229, 241, 251);

            // hook up timer event
            timer1.Tick += timer1_Tick;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OracleConnection con = DbHelper.GetConnection();
            string getData = @"SELECT SENTIMENT_ANALYSIS FROM ANALYZED_DATA ORDER BY ANALYZED_DATA_ID ASC";

            using (OracleCommand cmd = new OracleCommand(getData, con))
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                cmd.ExecuteReader();

                int counter = 0;
                double neutral_ctr = .0;
                double positive_ctr = .5;
                double negative_ctr = -.3;
                double somewhat_ctr = .8;

                while (reader.Read())
                {
                    string db_data = reader.GetString(0);
                    Console.WriteLine("The data is: " + db_data);

                    if (db_data == "neutral")
                    {
                        // Add point.
                        cht_data.Series[counter].Points.AddY(neutral_ctr += .0002);
                        //cht_data.Series[counter].Points.AddY(.5);
                    }
                    else if (db_data == "positive")
                    {
                        cht_data.Series[counter].Points.AddY(positive_ctr);
                        //cht_data.Series[counter].Points.AddY( .75);
                    }
                    else if (db_data == "somewhat negative")
                    {
                        cht_data.Series[counter].Points.AddY(somewhat_ctr += .00055);
                        //cht_data.Series[counter].Points.AddY(.2);
                    }
                    else if (db_data == "negative")
                    {
                        cht_data.Series[counter].Points.AddY(negative_ctr += .0002);
                        //cht_data.Series[counter].Points.AddY(.9);
                    }
                }
                Console.WriteLine("If you reached end of while, clear data...");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            timer1.Start();
            timer1.Interval = 1000;
            timer1_Tick(sender, e);
            dataBox01(sender, e);
            dataBox02(sender, e);
            dataBox03(sender, e);
        }
        private void dataBox01(object sender, EventArgs e)
        {
            OracleConnection con = DbHelper.GetConnection();
            string getData = @"SELECT TRUNC(CONFIDENCE_ANALYSIS,4) FROM ANALYZED_DATA ORDER BY ANALYZED_DATA_ID ASC";
            con.Open();

            using (OracleCommand cmd = new OracleCommand(getData, con))
            {
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    rBox_01.ForeColor = Color.White;
                    rBox_01.AppendText(reader["TRUNC(CONFIDENCE_ANALYSIS,4)"].ToString() + Environment.NewLine);
                }
            }
            con.Close();
        }

        private void dataBox02(object sender, EventArgs e)
        {
            OracleConnection con = DbHelper.GetConnection();
            string getData = @"SELECT INTENSITY_ANALYSIS FROM ANALYZED_DATA ORDER BY ANALYZED_DATA_ID ASC";
            con.Open();

            using (OracleCommand cmd = new OracleCommand(getData, con))
            {

                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rBox_02.ForeColor = Color.White;
                    rBox_02.AppendText(reader["INTENSITY_ANALYSIS"].ToString() + Environment.NewLine);
                }
            }
            con.Close();
        }

        private void dataBox03(object sender, EventArgs e)
        {
            OracleConnection con = DbHelper.GetConnection();
            string getData = @"SELECT ANALYSIS_TOTAL_TIME FROM ANALYZED_DATA ORDER BY ANALYZED_DATA_ID ASC";
            con.Open();

            using (OracleCommand cmd = new OracleCommand(getData, con))
            {

                OracleDataReader reader = cmd.ExecuteReader();

                //Loop through db.  Comment out and uncomment if() for single line.
                while (reader.Read())
                //if (reader.Read())
                {
                    rBox_03.ForeColor = Color.White;
                    rBox_03.AppendText(reader["ANALYSIS_TOTAL_TIME"].ToString() + Environment.NewLine);
                }
            }
            con.Close();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_json_Click(object sender, EventArgs e)
        {
            Console.WriteLine("JSON Button");
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}