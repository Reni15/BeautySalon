namespace BeautySalon.Data
{
    public class TypeService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTimeRegister { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
