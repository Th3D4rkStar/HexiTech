﻿namespace HexiTech.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int Quantity { get; set; }

        public string DateCreated { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}