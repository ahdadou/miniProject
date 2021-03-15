using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace MiniProject
{
    public partial class Cycle : UserControl
    {
        BindingSource bs=new BindingSource();
        
        public Cycle()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Cycle_Load(object sender, EventArgs e)
        {
            Db.RemplissageListeBox("select * from cycle", "cycle", "nomCycle", "idCycle", ref bs, lstBoxCycle);
            //active(false);
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            //active(true);
        }


        private void active(bool v)
        {
            btnValide.Visible = v;
            btnAnnuler.Visible = v;

            btnsupprimer.Visible = !v;
            btnModifier.Visible = !v;
            btnImprime.Visible = !v;

        }

        private void btnValide_Click(object sender, EventArgs e)
        {
            //active(false);


            string cs = ConfigurationManager.ConnectionStrings["ecoleConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(cs);
            cn.Open();
            string req = "insert into cycle values(@id,@nom,@nomArab)";
            SqlCommand com = new SqlCommand(req, cn);
            com.Parameters.Add(new SqlParameter("@id", Db.getId()));
            com.Parameters.Add(new SqlParameter("@nom", txtNom.Text));
            //string nomArab = "";
            //if (txtNomArab.Text != '') nomArab = txtNomArab.Text;
            com.Parameters.Add(new SqlParameter("@nomArab", txtNomArab.Text));
            var dr = com.ExecuteScalar();

           // dr.Close();
            dr = null;
            com = null;

            Db.RemplissageListeBox("select * from cycle", "cycle", "nomCycle", "idCycle", ref bs, lstBoxCycle);


        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            //active(false);

        }
    }
}
