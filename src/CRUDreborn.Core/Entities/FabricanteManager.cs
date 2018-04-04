using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public class FabricanteManager : IDomainService, IFabricanteManager
    {
        private IRepository<Fabricante, long> _fabricanteRepository;

        public FabricanteManager(IRepository<Fabricante, long> fabricanteRepository)
        {
            _fabricanteRepository = fabricanteRepository;
        }

        public async Task<long> Create(Fabricante fabricante)
        {
            if (fabricante.Name.Length >= 2 && fabricante.Description.Length >= 2)
            {
                if (fabricante.Name.Length <= 32)
                {
                    if(fabricante.Description.Length <= 50)
                    {
                        try
                        {
                            return await _fabricanteRepository.InsertAndGetIdAsync(fabricante);
                        }
                        catch (Exception e)
                        {
                            throw new UserFriendlyException("Error", e.Message.ToString());
                        }
                    }
                    else
                        throw new UserFriendlyException("Error","Please use less than 50 characters for the description");
                }
                else
                    throw new UserFriendlyException("Error", "Please use less than 32 characters for the name");
            }
            else
            {
                throw new UserFriendlyException("Error", "Please use more than 2 characters for the name and description");
            }
        }

        public async Task<Fabricante> Update(Fabricante fabricante)
        {
            if (fabricante.Name.Length >= 2 && fabricante.Description.Length >= 2)
            {
                if (fabricante.Name.Length <= 32)
                {
                    if (fabricante.Description.Length <= 50)
                    {
                        try
                        {
                            return await _fabricanteRepository.UpdateAsync(fabricante);
                        }
                        catch (Exception e)
                        {
                            throw new UserFriendlyException("Error", e.Message.ToString());
                        }
                    }
                    else
                        throw new UserFriendlyException("Error", "Please use less than 50 characters for the description");
                }
                else
                    throw new UserFriendlyException("Error", "Please use less than 32 characters for the name");
            }
            else
            {
                throw new UserFriendlyException("Error", "Please use more than 2 characters for the name and description");
            }
        }

        public async Task Delete(long id)
        {
            try
            {
                await _fabricanteRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public async Task<Fabricante> GetById(long id)
        {
            try
            {
                return await _fabricanteRepository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public async Task<List<Fabricante>> GetAll()
        {
            try
            {
                return await _fabricanteRepository.GetAllListAsync();
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }
    }
}
