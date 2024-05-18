using AdminWinForm.BusinesslogicLayer;
using AdminWinForm.Models;
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
        private async void LoadOrders()
        {
            try
            {
                List<Order> orders = await _orderLogic.GetAllOrders();

                dataGridView1.Rows.Clear();
                foreach (Order order in orders)
                {
                    dataGridView1.Rows.Add(order.OrderID, order.CustomerID, order.OrderDate, order.StartDate, order.EndDate, order.StartTime, order.EndTime, order.TotalHours, order.SubTotalPrice, order.TotalOrderPrice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af ordrer: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AllOrdersUI_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
