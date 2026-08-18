
// Type: Intermech.Navigator.Nodes.SavedObjectNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Nodes;

/// <summary>Идентификатор ноды сохранённого (напр. в итерации, возможно отсутствующего в БД) объекта системы</summary>
/// <summary>Конструктор идентификатора ноды сохранённого (напр. в итерации, возможно отсутствующего в БД) объекта</summary>
/// <param name="e">Структура с параметрами для создания идентификатора ноды</param>
public class SavedObjectNodeID([NotNull] CreateObjectNodeParams createObjectNodeParams) : 
  ObjectNodeID(Intermech.Diagnostics.Check.ArgumentNotNull<CreateObjectNodeParams>(createObjectNodeParams, nameof (createObjectNodeParams))),
  INodeID,
  IRelatedObjectNodeID,
  IObjectNodeID,
  ISavedObjectNodeID
{
  private bool? _objectExistInDB;
  private bool? _relationExistInDB;

  public bool ObjectExistInDB
  {
    get
    {
      this._objectExistInDB = new bool?(((int) this._objectExistInDB ?? (Session.Invoke<bool>((Session.SessionHandler<bool>) (session => !session.GetObjectInfo(this.ObjectVersionID).Empty)) ? 1 : 0)) != 0);
      return this._objectExistInDB.Value;
    }
  }

  public bool RelationExistInDB
  {
    get
    {
      bool? nullable = this._relationExistInDB;
      int num1;
      if (!nullable.HasValue)
      {
        bool? objectExistInDb = this._objectExistInDB;
        bool flag = false;
        num1 = objectExistInDb.GetValueOrDefault() == flag & objectExistInDb.HasValue ? 1 : 0;
      }
      else
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      int num2 = num1 != 0 ? 0 : (Session.Invoke<bool>((Session.SessionHandler<bool>) (session => session.GetRelation(this.ObjectVersionID, false) != null)) ? 1 : 0);
      this._relationExistInDB = nullable = new bool?(num2 != 0);
      nullable = nullable;
      return nullable.Value;
    }
  }
}
