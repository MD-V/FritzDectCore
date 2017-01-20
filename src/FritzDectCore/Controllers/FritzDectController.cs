using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FritzDectCore.Helper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FritzDectCore.Controllers
{
    [Route("api/[controller]")]
    public class FritzDectController : Controller
    {
        // GET: api/FritzDect
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/FritzDect/TurnOn/
        [Route("TurnOn")]
        public string TurnOn(string ain, string username, string password)
        {
            var helper = new FritzApiHelper();

            var result = helper.TurnOn(ain, username, password).Result;

            return $"On: {ain} {username} {password} Result: {result}";
        }

        // GET api/FritzDect/TurnOn/
        [Route("TurnOff")]
        public string TurnOff(string ain, string username, string password)
        {
            var helper = new FritzApiHelper();

            var result = helper.TurnOff(ain, username, password).Result;

            return $"Off: {ain} {username} {password} Result: {result}";
        }

        // GET api/FritzDect/TurnOn/
        [Route("SwitchList")]
        public string SwitchList( string username, string password)
        {
            var helper = new FritzApiHelper();

            var result = helper.SwitchList(username, password).Result;

            return $"List: {string.Join(",", result)} {username} {password} Result: {result}";
        }


        // POST api/FritzDect
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/FritzDect/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/FritzDect/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
