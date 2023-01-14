using SSDB.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Server.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UniversityId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GroupSid);
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Claims = httpContextAccessor.HttpContext?.User?.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
        }

        public string UserId { get; }
        public string UniversityId { get; }
        public List<KeyValuePair<string, string>> Claims { get; set; }
    }
}