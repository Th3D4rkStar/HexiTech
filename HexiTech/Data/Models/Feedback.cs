namespace HexiTech.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}