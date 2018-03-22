﻿using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using CRUDreborn.Authorization.Roles;
using CRUDreborn.Authorization.Users;
using CRUDreborn.Fabricante.Dtos;
using CRUDreborn.Produto.Dtos;
using CRUDreborn.Roles.Dto;
using CRUDreborn.Users.Dto;

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
                .ConstructUsing(x => new CRUDreborn.Entities.Produto(x.Name, x.Description, x.AssignedManufacturer, x.Consumable));

                config.CreateMap<UpdateProdutoInput, CRUDreborn.Entities.Produto>()
                .ConstructUsing(x => new CRUDreborn.Entities.Produto(x.Name, x.Description, x.AssignedManufacturer, x.Consumable));
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