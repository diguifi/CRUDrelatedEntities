using Abp.AutoMapper;
using AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Venda.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDreborn.Venda
{
    public class VendaAppService : IVendaAppService
    {
        private IVendaManager _vendaManager;

        public VendaAppService(IVendaManager vendaManager)
        {
            _vendaManager = vendaManager;
        }

        /// <summary>
        /// Creates a 'Venda' (Sale)
        /// </summary>
        /// <param name="input">Venda's input DTO</param>
        /// <returns>Creation Output DTO containing the Id</returns>
        public long CreateVenda(CreateVendaInput input)
        {
            var venda = input.MapTo<CRUDreborn.Entities.Venda>();
            var createdVendaId = _vendaManager.Create(venda);
            return createdVendaId;
        }

        /// <summary>
        /// Deletes a 'Venda' (Sale)
        /// </summary>
        /// <param name="id">Venda's Id</param>
        /// <returns></returns>
        public async Task DeleteVenda(long id)
        {
            await _vendaManager.Delete(id);
        }

        /// <summary>
        /// Gets all stored 'Venda' (Sale)
        /// </summary>
        /// <returns>List DTO containing all stored 'Venda'</returns>
        public GetAllVendasOutput GetAllVendas()
        {
            var venda = _vendaManager.GetAll().ToList();
            var output = Mapper.Map<List<GetAllVendasItem>>(venda);
            return new GetAllVendasOutput { Vendas = output };
        }

        /// <summary>
        /// Gets the sum of the total of all stored 'Vendas' (Sales)
        /// </summary>
        /// <returns>Sum of all sales' total</returns>
        public float GetTotalVendas()
        {
            var vendas = _vendaManager.GetAll().ToList();
            float totais = 0.0f;
            foreach (var sell in vendas)
            {
                totais += sell.Total;
            }
            return totais;
        }

        /// <summary>
        /// Gets the most sold 'Produtos' (Products)
        /// </summary>
        /// <returns>JSON string</returns>
        public string GetMostSold()
        {
            var vendas = _vendaManager.GetAll().ToList();
            var dictionary = new Dictionary<string, long>();
            foreach (var sell in vendas)
            {
                if (dictionary.ContainsKey(sell.AssignedProduct.Name.ToString()))
                {
                    dictionary[sell.AssignedProduct.Name.ToString()] += sell.Quantity;
                }
                else
                {
                    dictionary.Add(sell.AssignedProduct.Name.ToString(), sell.Quantity);
                }
            }

            var sortedDict = from entry in dictionary orderby entry.Value descending select entry;

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(sortedDict);

            return output;
        }

        /// <summary>
        /// Gets 'Vendas' (Sales) per day
        /// </summary>
        /// <returns>JSON string</returns>
        public string GetDaysSales()
        {
            var vendas = _vendaManager.GetAll().ToList();
            var dictionary = new Dictionary<string, long>();
            foreach (var sell in vendas)
            {
                sell.Date = sell.CreationTime.Year.ToString() +"-"+ sell.CreationTime.Month.ToString() +"-"+ sell.CreationTime.Day.ToString();
                if (dictionary.ContainsKey(sell.Date.ToString()))
                {
                    dictionary[sell.Date.ToString()] += 1;
                }
                else
                {
                    dictionary.Add(sell.Date.ToString(), 1);
                }
            }

            var sortedDict = from entry in dictionary orderby entry.Key descending select entry;

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(sortedDict);

            return output;
        }

        /// <summary>
        /// Gets a stored 'Venda' (Sale) by Id
        /// </summary>
        /// <param name="id">Venda's Id</param>
        /// <returns>Venda's DTO</returns>
        public async Task<GetVendaByIdOutput> GetById(long id)
        {
            var venda = await _vendaManager.GetById(id);
            return venda.MapTo<Dtos.GetVendaByIdOutput>();
        }

        /// <summary>
        /// Updates a 'Venda' (Sale)
        /// </summary>
        /// <param name="input">Venda's input DTO</param>
        /// <returns>Venda's DTO</returns>
        public async Task<UpdateVendaOutput> UpdateVenda(UpdateVendaInput input)
        {
            var venda = input.MapTo<CRUDreborn.Entities.Venda>();
            var vendaUpdated = await _vendaManager.Update(venda);
            return vendaUpdated.MapTo<UpdateVendaOutput>();
        }
    }
}
