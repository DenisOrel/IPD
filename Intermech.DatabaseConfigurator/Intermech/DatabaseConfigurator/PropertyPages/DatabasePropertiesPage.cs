// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.DatabasePropertiesPage
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class DatabasePropertiesPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private IServiceProvider _provider;
  private DatabasePropertiesPage.DatabaseProperties _props;
  private ClassWrapperForPropertyGrid _object;

  public DatabasePropertiesPage(IServiceProvider provider)
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin)
      return;
    this._provider = provider;
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("DataBaseSettings"), (IPropertyPage) this);
  }

  public string HelpTopicID => "1625";

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Object;

  public object Control
  {
    get
    {
      if (this._object == null)
      {
        this._props = new DatabasePropertiesPage.DatabaseProperties(this._provider);
        this._object = new ClassWrapperForPropertyGrid((object) this._props);
      }
      return (object) this._object;
    }
  }

  public string PageName => LocalizationHolder.rm.GetString("DatabaseConfigurator_224");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply()
  {
    if (this._props == null)
      return;
    this._props.ApplyUpdates();
    this._object.ResetOldValues();
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  public void Cancel()
  {
    if (this._props == null)
      return;
    this._props._inited = false;
  }

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  private class DatabaseProperties
  {
    protected bool _seqDes;
    private bool _enableEditOwnCondition;
    private bool _enabledPdmConfigurator = true;
    private bool _enabledSeriesDates;
    private bool _enabledVisibilityFiltration = true;
    private bool _enabledAutoSoftInstantiation;
    private bool _enabledDelayedAttrHistory = true;
    private bool _enabledDelayedEventlog;
    private int _maxTaskThreadsCount = 4;
    private bool _sendAttrs2DelayedNotifications;
    private bool _allVersionsAnnulmentMode = true;
    private bool _DisableDBPatch;
    private bool _EnableAttributeLCStepSecurity;
    private bool _SetProjectOnCreateRelation = true;
    private bool _CopyArchiveVisibility;
    private bool _CopyProjectVisibility;
    private bool _EnableArticlesAccessMode;
    private string _IndexTablespaceName = string.Empty;
    private bool _UpdateViewsList;
    internal bool _inited;
    private SearchInIndexSubstringGettingMode _searchInIndexSubstringGettingMode;
    private bool _useHiddenComposition = true;
    private IServiceProvider _serviceProvider;

    public DatabaseProperties(IServiceProvider serviceProvider)
    {
      this._serviceProvider = serviceProvider != null ? serviceProvider : throw new ArgumentNullException(nameof (serviceProvider));
    }

    internal void ApplyUpdates()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this._enableEditOwnCondition != sessionKeeper.Session.EnableEditOwnSelections)
          sessionKeeper.Session.EnableEditOwnSelections = this._enableEditOwnCondition;
        if (this._enabledPdmConfigurator != sessionKeeper.Session.EnabledPdmConfigurator)
        {
          if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service1)
            service1.EnabledPdmConfigurator = this._enabledPdmConfigurator;
          else
            sessionKeeper.Session.EnabledPdmConfigurator = this._enabledPdmConfigurator;
        }
        if (this._enabledSeriesDates != sessionKeeper.Session.EnabledSeriesDates)
          sessionKeeper.Session.EnabledSeriesDates = this._enabledSeriesDates;
        if (this._enabledVisibilityFiltration != sessionKeeper.Session.EnabledVisibilityFiltration)
          sessionKeeper.Session.EnabledVisibilityFiltration = this._enabledVisibilityFiltration;
        if (this._enabledAutoSoftInstantiation != sessionKeeper.Session.EnabledAutoSoftInstantiation)
          sessionKeeper.Session.EnabledAutoSoftInstantiation = this._enabledAutoSoftInstantiation;
        if (this._enabledDelayedEventlog != sessionKeeper.Session.IsDelayedEventlog)
          sessionKeeper.Session.IsDelayedEventlog = this._enabledDelayedEventlog;
        if (this._enabledDelayedAttrHistory != sessionKeeper.Session.IsDelayedAttrHistory)
          sessionKeeper.Session.IsDelayedAttrHistory = this._enabledDelayedAttrHistory;
        if (this._sendAttrs2DelayedNotifications != sessionKeeper.Session.SendAttrs2DelayedNotificationMode)
          sessionKeeper.Session.SendAttrs2DelayedNotificationMode = this._sendAttrs2DelayedNotifications;
        if (this._allVersionsAnnulmentMode != sessionKeeper.Session.AllVersionsAnnulmentMode)
          sessionKeeper.Session.AllVersionsAnnulmentMode = this._allVersionsAnnulmentMode;
        if (this._maxTaskThreadsCount != sessionKeeper.Session.MaxTaskThreadsCount)
          sessionKeeper.Session.MaxTaskThreadsCount = this._maxTaskThreadsCount;
        if (this._DisableDBPatch != (sessionKeeper.Session.Configurations.ReadInteger("KERNEL", "COMMON", "DisableDBPatch", 0L, DBConfigMode.GlobalOnly) == 1L))
          sessionKeeper.Session.Configurations.WriteInteger("KERNEL", "COMMON", "DisableDBPatch", this._DisableDBPatch ? 1L : 0L, 0L);
        bool flag = false;
        if (this._EnableAttributeLCStepSecurity != (sessionKeeper.Session.Configurations.ReadInteger("KERNEL", "SECURITY", "CHECK_ATTR_LCACCESS", 0L, DBConfigMode.GlobalOnly) != 0L))
        {
          sessionKeeper.Session.Configurations.WriteInteger("KERNEL", "SECURITY", "CHECK_ATTR_LCACCESS", this._EnableAttributeLCStepSecurity ? 1L : 0L, 0L);
          flag = true;
        }
        if (this._EnableArticlesAccessMode != sessionKeeper.Session.Configurations.ReadBool("ARCHIVES", "SECURITY", "ART_ACCESS", false, DBConfigMode.GlobalOnly))
        {
          sessionKeeper.Session.Configurations.WriteBool("ARCHIVES", "SECURITY", "ART_ACCESS", this._EnableArticlesAccessMode, 0L);
          flag = true;
        }
        if (this._SetProjectOnCreateRelation != sessionKeeper.Session.Configurations.ReadBool("KERNEL", "PROJECT", "SET_PROJ2CHILD", true, DBConfigMode.GlobalOnly))
        {
          sessionKeeper.Session.Configurations.WriteBool("KERNEL", "PROJECT", "SET_PROJ2CHILD", this._SetProjectOnCreateRelation, 0L);
          flag = true;
        }
        if (this._CopyArchiveVisibility != sessionKeeper.Session.Configurations.ReadBool("ARCHIVES", "SECURITY", "COPY_ARC_VISIBLE", false, DBConfigMode.GlobalOnly))
        {
          sessionKeeper.Session.Configurations.WriteBool("ARCHIVES", "SECURITY", "COPY_ARC_VISIBLE", this._CopyArchiveVisibility, 0L);
          flag = true;
        }
        if (this._CopyProjectVisibility != sessionKeeper.Session.Configurations.ReadBool("KERNEL", "SECURITY", "COPY_PROJ_VISIBLE", false, DBConfigMode.GlobalOnly))
        {
          sessionKeeper.Session.Configurations.WriteBool("KERNEL", "SECURITY", "COPY_PROJ_VISIBLE", this._CopyProjectVisibility, 0L);
          flag = true;
        }
        if (sessionKeeper.Session.Configurations.ReadString("KERNEL", "COMMON", "INDEX_TABLESPACE", string.Empty, DBConfigMode.GlobalOnly) != this._IndexTablespaceName)
        {
          sessionKeeper.Session.Configurations.WriteString("KERNEL", "COMMON", "INDEX_TABLESPACE", this._IndexTablespaceName, 0L);
          flag = true;
        }
        if (this._UpdateViewsList != sessionKeeper.Session.Configurations.ReadBool("KERNEL", "PERFORMANCE", "UPDATE_VIEWS_LIST", false, DBConfigMode.GlobalOnly))
        {
          sessionKeeper.Session.Configurations.WriteBool("KERNEL", "PERFORMANCE", "UPDATE_VIEWS_LIST", this._UpdateViewsList, 0L);
          flag = true;
        }
        if (this._useHiddenComposition != sessionKeeper.Session.Configurations.ReadBool("KERNEL", "PERFORMANCE", "UseHiddenComposition", true, DBConfigMode.GlobalOnly))
        {
          sessionKeeper.Session.Configurations.WriteBool("KERNEL", "PERFORMANCE", "UseHiddenComposition", this._useHiddenComposition, 0L);
          flag = true;
          if (this._serviceProvider.GetService(typeof (INotificationService)) is INotificationService service2)
            service2.FireEvent((object) this, (NotificationEventArgs) new ConfigurationOptionChangedEventArgs("KERNEL", "PERFORMANCE", "UseHiddenComposition", (object) this._useHiddenComposition));
        }
        if (flag)
          (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).ReloadServerSwitches(sessionKeeper.Session.SessionGUID);
      }
      SearchInIndexSubstringGettingModeHelper.SetSearchInIndexSubstringGettingMode(this._searchInIndexSubstringGettingMode);
    }

    internal void LoadCurrentValues()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        this._enableEditOwnCondition = session.EnableEditOwnSelections;
        this._enabledPdmConfigurator = session.EnabledPdmConfigurator;
        this._enabledSeriesDates = session.EnabledSeriesDates;
        this._enabledVisibilityFiltration = session.EnabledVisibilityFiltration;
        this._enabledAutoSoftInstantiation = session.EnabledAutoSoftInstantiation;
        this._enabledDelayedAttrHistory = session.IsDelayedAttrHistory;
        this._enabledDelayedEventlog = session.IsDelayedEventlog;
        this._maxTaskThreadsCount = session.MaxTaskThreadsCount;
        this._sendAttrs2DelayedNotifications = session.SendAttrs2DelayedNotificationMode;
        this._allVersionsAnnulmentMode = session.AllVersionsAnnulmentMode;
        this._DisableDBPatch = sessionKeeper.Session.Configurations.ReadInteger("KERNEL", "COMMON", "DisableDBPatch", 0L, DBConfigMode.GlobalOnly) == 1L;
        this._EnableAttributeLCStepSecurity = sessionKeeper.Session.Configurations.ReadInteger("KERNEL", "SECURITY", "CHECK_ATTR_LCACCESS", 0L, DBConfigMode.GlobalOnly) == 1L;
        this._IndexTablespaceName = sessionKeeper.Session.Configurations.ReadString("KERNEL", "COMMON", "INDEX_TABLESPACE", string.Empty, DBConfigMode.GlobalOnly);
        this._EnableArticlesAccessMode = sessionKeeper.Session.Configurations.ReadBool("ARCHIVES", "SECURITY", "ART_ACCESS", false, DBConfigMode.GlobalOnly);
        this._SetProjectOnCreateRelation = sessionKeeper.Session.Configurations.ReadBool("KERNEL", "PROJECT", "SET_PROJ2CHILD", true, DBConfigMode.GlobalOnly);
        this._CopyArchiveVisibility = sessionKeeper.Session.Configurations.ReadBool("ARCHIVES", "SECURITY", "COPY_ARC_VISIBLE", false, DBConfigMode.GlobalOnly);
        this._CopyProjectVisibility = sessionKeeper.Session.Configurations.ReadBool("KERNEL", "SECURITY", "COPY_PROJ_VISIBLE", false, DBConfigMode.GlobalOnly);
        this._UpdateViewsList = sessionKeeper.Session.Configurations.ReadBool("KERNEL", "PERFORMANCE", "UPDATE_VIEWS_LIST", false, DBConfigMode.GlobalOnly);
        this._useHiddenComposition = sessionKeeper.Session.Configurations.ReadBool("KERNEL", "PERFORMANCE", "UseHiddenComposition", true, DBConfigMode.GlobalOnly);
      }
      this._searchInIndexSubstringGettingMode = SearchInIndexSubstringGettingModeHelper.GetSearchInIndexSubstringGettingMode();
    }

    private void CheckInited()
    {
      if (this._inited)
        return;
      this.LoadCurrentValues();
      this._inited = true;
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("Attribute.DatabaseConfigurator_24")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_25")]
    public bool EnableEditOwnCondition
    {
      get
      {
        this.CheckInited();
        return this._enableEditOwnCondition;
      }
      set
      {
        if (this._enableEditOwnCondition == value)
          return;
        this._enableEditOwnCondition = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("Attribute.DatabaseConfigurator_26")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_27")]
    public bool EnabledPdmConfigurator
    {
      get
      {
        this.CheckInited();
        return this._enabledPdmConfigurator;
      }
      set
      {
        if (this._enabledPdmConfigurator == value)
          return;
        this._enabledPdmConfigurator = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("Attribute.DatabaseConfigurator_26.s")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_27.s")]
    public bool EnabledSeriesDates
    {
      get
      {
        this.CheckInited();
        return this._enabledSeriesDates;
      }
      set
      {
        if (this._enabledSeriesDates == value)
          return;
        this._enabledSeriesDates = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("Attribute.DatabaseConfigurator_26.v")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_27.v")]
    public bool EnabledVisibilityFiltration
    {
      get
      {
        this.CheckInited();
        return this._enabledVisibilityFiltration;
      }
      set
      {
        if (this._enabledVisibilityFiltration == value)
          return;
        this._enabledVisibilityFiltration = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("AutoSoftInstantiationNote")]
    [CustomDisplayName("AutoSoftInstantiationMode")]
    public bool EnabledAutoSoftInstantiation
    {
      get
      {
        this.CheckInited();
        return this._enabledAutoSoftInstantiation;
      }
      set
      {
        if (this._enabledAutoSoftInstantiation == value)
          return;
        this._enabledAutoSoftInstantiation = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("SetProjectOnCreateRelationNote")]
    [CustomDisplayName("SetProjectOnCreateRelationMode")]
    public bool SetProjectOnCreateRelation
    {
      get
      {
        this.CheckInited();
        return this._SetProjectOnCreateRelation;
      }
      set
      {
        if (this._SetProjectOnCreateRelation == value)
          return;
        this._SetProjectOnCreateRelation = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("CopyArchiveVisibilityNote")]
    [CustomDisplayName("CopyArchiveVisibilityMode")]
    public bool CopyArchiveVisibility
    {
      get
      {
        this.CheckInited();
        return this._CopyArchiveVisibility;
      }
      set
      {
        if (this._CopyArchiveVisibility == value)
          return;
        this._CopyArchiveVisibility = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("CopyProjectVisibilityNote")]
    [CustomDisplayName("CopyProjectVisibilityMode")]
    public bool CopyProjectVisibility
    {
      get
      {
        this.CheckInited();
        return this._CopyProjectVisibility;
      }
      set
      {
        if (this._CopyProjectVisibility == value)
          return;
        this._CopyProjectVisibility = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("DelayedAttrHistoryNote")]
    [CustomDisplayName("DelayedAttrHistoryMode")]
    public bool EnabledDelayedAttrHistory
    {
      get
      {
        this.CheckInited();
        return this._enabledDelayedAttrHistory;
      }
      set
      {
        if (this._enabledDelayedAttrHistory == value)
          return;
        this._enabledDelayedAttrHistory = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("DelayedEventlogNote")]
    [CustomDisplayName("DelayedEventlogMode")]
    public bool EnabledDelayedEventlog
    {
      get
      {
        this.CheckInited();
        return this._enabledDelayedEventlog;
      }
      set
      {
        if (this._enabledDelayedEventlog == value)
          return;
        this._enabledDelayedEventlog = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("SendAttrsNote")]
    [CustomDisplayName("SendAttrsMode")]
    public bool SendAttrs2DelayedNotifications
    {
      get
      {
        this.CheckInited();
        return this._sendAttrs2DelayedNotifications;
      }
      set
      {
        if (this._sendAttrs2DelayedNotifications == value)
          return;
        this._sendAttrs2DelayedNotifications = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("AllAnnulmentNote")]
    [CustomDisplayName("AllAnnulmentMode")]
    public bool AllVersionsAnnulmentMode
    {
      get
      {
        this.CheckInited();
        return this._allVersionsAnnulmentMode;
      }
      set
      {
        if (this._allVersionsAnnulmentMode == value)
          return;
        this._allVersionsAnnulmentMode = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("DisableDBPatchNote")]
    [CustomDisplayName("DisableDBPatchMode")]
    public bool DisableDBPatch
    {
      get
      {
        this.CheckInited();
        return this._DisableDBPatch;
      }
      set
      {
        if (this._DisableDBPatch == value)
          return;
        this._DisableDBPatch = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("EnableALCStepSecurityNote")]
    [CustomDisplayName("EnableALCStepSecurityMode")]
    public bool EnableAttributeLCStepSecurity
    {
      get
      {
        this.CheckInited();
        return this._EnableAttributeLCStepSecurity;
      }
      set
      {
        if (this._EnableAttributeLCStepSecurity == value)
          return;
        this._EnableAttributeLCStepSecurity = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("EnableAtriclesAccessModeNote")]
    [CustomDisplayName("EnableAtriclesAccessModeMode")]
    public bool EnableAtriclesAccessMode
    {
      get
      {
        this.CheckInited();
        return this._EnableArticlesAccessMode;
      }
      set
      {
        if (this._EnableArticlesAccessMode == value)
          return;
        this._EnableArticlesAccessMode = value;
      }
    }

    [CustomDescription("Attribute.DatabaseConfigurator_28")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_29")]
    public int MaxTaskThreadsCount
    {
      get
      {
        this.CheckInited();
        return this._maxTaskThreadsCount;
      }
      set
      {
        if (this._maxTaskThreadsCount == value || value <= 0)
          return;
        this._maxTaskThreadsCount = value;
      }
    }

    [Description("Имя табличного пространства для хранения индексов (для СУБД Oracle и PostgreSQL). Настройка не влияет на существующие индексы. После изменения настройки требуется перегрузить сервер приложений.")]
    [DisplayName("Имя табличного пространства для индексов")]
    public string IndexTablespaceName
    {
      get
      {
        this.CheckInited();
        return this._IndexTablespaceName;
      }
      set => this._IndexTablespaceName = value;
    }

    [Description("Поиск в индексе по подстроке")]
    [DisplayName("Поиск в индексе по подстроке")]
    public SearchInIndexSubstringGettingMode SearchInIndexSubstring
    {
      get
      {
        this.CheckInited();
        return this._searchInIndexSubstringGettingMode;
      }
      set
      {
        if (this._searchInIndexSubstringGettingMode == value)
          return;
        this._searchInIndexSubstringGettingMode = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [CustomDescription("UpdateViewsListOnObjectChangeDescr")]
    [CustomDisplayName("UpdateViewsListOnObjectChangeName")]
    public bool UpdateViewsList
    {
      get
      {
        this.CheckInited();
        return this._UpdateViewsList;
      }
      set
      {
        if (this._UpdateViewsList == value)
          return;
        this._UpdateViewsList = value;
      }
    }

    [TypeConverter(typeof (YesNoBooleanConverter))]
    [Description("При использовании не будут доступны пункт контекстного меню Разрешить скрывать состав, кнопки панели инструментов и команды главного меню для скрытия состава. Поиск состава будет производиться без использования метода DBRelationCollection.FindComposition.")]
    [DisplayName("Использовать функцию скрытия состава")]
    public bool UseHiddenComposition
    {
      get
      {
        this.CheckInited();
        return this._useHiddenComposition;
      }
      set => this._useHiddenComposition = value;
    }
  }
}
