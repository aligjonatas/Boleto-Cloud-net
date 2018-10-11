using System.Collections.Generic;

namespace BoletoCloudApi
{
    public class BoletoCloudResponse
    {
        public BoletoCloudResponse()
        {
            Sucesso = true;
        }

        ErroTratado erro;
        public bool Sucesso { get; set; }

        public string Mensagem { get; set; }

        public ErroTratado Erro
        {
            get
            {
                if (erro == null)
                    erro = new ErroTratado();
                return erro;
            }
            set
            {
                erro = value;
            }
        }

        public string VersaoApiBoletoCloud { get; set; }
        public string Token { get; set; }
        public string NossoNumero { get; set; }
        public string ContentType { get; set; }
        public string Location { get; set; }
        public byte[] Boleto { get; set; }
    }

    public class ErroTratado
    {
        public ErroTratado()
        {
            Mensagens = new List<string>();
        }

        public string TipoErro { get; set; }
        public int Codigo { get; set; }
        public List<string> Mensagens { get; set; }
    }
}
