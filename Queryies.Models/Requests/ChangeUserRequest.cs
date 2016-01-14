namespace Queryies.Models.Requests
{
    public class ChangeUserRequest
    {
        public string UserName { get; set; }
        public string DateForNewRole { get; set; }
        public string NewRole { get; set; }
    }
}
