// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.PDMHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using System;

#nullable disable
namespace Intermech.Tools.Data;

public static class PDMHelper
{
  public static bool IsArticle(int objectType)
  {
    return DBHelper.IsBasedOnType(objectType, IDCache.Default.AllArticles.Id);
  }

  public static bool IsMaterial(int objectType)
  {
    return DBHelper.IsBasedOnType(objectType, IDCache.Default.AllMaterials.Id);
  }

  public static bool IsDocumentWithArticles(int objectType)
  {
    if (DBHelper.IsBasedOnType(objectType, IDCache.Default.AllDocuments.Id))
    {
      if (DocumentTypeSettingsCache.GetSettings(objectType).OutputObjectTypes.Split(new string[1]
      {
        ","
      }, StringSplitOptions.RemoveEmptyEntries).Length != 0)
        return true;
    }
    return false;
  }
}
