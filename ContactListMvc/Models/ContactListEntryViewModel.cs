using ContactListMvc.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace ContactListMvc.Models
{
    public class ContactListEntryViewModel
    {
        [Required]
        public int Id {  get; set; }

        [Display(Name = "Contact Type")]
        [Required]
        public ContactType ContactType { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(500)]
        public string? Address {  get; set; }

        [Display(Name = "Phone number")]
        [RegularExpression("^(\\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\\s|\\.|\\-)?([0-9]{3}(\\s|\\.|\\-|)){2}$")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(100)]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
