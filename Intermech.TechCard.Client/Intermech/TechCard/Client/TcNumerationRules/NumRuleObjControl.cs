// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcNumerationRules.NumRuleObjControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using System.Diagnostics;

#nullable disable
namespace Intermech.TechCard.Client.TcNumerationRules;

/// <summary>
/// Базовый контрол для редактирования / просмотра параметров нумерации объекта
/// </summary>
public class NumRuleObjControl : NumRuleControl
{
  /// <summary>Ид. версии объекта правила нумерации</summary>
  protected long _objectID;

  /// <summary>Ид. версии объекта правила нумерации</summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this._objectID;
    set
    {
      if (this._objectID == value)
        return;
      this._objectID = value;
      this.DataLoad(value);
    }
  }

  /// <summary>Загрузка данных</summary>
  public override void DataLoad()
  {
    this.DataLoad(this._objectID);
    this.Modified = false;
  }

  /// <summary>Сохранение данных</summary>
  public override void DataSave()
  {
    this.DataSave(this._objectID);
    this.Modified = false;
  }

  /// <summary>Загрузка информации о элементе правила нумерации</summary>
  /// <param name="aObjectId"></param>
  public void DataLoad(long aObjectId)
  {
    if (aObjectId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(aObjectId);
      if (dbObject == null)
        return;
      if (this._numRule == null)
        this._numRule = new TechNumerationRule();
      this._numRule.Load(dbObject, sessionKeeper.Session);
      this.DataLoad(this._numRule);
    }
  }

  /// <summary>Сохранение информации о элементе правила нумерации</summary>
  /// <param name="aObjectId"></param>
  public void DataSave(long aObjectId)
  {
    if (aObjectId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(aObjectId);
      if (dbObject == null)
        return;
      if (this._numRule == null)
        this._numRule = new TechNumerationRule();
      this.DataSave(this._numRule);
      this._numRule.Save(dbObject, sessionKeeper.Session);
    }
  }
}
