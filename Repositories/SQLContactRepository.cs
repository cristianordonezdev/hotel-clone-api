
using hotel_clone_api.Data;
using hotel_clone_api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace hotel_clone_api.Repositories
{
    public class SQLContactRepository : IContactRepository
    {
        private readonly HotelDbContext _context;
        public SQLContactRepository(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<Contact> CreateContact(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<List<Contact>> GetAllContacts(int pageNumber = 1, int pageSize = 10, string query = null)
        {
            var contactsQuery = _context.Contacts.OrderByDescending(i => i.CreatedAt).AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                contactsQuery = contactsQuery.Where(i => i.Name.Contains(query) || i.Email.Contains(query) || i.Subject.Contains(query) || i.Message.Contains(query));
            }

            int skipResult = (pageNumber - 1) * pageSize;
            return await contactsQuery.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Contact?> GetContactById(Guid Id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(i => i.Id == Id);
            return contact;
        }
    }
}
