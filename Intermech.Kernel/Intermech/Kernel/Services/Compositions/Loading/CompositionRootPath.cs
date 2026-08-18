// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Loading.CompositionRootPath
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Services.Compositions.Loading;

internal class CompositionRootPath
{
  public readonly long ObjectID;
  private readonly CompositionRootPath _parentPath;

  public CompositionRootPath(long objectId, CompositionRootPath parent = null)
  {
    this.ObjectID = objectId;
    this._parentPath = parent;
  }

  public bool Contains(long objectId, bool parentOnly = false)
  {
    if (!parentOnly && this.ObjectID == objectId)
      return true;
    return this._parentPath != null && this._parentPath.Contains(objectId);
  }
}
