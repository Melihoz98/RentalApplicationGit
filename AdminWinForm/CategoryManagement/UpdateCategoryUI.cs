using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminWinForm.CategoryManagement
{
    public partial class UpdateCategoryUI : Form
    {
        public UpdateCategoryUI(int categoryID, string? categoryName, string? imagePath)
        {
            InitializeComponent();
        }
    }
}
