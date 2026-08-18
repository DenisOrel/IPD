// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.ViewInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Контейнер информации о закладке, предоставляемой провайдером.
/// </summary>
public class ViewInfo
{
  public System.Type ControlType;
  private int _priority;
  private ViewCreatorCallback _creatorCallback;
  private object _additionalInfo;
  /// <summary>раздел справки для контрола по умолчанию</summary>
  private static readonly int defaultHelpID = 670;
  /// <summary>путь к файлу справки по умолчанию</summary>
  private static readonly string defaultHelpPath = HelpProvidersClass.HelpPath;
  /// <summary>
  /// id раздела хелпа для данной закладки
  /// по умолчанию показываем страницу про
  /// Рабочую область навигатора
  /// </summary>
  private string _helpTopicID = ViewInfo.defaultHelpID.ToString();
  /// <summary>
  ///  путь к хелпу справки
  /// по умолчанию Help\Rus\
  /// </summary>
  private string _helpPath = ViewInfo.defaultHelpPath;

  /// <summary>
  /// Создает контейнер информации, который можно использовать для подавления закладки.
  /// </summary>
  /// <param name="priority">Приоритет закладки.</param>
  public ViewInfo(int priority)
    : this(priority, (ViewCreatorCallback) null, (object) null)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="priority"></param>
  /// <param name="topicID"> id раздела справки для закладки.
  /// если раздел справки не известен, не указывайте 0, -1, 100500 - воспользуйтесь другим конструктором!</param>
  public ViewInfo(int priority, int topicID)
    : this(priority, topicID, (ViewCreatorCallback) null, (object) null)
  {
  }

  public ViewInfo(int priority, System.Type controlType)
    : this(priority, new ViewCreatorCallback(ViewInfo.CreateByControlType), (object) controlType)
  {
    this.ControlType = controlType;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="priority"></param>
  /// <param name="topicID"> id раздела справки для закладки.
  /// если раздел справки не известен, не указывайте 0, -1, 100500 - воспользуйтесь другим конструктором!</param>
  /// <param name="controlType"></param>
  public ViewInfo(int priority, int topicID, System.Type controlType)
    : this(priority, topicID, new ViewCreatorCallback(ViewInfo.CreateByControlType), (object) controlType)
  {
    this.ControlType = controlType;
  }

  public ViewInfo(int priority, ViewCreatorCallback creatorCallback)
    : this(priority, creatorCallback, (object) null)
  {
  }

  public ViewInfo(int priority, ViewCreatorCallback creatorCallback, object additionalInfo)
    : this(priority, ViewInfo.defaultHelpID, creatorCallback, additionalInfo)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="priority"></param>
  /// <param name="topicID"> id раздела справки для закладки.
  /// если раздел справки не известен, не указывайте 0, -1, 100500 - воспользуйтесь другим конструктором!</param>
  /// <param name="creatorCallback"></param>
  /// <param name="additionalInfo"></param>
  public ViewInfo(
    int priority,
    int topicID,
    ViewCreatorCallback creatorCallback,
    object additionalInfo)
  {
    this._priority = priority;
    this._creatorCallback = creatorCallback;
    this._helpTopicID = topicID.ToString();
    this._additionalInfo = additionalInfo;
  }

  /// <summary>
  /// Возвращает приоритет, присвоенный закладке провайдером. Среди закладок с одинаковым именем будет
  /// выбрана та, приоритет которой больше.
  /// </summary>
  public int Priority => this._priority;

  /// <summary>Возвращает делегат метода, который создает закладку.</summary>
  public ViewCreatorCallback CreatorCallback => this._creatorCallback;

  /// <summary>
  /// Возвращает дополнительные сведения, которые должны быть переданы методу, создающему закладку.
  /// </summary>
  public object AdditionalInfo => this._additionalInfo;

  /// <summary>возвращает id раздела хелпа для данной закладки</summary>
  [Obsolete("Use IViewDescriptionProvider.", false)]
  public string HelpTopicID
  {
    get => this._helpTopicID;
    set => this._helpTopicID = value;
  }

  /// <summary>Путь к файлу справки</summary>
  [Obsolete("Use IViewDescriptionProvider.", false)]
  public string HelpPath
  {
    get => this._helpPath;
    set => this._helpPath = value;
  }

  private static Control CreateByControlType(
    ISelectedItems items,
    System.IServiceProvider services,
    object additionalInfo)
  {
    Control instance = (Control) Activator.CreateInstance((System.Type) additionalInfo);
    IView view = instance as IView;
    return instance;
  }
}
