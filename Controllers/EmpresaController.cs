using CatalogoEmprego.Data;
using CatalogoEmprego.Dtos.Empresa;
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
    public ActionResult<EmpresaResponseDto> PostEmpresa([FromBody] EmpresaCreateUpdateDto EmpresaNova)
    {

        var empresa = _empresaServico.AdicionarEmpresa(EmpresaNova);

        // retornar a empresa
        return CreatedAtAction(nameof(GetEmpresa),new{id = empresa.Id}, empresa);
    
    }
    
    [HttpGet]
    public ActionResult<List<EmpresaResponseDto>> GetEmpresas()
    {
       var empresas = _empresaServico.RecuperarEmpresas();
       return Ok(empresas) ;
    }

    [HttpGet("{id:int}")]
    public ActionResult<EmpresaResponseDto> GetEmpresa([FromRoute] int id)
    {
        
      try
      {
         var empresa = _empresaServico.RecuperarEmpresa(id);
         return empresa;
      }
      catch(Exception)
      {
         return NotFound();
      }

    }

    [HttpPut("{id:int}")]
    public ActionResult<EmpresaResponseDto> PutEmpresa([FromRoute] int id, [FromBody] EmpresaCreateUpdateDto EmpresaEditado)
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
