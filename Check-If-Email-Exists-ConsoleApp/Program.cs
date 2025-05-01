using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailVerifierConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Email Verification Tool");
            Console.WriteLine("----------------------");

            while (true)
            {
                Console.Write("Enter email to check (or 'exit' to quit): ");
                string email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(email))
                    continue;

                if (email.ToLower() == "exit")
                    break;

                await VerifyEmail(email);
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static async Task VerifyEmail(string email)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var payload = new { to_email = email };
                    string jsonPayload = JsonSerializer.Serialize(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    Console.WriteLine("Sending request to verify email...");
                    HttpResponseMessage response = await client.PostAsync("http://localhost:8080/v0/check_email", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        using var document = JsonDocument.Parse(result);
                        var root = document.RootElement;
                        Console.WriteLine("\nVerification Result:");
                        Console.WriteLine($"Input: {root.GetProperty("input").GetString()}");
                        Console.WriteLine($"Reachability: {root.GetProperty("is_reachable").GetString()}");
                        var misc = root.GetProperty("misc");
                        Console.WriteLine($"Disposable: {misc.GetProperty("is_disposable").GetBoolean()}");
                        Console.WriteLine($"Role Account: {misc.GetProperty("is_role_account").GetBoolean()}");
                        Console.WriteLine($"B2C: {misc.GetProperty("is_b2c").GetBoolean()}");
                        var mx = root.GetProperty("mx");
                        Console.WriteLine($"Accepts Mail: {mx.GetProperty("accepts_mail").GetBoolean()}");
                        var smtp = root.GetProperty("smtp");
                        Console.WriteLine($"Can Connect SMTP: {smtp.GetProperty("can_connect_smtp").GetBoolean()}");
                        Console.WriteLine($"Is Deliverable: {smtp.GetProperty("is_deliverable").GetBoolean()}");
                        var syntax = root.GetProperty("syntax");
                        Console.WriteLine($"Valid Syntax: {syntax.GetProperty("is_valid_syntax").GetBoolean()}");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {(int)response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Make sure the Docker container is running: docker run -p 8080:8080 reacherhq/backend:latest");
            }
        }
    }
}
