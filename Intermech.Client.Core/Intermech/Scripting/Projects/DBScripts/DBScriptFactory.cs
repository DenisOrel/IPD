
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptFactory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.IO;
using Intermech.Scripting.Common.DesignTime;
using System;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>
/// Класс сервиса для создания новых сценариев, хранящихся в базе данных IPS.
/// Реализация является thread safe.
/// </summary>
public sealed class DBScriptFactory : IScriptProjectFactory
{
  private LanguageInfo csharpLanguageInfo;

  public DBScriptFactory(LanguageInfo csharpLanguageInfo)
  {
    this.csharpLanguageInfo = csharpLanguageInfo != null ? csharpLanguageInfo : throw new ArgumentNullException(nameof (csharpLanguageInfo));
  }

  /// <summary>Создает новый пустой сценарий на указанном языке.</summary>
  /// <param name="languageInfo">Язык сценария</param>
  /// <returns>Объект сценария</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="languageInfo" /> не должен быть равен null</exception>
  public ScriptProject CreateEmptyProject(LanguageInfo languageInfo)
  {
    if (languageInfo == null)
      throw new ArgumentNullException(nameof (languageInfo));
    return languageInfo == this.csharpLanguageInfo ? (ScriptProject) this.CreateEmptyProjectInternal(languageInfo) : throw new ScriptDesignTimeException($"Язык {languageInfo.Name} не поддерживается.");
  }

  /// <summary>Создает новый пустой сценарий на указанном языке.</summary>
  /// <param name="fileExtension">Расширение файла сценария</param>
  /// <returns>Объект сценария</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="fileExtension" /> не должен быть равен null</exception>
  public ScriptProject CreateEmptyProject(string fileExtension)
  {
    if (fileExtension == null)
      throw new ArgumentNullException(nameof (fileExtension));
    if (!PathUtils.IsSamePath(fileExtension, this.csharpLanguageInfo.SourceExtension))
      throw new ScriptDesignTimeException($"Файлы сценариев {fileExtension} не поддерживаются.");
    return (ScriptProject) this.CreateEmptyProjectInternal(this.csharpLanguageInfo);
  }

  private DBScriptProject CreateEmptyProjectInternal(LanguageInfo languageInfo)
  {
    return new DBScriptProject(languageInfo);
  }
}
