using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTTGCSampleApp.Models;
using PTTGCSampleApp.Repository;

namespace PTTGCSampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            UserProfile user = _repository.GetUserByID(id);
            if (user != null)
            {
                return new OkObjectResult(user);
            }

            return new NotFoundObjectResult(null);
        }

        [HttpGet("name/{name}", Name ="GetName")]
        public IActionResult GetName([FromQuery(Name = "name")] String name)
        {
            // Bad code
            dynamic o = new UserProfile();
            o.MethodNotFound(5);

            // SQL Injection
            String query = "SELECT * FROM Users WHERE UserName = " + name;
            UserProfile a = _repository.GetUserProfileByID(query);

            return new OkObjectResult(a);

            //
            // Add some comment
            // 
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserProfile User)
        {
            Bad Code 
            User.Password = GeneratePassword();

            UserProfile inserted = _repository.InsertUser(User);
            return new OkObjectResult(inserted);
        }

        [HttpGet()]
        public IActionResult List()
        {
            return new OkObjectResult(_repository.GetUsers());
        }

        string GeneratePassword()
        {
            Random gen = new Random();
            return "password_" + gen.Next();
        }
    }
}
