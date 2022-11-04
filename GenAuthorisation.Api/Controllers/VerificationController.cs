using GenAuthorisation.Api.Enumerations;
using GenCommon.Shared.Extensions;
using GenCore.Data.Models;
using GenCore.Data.Repositories.Interface;
using GenCryptography.Service.Utilities.Interface;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GenAuthorisation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VerificationController : ControllerBase
    {
        private readonly IUserVerificationRepository _userVerificationRepo;
        private readonly IDecryptor _decryptor;

        public VerificationController(IDecryptor decryptor, IUserVerificationRepository userVerificationRepository)
        {
            _userVerificationRepo = userVerificationRepository;
            _decryptor = decryptor;
        }

        [HttpGet(Name = "Verify")]
        public IActionResult Verify(UserContactTypeEnum mode, string user)
        {
            string decryptedUser = _decryptor.Decrypt(new byte[] { }, user);

            if (decryptedUser.IsEmpty())
            {
                return BadRequest("Invalid user content");
            }

            User retrievedUser = decryptedUser.ToObj<User>();

            if (retrievedUser is null)
            {
                return BadRequest("Invalid user");
            }

            if (mode == UserContactTypeEnum.Email)
            {
                _userVerificationRepo.UpdateEmailVerified(retrievedUser.UserId);
            }
            else if (mode == UserContactTypeEnum.Phone)
            {
                _userVerificationRepo.UpdatePhoneVerified(retrievedUser.UserId);
            }

            return Ok();
        }
    }
}
