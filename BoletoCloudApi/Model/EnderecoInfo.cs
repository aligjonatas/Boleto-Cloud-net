namespace BoletoCloudApi.Model
{
    public class EnderecoInfo
    {
        /// <summary>
        /// CEP - Código de Endereçamento Postal
        /// </summary>
        public string CEP { get; set; }

        /// <summary>
        /// UF - Unidade Federativa
        /// </summary>
        public string UF { get; set; }

        /// <summary>
        /// Nome da localidade/cidade
        /// </summary>
        public string Localidade { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string Bairro { get; set; }

        /// <summary>
        /// Avenida, rodovia, rua, etc
        /// </summary>
        public string Logradouro { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Complemento, referência, etc
        /// </summary>
        public string Complemento { get; set; }
    }
}
