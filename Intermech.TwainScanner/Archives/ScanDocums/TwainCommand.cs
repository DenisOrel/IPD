// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwainCommand
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

#nullable disable
namespace Intermech.Archives.ScanDocums;

/// <summary>Команды-ответы сканера</summary>
public enum TwainCommand
{
  /// <summary>Нет сообщения</summary>
  Not = -1, // 0xFFFFFFFF
  /// <summary>сторонее сообщение (левое)</summary>
  Null = 0,
  /// <summary>Готовность к передае данных</summary>
  TransferReady = 1,
  /// <summary>Пользователь отменил передачу данных/запрос</summary>
  CloseRequest = 2,
  /// <summary>Пользователь нажал кнопку "Ок"</summary>
  CloseOk = 3,
  /// <summary>Какое либо сообщение сканера-железа</summary>
  DeviceEvent = 4,
}
