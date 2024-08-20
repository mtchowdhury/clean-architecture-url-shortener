using System.Reflection;
using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace UrlShortenerService.Infrastructure.Persistence;

public delegate void MyDelegate(string msg);
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DbSet<Url> Urls => Set<Url>();

    MyDelegate del  = targetmethod;
    public static void targetmethod(string msg1)
    {
        return;
    }
    public static int targetmethod2(string msg1, int num)
    {
        return 1;
    }

    Func<string, int, int> testfunc = targetmethod2;
    

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var result = testfunc("", 2);
        Func<int, int, int> de = delegate (int x , int y )
        {
            return 1;
        };
        de(1, 2);
        Func<int> del = ()=> { return 1; };
        Func<int, int, int> del1 = (int x, int y) => { return x + y; }; 
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        Action<int> tstAct = (int i )=> { return;  };    
        Action<int> tstAct2 = i => Console.Write(i) ;

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
