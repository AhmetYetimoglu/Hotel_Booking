using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webui.Models
{
    public class AlertMessage
    {
        public List<string> Message { get; set; }
        public string AlertType { get; set; }
    }
}