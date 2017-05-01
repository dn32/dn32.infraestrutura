using dn32.infraestrutura.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static dn32.infraestrutura.Compartilhado;
using System.Runtime.Loader;
using dn32.infraestrutura.Model;
using System.IO;

namespace dn32.infraestrutura
{
    public class Inicializar
    {
        public static void Inicialize(ParametrosDeInicializacao parametrosDeInicializacao)
        {
            lock (TravaDeInicializacao)
            {
                if (Inicializado)
                {
                    return;
                }

                ValideParametrosDeInicializacao(parametrosDeInicializacao);
                Compartilhado.ParametrosDeInicializacao = parametrosDeInicializacao;

                DicionarioDeServico = new Dictionary<string, Type>();
                DicionarioDeRepositorio = new Dictionary<string, Type>();
                DicionarioDeValidacao = new Dictionary<string, Type>();

                var enderecoBase = Directory.GetCurrentDirectory(); // Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var enderecoDaDllDervico = $"{enderecoBase}\\{parametrosDeInicializacao.NomeDoAssemblyDoServico}.dll";
                var enderecoDaDllValidacao = $"{enderecoBase}\\{parametrosDeInicializacao.NomeDoAssemblyDaValidacao}.dll";
                var enderecoDaDllREpositorio = $"{enderecoBase}\\{parametrosDeInicializacao.NomeDoAssemblyDoRepositorio}.dll";

                if (!File.Exists(enderecoDaDllDervico))
                {
                    throw new Exception($"Não foi possível encontrar a dll do serviço em {enderecoDaDllDervico}. Provavelmente o nome do serviço não foi informado corretamente na inicialização da infraestrutura.");
                }

                if (!File.Exists(enderecoDaDllValidacao))
                {
                    throw new Exception($"Não foi possível encontrar a dll da validação em {enderecoDaDllDervico}. Provavelmente o nome da validação não foi informado corretamente na inicialização da infraestrutura.");
                }

                if (!File.Exists(enderecoDaDllREpositorio))
                {
                    throw new Exception($"Não foi possível encontrar a dll do repositório em {enderecoDaDllDervico}. Provavelmente o nome do repositório não foi informado corretamente na inicialização da infraestrutura.");
                }

                Assembly dllServico = Assembly.Load(AssemblyLoadContext.GetAssemblyName(enderecoDaDllDervico));
                Assembly dllValidacao = Assembly.Load(AssemblyLoadContext.GetAssemblyName(enderecoDaDllValidacao));
                Assembly dllRepositorio = Assembly.Load(AssemblyLoadContext.GetAssemblyName(enderecoDaDllREpositorio));

                var servicos = dllServico.GetTypes().Where(p => p.GetTypeInfo().GetCustomAttribute(typeof(ServicoDeAttribute), true) != null).ToList();
                var validacoes = dllValidacao.GetTypes().Where(p => p.GetTypeInfo().GetCustomAttribute(typeof(ValidacaoDeAttribute), true) != null).ToList();
                var repositorios = dllRepositorio.GetTypes().Where(p => p.GetTypeInfo().GetCustomAttribute(typeof(RepositorioDeAttribute), true) != null).ToList();

                foreach (var type in servicos)
                {
                    var entidade = ((ServicoDeAttribute)type.GetTypeInfo().GetCustomAttribute(typeof(ServicoDeAttribute))).TipoDeEntidade;
                    if (DicionarioDeServico.ContainsKey(entidade.Name))
                    {
                        throw new Exception($"O serviço {entidade.Name} foi declarado mais de uma vez. Verifique declarações do tipo [ServicoDe(typeof({entidade.Name}))]");
                    }

                    DicionarioDeServico.Add(entidade.Name, type);
                }

                foreach (var type in validacoes)
                {
                    var entidade = ((ValidacaoDeAttribute)type.GetTypeInfo().GetCustomAttribute(typeof(ValidacaoDeAttribute))).TipoDeEntidade;
                    if (DicionarioDeValidacao.ContainsKey(entidade.Name))
                    {
                        throw new Exception($"A validação {entidade.Name} foi declarado mais de uma vez. Verifique declarações do tipo [ValidacaoDe(typeof({entidade.Name}))]");
                    }

                    DicionarioDeValidacao.Add(entidade.Name, type);
                }

                foreach (var type in repositorios)
                {
                    var entidade = ((RepositorioDeAttribute)type.GetTypeInfo().GetCustomAttribute(typeof(RepositorioDeAttribute))).TipoDeEntidade;
                    if (DicionarioDeRepositorio.ContainsKey(entidade.Name))
                    {
                        throw new Exception($"O repositório {entidade.Name} foi declarado mais de uma vez. Verifique declarações do tipo [RepositorioDe(typeof({entidade.Name}))]");
                    }

                    DicionarioDeRepositorio.Add(entidade.Name, type);
                }

                Inicializado = true;
            }
        }

        private static void ValideParametrosDeInicializacao(ParametrosDeInicializacao parametrosDeInicializacao)
        {
            if (string.IsNullOrWhiteSpace(parametrosDeInicializacao.NomeDoBancoDeDados))
            {
                throw new Exception("O endereço do banco de dados não foi informado na inicialização da infraestrutura.");
            }

            if (string.IsNullOrWhiteSpace(parametrosDeInicializacao.EnderecoDoBancoDeDados))
            {
                throw new Exception("O endereço do banco de dados não foi informado na inicialização da infraestrutura.");
            }
        }
    }
}
