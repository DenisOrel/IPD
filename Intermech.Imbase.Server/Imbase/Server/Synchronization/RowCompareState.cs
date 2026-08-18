// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Synchronization.RowCompareState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Synchronization;

internal class RowCompareState : BaseCompareState
{
  public override void Handle(SynchronizationAttributesAnalyzer context)
  {
    IDBObject dbObject = context.Session.GetObject(context.ImbaseObjectId, false);
    DataRow recordRow = ImbaseServer.GetRecordRow(context.Session, context.ImbaseObjectId, context.ImbaseRecordId, false);
    if (dbObject == null || recordRow == null)
      return;
    context.Log.AddMessage(MessageType.Extended, Environment.NewLine + "Получение списка изменившихся атрибутов.");
    IDBAttribute attributeByGuid1 = context.SourceObject.GetAttributeByGuid(Intermech.Imbase.Consts.ImbaseObjectRefAttGUID);
    if (attributeByGuid1 == null || attributeByGuid1.AsInteger != context.ImbaseObjectId)
    {
      AttributeValues attributeValues = new AttributeValues(Intermech.Imbase.Consts.ImbaseObjectRefAttID, (object) context.ImbaseObjectId)
      {
        AttributeName = MetaDataHelper.GetAttributeTypeName(Intermech.Imbase.Consts.ImbaseObjectRefAttID)
      };
      context.DifferentAttributeValues.Add(attributeValues);
      context.Log.AddMessage(MessageType.Extended, $"Атрибут {attributeValues.AttributeName} имеет некорректное значение.");
    }
    IDBAttribute attributeByGuid2 = context.SourceObject.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid2 == null || attributeByGuid2.AsInteger != context.ImbaseRecordId)
    {
      AttributeValues attributeValues = new AttributeValues(MetaDataHelper.GetAttributeTypeID("cad0020f-306c-11d8-b4e9-00304f19f545"), (object) context.ImbaseRecordId)
      {
        AttributeName = MetaDataHelper.GetAttributeTypeName(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"))
      };
      context.DifferentAttributeValues.Add(attributeValues);
      context.Log.AddMessage(MessageType.Extended, $"Атрибут {attributeValues.AttributeName} имеет некорректное значение.");
    }
    context.Log.AddMessage(MessageType.Extended, $"{Environment.NewLine}Анализ атрибутов строки №{context.ImbaseRecordId} объекта {dbObject.NameInMessages} [{dbObject.ObjectID}].");
    this.CompareWithRow(context, recordRow);
    if (context.FinishAnalyze)
      return;
    context.State = (IAttributeAnalyzerState) new TableCompareState();
    context.Analyze();
  }
}
