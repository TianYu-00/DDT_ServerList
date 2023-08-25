using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace DDT_ServerList
{
    public partial class FormMain : Form
    {
        private Timer loopTimer;
        public FormMain()
        {
            InitializeComponent();

            decimal timerSetTimeDeci = numericUpDown1.Value; //get value
            int timerSetTimeInt = Decimal.ToInt32(timerSetTimeDeci); //transform value into int
            loopTimer = new Timer();
            loopTimer.Interval = timerSetTimeInt * 1000;
            loopTimer.Tick += LoopFunction;
            loopTimer.Start();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            panel1.Hide();
            DDT7D_Data();


        
        }

        void DDT7D_Data()
        {
            WebClient client = new WebClient(); //download the web page
            //use client
            String ddt_7d_serverlist_string = client.DownloadString("http://quest1000.ddt.7road.net/serverlist.ashx?rnd=0%2E4183690962381661");

            var test = @"
                <Result value=""true"" message=""Success!"" total=""2804"" agentId=""1002580"" AreaName=""四号机"">
                  <Item ID=""1"" Name=""频道1"" IP=""114.132.52.192"" Port=""9200"" State=""4"" MustLevel=""100"" LowestLevel=""0"" Online=""111"" Remark="""" />
                  <Item ID=""2"" Name=""频道2"" IP=""114.132.52.192"" Port=""9300"" State=""2"" MustLevel=""100"" LowestLevel=""0"" Online=""900"" Remark="""" />
                  <Item ID=""12"" Name=""ZZQ"" IP=""114.132.52.192"" Port=""9200"" State=""1"" MustLevel=""100"" LowestLevel=""0"" Online=""0"" Remark="""" />
                  <Item ID=""13"" Name=""频道3"" IP=""114.132.52.192"" Port=""9400"" State=""2"" MustLevel=""100"" LowestLevel=""0"" Online=""100"" Remark="""" />
                  <Item ID=""14"" Name=""频道4"" IP=""114.132.52.192"" Port=""9500"" State=""2"" MustLevel=""100"" LowestLevel=""0"" Online=""102"" Remark="""" />
                </Result>
                ";

            //7D总人数 /////////////////////////////////////////////////////////////////////////////
            int startIndex = ddt_7d_serverlist_string.IndexOf("total=")+7;
            int endIndex = ddt_7d_serverlist_string.IndexOf("agentId")-(startIndex+2);
            string stringtemp = ddt_7d_serverlist_string.Substring(startIndex, endIndex);
            textBox_7DTotal.Text = stringtemp;
            //7D频道1-4 /////////////////////////////////////////////////////////////////////////////
            List<string> newStrings = new List<string>();
            string currentString = "";

            int currentIndex = 0;
            int currentOnlineIndex = 0;
            int onlineChannelArray = 0;

            string[] D_Channel = new string[4];

            foreach (char c in ddt_7d_serverlist_string)
            {
                currentIndex += 1;
                if (c == ' ')
                {
                    newStrings.Add(currentString);
                    
                    currentString = "";
                }
                else
                {
                    currentString += c;
                }

                if (currentString == "Online")
                {
                    currentOnlineIndex += 1;
                    if (currentOnlineIndex != 3)
                    {
                        
                        string tempText = ddt_7d_serverlist_string;
                        int currentTempIndex = currentIndex + 2;
                        int endTempIndex = 4;
                        string onlineTempSubstring = tempText.Substring(currentTempIndex, endTempIndex);
                        D_Channel[onlineChannelArray] = onlineTempSubstring.Replace("\"", "");
                        onlineChannelArray += 1;

                        //textBox6.Text += onlineTempSubstring;
                        //string tempTextBox = "textBox"+ currentOnlineIndex.ToString();
                        //tempTextBox.Text = onlineTempSubstring;
                    }
                    else
                    { 
                        
                    }
                    
                }
                
            }

            newStrings.Add(currentString); // Add the last section of the string
            

            // Print the new strings
            //foreach (string s in newStrings)
            //{
              //  Console.WriteLine(s);
                //textBox6.Text += s;
            //}

            currentIndex = 0;

            textBox_7DChannel1.Text = D_Channel[0];
            textBox_7DChannel2.Text = D_Channel[1];
            textBox_7DChannel3.Text = D_Channel[2];
            textBox_7DChannel4.Text = D_Channel[3];


            label_LastUpdated.Text = DateTime.Now.ToString("HH:mm:ss");

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            DDT7D_Data();

            decimal timerSetTimeDeci = numericUpDown1.Value; //get value
            int timerSetTimeInt = Decimal.ToInt32(timerSetTimeDeci); //transform value into int

            if (checkBox1.Checked)
            {
                loopTimer.Interval = timerSetTimeInt * 1000;
                loopTimer.Start();

            }
            else
            {
                loopTimer.Stop();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        private void LoopFunction(object sender, EventArgs e)
        {
            // Do something here
            DDT7D_Data();
        }

        private bool optionPanelHide = true;
        private void button2_Click(object sender, EventArgs e)
        {
            //Form2 form2 = new Form2();
            //form2.Show(); // or form.ShowDialog(this);
            optionPanelHide = !optionPanelHide;
            if (!optionPanelHide)
            {
                panel1.Show();
            }
            else
            {
                //hide it!
                panel1.Hide();
            }

            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ActiveForm.Opacity = ((double)(trackBar1.Value) / 10);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                Form.ActiveForm.TopMost = true;
            }
            else
            {
                Form.ActiveForm.TopMost = false;
            }
        }

        
    }
}
