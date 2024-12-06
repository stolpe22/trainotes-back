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

    public Usuario(string nome, string email)
    {
        this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
        this.email = email;
    }
}