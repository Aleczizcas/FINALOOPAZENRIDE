namespace Azenride
{
    public class Order
    {
        public string Username { get; set; }
        public string Service { get; set; }
        public double Cost { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }

        public Order(string username, string service, double cost, string pickupLocation, string dropoffLocation)
        {
            Username = username;
            Service = service;
            Cost = cost;
            PickupLocation = pickupLocation;
            DropoffLocation = dropoffLocation;
        }
    }
}