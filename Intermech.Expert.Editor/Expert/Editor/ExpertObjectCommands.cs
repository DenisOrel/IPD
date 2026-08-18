// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ExpertObjectCommands
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Expert.Editor;

internal class ExpertObjectCommands
{
  public static void DuplicateCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    long objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    string initValue = new UserPrompt().Execute(LocalizationHolder.rm.GetString("Expert.Editor_209"), LocalizationHolder.rm.GetString("Expert.Editor_210"));
    if (initValue == "")
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject prototype = sessionKeeper.Session.GetObject(objectID);
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(prototype.ObjectType).Create(prototype);
      AttributeValues[] valuesList = new AttributeValues[1]
      {
        new AttributeValues(ExpertConsts.Consts.attrObjectName, (object) initValue)
      };
      dbObject.SetAttributesValues(valuesList, false, false);
      dbObject.CommitCreation(true);
    }
  }
}
