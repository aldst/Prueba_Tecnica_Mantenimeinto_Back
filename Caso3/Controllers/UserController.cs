using Caso3.Models;
using Caso3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Caso3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Lista la información de los usuarios (el parámetro es opcional)
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <returns></returns>
        [HttpPost("GetAllusers")]
        public async Task<ActionResult> GetAll([FromBody] User? user = null)
        {
            var users = await _userService.GetAllUserAsync(user);
            return Ok(users);
        }

        /// <summary>
        /// Guarda o Actualiza la información de un usuario
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <returns></returns>
        [HttpPost("SaveUser")]
        public async Task<ActionResult> Save([FromBody] User user)
        {
            var success = await _userService.SaveAsync(user);
            if (!success) return BadRequest("Error creating user.");
            return Ok();
        }

        /// <summary>
        /// Eliminar el registro de un Usuario
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _userService.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok();
        }
    }
}
