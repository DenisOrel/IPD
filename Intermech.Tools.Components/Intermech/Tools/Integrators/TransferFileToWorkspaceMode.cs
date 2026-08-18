// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.TransferFileToWorkspaceMode
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>Тип импорта файлов в рабочую область</summary>
public enum TransferFileToWorkspaceMode
{
  /// <summary>Импорт отключен</summary>
  None,
  /// <summary>Включает импорт в режиме только выбранного файла</summary>
  SourceFileOnly,
  /// <summary>
  /// Включает импорт в режиме выбранного файла и всех файлов в данной папке отличающихся только расширением
  /// </summary>
  FilesByMask,
}
