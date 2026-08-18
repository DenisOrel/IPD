
// Type: Intermech.PropertyEditors.IntMaxTypeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for IntMaxTypeConverter.</summary>
public class IntMaxTypeConverter : Int64Converter
{
  private long _maxValue;

  public IntMaxTypeConverter()
    : this(0L)
  {
  }

  public IntMaxTypeConverter(long aMaxValue) => this._maxValue = aMaxValue;

  public void SetMaxValue(long aMaxValue) => this._maxValue = aMaxValue;
}
