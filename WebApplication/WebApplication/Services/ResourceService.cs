using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.DataAccess;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class ResourceService : IResourceService
    {
        private ResourceBookingDbContext _context;

        public ResourceService(ResourceBookingDbContext resourceBookingDbContext)
        {
            this._context = resourceBookingDbContext;
        }

        public async Task<Resource> AddResource(Resource resource)
        {
            try
            {
                var resourceWithProperId = _context.Resources.Add(resource).Entity;
                await _context.SaveChangesAsync();
                return resourceWithProperId;
            }
            catch (Exception e)
            {
                throw new Exception("database adding resource exception");
            }
        }

        public async Task<int> RemoveResource(int resourceId)
        {
            try
            {
                var resourceToDelete = _context.Resources.FirstOrDefault(r => r.Id == resourceId);
                _context.Remove(resourceToDelete);
                await _context.SaveChangesAsync();
                return resourceToDelete.Id;
            }
            catch (Exception e)
            {
                throw new Exception("database remove resource exception");
            }
        }

        public async Task<List<Resource>> GetResources(int resourcesAmount)
        {
            try
            {
                return _context.Resources.Take(resourcesAmount).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("database get resource exception");
            }
        }
    }
}