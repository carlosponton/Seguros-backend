using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seguros.Models;

namespace Seguros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        SecureContext db = new SecureContext();

        // GET: api/Clients
        [HttpGet]
        public IEnumerable<Client> GetClient()
        {
            return db.Client;
        }

        
    }
}