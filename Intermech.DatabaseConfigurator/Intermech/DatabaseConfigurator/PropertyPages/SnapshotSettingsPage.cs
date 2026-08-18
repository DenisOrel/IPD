// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.SnapshotSettingsPage
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Snapshots;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

internal class SnapshotSettingsPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private IServiceProvider _serviceProvider;
  private ClassWrapperForPropertyGrid _wrapper;
  private SnapshotSettingsPage.CurrentSnapshotSettings _snapsSettings;

  public SnapshotSettingsPage(IServiceProvider serviceProvider)
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin)
      return;
    this._serviceProvider = serviceProvider;
    ((IPropertyPagesService) this._serviceProvider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("DatabaseConfigurator_267"), (IPropertyPage) this);
    this._snapsSettings = new SnapshotSettingsPage.CurrentSnapshotSettings();
    this._wrapper = new ClassWrapperForPropertyGrid((object) this._snapsSettings);
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Object;

  public object Control => (object) this._wrapper;

  public string PageName => LocalizationHolder.rm.GetString("Client.Core_1677");

  public void Apply()
  {
    if (this._snapsSettings == null)
      return;
    this._snapsSettings.ApplyUpdates();
    this._wrapper.ResetOldValues();
  }

  public void Cancel()
  {
    if (this._snapsSettings == null)
      return;
    this._snapsSettings._inited = false;
  }

  public string HelpTopicID => string.Empty;

  public string HeaderText => LocalizationHolder.rm.GetString("Client.Core_1678");

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  internal class CurrentSnapshotSettings
  {
    internal bool _inited;
    private bool _modified;
    private int _maxIterationPerObjectVersion;
    private int _maxIterationLifeTime;
    private LevelPropertyClass _truncateLevel;

    public CurrentSnapshotSettings()
    {
      this._inited = false;
      this.LoadCurrentValues();
    }

    private void CheckInited()
    {
      if (this._inited)
        return;
      this.LoadCurrentValues();
      this._inited = true;
    }

    private void LoadCurrentValues()
    {
      SnapshotSettings snapshotSettings = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISnapshotService)) as ISnapshotService).GetSnapshotSettings();
      this._maxIterationLifeTime = snapshotSettings.IterationLifetime;
      this._maxIterationPerObjectVersion = snapshotSettings.MaxIterationsPerObject;
      this._truncateLevel = new LevelPropertyClass(snapshotSettings.TruncateLevel);
      this._modified = false;
    }

    public void ApplyUpdates()
    {
      if (!this._modified)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        SnapshotSettings settings = new SnapshotSettings(this._maxIterationPerObjectVersion, this._maxIterationLifeTime, this._truncateLevel.Level);
        (sessionKeeper.Session.GetCustomService(typeof (ISnapshotService)) as ISnapshotService).SetSnapshotSettings(sessionKeeper.Session.SessionGUID, settings);
        this._modified = false;
      }
    }

    [CustomDisplayName("Attribute.DatabaseConfigurator_30")]
    [CustomDescription("Attribute.DatabaseConfigurator_33")]
    [TypeConverter(typeof (IntZeroEmptyStringConverter))]
    public int MaxIterationLifeTime
    {
      get
      {
        this.CheckInited();
        return this._maxIterationLifeTime;
      }
      set
      {
        this._maxIterationLifeTime = value;
        this._modified = true;
      }
    }

    [CustomDisplayName("Attribute.DatabaseConfigurator_31")]
    [CustomDescription("Attribute.DatabaseConfigurator_34")]
    [TypeConverter(typeof (IntZeroEmptyStringConverter))]
    public int MaxIterationPerObjectVersion
    {
      get
      {
        this.CheckInited();
        return this._maxIterationPerObjectVersion;
      }
      set
      {
        this._maxIterationPerObjectVersion = value;
        this._modified = true;
      }
    }

    [CustomDisplayName("Attribute.DatabaseConfigurator_32")]
    [CustomDescription("Attribute.DatabaseConfigurator_35")]
    [TypeConverter(typeof (LevelConverterWithEmptyString))]
    public LevelPropertyClass TruncateLevel
    {
      get
      {
        this.CheckInited();
        return this._truncateLevel;
      }
      set
      {
        this._truncateLevel = value;
        this._modified = true;
      }
    }
  }
}
