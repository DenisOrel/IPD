
// Type: IMClient.OptimizationSettingsWrapper




using Intermech;
using Intermech.Controls.SpellCheck;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Search;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;


namespace IMClient
{
    internal sealed class OptimizationSettingsWrapper
    {
      private string _fileZipExclusions;
      private int _maxRows;
      private int _maxEventsCount;
      private IConfigurationManager _manager;
      private bool _hideNavigatorReadAllButton;
      private bool fileZipExclusionsReadonlyInitialized;
      private bool fileZipExclusionsReadonly;

      public void Apply()
      {
        UISettings.RestoreDocumentWindows = this.RestoreDocumentWindows;
        UISettings.AutoupdateNonActiveWindows = this.AutoupdateNonActiveWindows;
        OptimizationSettings.BackgroundTreeTasks = this.BackgroundTreeTasks;
        OptimizationSettings.SpellCheck = this.SpellCheck;
        OptimizationSettings.FullCompositionsSorting = this.FullCompositionsSorting;
        OptimizationSettings.FileZipExclusions = this._fileZipExclusions;
        OptimizationSettings.NotificationServiceMode = this.NotificationServiceMode;
        OptimizationSettings.MaxEventsCount = this._maxEventsCount;
        OptimizationSettings.HideNavigatorReadAllButton = this.HideNavigatorReadAllButton;
        SpellChecker.Instance.Dict.UserWords = this.SpellCheckDictionary;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          if (session.MaxRows == this._maxRows)
            return;
          session.MaxRows = this._maxRows;
          (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).ReloadMaxRows();
        }
      }

      public void RestoreValues()
      {
        this.RestoreDocumentWindows = UISettings.RestoreDocumentWindows;
        this.AutoupdateNonActiveWindows = UISettings.AutoupdateNonActiveWindows;
        this.BackgroundTreeTasks = OptimizationSettings.BackgroundTreeTasks;
        this.SpellCheck = OptimizationSettings.SpellCheck;
        this.SpellCheckDictionary = SpellChecker.Instance.Dict.UserWords;
        this.FullCompositionsSorting = OptimizationSettings.FullCompositionsSorting;
        this._fileZipExclusions = OptimizationSettings.FileZipExclusions;
        this.NotificationServiceMode = OptimizationSettings.NotificationServiceMode;
        this._maxEventsCount = OptimizationSettings.MaxEventsCount;
        this._hideNavigatorReadAllButton = OptimizationSettings.HideNavigatorReadAllButton;
        this._maxRows = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).MaxRows;
      }

      public OptimizationSettingsWrapper(IConfigurationManager manager)
      {
        this._manager = manager;
        manager.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(this.manager_ConfigurationBeforeSave);
        IConfiguration configuration1 = this._manager.Open("OptimizationSettings");
        this.RestoreValues();
        if (configuration1 != null)
        {
          this.BackgroundTreeTasks = false;
          bool result1;
          if (bool.TryParse(configuration1.GetProperty(nameof (BackgroundTreeTasks)), out result1))
            this.BackgroundTreeTasks = result1;
          if (bool.TryParse(configuration1.GetProperty(nameof (SpellCheck)), out result1))
            this.SpellCheck = result1;
          string property = configuration1.GetProperty(nameof (SpellCheckDictionary));
          try
          {
            this.SpellCheckDictionary = SpellChecker.Instance.Dict.UserFileLoad(property);
            SpellChecker.Instance.Dict.UserWords = this.SpellCheckDictionary;
          }
          catch
          {
          }
          this.FullCompositionsSorting = false;
          if (bool.TryParse(configuration1.GetProperty(nameof (FullCompositionsSorting)), out result1))
            this.FullCompositionsSorting = result1;
          try
          {
            this.NotificationServiceMode = (NotificationServiceMode) Enum.Parse(typeof (NotificationServiceMode), configuration1.GetProperty(nameof (NotificationServiceMode)), true);
          }
          catch
          {
            this.NotificationServiceMode = NotificationServiceMode.NotifyUser;
          }
          this._maxEventsCount = 100;
          int result2;
          if (int.TryParse(configuration1.GetProperty(nameof (MaxEventsCount)), out result2))
            this._maxEventsCount = result2;
        }
        IConfiguration configuration2 = this._manager.Open("UISettings");
        if (configuration2 != null)
        {
          try
          {
            this.RestoreDocumentWindows = (DocumentRestoreMode) Enum.Parse(typeof (DocumentRestoreMode), configuration2.GetProperty(nameof (RestoreDocumentWindows)), true);
          }
          catch
          {
            this.RestoreDocumentWindows = DocumentRestoreMode.CreateProxy;
          }
          this.AutoupdateNonActiveWindows = false;
          bool result;
          if (bool.TryParse(configuration2.GetProperty(nameof (AutoupdateNonActiveWindows)), out result))
            this.AutoupdateNonActiveWindows = result;
        }
        if (ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service)
          this._fileZipExclusions = service.ReadString("KERNEL", "OptimizationSettings", nameof (FileZipExclusions), "", DBConfigMode.GlobalOnly);
        this._hideNavigatorReadAllButton = service.ReadBool("CLIENT", "OptimizationSettings", nameof (HideNavigatorReadAllButton), true, DBConfigMode.GlobalOnly);
        this.Apply();
      }

      private void manager_ConfigurationBeforeSave(IConfigurationManager configurationManager)
      {
        IConfiguration configuration1 = this._manager.Open("UISettings") ?? this._manager.Create("UISettings");
        configuration1.SetProperty("RestoreDocumentWindows", this.RestoreDocumentWindows.ToString());
        configuration1.SetProperty("AutoupdateNonActiveWindows", this.AutoupdateNonActiveWindows.ToString());
        IConfiguration configuration2 = this._manager.Create("OptimizationSettings");
        configuration2.SetProperty("BackgroundTreeTasks", this.BackgroundTreeTasks.ToString());
        configuration2.SetProperty("SpellCheck", this.SpellCheck.ToString());
        configuration2.SetProperty("FullCompositionsSorting", this.FullCompositionsSorting.ToString());
        configuration2.SetProperty("NotificationServiceMode", this.NotificationServiceMode.ToString());
        configuration2.SetProperty("MaxEventsCount", this._maxEventsCount.ToString());
        configuration2.SetProperty("HideNavigatorReadAllButton", this._hideNavigatorReadAllButton.ToString());
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
          if (sessionKeeper.Session.RoleID != sessionKeeper.Session.IdentHelper.AdminRoleID)
            return;
          service.WriteString("KERNEL", "OptimizationSettings", "FileZipExclusions", this._fileZipExclusions, 0L);
          service.WriteBool("CLIENT", "OptimizationSettings", "HideNavigatorReadAllButton", this.HideNavigatorReadAllButton, 0L);
        }
      }

      [CustomDescription("Attribute.IMClient_13")]
      [CustomDisplayName("Attribute.IMClient_14")]
      public int MaxRows
      {
        [DebuggerStepThrough] get => this._maxRows;
        set
        {
          this._maxRows = value >= 1 ? value : throw new ArgumentException(LocalizationHolder.rm.GetString("IMClient_14"));
        }
      }

      [CustomDisplayName("Attribute.IMClient_16")]
      [CustomDescription("Attribute.IMClient_15")]
      public DocumentRestoreMode RestoreDocumentWindows { [DebuggerStepThrough] get; set; }

      [CustomDescription("Attribute.IMClient_FileZipExclusions_Desc")]
      [CustomDisplayName("Attribute.IMClient_FileZipExclusions")]
      public string FileZipExclusions
      {
        [DebuggerStepThrough] get => this._fileZipExclusions;
        set
        {
          if (!this.fileZipExclusionsReadonlyInitialized)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              this.fileZipExclusionsReadonly = sessionKeeper.Session.RoleID != sessionKeeper.Session.IdentHelper.AdminRoleID;
              this.fileZipExclusionsReadonlyInitialized = true;
            }
          }
          if (this.fileZipExclusionsReadonly)
            return;
          this._fileZipExclusions = value;
        }
      }

      [CustomDisplayName("Attribute.IMClient_18")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [CustomDescription("Attribute.IMClient_17")]
      public bool AutoupdateNonActiveWindows { [DebuggerStepThrough] get; set; }

      [CustomDisplayName("Attribute.IMClient_20")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [CustomDescription("Attribute.IMClient_19")]
      public bool BackgroundTreeTasks { [DebuggerStepThrough] get; set; }

      [CustomDescription("Optimization.HideNavigatorReadAllButton.Description")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [CustomDisplayName("Optimization.HideNavigatorReadAllButton")]
      [IsAdmin]
      public bool HideNavigatorReadAllButton
      {
        [DebuggerStepThrough] get => this._hideNavigatorReadAllButton;
        [DebuggerStepThrough] set
        {
          if (this._hideNavigatorReadAllButton == value)
            return;
          this.CheckAdminRights(LocalizationHolder.rma.GetString("Optimization.HideNavigatorReadAllButton.NoRightsException"));
          this._hideNavigatorReadAllButton = value;
        }
      }

      [CustomDisplayName("Attribute.IMClient_33")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      [CustomDescription("Attribute.IMClient_34")]
      public bool SpellCheck { [DebuggerStepThrough] get; set; }

      [Editor(typeof (SpellCheckerUIEditor), typeof (UITypeEditor))]
      [CustomDisplayName("Attribute.IMClient_47")]
      [TypeConverter(typeof (SpellCheckerConverter))]
      [CustomDescription("Attribute.IMClient_48")]
      public Hashtable SpellCheckDictionary { [DebuggerStepThrough] get; set; } = new Hashtable();

      [CustomDisplayName("Attribute.IMClient_36")]
      [CustomDescription("Attribute.IMClient_35")]
      [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
      public bool FullCompositionsSorting { [DebuggerStepThrough] get; set; }

      [CustomDisplayName("Optimization.MaxEventsCount.Name")]
      [CustomDescription("Optimization.MaxEventsCount.Descr")]
      public int MaxEventsCount
      {
        [DebuggerStepThrough] get => this._maxEventsCount;
        set
        {
          this._maxEventsCount = value >= 10 && value <= 1000 ? value : throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Optimization.MaxEventsCount.Limits"), (object) 10, (object) 1000));
        }
      }

      [CustomDescription("Optimization.NotificationServiceMode.Descr")]
      [CustomDisplayName("Optimization.NotificationServiceMode.Name")]
      public NotificationServiceMode NotificationServiceMode { [DebuggerStepThrough] get; set; }

      private void CheckAdminRights(string errorMessage)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (!sessionKeeper.Session.IsAdmin)
            throw new InvalidOperationException(errorMessage);
        }
      }
    }
}
