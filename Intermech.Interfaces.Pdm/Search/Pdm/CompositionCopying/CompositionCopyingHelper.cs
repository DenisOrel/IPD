// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionCopying.CompositionCopyingHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.CompositionCopying;

public static class CompositionCopyingHelper
{
  public static int[] GetAllowableForCreateCopyObjectTypes()
  {
    List<int> source = new List<int>();
    foreach (int copyBaseObjectType in CompositionCopyingConstants.AllowableForCreateCopyBaseObjectTypes)
    {
      source.Add(copyBaseObjectType);
      source.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(copyBaseObjectType));
    }
    return source.Distinct<int>().ToArray<int>();
  }

  public static int[] GetForbiddenForCreateCopyObjectTypes()
  {
    List<int> intList = new List<int>();
    foreach (int copyBaseObjectType in CompositionCopyingConstants.ForbiddenForCreateCopyBaseObjectTypes)
    {
      intList.Add(copyBaseObjectType);
      intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(copyBaseObjectType));
    }
    return intList.ToArray();
  }

  public static bool IsDocument(int objectType)
  {
    List<int> intList = new List<int>();
    intList.Add(CompositionCopyingConstants.DocumentObjectTypeID);
    intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(CompositionCopyingConstants.DocumentObjectTypeID));
    return intList.Contains(objectType);
  }

  public static int[] GetForbiddenForCreateCopyAssociatedWithDocumentElementObjectTypes()
  {
    List<int> intList = new List<int>();
    foreach (int elementBaseObjectType in CompositionCopyingConstants.ForbiddenForCreateCopyAssociatedWithDocumentElementBaseObjectTypes)
    {
      intList.Add(elementBaseObjectType);
      intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(elementBaseObjectType));
    }
    return intList.ToArray();
  }
}
