using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyTaskManagement.Data;
using MyTaskManagement.Models;

namespace MyTaskManagement.Pages.Tasks;

[Authorize]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public TaskItem TaskItem { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var userId = _userManager.GetUserId(User);
        TaskItem = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (TaskItem == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var existing = await _context.TaskItems.AsNoTracking().FirstOrDefaultAsync(t => t.Id == TaskItem.Id && t.UserId == _userManager.GetUserId(User));
        if (existing == null) return NotFound();

        TaskItem.UserId = existing.UserId; // Ensure user ID is not changed
        _context.Attach(TaskItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
