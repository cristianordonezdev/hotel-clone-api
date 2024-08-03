
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
            this._context = context;
        }
        public async Task<Contact> CreateContact(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return contacts;
        }

        public async Task<Contact?> GetContactById(Guid Id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(i => i.Id == Id);
            if (contact == null) {
                return null;
            }
            return contact;
        }
    }
}
