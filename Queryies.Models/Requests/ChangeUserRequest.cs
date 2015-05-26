using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queryies.Models.Requests
{
    public class ChangeUserRequest
    {
        public string UserName { get; set; }
        public string DateForNewRole { get; set; }
        public string NewRole { get; set; }
    }
}
