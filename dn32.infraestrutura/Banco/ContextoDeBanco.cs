using Raven.Client.Document;

namespace dn32.infraestrutura.Banco
{
    internal class Contexto
    {
        public static DocumentStore Store { get; set; }

        public Contexto()
        {
            if (Store == null)
            {
                Store = new DocumentStore
                {
                    Url = Compartilhado.ParametrosDeInicializacao.EnderecoDoBancoDeDados,
                    DefaultDatabase = Compartilhado.ParametrosDeInicializacao.NomeDoBancoDeDados,
                };

                Store.Conventions.FindTypeTagName = type =>
                {
                    return type.Name;
                };

                Store.Initialize();
            }

        }
    }
}
