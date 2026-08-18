// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PageNumBuilder
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary> Класс для работы с номером страницы (разбор, генерация). </summary>
public sealed class PageNumBuilder
{
  private string numExtension = string.Empty;
  private int mainPart = 1;

  /// <summary>Основная часть номера</summary>
  public int MainPart
  {
    get => this.mainPart;
    set
    {
      if (value == this.mainPart || value == 0)
        return;
      this.mainPart = value;
    }
  }

  /// <summary>Расширение номера</summary>
  public string Extension
  {
    get => this.numExtension;
    set
    {
      string @extension = value?.Trim() ?? string.Empty;
      if (!@extension.Equals(this.numExtension, StringComparison.Ordinal) && PageNumberingHelper.IsValidExtension(@extension))
        this.numExtension = @extension;
      else
        this.numExtension = string.Empty;
    }
  }

  /// <summary>Создает экземпляр билдера</summary>
  /// <param name="pageNumText">строка номера страницы</param>
  public static PageNumBuilder Parse(string pageNumText)
  {
    PageNumExtensionStyle numberingStyle = PageNumberingHelper.GetNumberingStyle(pageNumText);
    string str1 = (string) null;
    string str2 = (string) null;
    switch (numberingStyle)
    {
      case PageNumExtensionStyle.None:
        str1 = pageNumText;
        str2 = string.Empty;
        break;
      case PageNumExtensionStyle.DigitsAfterDot:
        string[] source = pageNumText.Split(new char[1]
        {
          '.'
        }, StringSplitOptions.RemoveEmptyEntries);
        str2 = ((IEnumerable<string>) source).Last<string>();
        str1 = ((IEnumerable<string>) source).First<string>();
        break;
      case PageNumExtensionStyle.Letter:
        str2 = pageNumText.Substring(pageNumText.Length - 1, 1);
        str1 = pageNumText.Substring(0, pageNumText.Length - 1);
        break;
    }
    if (str1 == null)
      return (PageNumBuilder) null;
    return new PageNumBuilder()
    {
      MainPart = Convert.ToInt32(str1),
      Extension = str2
    };
  }

  /// <summary>Увеличить значение основной части номера страницы</summary>
  public PageNumBuilder IncrementMainNumber()
  {
    ++this.mainPart;
    return this;
  }

  /// <summary>Увеличить значение расширения номера доп. страницы</summary>
  public PageNumBuilder IncrementExtension()
  {
    if (!string.IsNullOrWhiteSpace(this.numExtension))
    {
      string @extension = !PageNumberingHelper.IsNumericString(this.numExtension) ? ((char) ((uint) this.numExtension[0] + 1U)).ToString() : ((byte) ((uint) byte.Parse(this.numExtension) + 1U)).ToString();
      if (PageNumberingHelper.IsValidExtension(@extension))
        this.numExtension = @extension;
    }
    return this;
  }

  /// <summary>Установить начальное значение расширения</summary>
  public void ResetExtension()
  {
    if (string.IsNullOrWhiteSpace(this.numExtension))
      return;
    this.numExtension = PageNumberingHelper.IsNumericString(this.numExtension) ? "1" : 'а'.ToString();
  }

  /// <summary>Переопределение базового метода перевода в строку</summary>
  public override string ToString()
  {
    return !PageNumberingHelper.IsNumericString(this.Extension) ? $"{this.MainPart}{this.Extension}" : $"{this.MainPart}.{this.Extension}";
  }
}
