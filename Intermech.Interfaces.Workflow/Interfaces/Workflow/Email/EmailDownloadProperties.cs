// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.Email.EmailDownloadProperties
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Workflow.Email;

/// <summary>Свойства задачи приема почты</summary>
[Serializable]
public class EmailDownloadProperties
{
  /// <summary>Процент выполнения</summary>
  public int Percent;
  /// <summary>Текущее состояние</summary>
  public EmailDownloadState State;
  /// <summary>Ошибка</summary>
  public Exception ErrorException;
  /// <summary>Количество скачанных писем</summary>
  public int CountMessages;
}
