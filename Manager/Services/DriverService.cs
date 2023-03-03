using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransfloDriver.Models;
using TransfloDriver.Models.Dto;
using TransfloDriver.Repository.IRepostiory;
using TransfloManager.Interface;
using TransfloRepository.Interface;

namespace TransfloManager.Services
{
    public class DriverService : IDriverService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DriverService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
         
            _mapper = mapper;
        }


        public async Task AddDriver(DriverDTO createDTO)
        {
            Driver driver = _mapper.Map<Driver>(createDTO);
            _unitOfWork.DriverRepository.Add(driver);
            await _unitOfWork.CommitAsync();
        }
        public async Task UpdateDriver(DriverDTO updateDTO)
        {
            var driver=await GetDriver(updateDTO.Id);
            updateDTO.Createdby = driver.Createdby;
            updateDTO.IsAdmin = driver.IsAdmin;
            Driver model = _mapper.Map<Driver>(updateDTO);

             _unitOfWork.DriverRepository.Update(model);
            await _unitOfWork.CommitAsync();
        }
        public async Task< bool> DeleteDriver(int id)
        {
            var driver =await _unitOfWork.DriverRepository.GetAsync(u => u.Id == id);
            if (driver == null) return false;
            _unitOfWork.DriverRepository.Remove(driver);
            await _unitOfWork.CommitAsync();

            return true;//deleted
        }
        public async Task<DriverDTO> GetDriver(int id)
        {
            var driver =await _unitOfWork.DriverRepository.GetAsync(u => u.Id == id);
            if (driver == null) return null;
            var res = _mapper.Map<DriverDTO>(driver);
            return res;
        }
        public async Task<DriverDTO> GetDriverByEmail(string email)
        {
            var driver = await _unitOfWork.DriverRepository.GetAsync(u => u.Email == email);
            if (driver == null) return null;
            var res = _mapper.Map<DriverDTO>(driver);
            return res;
        }
        public async Task<bool> IsEmailExist(DriverDTO createDTO)
        {
            var res=await _unitOfWork.DriverRepository.GetAsync(u => u.Email.ToLower() == createDTO.Email.ToLower()) != null;
            return res;
        }
        public async Task<IEnumerable<DriverDTO>> GetAll(string Createdby) 
        {

           var drivers= await _unitOfWork.DriverRepository.GetAllAsync(a=>a.Createdby== Createdby);
            var res=_mapper.Map<IEnumerable<DriverDTO>>(drivers);
            return res;
        }

    }
}
