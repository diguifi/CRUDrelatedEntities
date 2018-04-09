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
    public class ProdutoManager : IDomainService, IProdutoManager
    {
        private IProductRepository _produtoRepository;

        public ProdutoManager(IProductRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public long Create(Produto produto)
        {
            if (produto.Name.Length >= 2 && produto.Description.Length >= 2)
            {
                if (produto.Name.Length <= 32)
                {
                    if (produto.Description.Length <= 50)
                    {
                        if (produto.AssignedManufacturer != null)
                        {
                            try
                            {
                                _produtoRepository.InsertAndAttach(produto);
                                return _produtoRepository.InsertAndGetId(produto);
                            }
                            catch (Exception e)
                            {
                                throw new UserFriendlyException("Error", e.Message.ToString());
                            }
                        }
                        else
                        {
                            throw new UserFriendlyException("Error", "Please select a manufacturer");
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

        public async Task<Produto> Update(Produto produto)
        {
            if (produto.Name.Length >= 2 && produto.Description.Length >= 2)
            {
                if (produto.Name.Length <= 32)
                {
                    if (produto.Description.Length <= 50)
                    {
                        if (produto.AssignedManufacturer != null)
                        {
                            try
                            {
                                return await _produtoRepository.UpdateAsync(produto);
                            }
                            catch (Exception e)
                            {
                                throw new UserFriendlyException("Error", e.Message.ToString());
                            }
                        }
                        else
                        {
                            throw new UserFriendlyException("Error", "Please select a manufacturer");
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
                await _produtoRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public async Task<Produto> GetById(long id)
        {
            try
            {
                return await _produtoRepository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public IEnumerable<Produto> GetAll()
        {
            try
            {
                return _produtoRepository.GetAllIncluding(x => x.AssignedManufacturer);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

        public IEnumerable<Produto> GetAllFromFabricante(long fab_id)
        {
            try
            {
                IEnumerable<Produto> produtos = GetAll();
                IEnumerable<Produto> filteringQuery = produtos.Where(p => p.AssignedManufacturer_Id == fab_id);

                return filteringQuery;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Error", e.Message.ToString());
            }
        }

    }
}
