
# dn32.infraestrutura
<img src="https://raw.githubusercontent.com/dn32/dn32.infraestrutura/master/Media/Logo/Logo-icone-png-128.png"/>


Essa infraestrutura foi desenvolvida durante o treinamento [Formação Completa Desenvolvedor Web](https://sinergiaweb.com.br)

#### É compatível com ASP.NET Core 1.1.2 ou superior

### Licença

dn32.infraestrutura é licenciado por meio de [Apache License](LICENSE).

-------------------------------------------------------------------------------------

- <h4>1° passo: Instalar o dn32.infraestrutura no meu projeto</h4>

Lembre-se dn32.infraestrutura é compatível com ASP.NET Core 1.1.2 ou superior

Baixe o pacote por meio do NuGet executando o seguinte comando:

```NuGet
PM> Install-Package dn32.infraestrutura
```

- <h4>2° passo: Implantar o dn32.infraestrutura no meu projeto</h4>

Adicione a inicialização da infraestrutura ao seu projeto por meio dos comandos abaixo em seu arquivo startup.cs

```C#
public void ConfigureServices(IServiceCollection services)
{

    services.AddMvc();

    var parametrosDeInicializacao = new ParametrosDeInicializacao
    {
        EnderecoDeBackupDoBancoDeDados = "c:/ravendb-backup",
        EnderecoDoBancoDeDados = "http://localhost:8080",
        NomeDoAssemblyDaValidacao = "MinhaAplicacao.Validacao",
        NomeDoAssemblyDoRepositorio = "MinhaAplicacao.Repositorio",
        NomeDoAssemblyDoServico = "MinhaAplicacao.Servico",
        NomeDoBancoDeDados = "banco-de-dados-de-minha-aplicacao",
        Services = services
    };

    Inicializar.Inicialize(parametrosDeInicializacao);
    
    .........
}
```
O Model

```C#
public class UnidadeDeTeste : ModelGenerico
{
    public string Descricao { get; set; }
    public int Numero { get; set; }
}
```

O Controller

```C#
public class UnidadeDeTesteController : ControladorGenerico<UnidadeDeTeste>
{
    public ActionResult Cadastro(UnidadeDeTeste unidadeDeTeste)
    {
        Servico.Cadastre(unidadeDeTeste);
        return View();
    }
}
```

O Serviço customizado

```C#
[ServicoDe(typeof(UnidadeDeTeste))]
public class ServicoDeUnidadeDeTeste : ServicoGenerico<UnidadeDeTeste>
{
    public int CadastreCustomizado(UnidadeDeTeste unidadeDeteste)
    {
        unidadeDeteste.Descricao += " - Servico";
        ((ValidacaoDeUnidadeDeTeste)Validacao).CadastreCustomizado(unidadeDeteste);
        return ((RepositorioDeUnidadeDeTeste)Repositorio).CadastreCustomizado(unidadeDeteste);
    }
}
```

O Validação customizada

```C#
[ValidacaoDe(typeof(UnidadeDeTeste))]
public class ValidacaoDeUnidadeDeTeste : ValidacaoGenerica<UnidadeDeTeste>
{
    public override void Cadastre(UnidadeDeTeste item)
    {
        if(item.Numero < 17 || item.Numero > 80)
        {
            throw new Exception(ConstantesDeTeste.O_NUMERO_DEVE_SEM_MAIOR_QUE_17_E_MENOR_QUE_80);
        }

        base.Cadastre(item);
    }

    public void CadastreCustomizado(UnidadeDeTeste unidadeDeteste)
    {

    }
}
```

O Repositório customizado

```C#
[RepositorioDe(typeof(UnidadeDeTeste))]
public class RepositorioDeUnidadeDeTeste : RepositorioGenerico<UnidadeDeTeste>
{
    public int CadastreCustomizado(UnidadeDeTeste unidadeDeteste)
    {
        unidadeDeteste.Descricao += "-Repositório";
       return Cadastre(unidadeDeteste);
    }
}
```
