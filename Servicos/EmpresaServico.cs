using CatalogoEmprego.Data;
using CatalogoEmprego.Dtos.Empresa;
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

    public EmpresaResponseDto RecuperarEmpresa(int id)
    {
        var empresa = _contexto.Empresas.SingleOrDefault(empresa => empresa.Id == id);

        if (empresa is null)
            throw new Exception("Empresa não encontrada");

        //Mapear do objeto empresa para empresaResponseDto
        var empresaResposta = new EmpresaResponseDto();
        empresaResposta.Id = empresa.Id;
        empresaResposta.NomeFantasia = empresa.NomeFantasia;
        empresaResposta.Cnpj = empresa.Cnpj;
        empresaResposta.Cidade = empresa.Cidade;
        empresaResposta.Estado = empresa.Estado;
        empresaResposta.Endereco = empresa.Endereco;
        empresaResposta.Telefone = empresa.Telefone;
        empresaResposta.Email = empresa.Email;

        return empresaResposta;
    }

    public EmpresaResponseDto AdicionarEmpresa(EmpresaCreateUpdateDto empresaDto)
    {
        //Mapear de EmpresaCreateUpdateDto para Empresa
        var empresa = new Empresa()
        {
            NomeFantasia = empresaDto.NomeFantasia,
            Cnpj = empresaDto.Cnpj,
            Endereco = empresaDto.Endereco,
            Telefone = empresaDto.Telefone,
            Email = empresaDto.Email
        };

        //Salvar o empresa no banco de dados
        //adicionadno a empresa no contexto na memoria
        _contexto.Empresas.Add(empresa);

        //Comando para salvar que realmente salva no banco de dados
        _contexto.SaveChanges();

        //Mapear de Empresa para EmpresaResponseDto
        var empresaResposta = new EmpresaResponseDto();
        empresaResposta.Id = empresa.Id;
        empresaResposta.NomeFantasia = empresa.NomeFantasia;
        empresaResposta.Cnpj = empresa.Cnpj;
        empresaResposta.Cidade = empresa.Cidade;
        empresaResposta.Estado = empresa.Estado;
        empresaResposta.Endereco = empresa.Endereco;
        empresaResposta.Telefone = empresa.Telefone;
        empresaResposta.Email = empresa.Email;


        return empresaResposta;

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

        if (empresa is null)
            throw new Exception("Empresa não encontrada");

        //Deletar no contexto na memoria
        _contexto.Remove(empresa);

        //Salvar a deleção do banco de dados
        _contexto.SaveChanges();

    }

}
