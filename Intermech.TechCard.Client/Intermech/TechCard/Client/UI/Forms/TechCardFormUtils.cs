// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Forms.TechCardFormUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.UI.Forms;

/// <summary>Form's utils</summary>
internal abstract class TechCardFormUtils
{
  /// <summary>Загрузка расположения и размеров формы</summary>
  /// <param name="techContrl">Форма</param>
  public static void LoadSettings(Control techContrl)
  {
    TechCardFormUtils.LoadSettings(techContrl, TechCardFormUtils.Mode.Position);
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  /// <param name="techContrl">Форма</param>
  /// <param name="mode">Режим сохранения размеров / позиции формы</param>
  /// <remarks>Внимание! Загрузка и сохранение параметров должна производиться в одинаковых режимах -
  /// это связано с добавлением имени типа контрола при загрузке / сохранении</remarks>
  public static void LoadSettings(Control techContrl, TechCardFormUtils.Mode mode)
  {
    TechCardFormUtils.LoadSettings(techContrl, mode, (IDictionary) null);
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  /// <param name="techContrl">Форма</param>
  /// <param name="mode">Режим сохранения размеров / позиции формы</param>
  /// <param name="config">Доп. настройки</param>
  /// <remarks>Внимание! Загрузка и сохранение параметров должна производиться в одинаковых режимах -
  /// это связано с добавлением имени типа контрола при загрузке / сохранении</remarks>
  public static void LoadSettings(
    Control techContrl,
    TechCardFormUtils.Mode mode,
    IDictionary config)
  {
    if (mode != TechCardFormUtils.Mode.All)
    {
      Form form = new Form();
      form.Name = techContrl.Name;
      form.Size = techContrl.Size;
      form.Location = techContrl.Location;
      FormStorage.LoadLayout((Control) form, config);
      techContrl.Location = form.Location;
      if (mode == TechCardFormUtils.Mode.LocationOnly)
        return;
      techContrl.Size = form.Size;
    }
    else
      FormStorage.LoadLayout(techContrl, config);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  /// <param name="techContrl">Форма</param>
  public static void SaveSettings(Control techContrl)
  {
    TechCardFormUtils.SaveSettings(techContrl, TechCardFormUtils.Mode.Position);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  /// <param name="techContrl">Форма</param>
  /// <param name="mode">Режим сохранения размеров / позиции формы</param>
  /// <remarks>Внимание! Загрузка и сохранение параметров должна производиться в одинаковых режимах -
  /// это связано с добавлением имени типа контрола при загрузке / сохранении</remarks>
  public static void SaveSettings(Control techContrl, TechCardFormUtils.Mode mode)
  {
    TechCardFormUtils.SaveSettings(techContrl, mode, (IDictionary) null);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  /// <param name="techContrl">Форма</param>
  /// <param name="mode">Режим сохранения размеров / позиции формы</param>
  /// <param name="config">Доп. настройки</param>
  /// <remarks>Внимание! Загрузка и сохранение параметров должна производиться в одинаковых режимах -
  /// это связано с добавлением имени типа контрола при загрузке / сохранении</remarks>
  public static void SaveSettings(
    Control techContrl,
    TechCardFormUtils.Mode mode,
    IDictionary config)
  {
    if (mode != TechCardFormUtils.Mode.All)
    {
      Form form1 = new Form();
      form1.Name = techContrl.Name;
      if (techContrl is Form form2 && form2.WindowState != FormWindowState.Normal)
      {
        form1.Location = form2.RestoreBounds.Location;
        if (mode != TechCardFormUtils.Mode.LocationOnly)
          form1.Size = form2.RestoreBounds.Size;
      }
      else
      {
        form1.Location = techContrl.Location;
        if (mode != TechCardFormUtils.Mode.LocationOnly)
          form1.Size = techContrl.Size;
      }
      FormStorage.SaveLayout((Control) form1, config);
    }
    else
      FormStorage.SaveLayout(techContrl, config);
  }

  /// <summary>Режим обработки настроек</summary>
  public enum Mode
  {
    /// <summary>Все параметры</summary>
    All,
    /// <summary>Size и Location</summary>
    Position,
    /// <summary>Только Location</summary>
    LocationOnly,
  }
}
