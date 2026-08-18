// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IScanerDocumentService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс службы сканирования графических документов</summary>
public interface IScanerDocumentService
{
  /// <summary>Выбрать сканирующее устройство</summary>
  void SelectDevice();

  /// <summary>Сканировать</summary>
  /// <param name="fileExt"></param>
  void AcquireDoc(string fileExt);

  /// <summary>Инициализация службы</summary>
  void Init();

  /// <summary>Событие передачи данных от сканера</summary>
  event EventHandler OnImageTransfer;

  /// <summary>Событие завершения сканирования</summary>
  event EventHandler OnEndScaning;

  /// <summary>навен ли OnImageTransfer null</summary>
  /// <returns></returns>
  bool IsNullOnImageTransfer();
}
