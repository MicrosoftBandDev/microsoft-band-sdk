// Decompiled with JetBrains decompiler
// Type: Google.Protobuf.WellKnownTypes.Duration
// Assembly: Google.Protobuf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=a7d26565bac4d604
// MVID: 3142230E-1A9B-42BB-AC54-848B892B3B7D
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Google.Protobuf.dll

using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Google.Protobuf.WellKnownTypes
{
  [DebuggerNonUserCode]
  public sealed class Duration : 
    IMessage<Duration>,
    IMessage,
    IEquatable<Duration>,
    IDeepCloneable<Duration>
  {
    public const int SecondsFieldNumber = 1;
    public const int NanosFieldNumber = 2;
    public const int NanosecondsPerSecond = 1000000000;
    public const int NanosecondsPerTick = 100;
    private static readonly MessageParser<Duration> _parser = new MessageParser<Duration>((Func<Duration>) (() => new Duration()));
    private long seconds_;
    private int nanos_;

    public static MessageParser<Duration> Parser => Duration._parser;

    public static MessageDescriptor Descriptor => Google.Protobuf.WellKnownTypes.Proto.Duration.Descriptor.MessageTypes[0];

    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => Duration.Descriptor;

    public Duration()
    {
    }

    public Duration(Duration other)
      : this()
    {
      this.seconds_ = other.seconds_;
      this.nanos_ = other.nanos_;
    }

    public Duration Clone() => new Duration(this);

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

    public override bool Equals(object other) => this.Equals(other as Duration);

    public bool Equals(Duration other) => !object.ReferenceEquals((object) other, (object) null) && (object.ReferenceEquals((object) other, (object) this) || this.Seconds == other.Seconds && this.Nanos == other.Nanos);

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

    public void MergeFrom(Duration other)
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

    public TimeSpan ToTimeSpan() => TimeSpan.FromTicks(checked (this.Seconds * 10000000L + (long) unchecked (this.Nanos / 100)));

    public static Duration FromTimeSpan(TimeSpan timeSpan)
    {
      long ticks = timeSpan.Ticks;
      long num1 = ticks / 10000000L;
      int num2 = checked ((int) unchecked (ticks % 10000000L) * 100);
      return new Duration() { Seconds = num1, Nanos = num2 };
    }

    public static Duration operator -(Duration value)
    {
      Preconditions.CheckNotNull<Duration>(value, nameof (value));
      return Duration.Normalize(checked (-value.Seconds), checked (-value.Nanos));
    }

    public static Duration operator +(Duration lhs, Duration rhs)
    {
      Preconditions.CheckNotNull<Duration>(lhs, nameof (lhs));
      Preconditions.CheckNotNull<Duration>(rhs, nameof (rhs));
      return Duration.Normalize(checked (lhs.Seconds + rhs.Seconds), checked (lhs.Nanos + rhs.Nanos));
    }

    public static Duration operator -(Duration lhs, Duration rhs)
    {
      Preconditions.CheckNotNull<Duration>(lhs, nameof (lhs));
      Preconditions.CheckNotNull<Duration>(rhs, nameof (rhs));
      return Duration.Normalize(checked (lhs.Seconds - rhs.Seconds), checked (lhs.Nanos - rhs.Nanos));
    }

    internal static Duration Normalize(long seconds, int nanoseconds)
    {
      int num = nanoseconds / 1000000000;
      seconds += (long) num;
      nanoseconds -= num * 1000000000;
      if (seconds < 0L && nanoseconds > 0)
      {
        ++seconds;
        nanoseconds -= 1000000000;
      }
      else if (seconds > 0L && nanoseconds < 0)
      {
        --seconds;
        nanoseconds += 1000000000;
      }
      return new Duration()
      {
        Seconds = seconds,
        Nanos = nanoseconds
      };
    }
  }
}
