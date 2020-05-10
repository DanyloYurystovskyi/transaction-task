using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TransactionService.BLL.Extensions
{
    public static class CurrencyStringExtensions
    {
        public static bool IsISO4217(this string currency)
        {
            if (currency.Length != 3)
                return false;

            var currencySymbols = CultureInfo
                .GetCultures(CultureTypes.SpecificCultures) //Only specific cultures contain region information
                .Select(x => (new RegionInfo(x.LCID)).ISOCurrencySymbol)
                .Distinct();

            return currencySymbols.Contains(currency);
        }
    }
}
