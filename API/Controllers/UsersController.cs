using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseAPIController
    {
        
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            var data = await _context.User.ToListAsync();
            data.ForEachT((v,i)=>{
                var xx = v.UserName;
            });
            
          return await _context.User.ToListAsync();
        }

        //  api/users/3 get by id
        [Authorize]
       [HttpGet("{id}")] //httpGet and param just same
        public async Task<ActionResult<AppUser>> GetUser(int id){
          
            return await _context.User.FindAsync(id);
        }
    }
}