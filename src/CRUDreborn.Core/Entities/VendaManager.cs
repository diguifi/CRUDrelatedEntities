using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public class VendaManager : IDomainService, IVendaManager
    {
        private IRepository<Venda, long> _vendaRepository;

        public VendaManager(IRepository<Venda, long> vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public async Task<long> Create(Venda venda)
        {
            return await _vendaRepository.InsertAndGetIdAsync(venda);
        }

        public async Task<Venda> Update(Venda venda)
        {
            return await _vendaRepository.UpdateAsync(venda);
        }

        public async Task Delete(long id)
        {
            await _vendaRepository.DeleteAsync(id);
        }

        public async Task<Venda> GetById(long id)
        {
            return await _vendaRepository.GetAsync(id);
        }

        public async Task<List<Venda>> GetAll()
        {
            return await _vendaRepository.GetAllListAsync();
        }
    }
}
