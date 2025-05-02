using Microsoft.AspNetCore.Identity;

namespace BeautySalon.Data
{
    public class User:IdentityUser
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
