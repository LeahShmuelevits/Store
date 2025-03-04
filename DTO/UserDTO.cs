using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record  UserDTO(
        [EmailAddress(ErrorMessage ="כתובת מייל לא תקינה")]
         [Required]
       [StringLength(50, ErrorMessage = "UserName max 50")]
        string UserName,
        string? FirstName,
        string? LastName, 
        [Required]
         [StringLength(20, ErrorMessage = "password between 5-20", MinimumLength = 5)]
    string Password);
    public record  GetUserDTO(int UserId,string UserName, string? FirstName, string? LastName);

}
