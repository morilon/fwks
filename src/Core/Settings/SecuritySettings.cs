namespace Fwks.ExampleService.Core.Settings;

public sealed class SecuritySettings
{
    public AuthServerSettings AuthServer { get; set; }
    public CorsSettings Cors { get; set; }
    public string EncryptionKey { get; set; }
    public string ObfuscationKey { get; set; }
}