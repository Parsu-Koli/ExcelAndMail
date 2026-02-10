public class Email
{
    public int Id { get; set; }

    // For bulk selection
    public List<string>? SelectedEmails { get; set; }

    public string Subject { get; set; }
    public string Body { get; set; }

    // For displaying history
    public List<string>? ToEmails { get; set; } // stores all recipients of a sent email
    public DateTime SentDate { get; set; }

    public string? ToEmail { get; set; }

    public string Status { get; set; }
}
