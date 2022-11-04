using GenCommon.Shared.Extensions;
using GenCore.Data.Models;
using GenCore.Data.Repositories.Interface;
using GenCryptography.Service.Utilities.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GenAuthorisation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ValidationController : ControllerBase
    {
        private readonly IUserEncryptionRepository _userEncryptionRepo;
        private readonly IUserTokensRepository _userTokensRepo;
        private readonly IEncryptor _encryptor;

        public ValidationController(IEncryptor encryptor, IUserTokensRepository userTokensRepository, IUserEncryptionRepository userEncryptionRepository)
        {
            _userEncryptionRepo = userEncryptionRepository;
            _userTokensRepo = userTokensRepository;
            _encryptor = encryptor;
        }

        [HttpPost(Name = "Validate")]
        public IActionResult Validate([FromBody] UserToken uiUserToken)
        {
            UserToken retrievedUserToken = _userTokensRepo.GetUser(uiUserToken.Token);

            if (retrievedUserToken is null)
            {
                return BadRequest("Invalid user");
            }

            if (DateTime.UtcNow > retrievedUserToken.Expiry)
            {
                return BadRequest("Token expired");
            }

            string retrievedUserJson = retrievedUserToken.ToJson();

            if (retrievedUserJson.IsEmpty())
            {
                return BadRequest("Retrieved user could not be verified");
            }

            UserEncryption userEncryption = _userEncryptionRepo.Get(retrievedUserToken.UserId);

            string encryptedRetrievedUserJson = _encryptor.Encrypt(userEncryption.EncryptionKey, retrievedUserJson);

            if (encryptedRetrievedUserJson.IsEmpty())
            {
                return BadRequest("User could not be processed");
            }

            return Ok(encryptedRetrievedUserJson);
        }
    }
}
