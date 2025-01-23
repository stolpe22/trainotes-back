using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] UsuarioViewModel usuarioView)
        {
            var filePath = Path.Combine("Storage", usuarioView.foto.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            usuarioView.foto.CopyTo(fileStream);

            var usuario = new Usuario(usuarioView.nome, usuarioView.email, filePath);
            _usuarioRepository.Add(usuario);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var usuario = _usuarioRepository.Get(id);

            var dataBytes = System.IO.File.ReadAllBytes(usuario.foto);

            return File(dataBytes, "image/png");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _usuarioRepository.Get();
            return Ok(usuarios);
        }
    }
}
