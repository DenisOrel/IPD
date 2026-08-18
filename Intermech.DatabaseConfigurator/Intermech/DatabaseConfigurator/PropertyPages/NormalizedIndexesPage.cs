// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.NormalizedIndexesPage
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.DatabaseConfigurator.Utils;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class NormalizedIndexesPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private System.IServiceProvider _provider;
  private ClassWrapperForPropertyGrid _object;
  private NormalizedIndexesPage.NormIndexesClass _normIndexesClass;

  public NormalizedIndexesPage(System.IServiceProvider provider)
  {
    this._provider = provider;
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("DatabaseConfigurator_81"), (IPropertyPage) this);
  }

  public string HelpTopicID => "1064";

  public object Control
  {
    get
    {
      if (this._object == null)
      {
        this._normIndexesClass = new NormalizedIndexesPage.NormIndexesClass(this._provider);
        this._object = new ClassWrapperForPropertyGrid((object) this._normIndexesClass);
      }
      return (object) this._object;
    }
  }

  public void Apply()
  {
    if (this._normIndexesClass == null || !this._normIndexesClass.IsChanged())
      return;
    this._normIndexesClass.ApplyUpdates();
    this._object.ResetOldValues();
  }

  public void Cancel()
  {
    if (this._normIndexesClass == null)
      return;
    this._normIndexesClass._inited = false;
  }

  public PropertyPageType Type => PropertyPageType.Object;

  public string PageName => LocalizationHolder.rm.GetString("DatabaseConfigurator_82");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  public event EventHandler Changed;

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  private class NormIndexesClass
  {
    private System.IServiceProvider _provider;
    private bool _delSpaces;
    private bool _upperCase;
    private bool _normalizeSimilar;
    internal bool _inited;
    private NormalizedIndexesPage.NormIndexesClass _clone;

    internal bool IsChanged()
    {
      return this._delSpaces != this._clone._delSpaces || this._upperCase != this._clone._upperCase || this._normalizeSimilar != this._clone._normalizeSimilar;
    }

    internal void ApplyUpdates()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
        service.WriteBool("KERNEL", "INDEX_PARAMS", "DEL_SPACES", this._delSpaces, 0L);
        service.WriteBool("KERNEL", "INDEX_PARAMS", "UPPER_CASE", this._upperCase, 0L);
        service.WriteBool("KERNEL", "INDEX_PARAMS", "CYRILLIC", this._normalizeSimilar, 0L);
        (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).ReloadIndexSettings();
        if (!IndexRebuilder.Indexing)
        {
          if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_83"), LocalizationHolder.rm.GetString("DatabaseConfigurator_84"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation) != DialogResult.Cancel)
          {
            IndexRebuilder task = new IndexRebuilder();
            ((IBackgroundTaskView) ServicesManager.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) task);
            new Thread(new ThreadStart(task.RebuildIndex)).Start();
          }
        }
      }
      this._inited = false;
    }

    private void CheckInited()
    {
      if (this._inited)
        return;
      IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
      this._delSpaces = service.ReadBool("KERNEL", "INDEX_PARAMS", "DEL_SPACES", true, DBConfigMode.GlobalOnly);
      this._upperCase = service.ReadBool("KERNEL", "INDEX_PARAMS", "UPPER_CASE", true, DBConfigMode.GlobalOnly);
      this._normalizeSimilar = service.ReadBool("KERNEL", "INDEX_PARAMS", "CYRILLIC", true, DBConfigMode.GlobalOnly);
      this._clone = (NormalizedIndexesPage.NormIndexesClass) this.MemberwiseClone();
      this._inited = true;
    }

    public NormIndexesClass(System.IServiceProvider provider)
    {
      this._inited = false;
      this._provider = provider;
    }

    [CustomDescription("Attribute.DatabaseConfigurator_1")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_2")]
    [TypeConverter(typeof (YesNoBooleanConverter))]
    public bool DelSpaces
    {
      get
      {
        this.CheckInited();
        return this._delSpaces;
      }
      set => this._delSpaces = value;
    }

    [CustomDescription("Attribute.DatabaseConfigurator_3")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_4")]
    [TypeConverter(typeof (YesNoBooleanConverter))]
    public bool UpperCase
    {
      get
      {
        this.CheckInited();
        return this._upperCase;
      }
      set => this._upperCase = value;
    }

    [CustomDescription("Attribute.DatabaseConfigurator_5")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_6")]
    [TypeConverter(typeof (YesNoBooleanConverter))]
    public bool NormalizeSimilar
    {
      get
      {
        this.CheckInited();
        return this._normalizeSimilar;
      }
      set => this._normalizeSimilar = value;
    }
  }
}
