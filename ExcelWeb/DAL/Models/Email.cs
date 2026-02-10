namespace DAL.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime? SentDate { get; set; }
        public string? Status { get; set; }
    }

}
