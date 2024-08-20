using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser 
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // <Default> data
        // Seed, if necessary
        if(_context.Urls.AsNoTracking().Any())
        {
            return;
        }
        var urlList = new List<Url>
        {
            new Url
            {
                OriginalUrl = "url1",
                CreatedBy = "system"
            },
            new Url
            {
                OriginalUrl = "url2",
                CreatedBy = "system"
            }
        };
        await _context.Urls.AddRangeAsync(urlList);
        _= _context.SaveChangesAsync();
    }
}
