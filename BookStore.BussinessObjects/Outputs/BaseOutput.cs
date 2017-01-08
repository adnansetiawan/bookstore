using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Outputs
{
    public class BaseOutput
    {
        public bool Success { get; set; } = true;
        public string Messages { get; set; } 
    }
}
