
// Type: Intermech.Search.Diff.PropertyDiff
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Reflection;


namespace Intermech.Search.Diff;

public sealed class PropertyDiff : DiffBase
{
  public PropertyDiff(
    PropertyInfo propertyInfo,
    DiffOperand fistOperand,
    DiffOperand secondOperand)
    : base(fistOperand, secondOperand)
  {
    this.PropertyInfo = !(propertyInfo == (PropertyInfo) null) ? propertyInfo : throw new ArgumentNullException(nameof (propertyInfo));
  }

  public PropertyInfo PropertyInfo { get; private set; }
}
