using System.Collections.Generic;
using System.Web.Http;
using FusionLOS.Models;

namespace App1WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Loan> Get()
        {
            return new Loan().GetLoans();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]Loan newloan)
        {
            new Loan().Add(newloan);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }    
}