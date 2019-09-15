using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class Payment : BaseEntity
    {
        [Required]
        public DateTimeOffset PaymentDate { get; set; }

        [Range(0.01, 99999999999.99, ErrorMessage = "Value must be a positive number.")]
        public decimal Amount { get; set; }

        [DisplayName("Payment Mode")]
        [StringLength(50)]
        [Required]
        public string PaymentMode { get; set; }         // cash, check, credit-debit card

        [DisplayName("Check Num")]
        [StringLength(50)]
        public string CheckNo { get; set; }

        [DisplayName("Check Image")]
        [StringLength(200)]
        public string CheckImage { get; set; }

        [DisplayName("Check Writer")]
        [StringLength(50)]
        public string CheckWriter { get; set; }         // Juan García, etc.

        [DisplayName("Card Holder")]
        [StringLength(50)]
        public string CreditCardHolder { get; set; }    // Juan García, etc.

        [DisplayName("Credit Card Num")]
        [StringLength(50)]
        public string CreditCardNo { get; set; }

        [DisplayName("Credit Card")]
        [StringLength(30)]
        public string CreditCardName { get; set; }      // VISA, Mastercard, Discovery

        [DisplayName("Source")]
        [StringLength(50)]
        [Required]
        public string ReceivableSource { get; set; }    // Online Orders, POS Sales, Layaway, Invoices

        [DisplayName("Source Id")]
        [StringLength(128)]
        [Required]
        public string ReceivableSourceId { get; set; }
    }
}