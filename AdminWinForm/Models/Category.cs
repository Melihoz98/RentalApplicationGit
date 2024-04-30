namespace AdminWinForm.Models
{
    public class Category
    {

        public Category() { }



        public Category(string? categoryName, string? imagePath)
        {
        
            CategoryName = categoryName;
            ImagePath = imagePath;

        }

        public Category(int categoryID, string categoryName, string imagePath) {
            CategoryID = categoryID;
            CategoryName = categoryName;
            ImagePath = imagePath;  
        
        }

        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? ImagePath { get; set; }
    }

}
