using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace todo_client
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                // For development only - don't validate certificates in production!
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            
            _httpClient = new HttpClient(handler);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        // Test connection to server
        public async Task<bool> TestConnection()
        {
            try
            {
                var url = $"{Constants.SERVERURL}";
                Console.WriteLine($"Testing connection to: {url}");
                
                var response = await _httpClient.GetAsync(url);
                Console.WriteLine($"Connection test response: {response.StatusCode}");
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection test error: {ex}");
                return false;
            }
        }

        // Register new user
        public async Task<(bool Success, string Message)> RegisterUser(string fname, string lname, string email, string password)
        {
            try
            {
                // Prepare request body
                var requestData = new
                {
                    fname,
                    lname,
                    email,
                    password
                };

                var json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                //constructing the API URl register endpoint
                var url = $"{Constants.SERVERURL}{Constants.REGISTER}";
                Console.WriteLine($"Attempting to connect to: {url}");
                
                //Requesting register to the server
                var response = await _httpClient.PostAsync(url, content);
                Console.WriteLine($"Response status code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Registration successful!");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        using var jsonDoc = JsonDocument.Parse(errorContent);
                        if (jsonDoc.RootElement.TryGetProperty("errors", out var errorProperty))
                        {
                            return (false, errorProperty.GetString() ?? "Unknown error");
                        }
                        else if (jsonDoc.RootElement.TryGetProperty("message", out var messageProperty))
                        {
                            return (false, messageProperty.GetString() ?? "Unknown error");
                        }else
                        {
                            return (false, "Unexpected error response");
                        }
                    }
                    catch
                    {
                        return (false, "Error with your inputs");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration error: {ex}");
                return (false, $"Connection failure: {ex.Message}");
            }
        }

        // Login user
        public async Task<(bool Success, string Message)> LoginUser(string email, string password)
        {
            try
            {
                // Prepare request body
                var requestData = new
                {
                    email,
                    password
                };
        
                var json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                //Constructing API URL login endpoint
                var url = $"{Constants.SERVERURL}{Constants.LOGIN}";
                Console.WriteLine($"Attempting to connect to: {url}");
        
                //Requesting login to the server
                var response = await _httpClient.PostAsync(url, content);
                Console.WriteLine($"Response status code: {response.StatusCode}");
        
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Login successful!");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        using var jsonDoc = JsonDocument.Parse(errorContent);
                        if (jsonDoc.RootElement.TryGetProperty("errors", out var errorProperty))
                        {
                            return (false, errorProperty.GetString() ?? "Unknown error");
                        }
                        else if (jsonDoc.RootElement.TryGetProperty("message", out var messageProperty))
                        {
                            return (false, messageProperty.GetString() ?? "Unknown error");
                        }
                        else
                        {
                            return (false, "Unexpected error response");
                        }
                    }
                    catch
                    {
                        return (false, "Error with your inputs");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex}");
                return (false, $"Connection failure: {ex.Message}");
            }
        }
    }
}