using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace BatteryProject
{
    public partial class BatteryTimeCounter : Form
    {
        
        private Stopwatch stopWatch;
        
        public BatteryTimeCounter()
        {
            InitializeComponent();
            stopWatch = new Stopwatch();
           
            timer1.Start();

        }

        void OnPowerChange(Object sender, PowerModeChangedEventArgs e) {

            if (e.Mode == PowerModes.Suspend)
            {
                timer1.Stop();
                MessageBox.Show("System je u standby");
            }
            else if (e.Mode == PowerModes.Resume) timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
             SystemEvents.PowerModeChanged += OnPowerChange;
            
            radioButton1.Checked = System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online;
            radioButton2.Checked = System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline;

            if (System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
            {
                stopWatch.Start();
            }
            else
            {
                stopWatch.Stop();
            }
            
            
            TimeSpan ts = stopWatch.Elapsed;

            lblElapsedTime.Text = "RunTime: " + String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10); ;

            lblProcent.Text = "" + (SystemInformation.PowerStatus.BatteryLifePercent * 100) + "%";
            progressBar1.Value = (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
            lblRemainingTime.Text = "" + (SystemInformation.PowerStatus.BatteryLifeRemaining/60)+"min.";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();     
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutMe().ShowDialog();
        }

        private void BatteryTimeCounter_Load(object sender, EventArgs e)
        {
         
        }



    }
}
