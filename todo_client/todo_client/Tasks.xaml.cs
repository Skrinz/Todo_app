using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class Tasks : ContentPage
{
    NetworkHelper networkHelper;
    HttpClient client;
    CancellationToken cts;
    CancellationTokenSource cs = new CancellationTokenSource();
    HttpResponseMessage response;
    ObservableCollection<TaskList> tasks = new ObservableCollection<TaskList>();
    
    public Tasks()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        
        networkHelper = new NetworkHelper();
        client = new HttpClient();
        cts = cs.Token;
        client.DefaultRequestHeaders.Accept.Clear();
        client.MaxResponseContentBufferSize = 256000;
        client.Timeout = TimeSpan.FromSeconds(3);
        tasksLV.ItemsSource = tasks;
    }
    
    async protected override void OnAppearing()
    {
        base.OnAppearing();

        // Only load tasks if they haven't been loaded yet
        if (tasks.Count == 0)
        {
            activityIndicator.IsRunning = true;
            // GET tasks only if needed
            if (networkHelper.HasInternet())
            {
                if (await networkHelper.IsHostReachable())
                {
                    var uri = new Uri(Constants.URL + Constants.POSTS);
                    response = await client.GetAsync(uri, cts);
                
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        JObject jObject = new JObject();
                        try
                        {
                            jObject = JObject.Parse(result);
                            ReceivedResult(jObject);
                        }
                        catch (Exception)
                        {
                            JArray jA = JArray.Parse(result);
                            jObject = JObject.Parse("{\"count\":" + jA.Count + ",\"data\":" + JsonConvert.SerializeObject(jA) + "}");
                            ReceivedResult(jObject);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error!", response.StatusCode.ToString(), "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Host Unreachable!", "The URL host for ToDo cannot be reached. Please try again later!", "OK");
                }
            }
            else
            {
                await DisplayAlert("No Internet Connection!", "Please check your internet connection and try again!", "OK");
            }
            activityIndicator.IsRunning = false;
        }
    }
    
    private void ReceivedResult(JObject jsonData)
    {
        tasks.Clear();
        for (int x = 0; x < Convert.ToInt32(jsonData["count"]); x++)
        {
            TaskList i = JsonConvert.DeserializeObject<TaskList>(jsonData["data"][x].ToString());
            tasks.Add(i);
        }
        activityIndicator.IsRunning = false;
    }

    private void TasksLV_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        ((CollectionView)sender).SelectedItem = null;
    }
    
    private async void OnRefresh(object sender, EventArgs e)
    {
        // Show a loading indicator
        activityIndicator.IsRunning = true;

        // Refresh the tasks
        await RefreshTasksAsync();

        // Stop the refresh animation
        activityIndicator.IsRunning = false;
        refreshView.IsRefreshing = false;
    }
    
    private async Task RefreshTasksAsync()
    {
        // Clear existing tasks if needed and fetch new data.
        tasks.Clear();
    
        if (networkHelper.HasInternet())
        {
            if (await networkHelper.IsHostReachable())
            {
                var uri = new Uri(Constants.URL + Constants.POSTS);
                response = await client.GetAsync(uri, cts);
            
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    JObject jObject = new JObject();
                    try
                    {
                        jObject = JObject.Parse(result);
                        ReceivedResult(jObject);
                    }
                    catch (Exception)
                    {
                        JArray jA = JArray.Parse(result);
                        jObject = JObject.Parse("{\"count\":" + jA.Count + ",\"data\":" + JsonConvert.SerializeObject(jA) + "}");
                        ReceivedResult(jObject);
                    }
                }
                else
                {
                    await DisplayAlert("Error!", response.StatusCode.ToString(), "OK");
                }
            }
            else
            {
                await DisplayAlert("Host Unreachable!", "The URL host for ToDo cannot be reached. Please try again later!", "OK");
            }
        }
        else
        {
            await DisplayAlert("No Internet Connection!", "Please check your internet connection and try again!", "OK");
        }
    }

    private void Delete_OnTapped(object? sender, TappedEventArgs e)
    {
        
    }
}