// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IFormDesignerEditorService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс сервиса редактора форм</summary>
public interface IFormDesignerEditorService
{
  /// <summary>Получить список форм для которых открыты дизайнеры</summary>
  /// <returns>Массив идентификаторов форм редактирования</returns>
  long[] GetFormIDs();

  /// <summary>Получить редактор для формы</summary>
  /// <param name="formID">идентификатор формы для которой открыт дизайнер</param>
  /// <returns>контрол-редактор, либо null если нет открытого дизайнера</returns>
  Control GetEditorControl(long formID);

  /// <summary>Добавить новый дизайнер в список дизайнеров</summary>
  /// <param name="formID">идентификатор формы для которой открыт дизайнер</param>
  /// <param name="editor">контрол-редактор</param>
  void Add(long formID, Control editor);

  /// <summary>Удалить дизайнер из списка дизайнеров</summary>
  /// <param name="formID">идентификатор формы для которой открыт дизайнер</param>
  void Remove(long formID);

  /// <summary>Проверить на наличие открытого дизайнера для формы</summary>
  /// <param name="formID">идентификатор формы</param>
  /// <returns>True если открыт дизайнер, False - если нет</returns>
  bool Contains(long formID);

  /// <summary>Очистить список открытых редакторов форм</summary>
  void Clear();

  /// <summary>
  /// 
  /// </summary>
  event FormDesignerToolBoxUpdateEvent ToolBoxUpdateEvent;
}
