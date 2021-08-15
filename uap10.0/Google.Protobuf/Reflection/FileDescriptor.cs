// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.FileDescriptor
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Google.Protobuf.Reflection
{
  public sealed class FileDescriptor : IDescriptor
  {
    private readonly ByteString descriptorData;
    private readonly FileDescriptorProto proto;
    private readonly IList<MessageDescriptor> messageTypes;
    private readonly IList<EnumDescriptor> enumTypes;
    private readonly IList<ServiceDescriptor> services;
    private readonly IList<FileDescriptor> dependencies;
    private readonly IList<FileDescriptor> publicDependencies;
    private readonly DescriptorPool pool;

    private FileDescriptor(
      ByteString descriptorData,
      FileDescriptorProto proto,
      FileDescriptor[] dependencies,
      DescriptorPool pool,
      bool allowUnknownDependencies,
      GeneratedCodeInfo generatedCodeInfo)
    {
      FileDescriptor file = this;
      this.descriptorData = descriptorData;
      this.pool = pool;
      this.proto = proto;
      this.dependencies = (IList<FileDescriptor>) new ReadOnlyCollection<FileDescriptor>((IList<FileDescriptor>) (FileDescriptor[]) dependencies.Clone());
      this.publicDependencies = FileDescriptor.DeterminePublicDependencies(this, proto, dependencies, allowUnknownDependencies);
      pool.AddPackage(this.Package, this);
      this.messageTypes = DescriptorUtil.ConvertAndMakeReadOnly<DescriptorProto, MessageDescriptor>((IList<DescriptorProto>) proto.MessageType, (DescriptorUtil.IndexedConverter<DescriptorProto, MessageDescriptor>) ((message, index) => new MessageDescriptor(message, file, (MessageDescriptor) null, index, generatedCodeInfo == null ? (GeneratedCodeInfo) null : generatedCodeInfo.NestedTypes[index])));
      this.enumTypes = DescriptorUtil.ConvertAndMakeReadOnly<EnumDescriptorProto, EnumDescriptor>((IList<EnumDescriptorProto>) proto.EnumType, (DescriptorUtil.IndexedConverter<EnumDescriptorProto, EnumDescriptor>) ((enumType, index) => new EnumDescriptor(enumType, file, (MessageDescriptor) null, index, generatedCodeInfo == null ? (System.Type) null : generatedCodeInfo.NestedEnums[index])));
      this.services = DescriptorUtil.ConvertAndMakeReadOnly<ServiceDescriptorProto, ServiceDescriptor>((IList<ServiceDescriptorProto>) proto.Service, (DescriptorUtil.IndexedConverter<ServiceDescriptorProto, ServiceDescriptor>) ((service, index) => new ServiceDescriptor(service, this, index)));
    }

    internal string ComputeFullName(MessageDescriptor parent, string name)
    {
      if (parent != null)
        return parent.FullName + "." + name;
      return this.Package.Length > 0 ? this.Package + "." + name : name;
    }

    private static IList<FileDescriptor> DeterminePublicDependencies(
      FileDescriptor @this,
      FileDescriptorProto proto,
      FileDescriptor[] dependencies,
      bool allowUnknownDependencies)
    {
      Dictionary<string, FileDescriptor> dictionary = new Dictionary<string, FileDescriptor>();
      foreach (FileDescriptor dependency in dependencies)
        dictionary[dependency.Name] = dependency;
      List<FileDescriptor> fileDescriptorList = new List<FileDescriptor>();
      for (int index1 = 0; index1 < proto.PublicDependency.Count; ++index1)
      {
        int index2 = proto.PublicDependency[index1];
        if (index2 < 0 || index2 >= proto.Dependency.Count)
          throw new DescriptorValidationException((IDescriptor) @this, "Invalid public dependency index.");
        string key = proto.Dependency[index2];
        FileDescriptor fileDescriptor = dictionary[key];
        if (fileDescriptor == null)
        {
          if (!allowUnknownDependencies)
            throw new DescriptorValidationException((IDescriptor) @this, "Invalid public dependency: " + key);
        }
        else
          fileDescriptorList.Add(fileDescriptor);
      }
      return (IList<FileDescriptor>) new ReadOnlyCollection<FileDescriptor>((IList<FileDescriptor>) fileDescriptorList);
    }

    internal FileDescriptorProto Proto => this.proto;

    public string Name => this.proto.Name;

    public string Package => this.proto.Package;

    public IList<MessageDescriptor> MessageTypes => this.messageTypes;

    public IList<EnumDescriptor> EnumTypes => this.enumTypes;

    public IList<ServiceDescriptor> Services => this.services;

    public IList<FileDescriptor> Dependencies => this.dependencies;

    public IList<FileDescriptor> PublicDependencies => this.publicDependencies;

    public ByteString SerializedData => this.descriptorData;

    string IDescriptor.FullName => this.Name;

    FileDescriptor IDescriptor.File => this;

    internal DescriptorPool DescriptorPool => this.pool;

    public T FindTypeByName<T>(string name) where T : class, IDescriptor
    {
      if (name.IndexOf('.') != -1)
        return default (T);
      if (this.Package.Length > 0)
        name = this.Package + "." + name;
      T symbol = this.pool.FindSymbol<T>(name);
      return (object) symbol != null && symbol.File == this ? symbol : default (T);
    }

    private static FileDescriptor BuildFrom(
      ByteString descriptorData,
      FileDescriptorProto proto,
      FileDescriptor[] dependencies,
      bool allowUnknownDependencies,
      GeneratedCodeInfo generatedCodeInfo)
    {
      if (dependencies == null)
        dependencies = new FileDescriptor[0];
      DescriptorPool pool = new DescriptorPool(dependencies);
      FileDescriptor fileDescriptor = new FileDescriptor(descriptorData, proto, dependencies, pool, allowUnknownDependencies, generatedCodeInfo);
      if (dependencies.Length != proto.Dependency.Count)
        throw new DescriptorValidationException((IDescriptor) fileDescriptor, "Dependencies passed to FileDescriptor.BuildFrom() don't match those listed in the FileDescriptorProto.");
      for (int index = 0; index < proto.Dependency.Count; ++index)
      {
        if (dependencies[index].Name != proto.Dependency[index])
          throw new DescriptorValidationException((IDescriptor) fileDescriptor, "Dependencies passed to FileDescriptor.BuildFrom() don't match those listed in the FileDescriptorProto. Expected: " + proto.Dependency[index] + " but was: " + dependencies[index].Name);
      }
      fileDescriptor.CrossLink();
      return fileDescriptor;
    }

    private void CrossLink()
    {
      foreach (MessageDescriptor messageType in (IEnumerable<MessageDescriptor>) this.messageTypes)
        messageType.CrossLink();
      foreach (ServiceDescriptor service in (IEnumerable<ServiceDescriptor>) this.services)
        service.CrossLink();
    }

    public static FileDescriptor InternalBuildGeneratedFileFrom(
      byte[] descriptorData,
      FileDescriptor[] dependencies,
      GeneratedCodeInfo generatedCodeInfo)
    {
      FileDescriptorProto from;
      try
      {
        from = FileDescriptorProto.Parser.ParseFrom(descriptorData);
      }
      catch (InvalidProtocolBufferException ex)
      {
        throw new ArgumentException("Failed to parse protocol buffer descriptor for generated code.", (Exception) ex);
      }
      try
      {
        return FileDescriptor.BuildFrom(ByteString.CopyFrom(descriptorData), from, dependencies, true, generatedCodeInfo);
      }
      catch (DescriptorValidationException ex)
      {
        throw new ArgumentException("Invalid embedded descriptor for \"" + from.Name + "\".", (Exception) ex);
      }
    }

    public override string ToString() => "FileDescriptor for " + this.proto.Name;

    public static FileDescriptor DescriptorProtoFileDescriptor => DescriptorProtoFile.Descriptor;
  }
}
