// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.ResolutionParentType
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>В контексте чего создано поручение - документа, поручения, либо создано без связи с чем либо</summary>
public enum ResolutionParentType
{
  None,
  Document,
  Resolution,
}
