using ContactListMvc.Infrastructure.Models;

namespace ContactListMvc.Infrastructure.Stores.Abstractions
{
    public interface IContactListEntryStore
    {
        Task<IReadOnlyList<ContactListEntryDTO>> GetAllContactsAsync();

        Task<ContactListEntryDTO?> GetByIdAsync(int id);

        Task<bool> CheckEntryExistsAsync(int id);

        Task<bool> CreateEntryAsync(ContactListEntryDTO entry);

        Task<bool> UpdateEntryAsync(int id, ContactListEntryDTO entry);

        Task<bool> DeleteEntryAsync(int id);
    }
}
