// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes.ProcRouteClass
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;

/// <summary>Internal class for proc route's element</summary>
internal class ProcRouteClass
{
  private long _objectId;
  private readonly CehRoutesClassList _cehRouteList;

  /// <summary>Initialize class data</summary>
  private void InitData()
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="objectId"></param>
  public ProcRouteClass(long objectId)
  {
    this._cehRouteList = new CehRoutesClassList((CustomTechClass) null);
    this._objectId = objectId;
    this.InitData();
  }

  /// <summary>Object id</summary>
  public long ObjectId
  {
    get => this._objectId;
    set => this._objectId = value;
  }

  /// <summary>Ceh elem's list</summary>
  public CehRoutesClassList CehRouteList => this._cehRouteList;
}
