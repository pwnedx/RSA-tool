using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA_tool
{
    public partial class Window : Form
    {
        public Window()
        {
            InitializeComponent();
        }

        public string RSAEncrypt(string public_key, string text)
        {
            using (var RSA = new RSACryptoServiceProvider(1024))
            {
                try
                {              
                    RSA.FromXmlString(public_key);
                    var RSAEncode = RSA.Encrypt(Encoding.UTF8.GetBytes(text), true);
                    var B64Encode = Convert.ToBase64String(RSAEncode);
                    return B64Encode;
                }
                finally
                {
                    RSA.PersistKeyInCsp = false;
                }
            }
        }

        public string RSADecrypt(string private_key, string text)
        {
            using (var RSA = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    RSA.FromXmlString(private_key);
                    var RSADecode = Encoding.UTF8.GetString(RSA.Decrypt(Convert.FromBase64String(text), true));
                    return RSADecode.ToString();
                }
                finally
                {
                    RSA.PersistKeyInCsp = false;
                }
            }
        }

        public void RSAGenerate()
        {
            using (var RSA = new RSACryptoServiceProvider(1024))
            {
                privateKeyBox.Text = RSA.ToXmlString(true);
                publicKeyBox.Text = RSA.ToXmlString(false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox.Text = RSAEncrypt(publicKeyBox.Text, textBox.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RSAGenerate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox.Text = RSADecrypt(privateKeyBox.Text, textBox.Text);
        }
    }
}
