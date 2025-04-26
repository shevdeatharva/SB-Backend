namespace Shubham_Bhawtu.Authentication.Models
{
    public class RegisterInfoModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Note: Store hashed passwords only
        public string Phone { get; set; }

        // Address fields
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Landmark { get; set; }
    }
}
