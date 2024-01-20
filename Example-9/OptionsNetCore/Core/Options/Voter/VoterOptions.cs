using System.ComponentModel.DataAnnotations;

namespace OptionsNetCore.Core.Options.Voter
{
    public class VoterOptions
    {
        public const string Voter = "VoterSettings";

        public int Age { get; set; }

        public string Name { get; set; }

        public string Suburb { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

    }
}
