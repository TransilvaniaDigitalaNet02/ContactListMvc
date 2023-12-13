using ContactListMvc.Business.Exceptions;
using ContactListMvc.Business.Models;
using ContactListMvc.Business.Services.Abstractions;
using ContactListMvc.Business.Validation;
using ContactListMvc.Infrastructure.Models;
using ContactListMvc.Infrastructure.Stores.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ContactListMvc.Business.Services.Implementation
{
    internal class ContactListService : IContactListService
    {
        private readonly IContactListEntryStore _store;

        public ContactListService(IContactListEntryStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
        }

        public async Task<IReadOnlyList<ContactListEntry>> GetAllContactsAsync()
        {
            IReadOnlyList<ContactListEntryDTO> contactList = await _store.GetAllContactsAsync();

            return contactList.Select(c => MapToBusinessModel(c)).ToList(); 
        }

        public async Task<ContactListEntry?> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException(
                    "ContactList entry identifier must be a positive integer",
                    nameof(id));
            }

            ContactListEntryDTO? contactListEntry = await _store.GetByIdAsync(id);

            return contactListEntry is null
                ? null
                : MapToBusinessModel(contactListEntry);
        }

        public async Task<bool> CheckEntryExistsAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException(
                    "ContactList entry identifier must be a positive integer",
                    nameof(id));
            }

            return await _store.CheckEntryExistsAsync(id);
        }

        public async Task<bool> CreateEntryAsync(ContactListEntry entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry)); 
            }

            DataAnnotationsValidator.Validate(entry);

            return await _store.CreateEntryAsync(MapToDto(entry));
        }

        public async Task<bool> UpdateEntryAsync(int id, ContactListEntry entry)
        {
            if (id < 0)
            {
                throw new ArgumentException(
                    "ContactList entry identifier must be a positive integer",
                    nameof(id));
            }

            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            if (id != entry.Id)
            {
                throw new ArgumentException(
                    "Ambiguous contact list entry identifier specification.",
                    nameof(id));
            }

            DataAnnotationsValidator.Validate(entry);

            try
            {
                return await _store.UpdateEntryAsync(id, MapToDto(entry));
            }
            catch (DbUpdateConcurrencyException e)
            {
                bool entryStillExists = await _store.CheckEntryExistsAsync(id);

                throw new ContactListUpdateConflictException(!entryStillExists, e);
            }
        }

        public async Task<bool> DeleteEntryAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException(
                    "ContactList entry identifier must be a positive integer",
                    nameof(id));
            }

            return await _store.DeleteEntryAsync(id);
        }

        private static ContactListEntryDTO MapToDto(ContactListEntry model)
        {
            return new ContactListEntryDTO
            {
                Id = model.Id,
                ContactType = (int)model.ContactType,
                Name = model.Name,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email
            };
        }

        private static ContactListEntry MapToBusinessModel(ContactListEntryDTO dto)
        {
            return new ContactListEntry
            {
                Id = dto.Id,
                ContactType = Enum.IsDefined(typeof(ContactType), dto.ContactType)
                                ? (ContactType)dto.ContactType
                                : throw new ArgumentException($"ContactType numeric value '{dto.ContactType}' cannot be converted to a valid enum value."),
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email
            };
        }
    }
}
