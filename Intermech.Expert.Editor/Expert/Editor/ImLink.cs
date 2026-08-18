// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ImLink
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.Expert.Editor;

internal class ImLink
{
  public static readonly ImLink il = new ImLink();
  public IImbaseSelector imSelector;

  public void Init(IServiceProvider locProv)
  {
    this.imSelector = locProv.GetService(typeof (IImbaseSelector)) as IImbaseSelector;
  }

  public long SelectFromRef(string caption, long catId)
  {
    return this.imSelector != null ? this.imSelector.SelectFromCatalog(caption, "", (object) catId, false, false, (int[]) null, -1) : -1L;
  }

  /// <summary>
  /// Get all IMBASE reference folders that can be used to create objects of the type
  /// </summary>
  /// <param name="ius">User Session</param>
  /// <param name="objTypeGUID">Object Type GUID</param>
  /// <returns>Array of Folder IDs</returns>
  internal long[] GetRefsForObjType(IUserSession ius, Guid objTypeGUID)
  {
    DataTable dataTable = ius.GetObjectCollection(ExpertConsts.Consts.objImbaseFolder).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrCreObjType, RelationalOperators.Equal, (object) objTypeGUID.ToString(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    }));
    long[] instance = (long[]) Array.CreateInstance(typeof (long), dataTable.Rows.Count);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      instance[index] = Convert.ToInt64(dataTable.Rows[index][0]);
    return instance;
  }
}
