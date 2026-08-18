// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecRowCheckMessage
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;

#nullable disable
namespace Intermech.AVS;

/// <summary>Вспомогательный класс для хранения сообщений о результатах проверки документа</summary>
public class SpecRowCheckMessage
{
  /// <summary>Тип проверки</summary>
  public AVSCheckType CheckType;
  /// <summary>Сообщение о результатах проверки</summary>
  public string CheckMessage;
  public int ProductIndex = -1;
  public AvsRowAttributeInfo Attr;

  /// <summary>Конструктор</summary>
  /// <param name="checkType">Тип проверки</param>
  /// <param name="checkMessage">Сообщение о результатах проверки</param>
  public SpecRowCheckMessage(
    AVSCheckType checkType,
    string checkMessage,
    int productIndex = -1,
    AvsRowAttributeInfo attr = null)
  {
    this.CheckType = checkType;
    this.CheckMessage = checkMessage;
    this.ProductIndex = productIndex;
    this.Attr = attr;
  }
}
