// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.Email.EmailDownloadState
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow.Email;

/// <summary>Состояние задачи приема почты</summary>
public enum EmailDownloadState
{
  /// <summary>Идет прием</summary>
  Downloading,
  /// <summary>Ошибка</summary>
  Error,
  /// <summary>Завершено</summary>
  Completed,
}
