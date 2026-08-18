// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsRowEventMessageViewer
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Класс для формирования различного рода уведомлений при отображении спецификаций
/// </summary>
public class AvsRowEventMessageViewer : IDisposable
{
  private AVSDocument avsDocument;
  private Dictionary<AVSRow, List<AvsRowEventMessage>> dict = new Dictionary<AVSRow, List<AvsRowEventMessage>>();

  public AvsRowEventMessageViewer(AVSDocument doc) => this.avsDocument = doc;

  /// <summary>Окно документа</summary>
  public AVSWindow AVSWindow => this.Document.AVSWindow;

  /// <summary>Документ</summary>
  public AVSDocument Document => this.avsDocument;

  /// <summary>Список уведомлений</summary>
  public Dictionary<AVSRow, List<AvsRowEventMessage>> Events
  {
    get => this.dict;
    set => this.dict = value;
  }

  /// <summary>Добавить событие</summary>
  /// <param name="row">Строка с которой связано событие</param>
  /// <param name="message">Добавляемое событие</param>
  public void AddEvent(AVSRow row, AvsRowEventMessage message)
  {
    List<AvsRowEventMessage> avsRowEventMessageList = new List<AvsRowEventMessage>();
    if (this.dict.ContainsKey(row))
    {
      avsRowEventMessageList = this.dict[row];
      if (message.EventType == AVSEventType.ChangeRow)
      {
        if (avsRowEventMessageList.Find((Predicate<AvsRowEventMessage>) (x => x.EventType == AVSEventType.AddRow)) != null)
          return;
        AvsRowEventMessage avsRowEventMessage = avsRowEventMessageList.Find((Predicate<AvsRowEventMessage>) (x => x.EventType == AVSEventType.ChangeRow && object.Equals((object) x.AttrInfo, (object) message.AttrInfo) && x.ProductIndex == message.ProductIndex));
        if (avsRowEventMessage != null)
        {
          if (avsRowEventMessage.OriginalValue == message.NewValue)
          {
            avsRowEventMessageList.Remove(avsRowEventMessage);
            return;
          }
          avsRowEventMessage.NewValue = message.NewValue;
          return;
        }
      }
      if (message.EventType == AVSEventType.RemoveRow)
        avsRowEventMessageList.RemoveAll((Predicate<AvsRowEventMessage>) (x => x.EventType == AVSEventType.ChangeRow));
    }
    avsRowEventMessageList.Add(message);
    this.dict[row] = avsRowEventMessageList;
    this.Show();
  }

  public EventUserControl EventUserControl
  {
    get => this.AVSWindow != null ? this.AVSWindow.EventUserControl : (EventUserControl) null;
  }

  /// <summary>Показать окно с событиями</summary>
  public void Show(bool show = true)
  {
    if (this.EventUserControl == null)
      return;
    this.EventUserControl.EventsHelper = this;
    if (this.dict.Count > 0)
    {
      this.EventUserControl.UpdateRows();
      if (!show)
        return;
      this.AVSWindow.CreateEventUserControl(true);
    }
    else
    {
      if (!this.EventUserControl.Visible)
        return;
      this.Close();
    }
  }

  /// <summary>Очистить все события</summary>
  public void Clear() => this.dict.Clear();

  public void Close()
  {
    if (this.EventUserControl == null)
      return;
    this.EventUserControl.Close();
  }

  public void Dispose()
  {
    this.avsDocument = (AVSDocument) null;
    this.dict.Clear();
  }
}
