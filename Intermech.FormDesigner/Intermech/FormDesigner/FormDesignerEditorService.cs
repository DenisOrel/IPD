// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.FormDesignerEditorService
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// Класс реализующий поддержку списка открытых редакторов.
/// </summary>
public class FormDesignerEditorService : IFormDesignerEditorService
{
  private Hashtable _hashtable = new Hashtable();

  /// <summary>Получить список форм, для которых открыты дизайнеры.</summary>
  /// <returns>Массив идентификаторов форм редактирования</returns>
  public long[] GetFormIDs() => this._hashtable.Keys.Cast<long>().ToArray<long>();

  /// <summary>Проверить на наличие открытого дизайнера для формы.</summary>
  /// <param name="formID">Идентификатор формы</param>
  /// <returns>True, если открыт дизайнер, False - если нет</returns>
  public bool Contains(long formID) => this._hashtable.ContainsKey((object) formID);

  /// <summary>Удалить дизайнер из списка дизайнеров.</summary>
  /// <param name="formID">Идентификатор формы, для которой открыт дизайнер</param>
  public void Remove(long formID) => this._hashtable.Remove((object) formID);

  /// <summary>Получить редактор для формы.</summary>
  /// <param name="formID">Идентификатор формы, для которой открыт дизайнер</param>
  /// <returns>Контрол-редактор, либо null если нет открытого дизайнера</returns>
  public Control GetEditorControl(long formID)
  {
    return !this._hashtable.ContainsKey((object) formID) ? (Control) null : this._hashtable[(object) formID] as Control;
  }

  /// <summary>Добавить новый дизайнер в список дизайнеров.</summary>
  /// <param name="formID">Идентификатор формы, для которой открыт дизайнер</param>
  /// <param name="editor">Контрол-редактор</param>
  public void Add(long formID, Control editor)
  {
    this._hashtable[(object) formID] = editor != null ? (object) editor : throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("FormDesigner_164"), (object) formID));
  }

  /// <summary>Очистить список открытых редакторов форм.</summary>
  public void Clear() => this._hashtable.Clear();

  /// <summary>
  /// 
  /// </summary>
  public event FormDesignerToolBoxUpdateEvent ToolBoxUpdateEvent;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="seneder"></param>
  /// <param name="items"></param>
  public void StoreToolBoxItems(object seneder, List<IMToolBoxItem> items)
  {
    if (this.ToolBoxUpdateEvent == null)
      return;
    this.ToolBoxUpdateEvent(seneder, new FormDesignerControlToolBoxUpdateEventArgs(items));
  }
}
