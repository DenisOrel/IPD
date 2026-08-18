// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.IEcoProperties
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using System;

#nullable disable
namespace Intermech.ECO.Client;

public interface IEcoProperties
{
  bool AutoMoveObjects { get; set; }

  bool WarnOnMove { get; set; }

  bool WriteComplect { get; set; }

  bool WriteDesOnReplace { get; set; }

  string KIInventoryNumberTemplate { get; set; }

  bool LeaveOTDNumberForChange { get; set; }

  bool AutoCheckOut { get; set; }

  bool PlaceInvNum { get; set; }

  string InvNumAttr { get; set; }

  int DaysBeforeEndTermWarning { get; set; }

  bool ShowHidden { get; set; }

  bool AutoOrigSize { get; set; }

  bool CreateLiteraVersion { get; set; }

  bool SetLiteraForFullSostav { get; set; }

  bool MoveAuthenticFiles { get; set; }

  int MaxDocsAllowed { get; set; }

  bool ReplaceEmptyDesignByTemplate { get; set; }

  bool HideOnCreation { get; set; }

  bool ProhibitCustomReason { get; set; }

  bool AskOnNewOrganizations { get; set; }

  bool CheckObjectCreation { get; set; }

  bool NoSlashInDPIDesign { get; set; }

  event EventHandler Changed;
}
