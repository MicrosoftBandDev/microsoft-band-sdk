// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Timestamp
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Timestamp : 
    IMessage<Timestamp>,
    IMessage,
    IEquatable<Timestamp>,
    IDeepCloneable<Timestamp>
  {
    public const int SecondsFieldNumber = 1;
    public const int NanosFieldNumber = 2;
    private static readonly MessageParser<Timestamp> _parser = new MessageParser<Timestamp>((Func<Timestamp>) (() => new Timestamp()));
    private long seconds_;
    private int nanos_;
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly long BclSecondsAtUnixEpoch = Timestamp.UnixEpoch.Ticks / 10000000L;

    public static MessageParser<Timestamp> Parser => Timestamp._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Timestamp.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Timestamp.Descriptor;

    public Timestamp()
    {
    }

    public Timestamp(Timestamp other)
      : this()
    {
      this.seconds_ = other.seconds_;
      this.nanos_ = other.nanos_;
    }

    public Timestamp Clone() => new Timestamp(this);

    public long Seconds
    {
      get => this.seconds_;
      set => this.seconds_ = value;
    }

    public int Nanos
    {
      get => this.nanos_;
      set => this.nanos_ = value;
    }

    public override bool Equals(object other) => this.Equals(other as Timestamp);

    public bool Equals(Timestamp other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Seconds == other.Seconds && this.Nanos == other.Nanos);

    public override int GetHashCode()
    {
      int num = 1;
      if (this.Seconds != 0L)
        num ^= this.Seconds.GetHashCode();
      if (this.Nanos != 0)
        num ^= this.Nanos.GetHashCode();
      return num;
    }

    public override string ToString() => JsonFormatter.Default.Format((IMessage) this);

    public void WriteTo(CodedOutputStream output)
    {
      if (this.Seconds != 0L)
      {
        output.WriteRawTag((byte) 8);
        output.WriteInt64(this.Seconds);
      }
      if (this.Nanos == 0)
        return;
      output.WriteRawTag((byte) 16);
      output.WriteInt32(this.Nanos);
    }

    public int CalculateSize()
    {
      int num = 0;
      if (this.Seconds != 0L)
        num += 1 + CodedOutputStream.ComputeInt64Size(this.Seconds);
      if (this.Nanos != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Nanos);
      return num;
    }

    public void MergeFrom(Timestamp other)
    {
      if (other == null)
        return;
      if (other.Seconds != 0L)
        this.Seconds = other.Seconds;
      if (other.Nanos == 0)
        return;
      this.Nanos = other.Nanos;
    }

    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 8:
            this.Seconds = input.ReadInt64();
            continue;
          case 16:
            this.Nanos = input.ReadInt32();
            continue;
          default:
            input.SkipLastField();
            continue;
        }
      }
    }

    public static Duration operator -(Timestamp lhs, Timestamp rhs)
    {
      Preconditions.CheckNotNull<Timestamp>(lhs, nameof (lhs));
      Preconditions.CheckNotNull<Timestamp>(rhs, nameof (rhs));
      return Duration.Normalize(checked (lhs.Seconds - rhs.Seconds), checked (lhs.Nanos - rhs.Nanos));
    }

    public static Timestamp operator +(Timestamp lhs, Duration rhs)
    {
      Preconditions.CheckNotNull<Timestamp>(lhs, nameof (lhs));
      Preconditions.CheckNotNull<Duration>(rhs, nameof (rhs));
      return Timestamp.Normalize(checked (lhs.Seconds + rhs.Seconds), checked (lhs.Nanos + rhs.Nanos));
    }

    public static Timestamp operator -(Timestamp lhs, Duration rhs)
    {
      Preconditions.CheckNotNull<Timestamp>(lhs, nameof (lhs));
      Preconditions.CheckNotNull<Duration>(rhs, nameof (rhs));
      return Timestamp.Normalize(checked (lhs.Seconds - rhs.Seconds), checked (lhs.Nanos - rhs.Nanos));
    }

    public DateTime ToDateTime() => Timestamp.UnixEpoch.AddSeconds((double) this.Seconds).AddTicks((long) (this.Nanos / 100));

    public DateTimeOffset ToDateTimeOffset() => new DateTimeOffset(this.ToDateTime(), TimeSpan.Zero);

    public static Timestamp FromDateTime(DateTime dateTime)
    {
      if (dateTime.Kind != DateTimeKind.Utc)
        throw new ArgumentException("Conversion from DateTime to Timestamp requires the DateTime kind to be Utc", nameof (dateTime));
      long num1 = dateTime.Ticks / 10000000L;
      int num2 = (int) (dateTime.Ticks % 10000000L) * 100;
      return new Timestamp()
      {
        Seconds = num1 - Timestamp.BclSecondsAtUnixEpoch,
        Nanos = num2
      };
    }

    public static Timestamp FromDateTimeOffset(DateTimeOffset dateTimeOffset) => Timestamp.FromDateTime(dateTimeOffset.UtcDateTime);

    internal static Timestamp Normalize(long seconds, int nanoseconds)
    {
      int num = nanoseconds / 1000000000;
      seconds += (long) num;
      nanoseconds -= num * 1000000000;
      if (nanoseconds < 0)
      {
        nanoseconds += 1000000000;
        --seconds;
      }
      return new Timestamp()
      {
        Seconds = seconds,
        Nanos = nanoseconds
      };
    }
  }
}
