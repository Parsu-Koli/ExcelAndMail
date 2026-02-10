namespace BLL.DTOs
{
    public class EmailRequestDTO
    {
        public List<string> ToEmails { get; set; }  // multi-select
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
