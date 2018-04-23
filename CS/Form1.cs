using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Companies c = new Companies();
            Company c1 = new Company("Microfost", "info@microfost.com", "1234-1234");
            Address a1 = new Address("Seattle", "507 - 20th Ave.", "12345");
            c1.AddAddress(a1);
            a1.AddPhone(new Phone("555-123-12-12", "Billy"));
            a1.AddPhone(new Phone("555-124-14-14", "Johny"));
            Address a2 = new Address("Tacoma", "908 W. Capital Way", "54321");
            a2.AddPhone(new Phone("555-777-12-12", "Paul"));
            a2.AddPhone(new Phone("555-777-14-14", "Mary"));
            c1.AddAddress(a2);
            c.Add(c1);

            Company c2 = new Company("BigApple", "info@bigapple.com", "4321-4321");
            Address a3 = new Address("Kirkland", "722 Moss Bay Blvd.", "67890");
            a3.AddPhone(new Phone("555-888-12-12", "Steve"));
            c2.AddAddress(a3);
            Address a4 = new Address("Redmond", "4110 Old Redmond Rd.", "09876");
            a4.AddPhone(new Phone("555-333-12-12", "Jack"));
            c2.AddAddress(a4);
            c.Add(c2);

            XtraReport r = new XtraReport();
            r.Bands.Add(new DetailBand());
            r.DataSource = c;
            r.ShowDesignerDialog();
        }
    }
}