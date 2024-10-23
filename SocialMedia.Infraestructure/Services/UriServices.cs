using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infraestructure.Interfaces;

namespace SocialMedia.Infraestructure.Services
{
    public class UriServices: IUriServices
    {
        private readonly string _baseUri;

        public UriServices(string baseUri){
            _baseUri = baseUri;
        }



        public Uri GetUri(PublicacionQueryFilter filter, string actionUrl){
                
                string baseUri= $"{_baseUri}{actionUrl}"; 

                return new Uri(baseUri);
        }
    }
}