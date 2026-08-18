// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ObjectImportedEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ObjectImportedEventArgs : SessionableEventArgs
{
  public IDBObject ImportedObject;
  public long BaseVersionID;
  public SystemTypes SystemType;
  public bool IsNew;
  public bool Handled;

  public ObjectImportedEventArgs(IUserSession session, IDBObject importedObject)
    : this(session, importedObject, 0L, SystemTypes.Unknown, false)
  {
  }

  public ObjectImportedEventArgs(
    IUserSession session,
    IDBObject importedObject,
    long baseVersionID,
    SystemTypes systemType,
    bool isNew)
    : base(session)
  {
    this.ImportedObject = importedObject;
    this.BaseVersionID = baseVersionID;
    this.SystemType = systemType;
    this.IsNew = isNew;
  }
}
