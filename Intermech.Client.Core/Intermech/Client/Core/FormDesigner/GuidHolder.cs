
// Type: Intermech.Client.Core.FormDesigner.GuidHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>
/// Статический класс для хранения глобальных идентификаторов.
/// </summary>
public class GuidHolder
{
  /// <summary>
  /// Глобальный идентификатор для типа "Форма ввода информации".
  /// </summary>
  public static Guid FormsTypeGuid = new Guid("cad0011b-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор для атрибута "Глобальный идентификатор типа объекта".
  /// </summary>
  public static Guid GlobalObjGuid = new Guid("cad00149-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор для атрибута "Глобальный идентификатор типа связи".
  /// </summary>
  public static Guid GlobalRelGuid = new Guid("cad0014a-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор для атрибута "Список форм редактирования".
  /// </summary>
  public static Guid FormListGuid = new Guid("cad0019d-306c-11d8-b4e9-00304f19f545");
  /// <summary>Глобальный идентификатор для атрибута "Условие".</summary>
  public static Guid ConditionAttrGuid = new Guid("cad00064-306c-11d8-b4e9-00304f19f545");
}
