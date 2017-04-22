using Raven.Client.Document;

namespace dn32.infraestrutura.Banco
{
    public class Contexto
    {
        public static DocumentStore Store { get; set; }

        public Contexto()
        {
            if (Store == null)
            {
                Store = new DocumentStore
                {
                    Url = "http://localhost:8080",
                    DefaultDatabase = "dn32"
                };

                Store.Conventions.FindTypeTagName = type =>
                {
                    return type.Name;
                };

                Store.Initialize();
            }

        }

        public static Contexto Crie()
        {
            return new Contexto();
        }
    }
}
