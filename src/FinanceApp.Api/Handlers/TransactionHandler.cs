using FinanceApp.Api.Data;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
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
            return new Response<Transaction?>(null, 500, "Couldn't create the category.");
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
            var startData =
            
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
            var transaction = await context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request. && x.UserId == request.UserId);

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
