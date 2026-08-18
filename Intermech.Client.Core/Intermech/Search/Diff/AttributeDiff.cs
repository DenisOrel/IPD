
// Type: Intermech.Search.Diff.AttributeDiff
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Search.Diff;

public sealed class AttributeDiff : DiffBase
{
  public AttributeDiff(int attributeTypeID, DiffOperand firstOperand, DiffOperand secondOperand)
    : base(AttributeDiff.ChangeFirstOperand(firstOperand, secondOperand), AttributeDiff.ChangeSecondOperand(firstOperand, secondOperand))
  {
    this.AttributeTypeID = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? attributeTypeID : throw new ArgumentException();
  }

  public int AttributeTypeID { get; private set; }

  private static DiffOperand ChangeFirstOperand(DiffOperand firstOperand, DiffOperand secondOperand)
  {
    if (firstOperand != null && firstOperand.Value is IList)
    {
      if (!(firstOperand.Value is IList first))
        first = (IList) new List<object>();
      if (secondOperand == null)
        second = (IList) new List<object>();
      else if (!(secondOperand.Value is IList second))
        second = (IList) new List<object>();
      return new DiffOperand((object) new ListItemDiffCollection(first, second));
    }
    return firstOperand != null && secondOperand != null && firstOperand.Value is BlobInfo && secondOperand.Value is BlobInfo ? new DiffOperand((object) new PropertyDiffCollection(firstOperand.Value, secondOperand.Value)) : firstOperand;
  }

  private static DiffOperand ChangeSecondOperand(
    DiffOperand firstOperand,
    DiffOperand secondOperand)
  {
    if (secondOperand != null && secondOperand.Value is IList)
    {
      if (!(secondOperand.Value is IList first))
        first = (IList) new List<object>();
      if (firstOperand == null)
        second = (IList) new List<object>();
      else if (!(firstOperand.Value is IList second))
        second = (IList) new List<object>();
      return new DiffOperand((object) new ListItemDiffCollection(first, second));
    }
    return firstOperand != null && secondOperand != null && firstOperand.Value is BlobInfo && secondOperand.Value is BlobInfo ? new DiffOperand((object) new PropertyDiffCollection(secondOperand.Value, firstOperand.Value)) : secondOperand;
  }
}
