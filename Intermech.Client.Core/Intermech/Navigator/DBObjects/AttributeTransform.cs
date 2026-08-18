
// Type: Intermech.Navigator.DBObjects.AttributeTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Базовый класс для преобразователя, который работает с произвольными атрибутами,
/// которому на "лету" можно указывать ID типа обрабатываемого атрибута
/// </summary>
public class AttributeTransform : IAttributeTransform
{
  /// <summary>Внутреннее поле для синхронизации</summary>
  protected object _syncRoot = new object();
  /// <summary>
  /// Идентификатор атрибута, по которому идёт преобразование
  /// </summary>
  public int _attrID = -1;
  /// <summary>
  /// Метаданные атрибута (включая список его допустимых значений)
  /// </summary>
  public MyAttributeMetadata _attrMetadata = new MyAttributeMetadata();

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="AnAttrID">Идентификатор атрибута, по которому идёт преобразование</param>
  public AttributeTransform(int AnAttrID) => this.AttrID = AnAttrID;

  /// <summary>Поле для синхронизации</summary>
  public object SyncRoot => this._syncRoot;

  /// <summary>Идентификатор атрибута</summary>
  public int AttrID
  {
    get => this._attrID;
    set
    {
      if (this._attrID == value)
        return;
      lock (this._syncRoot)
      {
        this._attrID = value;
        this._attrMetadata.Clear();
        this._attrMetadata.SetByID(this._attrID);
      }
    }
  }
}
