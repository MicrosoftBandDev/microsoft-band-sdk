// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Struct
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Struct : IMessage<Struct>, IMessage, IEquatable<Struct>, IDeepCloneable<Struct>
  {
    public const int FieldsFieldNumber = 1;
    private static readonly MessageParser<Struct> _parser = new MessageParser<Struct>((Func<Struct>) (() => new Struct()));
    private static readonly MapField<string, Value>.Codec _map_fields_codec = new MapField<string, Value>.Codec(FieldCodec.ForString(10U), FieldCodec.ForMessage<Value>(18U, Value.Parser), 10U);
    private readonly MapField<string, Value> fields_ = new MapField<string, Value>();

    public static MessageParser<Struct> Parser => Struct._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Struct.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Struct.Descriptor;

    public Struct()
    {
    }

    public Struct(Struct other)
      : this()
    {
      this.fields_ = other.fields_.Clone();
    }

    public Struct Clone() => new Struct(this);

    public MapField<string, Value> Fields => this.fields_;

    public override bool Equals(object other) => this.Equals(other as Struct);

    public bool Equals(Struct other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Fields.Equals(other.Fields));

    public override int GetHashCode() => 1 ^ this.Fields.GetHashCode();

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output) => this.fields_.WriteTo(output, Struct._map_fields_codec);

    public int CalculateSize() => 0 + this.fields_.CalculateSize(Struct._map_fields_codec);

    public void MergeFrom(Struct other)
    {
      if (other == null)
        return;
      this.fields_.Add((IDictionary<string, Value>) other.fields_);
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        if (num != 10U)
          input.SkipLastField();
        else
          this.fields_.AddEntriesFrom(input, Struct._map_fields_codec);
      }
    }
  }
}
