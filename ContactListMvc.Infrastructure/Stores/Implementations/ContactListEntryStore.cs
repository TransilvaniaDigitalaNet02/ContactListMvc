using ContactListMvc.Infrastructure.Data;
using ContactListMvc.Infrastructure.Entities;
using ContactListMvc.Infrastructure.Models;
using ContactListMvc.Infrastructure.Stores.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ContactListMvc.Infrastructure.Stores.Implementations
{
    internal sealed class ContactListEntryStore : IContactListEntryStore
    {
        private readonly DatabaseContext _database;

        public ContactListEntryStore(DatabaseContext database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task<IReadOnlyList<ContactListEntryDTO>> GetAllContactsAsync()
        {
            IReadOnlyList<ContactListEntryEntity> contacts = await _database.ContactListEntry.ToListAsync();

            return contacts.Select(c => MapToDto(c)).ToList();
        }

        public async Task<ContactListEntryDTO?> GetByIdAsync(int id)
        {
            ContactListEntryEntity? contactListEntry = await _database.ContactListEntry.FirstOrDefaultAsync(c => c.Id == id);

            return contactListEntry is null 
                ? null 
                : MapToDto(contactListEntry);
        }

        public async Task<bool> CheckEntryExistsAsync(int id)
        {
            bool contactListEntryExists = await _database.ContactListEntry.AnyAsync(c => c.Id == id);

            return contactListEntryExists;
        }

        public async Task<bool> CreateEntryAsync(ContactListEntryDTO entry)
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

        public async Task<bool> UpdateEntryAsync(int id, ContactListEntryDTO entry)
        {
            ContactListEntryEntity? contactListEntry = await _database.ContactListEntry.FirstOrDefaultAsync(c => c.Id == id);

            if (contactListEntry is not null)
            {
                // update non key properties
                contactListEntry.ContactType = entry.ContactType;
                contactListEntry.Name = entry.Name;
                contactListEntry.DateOfBirth = entry.DateOfBirth;
                contactListEntry.Address = entry.Address;
                contactListEntry.PhoneNumber = entry.PhoneNumber;
                contactListEntry.Email = entry.Email;

                _database.Update(contactListEntry);

                int rowsAffected = await _database.SaveChangesAsync();

                return rowsAffected > 0;
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

        private static ContactListEntryDTO MapToDto(ContactListEntryEntity entity)
        {
            return new ContactListEntryDTO
            {
                Id = entity.Id,
                ContactType = entity.ContactType,
                Name = entity.Name,
                DateOfBirth = entity.DateOfBirth,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email
            };
        }

        private static ContactListEntryEntity MapToEntity(ContactListEntryDTO dto)
        {
            return new ContactListEntryEntity
            {
                Id = dto.Id,
                ContactType = dto.ContactType,
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email
            };
        }
    }
}
