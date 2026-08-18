
// Type: Intermech.Navigator.Conditions.AdditionalDateTimeControlParameters
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

internal sealed class AdditionalDateTimeControlParameters
{
  public DateTimePickerFormat Format { get; private set; }

  public string FormatString { get; private set; }

  public bool CurrentDateEnable { get; private set; }

  public AdditionalDateTimeControlParameters(
    DateTimePickerFormat format,
    string formatString,
    bool currentDateEnable)
  {
    this.Format = format;
    this.FormatString = formatString;
    this.CurrentDateEnable = currentDateEnable;
  }
}
