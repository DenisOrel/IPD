// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IndexingException
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
[Serializable]
public class IndexingException : ApplicationException
{
  /// <summary>
  /// Наименование компьютера, на котором запустили выполнение задачи.
  /// </summary>
  public string ComputerName { get; set; }

  /// <summary>Наименование задачи.</summary>
  public string TaskName { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public IndexingException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.ComputerName = info.GetString(nameof (ComputerName));
    this.TaskName = info.GetString(nameof (TaskName));
  }

  /// <summary>Конструктор.</summary>
  /// <param name="msg">Текст сообщения</param>
  /// <param name="innerEx">Системная ошибка</param>
  public IndexingException(string msg, Exception innerEx = null)
    : base(msg, innerEx)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("ComputerName", (object) this.ComputerName);
    info.AddValue("TaskName", (object) this.TaskName);
  }
}
