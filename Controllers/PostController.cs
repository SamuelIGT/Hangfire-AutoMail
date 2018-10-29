using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangfireEmailSchedule.Models;
using HangfireEmailSchedule.Service;
using HangfireEmailSchedule.Util;
using Microsoft.AspNetCore.Mvc;

namespace HangfireEmailSchedule.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPost service;

        public PostController(IPost _service) {
            service = _service;
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {

            return service.GetPosts;
        }

        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult Get(int id)
        {
            Post post = service.GetPost(id);
            if (post == null) {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            if (post == null) {
                return BadRequest();
            }
            service.Add(post);

            return CreatedAtRoute("GetPost", new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
