﻿using hotel_clone_api.Models.Domain;

namespace hotel_clone_api.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> CreateContact(Contact contact);

        Task<List<Contact>> GetAllContacts(int pageNumber, int pageSize, string? query);

        Task<Contact?> GetContactById(Guid Id);
    }
}
