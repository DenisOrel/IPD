
// Type: Intermech.Search.RecentObjects.RecentObjectsClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.RecentObjects;

public sealed class RecentObjectsClientService : IRecentObjectsClientService
{
  private INotificationService _notificationService;
  private List<long> _currentUserRecentObjects = new List<long>();
  private bool _isCurrentUserRecentObjectsLoaded;
  private RecentObjectsSettings _currentUserRecentObjectsSettings;

  public RecentObjectsClientService(INotificationService notificationService)
  {
    this._notificationService = notificationService != null ? notificationService : throw new ArgumentNullException(nameof (notificationService));
    this._notificationService.Subscribe(new Intermech.Interfaces.Client.NotificationEventHandler(this.NotificationEventHandler));
  }

  public void AddToCurrentUserRecentObjects(long[] objectVersionIds)
  {
    if (objectVersionIds == null || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
      throw new ArgumentException();
    this.LoadCurrentUserRecentObjects();
    long[] array = this._currentUserRecentObjects.ToArray();
    foreach (long objectVersionId in objectVersionIds)
    {
      if (!this._currentUserRecentObjects.Contains(objectVersionId) && !this._currentUserRecentObjects.Contains(-objectVersionId))
      {
        if (this._currentUserRecentObjects.Count == this.GetCurrentUserRecentObjectsSettings().RecentObjectsMaxCount && this._currentUserRecentObjects.Count > 0)
          this._currentUserRecentObjects.RemoveAt(0);
        this._currentUserRecentObjects.Add(objectVersionId);
      }
    }
    this.NotifyRecentObjectsChanged(array, this._currentUserRecentObjects.ToArray());
  }

  public void ChangeRecentObjectsAccessSettings()
  {
    using (RecentObjectsAccessSettingsForm accessSettingsForm = new RecentObjectsAccessSettingsForm())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IRecentObjectsServerService customService = (IRecentObjectsServerService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsServerService));
        accessSettingsForm.ObjectVersionIds = customService.GetRecentObjectsAccessSettings(sessionKeeper.Session.SessionGUID);
      }
      if (accessSettingsForm.ShowDialog() != DialogResult.OK)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        ((IRecentObjectsServerService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsServerService))).SetRecentObjectsAccessSettings(sessionKeeper.Session.SessionGUID, accessSettingsForm.ObjectVersionIds);
    }
  }

  public void ClearCurrentUserRecentObjects()
  {
    this._currentUserRecentObjects.Clear();
    this._isCurrentUserRecentObjectsLoaded = true;
    this._notificationService.FireEvent((object) this, new NotificationEventArgs("RecentObjectsCleared"));
  }

  public long[] GetCurrentUserRecentObjects()
  {
    this.LoadCurrentUserRecentObjects();
    return this._currentUserRecentObjects.ToArray();
  }

  public void OpenOtherUserRecentObjects()
  {
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new RecentObjectsClientService.OnlyUserSelectedItemsAnalyzer(), true);
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects("Выбор пользователя", "Выберите пользователя, чьи недавние объекты нужно отобразить.", (IDescriptor) new UsersGroupsDescriptor(), (System.IServiceProvider) ServicesManager.ServiceContainer, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ((IRecentObjectsSharingService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsSharingService))).ValidateAccessMode(sessionKeeper.Session.SessionGUID, numArray[0]);
    Utils.OpenNewWindow((IDescriptor) new OtherUserRecentObjectsDescriptor(numArray[0]), (System.IServiceProvider) ServicesManager.ServiceContainer);
  }

  public void RemoveFromCurrentUserRecentObjects(long[] objectVersionIds)
  {
    if (objectVersionIds == null || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
      throw new ArgumentException();
    this.LoadCurrentUserRecentObjects();
    long[] array = this._currentUserRecentObjects.ToArray();
    foreach (long objectVersionId in objectVersionIds)
    {
      this._currentUserRecentObjects.Remove(objectVersionId);
      this._currentUserRecentObjects.Remove(-objectVersionId);
    }
    this.NotifyRecentObjectsChanged(array, this._currentUserRecentObjects.ToArray());
  }

  public RecentObjectsSettings GetCurrentUserRecentObjectsSettings()
  {
    if (this._currentUserRecentObjectsSettings == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._currentUserRecentObjectsSettings = ((IRecentObjectsServerService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsServerService))).GetCurrentUserRecentObjectsSettings(sessionKeeper.Session.SessionGUID);
    }
    return this._currentUserRecentObjectsSettings;
  }

  public void SetCurrentUserRecentObjectsSettings(RecentObjectsSettings recentObjectsSettings)
  {
    this._currentUserRecentObjectsSettings = recentObjectsSettings != null ? recentObjectsSettings : throw new ArgumentNullException(nameof (recentObjectsSettings));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ((IRecentObjectsServerService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsServerService))).SetCurrentUserRecentObjectsSettings(sessionKeeper.Session.SessionGUID, this._currentUserRecentObjectsSettings);
  }

  private void LoadCurrentUserRecentObjects()
  {
    if (this._isCurrentUserRecentObjectsLoaded)
      return;
    this._currentUserRecentObjects.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._currentUserRecentObjects.AddRange((IEnumerable<long>) ((IRecentObjectsServerService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsServerService))).GetCurrentUserRecentObjects(sessionKeeper.Session.SessionGUID));
    this._isCurrentUserRecentObjectsLoaded = true;
  }

  private void NotificationEventHandler(object sender, NotificationEventArgs e)
  {
    if (e.EventName == "ObjectsRemoved")
    {
      if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null)
        return;
      List<long> longList = new List<long>();
      foreach (long userRecentObject in this._currentUserRecentObjects)
      {
        if (objectsEventArgs.ObjectIDs.Contains(userRecentObject) || objectsEventArgs.ObjectIDs.Contains(-userRecentObject))
          longList.Add(userRecentObject);
      }
      this.RemoveFromCurrentUserRecentObjects(longList.ToArray());
    }
    else
    {
      if (!(e.EventName == "ApplicationClosing"))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        ((IRecentObjectsServerService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsServerService))).SaveCurrentUserRecentObjects(sessionKeeper.Session.SessionGUID, this._currentUserRecentObjects.ToArray());
    }
  }

  private void NotifyRecentObjectsChanged(long[] oldRecentObjects, long[] newRecentObjects)
  {
    long[] array1 = ((IEnumerable<long>) newRecentObjects).Where<long>((Func<long, bool>) (o => !((IEnumerable<long>) oldRecentObjects).Contains<long>(o) && !((IEnumerable<long>) oldRecentObjects).Contains<long>(-o))).ToArray<long>();
    long[] array2 = ((IEnumerable<long>) oldRecentObjects).Where<long>((Func<long, bool>) (o => !((IEnumerable<long>) newRecentObjects).Contains<long>(o) && !((IEnumerable<long>) newRecentObjects).Contains<long>(-o))).ToArray<long>();
    if (array1.Length == 0 && array2.Length == 0)
      return;
    this._notificationService.FireEvent((object) this, (NotificationEventArgs) new RecentObjectsChangedEventArgs(array1, array2));
  }

  private sealed class OnlyUserSelectedItemsAnalyzer : ISelectedItemsAnalyzer
  {
    private static readonly Guid OnlyUserSelectedItemsAnalyzerGuid = new Guid("0F7AAF8C-86AE-492E-A970-F9B0D45DF5BB");

    public Guid Guid
    {
      get
      {
        return RecentObjectsClientService.OnlyUserSelectedItemsAnalyzer.OnlyUserSelectedItemsAnalyzerGuid;
      }
    }

    public SelectedItemsAnalyzerResult Analyze(
      ISelectionWindow sender,
      ISelectedItemsHost itemsHost)
    {
      if (itemsHost != null)
      {
        ISelectedItems selectedItems = itemsHost.SelectedItems;
        IDBTypedObjectID typedObjectID;
        if (selectedItems != null && selectedItems.Count == 1 && SelectedItemsHelper.TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(selectedItems, out typedObjectID) && typedObjectID.ObjectType == Constants.UserObjectTypeID)
          return SelectedItemsAnalyzerResult.Enabled;
      }
      return SelectedItemsAnalyzerResult.Disabled;
    }
  }
}
