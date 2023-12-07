using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactListMvc.Models
{
    [Table("ContactList")]
    public class ContactListEntry
    {
        [Key]
        [Required]
        public int Id {  get; set; }

        [Required]
        public ContactType ContactType { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(500)]
        public string? Address {  get; set; }

        [MaxLength(100)]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
