
// Type: Intermech.Interfaces.Data.Actions.WriteAttributesActionBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Intermech.Interfaces.Data.Actions;

public abstract class WriteAttributesActionBase : IAction
{
  protected readonly AttributeValues[] attrValues;

  public WriteAttributesActionBase(AttributeValues[] attrValues)
  {
    this.attrValues = attrValues != null ? attrValues : throw new ArgumentNullException(nameof (attrValues));
  }

  public void Perform()
  {
    if (this.attrValues.Length == 0)
      return;
    Dictionary<string, Exception> dictionary = this.PerformWrite();
    if (dictionary == null || !UIReport.Enabled)
      return;
    foreach (KeyValuePair<string, Exception> keyValuePair in dictionary)
      UIReport.ReportEvent($"{keyValuePair.Key}: {keyValuePair.Value.Message}", TraceLevel.Warning);
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.GetActionName());
    if (this.attrValues.Length != 0)
    {
      stringBuilder.Append(' ');
      stringBuilder.Append(WriteAttributesActionBase.AttrValuesToString(this.attrValues));
    }
    return stringBuilder.ToString();
  }

  private static string AttrValuesToString(AttributeValues[] attrValues)
  {
    StringBuilder sb = new StringBuilder(attrValues.Length * 32 /*0x20*/);
    sb.Append('{');
    if (attrValues.Length != 0)
    {
      sb.AppendFormat("{0}={1}", (object) WriteAttributesActionBase.AttributeNameToString(attrValues[0]), (object) WriteAttributesActionBase.AttributeValueToString(attrValues[0]));
      WriteAttributesActionBase.AppendMultivalueIndicator(sb, attrValues[0]);
      for (int index = 1; index < attrValues.Length; ++index)
      {
        sb.Append(',');
        sb.Append(' ');
        sb.AppendFormat("{0}={1}", (object) WriteAttributesActionBase.AttributeNameToString(attrValues[index]), (object) WriteAttributesActionBase.AttributeValueToString(attrValues[index]));
        WriteAttributesActionBase.AppendMultivalueIndicator(sb, attrValues[index]);
      }
    }
    sb.Append('}');
    return sb.ToString();
  }

  private static void AppendMultivalueIndicator(StringBuilder sb, AttributeValues attrValue)
  {
    if (attrValue.Values.Length <= 1)
      return;
    sb.Append(' ');
    sb.Append("...");
  }

  private static string AttributeNameToString(AttributeValues attrValue)
  {
    return !string.IsNullOrEmpty(attrValue.AttributeName) ? attrValue.AttributeName : $"#{attrValue.AttributeID}";
  }

  private static string AttributeValueToString(AttributeValues attrValue)
  {
    if (attrValue.Values.Length == 0 || attrValue.Values[0] == null || Convert.IsDBNull(attrValue.Values[0]))
      return "<null>";
    return attrValue.Values[0] is string ? $"'{attrValue.Values[0]}'" : attrValue.Values[0].ToString();
  }

  protected abstract Dictionary<string, Exception> PerformWrite();

  protected abstract string GetActionName();
}
