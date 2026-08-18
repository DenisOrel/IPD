// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.ProviderHolder
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Bars;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>Хранитель сервисов.</summary>
public class ProviderHolder
{
  /// <summary>Провайдер сервисов</summary>
  public static System.IServiceProvider ServiceProvider = (System.IServiceProvider) null;
  /// <summary>Провайдер редакторов форм</summary>
  internal static IFormDesignerEditorService EditorService = (IFormDesignerEditorService) null;
  /// <summary>
  /// 
  /// </summary>
  public static BarManager BarManager = (BarManager) null;
  /// <summary>Интерфейсы для навигатора</summary>
  public static IFactory Factory = (IFactory) null;
  /// <summary>Индексы для меню</summary>
  public static Hashtable MenuIndex = new Hashtable();
  /// <summary>Картинки для меню</summary>
  public static ImageList iList = new ImageList();
  /// <summary>Строка отображения toolbox'а</summary>
  public static string DockString = string.Empty;
}
