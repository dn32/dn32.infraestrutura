using dn32.infraestrutura.Generico;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace dn32.infraestrutura
{
    //using static dn32.infraestrutura.Utilitarios;
    public static class Utilitarios
    {
        public static string ObtenhaIdDoElemento<T2>(int codigo) where T2 : ModelGenerico, new()
        {
            return $"{typeof(T2).Name}/{codigo}".ToLower();
        }

        public static int ObtenhaCodigoDoElemento(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return 0;
            }
            try
            {
                return Convert.ToInt32(Id.Split('/')[1]);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string ObtenhaIdDoElemento(Type tipo, int codigo)
        {
            return $"{tipo.Name}/{codigo}".ToLower();
        }

        public static void EhModelgenerico(Type tipo)
        {
            var ehFilhoDe = tipo.GetTypeInfo().IsSubclassOf(typeof(ModelGenerico));
            if (!ehFilhoDe)
            {
                throw new Exception(string.Format($"Não é possível mapear {tipo.Name}, pois ele não herda de {nameof(ModelGenerico)}"));
            }
        }

        public static bool ValideEmail(string email)
        {
            if (email == null)
            {
                return false;
            }

            var emailRegex = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9A-Za-z-_](\.(?!\.)[0-9A-Za-z-_])*)*)@(?!\.))(?((\d{1,3}\.){3}\d{1,3})|(([0-9A-Za-z]*\.(?!\.))+[0-9A-Za-z]{2,24}))$");
            return emailRegex.IsMatch(email);
        }

        public static string LimpeCPF(string CPF)
        {
            if (CPF == null)
            {
                return string.Empty;
            }

            return CPF.Trim().Replace(".", "").Replace("-", "").Replace(",", "");
        }

        public static bool ValidarCPF(string CPF)
        {
            CPF = LimpeCPF(CPF);
            int[] mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string TempCPF;
            string Digito;
            int soma;
            int resto;

            if (CPF.Length != 11)
                return false;

            TempCPF = CPF.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(TempCPF[i].ToString()) * mt1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            Digito = resto.ToString();
            TempCPF = TempCPF + Digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(TempCPF[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            Digito = Digito + resto.ToString();

            return CPF.EndsWith(Digito);
        }
        public static bool EhImagem(this string source)
        {
            source = source.ToLower();
            return (source.EndsWith(".png") || source.EndsWith(".jpg") || source.EndsWith(".jpeg") || source.EndsWith(".bmp") || source.EndsWith(".gif"));
        }
    }
}
