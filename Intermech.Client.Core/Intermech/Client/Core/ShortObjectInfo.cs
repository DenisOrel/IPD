
// Type: Intermech.Client.Core.ShortObjectInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core;

/// <summary>Краткая информация об объектк</summary>
public class ShortObjectInfo
{
  /// <summary>Значение элемента</summary>
  public long ObjectId;
  /// <summary>Текстовое описание элемента</summary>
  public string Caption = string.Empty;
  /// <summary>Какие-то пользовательские данные</summary>
  public int ObjectTypeId = -1;

  public ShortObjectInfo(long objectId, string caption, int objectTypeId)
  {
    this.ObjectId = objectId;
    this.Caption = caption;
    this.ObjectTypeId = objectTypeId;
  }
}
