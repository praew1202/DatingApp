using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PraewController : ControllerBase
    {
        
        private readonly DataContext _context;
        public PraewController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers(){
            var users = _context.User.ToList();
            return users;
        }

        //  api/users/3 get by id
       [HttpGet("{item}")]
        public ActionResult<AppUser> GetUser(int item){
            return _context.User.Find(item);
        }
    }
}