// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.Reflection.DescriptorProtoFile
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Diagnostics;

namespace Google.Protobuf.Reflection
{
  [DebuggerNonUserCode]
  internal static class DescriptorProtoFile
  {
    private static FileDescriptor descriptor = FileDescriptor.InternalBuildGeneratedFileFrom(Convert.FromBase64String("CiBnb29nbGUvcHJvdG9idWYvZGVzY3JpcHRvci5wcm90bxIPZ29vZ2xlLnBy" + "b3RvYnVmIkcKEUZpbGVEZXNjcmlwdG9yU2V0EjIKBGZpbGUYASADKAsyJC5n" + "b29nbGUucHJvdG9idWYuRmlsZURlc2NyaXB0b3JQcm90byLbAwoTRmlsZURl" + "c2NyaXB0b3JQcm90bxIMCgRuYW1lGAEgASgJEg8KB3BhY2thZ2UYAiABKAkS" + "EgoKZGVwZW5kZW5jeRgDIAMoCRIZChFwdWJsaWNfZGVwZW5kZW5jeRgKIAMo" + "BRIXCg93ZWFrX2RlcGVuZGVuY3kYCyADKAUSNgoMbWVzc2FnZV90eXBlGAQg" + "AygLMiAuZ29vZ2xlLnByb3RvYnVmLkRlc2NyaXB0b3JQcm90bxI3CgllbnVt" + "X3R5cGUYBSADKAsyJC5nb29nbGUucHJvdG9idWYuRW51bURlc2NyaXB0b3JQ" + "cm90bxI4CgdzZXJ2aWNlGAYgAygLMicuZ29vZ2xlLnByb3RvYnVmLlNlcnZp" + "Y2VEZXNjcmlwdG9yUHJvdG8SOAoJZXh0ZW5zaW9uGAcgAygLMiUuZ29vZ2xl" + "LnByb3RvYnVmLkZpZWxkRGVzY3JpcHRvclByb3RvEi0KB29wdGlvbnMYCCAB" + "KAsyHC5nb29nbGUucHJvdG9idWYuRmlsZU9wdGlvbnMSOQoQc291cmNlX2Nv" + "ZGVfaW5mbxgJIAEoCzIfLmdvb2dsZS5wcm90b2J1Zi5Tb3VyY2VDb2RlSW5m" + "bxIOCgZzeW50YXgYDCABKAki8AQKD0Rlc2NyaXB0b3JQcm90bxIMCgRuYW1l" + "GAEgASgJEjQKBWZpZWxkGAIgAygLMiUuZ29vZ2xlLnByb3RvYnVmLkZpZWxk" + "RGVzY3JpcHRvclByb3RvEjgKCWV4dGVuc2lvbhgGIAMoCzIlLmdvb2dsZS5w" + "cm90b2J1Zi5GaWVsZERlc2NyaXB0b3JQcm90bxI1CgtuZXN0ZWRfdHlwZRgD" + "IAMoCzIgLmdvb2dsZS5wcm90b2J1Zi5EZXNjcmlwdG9yUHJvdG8SNwoJZW51" + "bV90eXBlGAQgAygLMiQuZ29vZ2xlLnByb3RvYnVmLkVudW1EZXNjcmlwdG9y" + "UHJvdG8SSAoPZXh0ZW5zaW9uX3JhbmdlGAUgAygLMi8uZ29vZ2xlLnByb3Rv" + "YnVmLkRlc2NyaXB0b3JQcm90by5FeHRlbnNpb25SYW5nZRI5CgpvbmVvZl9k" + "ZWNsGAggAygLMiUuZ29vZ2xlLnByb3RvYnVmLk9uZW9mRGVzY3JpcHRvclBy" + "b3RvEjAKB29wdGlvbnMYByABKAsyHy5nb29nbGUucHJvdG9idWYuTWVzc2Fn" + "ZU9wdGlvbnMSRgoOcmVzZXJ2ZWRfcmFuZ2UYCSADKAsyLi5nb29nbGUucHJv" + "dG9idWYuRGVzY3JpcHRvclByb3RvLlJlc2VydmVkUmFuZ2USFQoNcmVzZXJ2" + "ZWRfbmFtZRgKIAMoCRosCg5FeHRlbnNpb25SYW5nZRINCgVzdGFydBgBIAEo" + "BRILCgNlbmQYAiABKAUaKwoNUmVzZXJ2ZWRSYW5nZRINCgVzdGFydBgBIAEo" + "BRILCgNlbmQYAiABKAUiqQUKFEZpZWxkRGVzY3JpcHRvclByb3RvEgwKBG5h" + "bWUYASABKAkSDgoGbnVtYmVyGAMgASgFEjoKBWxhYmVsGAQgASgOMisuZ29v" + "Z2xlLnByb3RvYnVmLkZpZWxkRGVzY3JpcHRvclByb3RvLkxhYmVsEjgKBHR5" + "cGUYBSABKA4yKi5nb29nbGUucHJvdG9idWYuRmllbGREZXNjcmlwdG9yUHJv" + "dG8uVHlwZRIRCgl0eXBlX25hbWUYBiABKAkSEAoIZXh0ZW5kZWUYAiABKAkS" + "FQoNZGVmYXVsdF92YWx1ZRgHIAEoCRITCgtvbmVvZl9pbmRleBgJIAEoBRIu" + "CgdvcHRpb25zGAggASgLMh0uZ29vZ2xlLnByb3RvYnVmLkZpZWxkT3B0aW9u" + "cyK2AgoEVHlwZRIPCgtUWVBFX0RPVUJMRRABEg4KClRZUEVfRkxPQVQQAhIO" + "CgpUWVBFX0lOVDY0EAMSDwoLVFlQRV9VSU5UNjQQBBIOCgpUWVBFX0lOVDMy" + "EAUSEAoMVFlQRV9GSVhFRDY0EAYSEAoMVFlQRV9GSVhFRDMyEAcSDQoJVFlQ" + "RV9CT09MEAgSDwoLVFlQRV9TVFJJTkcQCRIOCgpUWVBFX0dST1VQEAoSEAoM" + "VFlQRV9NRVNTQUdFEAsSDgoKVFlQRV9CWVRFUxAMEg8KC1RZUEVfVUlOVDMy" + "EA0SDQoJVFlQRV9FTlVNEA4SEQoNVFlQRV9TRklYRUQzMhAPEhEKDVRZUEVf" + "U0ZJWEVENjQQEBIPCgtUWVBFX1NJTlQzMhAREg8KC1RZUEVfU0lOVDY0EBIi" + "QwoFTGFiZWwSEgoOTEFCRUxfT1BUSU9OQUwQARISCg5MQUJFTF9SRVFVSVJF" + "RBACEhIKDkxBQkVMX1JFUEVBVEVEEAMiJAoUT25lb2ZEZXNjcmlwdG9yUHJv" + "dG8SDAoEbmFtZRgBIAEoCSKMAQoTRW51bURlc2NyaXB0b3JQcm90bxIMCgRu" + "YW1lGAEgASgJEjgKBXZhbHVlGAIgAygLMikuZ29vZ2xlLnByb3RvYnVmLkVu" + "dW1WYWx1ZURlc2NyaXB0b3JQcm90bxItCgdvcHRpb25zGAMgASgLMhwuZ29v" + "Z2xlLnByb3RvYnVmLkVudW1PcHRpb25zImwKGEVudW1WYWx1ZURlc2NyaXB0" + "b3JQcm90bxIMCgRuYW1lGAEgASgJEg4KBm51bWJlchgCIAEoBRIyCgdvcHRp" + "b25zGAMgASgLMiEuZ29vZ2xlLnByb3RvYnVmLkVudW1WYWx1ZU9wdGlvbnMi" + "kAEKFlNlcnZpY2VEZXNjcmlwdG9yUHJvdG8SDAoEbmFtZRgBIAEoCRI2CgZt" + "ZXRob2QYAiADKAsyJi5nb29nbGUucHJvdG9idWYuTWV0aG9kRGVzY3JpcHRv" + "clByb3RvEjAKB29wdGlvbnMYAyABKAsyHy5nb29nbGUucHJvdG9idWYuU2Vy" + "dmljZU9wdGlvbnMiwQEKFU1ldGhvZERlc2NyaXB0b3JQcm90bxIMCgRuYW1l" + "GAEgASgJEhIKCmlucHV0X3R5cGUYAiABKAkSEwoLb3V0cHV0X3R5cGUYAyAB" + "KAkSLwoHb3B0aW9ucxgEIAEoCzIeLmdvb2dsZS5wcm90b2J1Zi5NZXRob2RP" + "cHRpb25zEh8KEGNsaWVudF9zdHJlYW1pbmcYBSABKAg6BWZhbHNlEh8KEHNl" + "cnZlcl9zdHJlYW1pbmcYBiABKAg6BWZhbHNlIqoFCgtGaWxlT3B0aW9ucxIU" + "CgxqYXZhX3BhY2thZ2UYASABKAkSHAoUamF2YV9vdXRlcl9jbGFzc25hbWUY" + "CCABKAkSIgoTamF2YV9tdWx0aXBsZV9maWxlcxgKIAEoCDoFZmFsc2USLAod" + "amF2YV9nZW5lcmF0ZV9lcXVhbHNfYW5kX2hhc2gYFCABKAg6BWZhbHNlEiUK" + "FmphdmFfc3RyaW5nX2NoZWNrX3V0ZjgYGyABKAg6BWZhbHNlEkYKDG9wdGlt" + "aXplX2ZvchgJIAEoDjIpLmdvb2dsZS5wcm90b2J1Zi5GaWxlT3B0aW9ucy5P" + "cHRpbWl6ZU1vZGU6BVNQRUVEEhIKCmdvX3BhY2thZ2UYCyABKAkSIgoTY2Nf" + "Z2VuZXJpY19zZXJ2aWNlcxgQIAEoCDoFZmFsc2USJAoVamF2YV9nZW5lcmlj" + "X3NlcnZpY2VzGBEgASgIOgVmYWxzZRIiChNweV9nZW5lcmljX3NlcnZpY2Vz" + "GBIgASgIOgVmYWxzZRIZCgpkZXByZWNhdGVkGBcgASgIOgVmYWxzZRIfChBj" + "Y19lbmFibGVfYXJlbmFzGB8gASgIOgVmYWxzZRIZChFvYmpjX2NsYXNzX3By" + "ZWZpeBgkIAEoCRIYChBjc2hhcnBfbmFtZXNwYWNlGCUgASgJEicKH2phdmFu" + "YW5vX3VzZV9kZXByZWNhdGVkX3BhY2thZ2UYJiABKAgSQwoUdW5pbnRlcnBy" + "ZXRlZF9vcHRpb24Y5wcgAygLMiQuZ29vZ2xlLnByb3RvYnVmLlVuaW50ZXJw" + "cmV0ZWRPcHRpb24iOgoMT3B0aW1pemVNb2RlEgkKBVNQRUVEEAESDQoJQ09E" + "RV9TSVpFEAISEAoMTElURV9SVU5USU1FEAMqCQjoBxCAgICAAiLmAQoOTWVz" + "c2FnZU9wdGlvbnMSJgoXbWVzc2FnZV9zZXRfd2lyZV9mb3JtYXQYASABKAg6" + "BWZhbHNlEi4KH25vX3N0YW5kYXJkX2Rlc2NyaXB0b3JfYWNjZXNzb3IYAiAB" + "KAg6BWZhbHNlEhkKCmRlcHJlY2F0ZWQYAyABKAg6BWZhbHNlEhEKCW1hcF9l" + "bnRyeRgHIAEoCBJDChR1bmludGVycHJldGVkX29wdGlvbhjnByADKAsyJC5n" + "b29nbGUucHJvdG9idWYuVW5pbnRlcnByZXRlZE9wdGlvbioJCOgHEICAgIAC" + "IpgDCgxGaWVsZE9wdGlvbnMSOgoFY3R5cGUYASABKA4yIy5nb29nbGUucHJv" + "dG9idWYuRmllbGRPcHRpb25zLkNUeXBlOgZTVFJJTkcSDgoGcGFja2VkGAIg" + "ASgIEj8KBmpzdHlwZRgGIAEoDjIkLmdvb2dsZS5wcm90b2J1Zi5GaWVsZE9w" + "dGlvbnMuSlNUeXBlOglKU19OT1JNQUwSEwoEbGF6eRgFIAEoCDoFZmFsc2US" + "GQoKZGVwcmVjYXRlZBgDIAEoCDoFZmFsc2USEwoEd2VhaxgKIAEoCDoFZmFs" + "c2USQwoUdW5pbnRlcnByZXRlZF9vcHRpb24Y5wcgAygLMiQuZ29vZ2xlLnBy" + "b3RvYnVmLlVuaW50ZXJwcmV0ZWRPcHRpb24iLwoFQ1R5cGUSCgoGU1RSSU5H" + "EAASCAoEQ09SRBABEhAKDFNUUklOR19QSUVDRRACIjUKBkpTVHlwZRINCglK" + "U19OT1JNQUwQABINCglKU19TVFJJTkcQARINCglKU19OVU1CRVIQAioJCOgH" + "EICAgIACIo0BCgtFbnVtT3B0aW9ucxITCgthbGxvd19hbGlhcxgCIAEoCBIZ" + "CgpkZXByZWNhdGVkGAMgASgIOgVmYWxzZRJDChR1bmludGVycHJldGVkX29w" + "dGlvbhjnByADKAsyJC5nb29nbGUucHJvdG9idWYuVW5pbnRlcnByZXRlZE9w" + "dGlvbioJCOgHEICAgIACIn0KEEVudW1WYWx1ZU9wdGlvbnMSGQoKZGVwcmVj" + "YXRlZBgBIAEoCDoFZmFsc2USQwoUdW5pbnRlcnByZXRlZF9vcHRpb24Y5wcg" + "AygLMiQuZ29vZ2xlLnByb3RvYnVmLlVuaW50ZXJwcmV0ZWRPcHRpb24qCQjo" + "BxCAgICAAiJ7Cg5TZXJ2aWNlT3B0aW9ucxIZCgpkZXByZWNhdGVkGCEgASgI" + "OgVmYWxzZRJDChR1bmludGVycHJldGVkX29wdGlvbhjnByADKAsyJC5nb29n" + "bGUucHJvdG9idWYuVW5pbnRlcnByZXRlZE9wdGlvbioJCOgHEICAgIACInoK" + "DU1ldGhvZE9wdGlvbnMSGQoKZGVwcmVjYXRlZBghIAEoCDoFZmFsc2USQwoU" + "dW5pbnRlcnByZXRlZF9vcHRpb24Y5wcgAygLMiQuZ29vZ2xlLnByb3RvYnVm" + "LlVuaW50ZXJwcmV0ZWRPcHRpb24qCQjoBxCAgICAAiKeAgoTVW5pbnRlcnBy" + "ZXRlZE9wdGlvbhI7CgRuYW1lGAIgAygLMi0uZ29vZ2xlLnByb3RvYnVmLlVu" + "aW50ZXJwcmV0ZWRPcHRpb24uTmFtZVBhcnQSGAoQaWRlbnRpZmllcl92YWx1" + "ZRgDIAEoCRIaChJwb3NpdGl2ZV9pbnRfdmFsdWUYBCABKAQSGgoSbmVnYXRp" + "dmVfaW50X3ZhbHVlGAUgASgDEhQKDGRvdWJsZV92YWx1ZRgGIAEoARIUCgxz" + "dHJpbmdfdmFsdWUYByABKAwSFwoPYWdncmVnYXRlX3ZhbHVlGAggASgJGjMK" + "CE5hbWVQYXJ0EhEKCW5hbWVfcGFydBgBIAIoCRIUCgxpc19leHRlbnNpb24Y" + "AiACKAgi1QEKDlNvdXJjZUNvZGVJbmZvEjoKCGxvY2F0aW9uGAEgAygLMigu" + "Z29vZ2xlLnByb3RvYnVmLlNvdXJjZUNvZGVJbmZvLkxvY2F0aW9uGoYBCghM" + "b2NhdGlvbhIQCgRwYXRoGAEgAygFQgIQARIQCgRzcGFuGAIgAygFQgIQARIY" + "ChBsZWFkaW5nX2NvbW1lbnRzGAMgASgJEhkKEXRyYWlsaW5nX2NvbW1lbnRz" + "GAQgASgJEiEKGWxlYWRpbmdfZGV0YWNoZWRfY29tbWVudHMYBiADKAlCWwoT" + "Y29tLmdvb2dsZS5wcm90b2J1ZkIQRGVzY3JpcHRvclByb3Rvc0gBWgpkZXNj" + "cmlwdG9yogIDR1BCqgIaR29vZ2xlLlByb3RvYnVmLlJlZmxlY3Rpb26wAgE="), new FileDescriptor[0], new GeneratedCodeInfo((System.Type[]) null, new GeneratedCodeInfo[18]
    {
      new GeneratedCodeInfo(typeof (FileDescriptorSet), new string[1]
      {
        "File"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (FileDescriptorProto), new string[12]
      {
        "Name",
        "Package",
        "Dependency",
        "PublicDependency",
        "WeakDependency",
        "MessageType",
        "EnumType",
        "Service",
        "Extension",
        "Options",
        "SourceCodeInfo",
        "Syntax"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (DescriptorProto), new string[10]
      {
        "Name",
        "Field",
        "Extension",
        "NestedType",
        "EnumType",
        "ExtensionRange",
        "OneofDecl",
        "Options",
        "ReservedRange",
        "ReservedName"
      }, (string[]) null, (System.Type[]) null, new GeneratedCodeInfo[2]
      {
        new GeneratedCodeInfo(typeof (DescriptorProto.Types.ExtensionRange), new string[2]
        {
          "Start",
          "End"
        }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
        new GeneratedCodeInfo(typeof (DescriptorProto.Types.ReservedRange), new string[2]
        {
          "Start",
          "End"
        }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null)
      }),
      new GeneratedCodeInfo(typeof (FieldDescriptorProto), new string[9]
      {
        "Name",
        "Number",
        "Label",
        "Type",
        "TypeName",
        "Extendee",
        "DefaultValue",
        "OneofIndex",
        "Options"
      }, (string[]) null, new System.Type[2]
      {
        typeof (FieldDescriptorProto.Types.Type),
        typeof (FieldDescriptorProto.Types.Label)
      }, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (OneofDescriptorProto), new string[1]
      {
        "Name"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (EnumDescriptorProto), new string[3]
      {
        "Name",
        "Value",
        "Options"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (EnumValueDescriptorProto), new string[3]
      {
        "Name",
        "Number",
        "Options"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (ServiceDescriptorProto), new string[3]
      {
        "Name",
        "Method",
        "Options"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (MethodDescriptorProto), new string[6]
      {
        "Name",
        "InputType",
        "OutputType",
        "Options",
        "ClientStreaming",
        "ServerStreaming"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (FileOptions), new string[16]
      {
        "JavaPackage",
        "JavaOuterClassname",
        "JavaMultipleFiles",
        "JavaGenerateEqualsAndHash",
        "JavaStringCheckUtf8",
        "OptimizeFor",
        "GoPackage",
        "CcGenericServices",
        "JavaGenericServices",
        "PyGenericServices",
        "Deprecated",
        "CcEnableArenas",
        "ObjcClassPrefix",
        "CsharpNamespace",
        "JavananoUseDeprecatedPackage",
        "UninterpretedOption"
      }, (string[]) null, new System.Type[1]
      {
        typeof (FileOptions.Types.OptimizeMode)
      }, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (MessageOptions), new string[5]
      {
        "MessageSetWireFormat",
        "NoStandardDescriptorAccessor",
        "Deprecated",
        "MapEntry",
        "UninterpretedOption"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (FieldOptions), new string[7]
      {
        "Ctype",
        "Packed",
        "Jstype",
        "Lazy",
        "Deprecated",
        "Weak",
        "UninterpretedOption"
      }, (string[]) null, new System.Type[2]
      {
        typeof (FieldOptions.Types.CType),
        typeof (FieldOptions.Types.JSType)
      }, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (EnumOptions), new string[3]
      {
        "AllowAlias",
        "Deprecated",
        "UninterpretedOption"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (EnumValueOptions), new string[2]
      {
        "Deprecated",
        "UninterpretedOption"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (ServiceOptions), new string[2]
      {
        "Deprecated",
        "UninterpretedOption"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (MethodOptions), new string[2]
      {
        "Deprecated",
        "UninterpretedOption"
      }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null),
      new GeneratedCodeInfo(typeof (UninterpretedOption), new string[7]
      {
        "Name",
        "IdentifierValue",
        "PositiveIntValue",
        "NegativeIntValue",
        "DoubleValue",
        "StringValue",
        "AggregateValue"
      }, (string[]) null, (System.Type[]) null, new GeneratedCodeInfo[1]
      {
        new GeneratedCodeInfo(typeof (UninterpretedOption.Types.NamePart), new string[2]
        {
          "NamePart_",
          "IsExtension"
        }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null)
      }),
      new GeneratedCodeInfo(typeof (SourceCodeInfo), new string[1]
      {
        "Location"
      }, (string[]) null, (System.Type[]) null, new GeneratedCodeInfo[1]
      {
        new GeneratedCodeInfo(typeof (SourceCodeInfo.Types.Location), new string[5]
        {
          "Path",
          "Span",
          "LeadingComments",
          "TrailingComments",
          "LeadingDetachedComments"
        }, (string[]) null, (System.Type[]) null, (GeneratedCodeInfo[]) null)
      })
    }));

    public static FileDescriptor Descriptor => DescriptorProtoFile.descriptor;
  }
}
