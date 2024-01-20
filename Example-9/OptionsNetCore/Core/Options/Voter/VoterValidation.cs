using Microsoft.Extensions.Options;
using OptionsNetCore.Core.Options.Voter;

namespace OptionsNetCore.Core.Options.Voter
{
    public class VoterValidation : IValidateOptions<VoterOptions>
    {
        public ValidateOptionsResult Validate(string name, VoterOptions options)
        {
            string failures = null;

            if (options.Age <18)
                failures = "Voting age must be atleast 18;";

            if (!options.PostalCode.StartsWith('3'))
                failures += "Postal Code must start with 3 for this city;";

            if (!string.IsNullOrEmpty(failures))
                return ValidateOptionsResult.Fail(failures);

            return ValidateOptionsResult.Success;
        }
    }
}
