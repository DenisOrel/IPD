
// Type: Intermech.Holders.FoldersRecentHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Holders;

/// <summary>
/// Контейнер хранения истории путей сохранения файлов на локальной машине для актуального пользователя
/// </summary>
public class FoldersRecentHolder : RecentHolder
{
  public FoldersRecentHolder() => this.paramName = "RecentSaveFolders";
}
