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
        private IVendaRepository _vendaRepository;

        public VendaManager(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public void Create(Venda venda)
        {
            _vendaRepository.InsertAndAttach(venda);
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

        public IEnumerable<Venda> GetAll()
        {
            return _vendaRepository.GetAllIncluding(x => x.AssignedProduct);
        }
    }
}
