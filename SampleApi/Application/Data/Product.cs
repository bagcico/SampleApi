using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.Application.Data
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentHtml { get; set; }
        public double? Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}