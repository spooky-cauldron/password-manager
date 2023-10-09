using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PasswordManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordsController : ControllerBase
    {
        private readonly ILogger<PasswordsController> _logger;

        public PasswordsController(ILogger<PasswordsController> logger)
        {
            _logger = logger;
        }
        
        // Get all passwords endpoint
        [HttpGet]
        public ActionResult<IEnumerable<PasswordData>> GetPasswords()
        {
            var passwords = new List<PasswordData> { new("My Password", "secret") };
            return passwords;
        }
        
        // create a new password endpoint
        [HttpPost]
        public ActionResult<PasswordData> PostPassword(PasswordData password)
        {
            return CreatedAtAction(nameof(GetPasswords), password);
        }
    }
}