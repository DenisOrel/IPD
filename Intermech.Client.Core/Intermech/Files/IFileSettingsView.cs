
// Type: Intermech.Files.IFileSettingsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Mvp;
using System;


namespace Intermech.Files;

internal interface IFileSettingsView : IView
{
  char DriveLetter { get; set; }

  string SymlinkFolder { get; set; }

  bool LeaveSourcesOfImportedFiles { get; set; }

  void AttachPageChangedHandlers();

  void DetachPageChangedHandlers();

  void EnableDriveLetter(bool enabled);

  void EnableSymlinkFolder(bool enabled);

  void EnableImportOptions(bool enabled);

  /// <summary>Событие изменения какого-либо элемента управления.</summary>
  event EventHandler EditableStateChanged;
}
