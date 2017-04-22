using dn32.infraestrutura.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static dn32.infraestrutura.Util.Compartilhado;
using System.Runtime.Loader;
using dn32.infraestrutura.Model;
using System.IO;

namespace dn32.infraestrutura.Util
{
    public class Inicializar
    {
        public static void Inicialize(ParametrosDeInicializacao parametrosDeInicializacao)
        {
            DicionarioDeServico = new Dictionary<string, Type>();
            DicionarioDeRepositorio = new Dictionary<string, Type>();
            DicionarioDeValidacao = new Dictionary<string, Type>();

            var location = Assembly.GetEntryAssembly().Location;
            var enderecoBase = Path.GetDirectoryName(location);
            var enderecoDaDllDervico = $"{enderecoBase}\\{parametrosDeInicializacao.NomeDoAssemblyDoServico}.dll";
            var enderecoDaDllValidacao = $"{enderecoBase}\\{parametrosDeInicializacao.NomeDoAssemblyDaValidacao}.dll";
            var enderecoDaDllREpositorio = $"{enderecoBase}\\{parametrosDeInicializacao.NomeDoAssemblyDoRepositorio}.dll";
            EnderecoDoBancoDeDados = parametrosDeInicializacao.EnderecoDoBancoDeDados;

            if (string.IsNullOrWhiteSpace(EnderecoDoBancoDeDados))
            {
                throw new Exception("O endereço do banco de dados não foi informado na inicialização da infraestrutura.");
            }

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
                DicionarioDeServico.Add(entidade.Name, type);
            }

            foreach (var type in validacoes)
            {
                var entidade = ((ValidacaoDeAttribute)type.GetTypeInfo().GetCustomAttribute(typeof(ValidacaoDeAttribute))).TipoDeEntidade;
                DicionarioDeValidacao.Add(entidade.Name, type);
            }

            foreach (var type in repositorios)
            {
                var entidade = ((RepositorioDeAttribute)type.GetTypeInfo().GetCustomAttribute(typeof(RepositorioDeAttribute))).TipoDeEntidade;
                DicionarioDeRepositorio.Add(entidade.Name, type);
            }
        }
    }
}
