using System;

namespace trainote_api.Models;

public interface IUsuarioRepository
{
    void Add(Usuario usuario);
    List<Usuario> Get();
}
