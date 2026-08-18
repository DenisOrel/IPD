
// Type: Intermech.Client.Core.RedService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Drawing;


namespace Intermech.Client.Core;

/// <summary>служба для работы с настройками замечаний на закладке Просмотре</summary>
public class RedService : RedProperty, IRedService, IRedProperty
{
  /// <summary>объект для потокобезопасного доступа</summary>
  private static object SyncRoot = new object();
  /// <summary>имя секции в настройках для настроек ... просмотра</summary>
  private static readonly string RED_SECTION = "RedSection";
  /// <summary>цвет заливки</summary>
  private static readonly string BRUSHCOLOR = "BrushColor";
  /// <summary>прозрачность заливки= 0-255(0 - нет заливки)</summary>
  private static readonly string BRUSHALPHA = "BrushAlpha";
  /// <summary>цвет кривой</summary>
  private static readonly string PENCOLOR = "PenColor";
  /// <summary>прозрачность= 0-255(0 - нет заливки)</summary>
  private static readonly string PENALPHA = "PenAlpha";
  /// <summary>толщина(мм)</summary>
  private static readonly string PENTHICKNESS = "PenThickness";
  /// <summary>имя фонта</summary>
  private static readonly string FONTNAME = "FontName";
  /// <summary>высота текста</summary>
  private static readonly string FONTSIZE = "FontSize";
  /// <summary>цвет текста</summary>
  private static readonly string TEXTCOLOR = "TextColor";
  /// <summary>прозрачность= 0-255(0 - нет заливки)</summary>
  private static readonly string TEXTALPHA = "TextAlpha";
  /// <summary>стиль фаски</summary>
  private static readonly string NOTESTYLE = "NoteStyle";
  /// <summary>размер фаски</summary>
  private static readonly string FACET = "Facet";
  /// <summary>стиль стрелки</summary>
  private static readonly string NOTEARROW = "NoteArrow";
  /// <summary>размер стрелки</summary>
  private static readonly string ARROWSIZE = "ArrowSize";

  public RedService() => RedService.ReadSettings((IRedProperty) this);

  /// <summary>прочитать настройки для замечаний на закладке Просмотр</summary>
  private void ReadSettings() => RedService.ReadSettings((IRedProperty) this);

  /// <summary>Изменить настройки для замечаний на закладке Просмотр</summary>
  /// <param name="data">новые данные</param>
  public void ChangeSettings(IRedProperty data) => RedService.WriteSettings(data);

  /// <summary>Изменить настройки для замечаний на закладке Просмотр</summary>
  /// <param name="data"></param>
  public static void WriteSettings(IRedProperty data)
  {
    lock (RedService.SyncRoot)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBConfigurations configurations = sessionKeeper.Session.Configurations;
        string redSection1 = RedService.RED_SECTION;
        string pencolor = RedService.PENCOLOR;
        Color color = data.PenColor;
        long argb1 = (long) color.ToArgb();
        long userId1 = sessionKeeper.Session.UserID;
        configurations.WriteInteger("CLIENT", redSection1, pencolor, argb1, userId1);
        configurations.WriteInteger("CLIENT", RedService.RED_SECTION, RedService.PENALPHA, (long) data.PenAlpha, sessionKeeper.Session.UserID);
        string redSection2 = RedService.RED_SECTION;
        string brushcolor = RedService.BRUSHCOLOR;
        color = data.BrushColor;
        long argb2 = (long) color.ToArgb();
        long userId2 = sessionKeeper.Session.UserID;
        configurations.WriteInteger("CLIENT", redSection2, brushcolor, argb2, userId2);
        configurations.WriteInteger("CLIENT", RedService.RED_SECTION, RedService.BRUSHALPHA, (long) data.BrushAlpha, sessionKeeper.Session.UserID);
        configurations.WriteDouble("CLIENT", RedService.RED_SECTION, RedService.PENTHICKNESS, (double) data.PenThickness, sessionKeeper.Session.UserID);
        configurations.WriteString("CLIENT", RedService.RED_SECTION, RedService.FONTNAME, data.FontName, sessionKeeper.Session.UserID);
        configurations.WriteDouble("CLIENT", RedService.RED_SECTION, RedService.FONTSIZE, (double) data.FontSize, sessionKeeper.Session.UserID);
        string redSection3 = RedService.RED_SECTION;
        string textcolor = RedService.TEXTCOLOR;
        color = data.TextColor;
        long argb3 = (long) color.ToArgb();
        long userId3 = sessionKeeper.Session.UserID;
        configurations.WriteInteger("CLIENT", redSection3, textcolor, argb3, userId3);
        configurations.WriteInteger("CLIENT", RedService.RED_SECTION, RedService.TEXTALPHA, (long) data.TextAlpha, sessionKeeper.Session.UserID);
        configurations.WriteString("CLIENT", RedService.RED_SECTION, RedService.NOTESTYLE, data.NoteStyle.GetName<IRedNoteStyle>(), sessionKeeper.Session.UserID);
        configurations.WriteDouble("CLIENT", RedService.RED_SECTION, RedService.FACET, (double) data.Facet, sessionKeeper.Session.UserID);
        configurations.WriteString("CLIENT", RedService.RED_SECTION, RedService.NOTEARROW, data.NoteArrow.GetName<IRedArrowStyle>(), sessionKeeper.Session.UserID);
        configurations.WriteDouble("CLIENT", RedService.RED_SECTION, RedService.ARROWSIZE, (double) data.ArrowSize, sessionKeeper.Session.UserID);
      }
    }
  }

  /// <param name="data"></param>
  public static void ReadSettings(IRedProperty data)
  {
    IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    data.PenColor = Color.FromArgb(Convert.ToInt32(service.ReadInteger("CLIENT", RedService.RED_SECTION, RedService.PENCOLOR, (long) Color.Red.ToArgb(), DBConfigMode.UserAndGlobal)));
    data.PenAlpha = Convert.ToInt32(service.ReadInteger("CLIENT", RedService.RED_SECTION, RedService.PENALPHA, (long) byte.MaxValue, DBConfigMode.UserAndGlobal));
    data.BrushColor = Color.FromArgb(Convert.ToInt32(service.ReadInteger("CLIENT", RedService.RED_SECTION, RedService.BRUSHCOLOR, (long) Color.Red.ToArgb(), DBConfigMode.UserAndGlobal)));
    data.BrushAlpha = Convert.ToInt32(service.ReadInteger("CLIENT", RedService.RED_SECTION, RedService.BRUSHALPHA, (long) byte.MaxValue, DBConfigMode.UserAndGlobal));
    data.PenThickness = Convert.ToSingle(service.ReadDouble("CLIENT", RedService.RED_SECTION, RedService.PENTHICKNESS, 0.0, DBConfigMode.UserAndGlobal));
    data.FontName = service.ReadString("CLIENT", RedService.RED_SECTION, RedService.FONTNAME, "Arial", DBConfigMode.UserAndGlobal);
    data.FontSize = Convert.ToSingle(service.ReadDouble("CLIENT", RedService.RED_SECTION, RedService.FONTSIZE, 15.0, DBConfigMode.UserAndGlobal));
    data.TextColor = Color.FromArgb(Convert.ToInt32(service.ReadInteger("CLIENT", RedService.RED_SECTION, RedService.TEXTCOLOR, (long) Color.Black.ToArgb(), DBConfigMode.UserAndGlobal)));
    data.TextAlpha = Convert.ToInt32(service.ReadInteger("CLIENT", RedService.RED_SECTION, RedService.TEXTALPHA, (long) byte.MaxValue, DBConfigMode.UserAndGlobal));
    string enumValue1 = service.ReadString("CLIENT", RedService.RED_SECTION, RedService.NOTESTYLE, IRedNoteStyle.Box.GetName<IRedNoteStyle>(), DBConfigMode.UserAndGlobal);
    data.NoteStyle = enumValue1.ToEnum<IRedNoteStyle>();
    data.Facet = Convert.ToSingle(service.ReadDouble("CLIENT", RedService.RED_SECTION, RedService.FACET, 4.0, DBConfigMode.UserAndGlobal));
    string enumValue2 = service.ReadString("CLIENT", RedService.RED_SECTION, RedService.NOTEARROW, IRedArrowStyle.None.GetName<IRedArrowStyle>(), DBConfigMode.UserAndGlobal);
    data.NoteArrow = enumValue2.ToEnum<IRedArrowStyle>();
    data.ArrowSize = Convert.ToSingle(service.ReadDouble("CLIENT", RedService.RED_SECTION, RedService.ARROWSIZE, 4.0, DBConfigMode.UserAndGlobal));
  }
}
