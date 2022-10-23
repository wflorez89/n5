using AutoMapper;
using System;
using WilmerFlorez.Domain.Configuration.Commands.Permission;
using WilmerFlorez.Domain.Configuration.Output;
using WilmerFlorez.Domain.Entities;

namespace WilmerFlorez.Domain.Configuration.Mappers
{
    public class PermissionMapper : Profile
    {
        public PermissionMapper()
        {
            CreateMap<PermissionCreateCommand, Permission>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => Guid.NewGuid()));

            CreateMap<Permission, PermissionOutput>();

            CreateMap<PermissionUpdateCommand, Permission>();
        }
    }
}
