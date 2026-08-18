// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.EditorSettings
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Map;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Workflow.Design;

[Serializable]
public class EditorSettings : ISerializable
{
  private static readonly string gridCategory = LocalizationHolder.rm.GetString("Workflow.Design_141");
  private MapViewGridStyle _gridStyle;
  private SizeF _gridCellSize = new SizeF(50f, 50f);
  public Color _gridColor = Color.LightGray;
  public MapViewSnapStyle _gridSnapDrag;
  public Color _backColor = Color.White;
  public static bool Loaded = false;

  [CustomCategory("Attribute.Workflow.Design_1")]
  [CustomDisplayName("Attribute.Workflow.Design_3")]
  [DefaultValue(0)]
  [TypeConverter(typeof (EnumDescConverter))]
  public MapViewGridStyle GridStyle
  {
    get => this._gridStyle;
    set => this._gridStyle = value;
  }

  [CustomCategory("Attribute.Workflow.Design_1")]
  [CustomDisplayName("Attribute.Workflow.Design_4")]
  [CustomDescription("Attribute.Workflow.Design_5")]
  public SizeF GridCellSize
  {
    get => this._gridCellSize;
    set => this._gridCellSize = value;
  }

  [CustomCategory("Attribute.Workflow.Design_1")]
  [CustomDisplayName("Attribute.Workflow.Design_6")]
  [CustomDescription("Attribute.Workflow.Design_7")]
  [DefaultValue(0)]
  public Color GridColor
  {
    get => this._gridColor;
    set => this._gridColor = value;
  }

  [CustomCategory("Attribute.Workflow.Design_1")]
  [CustomDisplayName("Attribute.Workflow.Design_8")]
  [CustomDescription("Attribute.Workflow.Design_9")]
  [DefaultValue(0)]
  [TypeConverter(typeof (EnumDescConverter))]
  public MapViewSnapStyle GridSnapDrag
  {
    get => this._gridSnapDrag;
    set => this._gridSnapDrag = value;
  }

  [CustomCategory("Attribute.Workflow.Design_10")]
  [CustomDisplayName("Attribute.Workflow.Design_11")]
  [CustomDescription("Attribute.Workflow.Design_12")]
  [DefaultValue(0)]
  public Color BackColor
  {
    get => this._backColor;
    set => this._backColor = value;
  }

  public void Save()
  {
    EditorSettings.Loaded = true;
    if (!(ApplicationServices.Container.GetService(typeof (IDBConfigurations)) is IDBConfigurations service))
      return;
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) this);
      BlobInformation config_info = new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, "wfEditorSettings", ArcMethods.NotPacked, "b");
      service.WriteConfigData(config_info, serializationStream.ToArray());
    }
  }

  public static EditorSettings Load()
  {
    EditorSettings editorSettings = (EditorSettings) null;
    try
    {
      if (ApplicationServices.Container.GetService(typeof (IDBConfigurations)) is IDBConfigurations service)
      {
        byte[] config_file;
        service.LoadConfigData("wfEditorSettings", out BlobInformation _, out config_file);
        if (config_file.Length != 0)
        {
          MemoryStream memoryStream = new MemoryStream(config_file);
          memoryStream.Position = 0L;
          using (MemoryStream serializationStream = memoryStream)
            editorSettings = new BinaryFormatter().Deserialize((Stream) serializationStream) as EditorSettings;
          EditorSettings.Loaded = true;
        }
      }
    }
    catch
    {
    }
    return editorSettings ?? new EditorSettings();
  }

  public void SetProperties(GraphView view)
  {
    if (!EditorSettings.Loaded)
      return;
    view.BackColor = this.BackColor;
    view.GridCellSize = this.GridCellSize;
    view.GridColor = this.GridColor;
    view.GridSnapDrag = this.GridSnapDrag;
    view.GridStyle = this.GridStyle;
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("GridStyle", (int) this.GridStyle);
    info.AddValue("GridCellSize", (object) this.GridCellSize);
    info.AddValue("GridColor", (object) this.GridColor);
    info.AddValue("GridSnapDrag", (int) this.GridSnapDrag);
    info.AddValue("BackColor", (object) this.BackColor);
  }

  public EditorSettings(SerializationInfo info, StreamingContext ctxt)
    : this()
  {
    try
    {
      this.GridStyle = (MapViewGridStyle) info.GetValue(nameof (GridStyle), typeof (int));
      this.GridCellSize = (SizeF) info.GetValue(nameof (GridCellSize), typeof (SizeF));
      this.GridColor = (Color) info.GetValue(nameof (GridColor), typeof (Color));
      this.GridSnapDrag = (MapViewSnapStyle) info.GetValue(nameof (GridSnapDrag), typeof (int));
      this.BackColor = (Color) info.GetValue(nameof (BackColor), typeof (Color));
    }
    catch
    {
    }
  }

  public EditorSettings() => this.GridStyle = MapViewGridStyle.Dot;
}
