using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactListMvc.Infrastructure.Entities
{
    [Table("ContactList")]
    internal class ContactListEntryEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ContactType { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [RegularExpression("^(\\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\\s|\\.|\\-)?([0-9]{3}(\\s|\\.|\\-|)){2}$")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(100)]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
