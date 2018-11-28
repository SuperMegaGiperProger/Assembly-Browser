using System;

namespace AssemblyStructure.Models
{
    public interface IMemberDescription
    {
        string Name { get; }
        Type Type { get; }
    }
}