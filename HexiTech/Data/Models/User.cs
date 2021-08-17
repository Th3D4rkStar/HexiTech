﻿namespace HexiTech.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static DataConstants.User;

    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        public IEnumerable<UserShoppingCart> UsersShoppingCart { get; set; } = new List<UserShoppingCart>();

        public IEnumerable<UserOrdersList> UserOrdersLists { get; set; } = new List<UserOrdersList>();
    }
}