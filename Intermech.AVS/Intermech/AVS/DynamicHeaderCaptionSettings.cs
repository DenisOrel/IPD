// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DynamicHeaderCaptionSettings
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Output;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using System;

#nullable disable
namespace Intermech.AVS;

public class DynamicHeaderCaptionSettings : OutputAttributeMappingScheme, ICloneable
{
  private const string CaptionCellId = "HeaderCaption";
  private static CellOutputMapping _defMapping;

  public DynamicHeaderCaptionSettings()
    : base((OutputAttributeMappingScheme) null, -1L, (SettingsLevel) null)
  {
    this.UpdateXml();
  }

  public bool Changed { get; internal set; }

  public void LoadDefaultSettings()
  {
    this.CellMaping.Clear();
    this.SetCellMapping(DynamicHeaderCaptionSettings.DefaultMapping);
    this.UpdateXml();
  }

  private static CellOutputMapping DefaultMapping
  {
    get
    {
      if (DynamicHeaderCaptionSettings._defMapping == null)
      {
        DynamicHeaderCaptionSettings._defMapping = new CellOutputMapping()
        {
          CellId = "HeaderCaption",
          SectionGuid = "00000000-0000-0000-0000-000000000000",
          ObjTypeGuid = "00000000-0000-0000-0000-000000000000"
        };
        DynamicHeaderCaptionSettings._defMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrClassGuid, -1, (string) null)));
        DynamicHeaderCaptionSettings._defMapping.Add((OutputMappingBase) DelimiterMapping.Default.Clone());
        DynamicHeaderCaptionSettings._defMapping.Add((OutputMappingBase) new AttributeMapping((AttributeInfo) new AvsRowAttributeInfo(FieldSource.Object, AvsIDCache.AttrGostGuid, -1, (string) null)));
      }
      return DynamicHeaderCaptionSettings._defMapping;
    }
  }

  public override void LoadParams()
  {
  }

  public override void SaveParams()
  {
  }

  internal CellOutputMapping GetCellMapping()
  {
    return this.GetCellMapping("00000000-0000-0000-0000-000000000000", "HeaderCaption", "00000000-0000-0000-0000-000000000000");
  }

  public string СoncatenateAttributesValues(
    GetFieldValueByCellOutputMapping GetFieldStringValue)
  {
    return this.GetCellMapping()?.ConcatenateAttributesValues(GetFieldStringValue);
  }

  public override string ToString() => this.GetPreviewStringForCellId(this.GetCellMapping());

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public DynamicHeaderCaptionSettings Clone()
  {
    DynamicHeaderCaptionSettings headerCaptionSettings = new DynamicHeaderCaptionSettings();
    headerCaptionSettings.CopyParamsFrom((OutputAttributeMappingScheme) this);
    return headerCaptionSettings;
  }
}
