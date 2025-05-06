namespace todo_client;

public class Constants
{
    
    //"http://10.16.199.202:3000
    public static readonly string SERVERURL = "http://todoappcv.dcism.org";
    // User Endpoints
    public static readonly string REGISTER = "/users/register"; 
    public static readonly string LOGIN = "/users/login";
    // Task Endpoints
    public static readonly string CREATE_TASK = "/todos/";
    public static readonly string GET_TASKS = "/todos?userId=";
    public static readonly string PATCH_TASK = "/todos/"; // Append {id}
    public static readonly string DELETE_TASK = "/todos/"; // Append {id}

}