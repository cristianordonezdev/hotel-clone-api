namespace hotel_clone_api.Models.DTOs
{
    public class ContactDto
    {

            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public string Subject { get; set; }

            public string Message { get; set; }

            public DateTime CreatedAt { get; set; }
        
    }
}
