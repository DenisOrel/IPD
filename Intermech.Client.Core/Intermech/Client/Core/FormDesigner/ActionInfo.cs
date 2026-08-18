
// Type: Intermech.Client.Core.FormDesigner.ActionInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>
/// 
/// </summary>
internal static class ActionInfo
{
  public static readonly FormDesignerAction NoneAction = FormDesignerAction.Empty;
  public static readonly FormDesignerAction ApplyAction = new FormDesignerAction(new Guid("81B5AAFD-29B1-48bb-BD1C-F6EE7E3C87B7"), LocalizationHolder.rm.GetString("Client.Core_167"));
  public static readonly FormDesignerAction CancelAction = new FormDesignerAction(new Guid("242A6AF1-80B1-4cc7-9E84-7C45D8ABDAD4"), LocalizationHolder.rm.GetString("Client.Core_166"));
  public static readonly FormDesignerAction ClassifyAction = new FormDesignerAction(new Guid("9c9a974e-5067-4425-bb92-eab0b07e170d"), LocalizationHolder.rm.GetString("Client.Core_168"));
  public static readonly FormDesignerAction HelpAction = new FormDesignerAction(new Guid("642EA80D-4F7A-4483-9FD8-6C18D5F03F3E"), LocalizationHolder.rm.GetString("FormDesigner_HelpAction_ActionName"));
  public static readonly FormDesignerAction CheckOutAction = new FormDesignerAction(new Guid("DA8AF301-DC6B-4051-BA4A-580D78922CD9"), LocalizationHolder.rm.GetString("Client.Core_474"));
  public static readonly FormDesignerAction CheckInAction = new FormDesignerAction(new Guid("35DCFB0D-0BBF-48fc-B85C-4048156B5AFB"), LocalizationHolder.rm.GetString("Client.Core_475"));
  public static readonly FormDesignerAction ScriptForButtonsAction = new FormDesignerAction(new Guid("A66AB5E8-F25E-45F5-875A-98613961E91D"), LocalizationHolder.rm.GetString("ClientCore_Script_Run"), (IFormDesignerActionParams) new ScriptForButtonsActionParams());
}
