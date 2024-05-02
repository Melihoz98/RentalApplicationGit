using AdminWinForm.BusinesslogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminWinForm.OrderManagement
{

    public partial class AllOrdersUI : Form
    {
        readonly OrderLogic _orderLogic;
        public AllOrdersUI()
        {
            InitializeComponent();

            _orderLogic = new OrderLogic();

            this.Load += AllOrdersUI_Load;

        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
