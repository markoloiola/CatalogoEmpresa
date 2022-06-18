using CatalogoEmprego.Data;
using CatalogoEmprego.Models;
using CatalogoEmprego.Serviços;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoEmprego.Controllers;

[ApiController]
[Route("empresas")]

public class EmpresaController : ControllerBase
{

      private readonly EmpresaServico _empresaServico;
     
     public EmpresaController([FromServices] EmpresaServico empresaServico)
     {
        _empresaServico = empresaServico;
     }

    [HttpPost]
    public ActionResult<Empresa> PostEmpresa([FromBody] Empresa EmpresaNova)
    {

        var empresa = _empresaServico.AdicionarEmpresa(EmpresaNova);

        // retornar a empresa
        return CreatedAtAction(nameof(GetEmpresa),new{id = empresa.Id}, empresa);
    
    }
    
    [HttpGet]
    public ActionResult<List<Empresa>> GetEmpresas()
    {
       var empresas = _empresaServico.RecuperarEmpresas();
       return Ok(empresas) ;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Empresa> GetEmpresa([FromRoute] int id)
    {
        
      try
      {
         Empresa empresa = _empresaServico.RecuperarEmpresa(id);
         return empresa;
      }
      catch(Exception)
      {
         return NotFound();
      }

    }

    [HttpPut("{id:int}")]
    public ActionResult<Empresa> PutEmpresa([FromRoute] int id, [FromBody] Empresa EmpresaEditado)
    {      

       try
       {
         var empresa = _empresaServico.AtualizarEmpresa(id, EmpresaEditado);
          return Ok(empresa);
       }

       catch(Exception)
       {
            return NotFound();
       }
  
    }

    [HttpDelete("{id:int}")]

    public ActionResult DeleteEmpresa([FromRoute] int id)
    {
       try
       {
         //mandar o serviço deletar
         _empresaServico.DeletarEmpresa(id);

         return NoContent();
       }
       catch (Exception)
       {
          return NotFound();
       }
    }

}
