// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.SpecificationFormMethods
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Вспомогательный класс для декодирования форм конструкторских документов</summary>
public static class SpecificationFormMethods
{
  /// <summary>Преобразовать текстовое значение атрибута "Форма спецификации" в тип SpecificationForm</summary>
  /// <param name="attrValue">Значение атрибута</param>
  /// <returns></returns>
  public static AVSDocumentForm? DecodeSpecificationFormAttrValue(string attrValue)
  {
    switch (attrValue)
    {
      case "Единичная":
        return new AVSDocumentForm?(AVSDocumentForm.Single);
      case "Групповая А":
        return new AVSDocumentForm?(AVSDocumentForm.A);
      case "Групповая Б":
        return new AVSDocumentForm?(AVSDocumentForm.B);
      case "Групповая В":
        return new AVSDocumentForm?(AVSDocumentForm.V);
      case "Групповая Г":
        return new AVSDocumentForm?(AVSDocumentForm.G);
      case "Зеркальная":
        return new AVSDocumentForm?(AVSDocumentForm.Mirror);
      default:
        return new AVSDocumentForm?();
    }
  }

  /// <summary>Преобразовать значение типа SpecificationForm в допустимое значение атрибута "Форма спецификации"</summary>
  /// <param name="attrValue">Значение типа SpecificationForm</param>
  /// <returns></returns>
  public static string EncodeSpecificationFormAttrValue(AVSDocumentForm attrValue)
  {
    switch (attrValue)
    {
      case AVSDocumentForm.Single:
        return "Единичная";
      case AVSDocumentForm.A:
        return "Групповая А";
      case AVSDocumentForm.B:
        return "Групповая Б";
      case AVSDocumentForm.Mirror:
        return "Зеркальная";
      case AVSDocumentForm.V:
        return "Групповая В";
      case AVSDocumentForm.G:
        return "Групповая Г";
      default:
        return "Единичная";
    }
  }
}
