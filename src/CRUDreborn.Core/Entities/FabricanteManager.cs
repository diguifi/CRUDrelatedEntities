using Abp.Domain.Repositories;
using Abp.Domain.Services;
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
            return await _fabricanteRepository.InsertAndGetIdAsync(fabricante);
        }

        public async Task<Fabricante> Update(Fabricante fabricante)
        {
            return await _fabricanteRepository.UpdateAsync(fabricante);
        }

        public async Task Delete(long id)
        {
            await _fabricanteRepository.DeleteAsync(id);
        }

        public async Task<Fabricante> GetById(long id)
        {
            return await _fabricanteRepository.GetAsync(id);
        }

        public async Task<List<Fabricante>> GetAll()
        {
            return await _fabricanteRepository.GetAllListAsync();
        }
    }
}
