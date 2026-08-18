
// Type: Intermech.Client.Core.SelectObjectCompositionAutoload
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core;

/// <summary>Автоматическая загрузка состава при открытии формы</summary>
public enum SelectObjectCompositionAutoload
{
  /// <summary>Не загружать состав вообще</summary>
  None,
  /// <summary>Загружать полный состав</summary>
  Full,
  /// <summary>Загружать состав на определённую глубину</summary>
  Depth,
}
