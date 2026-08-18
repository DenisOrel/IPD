
// Type: Intermech.Navigator.Selections.PasteCommand.PasteIntoObjectHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections.PasteCommand;

/// <summary>Базовый класс обработчиков вставки</summary>
internal abstract class PasteIntoObjectHandler : IPasteIntoObjectHandler
{
  protected bool isCut;
  protected IDBObject targetObject;
  protected List<IDBTypedObjectID> pasteObjects;
  protected int targetObjectTypeID;
  protected IUserSession session;
  protected ISelectionsService selectionsService;

  public PasteIntoObjectHandler(
    IUserSession session,
    IDBObject targetObject,
    List<IDBTypedObjectID> pasteObjects,
    bool isCut)
  {
    this.targetObject = targetObject;
    this.pasteObjects = pasteObjects;
    this.isCut = isCut;
    this.targetObjectTypeID = targetObject.ObjectType;
    this.session = session;
    this.selectionsService = ServicesManager.GetService(typeof (ISelectionsService)) as ISelectionsService;
  }

  /// <summary>
  /// Вставляемый объект тоже является выборкой/классификатором
  /// </summary>
  /// <returns></returns>
  protected abstract void CheckPasteObject(
    List<long> pasteList,
    List<string> preparePasteErrors,
    IDBTypedObjectID pasteObject);

  public void Paste()
  {
    ServicesManager.GetService(typeof (ISelectionsService));
    List<long> longList = new List<long>();
    List<string> stringList = new List<string>();
    PasteHelper.EnableTypes4Paste(this.targetObject);
    MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
    this.session.GetRelationCollection(this.session.IdentHelper.DocRelationTypeID);
    foreach (IDBTypedObjectID pasteObject in this.pasteObjects)
    {
      if (this.targetObject.ObjectID == pasteObject.ObjectID)
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_685"), (object) pasteObject.Caption, (object) LocalizationHolder.rm.GetString("Client.Core_50"), (object) MessageBoxButtons.OK, (object) MessageBoxIcon.Asterisk));
      }
    }
  }
}
