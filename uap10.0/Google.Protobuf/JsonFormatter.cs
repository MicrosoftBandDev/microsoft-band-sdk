// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.JsonFormatter
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Google.Protobuf
{
  public sealed class JsonFormatter
  {
    private const string Hex = "0123456789abcdef";
    private static JsonFormatter defaultInstance = new JsonFormatter(JsonFormatter.Settings.Default);
    private static readonly string[] CommonRepresentations = new string[160]
    {
      "\\u0000",
      "\\u0001",
      "\\u0002",
      "\\u0003",
      "\\u0004",
      "\\u0005",
      "\\u0006",
      "\\u0007",
      "\\b",
      "\\t",
      "\\n",
      "\\u000b",
      "\\f",
      "\\r",
      "\\u000e",
      "\\u000f",
      "\\u0010",
      "\\u0011",
      "\\u0012",
      "\\u0013",
      "\\u0014",
      "\\u0015",
      "\\u0016",
      "\\u0017",
      "\\u0018",
      "\\u0019",
      "\\u001a",
      "\\u001b",
      "\\u001c",
      "\\u001d",
      "\\u001e",
      "\\u001f",
      "",
      "",
      "\\\"",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "\\u003c",
      "",
      "\\u003e",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "\\\\",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "\\u007f",
      "\\u0080",
      "\\u0081",
      "\\u0082",
      "\\u0083",
      "\\u0084",
      "\\u0085",
      "\\u0086",
      "\\u0087",
      "\\u0088",
      "\\u0089",
      "\\u008a",
      "\\u008b",
      "\\u008c",
      "\\u008d",
      "\\u008e",
      "\\u008f",
      "\\u0090",
      "\\u0091",
      "\\u0092",
      "\\u0093",
      "\\u0094",
      "\\u0095",
      "\\u0096",
      "\\u0097",
      "\\u0098",
      "\\u0099",
      "\\u009a",
      "\\u009b",
      "\\u009c",
      "\\u009d",
      "\\u009e",
      "\\u009f"
    };
    private readonly JsonFormatter.Settings settings;

    public static JsonFormatter Default => JsonFormatter.defaultInstance;

    static JsonFormatter()
    {
      for (int index = 0; index < JsonFormatter.CommonRepresentations.Length; ++index)
      {
        if (JsonFormatter.CommonRepresentations[index] == "")
          JsonFormatter.CommonRepresentations[index] = ((char) index).ToString();
      }
    }

    public JsonFormatter(JsonFormatter.Settings settings) => this.settings = settings;

    public string Format(IMessage message)
    {
      Preconditions.CheckNotNull<IMessage>(message, nameof (message));
      StringBuilder builder = new StringBuilder();
      if (message.Descriptor.IsWellKnownType)
        this.WriteWellKnownTypeValue(builder, message.Descriptor, (object) message, false);
      else
        this.WriteMessage(builder, message);
      return builder.ToString();
    }

    private void WriteMessage(StringBuilder builder, IMessage message)
    {
      if (message == null)
      {
        JsonFormatter.WriteNull(builder);
      }
      else
      {
        builder.Append("{ ");
        MessageDescriptor.FieldCollection fields = message.Descriptor.Fields;
        bool flag = true;
        foreach (FieldDescriptor fieldDescriptor in (IEnumerable<FieldDescriptor>) fields.InFieldNumberOrder())
        {
          IFieldAccessor accessor = fieldDescriptor.Accessor;
          if (fieldDescriptor.ContainingOneof == null || fieldDescriptor.ContainingOneof.Accessor.GetCaseFieldDescriptor(message) == fieldDescriptor)
          {
            object obj = accessor.GetValue(message);
            if ((fieldDescriptor.ContainingOneof != null || this.settings.FormatDefaultValues || !JsonFormatter.IsDefaultValue(accessor, obj)) && (fieldDescriptor.IsRepeated || fieldDescriptor.IsMap || this.CanWriteSingleValue(accessor.Descriptor, obj)))
            {
              if (!flag)
                builder.Append(", ");
              this.WriteString(builder, JsonFormatter.ToCamelCase(accessor.Descriptor.Name));
              builder.Append(": ");
              this.WriteValue(builder, accessor, obj);
              flag = false;
            }
          }
        }
        builder.Append(flag ? "}" : " }");
      }
    }

    internal static string ToCamelCase(string input)
    {
      bool flag1 = false;
      bool flag2 = true;
      bool flag3 = true;
      StringBuilder stringBuilder = new StringBuilder(input.Length);
      int index = 0;
      while (index < input.Length)
      {
        bool flag4 = char.IsUpper(input[index]);
        if (input[index] == '_')
        {
          flag1 = true;
          if (stringBuilder.Length != 0)
            flag3 = false;
        }
        else
        {
          if (flag3)
          {
            if (stringBuilder.Length != 0 && flag4 && (!flag2 || index + 1 < input.Length && char.IsLower(input[index + 1])))
            {
              flag3 = false;
            }
            else
            {
              stringBuilder.Append(char.ToLowerInvariant(input[index]));
              goto label_12;
            }
          }
          else if (flag1)
          {
            flag1 = false;
            if (char.IsLower(input[index]))
            {
              stringBuilder.Append(char.ToUpperInvariant(input[index]));
              goto label_12;
            }
          }
          stringBuilder.Append(input[index]);
        }
label_12:
        ++index;
        flag2 = flag4;
      }
      return stringBuilder.ToString();
    }

    private static void WriteNull(StringBuilder builder) => builder.Append("null");

    private static bool IsDefaultValue(IFieldAccessor accessor, object value)
    {
      if (accessor.Descriptor.IsMap)
        return ((ICollection) value).Count == 0;
      if (accessor.Descriptor.IsRepeated)
        return ((ICollection) value).Count == 0;
      switch (accessor.Descriptor.FieldType)
      {
        case FieldType.Double:
          return (double) value == 0.0;
        case FieldType.Float:
          return (double) (float) value == 0.0;
        case FieldType.Int64:
        case FieldType.SFixed64:
        case FieldType.SInt64:
          return (long) value == 0L;
        case FieldType.UInt64:
        case FieldType.Fixed64:
          return (ulong) value == 0UL;
        case FieldType.Int32:
        case FieldType.SFixed32:
        case FieldType.SInt32:
        case FieldType.Enum:
          return (int) value == 0;
        case FieldType.Fixed32:
        case FieldType.UInt32:
          return (uint) value == 0U;
        case FieldType.Bool:
          return !(bool) value;
        case FieldType.String:
          return (string) value == "";
        case FieldType.Group:
        case FieldType.Message:
          return value == null;
        case FieldType.Bytes:
          return (ByteString) value == ByteString.Empty;
        default:
          throw new ArgumentException("Invalid field type");
      }
    }

    private void WriteValue(StringBuilder builder, IFieldAccessor accessor, object value)
    {
      if (accessor.Descriptor.IsMap)
        this.WriteDictionary(builder, accessor, (IDictionary) value);
      else if (accessor.Descriptor.IsRepeated)
        this.WriteList(builder, accessor, (IList) value);
      else
        this.WriteSingleValue(builder, accessor.Descriptor, value);
    }

    private void WriteSingleValue(StringBuilder builder, FieldDescriptor descriptor, object value)
    {
      switch (descriptor.FieldType)
      {
        case FieldType.Double:
        case FieldType.Float:
          string str = ((IFormattable) value).ToString("r", (IFormatProvider) CultureInfo.InvariantCulture);
          if (str == "NaN" || str == "Infinity" || str == "-Infinity")
          {
            builder.Append('"');
            builder.Append(str);
            builder.Append('"');
            break;
          }
          builder.Append(str);
          break;
        case FieldType.Int64:
        case FieldType.UInt64:
        case FieldType.Fixed64:
        case FieldType.SFixed64:
        case FieldType.SInt64:
          builder.Append('"');
          IFormattable formattable1 = (IFormattable) value;
          builder.Append(formattable1.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture));
          builder.Append('"');
          break;
        case FieldType.Int32:
        case FieldType.Fixed32:
        case FieldType.UInt32:
        case FieldType.SFixed32:
        case FieldType.SInt32:
          IFormattable formattable2 = (IFormattable) value;
          builder.Append(formattable2.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case FieldType.Bool:
          builder.Append((bool) value ? "true" : "false");
          break;
        case FieldType.String:
          this.WriteString(builder, (string) value);
          break;
        case FieldType.Group:
        case FieldType.Message:
          if (descriptor.MessageType.IsWellKnownType)
          {
            this.WriteWellKnownTypeValue(builder, descriptor.MessageType, value, true);
            break;
          }
          this.WriteMessage(builder, (IMessage) value);
          break;
        case FieldType.Bytes:
          builder.Append('"');
          builder.Append(((ByteString) value).ToBase64());
          builder.Append('"');
          break;
        case FieldType.Enum:
          EnumValueDescriptor valueByNumber = descriptor.EnumType.FindValueByNumber((int) value);
          this.WriteString(builder, valueByNumber.Name);
          break;
        default:
          throw new ArgumentException("Invalid field type: " + (object) descriptor.FieldType);
      }
    }

    private void WriteWellKnownTypeValue(
      StringBuilder builder,
      MessageDescriptor descriptor,
      object value,
      bool inField)
    {
      if (value == null)
        JsonFormatter.WriteNull(builder);
      else if (descriptor.File == Int32Value.Descriptor.File)
        this.WriteSingleValue(builder, descriptor.FindFieldByNumber(1), value);
      else if (descriptor.FullName == Timestamp.Descriptor.FullName)
        this.MaybeWrapInString(builder, value, new Action<StringBuilder, IMessage>(this.WriteTimestamp), inField);
      else if (descriptor.FullName == Duration.Descriptor.FullName)
        this.MaybeWrapInString(builder, value, new Action<StringBuilder, IMessage>(this.WriteDuration), inField);
      else if (descriptor.FullName == FieldMask.Descriptor.FullName)
        this.MaybeWrapInString(builder, value, new Action<StringBuilder, IMessage>(this.WriteFieldMask), inField);
      else if (descriptor.FullName == Struct.Descriptor.FullName)
        this.WriteStruct(builder, (IMessage) value);
      else if (descriptor.FullName == ListValue.Descriptor.FullName)
      {
        IFieldAccessor accessor = descriptor.Fields[1].Accessor;
        this.WriteList(builder, accessor, (IList) accessor.GetValue((IMessage) value));
      }
      else if (descriptor.FullName == Value.Descriptor.FullName)
        this.WriteStructFieldValue(builder, (IMessage) value);
      else
        this.WriteMessage(builder, (IMessage) value);
    }

    private void MaybeWrapInString(
      StringBuilder builder,
      object value,
      Action<StringBuilder, IMessage> action,
      bool inField)
    {
      if (inField)
      {
        builder.Append('"');
        action(builder, (IMessage) value);
        builder.Append('"');
      }
      else
        action(builder, (IMessage) value);
    }

    private void WriteTimestamp(StringBuilder builder, IMessage value)
    {
      int nanoseconds = (int) value.Descriptor.Fields[2].Accessor.GetValue(value);
      Timestamp timestamp = Timestamp.Normalize((long) value.Descriptor.Fields[1].Accessor.GetValue(value), nanoseconds);
      DateTime dateTime = timestamp.ToDateTime();
      builder.Append(dateTime.ToString("yyyy'-'MM'-'dd'T'HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture));
      JsonFormatter.AppendNanoseconds(builder, Math.Abs(timestamp.Nanos));
      builder.Append('Z');
    }

    private void WriteDuration(StringBuilder builder, IMessage value)
    {
      int nanoseconds = (int) value.Descriptor.Fields[2].Accessor.GetValue(value);
      Duration duration = Duration.Normalize((long) value.Descriptor.Fields[1].Accessor.GetValue(value), nanoseconds);
      if (duration.Seconds == 0L && duration.Nanos < 0)
        builder.Append('-');
      builder.Append(duration.Seconds.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture));
      JsonFormatter.AppendNanoseconds(builder, Math.Abs(duration.Nanos));
      builder.Append('s');
    }

    private void WriteFieldMask(StringBuilder builder, IMessage value)
    {
      IList source = (IList) value.Descriptor.Fields[1].Accessor.GetValue(value);
      this.AppendEscapedString(builder, string.Join(",", source.Cast<string>().Select<string, string>(new Func<string, string>(JsonFormatter.ToCamelCase))));
    }

    private static void AppendNanoseconds(StringBuilder builder, int nanos)
    {
      if (nanos == 0)
        return;
      builder.Append('.');
      if (nanos % 1000000 == 0)
        builder.Append((nanos / 1000000).ToString("d", (IFormatProvider) CultureInfo.InvariantCulture));
      else if (nanos % 1000 == 0)
        builder.Append((nanos / 1000).ToString("d", (IFormatProvider) CultureInfo.InvariantCulture));
      else
        builder.Append(nanos.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture));
    }

    private void WriteStruct(StringBuilder builder, IMessage message)
    {
      builder.Append("{ ");
      IDictionary dictionary = (IDictionary) message.Descriptor.Fields[1].Accessor.GetValue(message);
      bool flag = true;
      foreach (DictionaryEntry dictionaryEntry in dictionary)
      {
        string key = (string) dictionaryEntry.Key;
        IMessage message1 = (IMessage) dictionaryEntry.Value;
        if (string.IsNullOrEmpty(key) || message1 == null)
          throw new InvalidOperationException("Struct fields cannot have an empty key or a null value.");
        if (!flag)
          builder.Append(", ");
        this.WriteString(builder, key);
        builder.Append(": ");
        this.WriteStructFieldValue(builder, message1);
        flag = false;
      }
      builder.Append(flag ? "}" : " }");
    }

    private void WriteStructFieldValue(StringBuilder builder, IMessage message)
    {
      FieldDescriptor caseFieldDescriptor = message.Descriptor.Oneofs[0].Accessor.GetCaseFieldDescriptor(message);
      if (caseFieldDescriptor == null)
        throw new InvalidOperationException("Value message must contain a value for the oneof.");
      object obj = caseFieldDescriptor.Accessor.GetValue(message);
      switch (caseFieldDescriptor.FieldNumber)
      {
        case 1:
          JsonFormatter.WriteNull(builder);
          break;
        case 2:
        case 3:
        case 4:
          this.WriteSingleValue(builder, caseFieldDescriptor, obj);
          break;
        case 5:
        case 6:
          IMessage message1 = (IMessage) caseFieldDescriptor.Accessor.GetValue(message);
          this.WriteWellKnownTypeValue(builder, message1.Descriptor, (object) message1, true);
          break;
        default:
          throw new InvalidOperationException("Unexpected case in struct field: " + (object) caseFieldDescriptor.FieldNumber);
      }
    }

    private void WriteList(StringBuilder builder, IFieldAccessor accessor, IList list)
    {
      builder.Append("[ ");
      bool flag = true;
      foreach (object obj in (IEnumerable) list)
      {
        if (this.CanWriteSingleValue(accessor.Descriptor, obj))
        {
          if (!flag)
            builder.Append(", ");
          this.WriteSingleValue(builder, accessor.Descriptor, obj);
          flag = false;
        }
      }
      builder.Append(flag ? "]" : " ]");
    }

    private void WriteDictionary(
      StringBuilder builder,
      IFieldAccessor accessor,
      IDictionary dictionary)
    {
      builder.Append("{ ");
      bool flag = true;
      FieldDescriptor fieldByNumber1 = accessor.Descriptor.MessageType.FindFieldByNumber(1);
      FieldDescriptor fieldByNumber2 = accessor.Descriptor.MessageType.FindFieldByNumber(2);
      foreach (DictionaryEntry dictionaryEntry in dictionary)
      {
        if (this.CanWriteSingleValue(fieldByNumber2, dictionaryEntry.Value))
        {
          if (!flag)
            builder.Append(", ");
          string text;
          switch (fieldByNumber1.FieldType)
          {
            case FieldType.Int64:
            case FieldType.UInt64:
            case FieldType.Int32:
            case FieldType.Fixed64:
            case FieldType.Fixed32:
            case FieldType.UInt32:
            case FieldType.SFixed32:
            case FieldType.SFixed64:
            case FieldType.SInt32:
            case FieldType.SInt64:
              text = ((IFormattable) dictionaryEntry.Key).ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
              break;
            case FieldType.Bool:
              text = (bool) dictionaryEntry.Key ? "true" : "false";
              break;
            case FieldType.String:
              text = (string) dictionaryEntry.Key;
              break;
            default:
              throw new ArgumentException("Invalid key type: " + (object) fieldByNumber1.FieldType);
          }
          this.WriteString(builder, text);
          builder.Append(": ");
          this.WriteSingleValue(builder, fieldByNumber2, dictionaryEntry.Value);
          flag = false;
        }
      }
      builder.Append(flag ? "}" : " }");
    }

    private bool CanWriteSingleValue(FieldDescriptor descriptor, object value) => descriptor.FieldType != FieldType.Enum || descriptor.EnumType.FindValueByNumber((int) value) != null;

    private void WriteString(StringBuilder builder, string text)
    {
      builder.Append('"');
      this.AppendEscapedString(builder, text);
      builder.Append('"');
    }

    private void AppendEscapedString(StringBuilder builder, string text)
    {
      for (int index = 0; index < text.Length; ++index)
      {
        char c = text[index];
        if (c < ' ')
          builder.Append(JsonFormatter.CommonRepresentations[(int) c]);
        else if (char.IsHighSurrogate(c))
        {
          ++index;
          if (index == text.Length || !char.IsLowSurrogate(text[index]))
            throw new ArgumentException("String contains low surrogate not followed by high surrogate");
          JsonFormatter.HexEncodeUtf16CodeUnit(builder, c);
          JsonFormatter.HexEncodeUtf16CodeUnit(builder, text[index]);
        }
        else
        {
          if (char.IsLowSurrogate(c))
            throw new ArgumentException("String contains high surrogate not preceded by low surrogate");
          switch ((uint) c)
          {
            case 173:
            case 1757:
            case 1807:
            case 6068:
            case 6069:
            case 65279:
            case 65529:
            case 65530:
            case 65531:
              JsonFormatter.HexEncodeUtf16CodeUnit(builder, c);
              continue;
            default:
              if (c >= '\u0600' && c <= '\u0603' || c >= '\u200B' && c <= '\u200F' || (c >= '\u2028' && c <= '\u202E' || c >= '\u2060' && c <= '\u2064') || c >= '\u206A' && c <= '\u206F')
              {
                JsonFormatter.HexEncodeUtf16CodeUnit(builder, c);
                continue;
              }
              builder.Append(c);
              continue;
          }
        }
      }
    }

    private static void HexEncodeUtf16CodeUnit(StringBuilder builder, char c)
    {
      builder.Append("\\u");
      builder.Append("0123456789abcdef"[(int) c >> 12 & 15]);
      builder.Append("0123456789abcdef"[(int) c >> 8 & 15]);
      builder.Append("0123456789abcdef"[(int) c >> 4 & 15]);
      builder.Append("0123456789abcdef"[(int) c & 15]);
    }

    public sealed class Settings
    {
      private static readonly JsonFormatter.Settings defaultInstance = new JsonFormatter.Settings(false);
      private readonly bool formatDefaultValues;

      public static JsonFormatter.Settings Default => JsonFormatter.Settings.defaultInstance;

      public bool FormatDefaultValues => this.formatDefaultValues;

      public Settings(bool formatDefaultValues) => this.formatDefaultValues = formatDefaultValues;
    }
  }
}
