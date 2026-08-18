// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionPairObjectCreator
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;

#nullable disable
namespace Intermech.CompositionTracking.Server;

internal class CompositionPairObjectCreator : PairedObjectsCreator
{
  private readonly ObjectEventHandler _objectEventHandler;

  public CompositionPairObjectCreator(ObjectEventHandler objectEventHandler)
  {
    this._objectEventHandler = objectEventHandler;
  }

  protected override void OnAfterCreateObject(IDBObject newObject, IDBObject prototype)
  {
    base.OnAfterCreateObject(newObject, prototype);
    if (newObject.ParentVersionID == -1L || this._objectEventHandler == null)
      return;
    this._objectEventHandler(newObject, newObject.Session);
  }
}
