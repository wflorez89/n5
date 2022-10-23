using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using WilmerFlorez.Domain.Configuration.Output;

namespace WilmerFlorez.Domain.Configuration.Commands.Permission
{
    public class PermissionCreateCommand : IRequest<PermissionOutput>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public Guid? PermissionTypeId { get; set; }
    }
}
