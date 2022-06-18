using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoEmprego.Models;

public class Empresa
{
   [Required]
   public int Id { get; set; }

   [Required]
   [Column(TypeName ="varchar(255)")]
   public  string RazaoSocial { get; set; }

    [Required] 
    [Column(TypeName ="varchar(255)")]
   public  string NomeFantasia { get; set; }

    [Required]
    [Column(TypeName ="varchar(25)")]
   public  string Cnpj { get; set; }

    [Required]
    [Column(TypeName ="varchar(50)")]
   public  string Cidade { get; set; }

    [Required]
    [Column(TypeName ="varchar(25)")]
   public  string Estado { get; set; }

    [Required]
    [Column(TypeName ="varchar(255)")]
   public  string Endereco { get; set; }

    [Required]
    [Column(TypeName ="varchar(50)")]
   public  string Telefone { get; set; }

    [Required]
    [Column(TypeName ="varchar(150)")]
   public  string Email { get; set; }
   
}
