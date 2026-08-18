// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.BackgroundTask.ReplaceAttributeBackgroundTask
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;

#nullable disable
namespace Intermech.Imbase.BackgroundTask;

internal sealed class ReplaceAttributeBackgroundTask : BaseBackgroundTask
{
  public ReplaceAttributeBackgroundTask(IServiceForBackgroundTask srv)
    : base(srv)
  {
    this._imageIndex = ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service ? service.ImageIndex("imgRefresh") : -1;
    this._canResume = true;
    this._canPause = true;
    this._canStop = true;
    this._canTerminate = true;
  }
}
