using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WilmerFlorez.Domain.Configuration.Commands.Permission;
using WilmerFlorez.Domain.Configuration.Output;
using WilmerFlorez.Queries.Interfaces;

namespace WilmerFlorez.Api.Controllers
{
    [Route("permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPermissionQueryService _permissionQueryService;

        public PermissionController(IMediator mediator, 
            IPermissionQueryService permissionQueryService)
        {
            _mediator = mediator;
            _permissionQueryService = permissionQueryService;
        }

        /// <summary>
        /// Service to create permission
        /// </summary>
        /// <param name="input"></param>
        [HttpPost("request-permission")]
        public async Task<PermissionOutput> RequestPermission([FromBody] PermissionCreateCommand input)
        {
            var result = await _mediator.Send(input);
            return result;
        }

        /// <summary>
        /// Service to update espcific permission
        /// </summary>
        /// <param name="input"></param>
        [HttpPut("update-permission")]
        public async Task<PermissionOutput> UpdatePermission([FromBody] PermissionUpdateCommand input)
        {
            var result = await _mediator.Send(input);
            return result;
        }

        /// <summary>
        /// Service to get all saved permissions 
        /// </summary>
        /// <param name="input"></param>
        [HttpGet("get-permissions")]
        public async Task<List<PermissionOutput>> GetPermissions()
        {
            var result = await _permissionQueryService.GetAllAsync();
            return result;
        }
    }
}
