using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

namespace NOAM_ASISTENCIA_v3.Server.Helpers.Services;

public class UserService(ApplicationDbContext context) : IUserService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.Users
            .Where(model => !model.Lockout)
            .Where(model => model.UserName == username)
            .AnyAsync();
    }
}
