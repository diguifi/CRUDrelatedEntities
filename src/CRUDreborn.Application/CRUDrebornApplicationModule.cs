using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using CRUDreborn.Authorization.Roles;
using CRUDreborn.Authorization.Users;
using CRUDreborn.Estoque.Dtos;
using CRUDreborn.Fabricante.Dtos;
using CRUDreborn.Produto.Dtos;
using CRUDreborn.Roles.Dto;
using CRUDreborn.Users.Dto;
using CRUDreborn.Venda.Dtos;

namespace CRUDreborn
{
    [DependsOn(typeof(CRUDrebornCoreModule), typeof(AbpAutoMapperModule))]
    public class CRUDrebornApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<CreateFabricanteInput, CRUDreborn.Entities.Fabricante>()
                .ConstructUsing(x => new CRUDreborn.Entities.Fabricante(x.Name, x.Description));

                config.CreateMap<UpdateFabricanteInput, CRUDreborn.Entities.Fabricante>()
                .ConstructUsing(x => new CRUDreborn.Entities.Fabricante(x.Name, x.Description));

                config.CreateMap<CreateProdutoInput, CRUDreborn.Entities.Produto>()
                .ConstructUsing(x => new CRUDreborn.Entities.Produto(x.Name, x.Description, x.AssignedManufacturer_Id, x.AssignedManufacturer, x.Consumable));

                config.CreateMap<UpdateProdutoInput, CRUDreborn.Entities.Produto>()
                .ConstructUsing(x => new CRUDreborn.Entities.Produto(x.Name, x.Description, x.AssignedManufacturer_Id, x.AssignedManufacturer, x.Consumable));

                config.CreateMap<CRUDreborn.Entities.Produto, GetAllProdutosOutput>().ReverseMap();

                config.CreateMap<CreateEstoqueInput, CRUDreborn.Entities.Estoque>()
                .ConstructUsing(x => new CRUDreborn.Entities.Estoque(x.Stock, x.Price, x.AssignedProduct_Id, x.AssignedProduct));

                config.CreateMap<UpdateEstoqueInput, CRUDreborn.Entities.Estoque>()
                .ConstructUsing(x => new CRUDreborn.Entities.Estoque(x.Stock, x.Price, x.AssignedProduct_Id, x.AssignedProduct));

                config.CreateMap<CRUDreborn.Entities.Estoque, GetAllEstoqueOutput>().ReverseMap();

                config.CreateMap<CreateVendaInput, CRUDreborn.Entities.Venda>()
                .ConstructUsing(x => new CRUDreborn.Entities.Venda(x.AssignedProduct_Id, x.AssignedProduct, x.Quantity, x.Total));

                config.CreateMap<UpdateVendaInput, CRUDreborn.Entities.Venda>()
                .ConstructUsing(x => new CRUDreborn.Entities.Venda(x.AssignedProduct_Id, x.AssignedProduct, x.Quantity, x.Total));
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // TODO: Is there somewhere else to store these, with the dto classes
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Role and permission
                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
            });
        }
    }
}
