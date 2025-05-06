namespace todo_client
{
    public static class SessionManager
    {
        public static UserData CurrentUser { get; set; }

        // Store the user data 
        public static void SetUser(UserData user)
        {
            CurrentUser = user;
        }

        // Clear the current user session
        public static void ClearUser()
        {
            CurrentUser = null;
        }
    }
}