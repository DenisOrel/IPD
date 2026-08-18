// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ApplicabilityStatusEnum
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Localization;

#nullable disable
namespace Intermech.Imbase.Server;

public enum ApplicabilityStatusEnum
{
  None,
  [ApplicabilityValue("+"), CustomDescription("ApplicabilityNoRestriction")] NoLimit,
  [ApplicabilityValue("!"), CustomDescription("ApplicabilityDenyAddRecord")] ForbiddenUse,
  [ApplicabilityValue("*"), CustomDescription("ApplicabilityDenyAddObject")] LimitedUse,
  [ApplicabilityValue("-"), CustomDescription("ApplicabilityDenyAll")] TotalForbiddenUse,
}
