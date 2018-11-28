using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using AssemblyStructure.Models;
using NUnit.Framework;

namespace TestAssemblyStructure
{
    public class TestAssemblyStructure
    {
        [TestFixture]
        public class AssemblyStructure_NamespaceDescriptions
        {
            private NamespaceDescription[] _result;

            [SetUp]
            public void Init()
            {
                string solutionPath = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName;
                string fullPath = Path.Combine(solutionPath, "Fixtures", "ClassLibrary.dll");

                var assemblyStructure = new AssemblyStructure.AssemblyStructure(fullPath);

                _result = assemblyStructure.NamespaceDescriptions;
            }

            [Test]
            public void ContainsClassLibraryNamespace()
            {
                NamespaceDescription classLibrary = GetItemByName("ClassLibrary", _result);
                Assert.NotNull(classLibrary);

                TypeDescription class1 = GetItemByName("Class1", classLibrary.Types);
                Assert.NotNull(class1);

                AssertMemberType("Field1", typeof(string), class1.Fields);
                AssertMemberType("Field2", typeof(List<int>), class1.Fields);
                AssertMemberType("Prop1", typeof(int), class1.Properties);
                AssertMemberType("Prop2", typeof(string), class1.Properties);

                AssertMethod(
                    "PrivateMethod",
                    typeof(void),
                    MethodDescription.VisibilityType.Private,
                    false, false, false,
                    class1.Methods
                );
                AssertMethod(
                    "PrivateStaticMethod",
                    typeof(int),
                    MethodDescription.VisibilityType.Private,
                    true, false, false,
                    class1.Methods
                );
                AssertMethod(
                    "PublicMethod",
                    typeof(char),
                    MethodDescription.VisibilityType.Public,
                    false, false, false,
                    class1.Methods
                );
                AssertMethod(
                    "PublicStaticMethod",
                    typeof(bool),
                    MethodDescription.VisibilityType.Public,
                    true, false, false,
                    class1.Methods
                );
                AssertMethod(
                    "InternalMethod",
                    typeof(string),
                    MethodDescription.VisibilityType.Internal,
                    false, false, false,
                    class1.Methods
                );
                AssertMethod(
                    "InternalStaticMethod",
                    typeof(short),
                    MethodDescription.VisibilityType.Internal,
                    true, false, false,
                    class1.Methods
                );
                AssertMethod(
                    "ProtectedMethod",
                    typeof(double),
                    MethodDescription.VisibilityType.Protected,
                    false, false, false,
                    class1.Methods
                );
                AssertMethod(
                    "ProtectedStatic",
                    typeof(float),
                    MethodDescription.VisibilityType.Protected,
                    true, false, false,
                    class1.Methods
                );
                AssertMethod(
                    "ProtectedInternalMethod",
                    typeof(uint),
                    MethodDescription.VisibilityType.ProtectedInternal,
                    false, false, false,
                    class1.Methods
                );
                AssertMethod(
                    "ProtectedInternalStaticMethod",
                    typeof(long),
                    MethodDescription.VisibilityType.ProtectedInternal,
                    true, false, false,
                    class1.Methods
                );
            }

            private void AssertMemberType<T>(string memberName, Type type, ICollection<T> collection) where T : class
            {
                IMemberDescription member = GetItemByName(memberName, collection) as IMemberDescription;

                Assert.NotNull(member);
                Assert.True(member.Type == type);
            }

            private void AssertMethod(
                string methodName,
                Type returnType,
                MethodDescription.VisibilityType visibility,
                bool isStatic, bool isAbstract, bool isFinal,
                ICollection<MethodDescription> collection
            )
            {
                MethodDescription methodDescription = GetItemByName(methodName, collection);

                Assert.NotNull(methodDescription);
                Assert.True(methodDescription.ReturnType == returnType);
                Assert.True(methodDescription.Visibility == visibility);
                Assert.True(methodDescription.IsStatic == isStatic);
                Assert.True(methodDescription.IsAbstract == isAbstract);
                Assert.True(methodDescription.IsFinal == isFinal);
            }

            private T GetItemByName<T>(string name, ICollection<T> collection) where T : class
            {
                var withName = new Func<T, bool>(namespaceDescription =>
                    GetPropertyValue(namespaceDescription, "Name")?.ToString() == name);
                bool exists = collection.Any(withName);

                return exists ? collection.First(withName) : null;
            }

            private static object GetPropertyValue(object obj, string propertyName)
            {
                Type type = obj.GetType();

                return type.GetProperty(propertyName)?.GetValue(obj) ?? type.GetField(propertyName)?.GetValue(obj);
            }
        }
    }
}