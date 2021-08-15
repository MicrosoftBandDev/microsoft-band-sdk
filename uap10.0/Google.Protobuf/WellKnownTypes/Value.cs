// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Value
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Value : IMessage<Value>, IMessage, IEquatable<Value>, IDeepCloneable<Value>
  {
    public const int NullValueFieldNumber = 1;
    public const int NumberValueFieldNumber = 2;
    public const int StringValueFieldNumber = 3;
    public const int BoolValueFieldNumber = 4;
    public const int StructValueFieldNumber = 5;
    public const int ListValueFieldNumber = 6;
    private static readonly MessageParser<Value> _parser = new MessageParser<Value>((Func<Value>) (() => new Value()));
    private object kind_;
    private Value.KindOneofCase kindCase_;

    public static MessageParser<Value> Parser => Value._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Struct.Descriptor.MessageTypes[1];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Value.Descriptor;

    public Value()
    {
    }

    public Value(Value other)
      : this()
    {
      switch (other.KindCase)
      {
        case Value.KindOneofCase.NullValue:
          this.NullValue = other.NullValue;
          break;
        case Value.KindOneofCase.NumberValue:
          this.NumberValue = other.NumberValue;
          break;
        case Value.KindOneofCase.StringValue:
          this.StringValue = other.StringValue;
          break;
        case Value.KindOneofCase.BoolValue:
          this.BoolValue = other.BoolValue;
          break;
        case Value.KindOneofCase.StructValue:
          this.StructValue = other.StructValue.Clone();
          break;
        case Value.KindOneofCase.ListValue:
          this.ListValue = other.ListValue.Clone();
          break;
      }
    }

    public Value Clone() => new Value(this);

    public NullValue NullValue
    {
      get => this.kindCase_ != Value.KindOneofCase.NullValue ? NullValue.NULL_VALUE : (NullValue) this.kind_;
      set
      {
        this.kind_ = (object) value;
        this.kindCase_ = Value.KindOneofCase.NullValue;
      }
    }

    public double NumberValue
    {
      get => this.kindCase_ != Value.KindOneofCase.NumberValue ? 0.0 : (double) this.kind_;
      set
      {
        this.kind_ = (object) value;
        this.kindCase_ = Value.KindOneofCase.NumberValue;
      }
    }

    public string StringValue
    {
      get => this.kindCase_ != Value.KindOneofCase.StringValue ? "" : (string) this.kind_;
      set
      {
        this.kind_ = (object) Preconditions.CheckNotNull<string>(value, nameof (value));
        this.kindCase_ = Value.KindOneofCase.StringValue;
      }
    }

    public bool BoolValue
    {
      get => this.kindCase_ == Value.KindOneofCase.BoolValue && (bool) this.kind_;
      set
      {
        this.kind_ = (object) value;
        this.kindCase_ = Value.KindOneofCase.BoolValue;
      }
    }

    public Struct StructValue
    {
      get => this.kindCase_ != Value.KindOneofCase.StructValue ? (Struct) null : (Struct) this.kind_;
      set
      {
        this.kind_ = (object) value;
        this.kindCase_ = value == null ? Value.KindOneofCase.None : Value.KindOneofCase.StructValue;
      }
    }

    public ListValue ListValue
    {
      get => this.kindCase_ != Value.KindOneofCase.ListValue ? (ListValue) null : (ListValue) this.kind_;
      set
      {
        this.kind_ = (object) value;
        this.kindCase_ = value == null ? Value.KindOneofCase.None : Value.KindOneofCase.ListValue;
      }
    }

    public Value.KindOneofCase KindCase => this.kindCase_;

    public void ClearKind()
    {
      this.kindCase_ = Value.KindOneofCase.None;
      this.kind_ = (object) null;
    }

    public override bool Equals(object other) => this.Equals(other as Value);

    public bool Equals(Value other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.NullValue == other.NullValue && this.NumberValue == other.NumberValue && (!(this.StringValue != other.StringValue) && this.BoolValue == other.BoolValue) && (object.Equals((object) this.StructValue, (object) other.StructValue) && object.Equals((object) this.ListValue, (object) other.ListValue)));

    public override int GetHashCode()
    {
      int num = 1;
      if (this.kindCase_ == Value.KindOneofCase.NullValue)
        num ^= this.NullValue.GetHashCode();
      if (this.kindCase_ == Value.KindOneofCase.NumberValue)
        num ^= this.NumberValue.GetHashCode();
      if (this.kindCase_ == Value.KindOneofCase.StringValue)
        num ^= this.StringValue.GetHashCode();
      if (this.kindCase_ == Value.KindOneofCase.BoolValue)
        num ^= this.BoolValue.GetHashCode();
      if (this.kindCase_ == Value.KindOneofCase.StructValue)
        num ^= this.StructValue.GetHashCode();
      if (this.kindCase_ == Value.KindOneofCase.ListValue)
        num ^= this.ListValue.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.kindCase_ == Value.KindOneofCase.NullValue)
      {
        output.WriteRawTag((byte) 8);
        output.WriteEnum((int) this.NullValue);
      }
      if (this.kindCase_ == Value.KindOneofCase.NumberValue)
      {
        output.WriteRawTag((byte) 17);
        output.WriteDouble(this.NumberValue);
      }
      if (this.kindCase_ == Value.KindOneofCase.StringValue)
      {
        output.WriteRawTag((byte) 26);
        output.WriteString(this.StringValue);
      }
      if (this.kindCase_ == Value.KindOneofCase.BoolValue)
      {
        output.WriteRawTag((byte) 32);
        output.WriteBool(this.BoolValue);
      }
      if (this.kindCase_ == Value.KindOneofCase.StructValue)
      {
        output.WriteRawTag((byte) 42);
        output.WriteMessage((IMessage) this.StructValue);
      }
      if (this.kindCase_ != Value.KindOneofCase.ListValue)
        return;
      output.WriteRawTag((byte) 50);
      output.WriteMessage((IMessage) this.ListValue);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.kindCase_ == Value.KindOneofCase.NullValue)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.NullValue);
      if (this.kindCase_ == Value.KindOneofCase.NumberValue)
        num += 9;
      if (this.kindCase_ == Value.KindOneofCase.StringValue)
        num += 1 + CodedOutputStream.ComputeStringSize(this.StringValue);
      if (this.kindCase_ == Value.KindOneofCase.BoolValue)
        num += 2;
      if (this.kindCase_ == Value.KindOneofCase.StructValue)
        num += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.StructValue);
      if (this.kindCase_ == Value.KindOneofCase.ListValue)
        num += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.ListValue);
      return num;
    }

    public void MergeFrom(Value other)
    {
      if (other == null)
        return;
      switch (other.KindCase)
      {
        case Value.KindOneofCase.NullValue:
          this.NullValue = other.NullValue;
          break;
        case Value.KindOneofCase.NumberValue:
          this.NumberValue = other.NumberValue;
          break;
        case Value.KindOneofCase.StringValue:
          this.StringValue = other.StringValue;
          break;
        case Value.KindOneofCase.BoolValue:
          this.BoolValue = other.BoolValue;
          break;
        case Value.KindOneofCase.StructValue:
          this.StructValue = other.StructValue;
          break;
        case Value.KindOneofCase.ListValue:
          this.ListValue = other.ListValue;
          break;
      }
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 8:
            this.kind_ = (object) input.ReadEnum();
            this.kindCase_ = Value.KindOneofCase.NullValue;
            continue;
          case 17:
            this.NumberValue = input.ReadDouble();
            continue;
          case 26:
            this.StringValue = input.ReadString();
            continue;
          case 32:
            this.BoolValue = input.ReadBool();
            continue;
          case 42:
            Struct @struct = new Struct();
            if (this.kindCase_ == Value.KindOneofCase.StructValue)
              @struct.MergeFrom(this.StructValue);
            input.ReadMessage((IMessage) @struct);
            this.StructValue = @struct;
            continue;
          case 50:
            ListValue listValue = new ListValue();
            if (this.kindCase_ == Value.KindOneofCase.ListValue)
              listValue.MergeFrom(this.ListValue);
            input.ReadMessage((IMessage) listValue);
            this.ListValue = listValue;
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }

    public enum KindOneofCase
    {
      None,
      NullValue,
      NumberValue,
      StringValue,
      BoolValue,
      StructValue,
      ListValue,
    }
  }
}
