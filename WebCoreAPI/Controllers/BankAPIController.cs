using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCoreAPI.Models;

namespace WebCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAPIController : ControllerBase
    {
        private readonly BankContext context;

        public BankAPIController(BankContext context){
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAccounts()
        {
            var data = await context.Accounts.ToListAsync();
            return Ok(data);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccounts(int id)
        {
            var account = await context.Accounts.FindAsync(id);
            if(account ==null){
                return NotFound();
            }
            return Ok(account);
            
        }

        [HttpPost]
        public async Task<ActionResult<List<Account>>> CreateAccount(Account account)
        {
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            return Ok(account); 
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<List<Account>>> UpdateAccount(Account account, int id)
        {
            if(id==account.AccountNumber){
                context.Entry(account).State= EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(account);
                
            }
            return BadRequest();
            
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<Account>>> DeleteAccount( int id)
        {
            var account = await context.Accounts.FindAsync(id);
            if(account ==null){
                return NotFound();
            }
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
            return Ok();
            
        }


    }
}
