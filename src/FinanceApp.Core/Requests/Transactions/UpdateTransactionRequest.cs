using FinanceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Requests.Transactions;

public class UpdateTransactionRequest : Request
{
    [Required(ErrorMessage = "Invalid title")]
    [MaxLength(80, ErrorMessage = "Title is limited to only 80 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Id is invalid")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Type is invalid")]
    public ETransactionType Type { get; set; }

    [Required(ErrorMessage = "Amount is invalid")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Category is invalid")]
    public long CategoryId { get; set; }

    [Required(ErrorMessage = "Date is invalid")]
    public DateTime? PaidOrReceivedAt { get; set; }
}
