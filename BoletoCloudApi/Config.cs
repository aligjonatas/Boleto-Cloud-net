using System.Configuration;
using System.Text;

namespace BoletoCloudApi
{
    public class Config
    {
        /// <summary>
        /// Token gerado no Boleto Cloud
        /// </summary>
        public string Token
        {
            get
            {
                return ConfigurationManager.AppSettings["BoletoCloud:Token"];
            }
        }

        /// <summary>
        /// Url da Api
        /// </summary>
        public string Url
        {
            get
            {
                return ConfigurationManager.AppSettings["BoletoCloud:Url"];
            }
        }

        /// <summary>
        /// Hash de autorização para consumo da Api
        /// </summary>
        public string AuthorizationHash
        {
            get
            {
                return ConvertStringToBase64($"{Token}:token");
            }
        }

        public string ConvertStringToBase64(string toEncode)
        {
            return ConvertStringToBase64(toEncode, null);
        }
        public string ConvertStringToBase64(string toEncode, Encoding encoding)
        {
            Encoding textEncoding = encoding != null ? encoding : ASCIIEncoding.ASCII;
            byte[] toEncodeAsBytes
                  = textEncoding.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
    }
}
