using FinanceApp.Api.Data;
using FinanceApp.Core;
using FinanceApp.Core.Handlers;
using FinanceApp.Core.Requests.Categories;
using FinanceApp.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{

    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {

        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Description = request.Description,
                Title = request.Title,
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Category successfull created.");

        }
        catch (Exception)
        {
            return new Response<Category?>(null, 500, "Couldn't create the category.");
        }

    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
            {
                return new Response<Category?>(null, 404, "Category not found.");
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, message: "Category successful deleted.");

        }
        catch (Exception)
        {
            //todo log with serilog
            return new Response<Category?>(null, 500, "Couldn't delete the category.");
        }
    }

    public async Task<PagedResponse<List<Category?>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context.Categories
                .AsNoTracking()
                .Where(c => c.UserId == "luan@gmail.com");

            var categories = await query
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category?>>(categories, count, request.PageNumber, request.PageSize);

        }
        catch (Exception)
        {
            return new PagedResponse<List<Category?>>(null, 500, "Failed to obtain the categories.");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
            {
                return new Response<Category?>(null, 404, "Category not found.");
            }


            return new Response<Category?>(category);

        }
        catch (Exception)
        {
            //todo log with serilog
            return new Response<Category?>(null, 500, "Couldn't find the category.");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
            {
                return new Response<Category?>(null, 404, "Category not found.");
            }

            category.Title = request.Title;
            category.Title = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, message: "Category successful created.");

        }
        catch (Exception)
        {
            //todo log with serilog
            return new Response<Category?>(null, 500, "Couldn't update the category.");
        }
    }
}
