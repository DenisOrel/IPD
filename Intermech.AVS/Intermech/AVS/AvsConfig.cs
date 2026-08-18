// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsConfig
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.AVSConfig;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.AVS;

/// <summary>Общие настройки AVS</summary>
[Serializable]
public class AvsConfig : IPropertyPage, IPropertyPageSearchOptionEvents
{
  internal bool NoteFieldSettingsIsChanged;
  protected object wrapperForPropertyGrid;
  protected readonly IDictionary<string, object> uiproperties = (IDictionary<string, object>) new Dictionary<string, object>();
  public readonly PropertyDescriptorCollection PropertyDescriptions = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  private static AvsConfig_General general;
  private static AvsConfig_Podbor podbor;
  private static AvsConfig_PositionDesignation positionDesignation;
  private static AvsConfig_CheckSP checkSP;
  private static AvsConfig_CheckEL checkEL;
  protected readonly List<string> ChangedAdminProps = new List<string>();

  public AvsConfig()
  {
    ClassWrapperForPropertyGrid wrapperForPropertyGrid = new ClassWrapperForPropertyGrid((object) this);
    wrapperForPropertyGrid.GetReadOnly += new ClassWrapperForPropertyGrid.OnGetReadOnly(this.OnGetIsReadOnly);
    this.wrapperForPropertyGrid = (object) wrapperForPropertyGrid;
  }

  /// <summary> Единственный экземпляр объекта </summary>
  [Browsable(false)]
  public static AvsConfig_General General
  {
    [DebuggerStepThrough] get
    {
      if (AvsConfig.general == null)
        AvsConfig.general = new AvsConfig_General();
      return AvsConfig.general;
    }
  }

  /// <summary> Единственный экземпляр объекта </summary>
  [Browsable(false)]
  public static AvsConfig_Podbor Podbor
  {
    [DebuggerStepThrough] get
    {
      if (AvsConfig.podbor == null)
        AvsConfig.podbor = new AvsConfig_Podbor();
      return AvsConfig.podbor;
    }
  }

  /// <summary> Единственный экземпляр объекта </summary>
  [Browsable(false)]
  public static AvsConfig_PositionDesignation PositionDesignation
  {
    [DebuggerStepThrough] get
    {
      if (AvsConfig.positionDesignation == null)
        AvsConfig.positionDesignation = new AvsConfig_PositionDesignation();
      return AvsConfig.positionDesignation;
    }
  }

  /// <summary> Единственный экземпляр объекта </summary>
  [Browsable(false)]
  public static AvsConfig_CheckSP CheckSP
  {
    [DebuggerStepThrough] get
    {
      if (AvsConfig.checkSP == null)
        AvsConfig.checkSP = new AvsConfig_CheckSP();
      return AvsConfig.checkSP;
    }
  }

  /// <summary> Единственный экземпляр объекта </summary>
  [Browsable(false)]
  public static AvsConfig_CheckEL CheckEL
  {
    [DebuggerStepThrough] get
    {
      if (AvsConfig.checkEL == null)
        AvsConfig.checkEL = new AvsConfig_CheckEL();
      return AvsConfig.checkEL;
    }
  }

  [Browsable(false)]
  public static AvsSettings AvsSettings
  {
    [DebuggerStepThrough] get
    {
      return new AvsSettings()
      {
        General = AvsConfig.General.Model,
        Podbor = AvsConfig.Podbor.Model,
        PosDesignation = AvsConfig.PositionDesignation.Model,
        CheckSPec = AvsConfig.CheckSP.Model,
        CheckEList = AvsConfig.CheckEL.Model
      };
    }
    set
    {
      AvsConfig.General.Model = value?.General ?? AvsConfig.General.Model;
      AvsConfig.Podbor.Model = value?.Podbor ?? AvsConfig.Podbor.Model;
      AvsConfig.PositionDesignation.Model = value?.PosDesignation ?? AvsConfig.PositionDesignation.Model;
      AvsConfig.CheckSP.Model = value?.CheckSPec ?? AvsConfig.CheckSP.Model;
      AvsConfig.CheckEL.Model = value?.CheckEList ?? AvsConfig.CheckEL.Model;
    }
  }

  /// <summary>Сохранить настройки в конфиг клиента IPS</summary>
  /// <param name="config">Раздел AVS в конфигурации клиента IPS</param>
  public virtual void SaveToConfigurationManager(IConfiguration config)
  {
    if (config == null)
      throw new ArgumentNullException(nameof (config));
  }

  /// <summary>Загрузить настройки из конфига клиента IPS</summary>
  /// <param name="config">Раздел AVS в конфигурации клиента IPS</param>
  public static void LoadConfigs(IConfiguration config)
  {
    AvsConfig.General.LoadFromConfigurationManager(config);
    AvsConfig.Podbor.LoadFromConfigurationManager(config);
    AvsConfig.PositionDesignation.LoadFromConfigurationManager(config);
  }

  /// <summary>Загрузить настройки из конфига клиента IPS</summary>
  /// <param name="config">Раздел AVS в конфигурации клиента IPS</param>
  public virtual void LoadFromConfigurationManager(IConfiguration config)
  {
    if (config == null)
      throw new ArgumentNullException(nameof (config));
  }

  /// <summary>Загрузить настройки с сервера IPS</summary>
  public static bool LoadValuesFromServer()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAppSettingsService<AvsSettings> service = ServiceUtils.GetService<IAppSettingsService<AvsSettings>>((object) sessionKeeper.Session, true);
      if (service != null)
      {
        AvsSettings avsSettings = AvsConfig.AvsSettings;
        if (service.LoadSettings(sessionKeeper.Session.SessionGUID, ref avsSettings))
        {
          AvsConfig.AvsSettings = avsSettings;
          return true;
        }
        AvsConfig.SynchronizeDuplicateSettingValues();
      }
      return false;
    }
  }

  /// <summary>Cинхронизация дублирующихся настроек.</summary>
  private static void SynchronizeDuplicateSettingValues()
  {
    if (AvsConfig.General.CheckSpecificationBeforeClose != AvsConfig.CheckSP.CheckSpecificationBeforeClose)
      AvsConfig.CheckSP.CheckSpecificationBeforeClose = AvsConfig.General.CheckSpecificationBeforeClose;
    if (AvsConfig.General.CheckElementListBeforeClose == AvsConfig.CheckEL.CheckElementListBeforeClose)
      return;
    AvsConfig.CheckEL.CheckElementListBeforeClose = AvsConfig.General.CheckElementListBeforeClose;
  }

  /// <summary>Сохранить настройки на сервере IPS</summary>
  public static void SaveValuesToServer()
  {
    AvsSettings settings = AvsConfig.ExcludeNotChangedAdminSettings(AvsConfig.AvsSettings);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IAppSettingsService<AvsSettings>>((object) sessionKeeper.Session, true)?.SaveSettings(sessionKeeper.Session.SessionGUID, settings);
    AvsConfig.General.ChangedAdminProps.Clear();
    AvsConfig.Podbor.ChangedAdminProps.Clear();
    AvsConfig.PositionDesignation.ChangedAdminProps.Clear();
  }

  /// <summary>
  /// Исключить из списка админские параметры, у которых значения не менялись в данной сессии
  /// </summary>
  /// <param name="settingsToSave"></param>
  /// <returns></returns>
  private static AvsSettings ExcludeNotChangedAdminSettings(AvsSettings settingsToSave)
  {
    foreach (string name in settingsToSave.General.Names)
    {
      if (settingsToSave.General.IsAdmin(name) && !AvsConfig.General.ChangedAdminProps.Contains(name))
        settingsToSave.General.Exclude(name);
    }
    foreach (string name in settingsToSave.Podbor.Names)
    {
      if (settingsToSave.Podbor.IsAdmin(name) && !AvsConfig.Podbor.ChangedAdminProps.Contains(name))
        settingsToSave.Podbor.Exclude(name);
    }
    foreach (string name in settingsToSave.PosDesignation.Names)
    {
      if (settingsToSave.PosDesignation.IsAdmin(name) && !AvsConfig.PositionDesignation.ChangedAdminProps.Contains(name))
        settingsToSave.PosDesignation.Exclude(name);
    }
    return settingsToSave;
  }

  /// <summary>Вернуть id раздела в хелпе для данной страницы</summary>
  [Browsable(false)]
  public string HelpTopicID => "1080";

  public event EventHandler Changed;

  protected void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  [Browsable(false)]
  public PropertyPageType Type
  {
    [DebuggerStepThrough] get => PropertyPageType.Object;
  }

  [Browsable(false)]
  public object Control
  {
    [DebuggerStepThrough] get => this.wrapperForPropertyGrid;
  }

  [Browsable(false)]
  public virtual string PageName
  {
    [DebuggerStepThrough] get => "Редактор AVS";
  }

  /// <summary>Текст заголовка (пустое значение - заголовок не отображается)</summary>
  [Browsable(false)]
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public virtual void Apply()
  {
    foreach (string str in this.uiproperties.Keys.ToList<string>())
    {
      if (this.uiproperties[str] != null)
      {
        if (this.Model.IsAdmin(str))
          this.ChangedAdminProps.Add(str);
        this.Model[str] = this.uiproperties[str];
        this.uiproperties[str] = (object) null;
      }
    }
    this.OnChanged();
  }

  public virtual void Cancel()
  {
    foreach (string key in this.uiproperties.Keys.ToList<string>())
      this.uiproperties[key] = (object) null;
    this.NoteFieldSettingsIsChanged = false;
  }

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  protected virtual bool OnGetIsReadOnly(PropertyDescriptor prop)
  {
    bool flag = ClassWrapperForPropertyGrid.IsUserRoleAdmin();
    return this.Model.IsAdmin(prop.Name) && !flag;
  }

  [Browsable(false)]
  public AvsSettingsSection Model { get; set; }

  public virtual object this[string sname]
  {
    get
    {
      return !this.uiproperties.ContainsKey(sname) || this.uiproperties[sname] == null ? this.Model[sname] : this.uiproperties[sname];
    }
    set => this.uiproperties[sname] = value;
  }

  public virtual void SetModelValue(object value, [CallerMemberName] string propName = "")
  {
    if (this.Model.IsAdmin(propName) && !object.Equals(this.Model[propName], value))
      this.ChangedAdminProps.Add(propName);
    this.Model[propName] = value;
    this.uiproperties[propName] = (object) null;
  }

  public virtual object GetModelValue([CallerMemberName] string propName = "")
  {
    return this.Model[propName];
  }

  public void RegisterPropertyPageItem(
    string propertyName,
    System.Type propertyType,
    string displayName,
    string description,
    object defaultValue = null,
    System.Type converterType = null,
    bool isAdmin = false)
  {
    AVSConfigPropertyDescription propertyDescription = new AVSConfigPropertyDescription(this, TypeDescriptor.CreateProperty(this.GetType(), propertyName, propertyType));
    description = isAdmin ? description + " (Только для администратора)." : description;
    propertyDescription.AddAttribute((Attribute) new DisplayNameAttribute(displayName));
    propertyDescription.AddAttribute((Attribute) new DescriptionAttribute(description));
    propertyDescription.AddAttribute((Attribute) new ReadOnlyAttribute(isAdmin));
    if (converterType != (System.Type) null)
      propertyDescription.AddAttribute((Attribute) new TypeConverterAttribute(converterType));
    this.PropertyDescriptions.Add((PropertyDescriptor) propertyDescription);
    System.Type type = propertyType.IsEnum ? typeof (int) : propertyType;
    object obj = !propertyType.IsEnum || defaultValue == null ? defaultValue : (object) (int) defaultValue;
    this.Model[propertyName] = (object) new SettingData(obj, type, isAdmin);
    this.uiproperties[propertyName] = (object) null;
  }
}
