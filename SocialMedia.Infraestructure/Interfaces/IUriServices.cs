using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Infraestructure.Interfaces
{
    public interface IUriServices
    {
        Uri GetUri(PublicacionQueryFilter filter, string actionUrl);
    }
}