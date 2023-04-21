using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class BorrowingHistory
    {
        public int Id { get; set; }

        [ForeignKey("Borrower")]
        public int BorrowerId { get; set; }
        public virtual Borrower? Borrower { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Borrow Date")]
        public DateTime BorrowDate { get; }
    }
}
