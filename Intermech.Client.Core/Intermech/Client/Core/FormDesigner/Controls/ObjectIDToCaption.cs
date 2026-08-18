
// Type: Intermech.Client.Core.FormDesigner.Controls.ObjectIDToCaption
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Класс для отображения заголовка объекта.</summary>
internal class ObjectIDToCaption
{
  private string _caption = string.Empty;
  /// <summary>
  /// флаг обработки версии объектов по VersionID или объектов по ID
  /// </summary>
  protected bool objectVersionProcessed = true;

  /// <summary>ID объекта.</summary>
  public long ObjectID { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="objID">ID объекта для просмотра</param>
  public ObjectIDToCaption(long objID, bool _objectVersionProcessed = true)
  {
    this.ObjectID = objID;
    this.objectVersionProcessed = _objectVersionProcessed;
    IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
    QuickObjectInfo quickObjectInfo = this.objectVersionProcessed ? service.GetObjectInfo(objID) : service.GetObjectInfoByID(objID);
    if (!string.IsNullOrEmpty(quickObjectInfo.Caption))
      this._caption = quickObjectInfo.Caption;
    else
      this._caption = $"{LocalizationHolder.rm.GetString("Client.Core_1132")} №{objID}";
  }

  /// <summary>Вывод в строку.</summary>
  /// <returns>Строка со значением</returns>
  public override string ToString() => this._caption;
}
