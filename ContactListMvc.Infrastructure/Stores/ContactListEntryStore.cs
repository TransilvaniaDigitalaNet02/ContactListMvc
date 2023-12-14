using ContactListMvc.Business.Abstractions.Stores;
using ContactListMvc.Business.Exceptions;
using ContactListMvc.Business.Models;
using ContactListMvc.Infrastructure.Data;
using ContactListMvc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactListMvc.Infrastructure.Stores
{
    internal sealed class ContactListEntryStore : IContactListEntryStore
    {
        private readonly DatabaseContext _database;

        public ContactListEntryStore(DatabaseContext database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task<IReadOnlyList<ContactListEntry>> GetAllContactsAsync()
        {
            IReadOnlyList<ContactListEntryEntity> contacts = await _database.ContactListEntry.ToListAsync();

            return contacts.Select(c => MapToBusinessModel(c)).ToList();
        }

        public async Task<ContactListEntry?> GetByIdAsync(int id)
        {
            ContactListEntryEntity? contactListEntry = await _database.ContactListEntry.FirstOrDefaultAsync(c => c.Id == id);

            return contactListEntry is null
                ? null
                : MapToBusinessModel(contactListEntry);
        }

        public async Task<bool> CheckEntryExistsAsync(int id)
        {
            bool contactListEntryExists = await _database.ContactListEntry.AnyAsync(c => c.Id == id);

            return contactListEntryExists;
        }

        public async Task<bool> CreateEntryAsync(ContactListEntry entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            ContactListEntryEntity entity = MapToEntity(entry);

            _database.Add(entity);

            int rowsAffected = await _database.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateEntryAsync(int id, ContactListEntry entry)
        {
            ContactListEntryEntity? contactListEntry = await _database.ContactListEntry.FirstOrDefaultAsync(c => c.Id == id);

            if (contactListEntry is not null)
            {
                // update non key properties
                contactListEntry.ContactType = (int)entry.ContactType;
                contactListEntry.Name = entry.Name;
                contactListEntry.DateOfBirth = entry.DateOfBirth;
                contactListEntry.Address = entry.Address;
                contactListEntry.PhoneNumber = entry.PhoneNumber;
                contactListEntry.Email = entry.Email;

                try
                {
                    _database.Update(contactListEntry);

                    int rowsAffected = await _database.SaveChangesAsync();

                    return rowsAffected > 0;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    bool entryStillExists = await CheckEntryExistsAsync(id);

                    throw new ContactListUpdateConflictException(!entryStillExists, e);
                }
            }

            return false;
        }

        public async Task<bool> DeleteEntryAsync(int id)
        {
            ContactListEntryEntity? contactListEntry = await _database.ContactListEntry.FirstOrDefaultAsync(c => c.Id == id);

            if (contactListEntry is not null)
            {
                _database.ContactListEntry.Remove(contactListEntry);

                int rowsAffected = await _database.SaveChangesAsync();

                return rowsAffected > 0;
            }

            return false;
        }

        private static ContactListEntry MapToBusinessModel(ContactListEntryEntity entity)
        {
            return new ContactListEntry
            {
                Id = entity.Id,
                ContactType = Enum.IsDefined(typeof(ContactType), entity.ContactType)
                                ? (ContactType)entity.ContactType
                                : throw new ArgumentException($"ContactType numeric value '{entity.ContactType}' cannot be converted to a valid enum value."),
                Name = entity.Name,
                DateOfBirth = entity.DateOfBirth,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email
            };
        }

        private static ContactListEntryEntity MapToEntity(ContactListEntry model)
        {
            return new ContactListEntryEntity
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
    }
}
