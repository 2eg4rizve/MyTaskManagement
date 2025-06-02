using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyTaskManagement.Data;
using MyTaskManagement.Models;

namespace MyTaskManagement.Pages.Tasks;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public DeleteModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public TaskItem TaskItem { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var userId = _userManager.GetUserId(User);
        TaskItem = await _context.TaskItems.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (TaskItem == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var task = await _context.TaskItems.FindAsync(TaskItem.Id);
        if (task != null && task.UserId == _userManager.GetUserId(User))
        {
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}
