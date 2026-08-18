// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CorrectDesignationSuffixAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Tools.Data;

#nullable disable
namespace Intermech.Tools.Integrators;

public class CorrectDesignationSuffixAction(ValueBag target, StringKey targetKey, int documentType) : 
  RemoveBadDesignationSuffixAction(target, targetKey, documentType)
{
  protected override string ChangeSuffix(string designation)
  {
    designation = base.ChangeSuffix(designation);
    return this.documentType == -1 ? designation : DocumentDesignationHelper.AppendDocCode(designation, this.documentType);
  }
}
