using System;
using System.Configuration;

namespace QuoteService.Features.Quotes
{
    public interface IQuoteConfiguration
    {
        string Origin { get; set; }
    }

    public class QuoteConfiguration: ConfigurationSection, IQuoteConfiguration
    {

        [ConfigurationProperty("baseAddress")]
        public string Origin
        {
            get { return (string)this["baseAddress"]; }
            set { this["baseAddress"] = value; }
        }

        public static IQuoteConfiguration Config
        {
            get { return ConfigurationManager.GetSection("quoteConfiguration") as IQuoteConfiguration; }
        }
    }
}
