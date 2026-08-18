
// Type: Intermech.Search.ObjectListFilters.ObjectListFiltersClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.ObjectListFilters;

public sealed class ObjectListFiltersClientService : IObjectListFiltersClientService
{
  private List<ObjectListFilter> _filters;

  public ObjectListFiltersClientService(INotificationService notificationService)
  {
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    notificationService.Subscribe(new NotificationEventHandler(this.NotificationService_EventFired));
  }

  public ObjectListFilter[] GetAllFilters()
  {
    this.LoadFiltersIfNotLoaded();
    return this._filters.ToArray();
  }

  public ObjectListFilter[] GetFiltersForObjectType(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    this.LoadFiltersIfNotLoaded();
    List<int> objectTypeIds = MetaDataHelper.GetObjectTypeParentsID(objectTypeID);
    objectTypeIds.Add(objectTypeID);
    return this._filters.Where<ObjectListFilter>((Func<ObjectListFilter, bool>) (o => o.ObjectTypeIds.Length == 0 || ((IEnumerable<int>) o.ObjectTypeIds).Any<int>((Func<int, bool>) (oo => objectTypeIds.Contains(oo))))).ToArray<ObjectListFilter>();
  }

  public void RefreshCache() => this._filters = (List<ObjectListFilter>) null;

  public ObjectListFilter CreateNewFilter(ObjectListFilterType type)
  {
    using (TextBoxForm textBoxForm = new TextBoxForm())
    {
      textBoxForm.LabelText = "Введите имя нового фильтра объектов:";
      textBoxForm.TextBoxText = "Новый фильтр объектов";
      textBoxForm.Text = "Создание фильтра объектов";
      if (textBoxForm.ShowDialog() == DialogResult.OK)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ObjectListFilter newFilter = ((IObjectListFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (IObjectListFiltersServerService))).CreateNewFilter(sessionKeeper.Session.SessionGUID, textBoxForm.TextBoxText, type);
          ServiceLocator.Get<INotificationService>().FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", newFilter.ID, Constants.CommonSelectionObjectTypeID));
          return newFilter;
        }
      }
    }
    return (ObjectListFilter) null;
  }

  public void RemoveFilter(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session))
        ((IObjectListFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (IObjectListFiltersServerService))).RemoveFilter(sessionKeeper.Session.SessionGUID, objectVersionID);
    }
  }

  private void NotificationService_EventFired(object sender, NotificationEventArgs e)
  {
    if (e.EventName == "ObjectsCreated")
    {
      if (!(e is DBObjectsEventArgs))
        return;
      DBObjectsEventArgs objectsEventArgs = (DBObjectsEventArgs) e;
      if (objectsEventArgs.ObjectTypeIDs == null || !objectsEventArgs.ObjectTypeIDs.Contains(Constants.CommonSelectionObjectTypeID) && !objectsEventArgs.ObjectTypeIDs.Contains(Constants.PersonalSelectionObjectTypeID))
        return;
      this._filters = (List<ObjectListFilter>) null;
    }
    else if (e.EventName == "ObjectsChanged")
    {
      if (!(e is DBObjectsEventArgs))
        return;
      DBObjectsEventArgs dbObjectsEventArgs = (DBObjectsEventArgs) e;
      if (this._filters == null || !this._filters.Any<ObjectListFilter>((Func<ObjectListFilter, bool>) (o => dbObjectsEventArgs.ObjectIDs.Contains(o.ID))))
        return;
      this._filters = (List<ObjectListFilter>) null;
    }
    else
    {
      if (!(e.EventName == "ObjectsRemoved") || !(e is DBObjectsEventArgs))
        return;
      DBObjectsEventArgs dbObjectsEventArgs = (DBObjectsEventArgs) e;
      if (this._filters == null)
        return;
      this._filters.RemoveAll((Predicate<ObjectListFilter>) (o => dbObjectsEventArgs.ObjectIDs.Contains(o.ID)));
    }
  }

  private void LoadFiltersIfNotLoaded()
  {
    if (this._filters != null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._filters = ((IEnumerable<ObjectListFilter>) ((IObjectListFiltersServerService) sessionKeeper.Session.GetCustomService(typeof (IObjectListFiltersServerService))).FindAllFilters(sessionKeeper.Session.SessionGUID)).ToList<ObjectListFilter>();
  }
}
