using Spectre.Console;
using Encryption;
using Utils;

class Program
{
    static void Main(string[] args)
    {

        // Load env variables
        EnvLoader.LoadEnv(".env");

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .PageSize(10)
                .AddChoices(new[] {
                    "Generate RSA key pair", "Encrypt", "Decrypt",
                }
            )
        );

        string inputPathPrompt = "";
        string outputPathPrompt = "";

        switch (choice)
        {
            case "Generate RSA key pair":
                inputPathPrompt = "[blue]Please specify the directory to save the public RSA key[/]";
                outputPathPrompt = "[blue]Please specify the directory to save the private RSA key[/]";
                break;
            case "Encrypt":
                inputPathPrompt = "[blue]Please specify the input path to the file to encrypt[/]";
                outputPathPrompt = "[blue]Please specify the output path for the encrypted file[/]";
                break;
            case "Decrypt":
                inputPathPrompt = "[blue]Please specify the input path to the file to decrypt[/]";
                outputPathPrompt = "[blue]Please specify the output path for the decrypted file[/]";
                break;
            default:
                Console.WriteLine("Unexpected value");
                Environment.Exit(1);
                break;
        }

        var inputPath = AnsiConsole.Prompt(
            new TextPrompt<string>($"[blue]{inputPathPrompt}[/]")
                .AllowEmpty()
                .ValidationErrorMessage("[red]Validation Error[/]")
                .Validate(inputPath =>
                {
                    if (string.IsNullOrWhiteSpace(inputPath))
                    {
                        return ValidationResult.Error("[red]Path cannot be empty[/]");
                    }
                    else if (inputPath.Length < 3)
                    {
                        return ValidationResult.Error("[red]Path is too short[/]");
                    }
                    else
                    {
                        return ValidationResult.Success();
                    }
                }
            )
        );

        var outputPath = AnsiConsole.Prompt(
            new TextPrompt<string>($"[blue]{outputPathPrompt}[/]")
                .AllowEmpty()
                .ValidationErrorMessage("[red]Validation Error[/]")
                .Validate(outputPath =>
                {
                    if (string.IsNullOrWhiteSpace(inputPath))
                    {
                        return ValidationResult.Error("[red]Path cannot be empty[/]");
                    }
                    else if (outputPath.Length < 3)
                    {
                        return ValidationResult.Error("[red]Path is too short[/]");
                    }
                    else
                    {
                        return ValidationResult.Success();
                    }
                }
            )
        );

        if (choice == "Generate RSA key pair")
        {
            AnsiConsole.Markup("[red]Not yet implemented[/]");
            Console.Out.Flush();
        }
        else if (choice == "Encrypt")
        {
            EncryptionHelper.EncryptFile(inputPath, outputPath);
            AnsiConsole.Markup($"[green]File encrypted and saved to {outputPath}[/]");
        }
        else if (choice == "Decrypt")
        {
            EncryptionHelper.DecryptFile(inputPath, outputPath);
            AnsiConsole.Markup($"[green]File decrypted and saved to {outputPath}[/]");
        }
        else
        {
            Console.WriteLine("Unexpected value");
            Environment.Exit(1);
        }
    }
}
