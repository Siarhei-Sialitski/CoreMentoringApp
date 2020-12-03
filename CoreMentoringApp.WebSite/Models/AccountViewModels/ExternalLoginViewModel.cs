using System.ComponentModel.DataAnnotations;

namespace CoreMentoringApp.WebSite.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
