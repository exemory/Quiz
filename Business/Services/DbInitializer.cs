using Business.Interfaces;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class DbInitializer : IDbInitializer
{
    private readonly QuizContext _context;

    public DbInitializer(QuizContext context, UserManager<User> userManager)
    {
        _context = context;
    }

    public async Task Initialize()
    {
        await _context.Database.MigrateAsync();
    }
}