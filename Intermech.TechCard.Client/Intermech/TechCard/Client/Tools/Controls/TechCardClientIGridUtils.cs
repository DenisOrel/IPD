// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.TechCardClientIGridUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls;

/// <summary>TechCard IGrid utils class</summary>
internal static class TechCardClientIGridUtils
{
  /// <summary>Context menu for TechCard grid's header</summary>
  private static ContextMenu _gridHeaderContextMenu;

  /// <summary>Get context menu</summary>
  /// <returns></returns>
  private static ContextMenu GetHeaderContextMenu()
  {
    if (TechCardClientIGridUtils._gridHeaderContextMenu != null)
      return TechCardClientIGridUtils._gridHeaderContextMenu;
    List<MenuItem> menuItemList = new List<MenuItem>();
    MenuItem menuItem = new MenuItem()
    {
      Text = LocalizationHolder.rm.GetString("TechCard.Client_321")
    };
    menuItem.Click += new EventHandler(TechCardClientIGridUtils.CustomizeHeaderEventHandler);
    menuItemList.Add(menuItem);
    TechCardClientIGridUtils._gridHeaderContextMenu = new ContextMenu(menuItemList.ToArray());
    return TechCardClientIGridUtils._gridHeaderContextMenu;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void CustomizeHeaderEventHandler(object sender, EventArgs e)
  {
    if (sender == null || !((sender is MenuItem menuItem ? menuItem.Parent as ContextMenu : (ContextMenu) null)?.SourceControl is TechCardGrid sourceControl))
      return;
    sourceControl.OnHeaderMenuCustomizeClick(sender, e);
  }

  /// <summary>Обработчик события ShowTreeListMenu</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public static void ShowTechGridHeaderMenu(object sender, iGColHdrMouseUpEventArgs e)
  {
    if (!(sender is TechCardGrid))
      return;
    TechCardClientIGridUtils.GetHeaderContextMenu()?.Show((Control) sender, e.MousePos);
  }

  /// <summary>Загрузка параметров XtraTreeList</summary>
  /// <param name="config"></param>
  /// <param name="grid"></param>
  public static void LoadSettings(IConfiguration config, TechCardGrid grid)
  {
    grid.BeginUpdate();
    try
    {
      grid.Cols.Clear();
      if (config != null && config.HasProperty(grid.Name + "_CollumnsLayout"))
      {
        string property = config.GetProperty(grid.Name + "_CollumnsLayout");
        TechCardClientIGridUtils.SetCollumnsState(grid, property);
      }
      if (grid.Cols.Count == 0)
      {
        IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(-50);
        if (attributeType1 != null)
        {
          iGCol iGcol = grid.Cols.Add();
          iGcol.Text = (object) attributeType1.Name;
          iGcol.Tag = (object) attributeType1.AttributeGuid;
          iGcol.Width = 150;
        }
        Guid attrTypeGuid = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
        IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(attrTypeGuid);
        if (attributeType2 != null)
        {
          iGCol iGcol = grid.Cols.Add();
          iGcol.Text = (object) attributeType2.Name;
          iGcol.Tag = (object) attrTypeGuid;
          iGcol.Width = 150;
        }
        attrTypeGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
        IMSAttributeType attributeType3 = MetaDataHelper.GetAttributeType(attrTypeGuid);
        if (attributeType3 == null)
          return;
        iGCol iGcol1 = grid.Cols.Add();
        iGcol1.Text = (object) attributeType3.Name;
        iGcol1.Tag = (object) attrTypeGuid;
        iGcol1.Width = 150;
      }
      else
      {
        for (int index = grid.Cols.Count - 1; index >= 0; --index)
        {
          if (grid.Cols[index].Tag == null)
            grid.Cols.RemoveAt(index);
        }
      }
    }
    finally
    {
      grid.EndUpdate();
    }
  }

  /// <summary>Сохранение параметров XtraTreeList</summary>
  /// <param name="config"></param>
  /// <param name="grid"></param>
  public static void SaveSettings(IConfiguration config, TechCardGrid grid)
  {
    if (config == null)
      return;
    string collumnsState = TechCardClientIGridUtils.GetCollumnsState(grid);
    config.SetProperty(grid.Name + "_CollumnsLayout", collumnsState);
  }

  /// <summary> Получить строку с состоянием колонок (размеров, порядка следования и т.п.). </summary>
  /// <param name="grid"></param>
  /// <returns></returns>
  public static string GetCollumnsState(TechCardGrid grid)
  {
    if (grid == null || grid.Cols.Count == 0)
      return string.Empty;
    TechCardClientIGridUtils.ColumnPropHolder[] graph = new TechCardClientIGridUtils.ColumnPropHolder[grid.Cols.Count];
    for (int index = 0; index < grid.Cols.Count; ++index)
    {
      iGCol col = grid.Cols[index];
      if (col != null)
      {
        TechCardClientIGridUtils.ColumnPropHolder columnPropHolder = new TechCardClientIGridUtils.ColumnPropHolder(col);
        graph[index] = columnPropHolder;
      }
    }
    try
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter()
        {
          AssemblyFormat = FormatterAssemblyStyle.Simple
        }.Serialize((Stream) serializationStream, (object) graph);
        return Convert.ToBase64String(serializationStream.ToArray());
      }
    }
    catch
    {
      return string.Empty;
    }
  }

  /// <summary>
  ///  <summary> Восстановить состояние колонок (размеров, порядка следования и т.п.). </summary>
  /// </summary>
  /// <param name="grid"></param>
  /// <param name="collumnsState"></param>
  /// <returns></returns>
  public static bool SetCollumnsState(TechCardGrid grid, string collumnsState)
  {
    if (grid != null)
    {
      if (!(collumnsState == string.Empty))
      {
        try
        {
          object obj;
          using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(collumnsState)))
            obj = new BinaryFormatter()
            {
              AssemblyFormat = FormatterAssemblyStyle.Simple
            }.Deserialize((Stream) serializationStream);
          if (obj is Array)
          {
            TechCardClientIGridUtils.ColumnPropHolder[] columnPropHolderArray = (TechCardClientIGridUtils.ColumnPropHolder[]) (obj as Array);
            if (columnPropHolderArray.Length != 0)
            {
              Dictionary<int, iGCol> dictionary = new Dictionary<int, iGCol>();
              foreach (TechCardClientIGridUtils.ColumnPropHolder columnPropHolder in columnPropHolderArray)
              {
                iGCol column = (iGCol) null;
                foreach (iGCol col in (IEnumerable) grid.Cols)
                {
                  if (col != null && col.Tag != null && col.Tag.Equals(columnPropHolder.Tag))
                  {
                    column = col;
                    break;
                  }
                }
                if (column == null)
                  column = grid.Cols.Add();
                columnPropHolder.UpdateColumnData(column);
                dictionary.Add(columnPropHolder.Order, column);
              }
              foreach (KeyValuePair<int, iGCol> keyValuePair in dictionary)
              {
                if (keyValuePair.Key < grid.Cols.Count)
                  keyValuePair.Value.Order = keyValuePair.Key;
              }
              return true;
            }
          }
          return false;
        }
        catch
        {
          return false;
        }
      }
    }
    return false;
  }

  /// <summary>Add object to techcard grid</summary>
  /// <param name="techGrid"></param>
  /// <param name="dbAttributable"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  public static iGRow AddObjectToGrid(
    TechCardGrid techGrid,
    IDBAttributable dbAttributable,
    object data)
  {
    if (techGrid == null || techGrid.Cols.Count == 0 || dbAttributable == null)
      return (iGRow) null;
    iGRow grid = techGrid.Rows.Add();
    grid.Tag = (object) new TechCntrDataHolder(dbAttributable, data);
    AttributeValues[] attributesValues = dbAttributable.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.CheckVisibility | GetAttributeValuesModes.IncludeCaption);
    Dictionary<Guid, AttributeValues> dictionary = new Dictionary<Guid, AttributeValues>();
    foreach (AttributeValues attributeValues in attributesValues)
      dictionary.Add(attributeValues.AttributeGuid, attributeValues);
    foreach (iGCol col in (IEnumerable) techGrid.Cols)
    {
      if (col != null && col.Tag is Guid)
      {
        int index = col.Index;
        if (dictionary.ContainsKey((Guid) col.Tag))
        {
          AttributeValues attributeValues = dictionary[(Guid) col.Tag];
          grid.Cells[index].Value = attributeValues.Descriptions == null || attributeValues.Descriptions.Length == 0 ? (attributeValues.Values == null || attributeValues.Values.Length == 0 ? (object) null : attributeValues.Values[0]) : attributeValues.Descriptions[0];
        }
        else
          grid.Cells[index].Value = (object) null;
      }
    }
    return grid;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="row"></param>
  /// <returns></returns>
  public static object GetRowData(iGRow row)
  {
    if (row == null)
      return (object) null;
    object rowData = row.Tag;
    if (rowData is TechCntrDataHolder)
      rowData = ((TechCntrDataHolder) rowData).Data;
    return rowData;
  }

  /// <summary>TechCardGrid column's property holder</summary>
  [Serializable]
  private sealed class ColumnPropHolder : ISerializable
  {
    /// <summary>Structure version</summary>
    internal static int Version = 1;
    private string _key = string.Empty;
    private object _text;
    private object _tag;
    private int _order;
    private bool _allowGroup;

    /// <summary>Is object serializing</summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static bool IsObjectSerializable(object obj)
    {
      return obj != null && obj.GetType().GetCustomAttributes(typeof (SerializableAttribute), false).Length != 0;
    }

    /// <summary>Constructor</summary>
    /// <param name="column"></param>
    public ColumnPropHolder(iGCol column) => this.AssignData(column);

    /// <summary>Constructor</summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private ColumnPropHolder(SerializationInfo info, StreamingContext context)
    {
      int num = 0;
      foreach (SerializationEntry serializationEntry in info)
      {
        if (serializationEntry.Name == "StructVers")
        {
          num = Convert.ToInt32(serializationEntry.Value);
          break;
        }
      }
      if (num < 1)
        return;
      this._key = info.GetString("Key");
      if (info.GetBoolean("Text_Serializable"))
      {
        System.Type type = System.Type.GetType(info.GetString("Text_Type"));
        if (type != (System.Type) null)
          this._text = info.GetValue("Text_Data", type);
      }
      if (info.GetBoolean("Text_Serializable"))
      {
        System.Type type = System.Type.GetType(info.GetString("Tag_Type"));
        if (type != (System.Type) null)
          this._tag = info.GetValue("Tag_Data", type);
      }
      this._order = info.GetInt32(nameof (Order));
      this.Width = info.GetInt32(nameof (Width));
      this._allowGroup = info.GetBoolean("AllowGroup");
    }

    /// <summary>Assign data</summary>
    /// <param name="column"></param>
    public void AssignData(iGCol column)
    {
      if (column == null)
        return;
      this._key = column.Key;
      this._text = column.Text;
      this._tag = column.Tag;
      this._order = column.Order;
      this.Width = column.Width;
      this._allowGroup = column.AllowGrouping;
    }

    /// <summary>Update column data</summary>
    /// <param name="column"></param>
    public void UpdateColumnData(iGCol column)
    {
      if (column == null)
        return;
      column.Key = this._key;
      column.Text = this._text;
      column.Tag = this._tag;
      column.Width = this.Width;
      column.AllowGrouping = this._allowGroup;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="si"></param>
    /// <param name="ctx"></param>
    public void GetObjectData(SerializationInfo si, StreamingContext ctx)
    {
      si.AddValue("StructVers", TechCardClientIGridUtils.ColumnPropHolder.Version);
      si.AddValue("Key", (object) this._key);
      bool flag1 = TechCardClientIGridUtils.ColumnPropHolder.IsObjectSerializable(this._text);
      si.AddValue("Text_Serializable", flag1);
      if (flag1)
      {
        si.AddValue("Text_Type", (object) this._text.GetType().ToString());
        si.AddValue("Text_Data", this._text, this._text.GetType());
      }
      bool flag2 = TechCardClientIGridUtils.ColumnPropHolder.IsObjectSerializable(this._tag);
      si.AddValue("Tag_Serializable", flag2);
      if (flag2)
      {
        si.AddValue("Tag_Type", (object) this._tag.GetType().ToString());
        si.AddValue("Tag_Data", this._tag, this._tag.GetType());
      }
      si.AddValue("Order", this._order);
      si.AddValue("Width", this.Width);
      si.AddValue("AllowGroup", this._allowGroup);
    }

    /// <summary>
    /// 
    /// </summary>
    public object Tag => this._tag;

    /// <summary>
    /// 
    /// </summary>
    public int Width { get; private set; }

    /// <summary>Get order</summary>
    public int Order => this._order;
  }
}
