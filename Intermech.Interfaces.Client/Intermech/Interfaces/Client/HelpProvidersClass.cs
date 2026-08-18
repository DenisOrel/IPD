// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.HelpProvidersClass
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>для общения с хелпом к ips</summary>
public static class HelpProvidersClass
{
  /// <summary>путь к папке с хелпом</summary>
  private static string HELP_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory + "Help\\";
  /// <summary>имя файла справки</summary>
  private static readonly string HELP_FILE = "\\IPSHelp.chm";
  /// <summary>папка в которой расположен русский хелп</summary>
  private static readonly string RUS_HELP_DIRECTORY = "RUS";
  /// <summary>полный путь к файлу помощи</summary>
  public static string HelpPath;
  /// <summary>
  /// т.к.
  /// Any help window that you create through the HTML Help API is
  /// owned by the calling, or parent, program.
  /// This allows the help window to stay on top of its parent,
  /// yet not be on top of any other program that has focus.
  /// 
  /// а пользователям это не нравится, создадим окно-заглушку,
  /// показывать не будем, но будем вызывать хелп для него,
  /// т. обр. окошко хелпа будет не только терять фокус,
  /// но и уходить на задний план, как того хотят в 948353
  /// </summary>
  private static Form dummy = new Form();
  private static Dictionary<System.Type, string> helpTopics = new Dictionary<System.Type, string>();

  /// <summary>
  /// Подключить вызов справки для контрола
  /// Убрать кнопки MinimizeBox и MaximizeBox, добавить кнопку вызова справки,
  /// если это форма
  /// </summary>
  /// <param name="currentControl">Сам контрол</param>
  /// <param name="topic">Раздел справки для отображения</param>
  public static void SetHelpOptionForControl(Control currentControl, int topic)
  {
    HelpProvidersClass.SetHelpOptionForControl(currentControl, topic.ToString());
  }

  /// <summary>
  /// Подключить вызов справки для контрола
  /// Убрать кнопки MinimizeBox и MaximizeBox, добавить кнопку вызова справки,
  /// если это форма
  /// </summary>
  /// <param name="currentControl">Сам контрол</param>
  /// <param name="topic">Раздел справки для отображения</param>
  public static void SetHelpOptionForControl(Control currentControl, string topic)
  {
    System.Type type = currentControl.GetType();
    if (currentControl is Form form)
    {
      form.MinimizeBox = form.MaximizeBox = false;
      form.HelpButton = true;
      form.HelpButtonClicked += new CancelEventHandler(HelpProvidersClass.currentForm_HelpButtonClicked);
      form.FormClosing += new FormClosingEventHandler(HelpProvidersClass.currentForm_Disposed);
    }
    currentControl.HelpRequested += new HelpEventHandler(HelpProvidersClass.currentControl_HelpRequested);
    currentControl.Disposed += new EventHandler(HelpProvidersClass.currentForm_Disposed);
    if (HelpProvidersClass.helpTopics.ContainsKey(type))
      return;
    HelpProvidersClass.helpTopics.Add(type, topic);
    currentControl.Disposed += new EventHandler(HelpProvidersClass.currentForm_IsFirst_Disposed);
    if (form == null)
      return;
    form.FormClosing += new FormClosingEventHandler(HelpProvidersClass.currentForm_IsFirst_Disposed);
  }

  private static void currentForm_Disposed(object sender, EventArgs e)
  {
    if (sender is Form form)
    {
      form.HelpButtonClicked -= new CancelEventHandler(HelpProvidersClass.currentForm_HelpButtonClicked);
      form.FormClosing -= new FormClosingEventHandler(HelpProvidersClass.currentForm_Disposed);
    }
    if (!(sender is Control control))
      return;
    control.HelpRequested -= new HelpEventHandler(HelpProvidersClass.currentControl_HelpRequested);
    control.Disposed -= new EventHandler(HelpProvidersClass.currentForm_Disposed);
  }

  private static void currentForm_IsFirst_Disposed(object sender, EventArgs e)
  {
    if (sender is Control control)
    {
      if (HelpProvidersClass.helpTopics.ContainsKey(control.GetType()))
        HelpProvidersClass.helpTopics.Remove(control.GetType());
      control.Disposed -= new EventHandler(HelpProvidersClass.currentForm_IsFirst_Disposed);
    }
    if (!(sender is Form form))
      return;
    form.FormClosing -= new FormClosingEventHandler(HelpProvidersClass.currentForm_IsFirst_Disposed);
  }

  private static void currentControl_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    System.Type type = sender.GetType();
    if (!HelpProvidersClass.helpTopics.ContainsKey(type))
      return;
    string helpTopic = HelpProvidersClass.helpTopics[type];
    Help.ShowHelp((Control) HelpProvidersClass.dummy, HelpProvidersClass.HelpPath, HelpNavigator.TopicId, (object) helpTopic);
  }

  private static void currentForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    System.Type type = sender.GetType();
    if (!HelpProvidersClass.helpTopics.ContainsKey(type))
      return;
    string helpTopic = HelpProvidersClass.helpTopics[type];
    Help.ShowHelp((Control) HelpProvidersClass.dummy, HelpProvidersClass.HelpPath, HelpNavigator.TopicId, (object) helpTopic);
    e.Cancel = true;
  }

  public static string GetHelpKeyword(System.Type controlType)
  {
    return !HelpProvidersClass.helpTopics.ContainsKey(controlType) ? string.Empty : HelpProvidersClass.helpTopics[controlType];
  }

  /// <summary>показать весь хелп</summary>
  public static void ShowHelp()
  {
    Help.ShowHelp((Control) HelpProvidersClass.dummy, HelpProvidersClass.HelpPath, HelpNavigator.TableOfContents);
  }

  /// <summary>показать страницу с индексами</summary>
  public static void ShowIndexes()
  {
    Help.ShowHelp((Control) HelpProvidersClass.dummy, HelpProvidersClass.HelpPath, HelpNavigator.Index);
  }

  /// <summary>показать страницу поиска</summary>
  public static void ShowSearch()
  {
    Help.ShowHelp((Control) HelpProvidersClass.dummy, HelpProvidersClass.HelpPath, HelpNavigator.Find, (object) string.Empty);
  }

  /// <summary>Показать раздел справки</summary>
  /// <param name="topicID">раздел справки для отображения</param>
  /// <param name="customPath">путь к хелпу</param>
  public static void ShowHelpTopic(string topicID, string customPath)
  {
    if (File.Exists(customPath))
      Help.ShowHelp((Control) HelpProvidersClass.dummy, customPath, HelpNavigator.TopicId, (object) topicID);
    else
      HelpProvidersClass.ShowHelpTopic(topicID);
  }

  /// <summary>Показать раздел справки</summary>
  /// <param name="topicID">Раздел справки для отображения</param>
  public static void ShowHelpTopic(string topicID)
  {
    Help.ShowHelp((Control) HelpProvidersClass.dummy, HelpProvidersClass.HelpPath, HelpNavigator.TopicId, (object) topicID);
  }

  /// <summary>Показать раздел справки</summary>
  /// <param name="topicID">Раздел справки для отображения</param>
  public static void ShowHelpTopic(int topicID)
  {
    HelpProvidersClass.ShowHelpTopic(topicID.ToString());
  }

  /// <summary>
  /// конструктор. устанавливаем пути к файлу помощи
  /// в зависимости от установаленного в системе языка
  /// если папка с кодом языка либо файл помощи в этой папке не были найдены
  /// будем использовать файл помощи в папке RUS
  /// ну если там ничего нет - совсем плохо
  /// </summary>
  static HelpProvidersClass()
  {
    string windowsLanguageName = CultureInfo.InstalledUICulture.ThreeLetterWindowsLanguageName;
    string path = HelpProvidersClass.HELP_DIRECTORY + windowsLanguageName;
    if (Directory.Exists(path))
    {
      if (File.Exists(path + HelpProvidersClass.HELP_FILE))
        HelpProvidersClass.HelpPath = path + HelpProvidersClass.HELP_FILE;
      else
        HelpProvidersClass.HelpPath = HelpProvidersClass.HELP_DIRECTORY + HelpProvidersClass.RUS_HELP_DIRECTORY + HelpProvidersClass.HELP_FILE;
    }
    else
      HelpProvidersClass.HelpPath = HelpProvidersClass.HELP_DIRECTORY + HelpProvidersClass.RUS_HELP_DIRECTORY + HelpProvidersClass.HELP_FILE;
  }
}
