// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.IEcoPropHolder
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

#nullable disable
namespace Intermech.ECO.Server;

public interface IEcoPropHolder
{
  bool AutoMoveObjects { get; set; }

  bool WarnOnMove { get; set; }

  bool WriteComplect { get; set; }

  bool WriteDesOnReplace { get; set; }

  bool LeaveOTDNumberForChange { get; set; }

  bool AutoCheckOut { get; set; }

  int DaysBeforeEndTermWarning { get; set; }

  bool PlaceInvNum { get; set; }

  string InvNumAttr { get; set; }

  bool HideHiddenObjects { get; set; }

  bool AutoOriginalSize { get; set; }

  bool CreateLiteraVersion { get; set; }

  bool SetLiteraForFullSostav { get; set; }

  bool MoveAuthenticFiles { get; set; }

  int MaxDocNum { get; set; }

  bool ReplaceEmptyDesByTemplate { get; set; }

  bool ProhibitCustomReason { get; set; }

  bool AskOnNewOrganizations { get; set; }

  bool CheckObjectCreation { get; set; }

  bool NoSlashInDPIDesign { get; set; }
}
