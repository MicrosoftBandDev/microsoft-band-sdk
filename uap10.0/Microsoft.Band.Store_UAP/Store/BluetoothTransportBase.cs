// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Store.BluetoothTransportBase
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;

namespace Microsoft.Band.Store
{
  internal abstract class BluetoothTransportBase : IDisposable
  {
    protected RfcommDeviceService rfcommService;
    private bool isConnected;
    private ICargoStream cargoStream;
    private CargoStreamReader cargoReader;
    private bool disposed;
    protected ILoggerProvider loggerProvider;

    public event EventHandler<TransportDisconnectedEventArgs> Disconnected;

    protected BluetoothTransportBase(ILoggerProvider loggerProvider)
    {
      this.loggerProvider = loggerProvider;
      this.isConnected = false;
    }

    protected BluetoothTransportBase(RfcommDeviceService service, ILoggerProvider loggerProvider)
      : this(loggerProvider)
    {
      this.rfcommService = service;
    }

    public ICargoStream CargoStream
    {
      get
      {
        this.CheckIfDisposed();
        return this.cargoStream;
      }
    }

    public CargoStreamReader CargoReader
    {
      get
      {
        this.CheckIfDisposed();
        return this.cargoReader;
      }
    }

    public BandConnectionType ConnectionType => BandConnectionType.Bluetooth;

    protected void RaiseDisconnectedEvent(TransportDisconnectedEventArgs args)
    {
      EventHandler<TransportDisconnectedEventArgs> disconnected = this.Disconnected;
      if (disconnected == null)
        return;
      disconnected((object) this, args);
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
    public void Connect(RfcommDeviceService service, ushort maxConnectAttempts = 1)
    {
      if (service == null)
        throw new ArgumentNullException(nameof (service));
      this.CheckIfDisposed();
      if (this.IsConnected)
        return;
      if (maxConnectAttempts == (ushort) 0)
        throw new ArgumentOutOfRangeException(nameof (maxConnectAttempts));
      this.loggerProvider.Log(ProviderLogLevel.Info, "Socket.ConnectAsync()... Max Attempts: {0}, ConnectionServiceName: {1}", (object) maxConnectAttempts, (object) service.ConnectionServiceName);
      ushort num = 0;
      Exception exception = (Exception) null;
      Stopwatch stopwatch1 = Stopwatch.StartNew();
      StreamSocket socket;
      do
      {
        ++num;
        Stopwatch stopwatch2 = Stopwatch.StartNew();
        socket = new StreamSocket();
        try
        {
          this.loggerProvider.Log(ProviderLogLevel.Info, "Socket.ConnectAsync()... Attempt: {0}/{1}", (object) num, (object) maxConnectAttempts);
          WindowsRuntimeSystemExtensions.AsTask(socket.ConnectAsync(service.ConnectionHostName, service.ConnectionServiceName)).Wait();
          this.isConnected = true;
          this.loggerProvider.Log(ProviderLogLevel.Info, "Socket.ConnectAsync() succeeded: Attempt: {0}/{1}, Elapsed: {2}", (object) num, (object) maxConnectAttempts, (object) stopwatch2.Elapsed);
        }
        catch (Exception ex)
        {
          socket.Dispose();
          if (exception == null)
            exception = ex;
          this.loggerProvider.LogException(ProviderLogLevel.Warning, ex, "Socket.ConnectAsync() failed: Attempt: {0}/{1}, Elapsed: {2}", new object[3]
          {
            (object) num,
            (object) maxConnectAttempts,
            (object) stopwatch2.Elapsed
          });
        }
      }
      while (!this.isConnected && (int) num < (int) maxConnectAttempts);
      if (this.isConnected)
      {
        this.cargoStream = (ICargoStream) new StreamSocketStream(socket);
        this.cargoReader = new CargoStreamReader(this.cargoStream, BufferServer.Pool_8192);
        this.loggerProvider.Log(ProviderLogLevel.Info, "Socket.ConnectAsync() succeeded: Elapsed: {0}, ConnectionServiceName: {1}", (object) stopwatch1.Elapsed, (object) service.ConnectionServiceName);
      }
      else
      {
        this.loggerProvider.Log(ProviderLogLevel.Error, "Socket.ConnectAsync() failed: Attempts: {0}, Elapsed: {1}, ConnectionServiceName: {2}", (object) num, (object) stopwatch1.Elapsed, (object) service.ConnectionServiceName);
        throw new BandIOException(BandResources.ConnectionAttemptFailed, exception);
      }
    }

    public virtual void Disconnect()
    {
      if (this.IsConnected)
      {
        this.isConnected = false;
        CargoStreamReader cargoReader = this.cargoReader;
        if (cargoReader != null)
        {
          cargoReader.Dispose();
          this.cargoReader = (CargoStreamReader) null;
        }
        this.cargoStream = (ICargoStream) null;
      }
      this.RaiseDisconnectedEvent(new TransportDisconnectedEventArgs(TransportDisconnectedReason.DisconnectCall));
    }

    public bool IsConnected => this.isConnected;

    protected void CheckIfDisconnected()
    {
      if (!this.IsConnected)
        throw new InvalidOperationException("Transport not connected");
    }

    protected void CheckIfDisposed()
    {
      if (this.disposed)
        throw new ObjectDisposedException(nameof (BluetoothTransportBase));
    }

    public void Dispose() => this.Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.disposed)
        return;
      this.Disconnect();
      ICargoStream cargoStream = this.cargoStream;
      if (cargoStream != null)
      {
        ((IDisposable) cargoStream).Dispose();
        this.cargoStream = (ICargoStream) null;
      }
      this.disposed = true;
    }
  }
}
