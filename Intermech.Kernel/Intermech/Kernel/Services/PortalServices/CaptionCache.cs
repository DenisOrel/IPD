// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.CaptionCache
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class CaptionCache
{
  private Dictionary<long, string> _cache;
  private bool _infoRequired;

  public CaptionCache(bool infoRequired)
  {
    if (infoRequired)
      this._cache = new Dictionary<long, string>();
    this._infoRequired = infoRequired;
  }

  public void Add(long objectID, int objectType, string caption)
  {
    if (!this._infoRequired || this._cache.ContainsKey(objectID))
      return;
    this._cache.Add(objectID, $"{MetaDataHelper.GetObjectName(objectType)} {caption}");
  }

  public string GetCaption(long objectID)
  {
    if (!this._infoRequired)
      return string.Empty;
    string str;
    return !this._cache.TryGetValue(objectID, out str) ? $"<Объект {objectID}>" : str;
  }
}
