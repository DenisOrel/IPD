// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OneErrorMessage
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Victor;
using Intermech.Bars;
using Intermech.Document.UI;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary> Отображение списка ошибок сбора ведомости с возможностью перехода от описания ошибки к месту этой ошибки </summary>
/// 
///             Пилипенок
public class OneErrorMessage : ImErrorMessage
{
  private OneError error;

  public OneErrorMessage(OneError error) => this.error = error;

  public override string Text
  {
    get => this.error._message;
    set
    {
    }
  }

  /// <summary> Отображение карточки записи лб этом элементе </summary>
  public override void DoubleClick()
  {
    if (this.error._f_PRJLINK_ID == 0L)
      return;
    int num = (int) PropertiesWindow.Execute(RelationExtensions.GetItems(new Dictionary<long, List<long>>()
    {
      [this.error._objectIdSP_KudaVhodit] = new List<long>((IEnumerable<long>) new long[1]
      {
        this.error._f_PRJLINK_ID
      })
    }));
  }

  /// <summary> Локальное меню </summary>
  /// <param name="contextMenuItems"></param>
  public override void GetContextMenu(List<ToolbarItemBase> contextMenuItems)
  {
    MenuButtonItem menuButtonItem1 = new MenuButtonItem("Открыть спецификацию, содержащую эту ошибку");
    menuButtonItem1.CommandName = "OpenSpecification_Whith_Error";
    menuButtonItem1.ToolTipText = "Открыть спецификацию, содержащую эту ошибку";
    menuButtonItem1.Click += new EventHandler(this.OpenSpecification_Whith_Error);
    contextMenuItems.Add((ToolbarItemBase) menuButtonItem1);
    MenuButtonItem menuButtonItem2 = new MenuButtonItem("Список ошибок вывести на принтер");
    menuButtonItem2.CommandName = "Print";
    menuButtonItem2.ToolTipText = "Список ошибок вывести на принтер";
    menuButtonItem2.Click += new EventHandler(this.Print);
    contextMenuItems.Add((ToolbarItemBase) menuButtonItem2);
    MenuButtonItem menuButtonItem3 = new MenuButtonItem("Список ошибок вывести в файл");
    menuButtonItem3.CommandName = "SaveToFile";
    menuButtonItem3.ToolTipText = "Список ошибок вывести в файл";
    menuButtonItem3.Click += new EventHandler(this.SaveToFile);
    contextMenuItems.Add((ToolbarItemBase) menuButtonItem3);
  }

  /// <summary> Открывается спецификация с этой записью error._objectIdSP_KudaVhodit</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public virtual void OpenSpecification_Whith_Error(object sender, EventArgs e)
  {
    AVSPlugin.Instance.OpenAVSWindow(this.error._objectIdSP_KudaVhodit);
  }

  /// <summary> Вывод на принтер всего списка ErrorsControl.ErrorStringsRows</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public virtual void Print(object sender, EventArgs e)
  {
    List<ImErrorMessage> errorRows = this.ErrorsControl.ErrorRows;
    Processing_Ved_Static.Print_Strings(this.ErrorsControl.ErrorStringsRows);
  }

  /// <summary> Вывести в файл ErrorsControl.ErrorStringsRows</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public virtual void SaveToFile(object sender, EventArgs e)
  {
    List<ImErrorMessage> errorRows = this.ErrorsControl.ErrorRows;
    Processing_Ved_Static.SaveToFile(this.ErrorsControl.ErrorStringsRows);
  }
}
