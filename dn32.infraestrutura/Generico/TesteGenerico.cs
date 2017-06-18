
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dn32.infraestrutura.Generico
{
    public abstract class TesteGenerico<T> where T : ModelGenerico, new()
    {
        private ServicoGenerico<T> _servico { get; set; }

        public virtual ServicoGenerico<T> Servico
        {
            get
            {
                if (_servico == null)
                {
                    _servico = FabricaDeServico.Crie<T>();
                }

                return _servico;
            }
        }

        public TesteGenerico()
        {
            InicializarInfraestrutura();
        }

        public abstract void InicializarInfraestrutura();

        private static Random Random { get; set; }

        public static int ObtenhaUmCodigo()
        {
            if (Random == null)
            {
                Random = new Random();
            }

            return Random.Next(1, int.MaxValue);
        }

        public static bool Compare(object objeto1, object objeto2, params string[] pripriedadesAIgnorar)
        {
            var json1 = Serialize(objeto1, pripriedadesAIgnorar);
            var json2 = Serialize(objeto2, pripriedadesAIgnorar);
            return json1 == json2;
        }

        public static string Serialize(object objeto, params string[] pripriedadesAIgnorar)
        {
            var propriedadesAIgnorar = new IgnorarPropriedades(pripriedadesAIgnorar);
            return JsonConvert.SerializeObject(objeto, Formatting.Indented, new JsonSerializerSettings { ContractResolver = propriedadesAIgnorar });
        }

        private class IgnorarPropriedades : DefaultContractResolver
        {
            private string[] Propriedades { get; set; }

            public IgnorarPropriedades(params string[] propriedades)
            {
                Propriedades = propriedades;
            }

            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                var properties = base.CreateProperties(type, memberSerialization);
                return properties.Where(p => !Propriedades.Contains(p.PropertyName)).OrderBy(p => p.PropertyName).ToList();
            }
        }
    }
}
