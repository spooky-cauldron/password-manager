using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PasswordManager.Services;

namespace PasswordManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordsController : ControllerBase
    {
        readonly ILogger<PasswordsController> logger;
        readonly PasswordDbContext dbContext;
        readonly ISecurity security;

        public PasswordsController(ILogger<PasswordsController> logger, PasswordDbContext dbContext, ISecurity security)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.security = security;
        }
        
        // Get all passwords metadata endpoint
        [HttpGet]
        public ActionResult<IEnumerable<PasswordInfo>> GetPasswords()
        {
            var passwords = dbContext.Passwords;
            var passwordInfo = passwords
                .Select(p => new PasswordInfo(p.Id, p.Name))
                .ToArray();
            return passwordInfo;
        }
        
        // Get single password endpoint
        [HttpGet("{id}")]
        public ActionResult<PasswordResponse> GetPassword(long id)
        {
            var password = dbContext.Passwords.Find(id);
            if (password == null)
            {
                return NotFound();
            }

            var decryptedPassword = security.Decrypt(password.EncryptedValue);
            return new PasswordResponse(password.Id, password.Name, decryptedPassword);
        }
        
        // create a new password endpoint
        [HttpPost]
        public async Task<ActionResult<PasswordInfo>> PostPassword(PasswordPostBody passwordBody)
        {
            var encryptedPassword = security.Encrypt(passwordBody.Value);
            var newPasswordData = new PasswordStore
            {
                Name = passwordBody.Name,
                EncryptedValue = encryptedPassword,
            };
            var newPassword = dbContext.Passwords.Add(newPasswordData);
            await dbContext.SaveChangesAsync();
            var response = new PasswordInfo(newPassword.Entity.Id, newPassword.Entity.Name);
            return CreatedAtAction(nameof(GetPasswords), response);
        }
    }
}