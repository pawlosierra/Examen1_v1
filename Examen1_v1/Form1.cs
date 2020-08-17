using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Examen1_v1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            Data.Client.UpdateClient();
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            if(-2 == BusinessLayer.Commandes.UpdateCommandes())//UpdateCommandes()
            {
                MessageBox.Show("Commande rejetée: inférieur à 10.00 Can$");
                //bindingSource2.DataSource = Data.Commandes.GetCommandes();
                Data.DataTables.GetDataSet().Tables["Commandes"].RejectChanges();
            }
            //Data.Commandes.UpdateCommandes();
        }

        //internal void montrerClient()
        //{
        //    dataGridView1.ReadOnly = false;
        //    dataGridView1.AllowUserToAddRows = true;
        //    dataGridView1.AllowUserToDeleteRows = true;
        //    dataGridView1.RowHeadersVisible = true;
        //    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    dataGridView1.Dock = DockStyle.Fill;
        //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //    dataGridView1.DataSource = Data.Client.GetClient();
        //}
        private void montrerLesClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            bindingSource1.DataSource = Data.Client.GetClient();
            bindingSource1.Sort = "ClientId";
            dataGridView1.DataSource = bindingSource1;

            dataGridView1.Columns["Nom"].HeaderText = "Nom du client";
        }

        private void montrerLesCommandesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            bindingSource2.DataSource = Data.Commandes.GetCommandes();
            bindingSource2.Sort = "ComId";
            dataGridView1.DataSource = bindingSource2;

            dataGridView1.Columns["ComId"].HeaderText = "Commande ID";
            dataGridView1.Columns["ClientId"].HeaderText = "Client ID";
            dataGridView1.Columns["ComId"].DisplayIndex = 0;
            dataGridView1.Columns["Description"].DisplayIndex = 1;
            dataGridView1.Columns["Prix"].DisplayIndex = 2;
            dataGridView1.Columns["ClientId"].DisplayIndex = 3;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Commande rejetée");
        }
    }
}
