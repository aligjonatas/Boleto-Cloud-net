using System;
using System.Collections.Generic;

namespace BoletoCloudApi.Model
{
    /// <summary>
    /// Campos da Requisição
    /// Ref.: https://boleto.cloud/app/dev/api#boletos-criar-campos
    /// </summary>
    public class BoletoInfo
    {
        public BoletoInfo()
        {
            Instrucoes = new List<string>();
        }

        ContaInfo contaInfo;
        BeneficiarioInfo beneficiarioInfo;
        PagadorInfo pagadorInfo;

        /// <summary>
        /// Data na qual o documento de cobrança foi emitido. Sempre deve ser uma data igual a atual ou anterior (no passado) a data atual. 
        /// <para>Obs: Essa data não é a mesma do campo "Data de Processamento" do boleto, pois a última é gerada pela API.</para>
        /// </summary>
        public DateTime DataEmissao { get; set; }

        /// <summary>
        /// Data limite definida para pagamento do boleto. Sempre deve ser uma data igual a atual ou posterior (futuro) a data atual.
        /// </summary>
        public DateTime Vencimento { get; set; }

        /// <summary>
        /// Número do documento: geralmente é um número de controle do sistema de informação do beneficiário, usado para identificar o documento de origem do beneficiário.
        /// </summary>
        public string Documento { get; set; }

        /// <summary>
        /// Número Identificador Bancário(NIB), denominado pelo banco como "Nosso Número" utilizado para identificar o título de forma única no sistema de cobrança. Esse número tem sua composição baseada em regras que variam em função do banco e do serviço de cobrança, caso deseje que esse número seja gerado pela API utilize o campo 'Sequencial'
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Número sequencial utilizado como base para gerar o NIB, este deve ser um número sequencial e único informado pelo seu sistema. O número de dígitos permitidos para esse campo pode variar dependendo do banco. Se esse campo for informado, então o campo 'Numero' não deve ser informado
        /// </summary>
        public string Sequencial { get; set; }

        /// <summary>
        /// Sigla que identifica o tipo de título de cobrança. 
        /// <para>Valores: AP, CC, CH, CPR, DAE, DAM, DAU, DD, DM, DMI, DR, DS, DSI, EC, FAT, LC, ME, NCC, NCE, NCI, NCR, ND, NF, NP, NPR, NS, O, PC, RC, TM, TS, W.</para>
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// False por padrão. O campo aceite indica se o pagador reconheceu ou não o título de cobrança como sendo dele (contra ele).
        /// </summary>
        public bool Aceite { get; set; }

        /// <summary>
        /// Valor nominal do documento de cobrança, ou seja, o valor do boleto.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Porcentagem de juros a ser calculada por dia, com base no valor do documento, campo 'Valor'.
        /// </summary>
        public decimal Juros { get; set; }

        /// <summary>
        /// Porcentagem de multa a ser calculada com base no valor do documento, campo 'Valor'.
        /// </summary>
        public decimal Multa { get; set; }

        /// <summary>
        /// Instrução ao caixa, até 8 campos desse podem estar presentes em uma única requisição, o que corresponde as 8 linhas presentes no campo instruções ao caixa na ficha de compensação do boleto padrão.
        /// </summary>
        public List<string> Instrucoes { get; set; }

        public ContaInfo ContaInfo
        {
            get
            {
                if (contaInfo == null)
                    contaInfo = new ContaInfo();
                return contaInfo;
            }
            set
            {
                contaInfo = value;
            }
        }

        public BeneficiarioInfo BeneficiarioInfo
        {
            get
            {
                if (beneficiarioInfo == null)
                    beneficiarioInfo = new BeneficiarioInfo();
                return beneficiarioInfo;
            }
            set
            {
                beneficiarioInfo = value;
            }
        }

        public PagadorInfo PagadorInfo
        {
            get
            {
                if (pagadorInfo == null)
                    pagadorInfo = new PagadorInfo();
                return pagadorInfo;
            }
            set
            {
                pagadorInfo = value;
            }
        }
    }
}
