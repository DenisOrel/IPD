// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImEventObjLCStepData
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImEventObjLCStepData : ImEventObjData
{
  private IDBLifecycleStep _lcStep;

  public IDBLifecycleStep LcStep => this._lcStep;

  public ImEventObjLCStepData(IDBObject dbObject, IDBLifecycleStep lcStep, ImEventType eventType)
    : base(dbObject, eventType)
  {
    this._lcStep = lcStep;
  }
}
