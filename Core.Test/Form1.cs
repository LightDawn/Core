using Core.Cmn;
using Core.Cmn.FarsiUtils;
using Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //   PersianDateTest();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            System.Collections.Generic.List<User> list1 = new List<User>();
            System.Collections.Generic.List<User> list2 = new List<User>();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var item = new User();
                item["Id"] = i;
                item["LName"] = i.ToString();
                item["UserName"] = i.ToString();
                item["FName"] = i.ToString();
                list1.Add(item);
                var item1 = new User();
                item1["Id1"] = item["Id"];
                item1["LName"] = item["LName"];
                item1["UserName"] = item["UserName"];
                item1["FName"] = item["FName"];
                list1.Add(item1);
            }
            sw.Stop();
            var clock = sw.Elapsed;


            sw.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                var item = new User();
                item.Id = i;
                item.LName = i.ToString();
                item.UserName = i.ToString();
                item.FName = i.ToString();
                list2.Add(item);
                var item1 = new User();
                item1.Id = item.Id;
                item1.LName = item.LName;
                item1.UserName = item.UserName;
                item1.FName = item.FName;
                list2.Add(item1);

            }
            sw.Stop();
            var clock2 = sw.Elapsed;
            MessageBox.Show(clock.ToString() + "\n" + clock2.ToString() + "\n");
        }

        private void PersianDateTest()
        {
            var aa = PersianDate.IsValidPersianDate(textBox1.Text).ToString();
            var bb = "true";
            try { PersianDate.Parse(textBox1.Text).ToString(); }
            catch
            {
                bb = "false";
            }
            MessageBox.Show(string.Format("{0}  {1}", aa, bb));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PersianDate p = new PersianDate();
            ExceptionHandlerBase.asdf();
            var aa = PersianDate.IsValidPersianDate(textBox1.Text).ToString();
            var bb = "true";
            try { PersianDate.Parse(textBox1.Text).ToString(); }
            catch
            {
                bb = "false";
            }
            MessageBox.Show(string.Format("{0}  {1}", aa, bb));

        }
    }
}
