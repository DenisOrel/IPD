// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OldAVSField
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс, сопоставлящий заголовок старого поля AVS с системой атрибутов AVS </summary>
internal class OldAVSField
{
  private static HybridDictionary _checkedOldAVSFieldCaptions = new HybridDictionary();
  private int _attributeID = -1;
  private string _fieldCaption = string.Empty;
  private ConvertField _convertField;

  /// <summary> Конструктор </summary>
  protected OldAVSField(string fieldCaption, int attributeID)
  {
    this._attributeID = attributeID;
    this._fieldCaption = fieldCaption;
  }

  /// <summary> Статический поиск атрибута в IPS по заголовку поля AVS </summary>
  public static OldAVSField GetFieldByCaption(string fieldCaption)
  {
    if (OldAVSField._checkedOldAVSFieldCaptions.Contains((object) fieldCaption))
      return (OldAVSField) OldAVSField._checkedOldAVSFieldCaptions[(object) fieldCaption];
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(fieldCaption, false);
    return attributeType == null ? (OldAVSField) null : new OldAVSField(fieldCaption, attributeType.AttributeID);
  }

  /// <summary> Кэш уже обработаных заголовков полей. Ключём является заголовок, значением - идентификатор сопоставляемого ему атрибута IPS или -1, если таковой не найден </summary>
  public static HybridDictionary CheckedOldAVSFieldCaptions
  {
    get => OldAVSField._checkedOldAVSFieldCaptions;
  }

  /// <summary> Идентификатор сопоставляемого атрибута </summary>
  public int AttributeID => this._attributeID;

  /// <summary> Заоловок поля </summary>
  public string FieldCaption => this._fieldCaption;

  public ConvertField ConvertField
  {
    get
    {
      if (this._convertField == null)
        this._convertField = new ConvertField(this);
      return this._convertField;
    }
  }
}
