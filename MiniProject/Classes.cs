using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace MiniProject
{
    public partial class Classes : UserControl
    {
        bool update = false;
        BindingSource bsC = new BindingSource();
        BindingSource bsN = new BindingSource();
        SqlDataAdapter daC;
        SqlDataAdapter daN;
        public Classes()
        {
            InitializeComponent();
        }

        private void Classes_Load(object sender, EventArgs e)
        {
            active(false);
            //Db.fermerConnection();
            Db.RemplissageListeBox("select * from Cycle", "Cycle", "nomCycle", "idCycle", ref bsC, comboBox1, ref daC);
            try
            {
                //string req = "select * from branche where idCycle='" + comboBox1.SelectedValue + "'";
                //Db.RemplissageListeBox(req, "branche", "nomBranche", "idBranche", ref bsB, lstBoxBranche, ref daB);
                //txtIdCycle.Text = comboBox1.SelectedValue.ToString();


                string req1 = "select idNiveau as [Id Niveau],nomNiveau as [Nom Niveau],nomNiveauarabe as [اسم المستوى] from niveau where idCycle='" + comboBox1.SelectedValue + "'";
                //Db.RemplirGrd2(req, "niveau", bsN, dgvNiveau, ref daN);
                Db.RemplirGrd(req1, "niveau", bsN, dgvNiveau, ref daN);



            }
            catch (Exception ex) { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {

                try
                {
                    if (comboBox1.SelectedIndex >= 0)
                    {



                       // dgvNiveau.Rows.Clear();
                        string req = "select idNiveau as [Id Niveau],nomNiveau as [Nom Niveau],nomNiveauarabe as [اسم المستوى] from niveau where idCycle='" + comboBox1.SelectedValue + "'";
                        Db.RemplirGrd(req, "niveau", bsN, dgvNiveau, ref daN);

                    }
                    //MessageBox.Show(comboBox1.SelectedValue.ToString());

                    //string req = "select * from niveau where idCycle='" + comboBox1.SelectedValue + "'";
                    //Db.RemplirGrd2(req, "niveau", bsN, dgvNiveau, ref daN);


                }
                catch (Exception ex) { }



            }
        }

        private void dgvNiveau_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void active(bool v)
        {
            btnAnnuler.Visible = v;
            btnValide.Visible = v;
            dgvNiveau.ReadOnly = !v;
            //lstBoxBranche.Enabled = !v;
            btnsupprimer.Visible = !v;
            btnModifier.Visible = !v;
            btnAjouter.Visible = !v;
            comboBox1.Enabled = !v;
            //dgvNiveau.Columns[0].ReadOnly =true;
            //dgvNiveau.Columns[1].ReadOnly = !v;
            //dgvNiveau.Columns[2].ReadOnly = !v;

        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            active(true);
            //dgvNiveau.Rows[dgvNiveau.Rows.Count-1].Selected = true;
            //Cursor = Cursors.Hand;
            //dgvNiveau.Rows[dgvNiveau.Rows.Count - 1].Cells[0].Selected = false;
            //dgvNiveau.Rows[dgvNiveau.Rows.Count - 1].Cells[1].Selected = true;
            bsN.AddNew();
            dgvNiveau.CurrentRow.Cells[0].Value = Db.getId();



        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            active(false);
            bsN.CancelEdit();
            if(!update)
            dgvNiveau.Rows.RemoveAt(dgvNiveau.CurrentRow.Index);
            update = false;


        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            active(true);
            update = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormCycle f = new FormCycle();
            f.ShowDialog();
            Db.RemplissageListeBox("select * from Cycle", "Cycle", "nomCycle", "idCycle", ref bsC, comboBox1, ref daC);
            
        }

        private void btnValide_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(dgvNiveau.CurrentRow.Cells[0].Value.ToString());
            //Save

            string cs = ConfigurationManager.ConnectionStrings["ecoleConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(cs);
            cn.Open();

            if (update)
            {
                string req = "update niveau set nomNiveau=@nom,nomNiveauarabe=@nomArab,idCycle=@idCycle where idNiveau=@idNiveau";
                SqlCommand cmd = new SqlCommand(req, cn);
                cmd.Parameters.Add(new SqlParameter("@idNiveau", dgvNiveau.CurrentRow.Cells[0].Value));
                cmd.Parameters.Add(new SqlParameter("@nom", dgvNiveau.CurrentRow.Cells[1].Value));
                cmd.Parameters.Add(new SqlParameter("@nomArab", dgvNiveau.CurrentRow.Cells[2].Value));
                cmd.Parameters.Add(new SqlParameter("@idCycle", comboBox1.SelectedValue));
                SqlDataReader dr = cmd.ExecuteReader();
                update = false;
            }
            else
            {
            string req = "insert into niveau values(@idNiveau,@nom,@nomArab,@idCycle)";
            SqlCommand cmd = new SqlCommand(req, cn);
            cmd.Parameters.Add(new SqlParameter("@idNiveau", dgvNiveau.CurrentRow.Cells[0].Value));
            cmd.Parameters.Add(new SqlParameter("@nom", dgvNiveau.CurrentRow.Cells[1].Value));
            cmd.Parameters.Add(new SqlParameter("@nomArab", dgvNiveau.CurrentRow.Cells[2].Value));
            cmd.Parameters.Add(new SqlParameter("@idCycle", comboBox1.SelectedValue));
            SqlDataReader dr = cmd.ExecuteReader();
            }
          
            cn.Close();
            active(false);
            this.bsN.EndEdit();


            try
            {
                if (comboBox1.SelectedIndex >= 0)
                {
                    // dgvNiveau.Rows.Clear();
                    string reqq = "select idNiveau as [Id Niveau],nomNiveau as [Nom Niveau],nomNiveauarabe as [اسم المستوى] from niveau where idCycle='" + comboBox1.SelectedValue + "'";
                    Db.RemplirGrd(reqq, "niveau", bsN, dgvNiveau, ref daN);

                }
            }
            catch (Exception ex) { }


        }

        private void btnsupprimer_Click(object sender, EventArgs e)
        {
            if (dgvNiveau.Rows.Count > 0)
            {
                // string cs = ConfigurationManager.ConnectionStrings["ecoleConnectionString"].ConnectionString;

                if (MessageBox.Show("Voulez Vous Supprimer Ce Branche", "Attention", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    bsN.RemoveCurrent();
                    this.daN.Update(Db.ds, "niveau");

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rptListNiveau r = new rptListNiveau();
            r.SetDatabaseLogon("sa", "31325653");
            //r.DatasourceConnection[0].IntegratedSecurity = true;
            //r.DataSourceConnections[0].SetConnection("desktop-opq5k4o", "Ecoguide", true);
            FrmImpression f = new FrmImpression(r);
            f.ShowDialog();
        }
    }
}
