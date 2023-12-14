namespace ContactListMvc.Infrastructure.Models
{
    public sealed class ContactListEntryDTO
    {
        public int Id { get; set; }

        public int ContactType { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
