// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.TechCardParams.TechCardParamsHelper
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.Settings.TechCardParams;

/// <summary>Класс хелпер для параметров (настроек) ТechCard</summary>
public static class TechCardParamsHelper
{
  /// <summary>
  /// 
  /// </summary>
  private static Intermech.Interfaces.TechCard.TechCardParams _techParams;

  /// <summary>Параметры ТechCard</summary>
  public static Intermech.Interfaces.TechCard.TechCardParams TechParams
  {
    get
    {
      if (TechCardParamsHelper._techParams == null)
        TechCardParamsHelper.LoadValues();
      return TechCardParamsHelper._techParams;
    }
  }

  /// <summary>Загрузка тек. параметров</summary>
  public static void LoadValues()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IAppSettingsService<Intermech.Interfaces.TechCard.TechCardParams>>((object) sessionKeeper.Session, true).LoadSettings(sessionKeeper.Session.SessionGUID, ref TechCardParamsHelper._techParams);
  }

  /// <summary>Сохранение тек. параметров</summary>
  public static void SaveValues()
  {
    if (TechCardParamsHelper._techParams == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IAppSettingsService<Intermech.Interfaces.TechCard.TechCardParams>>((object) sessionKeeper.Session, true).SaveSettings(sessionKeeper.Session.SessionGUID, TechCardParamsHelper._techParams);
  }
}
