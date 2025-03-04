using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities;

public partial class User
{
    [Required]

    public int UserId { get; set; }


    [EmailAddress]
    [Required]
    [StringLength(50, ErrorMessage = "UserName max 50")]
    public string UserName { get; set; } = null!;

    [StringLength(20, ErrorMessage = "password between 5-20", MinimumLength = 5)]
    [Required]
    public string Password { get; set; } = null!;

    [StringLength(50, ErrorMessage = "FirstName max 50")]
    public string? FirstName { get; set; }

    [StringLength(50, ErrorMessage = "LastName max 50")]
    public string? LastName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}



