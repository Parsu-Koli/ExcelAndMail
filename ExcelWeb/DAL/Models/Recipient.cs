namespace DAL.Models
{
    public class Recipient
    {
        public int Id { get; set; }
        public string Name { get; set; } // optional, can display in dropdown
        public string Email { get; set; }
    }
}
