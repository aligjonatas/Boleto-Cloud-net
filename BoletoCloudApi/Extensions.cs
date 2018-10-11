using System;
using System.Globalization;

namespace BoletoCloudApi
{
    public static class Extensions
    {
        private static readonly CultureInfo CultureInfo = new CultureInfo("en-us");

        /// <summary>
        /// Formata valores decimais adicionando as casas quando necessário
        /// <para>Ex.: 8 vira 8.00</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatDecimal(this decimal value)
        {
            return value.ToString("n2", CultureInfo).Replace(",", string.Empty);
        }

        /// <summary>
        /// Formata datas com a máscara yyyy-MM-dd
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatDate(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }
    }
}
