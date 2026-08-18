// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsRowEventMessage
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;

#nullable disable
namespace Intermech.AVS;

/// <summary>Вспомогательный класс для сообщений</summary>
public class AvsRowEventMessage
{
  /// <summary>Тип проверки</summary>
  public AVSEventType EventType;
  /// <summary>Сообщение о результатах проверки</summary>
  private string message;
  /// <summary>Значение поля, которое хранилось в документе</summary>
  public string OriginalValue;
  /// <summary>Значение поля, которое хранилось в документе</summary>
  public string NewValue;
  private int productIndex = -1;
  private AvsRowAttributeInfo attrInfo;

  public int ProductIndex
  {
    get => this.productIndex;
    set => this.productIndex = value;
  }

  public AvsRowAttributeInfo AttrInfo
  {
    get => this.attrInfo;
    set => this.attrInfo = value;
  }

  public string Message
  {
    get
    {
      if (!string.IsNullOrEmpty(this.message) || this.AttrInfo == null || this.EventType != AVSEventType.ChangeRow)
        return this.message ?? "";
      return $"Изменено значение поля '{this.AttrInfo.Name}'. Старое значение: '{this.OriginalValue}', Новое значение: '{this.NewValue}'";
    }
    set => this.message = value;
  }

  /// <summary>Конструктор</summary>
  /// <param name="checkType">Тип проверки</param>
  /// <param name="checkMessage">Сообщение о результатах проверки</param>
  public AvsRowEventMessage(AVSEventType eventType, string message = null)
  {
    this.EventType = eventType;
    this.Message = message;
  }
}
