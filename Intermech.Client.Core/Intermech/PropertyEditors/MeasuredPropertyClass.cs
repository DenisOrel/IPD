
// Type: Intermech.PropertyEditors.MeasuredPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Data;


namespace Intermech.PropertyEditors;

[Serializable]
public class MeasuredPropertyClass : StringPropertyClass
{
  public MeasuredPropertyClass()
    : base(string.Empty)
  {
  }

  public MeasuredPropertyClass(string aString)
    : base(aString)
  {
  }

  public MeasuredPropertyClass(
    string aString,
    string aDescription,
    DataTable aPossibleValuesDataTable)
    : base(aString, aDescription, aPossibleValuesDataTable)
  {
  }
}
