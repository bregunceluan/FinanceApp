using FinanceApp.Api.Data;
using FinanceApp.Core;
using FinanceApp.Core.Exceptions;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Libraries.Extensions;
using FinanceApp.Core.Requests.Transactions;
using FinanceApp.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{           
    //todo log with serilog to the exceptions.
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction()
            {
                Type = request.Type,
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                CreatedAt = DateTime.UtcNow,
                Title = request.Title,

            };

            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 201, "Transaction was successful created.");
        }
        catch (Exception)
        {
            return new Response<Transaction?>(null, 500, "Couldn't create transaction.");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (transaction is null)
            {
                return new Response<Transaction?>(null, 404, "Transaction not found.");
            }

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction);
        }
        catch (Exception)
        {
            //todo log with serilog
            return new Response<Transaction?>(null, 500, "Couldn't delete the transaction.");
        }

    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {           
            var transaction = await context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
            {
                return new Response<Transaction?>(null, 404, "Transaction not found.");
            }

            return new Response<Transaction?>(transaction);

        }
        catch (Exception)
        {
            return new Response<Transaction?>(null, 500, "Couldn't get the transaction.");
        }

    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDayOfMonth();
            request.EndDate ??= DateTime.Now.GetLastDayOfMonth();

            if(request.StartDate > request.EndDate) return new PagedResponse<List<Transaction>?>(null, 404, "Date providade are not valid.");

            var query = context.Transactions
                .AsNoTracking()
                .Where(x => x.CreatedAt >= request.StartDate
                && request.EndDate <= request.EndDate
                && x.UserId == request.UserId)
                .OrderBy(t=>t.CreatedAt);


            var transactions = await query
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync();

            var count = transactions.Count();

            if (transactions is null)
            {
                return new PagedResponse<List<Transaction>?>(null, 404, "Transaction not found.");
            }

            return new PagedResponse<List<Transaction>?>(transactions,201, transactions.Any() ? "Transactions founded" : "Wasn't found any transaction at this date." );

        }
        catch (DateTimeException)
        {
            return new PagedResponse<List<Transaction>?>(null, 500, "Couldn't calculate the date.");
        }
        catch (Exception)
        {
            return new PagedResponse<List<Transaction>?>(null, 500, "Couldn't get the transactions.");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
            {
                return new Response<Transaction?>(null, 404, "Transaction not found.");
            }

            transaction.Type = request.Type;
            transaction.Amount = request.Amount;
            transaction.CategoryId = request.CategoryId;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.Title = request.Title;        

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 201, "Transaction was successful updated.");
        }
        catch (Exception)
        {
            return new Response<Transaction?>(null, 500, "Couldn't update the transaction.");
        }
    }
}
