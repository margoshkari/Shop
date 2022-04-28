using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SubmitBtn.Enabled = false;
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            SelectRequestAsync();
        }
        private void insertBtn_Click(object sender, EventArgs e)
        {
            textBox.Text = "ProductName: \r\nPrice: \r\nidCategory: \r\nidManuf: ";
            SubmitBtn.Enabled = true;
        }
        private async Task SelectRequestAsync()
        {
            WebRequest request = WebRequest.Create("http://ipaddress-001-site1.gtempurl.com/Database/Select");
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    textBox.Text = reader.ReadToEnd();
                }
            }
            response.Close();
        }
        private async Task InsertRequestAsync()
        {
            List<string> list = textBox.Text.Split(new char[] { ':', '\n','\r', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            list.RemoveAll(x => x == "ProductName" || x == "Price" || x == "idCategory" || x == "idManuf");
            if(list.Count == 4)
            {
                WebRequest request = WebRequest.Create($"http://ipaddress-001-site1.gtempurl.com/Database/Insert?name={list[0]}&price={list[1].ToString()}&categoryId={list[2].ToString()}&manufId={list[3].ToString()}");
                WebResponse response = await request.GetResponseAsync();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        textBox.Text = reader.ReadToEnd();
                    }
                }
                response.Close();
            }
            else
            {
                textBox.Text = "Fill all fields!";
            }
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            InsertRequestAsync();
        }
    }
}

