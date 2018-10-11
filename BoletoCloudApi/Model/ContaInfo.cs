namespace BoletoCloudApi.Model
{
    public class ContaInfo
    {
        /// <summary>
        /// Código de compensação BACEN do banco.
        /// <para>Ex. Bradesco: 237</para>
        /// </summary>
        public string CodigoBanco { get; set; }

        /// <summary>
        /// O número da agência deve ser informado no formato padrão ("9999-D"), onde "9" corresponde a um dígito de zero a nove e "D", que representa o dígito verificador
        /// </summary>
        public string CodigoAgencia { get; set; }

        /// <summary>
        /// O número da conta deve ser informado no formato padrão ("9999999-D"), onde "9" corresponde a um dígito de zero a nove e "D", que representa o dígito verificador
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// O número da carteira de cobrança deve ser um número entre 1 e 99.
        /// </summary>
        public string Carteira { get; set; }
    }
}
