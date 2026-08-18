// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.SuccessiveResolutionProcess
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Office.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Intermech.Office.Server;

internal class SuccessiveResolutionProcess([NotNull] string name, bool controlResolution) : 
  ResolutionProcess(Intermech.Diagnostics.Check.ArgumentNotNull<string>(name, nameof (name)), controlResolution)
{
  protected override void Initialize([NotNull] OrderProcessTemplates processTemplates)
  {
    this._ProcessTemplate = this._Control ? processTemplates.SuccessiveControl : processTemplates.SuccessiveNoControl;
  }

  protected override void OnExecute(
    IUserSession session,
    [NotNull] IDBObject resolution,
    [NotNull] IProcess process,
    [NotNull] IList<long> executorIDs)
  {
    IDBAttribute dbAttribute = resolution.GetAttributeByID(OfficeConsts.AttrExecutionOrderID);
    if (dbAttribute == null)
    {
      dbAttribute = resolution.Attributes.AddAttribute(OfficeConsts.AttrExecutionOrderID, false);
      for (int newValue = 1; newValue <= executorIDs.Count; ++newValue)
      {
        if (newValue == 1)
          dbAttribute.Value = (object) newValue;
        else
          dbAttribute.AddValue((object) newValue);
      }
    }
    if (dbAttribute.ValuesCount != executorIDs.Count)
      throw new Exception("Неверно указан порядок исполнения поручения.");
    SortedDictionary<long, List<long>> sortedDictionary = new SortedDictionary<long, List<long>>();
    for (int index = 0; index < dbAttribute.ValuesCount; ++index)
    {
      dbAttribute.Index = index;
      if (dbAttribute.IsNull)
        throw new Exception("Отсутствует значение у атрибута Порядок исполнения.");
      List<long> longList;
      if (!sortedDictionary.TryGetValue(dbAttribute.AsInteger, out longList))
      {
        longList = new List<long>();
        sortedDictionary.Add(dbAttribute.AsInteger, longList);
      }
      longList.Add(executorIDs[index]);
    }
    StringBuilder stringBuilder = new StringBuilder();
    int num = 0;
    foreach (KeyValuePair<long, List<long>> keyValuePair in sortedDictionary)
    {
      if (num > 0)
        stringBuilder.Append(';');
      for (int index = 0; index < keyValuePair.Value.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(',');
        stringBuilder.Append(keyValuePair.Value[index]);
      }
      ++num;
    }
    IVariable variable = process.StartActivity.Variables.Find("EXECUTION_ORDER");
    if (variable == null)
      throw new VariableMissingException("EXECUTION_ORDER");
    variable.Value = stringBuilder.ToString();
  }
}
