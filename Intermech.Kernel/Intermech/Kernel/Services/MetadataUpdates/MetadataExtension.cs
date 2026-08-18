// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.MetadataExtension
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class MetadataExtension
{
  public string ParamName;
  public int CategoryType;
  public int InListID;
  public string Value;

  public MetadataExtension(string paramName, int categoryType, int inListId, string value)
  {
    this.ParamName = paramName;
    this.CategoryType = categoryType;
    this.InListID = inListId;
    this.Value = value;
  }
}
