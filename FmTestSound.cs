using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AhDung.WinForm
{
    public partial class FmTestSound : Form
    {
        public FmTestSound()
        {
            InitializeComponent();

            radioButton13.Tag = MessageBoxIcon.None;
            radioButton12.Tag = MessageBoxIcon.Error;
            radioButton9.Tag = MessageBoxIcon.Question;
            radioButton10.Tag = MessageBoxIcon.Warning;
            radioButton11.Tag = MessageBoxIcon.Information;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked) { return; }

            Play(rb.Text);
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked) { return; }

            label1.Text = PlayMessageSound((MessageBoxIcon)rb.Tag).ToString();
        }


        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern bool PlaySound([MarshalAs(UnmanagedType.LPWStr)] string soundName, IntPtr hmod, int soundFlags);

        /// <summary>
        /// 播放系统事件声音
        /// </summary>
        /// <param name="eventSound"></param>
        private static void Play(string eventSound)
        {
            PlaySound(eventSound, IntPtr.Zero, 0x10000/*SND_ALIAS*/ | 0x1/*SND_ASYNC*/);
        }

        /// <summary>
        /// 播放系统事件声音
        /// </summary>
        private static void PlaySysEventSound(MessageBoxIcon msgType)
        {
            string eventString;
            switch (msgType)
            {
                case MessageBoxIcon.None: eventString = "SystemDefault"; break;

                //Question原本是没声音的，此实现让它蹭一下Information的
                case MessageBoxIcon.Question:

                //MessageBoxIcon.Information同样
                case MessageBoxIcon.Asterisk: eventString = "SystemAsterisk"; break;

                //MessageBoxIcon.Hand、MessageBoxIcon.Stop同样
                case MessageBoxIcon.Error: eventString = "SystemHand"; break;

                //MessageBoxIcon.Warning同样
                case MessageBoxIcon.Exclamation: eventString = "SystemExclamation"; break;

                default: throw new ArgumentOutOfRangeException();
            }

            PlaySound(eventString, IntPtr.Zero, 0x10000/*SND_ALIAS*/ | 0x1/*SND_ASYNC*/);
        }

        /// <summary>
        /// 播放消息提示音
        /// </summary>
        private static bool PlayMessageSound(MessageBoxIcon msgType)
        {
            return MessageBeep((int)msgType);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool MessageBeep(int type);

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox2.Controls)
            {
                RadioButton rb;
                if ((rb = c as RadioButton) != null && rb.Checked)
                {
                    PlaySysEventSound((MessageBoxIcon)rb.Tag);
                    return;
                }
            }
        }

    }
}
