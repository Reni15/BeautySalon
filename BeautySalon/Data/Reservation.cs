namespace BeautySalon.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UserId {  get; set; }
        public User Users { get; set; }

        public int ServiceId {  get; set; }
        public Service Services { get; set; }

        public DateTime DateRegister { get; set; }
    }
}
