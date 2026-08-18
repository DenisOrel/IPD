// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Settings.ArtsCompositionParamsEditor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Settings;

internal class ArtsCompositionParamsEditor : 
  ISortedPropertyGrid,
  IPropertyPageSearchOptionEvents,
  IPropertyPage
{
  /// <summary>
  /// 
  /// </summary>
  private IArtsCompositionParams _params;
  /// <summary>
  /// 
  /// </summary>
  private IArtsCompositionParamsService _paramsService;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeData()
  {
    this._paramsService = ServiceUtils.GetService<IArtsCompositionParamsService>((object) ApplicationServices.Container, false);
  }

  /// <summary>Событие на изменения</summary>
  /// <param name="sender">вызвавший объект</param>
  /// <param name="e">параметры</param>
  private void OnChanged(object sender, EventArgs e)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  /// <summary>
  /// 
  /// </summary>
  private ArtsCompositionParamsEditor()
  {
    this.InitializeData();
    this.PageName = LocalizationHolder.rm.GetString("TechCard.Client_534");
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public PropertySort Sort => PropertySort.Categorized;

  /// <summary>Отменить изменения</summary>
  public void Cancel() => this._paramsService?.LoadSettings(out this._params);

  /// <summary>Control для настроек</summary>
  public object Control
  {
    get
    {
      if (this._params == null)
        this.Cancel();
      return (object) new ClassWrapperForPropertyGrid((object) new ArtsCompositionParamsWrapper(this._params ?? (IArtsCompositionParams) new ArtsCompositionParams()));
    }
  }

  /// <summary>Принять изменения</summary>
  public void Apply()
  {
    if (this._paramsService == null || this._params == null)
      return;
    this._paramsService.SaveSettings(this._params);
  }

  /// <summary>Тип возвращаемого объекта</summary>
  public PropertyPageType Type => PropertyPageType.Object;

  /// <summary>Заголовок</summary>
  public string PageName { get; }

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  /// <summary>Событие на изменение</summary>
  public event EventHandler Changed;

  /// <summary>вернуть id раздела в справке для данной страницы</summary>
  public string HelpTopicID => string.Empty;

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  /// <summary>Регистрации закладки в сервисах</summary>
  internal static void RegisterSettingsPage()
  {
    ServiceUtils.GetService<IPropertyPagesService>((object) ApplicationServices.Container, false)?.AddPage(LocalizationHolder.rm.GetString("TechCard.Client_533"), (IPropertyPage) new ArtsCompositionParamsEditor());
  }
}
