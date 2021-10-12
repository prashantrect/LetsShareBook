namespace LSB.Models
{
    public class Profile
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }

        public Location Location { get; set; }

    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}