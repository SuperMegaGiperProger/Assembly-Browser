using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyStructure.Models
{
    public class TypeDescription
    {
        public string Name => _type.Name;

        public FieldDescription[] Fields =>
            _type.GetFields().Select(fieldInfo => new FieldDescription(fieldInfo)).ToArray();

        public PropertyDescription[] Properties =>
            _type.GetProperties().Select(propertyInfo => new PropertyDescription(propertyInfo)).ToArray();

        public MethodDescription[] Methods =>
            _type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                .Select(methodInfo => new MethodDescription(methodInfo)).ToArray();

        private Type _type;

        public TypeDescription(Type type)
        {
            _type = type;
        }
    }
}