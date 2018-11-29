using System.Collections.Generic;
using System.Linq;

namespace AssemblyStructure.Models
{
    public class NamespaceDescription
    {
        public string Name { get; }
        public List<TypeDescription> Types { get; }

        public NamespaceDescription(string name, IEnumerable<TypeDescription> types = null)
        {
            Name = name;
            Types = types?.ToList() ?? new List<TypeDescription>();
        }
    }
}