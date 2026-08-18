// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.ProcRoutePasteItemsCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces.Compositions;
using Intermech.TechCard.Client.Commands;
using System;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

internal class ProcRoutePasteItemsCommand : PasteCommand
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetObjInfo"></param>
  /// <returns></returns>
  protected override bool CheckTargetObjectAllowModification(ObjInfoItem targetObjInfo)
  {
    ProcRouteAddItemsCommand.CheckProcRouteObjectAllowCompositionModification(targetObjInfo, this._clipBoardObjects.Select<ClipboardObject, IDBTypedObjectID>((Func<ClipboardObject, IDBTypedObjectID>) (item => (IDBTypedObjectID) item)));
    return base.CheckTargetObjectAllowModification(targetObjInfo);
  }

  public ProcRoutePasteItemsCommand()
    : base()
  {
  }
}
