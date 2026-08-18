
// Type: Intermech.Navigator.Selections.PasteCommand.PasteIntoSelectionHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using System.Collections.Generic;


namespace Intermech.Navigator.Selections.PasteCommand;

internal class PasteIntoSelectionHandler(
  IUserSession session,
  IDBObject targetObject,
  List<IDBTypedObjectID> pasteObjects,
  bool isCut) : PasteIntoObjectHandler(session, targetObject, pasteObjects, isCut)
{
  protected virtual bool EnablePasteSelection(
    List<string> preparePasteErrors,
    IDBTypedObjectID pasteObject)
  {
    preparePasteErrors.Add(string.Format(LocalizationHolder.rm.GetString("Client.Core_690"), (object) this.session.GetObject(pasteObject.ObjectID).NameInMessages));
    return false;
  }

  protected override void CheckPasteObject(
    List<long> pasteList,
    List<string> preparePasteErrors,
    IDBTypedObjectID pasteObject)
  {
  }
}
