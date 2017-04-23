
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

- <h4>2° passo: Implantar o **dn32.infraestrutura** no meu projeto</h4>

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
