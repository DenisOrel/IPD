// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.TechCardParams.TechCardParamsEditor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Settings.TechCardParams;

/// <summary>Контрол редактирования параметров техкарда</summary>
public class TechCardParamsEditor : 
  IPropertyPage,
  ISortedPropertyGrid,
  IPropertyPageSearchOptionEvents
{
  /// <summary>
  /// 
  /// </summary>
  private readonly string _pageName;
  /// <summary>Класс для отображения настроек TechCard на закладке</summary>
  private TechCardParamsDescriptor _objectDescriptor;

  /// <summary>Constructor</summary>
  public TechCardParamsEditor()
  {
    this._pageName = LocalizationHolder.rm.GetString("TechCard.Client_363");
  }

  /// <summary>Отменить изменения</summary>
  public void Cancel()
  {
    TechCardParamsHelper.LoadValues();
    this._objectDescriptor = new TechCardParamsDescriptor(TechCardParamsHelper.TechParams);
  }

  /// <summary>Контрол для вставки</summary>
  public object Control
  {
    get
    {
      if (this._objectDescriptor == null)
        this._objectDescriptor = new TechCardParamsDescriptor(TechCardParamsHelper.TechParams);
      return (object) this._objectDescriptor;
    }
  }

  /// <summary>Принять изменения</summary>
  public void Apply()
  {
    TechCardParamsHelper.SaveValues();
    if (this._objectDescriptor != null)
      this._objectDescriptor.ResetOldValues();
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  /// <summary>Тип возвращаемого объекта</summary>
  public PropertyPageType Type => PropertyPageType.Object;

  /// <summary>Заголовок</summary>
  public string PageName => this._pageName;

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  /// <summary>Событие на изменение</summary>
  public event EventHandler Changed;

  /// <summary>вернуть id раздела в хелпе для данной страницы</summary>
  public string HelpTopicID => "1443";

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public PropertySort Sort => PropertySort.Categorized;

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is TechCardParamsDescriptor control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  /// <summary>
  /// 
  /// </summary>
  internal static void RegisterSettingsPage()
  {
    ServiceUtils.GetService<IPropertyPagesService>((object) ApplicationServices.Container, false)?.AddPage(LocalizationHolder.rm.GetString("TechCard.Client_362"), (IPropertyPage) new TechCardParamsEditor());
  }
}
