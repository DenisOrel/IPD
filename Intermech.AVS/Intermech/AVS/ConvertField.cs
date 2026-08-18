// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ConvertField
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс, описывающий параметры конвертации старых полей AVS </summary>
internal class ConvertField
{
  private string _oldCaption;
  private int _newAttributeID = -1;
  /// <summary> Словарь, где ключом выступает идентификатор типа связи, а значением - действие, которое необходимо произвести с атрибутом </summary>
  private HybridDictionary _relationTypeActions = new HybridDictionary();
  /// <summary> Словарь, где ключом выступает идентификатор типа объекта, а значением - действие, которое необходимо произвести с атрибутом </summary>
  private HybridDictionary _objectTypeActions = new HybridDictionary();

  /// <summary> Конструктор </summary>
  public ConvertField(OldAVSField oldAVSField)
  {
    this._oldCaption = oldAVSField.FieldCaption;
    this._newAttributeID = oldAVSField.AttributeID;
  }

  /// <summary> Получить класс, описывающий, куда импортировать поле старого AVS применительно с некоторой записи спецификации </summary>
  public ConvertFullData GetConvertFullDataForRecord(int relationTypeID, int objectTypeID)
  {
    bool flag1 = false;
    ConvertAction сonvertAction1 = ConvertAction.None;
    if (this._relationTypeActions.Contains((object) relationTypeID))
    {
      сonvertAction1 = (ConvertAction) this._relationTypeActions[(object) relationTypeID];
      flag1 = true;
    }
    if (сonvertAction1 == ConvertAction.None)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationType relationType = sessionKeeper.Session.GetRelationType(relationTypeID);
        if (relationType != null)
        {
          IDBAttribute4TypeCollection attributes = relationType.Attributes;
          сonvertAction1 = attributes == null || attributes.GetAttributeByID(this.NewAttributeID) == null ? ConvertAction.None : ConvertAction.Write;
        }
      }
      if (!flag1)
        this._relationTypeActions[(object) relationTypeID] = (object) сonvertAction1;
    }
    if (сonvertAction1 != ConvertAction.None)
      return new ConvertFullData(ConvertTarget.ToRelationAttribute, сonvertAction1);
    bool flag2 = false;
    ConvertAction сonvertAction2 = ConvertAction.None;
    if (this._objectTypeActions.Contains((object) objectTypeID))
    {
      сonvertAction2 = (ConvertAction) this._objectTypeActions[(object) objectTypeID];
      flag2 = true;
    }
    if (сonvertAction2 == ConvertAction.None)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(objectTypeID);
        if (objectType != null)
        {
          IDBAttribute4TypeCollection attributes = objectType.Attributes;
          сonvertAction2 = attributes == null || attributes.GetAttributeByID(this.NewAttributeID) == null ? ConvertAction.None : ConvertAction.Write;
        }
      }
      if (!flag2)
        this._objectTypeActions[(object) objectTypeID] = (object) сonvertAction2;
    }
    return сonvertAction2 != ConvertAction.None ? new ConvertFullData(ConvertTarget.ToObjectAttribute, сonvertAction2) : new ConvertFullData(ConvertTarget.ToDocumentField, ConvertAction.Write);
  }

  /// <summary> Заголоовк поля в старом AVS </summary>
  public string OldCaption => this._oldCaption;

  /// <summary> Идентификатор атрибута в IPS </summary>
  public int NewAttributeID => this._newAttributeID;
}
