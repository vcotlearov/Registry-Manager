using System.Collections.Generic;

namespace Registry_Manager.UI.Models
{
    public class RMTemplate
    {
        public string Name { get; set; }
        public List<RMParameter> Parameters { get; set; }

        public RMTemplate()
        {
            Parameters = new List<RMParameter>();
        }
    }
}
