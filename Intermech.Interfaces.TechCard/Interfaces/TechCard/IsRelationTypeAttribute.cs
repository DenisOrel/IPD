// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.IsRelationTypeAttribute
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Custom attribute for relation types</summary>
/// <remarks>For compatibility only</remarks>
[AttributeUsage(AttributeTargets.Field)]
/// <summary>
/// 
/// </summary>
/// <param name="isTechCardType"></param>
internal class IsRelationTypeAttribute(bool isTechCardType) : IsTechCardTypeAttribute(isTechCardType)
{
}
