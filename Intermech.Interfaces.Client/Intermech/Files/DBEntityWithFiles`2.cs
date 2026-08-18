// Decompiled with JetBrains decompiler
// Type: Intermech.Files.DBEntityWithFiles`2
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Files;

/// <summary>Описывает список файлов и их владельца.</summary>
/// <typeparam name="TEntity">Тип объекта-владельца файлов</typeparam>
/// <typeparam name="TFile">Тип записи о файле</typeparam>
public class DBEntityWithFiles<TEntity, TFile>
{
  private TEntity owner;
  private List<TFile> files;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Объект-владелец файлов</param>
  /// <param name="files">Список файлов</param>
  /// <exception cref="T:ArgumentNullException">files</exception>
  public DBEntityWithFiles(TEntity owner, List<TFile> files)
  {
    if (files == null)
      throw new ArgumentNullException(nameof (files));
    this.owner = owner;
    this.files = files;
  }

  /// <summary>Возвращает владельца файлов.</summary>
  public TEntity Owner
  {
    [DebuggerStepThrough] get => this.owner;
  }

  /// <summary>Возвращает список файлов.</summary>
  public List<TFile> Files
  {
    [DebuggerStepThrough] get => this.files;
  }
}
