namespace VR_Web_Project
{
    public class Order
    {
        int Price { get; set; }

        public Order(int price)
        {
            Price = price;
        }

        public int GetPrice()
        {
            return Price;
        }
    }
}