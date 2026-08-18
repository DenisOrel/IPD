// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.Helper.SearchLinksHelper
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync.Helper;

internal class SearchLinksHelper
{
  internal static object GetSearchObjectLinkValue(IUserSession session, object sourceValue)
  {
    if (!(sourceValue is string str))
      return sourceValue;
    int length = str.IndexOf("|");
    long result;
    if (!long.TryParse(length >= 0 ? str.Substring(0, length) : str, out result))
      return sourceValue;
    int[] objTypeIDs = PumpSettings.GetArtTypes() ?? MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00268-306c-11d8-b4e9-00304f19f545")).ToArray();
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.SearchObjDocIdAttrGuid, RelationalOperators.Equal, (object) result, LogicalOperators.NONE, 0)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -12, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DataTable objectData = DataHelper.GetObjectData(objTypeIDs, session, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
    return objectData == null || objectData.Rows.Count == 0 ? sourceValue : (object) Convert.ToString(objectData.Rows[0][0]);
  }

  internal static object GetSearchDocumentLinkValue(IUserSession session, object sourceValue)
  {
    if (!(sourceValue is string str))
      return sourceValue;
    int length = str.IndexOf("|");
    long result;
    if (length <= 0 || !long.TryParse(str.Substring(0, length), out result))
      return sourceValue;
    int[] objTypeIDs = PumpSettings.GetDocTypes() ?? MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545")).ToArray();
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.SearchObjDocIdAttrGuid, RelationalOperators.Equal, (object) result, LogicalOperators.NONE, 0)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -12, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    DataTable objectData = DataHelper.GetObjectData(objTypeIDs, session, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
    return objectData == null || objectData.Rows.Count == 0 ? sourceValue : (object) Convert.ToString(objectData.Rows[0][0]);
  }
}
