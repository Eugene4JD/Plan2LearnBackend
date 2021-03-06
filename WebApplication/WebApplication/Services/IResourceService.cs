using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IResourceService
    {
        public Task<Resource> AddResource(Resource resource);
        public Task<int> RemoveResource(int resourceId);

        public Task<List<Resource>> GetResources(int resourcesAmount);
    }
}