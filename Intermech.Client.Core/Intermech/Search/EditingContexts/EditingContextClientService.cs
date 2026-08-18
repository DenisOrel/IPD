
// Type: Intermech.Search.EditingContexts.EditingContextClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Navigator;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Search.EditingContexts;

public sealed class EditingContextClientService : IEditingContextClientService
{
  private LazyService<ICurrentUserAndRole> _currentUserAndRole = new LazyService<ICurrentUserAndRole>();
  private LazyService<IFiltrationService> _filtrationService = new LazyService<IFiltrationService>();
  private LazyService<INotificationService> _notificationService = new LazyService<INotificationService>();

  public void ActivateEditingContext(long editingContextID)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(editingContextID))
      throw new ArgumentException();
    if (this._currentUserAndRole.Value.CachedEditingContextID == editingContextID)
      return;
    this._currentUserAndRole.Value.EditingContextID = editingContextID;
    if (MessageBox.Show("Включить режим автоматического пополнения текущего контекста редактирования", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
      this._currentUserAndRole.Value.SilentMode = false;
      this._currentUserAndRole.Value.EditingContextMode = EditingContextMode.AutoUpdate;
    }
    else
      this._currentUserAndRole.Value.EditingContextMode = this._currentUserAndRole.Value.CachedContextMode;
    if (this._currentUserAndRole.Value.CachedEditingContextSource == EditingContextSource.SessionContext)
      return;
    this._filtrationService.Value.FiltrationApplyUpdates(true);
  }

  public void AddObjectsToCurrentEditingContext(long[] objectVersionIds)
  {
    if (objectVersionIds == null)
      throw new ArgumentNullException(nameof (objectVersionIds));
    if (objectVersionIds.Length == 0)
      throw new ArgumentException();
    if (ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(this._currentUserAndRole.Value.CachedEditingContextID))
      throw new InvalidOperationException();
    this.AddObjectsToCurrentEditingContextInternal(objectVersionIds, AddObjectsToEditingContextType.Objects);
  }

  public void AddObjectsWithCompositionToCurrentEditingContext(long[] objectVersionIds)
  {
    if (objectVersionIds == null)
      throw new ArgumentNullException(nameof (objectVersionIds));
    if (objectVersionIds.Length == 0)
      throw new ArgumentException();
    if (ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(this._currentUserAndRole.Value.CachedEditingContextID))
      throw new InvalidOperationException();
    AddObjectsToEditingContextType addObjectsToEditingContextType;
    switch (EditingContextClientHelper.ShowSelectAddObjectsToEditingContextTypeDialog())
    {
      case DialogResult.Yes:
        addObjectsToEditingContextType = AddObjectsToEditingContextType.ObjectsWithRecursiveComposition;
        break;
      case DialogResult.No:
        addObjectsToEditingContextType = AddObjectsToEditingContextType.ObjectsWithComposition;
        break;
      default:
        return;
    }
    this.AddObjectsToCurrentEditingContextInternal(objectVersionIds, addObjectsToEditingContextType);
  }

  public void ReplaceVersionInCurrentEditingContext(long objectVersionID)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(this._currentUserAndRole.Value.CachedEditingContextID))
      throw new InvalidOperationException();
    long F_ID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      F_ID = sessionKeeper.Session.GetObject(objectVersionID).ID;
    long num1 = ObjectVersionSelection.SelectVersion(F_ID, true, (List<long>) null, objectVersionID);
    if (ObjectHelper.IsUnknownObjectVersionID(num1))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session, (object) this))
      {
        (sessionKeeper.Session.GetCustomService(typeof (IEditingContextServerService)) as IEditingContextServerService).ReplaceVersionInEditingContext(sessionKeeper.Session.SessionGUID, objectVersionID, num1, this._currentUserAndRole.Value.CachedEditingContextID);
        int num2 = (int) MessageBox.Show("Замена версии в контексте успешно завершена", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
    this._notificationService.Value.FireEvent((object) this, new NotificationEventArgs("ObjectTypeAndRelationFiltrationChanged"));
  }

  private void AddObjectsToCurrentEditingContextInternal(
    long[] objectVersionIds,
    AddObjectsToEditingContextType addObjectsToEditingContextType)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session, (object) this))
      {
        IEditingContextServerService customService = sessionKeeper.Session.GetCustomService(typeof (IEditingContextServerService)) as IEditingContextServerService;
        AddObjectsToEditingContextParams editingContextParams = new AddObjectsToEditingContextParams(this._currentUserAndRole.Value.CachedEditingContextID)
        {
          ObjectVersionIds = objectVersionIds,
          Type = addObjectsToEditingContextType
        };
        Guid sessionGuid = sessionKeeper.Session.SessionGUID;
        AddObjectsToEditingContextParams addObjectsToEditingContextParams = editingContextParams;
        EditingContextClientHelper.ShowAddObjectsToEditingContextResultDialog(customService.AddObjectsToEditingContext(sessionGuid, addObjectsToEditingContextParams));
      }
    }
    this._notificationService.Value.FireEvent((object) this, new NotificationEventArgs("ObjectTypeAndRelationFiltrationChanged"));
  }
}
