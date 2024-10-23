
using FluentValidation;
using SocialMedia.Core.DTO;

namespace SocialMedia.Infraestructure.Validators
{
    public class PublicacionValidator:AbstractValidator<PublicacionDTO>
    {

        public PublicacionValidator(){
            RuleFor(publicacion=>publicacion.Descripcion)
                  .NotNull()
                  .Length(10,1000);
            RuleFor(publicacion=>publicacion.Fecha)
                  .NotNull()
                  .LessThan(DateTime.Now);
                  
        }
        
    }
}