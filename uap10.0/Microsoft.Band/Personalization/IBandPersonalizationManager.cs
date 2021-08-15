// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.Personalization.IBandPersonalizationManager
// Assembly: Microsoft.Band, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 224A5D14-177F-4224-A6AA-76F665C6D960
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\netcore451\Microsoft.Band.dll

using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Band.Personalization
{
  public interface IBandPersonalizationManager
  {
    Task<BandImage> GetMeTileImageAsync();

    Task<BandImage> GetMeTileImageAsync(CancellationToken cancel);

    Task SetMeTileImageAsync(BandImage image);

    Task SetMeTileImageAsync(BandImage image, CancellationToken cancel);

    Task<BandTheme> GetThemeAsync();

    Task<BandTheme> GetThemeAsync(CancellationToken cancel);

    Task SetThemeAsync(BandTheme theme);

    Task SetThemeAsync(BandTheme theme, CancellationToken cancel);
  }
}
