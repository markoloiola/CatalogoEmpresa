using CatalogoEmprego.Data;
using CatalogoEmprego.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoEmprego.Serviços;

public class EmpresaServico
{
    //Reservado para qualquer regra de negocio do sistema
    //readonly quer dizer somente para leitura
    
    private readonly CatalogoContexto _contexto;

    public EmpresaServico([FromServices] CatalogoContexto contexto)
    {
       _contexto = contexto;
    }

    public List<Empresa> RecuperarEmpresas()
    {
        //qualquer regra de negocio  pra aplicaçao
        return _contexto.Empresas.ToList();
    }

    public Empresa RecuperarEmpresa(int id)
    {
        var empresa = _contexto.Empresas.SingleOrDefault(empresa => empresa.Id == id);
      
        if (empresa is null)
          throw new Exception("Empresa não encontrada");

       return empresa; 
    }

    public Empresa AdicionarEmpresa(Empresa empresa)
    {
        //espaço para regra de negocio, caso a gente precise no futuro tratar alguma propriedade 
        //calculo de imposto,etc
        //tem que ser feita no serviço e não no controlador
        
        //Salvar o empresa no banco de dados
        //adicionadno a empresa no contexto na memoria
        _contexto.Empresas.Add(empresa);

        //Comando para salvar que realmente salva no banco de dados
        _contexto.SaveChanges();

        return empresa;

    }

    public Empresa AtualizarEmpresa(int id, Empresa EmpresaEditado)
    {
        //Buscar a empresa no bd
        var empresa = _contexto.Empresas.SingleOrDefault(empresa => empresa.Id == id);

        if (empresa is null)
          throw new Exception("Empresa não encontrada");

        //Copiar od dados que vieram do cliente
        empresa.RazaoSocial = EmpresaEditado.RazaoSocial;
        empresa.NomeFantasia = EmpresaEditado.NomeFantasia;
        empresa.Cnpj = EmpresaEditado.Cnpj;
        empresa.Cidade = EmpresaEditado.Cidade;
        empresa.Estado = EmpresaEditado.Estado;
        empresa.Endereco = EmpresaEditado.Endereco;
        empresa.Telefone = EmpresaEditado.Telefone;
        empresa.Email = EmpresaEditado.Email;

        //salvar as alterações no banco de dados
        _contexto.SaveChanges();

        return empresa;

    }

    public void DeletarEmpresa(int id)
    {
        //Buscar a empresa no bd
        var empresa = _contexto.Empresas.SingleOrDefault(empresa => empresa.Id == id);

        if (empresa  is null)
           throw new Exception("Empresa não encontrada");

        //Deletar no contexto na memoria
        _contexto.Remove(empresa);

        //Salvar a deleção do banco de dados
        _contexto.SaveChanges();

    }
       
}
