using System;
using Microsoft.AspNetCore.Connections;
using trainote_api.Context;
using trainote_api.Models;

namespace trainote_api.Infrastructure;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;
    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    public void Add(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public List<Usuario> Get()
    {
        return _context.Usuarios.ToList();
    }

    public Usuario? Get(int id)
    {
        return _context.Usuarios.Find(id);
    }
}
