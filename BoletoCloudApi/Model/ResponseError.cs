using System.Collections.Generic;

namespace BoletoCloudApi.Model
{
    public class ResponseError
    {
        public Erro erro { get; set; }
    }
    public class Causa
    {
        public string codigo { get; set; }
        public string mensagem { get; set; }
        public string suporte { get; set; }
    }
    public class Erro
    {
        public int status { get; set; }
        public string tipo { get; set; }
        public List<Causa> causas { get; set; }
    }

}
