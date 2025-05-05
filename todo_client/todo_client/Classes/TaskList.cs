using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace todo_client;

public class TaskList : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private int _id { get; set; }

    private int _userId { get; set;}
    
    private String _title { get; set; }
    
    private String _details { get; set; }
    
    private bool _completed { get; set; }

    public int id
    {
        get { return _id; }
        set { _id = value; OnPropertyChanged(nameof(id)); }
    }
    public int userId
    {
        get { return _userId; }
        set { _userId = value; OnPropertyChanged(nameof(_userId)); }
    }

    public String title
    {
        get { return _title; }
        set { _title = value; OnPropertyChanged(nameof(title)); }
    }

    public String details
    {
        get { return _details; }
        set { _details = value; OnPropertyChanged(nameof(details)); }
    }

    public bool completed
    {
        get { return _completed; }
        set { _completed = value; OnPropertyChanged(nameof(completed)); }
    }
}