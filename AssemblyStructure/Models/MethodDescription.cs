using System;
using System.Collections;
using System.Reflection;

namespace AssemblyStructure.Models
{
    public class MethodDescription
    {
        public enum VisibilityType
        {
            Private,
            Public,
            Internal,
            Protected,
            ProtectedInternal
        }

        public string Name => _methodInfo.Name;
        public Type ReturnType => _methodInfo.ReturnType;
        public VisibilityType Visibility => getVisibility();
        public bool IsStatic => _methodInfo.IsStatic;
        public bool IsAbstract => _methodInfo.IsAbstract;
        public bool IsFinal => _methodInfo.IsFinal;
        
        private readonly MethodInfo _methodInfo;

        public MethodDescription(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;
        }

        private VisibilityType getVisibility()
        {
            var signatureConfig = new bool[]
                {_methodInfo.IsPublic, _methodInfo.IsAssembly, _methodInfo.IsFamily, _methodInfo.IsFamilyOrAssembly};

            for (int i = 0; i < 4; ++i)
            {
                if (signatureConfig[i]) return (VisibilityType) (i + 1);
            }

            return VisibilityType.Private;
        }
    }
}