// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.ResolutionContextInfo
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using System;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Информация о канцелярской входимости поручения</summary>
[Serializable]
public class ResolutionContextInfo
{
  /// <summary>Идентификатор поручения, для которого была получена информация о контексте</summary>
  public readonly long ResolutionID;
  /// <summary>Идентификатор связи типа "Состав канцелярского поручения", для которого была получена информация о контексте</summary>
  public readonly long PrjLinkID;
  /// <summary>Куда входит непосредственно входит поручение - в документ, другое поручение, либо "само по себе", никуда не входит</summary>
  public readonly ResolutionParentType ParentType;
  /// <summary>Тип объекта, в который входит поручение</summary>
  public readonly int ParentObjType;
  /// <summary>ID версии объекта (документа или поручения), в контексте которого создано данное</summary>
  public readonly long ParentObjectVersionID;

  public ResolutionContextInfo(
    long resolutionID,
    long prjLinkID,
    ResolutionParentType parentType,
    int parentObjType,
    long parentObjectVersionID)
  {
    this.ResolutionID = resolutionID;
    this.PrjLinkID = prjLinkID;
    this.ParentType = parentType;
    this.ParentObjType = parentObjType;
    this.ParentObjectVersionID = parentObjectVersionID;
  }
}
