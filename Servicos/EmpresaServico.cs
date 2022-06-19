using CatalogoEmprego.Data;
using CatalogoEmprego.Dtos.Empresa;
using CatalogoEmprego.Models;
using Mapster;
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

    public List<EmpresaResponseDto> RecuperarEmpresas()
    {
        return _contexto.Empresas.ProjectToType<EmpresaResponseDto>().ToList();
        //Pode ser feito igual abaico tbm
        // var empresas = _contexto.Empresas.ToList();
        // var empresasResposta = empresas.Adapt<List<EmpresaResponseDto>>();
        // return empresasResposta;
        
    }

    public EmpresaResponseDto RecuperarEmpresa(int id)
    {
        var empresa = _contexto.Empresas.SingleOrDefault(empresa => empresa.Id == id);

        if (empresa is null)
            throw new Exception("Empresa não encontrada");

        //Mapear do objeto empresa para empresaResponseDto
        //MAPEAMENTO MANUAL
        // var empresaResposta = new EmpresaResponseDto();
        // empresaResposta.Id = empresa.Id;
        // empresaResposta.NomeFantasia = empresa.NomeFantasia;
        // empresaResposta.Cnpj = empresa.Cnpj;
        // empresaResposta.Cidade = empresa.Cidade;
        // empresaResposta.Estado = empresa.Estado;
        // empresaResposta.Endereco = empresa.Endereco;
        // empresaResposta.Telefone = empresa.Telefone;
        // empresaResposta.Email = empresa.Email;
        //MAPEAMENTO A PARTIR DO MAPSTER DE FORMA AUTOMÁTICA
        EmpresaResponseDto empresaResposta = empresa.Adapt<EmpresaResponseDto>();
        return empresaResposta;
    }

    public EmpresaResponseDto AdicionarEmpresa(EmpresaCreateUpdateDto empresaDto)
    {
        //Mapear de EmpresaCreateUpdateDto para Empresa - MANUAL
        // var empresa = new Empresa()
        // {
        //     NomeFantasia = empresaDto.NomeFantasia,
        //     Cnpj = empresaDto.Cnpj,
        //     Endereco = empresaDto.Endereco,
        //     Telefone = empresaDto.Telefone,
        //     Email = empresaDto.Email
        // };

        var empresa = empresaDto.Adapt<Empresa>();

        //Salvar o empresa no banco de dados
        //adicionadno a empresa no contexto na memoria
        _contexto.Empresas.Add(empresa);

        //Comando para salvar que realmente salva no banco de dados
        _contexto.SaveChanges();

        //Mapear de Empresa para EmpresaResponseDto
        var empresaResposta = empresa.Adapt<EmpresaResponseDto>();

        return empresaResposta;

    }

    public EmpresaResponseDto AtualizarEmpresa(int id, EmpresaCreateUpdateDto EmpresaEditado)
    {
        //Buscar a empresa no bd
        var empresa = _contexto.Empresas.SingleOrDefault(empresa => empresa.Id == id);

        if (empresa is null)
            throw new Exception("Empresa não encontrada");

        //Mapeando do EmpresaCreatUpdateDto para Empresa (Objeto existentes)
        EmpresaEditado.Adapt(empresa);

        //salvar as alterações no banco de dados
        _contexto.SaveChanges();

        //Mapear de Empresa para EmpresaResponseDto
        var empresaResposta = empresa.Adapt<EmpresaResponseDto>();

        return empresaResposta;

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
