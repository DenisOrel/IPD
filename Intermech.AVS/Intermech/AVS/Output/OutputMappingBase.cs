// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.OutputMappingBase
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Xml.Linq;

#nullable disable
namespace Intermech.AVS.Output;

/// <summary>
/// Базовый класс для модели данных схемы вывода атрибутов
/// </summary>
public abstract class OutputMappingBase
{
  public CellOutputMapping Owner { get; set; }

  public string SectionGuid => this.Owner?.SectionGuid ?? string.Empty;

  public string ObjTypeGuid => this.Owner?.ObjTypeGuid ?? string.Empty;

  public string CellId => this.Owner?.CellId ?? string.Empty;

  public int Order { get; set; } = -1;

  internal virtual XElement ToXML() => (XElement) null;
}
