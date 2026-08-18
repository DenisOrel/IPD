// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ColumnsCache
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace Intermech.AVS;

internal static class ColumnsCache
{
  internal static Dictionary<int, string> _cachedAttributesCaptions = new Dictionary<int, string>();
  internal static Dictionary<int, Type> _cachedAttributesTypes = new Dictionary<int, Type>();
  internal static Dictionary<int, FieldTypes> _cachedFieldTypes = new Dictionary<int, FieldTypes>();
  internal static Dictionary<int, AttributeOptions> _cachedAttributeOptions = new Dictionary<int, AttributeOptions>();
  private static int _cacheCounter = 0;

  internal static void StartCache()
  {
    Thread.BeginCriticalRegion();
    if (ColumnsCache._cacheCounter == 0)
    {
      ColumnsCache._cachedAttributesCaptions.Clear();
      ColumnsCache._cachedAttributesTypes.Clear();
      ColumnsCache._cachedFieldTypes.Clear();
      ColumnsCache._cachedAttributeOptions.Clear();
    }
    ++ColumnsCache._cacheCounter;
  }

  internal static void FinishCache()
  {
    if (ColumnsCache._cacheCounter > 0)
      --ColumnsCache._cacheCounter;
    Thread.EndCriticalRegion();
  }
}
