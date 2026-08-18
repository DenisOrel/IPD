
// Type: Intermech.Navigator.Selections.PasteCommand.PasteIntoHandSelectionHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Selections.PasteCommand;

internal sealed class PasteIntoHandSelectionHandler : PasteIntoSelectionHandler
{
  protected SelectionType selectionType;

  public PasteIntoHandSelectionHandler(
    IUserSession session,
    IDBObject targetObject,
    List<IDBTypedObjectID> pasteObjects,
    bool isCut)
    : base(session, targetObject, pasteObjects, isCut)
  {
    this.selectionType = (SelectionType) targetObject.GetAttributeByGuid(new Guid("cad00158-306c-11d8-b4e9-00304f19f545")).AsInteger;
  }

  protected override bool EnablePasteSelection(
    List<string> preparePasteErrors,
    IDBTypedObjectID pasteObject)
  {
    return true;
  }
}
