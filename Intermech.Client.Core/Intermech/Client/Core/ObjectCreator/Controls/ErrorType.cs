
// Type: Intermech.Client.Core.ObjectCreator.Controls.ErrorType
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>тип произошедшей ошибки</summary>
public enum ErrorType
{
  Unknown,
  /// <summary>
  /// ошибок нет, но уйти с текущей страницы нельзя,
  /// потому что не завершены все необходимые проверки
  /// </summary>
  CheckNotCompleted,
}
