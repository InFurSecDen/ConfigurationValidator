Configuration Validator
=======================

A small side project for validating the items loaded into your .NET Core application's global configuration.

Currently supported validation rules:

* `required`: Checks to see if the value for a specific key is set, and is not `null`.
    * _Valid values: `true`, `false`_

Future planned rules:

* `format`: Checks to see if the value matches a specific format, such as an `int`, `bool`, or `base64`/`byte[]` type
* `regex`: Checks to see if the value matches some supplied regex mask
* `maxlength`: Checks to see that the string for this value does not exceed some specified length
* `minlength`: Checks to see that the string for this value is not smaller than some specified length
* `minbytes`: Checks to see the value is at least a minimum length of bytes (requires `base64` `format` check)
* `matches`: Checks to see the value matches one of a specified list of values
* `uniquepassword`: Checks to see the value is not on a known list of compromised passwords (requires outbound internet connection to talk to https://haveibeenpwned.com/Passwords)
    * _This will throw a warning but **not an exception**, since this will leak the presence of secrets if a new password is added to Pwned Passwords that is used by the application. It is up to the developer to rectify this._

## How do I get this in my awesome project?

Add the `InFurSecDen.Utils.ConfigutationValidator` NuGet package using the usual NuGet methods:

Powershell:
```powershell
Install-Package InFurSecDen.Utils.ConfigutationValidator -IncludePrerelease
```

`dotnet` CLI:
```bash
dotnet add package InFurSecDen.Utils.ConfigurationValidator -prerelease
```

`.csproj`:
```xml
<PackageReference Include="InFurSecDen.Utils.ConfigutationValidator" Version="x.x.x"/>
```

## How do I use it?

### configurationschema.json

Create a file (usually `configurationschema.json`) in the following format:

```json
{
	"Key1": {
		"required": true
	},
	"Key2:SubKey1": {
		"required": true
	},
	"Key2:SubKey2": {
		"required": true
	}
}
```

### Configuration Builder

In your `IConfigurationBuilder` builder methods (usually in `Program.cs` as of ASP.NET Core 2.1), after all of the configuration sources are defined, add the following line (assuming `config` is the name of your `IConfigurationBuilder` object):

```csharp
var errors = config.Validate(File.ReadAllText("configurationschema.json"));
```

If `errors.Count` returns zero, the schema was successfully validated.

Optionally, you can throw an `AggregateException` with the following line:

```csharp
config.Validate(File.ReadAllText("configurationschema.json"), throwOnError: true);
```

## All this talk about NuGet is making me hungry for chicken nuggets! 🦊

Me too. If you're based in New Zealand, I recommend the BP Connect chicken nuggets, they're by far the biggest and have the tastiest seasonings. No, seriously.

Don't go to K-Fry. I don't know how but they somehow manage to totally fuck up cooking chicken, and this offends me mightily.