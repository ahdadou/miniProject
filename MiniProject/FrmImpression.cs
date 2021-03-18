using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
namespace MiniProject
{
    public partial class FrmImpression : Form
    {
        ReportClass report;
        public FrmImpression(ReportClass r)
        {
            InitializeComponent();
            this.report = r;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = report;
        }
    }
}
