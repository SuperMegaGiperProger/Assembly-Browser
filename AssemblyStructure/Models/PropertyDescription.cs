using System;
using System.Reflection;

namespace AssemblyStructure.Models
{
    public class PropertyDescription : IMemberDescription
    {
        private PropertyInfo _propertyInfo;

        public string Name => _propertyInfo.Name;
        public Type Type => _propertyInfo.PropertyType;

        public PropertyDescription(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }
    }
}