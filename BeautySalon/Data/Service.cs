namespace BeautySalon.Data
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //M : 1
        public int TypeServicesId { get; set; }//FK       
        public TypeService TypeServices { get; set; } //table TO
        public string Description { get; set; }
        
        public string URLimages { get; set; }
        public DateTime DateRegister { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
