using BoletoCloudApi.Model;
using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace BoletoCloudApi
{
    public class Service
    {
        public BoletoInfo BoletoInfo { get; private set; }
        public Config Config { get; private set; }

        public Service()
        {
            this.Config = new Config();
        }

        public Service(BoletoInfo boletoInfo)
        {
            this.BoletoInfo = boletoInfo;
            this.Config = new Config();
        }

        /// <summary>
        /// Consome Api do Boleto Cloud para geração de boleto
        /// </summary>
        /// <returns></returns>
        public BoletoCloudResponse GeraBoleto()
        {
            if (BoletoInfo == null)
                throw new ArgumentNullException("BoletoInfo");

            if (string.IsNullOrEmpty(this.Config.Url))
                throw new ArgumentNullException("Url da Api não configurada");

            if (string.IsNullOrEmpty(this.Config.Token))
                throw new ArgumentNullException("Token da Api não configurado");

            if (BoletoInfo.Instrucoes.Count > 8)
                throw new IndexOutOfRangeException("Permitido somente 8 linhas de Instruções");

            var result = new BoletoCloudResponse();
            var client = new RestClient(this.Config.Url);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "74f4691e-79f3-4934-8167-c464d9a26e53");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Basic {this.Config.AuthorizationHash}");
            request.AddParameter("undefined", FillParameters(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.ResponseStatus == ResponseStatus.Error && response.ErrorException != null)
                throw response.ErrorException;

            TrataResposta(result, response);

            return result;
        }

        /// <summary>
        /// Trata respostas Rest do Api
        /// </summary>
        /// <param name="result">Retorno tratado (faced)</param>
        /// <param name="response">Retorno da Api</param>
        private static void TrataResposta(BoletoCloudResponse result, IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK)
            {
                // Sucesso na geração (HttpStatusCode.Created) ou consulta (HttpStatusCode.OK)
                if (response.Headers.Any(t => t.Name == "X-BoletoCloud-Version"))
                {
                    result.VersaoApiBoletoCloud = response.Headers.FirstOrDefault(t => t.Name == "X-BoletoCloud-Version").Value.ToString();
                }

                if (response.Headers.Any(t => t.Name == "X-BoletoCloud-Token"))
                {
                    result.Token = response.Headers.FirstOrDefault(t => t.Name == "X-BoletoCloud-Token").Value.ToString();
                }

                if (response.Headers.Any(t => t.Name == "X-BoletoCloud-NIB-Nosso-Numero"))
                {
                    result.NossoNumero = response.Headers.FirstOrDefault(t => t.Name == "X-BoletoCloud-NIB-Nosso-Numero").Value.ToString();
                }

                if (response.Headers.Any(t => t.Name == "Content-Type"))
                {
                    result.ContentType = response.Headers.FirstOrDefault(t => t.Name == "Content-Type").Value.ToString();
                }

                if (response.Headers.Any(t => t.Name == "Location"))
                {
                    result.Location = response.Headers.FirstOrDefault(t => t.Name == "Location").Value.ToString();
                }

                result.Sucesso = true;

                result.Mensagem = response.StatusCode == HttpStatusCode.Created ?
                    $"Boleto n. {result.NossoNumero} gerado com sucesso" :
                    "Consulta realizada com sucesso";

                result.Boleto = response.RawBytes;
            }
            else
            {
                // Falha 
                ResponseError error = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseError>(response.Content);

                result.Sucesso = false;
                result.Erro.TipoErro = error.erro.tipo;
                result.Erro.Codigo = error.erro.status;

                for (int i = 0; i < error.erro.causas.Count; i++)
                    result.Erro.Mensagens.Add($"{error.erro.causas[i].codigo} - {error.erro.causas[i].mensagem}");
            }
        }

        /// <summary>
        /// Preenche parâmetros para geração do corpo da requisição
        /// </summary>
        /// <returns></returns>
        string FillParameters()
        {
            var builder = new StringBuilder();

            builder.Append($@"boleto.conta.banco={BoletoInfo.ContaInfo.CodigoBanco}");
            builder.Append($"&boleto.conta.agencia={BoletoInfo.ContaInfo.CodigoAgencia}");
            builder.Append($"&boleto.conta.numero={BoletoInfo.ContaInfo.Numero}");
            builder.Append($"&boleto.conta.carteira={BoletoInfo.ContaInfo.Carteira}");
            builder.Append($"&boleto.beneficiario.nome={BoletoInfo.BeneficiarioInfo.Nome}");
            builder.Append($"&boleto.beneficiario.cprf={BoletoInfo.BeneficiarioInfo.CPFCNPJ}");
            builder.Append($"&boleto.beneficiario.endereco.cep={BoletoInfo.BeneficiarioInfo.EnderecoInfo.CEP}");
            builder.Append($"&boleto.beneficiario.endereco.uf={BoletoInfo.BeneficiarioInfo.EnderecoInfo.UF}");
            builder.Append($"&boleto.beneficiario.endereco.localidade={BoletoInfo.BeneficiarioInfo.EnderecoInfo.Localidade}");
            builder.Append($"&boleto.beneficiario.endereco.bairro={BoletoInfo.BeneficiarioInfo.EnderecoInfo.Bairro}");
            builder.Append($"&boleto.beneficiario.endereco.logradouro={BoletoInfo.BeneficiarioInfo.EnderecoInfo.Logradouro}");
            builder.Append($"&boleto.beneficiario.endereco.numero={BoletoInfo.BeneficiarioInfo.EnderecoInfo.Numero}");
            builder.Append($"&boleto.beneficiario.endereco.complemento={BoletoInfo.BeneficiarioInfo.EnderecoInfo.Complemento}");
            builder.Append($"&boleto.emissao={BoletoInfo.DataEmissao.FormatDate()}");
            builder.Append($"&boleto.vencimento={BoletoInfo.Vencimento.FormatDate()}");
            builder.Append($"&boleto.documento={BoletoInfo.Documento}");

            if (!string.IsNullOrEmpty(BoletoInfo.Numero))
                builder.Append($"&boleto.numero={BoletoInfo.Numero}");
            else
                builder.Append($"&boleto.sequencial={BoletoInfo.Sequencial}");

            builder.Append($"&boleto.titulo={BoletoInfo.Titulo}");
            builder.Append($"&boleto.valor={BoletoInfo.Valor.FormatDecimal()}");
            builder.Append($"&boleto.juros={BoletoInfo.Juros.FormatDecimal()}");
            builder.Append($"&boleto.multa={BoletoInfo.Multa.FormatDecimal()}");
            builder.Append($"&boleto.pagador.nome={BoletoInfo.PagadorInfo.Nome}");
            builder.Append($"&boleto.pagador.cprf={BoletoInfo.PagadorInfo.CPFCNPJ}");
            builder.Append($"&boleto.pagador.endereco.cep={BoletoInfo.PagadorInfo.EnderecoInfo.CEP}");
            builder.Append($"&boleto.pagador.endereco.uf={BoletoInfo.PagadorInfo.EnderecoInfo.UF}");
            builder.Append($"&boleto.pagador.endereco.localidade={BoletoInfo.PagadorInfo.EnderecoInfo.Localidade}");
            builder.Append($"&boleto.pagador.endereco.bairro={BoletoInfo.PagadorInfo.EnderecoInfo.Bairro}");
            builder.Append($"&boleto.pagador.endereco.logradouro={BoletoInfo.PagadorInfo.EnderecoInfo.Logradouro}");
            builder.Append($"&boleto.pagador.endereco.numero={BoletoInfo.PagadorInfo.EnderecoInfo.Numero}");
            builder.Append($"&boleto.pagador.endereco.complemento={BoletoInfo.PagadorInfo.EnderecoInfo.Complemento}");

            for (int i = 0; i < BoletoInfo.Instrucoes.Count; i++)
                builder.Append($"&boleto.instrucao={BoletoInfo.Instrucoes[i]}");

            return builder.ToString();
        }

        /// <summary>
        /// Consome Api do Boleto Cloud para consulta de boletos gerados
        /// </summary>
        /// <param name="token">Token retornado na geração do boleto</param>
        /// <returns></returns>
        public BoletoCloudResponse ConsultaBoleto(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("Token para consulta não informado");

            if (string.IsNullOrEmpty(this.Config.Url))
                throw new ArgumentNullException("Url da Api não configurada");

            if (string.IsNullOrEmpty(this.Config.Token))
                throw new ArgumentNullException("Token da Api não configurado");

            var result = new BoletoCloudResponse();
            var client = new RestClient($"{this.Config.Url}/{token}");

            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "41a04afb-9aeb-4755-8ffd-c97380c04dfd");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", $"Basic {this.Config.AuthorizationHash}");
            IRestResponse response = client.Execute(request);

            if (response.ResponseStatus == ResponseStatus.Error && response.ErrorException != null)
                throw response.ErrorException;

            TrataResposta(result, response);

            return result;
        }
    }
}
