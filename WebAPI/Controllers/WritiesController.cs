using Business.Abstarct;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritiesController : ControllerBase
    {
        IWriteService _writeService;

        public WritiesController(IWriteService writeService)
        {
            _writeService = writeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] Write write, [FromForm] IFormFile imageFile)
        {
            var result = _writeService.Add(write, imageFile);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Write write)
        {
            var result = _writeService.Update(write);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("updatewithfile")]
        public async Task<IActionResult> UpdateWithFile([FromForm] Write write, [FromForm] IFormFile imageFile)
        {
            var result = _writeService.UpdateWithFile(write, imageFile);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Write write)
        {
            var result = _writeService.Delete(write);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _writeService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _writeService.GetById(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getallbyuserid")]
        public IActionResult GetAllByUserId(int userId)
        {
            var result = _writeService.GetAllByUserId(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getallorderbycreationdate")]
        public IActionResult GetAllOrderByCreationDate()
        {
            var result = _writeService.GetAllOrderByCreationDate();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getwritedetails")]
        public IActionResult GetWriteDetails()
        {
            var result = _writeService.GetWriteDetails();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
