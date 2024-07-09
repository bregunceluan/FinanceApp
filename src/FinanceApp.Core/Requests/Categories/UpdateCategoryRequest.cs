using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Requests.Categories;

public class UpdateCategoryRequest : Request
{
    [Required]
    public long Id { get; set; }    

    [Required(ErrorMessage = "Invalid title")]
    [MaxLength(80, ErrorMessage = "Title is limited to only 80 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Invalid title")]
    [MinLength(5, ErrorMessage = "Description is too short, it requires mode than 5 characters.")]
    public string Description { get; set; } = string.Empty;
}
