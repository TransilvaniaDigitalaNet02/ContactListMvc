using ContactListMvc.Business.Abstractions.Services;
using ContactListMvc.Business.Abstractions.Stores;
using ContactListMvc.Business.Models;
using ContactListMvc.Business.Validation;

namespace ContactListMvc.Business.Services
{
    internal sealed class ContactListService : IContactListService
    {
        private readonly IContactListEntryStore _store;

        public ContactListService(IContactListEntryStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
        }

        public async Task<IReadOnlyList<ContactListEntry>> GetAllContactsAsync()
        {
            IReadOnlyList<ContactListEntry> contactList = await _store.GetAllContactsAsync();

            return contactList;
        }

        public async Task<ContactListEntry?> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException(
                    "ContactList entry identifier must be a positive integer",
                    nameof(id));
            }

            ContactListEntry? contactListEntry = await _store.GetByIdAsync(id);

            return contactListEntry is null
                ? null
                : contactListEntry;
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

            return await _store.CreateEntryAsync(entry);
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

            return await _store.UpdateEntryAsync(id, entry);
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
    }
}
