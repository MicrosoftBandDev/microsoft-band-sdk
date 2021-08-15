// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BackgroundTileEventHandler
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using Microsoft.Band.Tiles;
using System;
using System.Collections.Generic;
using System.Threading;
using Windows.Foundation.Collections;

namespace Microsoft.Band
{
  public class BackgroundTileEventHandler
  {
    private static BackgroundTileEventHandler instance;
    private static readonly object syncLock = new object();

    public static BackgroundTileEventHandler Instance
    {
      get
      {
        object syncLock = BackgroundTileEventHandler.syncLock;
        bool lockTaken = false;
        try
        {
          Monitor.Enter(syncLock, ref lockTaken);
          if (BackgroundTileEventHandler.instance == null)
            BackgroundTileEventHandler.instance = new BackgroundTileEventHandler();
          return BackgroundTileEventHandler.instance;
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(syncLock);
        }
      }
    }

    public event EventHandler<BandTileEventArgs<IBandTileOpenedEvent>> TileOpened;

    public event EventHandler<BandTileEventArgs<IBandTileButtonPressedEvent>> TileButtonPressed;

    public event EventHandler<BandTileEventArgs<IBandTileClosedEvent>> TileClosed;

    public bool HandleTileEvent(ValueSet message)
    {
      BandTileEventBase bandTileEventBase = BandTileEventBase.ConstructFromDictionary((IDictionary<string, object>) message);
      bool flag;
      if (bandTileEventBase == null)
      {
        flag = false;
      }
      else
      {
        flag = true;
        try
        {
          switch (bandTileEventBase)
          {
            case IBandTileOpenedEvent _:
              if (this.TileOpened != null)
              {
                this.TileOpened((object) this, new BandTileEventArgs<IBandTileOpenedEvent>((IBandTileOpenedEvent) bandTileEventBase));
                break;
              }
              break;
            case IBandTileButtonPressedEvent _:
              if (this.TileButtonPressed != null)
              {
                this.TileButtonPressed((object) this, new BandTileEventArgs<IBandTileButtonPressedEvent>((IBandTileButtonPressedEvent) bandTileEventBase));
                break;
              }
              break;
            case IBandTileClosedEvent _:
              if (this.TileClosed != null)
              {
                this.TileClosed((object) this, new BandTileEventArgs<IBandTileClosedEvent>((IBandTileClosedEvent) bandTileEventBase));
                break;
              }
              break;
          }
        }
        catch
        {
          throw;
        }
      }
      return flag;
    }
  }
}
