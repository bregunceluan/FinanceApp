using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Request;

public abstract class PagedRequest : Request
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 25;
}
