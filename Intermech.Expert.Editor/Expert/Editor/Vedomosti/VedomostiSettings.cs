// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Vedomosti.VedomostiSettings
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Expert.Editor.Vedomosti;

/// <summary> Настройки редактирования ведомостей </summary>
internal class VedomostiSettings : IPropertyPage
{
  public event EventHandler Changed;

  public PropertyPageType Type
  {
    get => throw new Exception("The method or operation is not implemented.");
  }

  public object Control => throw new Exception("The method or operation is not implemented.");

  public string PageName => throw new Exception("The method or operation is not implemented.");

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply() => throw new Exception("The method or operation is not implemented.");

  public void Cancel() => throw new Exception("The method or operation is not implemented.");

  /// <summary>вернуть id раздела в хелпе для данной страницы</summary>
  public string HelpTopicID
  {
    get => throw new Exception(LocalizationHolder.rm.GetString("Expert.Editor_594"));
  }
}
