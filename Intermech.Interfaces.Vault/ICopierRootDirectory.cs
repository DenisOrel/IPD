// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.ICopierRootDirectory
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>Класс для перемещения корневого хранилища.</summary>
public interface ICopierRootDirectory
{
  /// <summary>событие -  завершение индексации копируемых файлов</summary>
  event EventHandler IndexCompletedEvent;

  /// <summary>событие - копирования файла (создание папки)</summary>
  event EventHandler ItemMoveEvent;

  /// <summary>событие - при пермещении возникла ошибка</summary>
  event EventHandler MoveErrorEvent;

  /// <summary>событие  - перемещение завершено</summary>
  event EventHandler MoveCompleteEvent;

  /// <summary>состояние задачи копирования</summary>
  CopierState CopierState { get; }

  string SourceDirectoryPath { get; }

  /// <summary>начать перемещение хранилища</summary>
  void StartDirectoryReplace();

  /// <summary>отменить перемещение хранилища</summary>
  void Cancel();
}
