// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.LoadSaveTextHelper
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>
/// Связывает кнопки тулбара с тегами 1 и 2 с командами "Загрузить из файла" и "Сохранить в файл"
/// </summary>
public class LoadSaveTextHelper
{
  private TextBox _box;

  public LoadSaveTextHelper(ToolBar tb, TextBox box)
  {
    this._box = box;
    tb.ButtonClick += new ToolBarButtonClickEventHandler(this.TB_ButtonClick);
  }

  private void TB_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    int int32 = Convert.ToInt32(e.Button.Tag);
    switch (int32)
    {
      case 1:
      case 2:
        string fn = wfFunx.PromptForFileName("txt", int32 == 2);
        if (!(fn != ""))
          break;
        if (int32 == 1)
        {
          this._box.Text = wfFunx.FileToString(fn);
          this._box.Modified = true;
          break;
        }
        wfFunx.StringToFile(this._box.Text, fn);
        break;
    }
  }
}
