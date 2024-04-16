namespace RentalService.Models
{
    public class Category
    {

        public Category() { }



        public Category(string? categoryName)
        {
        
            CategoryName = categoryName;

        }

        public Category(int categoryID, string categoryName) {
            CategoryID = categoryID;
            CategoryName = categoryName;
        
        }





        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
    }

}
