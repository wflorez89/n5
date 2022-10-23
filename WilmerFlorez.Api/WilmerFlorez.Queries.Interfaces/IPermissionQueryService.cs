using WilmerFlorez.Domain.Configuration.Output;

namespace WilmerFlorez.Queries.Interfaces
{
    public  interface IPermissionQueryService
    {
        Task<List<PermissionOutput>> GetAllAsync();
    }
}
