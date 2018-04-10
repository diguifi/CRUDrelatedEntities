using Abp.Application.Services;
using CRUDreborn.Estoque.Dtos;
using CRUDreborn.Produto.Dtos;
using System.Threading.Tasks;

namespace CRUDreborn.Produto
{
    public interface IProdutoAppService : IApplicationService
    {
        CreateProdutoOutput CreateProduto(CreateProdutoInput input);
        Task<UpdateProdutoOutput> UpdateProduto(UpdateProdutoInput input);
        Task DeleteProduto(long id);
        Task<GetProdutoByIdOutput> GetById(long id);
        GetAllProdutosOutput GetAllProdutos();
        GetAllProdutosOutput GetAllProdutosCheckingEstoque();
        GetAllEstoqueOutput GetAllAssignedEstoque(long prod_id);
    }
}
