using SocialMedia.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Entities
{
    public class Seguridad:BaseEntity
    {
        public string User { get; set; }=string.Empty;

        public string UserName { get; set; }= string.Empty;

        public string Password { get; set; } = string.Empty;

        public RolType Role {  get; set; }
    }
}
