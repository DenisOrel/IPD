// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.CopierState
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>состояние задачи копирования</summary>
public enum CopierState
{
  /// <summary>начало работы</summary>
  Start,
  /// <summary>индексация файлов</summary>
  Indexing,
  /// <summary>создание структуры папок</summary>
  CreateFolders,
  /// <summary>перемещение файлов</summary>
  MoveFiles,
  /// <summary>работа остановлена/завершена</summary>
  Stop,
}
