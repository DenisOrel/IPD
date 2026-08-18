// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.Common.HelperConsts
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.Common;

internal class HelperConsts
{
  public static int ObjtypeDocument { get; private set; }

  public static List<int> ComparedObjectTypes { get; private set; }

  public static void Initialize()
  {
    HelperConsts.ObjtypeDocument = MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
    HelperConsts.ComparedObjectTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(HelperConsts.ObjtypeDocument);
  }
}
