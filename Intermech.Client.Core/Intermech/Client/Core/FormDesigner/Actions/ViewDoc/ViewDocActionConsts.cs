
// Type: Intermech.Client.Core.FormDesigner.Actions.ViewDoc.ViewDocActionConsts
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;


namespace Intermech.Client.Core.FormDesigner.Actions.ViewDoc;

/// <summary>Константы действия "Просмотр документа"</summary>
internal static class ViewDocActionConsts
{
  /// <summary>Глобальный идентификатор действия.</summary>
  public static readonly Guid ActionGuid = new Guid("585EC975-088C-46C2-A69B-AAB7386EE72B");
  /// <summary>Caption for context command action</summary>
  public static readonly string ActionCaption = LocalizationHolder.GetString("FormDesigner_Action_ViewDoc");
  /// <summary>
  /// Команда контекстного меню выполняемая данным действием "Просмотр документа".
  /// </summary>
  public const string ActionCommand = "ViewDocument";
}
