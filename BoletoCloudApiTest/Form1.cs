using BoletoCloudApi;
using BoletoCloudApi.Model;
using System;
using System.Windows.Forms;

namespace BoletoCloudApiTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCriaBoleto_Click(object sender, EventArgs e)
        {

            var boletoInfo = new BoletoInfo();

            // Dados da Conta
            boletoInfo.ContaInfo.CodigoBanco = "237";
            boletoInfo.ContaInfo.CodigoAgencia = "1234-5";
            boletoInfo.ContaInfo.Numero = "123456-1";
            boletoInfo.ContaInfo.Carteira = "12";

            // Dados do boleto
            boletoInfo.DataEmissao = new DateTime(2018, 10, 10);
            boletoInfo.Vencimento = new DateTime(2018, 10, 30);
            boletoInfo.Documento = "EX1";

            //boletoInfo.Numero = "12345678905-P"; // Não pode repetir. Usar Numero ou Sequencial
            boletoInfo.Sequencial = "2"; // Utilizar este ou número (Ref.: https://boleto.cloud/app/dev/api#boletos-criar-numeracao)

            boletoInfo.Titulo = "DM";
            boletoInfo.Valor = 850.2M;
            boletoInfo.Juros = 2.5M;
            boletoInfo.Multa = 50.0M;

            // Comentários
            boletoInfo.Instrucoes.Add("Comentário linha 1");
            boletoInfo.Instrucoes.Add("Comentário linha 2");
            boletoInfo.Instrucoes.Add("Comentário linha 3");

            // Informações do Beneficiário
            boletoInfo.BeneficiarioInfo.Nome = "Teste API";
            boletoInfo.BeneficiarioInfo.CPFCNPJ = "15.719.277/0001-46";
            boletoInfo.BeneficiarioInfo.EnderecoInfo.CEP = "59020-000";
            boletoInfo.BeneficiarioInfo.EnderecoInfo.UF = "RN";
            boletoInfo.BeneficiarioInfo.EnderecoInfo.Localidade = "Natal";
            boletoInfo.BeneficiarioInfo.EnderecoInfo.Bairro = "Petrópolis";
            boletoInfo.BeneficiarioInfo.EnderecoInfo.Logradouro = "Avenida Hermes da Fonseca";
            boletoInfo.BeneficiarioInfo.EnderecoInfo.Numero = "384";
            boletoInfo.BeneficiarioInfo.EnderecoInfo.Complemento = "Sala 2A, segundo andar";

            // Dados do Pagador
            boletoInfo.PagadorInfo.Nome = "Alberto Santos Dumont";
            boletoInfo.PagadorInfo.CPFCNPJ = "724.697.190-46";
            boletoInfo.PagadorInfo.EnderecoInfo.CEP = "36240-000";
            boletoInfo.PagadorInfo.EnderecoInfo.UF = "MG";
            boletoInfo.PagadorInfo.EnderecoInfo.Localidade = "Santos Dumont";
            boletoInfo.PagadorInfo.EnderecoInfo.Bairro = "Casa Natal";
            boletoInfo.PagadorInfo.EnderecoInfo.Logradouro = "BR-499";
            boletoInfo.PagadorInfo.EnderecoInfo.Numero = "s/n";
            boletoInfo.PagadorInfo.EnderecoInfo.Complemento = "Sítio - Subindo a serra da Mantiqueira";

            var service = new Service(boletoInfo);

            // Faz a chamada para geração
            // PS.: No retorno a property 'Boleto' já vem preenchida. O ideal é armazenar o Número e o Token do boleto
            var response = service.GeraBoleto();

            if (response.Sucesso)
            {
                System.IO.File.WriteAllBytes($@"c:\temp\{response.NossoNumero}.pdf", response.Boleto);
            }
            else
            {
                // falha... analisar boletoCloudResponse.Erro
            }
        }

        private void btnConsultaBoleto_Click(object sender, EventArgs e)
        {
            var service = new Service();
            var response = service.ConsultaBoleto("aw7AxQlgrpTQPRdg_pqgmNXVSNMHOQpayoANmMrfo7c=");

            if(response.Sucesso)
            {
                System.IO.File.WriteAllBytes($@"c:\temp\{Guid.NewGuid()}.pdf", response.Boleto);
            }
            else
            {
                // falha... analisar boletoCloudResponse.Erro
            }
        }
    }
}
