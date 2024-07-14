using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Requests.Transactions;

public class DeleteTransactionRequest : Request
{
    [Required]
    public long Id { get; set; } 
}
