using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViecLam.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string? Heading { get; set; }
        public string? SubHeading { get; set; }
        public string? Poster {  get; set; }
        public DateTime BlogDate { get; set; } = DateTime.Now;
        public string? BlogDetail { get; set; }
        [Required]
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
