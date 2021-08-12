namespace HexiTech.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static DataConstants.User;

    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }
        
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}