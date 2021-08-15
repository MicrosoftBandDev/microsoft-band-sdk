// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.UsbTransportBase
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;

namespace Microsoft.Band
{
  internal abstract class UsbTransportBase : IDeviceTransport, IReadableTransport, IDisposable
  {
    protected ICargoStream cargoStream;
    protected CargoStreamReader cargoReader;
    protected CargoStreamWriter cargoWriter;
    protected bool isDisposed;

    public event EventHandler<TransportDisconnectedEventArgs> Disconnected;

    public ICargoStream CargoStream => this.ReturnIfNotDisposed<ICargoStream>(this.cargoStream);

    public CargoStreamReader CargoReader => this.ReturnIfNotDisposed<CargoStreamReader>(this.cargoReader);

    public CargoStreamWriter CargoWriter => this.ReturnIfNotDisposed<CargoStreamWriter>(this.cargoWriter);

    public abstract BandConnectionType ConnectionType { get; }

    public abstract bool IsConnected { get; }

    private T ReturnIfNotDisposed<T>(T value)
    {
      this.CheckIfDisposed();
      return value;
    }

    public abstract void Connect(ushort maxConnectAttempts = 1);

    public abstract void Disconnect();

    public void WriteCommandPacket(
      ushort commandId,
      uint argBufSize,
      uint dataStageSize,
      Action<ICargoWriter> writeArgBuf,
      bool flush)
    {
      this.CheckIfDisposed();
      this.CheckIfDisconnected();
      this.CargoWriter.WriteCommandPacket(commandId, argBufSize, dataStageSize, writeArgBuf, false, true);
    }

    protected void RaiseDisconnectedEvent(TransportDisconnectedEventArgs args)
    {
      EventHandler<TransportDisconnectedEventArgs> disconnected = this.Disconnected;
      if (disconnected == null)
        return;
      disconnected((object) this, args);
    }

    protected abstract void CheckIfDisposed();

    protected abstract void CheckIfDisconnected();

    public void Dispose()
    {
      if (this.isDisposed)
        return;
      this.isDisposed = true;
      this.Disconnect();
    }
  }
}
