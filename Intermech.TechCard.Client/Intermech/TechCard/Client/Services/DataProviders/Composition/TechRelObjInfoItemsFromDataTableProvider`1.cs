// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.DataProviders.Composition.TechRelObjInfoItemsFromDataTableProvider`1
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Expert;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.Services.DataProviders.Composition;

/// <summary>
/// Провайдер данных о составе / применяемости для DataTable
/// </summary>
internal class TechRelObjInfoItemsFromDataTableProvider<T> : 
  ITechCardDataEnumerableProvider<T>,
  ITechCardDataProvider<IEnumerable<T>>
  where T : RelObjInfoItem
{
  private int _idxFldLinkId;
  private int _idxFldLinkType;
  private int _idxFldObjectType;
  private int _idxFldPartId;
  private int _idxFldPartObjId;
  private int _idxFldProjObjId;
  /// <summary>Данные о составе, применяемости объекта</summary>
  private readonly DataTable _compositionInfoTable;
  /// <summary>
  /// 
  /// </summary>
  private readonly bool _compositionMode;

  /// <summary>
  /// 
  /// </summary>
  private void ValidateDataTable()
  {
    this._idxFldLinkId = this._compositionInfoTable.Columns.IndexOf("F_PRJLINK_ID");
    if (this._idxFldLinkId == -1)
      throw new Exception("Field 'F_PRJLINK_ID' not found'");
    this._idxFldLinkType = this._compositionInfoTable.Columns.IndexOf("F_RELATION_TYPE");
    if (this._idxFldLinkType == -1)
      throw new Exception("Field 'F_RELATION_TYPE' not found'");
    this._idxFldPartObjId = this._compositionInfoTable.Columns.IndexOf(DataHelper.Consts.cnt_fld_PartObjID);
    if (this._idxFldPartObjId == -1)
    {
      if (this._compositionMode)
        this._idxFldPartObjId = this._compositionInfoTable.Columns.IndexOf("F_OBJECT_ID");
      if (this._idxFldPartObjId == -1)
        throw new Exception($"Field '{DataHelper.Consts.cnt_fld_PartObjID}' not found'");
    }
    this._idxFldProjObjId = this._compositionInfoTable.Columns.IndexOf("F_PROJ_ID");
    if (this._idxFldProjObjId == -1)
      throw new Exception("Field 'F_PROJ_ID' not found'");
    this._idxFldPartId = this._compositionInfoTable.Columns.IndexOf("F_PART_ID");
    this._idxFldObjectType = this._compositionInfoTable.Columns.IndexOf("F_OBJECT_TYPE");
  }

  /// <summary>Создание записи для строки</summary>
  /// <param name="dataRow"></param>
  /// <returns></returns>
  private T CreateItem(DataRow dataRow)
  {
    return Activator.CreateInstance(typeof (T), (object) Convert.ToInt64(dataRow[this._idxFldLinkId]), (object) Convert.ToInt32(dataRow[this._idxFldLinkType])) as T;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="compositionInfoTable"></param>
  public TechRelObjInfoItemsFromDataTableProvider(
    [NotNull] DataTable compositionInfoTable,
    bool compositionMode = true)
  {
    this._compositionMode = compositionMode;
    this._compositionInfoTable = compositionInfoTable;
    this.ValidateDataTable();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerable<T> Execute()
  {
    foreach (DataRow row in (InternalDataCollectionBase) this._compositionInfoTable.Rows)
    {
      ObjInfoIDItem objInfoIdItem1 = new ObjInfoIDItem(Convert.ToInt64(row[this._idxFldPartObjId]));
      if (this._idxFldPartId != -1)
        objInfoIdItem1.ID = Convert.ToInt64(row[this._idxFldPartId]);
      ObjInfoIDItem objInfoIdItem2 = new ObjInfoIDItem(Convert.ToInt64(row[this._idxFldProjObjId]));
      if (this._idxFldObjectType != -1)
      {
        int int32 = Convert.ToInt32(row[this._idxFldObjectType]);
        if (this._compositionMode)
          objInfoIdItem1.ObjTypeID = int32;
        else
          objInfoIdItem2.ObjTypeID = int32;
      }
      T obj = this.CreateItem(row);
      if (!((TypedInfoItem) obj == (TypedInfoItem) null))
      {
        obj.PartInfo = (ObjInfoItem) objInfoIdItem1;
        obj.ProjInfo = (ObjInfoItem) objInfoIdItem2;
        yield return obj;
      }
    }
  }
}
