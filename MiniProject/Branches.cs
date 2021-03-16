using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniProject
{
    public partial class Branches : UserControl
    {
        string id = "";
        BindingSource bsP = new BindingSource();
        BindingSource bsF = new BindingSource();
        public Branches()
        {
            InitializeComponent();
        }

        private void Branches_Load(object sender, EventArgs e)
        {
            //Db.RemplissageListeBox("select * from Cycle", "Cycle", "nomCycle", "idCycle", ref bsP, comboBox1);
            //Db.RemplissageListeBoxRelation("select * from branche", "Cycle", "branche", "idCycle", "nomBrance", "idBranche", ref bsP, ref bsF, lstBoxCycle);
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
