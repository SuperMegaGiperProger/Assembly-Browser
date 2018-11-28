using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblyStructure.Models;

namespace AssemblyStructure
{
    public class AssemblyStructure
    {
        private Assembly _assembly;
        private Dictionary<string, NamespaceDescription> _namespaceDescriptions;
        public NamespaceDescription[] NamespaceDescriptions => _namespaceDescriptions.Values.ToArray();

        public AssemblyStructure(string assemblyPath)
        {
            _assembly = Assembly.LoadFile(assemblyPath);

            _namespaceDescriptions = new Dictionary<string, NamespaceDescription>();
            LoadAssemblyDescription();
        }

        private void LoadAssemblyDescription()
        {
            Array.ForEach(_assembly.GetTypes(), AddType);
        }

        private void AddType(Type type)
        {
            string namespaceName = type.Namespace; // TODO: case namespaceName is null 
            NamespaceDescription namespaceDescription = GetNamespaceDescription(namespaceName);
            
            namespaceDescription.Types.Add(new TypeDescription(type));
        }

        private NamespaceDescription GetNamespaceDescription(string name)
        {
            NamespaceDescription namespaceDescription;
            bool exists = _namespaceDescriptions.TryGetValue(name, out namespaceDescription);
            
            return exists ? namespaceDescription : AddNamespace(name);
        }

        private NamespaceDescription AddNamespace(string name)
        {
            var namespaceDescription = new NamespaceDescription(name);
            
            _namespaceDescriptions.Add(name, namespaceDescription);

            return namespaceDescription;
        }
    }
}