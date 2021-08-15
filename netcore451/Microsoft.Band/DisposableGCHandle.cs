// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.DisposableGCHandle
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Band
{
  internal sealed class DisposableGCHandle : IDisposable
  {
    private GCHandle handle;

    internal static DisposableGCHandle Alloc(
      object target,
      GCHandleType handleType = GCHandleType.Normal)
    {
      return target != null ? new DisposableGCHandle()
      {
        handle = GCHandle.Alloc(target, handleType)
      } : throw new ArgumentNullException(nameof (target));
    }

    internal object Target => this.handle.Target;

    internal bool IsAllocated => this.handle.IsAllocated;

    internal IntPtr AddrOfPinnedObject() => this.handle.AddrOfPinnedObject();

    internal void Free() => this.handle.Free();

    public void Dispose()
    {
      if (!this.IsAllocated)
        return;
      this.Free();
    }
  }
}
