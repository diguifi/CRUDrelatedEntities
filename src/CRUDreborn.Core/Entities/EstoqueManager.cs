using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public class EstoqueManager : IDomainService, IEstoqueManager
    {
        private IEstoqueRepository _estoqueRepository;

        public EstoqueManager(IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        public long Create(Estoque estoque, IEnumerable<Estoque> estoques)
        {
            bool duplicada = false;

            foreach (var est in estoques)
            {
                if (est.AssignedProduct_Id == estoque.AssignedProduct.Id)
                    duplicada = true;
            }

            if (!duplicada)
            {
                if (estoque.Stock.GetType() == typeof(long))
                {
                    if (estoque.Price.GetType() == typeof(float))
                    {
                        if (estoque.AssignedProduct != null)
                        {
                            try
                            {
                                _estoqueRepository.InsertAndAttach(estoque);
                                return _estoqueRepository.InsertAndGetId(estoque);
                            }
                            catch (Exception e)
                            {
                                throw new UserFriendlyException("Error 5", e.Message.ToString());
                            }
                        }
                        else
                        {
                            throw new UserFriendlyException("Error 4", "Please select a product");
                        }
                    }
                    else
                        throw new UserFriendlyException("Error 3", "Please insert a float value for the price");
                }
                else
                {
                    throw new UserFriendlyException("Error 2", "Please insert an integer number for stock");
                }
            }
            else
            {
                throw new UserFriendlyException("Error 1", "Product is already in stock");
            }
        }

        public async Task<Estoque> Update(Estoque estoque)
        {
            
                if (estoque.Stock.GetType() == typeof(long))
                {
                    if (estoque.Price.GetType() == typeof(float))
                    {
                            try
                            {
                                return await _estoqueRepository.UpdateAsync(estoque);
                            }
                            catch (Exception e)
                            {
                                throw new UserFriendlyException("Error 5", e.Message.ToString());
                            }
                    }
                    else
                        throw new UserFriendlyException("Error 3", "Please insert a float value for the price");
                }
                else
                {
                    throw new UserFriendlyException("Error 2", "Please insert an integer number for stock");
                }
            
        }

        public async Task<Estoque> UpdateQuantity(Estoque estoque)
        {
                if (estoque.Stock.GetType() == typeof(long))
                {
                    try
                    {
                        return await _estoqueRepository.UpdateAsync(estoque);
                    }
                    catch (Exception e)
                    {
                        throw new UserFriendlyException("Error", e.Message.ToString());
                    }
                }
                else
                {
                    throw new UserFriendlyException("Error", "Please insert an integer number for stock");
                }
        }

        public async Task Delete(long id)
        {
            try
            {
                await _estoqueRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public async Task<Estoque> GetById(long id)
        {
            try
            {
                return await _estoqueRepository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public IEnumerable<Estoque> GetAll()
        {
            try
            {
                return _estoqueRepository.GetAllIncluding(x => x.AssignedProduct);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public IEnumerable<Estoque> GetAllFromProduto(long prod_id)
        {
            try
            {
                IEnumerable<Estoque> estoque = GetAll();
                IEnumerable<Estoque> filteringQuery = estoque.Where(e => e.AssignedProduct_Id == prod_id);

                return filteringQuery;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }
    }
}
