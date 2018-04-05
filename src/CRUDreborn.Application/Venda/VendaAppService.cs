using Abp.AutoMapper;
using AutoMapper;
using CRUDreborn.Entities;
using CRUDreborn.Venda.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void CreateVenda(CreateVendaInput input)
        {
            var venda = input.MapTo<CRUDreborn.Entities.Venda>();
            _vendaManager.Create(venda);
        }

        public async Task DeleteVenda(long id)
        {
            await _vendaManager.Delete(id);
        }

        public GetAllVendasOutput GetAllVendas()
        {
            var venda = _vendaManager.GetAll().ToList();
            var output = Mapper.Map<List<GetAllVendasItem>>(venda);
            return new GetAllVendasOutput { Vendas = output };
        }

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

        public string GetDaysSales()
        {
            var vendas = _vendaManager.GetAll().ToList();
            var dictionary = new Dictionary<string, long>();
            foreach (var sell in vendas)
            {
                sell.Date = sell.CreationTime.Year.ToString() +"-"+ sell.CreationTime.Month.ToString() +"-"+ sell.CreationTime.Day.ToString();
                if (dictionary.ContainsKey(sell.Date.ToString()))
                {
                    dictionary[sell.Date.ToString()] += sell.Quantity;
                }
                else
                {
                    dictionary.Add(sell.Date.ToString(), sell.Quantity);
                }
            }

            var sortedDict = from entry in dictionary orderby entry.Key descending select entry;

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(sortedDict);

            return output;
        }

        public async Task<GetVendaByIdOutput> GetById(long id)
        {
            var venda = await _vendaManager.GetById(id);
            return venda.MapTo<Dtos.GetVendaByIdOutput>();
        }

        public async Task<UpdateVendaOutput> UpdateVenda(UpdateVendaInput input)
        {
            var venda = input.MapTo<CRUDreborn.Entities.Venda>();
            var vendaUpdated = await _vendaManager.Update(venda);
            return vendaUpdated.MapTo<UpdateVendaOutput>();
        }
    }
}
