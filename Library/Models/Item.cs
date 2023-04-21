using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        public string? RunTime { get; set; }
        public int? NumOfPage { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }


        public Category? Category { get; set; }

    }
}
