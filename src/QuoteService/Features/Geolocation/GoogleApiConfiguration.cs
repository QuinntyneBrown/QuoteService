using System;
using System.Configuration;

namespace QuoteService.Features.Geolocation
{    
    public interface IGoogleApiConfiguration
    {
        string ApiKey { get; set; }
    }

    public class GoogleApiConfiguration: ConfigurationSection, IGoogleApiConfiguration
    {

        [ConfigurationProperty("apiKey")]
        public string ApiKey
        {
            get { return (string)this["apiKey"]; }
            set { this["apiKey"] = value; }
        }

        
        public static readonly Lazy<IGoogleApiConfiguration> LazyConfig = new Lazy<IGoogleApiConfiguration>(() =>
        {
            var section = ConfigurationManager.GetSection("googleApiConfiguration") as IGoogleApiConfiguration;
            if (section == null)
            {
                throw new ConfigurationErrorsException("googleApiConfiguration");
            }

            return section;
        }, true);
    }
}
