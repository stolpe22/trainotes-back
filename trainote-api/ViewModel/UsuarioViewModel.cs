using System;

namespace trainote_api.ViewModel;

public class UsuarioViewModel
{
    public string nome { get; set; }
    public string email { get; set; }
    public IFormFile foto { get; set; }
}
