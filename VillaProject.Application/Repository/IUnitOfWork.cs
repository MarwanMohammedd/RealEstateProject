using VillaProject.Application.Repository;
using VillaProject.Domain.Entities;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<Villa> Villas { get; }
    public IGenericRepository<VillaNumber> VillaNumbers { get; }
    void Save();
}