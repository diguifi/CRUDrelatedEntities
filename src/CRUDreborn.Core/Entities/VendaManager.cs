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
    public class VendaManager : IDomainService, IVendaManager
    {
        private IVendaRepository _vendaRepository;

        public VendaManager(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public long Create(Venda venda)
        {
            if (venda.AssignedProduct != null)
            {
                if (venda.Quantity.GetType() == typeof(long))
                {
                    if (venda.Total.GetType() == typeof(float))
                    {
                        try
                        {
                            _vendaRepository.InsertAndAttach(venda);
                            return _vendaRepository.InsertAndGetId(venda);
                        }
                        catch (Exception e)
                        {
                            throw new UserFriendlyException("Error", e.Message.ToString());
                        }
                    }
                    else
                        throw new UserFriendlyException("Error", "Please insert a float value for the total of sales");
                }
                else
                    throw new UserFriendlyException("Error", "Please insert an integer number for the quantity of sales");
            }
            else
                throw new UserFriendlyException("Error", "Please select a product to be sold");
        }

        public async Task<Venda> Update(Venda venda)
        {
            if (venda.AssignedProduct != null)
            {
                if (venda.Quantity.GetType() == typeof(long))
                {
                    if (venda.Total.GetType() == typeof(float))
                    {
                        try
                        {
                            return await _vendaRepository.UpdateAsync(venda);
                        }
                        catch (Exception e)
                        {
                            throw new UserFriendlyException("Error", e.Message.ToString());
                        }
                    }
                    else
                        throw new UserFriendlyException("Error", "Please insert a float value for the total of sales");
                }
                else
                    throw new UserFriendlyException("Error", "Please insert an integer number for the quantity of sales");
            }
            else
                throw new UserFriendlyException("Error", "Please select a product to be sold");
        }

        public async Task Delete(long id)
        {
            try
            {
                await _vendaRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public async Task<Venda> GetById(long id)
        {
            try
            {
                return await _vendaRepository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public IEnumerable<Venda> GetAll()
        {
            try
            {
                return _vendaRepository.GetAllIncluding(x => x.AssignedProduct);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }
    }
}
