using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Core.Services
{
    public class SeguridadService:ISeguridadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeguridadService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<Seguridad?> GetLoginByCredencial(UserLogin userLogin)
        {
            return await _unitOfWork.SeguridadRepository.GetLoginByCredencial(userLogin);
        }


        public async Task RegisterUser(Seguridad seguridad)
        {
            await _unitOfWork.SeguridadRepository.Add(seguridad);
            await _unitOfWork.SaveChangeAsync();


            
        }
    }
}
