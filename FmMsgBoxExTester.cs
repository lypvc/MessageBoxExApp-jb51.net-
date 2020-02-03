using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AhDung.WinForm
{
    public partial class FmMsgBoxExTester : Form
    {
        MessageBoxButtons msgButtons;
        MessageBoxIcon msgIcon;
        MessageBoxDefaultButton msgDfButton;

        public FmMsgBoxExTester()
        {
            InitializeComponent();

            grpMsbButtons.Tag = typeof(MessageBoxButtons);
            grpMsgIcon.Tag = typeof(MessageBoxIcon);
            grpDfButton.Tag = typeof(MessageBoxDefaultButton);

            msgButtons = MessageBoxButtons.OK;
            msgIcon = MessageBoxIcon.None;
            msgDfButton = MessageBoxDefaultButton.Button1;
        }

        private void btnShowMsgEx_Click(object sender, EventArgs e)
        {
            txbResult.AppendText(
            MessageBoxEx.Show(txbMessage.Text
                , txbCaption.Text
                , txbAttachMessage.Text
                , msgButtons
                , msgIcon
                , msgDfButton
                )
                + "\r\n");
        }

        private void btnShowMsgStd_Click(object sender, EventArgs e)
        {
            txbResult.AppendText(
            MessageBox.Show(txbMessage.Text
                , txbCaption.Text
                , msgButtons
                , msgIcon
                , msgDfButton
                )
                + "\r\n");
        }

        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked) { return; }

            Type t = rb.Parent.Tag as Type;
            object val = Enum.Parse(t, rb.Tag == null ? rb.Text : rb.Tag.ToString());
            if (t == typeof(MessageBoxButtons)) { msgButtons = (MessageBoxButtons)val; }
            else if (t == typeof(MessageBoxIcon)) { msgIcon = (MessageBoxIcon)val; }
            else { msgDfButton = (MessageBoxDefaultButton)val; }
        }

        //private static T GetEnumValueFromGroupBox<T>(GroupBox grp) where T : struct
        //{
        //    foreach (Control c in grp.Controls)
        //    {
        //        RadioButton rb;
        //        if ((rb = c as RadioButton) == null || !rb.Checked) { continue; }

        //        return (T)Enum.Parse(grp.Tag as Type, rb.Tag == null ? rb.Text : rb.Tag.ToString());
        //    }

        //    throw new ArgumentOutOfRangeException();
        //}

        private void ckbEnableAnimate_CheckedChanged(object sender, EventArgs e)
        {
            MessageBoxEx.EnableAnimate = ckbEnableAnimate.Checked;
        }

        private void ckbEnableSound_CheckedChanged(object sender, EventArgs e)
        {
            MessageBoxEx.EnableSound = ckbEnableSound.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.cnblogs.com/ahdung/");
        }

    }
}
