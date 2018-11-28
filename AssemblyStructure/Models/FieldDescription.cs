using System;
using System.Reflection;

namespace AssemblyStructure.Models
{
    public class FieldDescription : IMemberDescription
    {
        private FieldInfo _fieldInfo;

        public string Name => _fieldInfo.Name;
        public Type Type => _fieldInfo.FieldType;

        public FieldDescription(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;
        }
    }
}