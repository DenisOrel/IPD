// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ElementStatusesService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services;

[Serializable]
public sealed class ElementStatusesService : LongLifeObject, IElementStatusesService
{
  internal int FBitsCount;
  internal Dictionary<string, ElementStatusesPluginDescription> FPlugins = new Dictionary<string, ElementStatusesPluginDescription>();
  internal Dictionary<string, int> FPluginBits = new Dictionary<string, int>();
  internal Dictionary<string, int> FPluginBitsCount = new Dictionary<string, int>();

  public static void PrepareElementStatusesColumn(ref DataTable source, int columnIndex)
  {
    if (source == null)
      return;
    int index = columnIndex;
    if (index < 0 || index >= source.Columns.Count)
    {
      index = source.Columns.IndexOf("cad005f1-306c-11d8-b4e9-00304f19f545");
      if (index < 0)
        index = source.Columns.IndexOf(ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_ELEMENT_STATUSES));
    }
    if (index < 0)
      return;
    DataColumn column1 = source.Columns[index];
    if (column1.DataType == typeof (byte[]))
      return;
    string columnName1 = column1.ColumnName;
    string caption = column1.Caption;
    string columnName2 = Guid.NewGuid().ToString();
    DataColumn column2 = source.Columns.Add(columnName2, typeof (byte[]));
    int ordinal = column1.Ordinal;
    source.Columns.Remove(column1);
    column2.ColumnName = columnName1;
    column2.Caption = caption;
    column2.SetOrdinal(ordinal);
    if (!(ServerServices.GetService(typeof (IElementStatusesService)) is IElementStatusesService service))
      return;
    int capacity = service.Capacity;
    foreach (DataRow row in (InternalDataCollectionBase) source.Rows)
      row[column2] = (object) new byte[capacity];
  }

  public static int GetStatusesColumnIndex(ref DataTable source)
  {
    if (source == null)
      return -1;
    int statusesColumnIndex = source.Columns.IndexOf("cad005f1-306c-11d8-b4e9-00304f19f545");
    if (statusesColumnIndex < 0)
      statusesColumnIndex = source.Columns.IndexOf(ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_ELEMENT_STATUSES));
    if (statusesColumnIndex < 0)
      statusesColumnIndex = source.Columns.IndexOf(-77.ToString());
    if (statusesColumnIndex < 0)
    {
      for (int index = 0; index < source.Columns.Count; ++index)
      {
        if (source.Columns[index].DataType == typeof (byte[]))
        {
          statusesColumnIndex = index;
          break;
        }
      }
    }
    return statusesColumnIndex;
  }

  public void RegisterServerPlugin(ElementStatusesPluginDescription serverPlugin)
  {
    if (serverPlugin == null || serverPlugin.PluginGuid == string.Empty || serverPlugin.ElementStatesBits <= 0)
      return;
    this.FPlugins[serverPlugin.PluginGuid] = serverPlugin;
    this.FPluginBits[serverPlugin.PluginGuid] = this.FBitsCount;
    this.FPluginBitsCount[serverPlugin.PluginGuid] = serverPlugin.ElementStatesBits;
    this.FBitsCount += serverPlugin.ElementStatesBits;
  }

  public int Capacity => (this.FBitsCount + 7) / 8;

  public int CapacityInBits => this.FBitsCount;

  public Dictionary<string, ElementStatusesPluginDescription> Plugins => this.FPlugins;

  public Dictionary<string, int> PluginBits => this.FPluginBits;

  public Dictionary<string, int> PluginBitsCount => this.FPluginBitsCount;

  public short GetElementStatuses16(string pluginGuid, byte[] elementStatuses)
  {
    if (!this.FPluginBits.ContainsKey(pluginGuid))
      return 0;
    int fpluginBit = this.FPluginBits[pluginGuid];
    return BitsArray.ExtractInt16(elementStatuses, fpluginBit, this.FPluginBitsCount[pluginGuid]);
  }

  public int GetElementStatuses32(string pluginGuid, byte[] elementStatuses)
  {
    if (!this.FPluginBits.ContainsKey(pluginGuid))
      return 0;
    int fpluginBit = this.FPluginBits[pluginGuid];
    return BitsArray.ExtractInt32(elementStatuses, fpluginBit, this.FPluginBitsCount[pluginGuid]);
  }

  public void SetElementStatuses16(string pluginGuid, byte[] elementStatuses, short value)
  {
    if (!this.FPluginBits.ContainsKey(pluginGuid))
      return;
    int fpluginBit = this.FPluginBits[pluginGuid];
    BitsArray.PasteInt16(elementStatuses, value, fpluginBit, this.FPluginBitsCount[pluginGuid]);
  }

  public void SetElementStatuses32(string pluginGuid, byte[] elementStatuses, int value)
  {
    if (!this.FPluginBits.ContainsKey(pluginGuid))
      return;
    int fpluginBit = this.FPluginBits[pluginGuid];
    BitsArray.PasteInt32(elementStatuses, value, fpluginBit, this.FPluginBitsCount[pluginGuid]);
  }
}
