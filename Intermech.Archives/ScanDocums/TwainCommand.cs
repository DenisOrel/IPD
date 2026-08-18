// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwainCommand
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

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
