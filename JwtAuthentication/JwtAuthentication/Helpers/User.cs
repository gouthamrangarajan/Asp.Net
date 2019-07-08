using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Helpers
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Screens { get; set; }
    }
}
