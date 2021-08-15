// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.FieldCodec
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using System;
using System.Collections.Generic;

namespace Google.Protobuf
{
  public static class FieldCodec
  {
    public static FieldCodec<string> ForString(uint tag) => new FieldCodec<string>((Func<CodedInputStream, string>) (input => input.ReadString()), (Action<CodedOutputStream, string>) ((output, value) => output.WriteString(value)), new Func<string, int>(CodedOutputStream.ComputeStringSize), tag);

    public static FieldCodec<ByteString> ForBytes(uint tag) => new FieldCodec<ByteString>((Func<CodedInputStream, ByteString>) (input => input.ReadBytes()), (Action<CodedOutputStream, ByteString>) ((output, value) => output.WriteBytes(value)), new Func<ByteString, int>(CodedOutputStream.ComputeBytesSize), tag);

    public static FieldCodec<bool> ForBool(uint tag) => new FieldCodec<bool>((Func<CodedInputStream, bool>) (input => input.ReadBool()), (Action<CodedOutputStream, bool>) ((output, value) => output.WriteBool(value)), new Func<bool, int>(CodedOutputStream.ComputeBoolSize), tag);

    public static FieldCodec<int> ForInt32(uint tag) => new FieldCodec<int>((Func<CodedInputStream, int>) (input => input.ReadInt32()), (Action<CodedOutputStream, int>) ((output, value) => output.WriteInt32(value)), new Func<int, int>(CodedOutputStream.ComputeInt32Size), tag);

    public static FieldCodec<int> ForSInt32(uint tag) => new FieldCodec<int>((Func<CodedInputStream, int>) (input => input.ReadSInt32()), (Action<CodedOutputStream, int>) ((output, value) => output.WriteSInt32(value)), new Func<int, int>(CodedOutputStream.ComputeSInt32Size), tag);

    public static FieldCodec<uint> ForFixed32(uint tag) => new FieldCodec<uint>((Func<CodedInputStream, uint>) (input => input.ReadFixed32()), (Action<CodedOutputStream, uint>) ((output, value) => output.WriteFixed32(value)), 4, tag);

    public static FieldCodec<int> ForSFixed32(uint tag) => new FieldCodec<int>((Func<CodedInputStream, int>) (input => input.ReadSFixed32()), (Action<CodedOutputStream, int>) ((output, value) => output.WriteSFixed32(value)), 4, tag);

    public static FieldCodec<uint> ForUInt32(uint tag) => new FieldCodec<uint>((Func<CodedInputStream, uint>) (input => input.ReadUInt32()), (Action<CodedOutputStream, uint>) ((output, value) => output.WriteUInt32(value)), new Func<uint, int>(CodedOutputStream.ComputeUInt32Size), tag);

    public static FieldCodec<long> ForInt64(uint tag) => new FieldCodec<long>((Func<CodedInputStream, long>) (input => input.ReadInt64()), (Action<CodedOutputStream, long>) ((output, value) => output.WriteInt64(value)), new Func<long, int>(CodedOutputStream.ComputeInt64Size), tag);

    public static FieldCodec<long> ForSInt64(uint tag) => new FieldCodec<long>((Func<CodedInputStream, long>) (input => input.ReadSInt64()), (Action<CodedOutputStream, long>) ((output, value) => output.WriteSInt64(value)), new Func<long, int>(CodedOutputStream.ComputeSInt64Size), tag);

    public static FieldCodec<ulong> ForFixed64(uint tag) => new FieldCodec<ulong>((Func<CodedInputStream, ulong>) (input => input.ReadFixed64()), (Action<CodedOutputStream, ulong>) ((output, value) => output.WriteFixed64(value)), 8, tag);

    public static FieldCodec<long> ForSFixed64(uint tag) => new FieldCodec<long>((Func<CodedInputStream, long>) (input => input.ReadSFixed64()), (Action<CodedOutputStream, long>) ((output, value) => output.WriteSFixed64(value)), 8, tag);

    public static FieldCodec<ulong> ForUInt64(uint tag) => new FieldCodec<ulong>((Func<CodedInputStream, ulong>) (input => input.ReadUInt64()), (Action<CodedOutputStream, ulong>) ((output, value) => output.WriteUInt64(value)), new Func<ulong, int>(CodedOutputStream.ComputeUInt64Size), tag);

    public static FieldCodec<float> ForFloat(uint tag) => new FieldCodec<float>((Func<CodedInputStream, float>) (input => input.ReadFloat()), (Action<CodedOutputStream, float>) ((output, value) => output.WriteFloat(value)), new Func<float, int>(CodedOutputStream.ComputeFloatSize), tag);

    public static FieldCodec<double> ForDouble(uint tag) => new FieldCodec<double>((Func<CodedInputStream, double>) (input => input.ReadDouble()), (Action<CodedOutputStream, double>) ((output, value) => output.WriteDouble(value)), new Func<double, int>(CodedOutputStream.ComputeDoubleSize), tag);

    public static FieldCodec<T> ForEnum<T>(
      uint tag,
      Func<T, int> toInt32,
      Func<int, T> fromInt32)
    {
      return new FieldCodec<T>((Func<CodedInputStream, T>) (input => fromInt32(input.ReadEnum())), (Action<CodedOutputStream, T>) ((output, value) => output.WriteEnum(toInt32(value))), (Func<T, int>) (value => CodedOutputStream.ComputeEnumSize(toInt32(value))), tag);
    }

    public static FieldCodec<T> ForMessage<T>(uint tag, MessageParser<T> parser) where T : IMessage<T> => new FieldCodec<T>((Func<CodedInputStream, T>) (input =>
    {
      T template = parser.CreateTemplate();
      input.ReadMessage((IMessage) template);
      return template;
    }), (Action<CodedOutputStream, T>) ((output, value) => output.WriteMessage((IMessage) value)), (Func<T, int>) (message => CodedOutputStream.ComputeMessageSize((IMessage) message)), tag);

    public static FieldCodec<T> ForClassWrapper<T>(uint tag) where T : class
    {
      FieldCodec<T> nestedCodec = FieldCodec.WrapperCodecs.GetCodec<T>();
      return new FieldCodec<T>((Func<CodedInputStream, T>) (input => FieldCodec.WrapperCodecs.Read<T>(input, nestedCodec)), (Action<CodedOutputStream, T>) ((output, value) => FieldCodec.WrapperCodecs.Write<T>(output, value, nestedCodec)), (Func<T, int>) (value => FieldCodec.WrapperCodecs.CalculateSize<T>(value, nestedCodec)), tag, default (T));
    }

    public static FieldCodec<T?> ForStructWrapper<T>(uint tag) where T : struct
    {
      FieldCodec<T> nestedCodec = FieldCodec.WrapperCodecs.GetCodec<T>();
      return new FieldCodec<T?>((Func<CodedInputStream, T?>) (input => new T?(FieldCodec.WrapperCodecs.Read<T>(input, nestedCodec))), (Action<CodedOutputStream, T?>) ((output, value) => FieldCodec.WrapperCodecs.Write<T>(output, value.Value, nestedCodec)), (Func<T?, int>) (value => value.HasValue ? FieldCodec.WrapperCodecs.CalculateSize<T>(value.Value, nestedCodec) : 0), tag, new T?());
    }

    private static class WrapperCodecs
    {
      private const int WrapperValueFieldNumber = 1;
      private static readonly Dictionary<Type, object> Codecs = new Dictionary<Type, object>()
      {
        {
          typeof (bool),
          (object) FieldCodec.ForBool(WireFormat.MakeTag(1, WireFormat.WireType.Varint))
        },
        {
          typeof (int),
          (object) FieldCodec.ForInt32(WireFormat.MakeTag(1, WireFormat.WireType.Varint))
        },
        {
          typeof (long),
          (object) FieldCodec.ForInt64(WireFormat.MakeTag(1, WireFormat.WireType.Varint))
        },
        {
          typeof (uint),
          (object) FieldCodec.ForUInt32(WireFormat.MakeTag(1, WireFormat.WireType.Varint))
        },
        {
          typeof (ulong),
          (object) FieldCodec.ForUInt64(WireFormat.MakeTag(1, WireFormat.WireType.Varint))
        },
        {
          typeof (float),
          (object) FieldCodec.ForFloat(WireFormat.MakeTag(1, WireFormat.WireType.Fixed32))
        },
        {
          typeof (double),
          (object) FieldCodec.ForDouble(WireFormat.MakeTag(1, WireFormat.WireType.Fixed64))
        },
        {
          typeof (string),
          (object) FieldCodec.ForString(WireFormat.MakeTag(1, WireFormat.WireType.LengthDelimited))
        },
        {
          typeof (ByteString),
          (object) FieldCodec.ForBytes(WireFormat.MakeTag(1, WireFormat.WireType.LengthDelimited))
        }
      };

      internal static FieldCodec<T> GetCodec<T>()
      {
        object obj;
        if (!FieldCodec.WrapperCodecs.Codecs.TryGetValue(typeof (T), out obj))
          throw new InvalidOperationException("Invalid type argument requested for wrapper codec: " + (object) typeof (T));
        return (FieldCodec<T>) obj;
      }

      internal static T Read<T>(CodedInputStream input, FieldCodec<T> codec)
      {
        int byteLimit = input.ReadLength();
        int oldLimit = input.PushLimit(byteLimit);
        T obj = codec.DefaultValue;
        uint num;
        while ((num = input.ReadTag()) != 0U)
        {
          if ((int) num == (int) codec.Tag)
            obj = codec.Read(input);
          else
            input.SkipLastField();
        }
        input.CheckReadEndOfStreamTag();
        input.PopLimit(oldLimit);
        return obj;
      }

      internal static void Write<T>(CodedOutputStream output, T value, FieldCodec<T> codec)
      {
        output.WriteLength(codec.CalculateSizeWithTag(value));
        codec.WriteTagAndValue(output, value);
      }

      internal static int CalculateSize<T>(T value, FieldCodec<T> codec)
      {
        int sizeWithTag = codec.CalculateSizeWithTag(value);
        return CodedOutputStream.ComputeLengthSize(sizeWithTag) + sizeWithTag;
      }
    }
  }
}
