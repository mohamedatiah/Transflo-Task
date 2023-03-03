
using TransfloDriver.Data;
using TransfloDriver.Models;
using TransfloDriver.Repository.IRepostiory;
using TransfloRepository.Repository;

namespace TransfloDriver.Repository
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
