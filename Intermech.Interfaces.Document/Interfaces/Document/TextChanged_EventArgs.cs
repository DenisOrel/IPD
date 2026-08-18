// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.TextChanged_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Данные события TextChanged</summary>
public class TextChanged_EventArgs : EventArgs
{
  /// <summary>Изменения не влияющие на дату модификации документа</summary>
  public bool SaveModificationDate;
  /// <summary>Обновить редактор если он активен</summary>
  public bool UpdateActiveEditor;
  /// <summary>Новый текст</summary>
  public string NewText;
  /// <summary>Старый текст</summary>
  public string OldText;
  /// <summary>Сбросить RTF</summary>
  public bool ClearRTF;
  /// <summary>Обновить интерфейс</summary>
  public bool UpdateUI;
  /// <summary>Обновить разбивку</summary>
  public bool UpdateLayout;

  /// <summary>Конструктор</summary>
  /// <param name="newText">Новый текст</param>
  /// <param name="clearRTF">Сбросить RTF</param>
  /// <param name="updateActiveEditor">Обновить редактор если он активен</param>
  /// <param name="saveModificationDate">Изменения не влияющие на дату модификации документа</param>
  /// <param name="updateUI">Обновить интерфейс</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public TextChanged_EventArgs(
    string oldText,
    string newText,
    bool clearRTF,
    bool updateActiveEditor,
    bool saveModificationDate,
    bool updateUI,
    bool updateLayout)
  {
    this.OldText = oldText;
    this.NewText = newText;
    this.ClearRTF = clearRTF;
    this.UpdateActiveEditor = updateActiveEditor;
    this.SaveModificationDate = saveModificationDate;
    this.UpdateUI = updateUI;
    this.UpdateLayout = updateLayout;
  }
}
