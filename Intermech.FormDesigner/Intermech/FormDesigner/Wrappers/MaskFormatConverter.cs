// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.MaskFormatConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>MaskFormatConverter для руссификации.</summary>
public class MaskFormatConverter : BaseEnumConverter
{
  /// <summary>Конструктор.</summary>
  public MaskFormatConverter()
    : base(typeof (MaskFormat))
  {
    this._hash.Add((object) MaskFormat.ExcludePromptAndLiterals, (object) LocalizationHolder.rm.GetString("Attribute_DormDesigner_MaskFormat_ExcludePromptAndLiterals"));
    this._hash.Add((object) MaskFormat.IncludeLiterals, (object) LocalizationHolder.rm.GetString("Attribute_DormDesigner_MaskFormat_IncludeLiterals"));
    this._hash.Add((object) MaskFormat.IncludePrompt, (object) LocalizationHolder.rm.GetString("Attribute_DormDesigner_MaskFormat_IncludePrompt"));
    this._hash.Add((object) MaskFormat.IncludePromptAndLiterals, (object) LocalizationHolder.rm.GetString("Attribute_DormDesigner_MaskFormat_IncludePromptAndLiterals"));
  }
}
