
// Type: Intermech.PropertyEditors.MeasuredValueContainer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.PropertyEditors;

/// <summary>класс сугубо для перекрытия ToString</summary>
public class MeasuredValueContainer(double aValue, long measureID, string caption) : MeasuredValue(aValue, measureID, caption)
{
  public override string ToString() => this.Value.ToString();
}
