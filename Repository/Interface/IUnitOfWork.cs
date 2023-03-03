using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransfloDriver.Repository.IRepostiory;

namespace TransfloRepository.Interface
{
    public interface IUnitOfWork
    {
        IDriverRepository DriverRepository { get; }
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
