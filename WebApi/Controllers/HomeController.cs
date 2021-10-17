using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]

    // HttpGet (ok  200) , Put(noContent 204) , Post(created) , Delete(noContent)
    public class HomeController : Controller
    {

        //private readonly Context context;

        //public HomeController(Context context)
        //{
        //    context = context;
        //}


        Context context = new Context();


        [HttpGet]
        public IActionResult GetPerson()
        {
            return Ok(context.people.ToList());
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var getbyid = context.people.FirstOrDefault(x => x.Id == id);
            return Ok(getbyid);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            context.Add(person);
            context.SaveChanges();
            return Created("", person);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson([FromBody] Person person)
        {
            var user = context.people.Find(person.Id);
            user.Firstname = person.Firstname;
            user.Lastname = person.Lastname;
            user.Address = person.Address;
            context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult deleteuser(int id)
        {
            var user = context.people.Find(id);
            context.Remove(user);
            context.SaveChanges();
            return NoContent();
        }

    } 
}
