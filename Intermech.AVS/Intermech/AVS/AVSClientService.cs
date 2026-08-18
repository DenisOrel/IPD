// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSClientService
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Bars;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Клиентский сервис AVS</summary>
public class AVSClientService : IAVSClientService
{
  /// <summary>Идентификатор активной спецификации</summary>
  public long ActiveDocumentId => AVSPlugin.Instance.ActiveDocumentId;

  /// <summary>Спецификация нуждается в обновлении</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="reasonList">Возвращает сообщения о причинах необходимости обновления</param>
  /// <returns></returns>
  public bool SpecificationIsNeedUpdate(
    long objectID,
    int objectType,
    Guid objectGuid,
    out List<string> reasonList)
  {
    long documentId = -1;
    if (!(AVSPlugin.Instance.FindAVSWindow(objectID, objectType, objectGuid) is AVSWindow avsWindow) || avsWindow.ReadOnly)
      return AVSDocument.SpecificationIsNeedUpdate(objectID, objectType, out documentId, out reasonList);
    reasonList = new List<string>();
    return false;
  }

  /// <summary>Открыть конструкторский документ в редакторе AVS</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="saveIfUpdatedForLoad">Сохранить файл документа после обновления при открытии</param>
  /// <returns>Возвращает окно редактирования</returns>
  public object EditAVSDocument(
    long objectID,
    int objectType,
    bool saveIfUpdatedForLoad,
    bool checkIfCanEdit)
  {
    string reasonMessage;
    if (!checkIfCanEdit || DocumentEditorLaunchHandler.AdvancedEditModeCheckForObject(LaunchType.Edit, objectID, out reasonMessage).Item1)
      return (object) AVSPlugin.Instance.OpenAVSWindow(new OpenAVSDocArgs(objectID, objectType, saveIfUpdatedForLoad: saveIfUpdatedForLoad));
    int num = (int) MessageBox.Show("Невозможно редактировать спецификацию.\r\nПричина: " + reasonMessage, "AVS", MessageBoxButtons.OK);
    return (object) null;
  }

  /// <summary>Активное окно является окном со спецификацией</summary>
  public bool ActiveWindowIsSpecification
  {
    get
    {
      AVSWindow activeAvsWindow = AVSPlugin.Instance.ActiveAVSWindow;
      return activeAvsWindow != null && activeAvsWindow.IsSpecification;
    }
  }

  /// <summary>Добавить внешнюю команду в AVS</summary>
  /// <param name="externalAVSCommand">Команда</param>
  public void AddExternalAVSCommand(ExternalAVSCommand externalAVSCommand)
  {
    if (externalAVSCommand == null)
      throw new ArgumentNullException(nameof (externalAVSCommand));
    ICommandManager service = (ICommandManager) ServicesManager.GetService(typeof (ICommandManager));
    if (service != null)
    {
      if (externalAVSCommand.MenuItem == null)
      {
        externalAVSCommand.MenuItem = (MenuItemBase) DocumentMenuHelper.CreateMenuItem(externalAVSCommand.CommandName, externalAVSCommand.Caption, externalAVSCommand.Hint, false, false, service);
        if (externalAVSCommand.MenuItem != null)
          ((BarManager) ServicesManager.GetService(typeof (BarManager)))?.MenuBar.FindMenuBar("AVS")?.Items.Add((ToolbarItemBase) externalAVSCommand.MenuItem);
      }
      else if (service.FindCommand(externalAVSCommand.MenuItem.CommandName) == null)
        service.Add((ButtonItemBase) externalAVSCommand.MenuItem);
    }
    if (AVSPlugin.Instance.ExternalAVSCommands.ContainsKey(externalAVSCommand.CommandName))
      return;
    AVSPlugin.Instance.ExternalAVSCommands.Add(externalAVSCommand.CommandName, externalAVSCommand);
  }

  /// <summary>Удалить внешнюю команду из AVS</summary>
  /// <param name="name">Имя команды</param>
  /// <param name="removeMenuItem">Удалить пункт меню</param>
  public void RemoveExternalAVSCommand(string name)
  {
    AVSPlugin.Instance.ExternalAVSCommands.Remove(name);
    ICommandState command = ((ICommandManager) ServicesManager.GetService(typeof (ICommandManager))).FindCommand(name);
    if (command == null)
      return;
    command.Visible = false;
  }

  /// <summary>Получить внешнюю команду AVS</summary>
  /// <param name="name"></param>
  /// <returns>Возвращает класс с именем и обработчиком команды. Если команды нет, то метод возвращает null</returns>
  public ExternalAVSCommand GetExternalAVSCommand(string name)
  {
    ExternalAVSCommand externalAvsCommand;
    AVSPlugin.Instance.ExternalAVSCommands.TryGetValue(name, out externalAvsCommand);
    return externalAvsCommand;
  }

  /// <summary>Поддерживается ли тип объекта БД как документ AVS</summary>
  /// <param name="dbObjectTypeID">Идентификатор типа объекта БД</param>
  public bool IsAVSDocumentSupportedType(int dbObjectTypeID)
  {
    return AVSDocumentsSettings.Instance.IsAVSDocumentSupportedType(dbObjectTypeID);
  }

  public object GetViewDocument(long objectID, int objectType)
  {
    return this.NeedUpdate(objectID, objectType) ? (object) AVSPlugin.Instance.LoadAVSDocument(objectID, objectType, true).Document : (object) null;
  }

  /// <summary>Надо ли обновлять документ</summary>
  /// <param name="objectID">Идентификатор версии СП</param>
  /// <param name="objectType">Тип СП</param>
  /// <returns></returns>
  private bool NeedUpdate(long objectID, int objectType)
  {
    return AvsConfig.General.UpdateModeInReadOnly == UpdateModeInReadOnlyEnum.Part;
  }

  public event BeforeCommitCreationAVSDocumentEventHandler BeforeCommitCreationAVSDocumentEvent;

  public void OnBeforeCommitCreationAVSDocument(BeforeCommitCreationAVSDocumentEventArgs e)
  {
    BeforeCommitCreationAVSDocumentEventHandler avsDocumentEvent = this.BeforeCommitCreationAVSDocumentEvent;
    if (avsDocumentEvent == null)
      return;
    avsDocumentEvent((object) this, e);
  }
}
