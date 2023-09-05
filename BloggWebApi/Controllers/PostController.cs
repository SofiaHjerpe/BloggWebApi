using BloggWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloggWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // GET: api/<PostController>
        [HttpGet]
        public List<Post> Get()
        {
            var db = new DatabaseConnection();
            var posts = db.GetAllPosts();
            return posts;
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var db = new DatabaseConnection();  
            var post = db.GetPostById(id);  

            if(post == null) { 
                return NotFound();  
            }
            return Ok(post);    
        }

        // POST api/<PostController>
        [HttpPost]
        public void Post([FromBody] Post post )
        {
            var db = new DatabaseConnection();  
            db.SavePost(post);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Post post)
        {
            var db = new DatabaseConnection();  
            db.UpdatePost(id, post);  
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var db = new DatabaseConnection();  
            db.DeletePost(id);  
        }
    }
}
