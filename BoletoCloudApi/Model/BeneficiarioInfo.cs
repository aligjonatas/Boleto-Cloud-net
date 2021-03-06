﻿namespace BoletoCloudApi.Model
{
    public class BeneficiarioInfo
    {
        EnderecoInfo enderecoInfo;

        /// <summary>
        /// Nome, nome fantasia, razão social
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Cadastro de Pessoa na Receita Federal: CPF ou CNPJ
        /// </summary>
        public string CPFCNPJ { get; set; }

        /// <summary>
        /// Endereco do Beneficiario
        /// </summary>
        public EnderecoInfo EnderecoInfo
        {
            get
            {
                if (enderecoInfo == null)
                    enderecoInfo = new EnderecoInfo();
                return enderecoInfo;
            }
            set
            {
                enderecoInfo = value;
            }
        }
    }
}
