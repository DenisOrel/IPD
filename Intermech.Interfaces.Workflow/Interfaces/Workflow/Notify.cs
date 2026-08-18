// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.Notify
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

/// <summary>Уведомление об изменениях</summary>
[Serializable]
public class Notify
{
  public long UserID;
  public string UserName;
  public DateTime Date;
  public NotifyOptions Options;
  public List<int> Attributes;
  public string Comment;

  public Notify()
  {
  }

  public Notify(long userID, string userName)
  {
    this.UserID = userID;
    this.UserName = userName;
    this.Date = DateTime.MinValue;
    this.Options = NotifyOptions.None;
    this.Attributes = (List<int>) null;
    this.Comment = string.Empty;
  }

  public Notify(long userID, string userName, NotifyOptions options, List<int> attributes)
  {
    this.UserID = userID;
    this.UserName = userName;
    this.Date = DateTime.MinValue;
    this.Options = options;
    this.Attributes = attributes;
    this.Comment = string.Empty;
  }

  public Notify(
    long userID,
    string userName,
    NotifyOptions options,
    List<int> attributes,
    string comment)
  {
    this.UserID = userID;
    this.UserName = userName;
    this.Date = DateTime.MinValue;
    this.Options = options;
    this.Attributes = attributes;
    this.Comment = comment;
  }

  public Notify(
    long userID,
    string userName,
    DateTime date,
    NotifyOptions options,
    List<int> attributes,
    string comment)
  {
    this.UserID = userID;
    this.UserName = userName;
    this.Date = date;
    this.Options = options;
    this.Attributes = attributes;
    this.Comment = comment;
  }
}
