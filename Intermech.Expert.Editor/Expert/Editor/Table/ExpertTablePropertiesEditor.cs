// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.ExpertTablePropertiesEditor
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Expert.Editor.Table;

/// <summary>Редактор свойств</summary>
public class ExpertTablePropertiesEditor : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private IExpertTableProperties _control;
  private IExpertTablePropertiesService _service;

  /// <summary>Конструктор</summary>
  public ExpertTablePropertiesEditor()
  {
    if (ServicesManager.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service)
    {
      service.AddPage(LocalizationHolder.rm.GetString("Expert.Editor_17"), (IPropertyPage) this);
      this._service = ServicesManager.GetService(typeof (IExpertTablePropertiesService)) as IExpertTablePropertiesService;
    }
    this.Cancel();
  }

  private void OnChanged(object sender, EventArgs e)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler Changed;

  /// <summary>
  /// 
  /// </summary>
  public PropertyPageType Type => PropertyPageType.Object;

  /// <summary>
  /// 
  /// </summary>
  public object Control
  {
    get
    {
      return this._control != null ? (object) new ClassWrapperForPropertyGrid((object) this._control) : (object) null;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public string PageName => LocalizationHolder.rm.GetString("Expert.Editor_18");

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  /// <summary>
  /// 
  /// </summary>
  public void Apply()
  {
    if (this._service == null)
      return;
    this._service.Current = this._control;
  }

  /// <summary>
  /// 
  /// </summary>
  public void Cancel()
  {
    if (this._service == null)
      return;
    this._control = (IExpertTableProperties) null;
    this._control = this._service.Current;
    if (this._control == null)
      return;
    this._control.Changed += new EventHandler(this.OnChanged);
  }

  /// <summary>вернуть id раздела в хелпе для данной страницы</summary>
  public string HelpTopicID => "1082";

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }
}
