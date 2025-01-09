using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainote_api.Models;
[Table("usuario")]
public class Usuario
{
    [Key]
    public int id { get; private set; }
    public string nome { get; private set; }
    public string email { get; private set; }
    public string? foto { get; set; }

    public Usuario(string nome, string email, string foto)
    {
        this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
        this.email = email;
        this.foto = foto;
    }
}