
// Type: Intermech.Client.Core.FormDesigner.Actions.ContextCommand.ContextCommandActionConsts
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;


namespace Intermech.Client.Core.FormDesigner.Actions.ContextCommand;

/// <summary>
/// Consts for ContextCommandAction/ContextCommandActionParams
/// </summary>
internal static class ContextCommandActionConsts
{
  /// <summary>Guid for context command action</summary>
  public static Guid ActionGuid = new Guid("{1FC2D497-14BF-4EB2-AFBC-58181B59A2DA}");
  /// <summary>Caption for context command action</summary>
  public static readonly string ActionCaption = LocalizationHolder.rm.GetString("FormDesigner_Action_ContextCommand");
}
