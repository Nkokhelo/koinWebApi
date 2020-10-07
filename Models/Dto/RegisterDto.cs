using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace koinfast.Models.Dto
{
  public class RegisterDto
  {

    [Required]
    public Title Title { get; set; }

    [Required]
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "{0} is invalid, please provide a valid {0}")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Name { get; set; }

    [Required]
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "{0} is invalid, please provide a valid {0}")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
    public string Surname { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "{0} must have digits only")]
    [StringLength(13, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Phone { get; set; }

    [Required]
    public string Country { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "{0} must have digits only")]
    [Display(Name = "Account Number")]
    [StringLength(13, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]

    public string AccNo { get; set; }

    [Required]
    [StringLength(13, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "{0} is invalid, please provide a valid {0}")]
    [Display(Name = "Bank Name")]
    public string Bank { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public int SponsorId { get; set; }


  }
}