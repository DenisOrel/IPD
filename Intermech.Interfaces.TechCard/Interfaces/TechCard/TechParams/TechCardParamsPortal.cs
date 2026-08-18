// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechParams.TechCardParamsPortal
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;

#nullable disable
namespace Intermech.Interfaces.TechCard.TechParams;

/// <summary>Класс для хранения параметров ТechCard для портала</summary>
[Serializable]
public class TechCardParamsPortal : TechCardParamsBase
{
  /// <summary>
  /// Режим автоматической привязки технологических данных при импорте изделий через портал
  /// </summary>
  public TechCardParams.PortalSourceSystemType AutoLinkArticleMode { get; set; }
}
