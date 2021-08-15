// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.DescriptorPool
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Google.Protobuf.Reflection
{
  internal sealed class DescriptorPool
  {
    private readonly IDictionary<string, IDescriptor> descriptorsByName = (IDictionary<string, IDescriptor>) new Dictionary<string, IDescriptor>();
    private readonly IDictionary<DescriptorPool.DescriptorIntPair, FieldDescriptor> fieldsByNumber = (IDictionary<DescriptorPool.DescriptorIntPair, FieldDescriptor>) new Dictionary<DescriptorPool.DescriptorIntPair, FieldDescriptor>();
    private readonly IDictionary<DescriptorPool.DescriptorIntPair, EnumValueDescriptor> enumValuesByNumber = (IDictionary<DescriptorPool.DescriptorIntPair, EnumValueDescriptor>) new Dictionary<DescriptorPool.DescriptorIntPair, EnumValueDescriptor>();
    private readonly HashSet<FileDescriptor> dependencies;
    private static readonly Regex ValidationRegex = new Regex("^[_A-Za-z][_A-Za-z0-9]*$", FrameworkPortability.CompiledRegexWhereAvailable);

    internal DescriptorPool(FileDescriptor[] dependencyFiles)
    {
      this.dependencies = new HashSet<FileDescriptor>();
      for (int index = 0; index < dependencyFiles.Length; ++index)
      {
        this.dependencies.Add(dependencyFiles[index]);
        this.ImportPublicDependencies(dependencyFiles[index]);
      }
      foreach (FileDescriptor dependencyFile in dependencyFiles)
        this.AddPackage(dependencyFile.Package, dependencyFile);
    }

    private void ImportPublicDependencies(FileDescriptor file)
    {
      foreach (FileDescriptor publicDependency in (IEnumerable<FileDescriptor>) file.PublicDependencies)
      {
        if (this.dependencies.Add(publicDependency))
          this.ImportPublicDependencies(publicDependency);
      }
    }

    internal T FindSymbol<T>(string fullName) where T : class
    {
      IDescriptor descriptor;
      this.descriptorsByName.TryGetValue(fullName, out descriptor);
      if (descriptor is T obj1)
        return obj1;
      foreach (FileDescriptor dependency in this.dependencies)
      {
        dependency.DescriptorPool.descriptorsByName.TryGetValue(fullName, out descriptor);
        if (descriptor is T obj4)
          return obj4;
      }
      return default (T);
    }

    internal void AddPackage(string fullName, FileDescriptor file)
    {
      int length = fullName.LastIndexOf('.');
      string name;
      if (length != -1)
      {
        this.AddPackage(fullName.Substring(0, length), file);
        name = fullName.Substring(length + 1);
      }
      else
        name = fullName;
      IDescriptor descriptor;
      if (this.descriptorsByName.TryGetValue(fullName, out descriptor) && !(descriptor is PackageDescriptor))
        throw new DescriptorValidationException((IDescriptor) file, "\"" + name + "\" is already defined (as something other than a package) in file \"" + descriptor.File.Name + "\".");
      this.descriptorsByName[fullName] = (IDescriptor) new PackageDescriptor(name, fullName, file);
    }

    internal void AddSymbol(IDescriptor descriptor)
    {
      DescriptorPool.ValidateSymbolName(descriptor);
      string fullName = descriptor.FullName;
      IDescriptor descriptor1;
      if (this.descriptorsByName.TryGetValue(fullName, out descriptor1))
      {
        int length = fullName.LastIndexOf('.');
        string description;
        if (descriptor.File == descriptor1.File)
        {
          if (length == -1)
            description = "\"" + fullName + "\" is already defined.";
          else
            description = "\"" + fullName.Substring(length + 1) + "\" is already defined in \"" + fullName.Substring(0, length) + "\".";
        }
        else
          description = "\"" + fullName + "\" is already defined in file \"" + descriptor1.File.Name + "\".";
        throw new DescriptorValidationException(descriptor, description);
      }
      this.descriptorsByName[fullName] = descriptor;
    }

    private static void ValidateSymbolName(IDescriptor descriptor)
    {
      if (descriptor.Name == "")
        throw new DescriptorValidationException(descriptor, "Missing name.");
      if (!DescriptorPool.ValidationRegex.IsMatch(descriptor.Name))
        throw new DescriptorValidationException(descriptor, "\"" + descriptor.Name + "\" is not a valid identifier.");
    }

    internal FieldDescriptor FindFieldByNumber(
      MessageDescriptor messageDescriptor,
      int number)
    {
      FieldDescriptor fieldDescriptor;
      this.fieldsByNumber.TryGetValue(new DescriptorPool.DescriptorIntPair((IDescriptor) messageDescriptor, number), out fieldDescriptor);
      return fieldDescriptor;
    }

    internal EnumValueDescriptor FindEnumValueByNumber(
      EnumDescriptor enumDescriptor,
      int number)
    {
      EnumValueDescriptor enumValueDescriptor;
      this.enumValuesByNumber.TryGetValue(new DescriptorPool.DescriptorIntPair((IDescriptor) enumDescriptor, number), out enumValueDescriptor);
      return enumValueDescriptor;
    }

    internal void AddFieldByNumber(FieldDescriptor field)
    {
      DescriptorPool.DescriptorIntPair key = new DescriptorPool.DescriptorIntPair((IDescriptor) field.ContainingType, field.FieldNumber);
      FieldDescriptor fieldDescriptor;
      if (this.fieldsByNumber.TryGetValue(key, out fieldDescriptor))
        throw new DescriptorValidationException((IDescriptor) field, "Field number " + (object) field.FieldNumber + "has already been used in \"" + field.ContainingType.FullName + "\" by field \"" + fieldDescriptor.Name + "\".");
      this.fieldsByNumber[key] = field;
    }

    internal void AddEnumValueByNumber(EnumValueDescriptor enumValue)
    {
      DescriptorPool.DescriptorIntPair key = new DescriptorPool.DescriptorIntPair((IDescriptor) enumValue.EnumDescriptor, enumValue.Number);
      if (this.enumValuesByNumber.ContainsKey(key))
        return;
      this.enumValuesByNumber[key] = enumValue;
    }

    internal IDescriptor LookupSymbol(string name, IDescriptor relativeTo)
    {
      IDescriptor symbol;
      if (name.StartsWith("."))
      {
        symbol = this.FindSymbol<IDescriptor>(name.Substring(1));
      }
      else
      {
        int length = name.IndexOf('.');
        string str = length == -1 ? name : name.Substring(0, length);
        StringBuilder stringBuilder = new StringBuilder(relativeTo.FullName);
        int num;
        while (true)
        {
          num = stringBuilder.ToString().LastIndexOf(".");
          if (num != -1)
          {
            stringBuilder.Length = num + 1;
            stringBuilder.Append(str);
            symbol = this.FindSymbol<IDescriptor>(stringBuilder.ToString());
            if (symbol == null)
              stringBuilder.Length = num;
            else
              goto label_6;
          }
          else
            break;
        }
        symbol = this.FindSymbol<IDescriptor>(name);
        goto label_9;
label_6:
        if (length != -1)
        {
          stringBuilder.Length = num + 1;
          stringBuilder.Append(name);
          symbol = this.FindSymbol<IDescriptor>(stringBuilder.ToString());
        }
      }
label_9:
      return symbol != null ? symbol : throw new DescriptorValidationException(relativeTo, "\"" + name + "\" is not defined.");
    }

    private struct DescriptorIntPair : IEquatable<DescriptorPool.DescriptorIntPair>
    {
      private readonly int number;
      private readonly IDescriptor descriptor;

      internal DescriptorIntPair(IDescriptor descriptor, int number)
      {
        this.number = number;
        this.descriptor = descriptor;
      }

      public bool Equals(DescriptorPool.DescriptorIntPair other) => this.descriptor == other.descriptor && this.number == other.number;

      public override bool Equals(object obj) => obj is DescriptorPool.DescriptorIntPair other && this.Equals(other);

      public override int GetHashCode() => this.descriptor.GetHashCode() * (int) ushort.MaxValue + this.number;
    }
  }
}
