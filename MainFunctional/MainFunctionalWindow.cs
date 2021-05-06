using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MainFunctional
{
    public partial class MainFunctionalWindow : Form
    {
        private readonly MailslotServer mailslotServer = new MailslotServer("FM_Mailslot_ToMF");
        private readonly MailslotClient mailslotClient = new MailslotClient("FM_Mailslot_ToMain");

        private readonly Thread mailslotServerThread;
        private bool isWorking = false;

        private readonly List<string[]> items = new List<string[]>();

        private bool isUpdateRequired = false;

        public MainFunctionalWindow()
        {
            InitializeComponent();

            updateTimer.Start();

            Visible = false;
            Opacity = 0;

            isWorking = true;
            mailslotServerThread = new Thread(MailslotServerWork);
            mailslotServerThread.Priority = ThreadPriority.BelowNormal;
            mailslotServerThread.Start();

            logListView.Columns[1].Width = (logListView.Width - logListView.Columns[0].Width - 20) / 2 - 20;
            logListView.Columns[2].Width = logListView.Columns[1].Width / 2 + 15;
            logListView.Columns[3].Width = logListView.Columns[2].Width;
        }

        private void MailslotServerWork()
        {
            Visible = false;

            mailslotClient.SendMessage("STARTED");

            string message;
            while (isWorking)
            {
                if ((message = mailslotServer.GetNextMessage()) != null)
                {
                    string[] buffer = message.Split('|');

                    if (buffer[0].Equals("QUIT"))
                    {
                        isWorking = false;
                    }
                    else if (buffer[0].Equals("LOCATION"))
                    {
                        Location = new Point(int.Parse(buffer[1]), int.Parse(buffer[2]));
                        Visible = false;
                    }
                    else if (buffer[0].Equals("SHOW"))
                    {
                        bool prev = Visible;

                        processNameLabel2.Text = buffer[1];

                        if (!prev)
                        {
                            Location = new Point(int.Parse(buffer[2]), int.Parse(buffer[3]));
                        }

                        Visible = true;
                        Opacity = 100;
                    }
                    else
                    {
                        items.Add(buffer);

                        isUpdateRequired = true;
                    }
                }
            }

            mailslotClient.SendMessage("QUITED");
        }

        private void MainFunctionalWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            e.Cancel = true;
        }

        private void saveLogButton_Click(object sender, EventArgs e)
        {
            StringBuilder buffer = new StringBuilder("SAVE|");

            buffer.Append(string.Format("{0}\t{1}|", processNameLabel1.Text, processNameLabel2.Text));

            foreach (string[] item in items)
            {
                buffer.Append(string.Format("{0}\t{1}\t{2}\t{3}|", item[0], item[1], item[2], item[3]));
            }

            buffer.Remove(buffer.Length - 1, 1);

            mailslotClient.SendMessage(buffer.ToString());
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (isUpdateRequired)
            {
                logListView.BeginUpdate();

                logListView.Items.Add(new ListViewItem(items[items.Count - 1]));

                logListView.EndUpdate();

                logListView.EnsureVisible(logListView.Items.Count - 1);

                isUpdateRequired = false;
            }
        }

        private void MainFunctionalWindow_SizeChanged(object sender, EventArgs e)
        {
            logListView.Columns[1].Width = (logListView.Width - logListView.Columns[0].Width - 20) / 2 - 20;
            logListView.Columns[2].Width = logListView.Columns[1].Width / 2 + 15;
            logListView.Columns[3].Width = logListView.Columns[2].Width;
        }
    }
}
