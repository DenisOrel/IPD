
// Type: Intermech.Windows.Forms.IpsBaseUserControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Common;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Windows.Forms;

/// <summary>Базовый User Control</summary>
public class IpsBaseUserControl : 
  BaseUserControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IDesignModeControlsContainer,
  IArrowKeysNavigationSupported,
  ILastFocusedControlTracker,
  IContextAware,
  IControlServiceContainer,
  IAdvancedServiceContainer,
  IServiceContainer,
  System.IServiceProvider,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  /// <summary>Default constructor</summary>
  public IpsBaseUserControl() => this.AddService<IpsBaseUserControl>(this);

  /// <summary>Releases the unmanaged resources used by the Intermech.Windows.Forms.BaseUserControl and optionally releases the managed
  /// resources.</summary>
  /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.RemoveService<IpsBaseUserControl>();
    base.Dispose(disposing);
  }

  /// <summary>Чтение данных из FormStorage</summary>
  protected override bool LoadPropertiesFromStorage()
  {
    Dictionary<string, object> dic = new Dictionary<string, object>();
    bool flag = false;
    try
    {
      flag = FormStorage.LoadLayout((Control) this, this.ConfigName, (IDictionary) dic, true, out Point _, out Size _);
    }
    catch (Exception ex)
    {
    }
    if (flag)
      this.ParseDictionaryFromFormStorage(dic);
    return flag;
  }

  /// <summary>Сохранение данных в FormStorage</summary>
  protected override void SavePropertiesToStorage()
  {
    Dictionary<string, object> dic = new Dictionary<string, object>();
    this.FillPropsDictionary(dic);
    FormStorage.SaveLayout((Control) this, this.ConfigName, (IDictionary) dic);
  }
}
