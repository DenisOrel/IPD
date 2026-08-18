// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SidecarFileResult
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>Результат обновления ассоциированного файла.</summary>
public abstract class SidecarFileResult
{
  /// <summary>Создает объект.</summary>
  /// <param name="isSuccessful">признак, что операция обновления прошла успешно</param>
  protected SidecarFileResult(bool isSuccessful) => this.IsSuccessful = isSuccessful;

  /// <summary>
  /// Возвращает признак, что операция обновления прошла успешно.
  /// </summary>
  public bool IsSuccessful { get; }

  /// <summary>Ассоциированный файл был успешно обновлен.</summary>
  public sealed class Success : SidecarFileResult
  {
    /// <summary>Создает объект</summary>
    /// <param name="filePath">Абсолютный путь к файлу</param>
    public Success(string filePath)
      : base(true)
    {
      this.FilePath = filePath != null ? filePath : throw new ArgumentNullException(nameof (filePath));
    }

    /// <summary>Возвращает абсолютный путь к файлу.</summary>
    public string FilePath { get; }
  }

  /// <summary>Ошибка обновления ассоциированного файла.</summary>
  public sealed class Error : SidecarFileResult
  {
    /// <summary>Создает объект.</summary>
    /// <param name="message">Текст сообщения об ошибке</param>
    public Error(string message)
      : base(false)
    {
      this.Message = message != null ? message : throw new ArgumentNullException(nameof (message));
    }

    /// <summary>Возвращает текст сообщения об ошибке.</summary>
    public string Message { get; }
  }
}
