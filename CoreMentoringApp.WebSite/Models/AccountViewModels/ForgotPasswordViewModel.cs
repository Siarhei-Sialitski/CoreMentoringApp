using System.ComponentModel.DataAnnotations;

namespace CoreMentoringApp.WebSite.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
