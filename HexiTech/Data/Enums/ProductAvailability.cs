namespace HexiTech.Data.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ProductAvailability
    {
        [Display(Name = "Available.")]
        Available = 1,
        [Display(Name = "At warehouse.")]
        AtWarehouse = 2,
        [Display(Name = "Out of stock.")]
        OutOfStock = 3
    }
}