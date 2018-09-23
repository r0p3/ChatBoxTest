using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBoxTest
{
    public partial class Form1 : Form
    {
        public Color usernameC = Color.Gray, messageC = Color.Black, backgroundC = Color.LightBlue, chatBackgroundC = Color.White;
        public Form1()
        {
            InitializeComponent();
            pBackground.Click += color_Click;
            pUsername.Click += color_Click;
            pMessage.Click += color_Click;
            pBackground.BackColor = backgroundC;
            pUsername.BackColor = usernameC;
            pMessage.BackColor = messageC;
            pChatBox.Click += color_Click;

        }

        private void color_Click(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color newColor = colorDialog1.Color;
                switch (p.Name)
                {
                    case "pUsername":
                        usernameC = newColor;
                        break;
                    case "pMessage":
                        messageC = newColor;
                        break;
                    case "pBackground":
                        backgroundC = newColor;
                        break;
                    case "pChatBox":
                        chatBackgroundC = newColor;
                        richTextBox1.BackColor = newColor;
                        break;
                    default:
                        break;
                }
                p.BackColor = newColor;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addToChat(true,"r0p3", "hello i like this.................................................");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            addToChat(false, "Me", "Hello my friend...........................");
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        public void addToChat(bool me, string username, string message)
        {
            HorizontalAlignment alignment = (!me) ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            bool firstMessage = true;
            string chatMessage = "";
            if (alignment == richTextBox1.SelectionAlignment && richTextBox1.TextLength != 0)
            {
                chatMessage = message;
                firstMessage = false;
            }
            else
            {
                chatMessage = string.Format("{0}\n{1}", username, message);
                if(richTextBox1.TextLength != 0)
                    richTextBox1.AppendText(Environment.NewLine);
            }
            int startIndex = richTextBox1.TextLength;
            int newMessageLength = chatMessage.Length;         

            richTextBox1.AppendText(chatMessage + Environment.NewLine);
            richTextBox1.Select(startIndex, newMessageLength);
            richTextBox1.SelectionAlignment = alignment;
            richTextBox1.ScrollToCaret();

            if(firstMessage)
            {
                richTextBox1.Select(startIndex, username.Length);
                richTextBox1.SelectionColor = usernameC;
                richTextBox1.SelectionFont = new Font("Arial", 8, FontStyle.Italic);
                richTextBox1.Select(startIndex + username.Length + 1, message.Length);
            }
            else
            {
                richTextBox1.Select(startIndex, message.Length);
            }
            richTextBox1.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            richTextBox1.SelectionBackColor = backgroundC;
            richTextBox1.SelectionColor = messageC;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                if (rnd.Next(0, 2) == 0)
                    addToChat(false, "me", createRandomMessage());
                else
                    addToChat(true, "r0p3", createRandomMessage());
                Thread.Sleep(100);
            }
        }

        private string createRandomMessage()
        {
            string chooseFromMe = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖabcdefghijklmnopqrstuvwxyzåäö                                             ";
            string message = "";
            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(0,255); i++)
            {
                message += chooseFromMe.ToCharArray()[rnd.Next(0, chooseFromMe.Length)];
            }
            return message;
        }
    }
}
