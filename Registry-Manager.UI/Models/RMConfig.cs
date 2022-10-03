using System.Collections.Generic;

namespace Registry_Manager.UI.Models
{
    public class RMConfig
    {
        public string Name { get; set; }
        public List<RMTemplate> Templates { get; set; }
        public List<RMGroup> Groups { get; set; }

        public RMConfig()
        {
            Templates = new List<RMTemplate>();
            Groups = new List<RMGroup>();
        }
    }
}
