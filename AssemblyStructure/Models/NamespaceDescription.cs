using System.Collections.Generic;
using System.Linq;

namespace AssemblyStructure.Models
{
    public class NamespaceDescription
    {
        public readonly string Name;
        public readonly List<TypeDescription> Types;

        public NamespaceDescription(string name, IEnumerable<TypeDescription> types = null)
        {
            Name = name;
            Types = types?.ToList() ?? new List<TypeDescription>();
        }
    }
}