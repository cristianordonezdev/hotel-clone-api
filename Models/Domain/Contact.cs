namespace hotel_clone_api.Models.Domain
{
    public class Contact
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public Contact()
        {
            CreatedAt = DateTime.UtcNow;
        }
    } 
}
