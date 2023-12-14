using ContactListMvc.Business.Models;

namespace ContactListMvc.Business.Services.Abstractions
{
    public interface IContactListService
    {
        Task<IReadOnlyList<ContactListEntry>> GetAllContactsAsync();

        Task<ContactListEntry?> GetByIdAsync(int id);

        Task<bool> CheckEntryExistsAsync(int id);

        Task<bool> CreateEntryAsync(ContactListEntry entry);

        Task<bool> UpdateEntryAsync(int id, ContactListEntry entry);

        Task<bool> DeleteEntryAsync(int id);
    }
}
