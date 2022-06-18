using CatalogoEmprego.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoEmprego.Data;

public class CatalogoContexto : DbContext
{
    //Construtor configurando do jeito que o webapi
    public CatalogoContexto(DbContextOptions<CatalogoContexto> options) : base(options)
    {
        
    }

    // Criar as propriedades que representam cada tabela do meu banco de dados

    public DbSet<Empresa>  Empresas { get; set; }
     
}
