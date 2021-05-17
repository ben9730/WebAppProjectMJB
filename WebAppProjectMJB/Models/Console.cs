using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppProjectMJB.Models
{
    public class Console
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Game> Games { get; set; }

        public Accessories Accessories { get; set; }

    }
}
