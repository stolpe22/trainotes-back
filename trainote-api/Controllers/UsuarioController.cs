using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trainote_api.Models;
using trainote_api.ViewModel;

namespace trainote_api.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }
        [HttpPost]
        public IActionResult Add(UsuarioViewModel usuarioView)
        {
            var usuario = new Usuario(usuarioView.nome, usuarioView.email);
            _usuarioRepository.Add(usuario);
            return Ok();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _usuarioRepository.Get();
            return Ok(usuarios);
        }
    }
}
