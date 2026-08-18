// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Synchronization.TableCompareState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase.Server.Synchronization;

internal class TableCompareState : BaseCompareState
{
  public override void Handle(SynchronizationAttributesAnalyzer context)
  {
    long num = context.Session.GetObjectInfo(context.ImbaseObjectId).ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID ? TableLoadHelper.GetTableReference(context.Session, context.ImbaseObjectId) : context.ImbaseObjectId;
    IDBObject dbObject = context.Session.GetObject(num);
    context.Log.AddMessage(MessageType.Extended, $"{Environment.NewLine}Анализ атрибутов объекта {dbObject.NameInMessages} [{dbObject.ObjectID}].");
    this.CompareWithObject(context, num);
    if (context.FinishAnalyze)
      return;
    context.State = (IAttributeAnalyzerState) new HierarchyCompareState();
    context.Analyze();
  }
}
