
// Type: Intermech.Client.Core.FormDesigner.External.Classes.ActionsInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;


namespace Intermech.Client.Core.FormDesigner.External.Classes;

/// <summary>
/// 
/// </summary>
internal static class ActionsInfo
{
  public static readonly FormDesignerAction ExternalEditorAction = new FormDesignerAction(new Guid("8C4FC928-43DD-4ae5-83A4-74C951CF6CFA"), LocalizationHolder.rm.GetString("Client.Core_173"), (IFormDesignerActionParams) new ExternalEditorActionParams());
}
