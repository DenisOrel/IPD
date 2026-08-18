
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.IAfterObjectCreatorDialogHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

internal interface IAfterObjectCreatorDialogHandler
{
  /// <summary>Вызывается у обработчиков по очереди.</summary>
  /// <param name="newObject"></param>
  /// <param name="itemIndex"></param>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  /// <returns>Если с объектом в обработчике произошли изменения вернуть True.</returns>
  bool Handle(
    IDBObject newObject,
    int itemIndex,
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo);
}
