// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.PublishFileInfo
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System.Text;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Класс для записи информации о листах для публикации в AutoCad
/// </summary>
internal class PublishFileInfo
{
  internal PublishFileInfo()
  {
  }

  /// <summary>Полный путь к файлу чертежа</summary>
  internal string DwgPath { get; set; }

  /// <summary>Название листа</summary>
  internal string LayoutName { get; set; }

  /// <summary>Настройки</summary>
  internal string Setup { get; set; }

  /// <summary>Название чертежа</summary>
  internal string DwgName { get; set; }

  /// <summary>Индекс листа в самом автокаде</summary>
  internal int TabOrder { get; set; }

  /// <summary>Создать информацию для публикации по текущему файлу</summary>
  /// <returns></returns>
  internal string CreatePublishInfo()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append($"[DWF6Sheet:{this.DwgName}-{this.LayoutName}]");
    stringBuilder.AppendLine();
    stringBuilder.Append("DWG=" + this.DwgPath);
    stringBuilder.AppendLine();
    stringBuilder.Append("Layout=" + this.LayoutName);
    stringBuilder.AppendLine();
    stringBuilder.Append("Setup=");
    return stringBuilder.ToString();
  }
}
