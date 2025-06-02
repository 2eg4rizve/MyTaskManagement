using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTaskManagement.Data;
using MyTaskManagement.Models;

namespace MyTaskManagement.Pages.Tasks;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public TaskItem TaskItem { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        TaskItem.UserId = _userManager.GetUserId(User);
        _context.TaskItems.Add(TaskItem);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
