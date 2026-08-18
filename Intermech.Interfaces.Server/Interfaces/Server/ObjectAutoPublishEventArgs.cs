// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ObjectAutoPublishEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;

#nullable disable
namespace Intermech.Interfaces.Server;

public class ObjectAutoPublishEventArgs
{
  public IPublisher Publisher;
  public string TaskName;
  public bool Handled;
  public long ObjectID;
  public int ObjectType;
  public TaskPriority Priority;
  public IUserSession Session;

  public ObjectAutoPublishEventArgs(IUserSession session, long objectID, int objectType)
  {
    this.ObjectID = objectID;
    this.ObjectType = objectType;
    this.Session = session;
  }
}
