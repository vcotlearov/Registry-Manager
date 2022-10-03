using System.Collections.Generic;

namespace Registry_Manager.UI.Models
{
    public class RMGroup
    {
        public string Name { get; set; }
        public List<RMRecord> Records { get; set; }

        public RMGroup()
        {
            Records = new List<RMRecord>();
        }
    }
}
