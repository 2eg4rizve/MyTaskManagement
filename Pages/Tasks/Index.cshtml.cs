using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyTaskManagement.Data;
using MyTaskManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MyTaskManagement.Pages.Tasks
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<TaskItem> TaskItem { get; set; } = new List<TaskItem>();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            TaskItem = await _context.TaskItems
                .Where(t => t.UserId == userId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }
    }
}
