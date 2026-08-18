// Decompiled with JetBrains decompiler
// Type: Intermech.Project.DBObjectTask
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Project;

public class DBObjectTask : Task
{
  public DBObjectTask([CanBeNull] IDBProjectTask obj)
  {
    this._Object = (IDBObject) obj;
    this._Partial = true;
    this.Load((IDBObject) obj, new bool?(false));
  }

  public override IDBObject GetObject(bool throwNotFoundException) => this._Object;

  public override void ReleaseObject()
  {
  }

  public override IUserSession GetSession() => this._Object.Session;

  public override bool ReleaseSession() => true;
}
