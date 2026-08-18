
// Type: Intermech.Navigator.GlobalNode.MyNodeId
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using System;


namespace Intermech.Navigator.GlobalNode;

/// <summary>
/// Наследуемся, чтобы сделать поле данных для значения атрибута.
/// </summary>
internal class MyNodeId : NodeID, IMyStatus
{
  private int myStatus;

  public MyNodeId(int objTypeId, long objId, long id, long checkedOutBy, int myStatus)
    : base(objTypeId, objId, id, checkedOutBy, 0L, -1, string.Empty, -1, 0L, 0L, ObjectFiltrationState.fsNotRequired, 0L, 0L, string.Empty, 0L, Guid.Empty, 0L)
  {
    this.myStatus = myStatus;
  }

  /// <summary>
  /// Свойство для чтения/записи значения атрибута в идентификатор.
  /// </summary>
  public int MyStatus => this.myStatus;
}
