
using System.Text;
using System.Text.Json;


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
                http://10.16.199.202:3000/users/register
                Console.WriteLine($"Attempting to connect to: {url}");
                
                //Requesting register to the server
                var response = await _httpClient.PostAsync(url, content);
                Console.WriteLine($"Response status code: {response.StatusCode}");
                
                //successful registration
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Registration successful!");
                }
                else//not successful registration
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
        public async Task<(bool Success, string Message, UserData User)> LoginUser(string email, string password)
        {
            try
            {
                var requestData = new { email, password };
                var json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{Constants.SERVERURL}{Constants.LOGIN}";
                var response = await _httpClient.PostAsync(url, content);

                Console.WriteLine($"Response status code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    // Read response content
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response Content: {responseContent}");
                    
                    // Deserialize the response to get the 'user' object from the response body
                    var responseObject = JsonSerializer.Deserialize<ResponseWrapper>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // Check if the 'user' object is not null
                    if (responseObject?.User != null)
                    {
                        // Return the user object from the response
                        return (true, "Login successful!", responseObject.User);
                    }
                    else
                    {
                        return (false, "User data is null", null);
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        using var jsonDoc = JsonDocument.Parse(errorContent);
                        if (jsonDoc.RootElement.TryGetProperty("message", out var messageProperty))
                        {
                            return (false, messageProperty.GetString() ?? "Login failed with no message", null);
                        }
                        else
                        {
                            return (false, "Login failed: Unexpected error format", null);
                        }
                    }
                    catch
                    {
                        return (false, "Login failed: Invalid error response", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Exception occurred: {ex.Message}", null);
            }
        }
        public class ResponseWrapper
        {
            public UserData User { get; set; }
        }
        
        // Get all task lists from the database
        public async Task<List<TaskList>> GetTasksAsync(int userId)
        {
            try
            {
                var url = $"{Constants.SERVERURL}{Constants.GET_TASKS}{userId}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<TaskList>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<TaskList>();
                }

                Console.WriteLine($"Failed to get tasks: {response.StatusCode}");
                return new List<TaskList>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tasks: {ex.Message}");
                return new List<TaskList>();
            }
        }
        
        // Create task
        public async Task<bool> CreateTaskAsync(TaskList newTask)
        {
            try
            {
                var json = JsonSerializer.Serialize(newTask);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{Constants.SERVERURL}{Constants.CREATE_TASK}";
                var response = await _httpClient.PostAsync(url, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating task: {ex.Message}");
                return false;
            }
        }

        // Patch task
        public async Task<bool> UpdateTaskAsync(int taskId, Dictionary<string, object> updates)
        {
            try
            {
                var json = JsonSerializer.Serialize(updates);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"{Constants.SERVERURL}{Constants.PATCH_TASK}{taskId}";
                var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };

                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating task: {ex.Message}");
                return false;
            }
        }
        
        //Delete task
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            try
            {
                var url = $"{Constants.SERVERURL}{Constants.DELETE_TASK}{taskId}";
                var response = await _httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting task: {ex.Message}");
                return false;
            }
        }
    }
}