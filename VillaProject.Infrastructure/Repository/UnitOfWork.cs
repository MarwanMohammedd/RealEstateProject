using VillaProject.Application.Repository;
using VillaProject.Domain.Entities;
using VillaProject.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _applicationBDContext;

    public IGenericRepository<Villa> Villas { get; private set; }
    public IGenericRepository<VillaNumber> VillaNumbers { get; private set; }

    public UnitOfWork(ApplicationDBContext _applicationBDContext)
    {
        this._applicationBDContext = _applicationBDContext;
        Villas = new GenericRepository<Villa>(this._applicationBDContext);
        VillaNumbers = new GenericRepository<VillaNumber>(this._applicationBDContext);
    }
    public void Dispose()
    {
        _applicationBDContext.Dispose();
    }
    public void Save()
    {
        _applicationBDContext.SaveChanges();
    }
}