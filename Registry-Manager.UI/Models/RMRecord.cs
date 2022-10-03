using System.Collections.Generic;

namespace Registry_Manager.UI.Models
{
    public class RMRecord
    {
        public string Name { get; set; }
        public string TemplateName { get; set; }
        public List<RMParameter> Parameters { get; set; }

        public RMRecord()
        {
            Parameters = new List<RMParameter>();
        }
    }
}