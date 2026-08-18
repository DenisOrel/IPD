// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.HelperClasses.StatusIcons
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.Model;
using System.Drawing;

#nullable disable
namespace Intermech.AVS.HelperClasses;

/// <summary>Класс иконок статусов в табличном представлении</summary>
public class StatusIcons
{
  private static Image none;
  private static Image actualSubstitute;
  private static Image substitute;

  /// <summary>Нет иконки</summary>
  public static Image None
  {
    get
    {
      if (StatusIcons.none == null)
        StatusIcons.none = DocumentMenuHelper.LoadImageFromResurces(typeof (StatusIcons).Assembly, "Intermech.AVS.Resources.EmptyImage.png");
      return StatusIcons.none;
    }
  }

  /// <summary>Актуальный заменитель</summary>
  public static Image ActualSubstitute
  {
    get
    {
      if (StatusIcons.actualSubstitute == null)
        StatusIcons.actualSubstitute = DocumentMenuHelper.LoadImageFromResurces(typeof (StatusIcons).Assembly, "Intermech.AVS.Resources.rsActualSubstitute.png");
      return StatusIcons.actualSubstitute;
    }
  }

  /// <summary>Заменитель</summary>
  public static Image Substitute
  {
    get
    {
      if (StatusIcons.substitute == null)
        StatusIcons.substitute = DocumentMenuHelper.LoadImageFromResurces(typeof (StatusIcons).Assembly, "Intermech.AVS.Resources.rsSubstitute.png");
      return StatusIcons.substitute;
    }
  }
}
