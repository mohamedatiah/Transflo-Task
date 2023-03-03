using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransfloDriver.Models.Dto;
using TransfloDriver.Models;

namespace TransfloManager.Interface
{
    public interface IDriverService
    {
        public  Task AddDriver(DriverDTO createDTO);
        public  Task UpdateDriver(DriverDTO updateDTO);
        public  Task<bool> DeleteDriver(int id);
        public  Task<DriverDTO> GetDriver(int id);
        public Task<DriverDTO> GetDriverByEmail(string email);
        public Task<bool> IsEmailExist(DriverDTO createDTO);
        public  Task<IEnumerable<DriverDTO>> GetAll(string Createdby);


    }
}
