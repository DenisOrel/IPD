
// Type: Intermech.Windows.Forms.IpsBaseDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Common;
using Intermech.Diagnostics;
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

/// <summary>База для диалога</summary>
public class IpsBaseDialog : 
  BaseDialog,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
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
  public IpsBaseDialog() => this.AddService<IpsBaseDialog>(this);

  public IpsBaseDialog([CanBeNull] string contextName, [CanBeNull] Form centerOnForm = null)
    : this(centerOnForm, contextName: contextName)
  {
  }

  public IpsBaseDialog([CanBeNull] System.IServiceProvider ownerServices, [CanBeNull] string contextName = null)
    : this((Form) null, ownerServices, contextName)
  {
  }

  public IpsBaseDialog([CanBeNull] Form centerOnForm, [CanBeNull] System.IServiceProvider ownerServices = null, [CanBeNull] string contextName = null)
    : base(centerOnForm, ownerServices, contextName)
  {
    this.AddService<IpsBaseDialog>(this);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.RemoveService<IpsBaseDialog>();
    base.Dispose(disposing);
  }

  /// <summary>Чтение данных из FormStorage</summary>
  public override bool LoadPropertiesFromStorage()
  {
    Dictionary<string, object> dic = new Dictionary<string, object>();
    Point lLocation = Point.Empty;
    Size lSize = Size.Empty;
    bool flag = false;
    try
    {
      flag = FormStorage.LoadLayout((Control) this, this.ConfigName, (IDictionary) dic, true, out lLocation, out lSize);
    }
    catch
    {
    }
    if (flag)
    {
      this.Location = lLocation;
      if (this.FormBorderStyle == FormBorderStyle.Sizable || this.FormBorderStyle == FormBorderStyle.SizableToolWindow)
        this.Size = lSize;
      this.ParseDictionaryFromFormStorage(dic);
    }
    return flag;
  }

  /// <summary>Сохранение данных в FormStorage</summary>
  public override void SavePropertiesToStorage()
  {
    Dictionary<string, object> dic = new Dictionary<string, object>();
    this.FillPropsDictionary(dic);
    FormStorage.SaveLayout((Control) this, this.ConfigName, (IDictionary) dic);
  }
}
