using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Library.Models
{
    public class BorrowingDetail
    {
        public int Id { get; set; }

        [ForeignKey("BorrowingHistory")]
        public int BorrowingHistoryId { get; set; }
        public virtual BorrowingHistory? BorrowingHistory { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public virtual Item? Item { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Return Date")]
        public DateTime ReturnDate { get; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }

    }
}
