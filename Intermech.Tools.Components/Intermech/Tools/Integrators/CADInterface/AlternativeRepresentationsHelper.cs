// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.AlternativeRepresentationsHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.Data;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal static class AlternativeRepresentationsHelper
{
  private static OrderedList<StringKey> jtDocAttributes;

  [MethodImpl(MethodImplOptions.Synchronized)]
  public static ValueBag CopyAttributes(long sourceObjectId, int sourceObjectType)
  {
    if (sourceObjectId == 0L)
      throw new ArgumentException();
    if (sourceObjectType == -1)
      sourceObjectType = DBHelper.GetObjectType(sourceObjectId);
    if (AlternativeRepresentationsHelper.jtDocAttributes == null)
    {
      AlternativeRepresentationsHelper.jtDocAttributes = new OrderedList<StringKey>();
      AlternativeRepresentationsHelper.jtDocAttributes.Add((StringKey) IDCache.Default.Designation.Text);
      AlternativeRepresentationsHelper.jtDocAttributes.Add((StringKey) IDCache.Default.Name.Text);
    }
    ValueBag valueBag = DbOperationsHelper.ReadObjectAttributes((IDBObjectRef) new DirectDBObjectRef(sourceObjectId), (IDBAttributableTypeRef) new DirectObjectAttributesRef(sourceObjectType));
    foreach (ValueRecord valueRecord in valueBag.FindAll((Predicate<ValueRecord>) (attrValue => !AlternativeRepresentationsHelper.jtDocAttributes.Contains(attrValue.Key))))
      valueRecord.Remove();
    ValueRecord valueRecord1 = valueBag.Find((StringKey) IDCache.Default.Designation.Text);
    if (valueRecord1 != null && DBHelper.IsBasedOnType(sourceObjectType, IDCache.Default.AllDocuments.Id))
      valueRecord1.Value = (object) DocumentDesignationHelper.RemoveDocCode((string) valueRecord1.Value, sourceObjectType);
    valueBag.AcceptChanges();
    return valueBag;
  }
}
