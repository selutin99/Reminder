using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reminder
{
    public partial class Form1 : Form
    {
        public static bool flag = false;
        public static bool flagForIcon = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (inputTextBox.MaskCompleted)
            {
                DialogResult result = MessageBox.Show("У Вас остались несохранённые изменения! Останитесь здесь?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (result)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        e.Cancel = true;
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        ReminderIcon.Visible = false;
                        inputTextBox.Clear();
                        Close();
                        break;
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Clear();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!inputTextBox.MaskCompleted)
            {
                MessageBox.Show("Введите корректное значение!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputTextBox.Clear();
            }
            else if(validateTime())
            {
                Visible = false;
                ShowInTaskbar = false;
                ReminderIcon.Visible = true;
                timer1.Enabled = true;
                flag = true;
            }
            else
            {
                MessageBox.Show("Введите корректное значение!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputTextBox.Clear();
            }
        }

        public bool validateTime()
        {
            char [] time = inputTextBox.Text.ToCharArray();
            string hours = time[0] +""+ time[1];
            string minutes = time[3] + "" + time[4];
            int Inthours=0, Intminutes=0;
            if (hours.Equals("00"))
            {
                Inthours = 0;
            }
            else if (minutes.Equals("00"))
            {
                Intminutes = 0;
            }
            else
            {
                 Inthours = int.Parse(hours);
                 Intminutes = int.Parse(minutes);
            }

            if (Inthours <= 23 && Intminutes <= 59)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void iconClick()
        {
            Visible = true;
            ShowInTaskbar = true;
            ReminderIcon.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag)
            {
                string resultHours, resultMinutes, resultTime;
                DateTime currTime = DateTime.Now;

                resultHours = currTime.TimeOfDay.Hours.ToString();
                resultMinutes = currTime.TimeOfDay.Minutes.ToString();
                resultTime = resultHours + ":" + resultMinutes;

                if (inputTextBox.Text.Equals(resultTime))
                {
                    flag = false;
                    inputTextBox.Clear();
                    //ReminderIcon.ShowBalloonTip(30000, "Оповещение", "Время!", ToolTipIcon.Info);
                    MessageBox.Show("Время!","Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    iconClick();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Visible)
            {
                ReminderIcon.Visible = true;
                iconClick();
            }
        }

        private void AddStrip_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Visible = true;
            ShowInTaskbar = true;
            ReminderIcon.Visible = false;
        }

        private void ExitStrip_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            inputTextBox.Clear();
            Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text= DateTime.Now.ToLongTimeString();
        }
    }
}
