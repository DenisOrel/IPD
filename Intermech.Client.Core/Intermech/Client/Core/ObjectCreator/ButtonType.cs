
// Type: Intermech.Client.Core.ObjectCreator.ButtonType
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.ObjectCreator;

/// <summary>Тип кнопки</summary>
[Flags]
public enum ButtonType
{
  /// <summary>
  /// 
  /// </summary>
  None = 0,
  /// <summary>Далее</summary>
  Next = 1,
  /// <summary>Назад</summary>
  Back = 2,
  /// <summary>Готово</summary>
  Finish = 4,
}
