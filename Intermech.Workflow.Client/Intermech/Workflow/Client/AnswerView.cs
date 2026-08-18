// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AnswerView
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Workflow.Design;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

[ViewDescriptionProvider(typeof (AnswerView.AnswerViewDescriptionProvider))]
public class AnswerView : UserControl, IView
{
  private ImageList AnswerIL;
  private ToolBar answerBar;
  private ToolBarButton toolBarButton1;
  private ToolBarButton toolBarButton2;
  private TextBox AnswerBox;
  private IContainer components;
  private long _objectID;

  public AnswerView()
  {
    this.InitializeComponent();
    wfFunx.RegisterLoadSaveCommands(this.answerBar, this.AnswerBox);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AnswerView));
    this.answerBar = new ToolBar();
    this.toolBarButton1 = new ToolBarButton();
    this.toolBarButton2 = new ToolBarButton();
    this.AnswerIL = new ImageList(this.components);
    this.AnswerBox = new TextBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.answerBar, "answerBar");
    this.answerBar.Buttons.AddRange(new ToolBarButton[2]
    {
      this.toolBarButton1,
      this.toolBarButton2
    });
    this.answerBar.Divider = false;
    this.answerBar.ImageList = this.AnswerIL;
    this.answerBar.Name = "answerBar";
    componentResourceManager.ApplyResources((object) this.toolBarButton1, "toolBarButton1");
    this.toolBarButton1.Name = "toolBarButton1";
    this.toolBarButton1.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.toolBarButton2, "toolBarButton2");
    this.toolBarButton2.Name = "toolBarButton2";
    this.toolBarButton2.Tag = (object) "2";
    this.AnswerIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("AnswerIL.ImageStream");
    this.AnswerIL.TransparentColor = Color.Fuchsia;
    this.AnswerIL.Images.SetKeyName(0, "открыть.png");
    this.AnswerIL.Images.SetKeyName(1, "сохранить.png");
    componentResourceManager.ApplyResources((object) this.AnswerBox, "AnswerBox");
    this.AnswerBox.Name = "AnswerBox";
    this.Controls.Add((Control) this.AnswerBox);
    this.Controls.Add((Control) this.answerBar);
    this.Name = nameof (AnswerView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private IDBAttribute GetAttribute(IUserSession session, bool readOnly)
  {
    if (this._objectID == 0L)
      return (IDBAttribute) null;
    IDBAttribute attribute = session.GetObjectAttributeByID(this._objectID, wfConsts.AttrActivityMessageID);
    if (attribute == null && !readOnly)
      attribute = session.AddObjectAttribute(this._objectID, wfConsts.AttrActivityMessageID, false, false, (object[]) null);
    return attribute;
  }

  public int ImageIndex => Holder.AnswerImageIndex;

  public int OrderID => 1;

  public string Caption => LocalizationHolder.rm.GetString("Workflow.Client_1");

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
  }

  public void Deactivate(IView nextView)
  {
    if (this._objectID == 0L || !this.AnswerBox.Modified)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attribute = this.GetAttribute(sessionKeeper.Session, false);
      if (attribute == null)
        return;
      string text = this.AnswerBox.Text;
      attribute.Value = (object) text;
      this.AnswerBox.Modified = false;
    }
  }

  public void Activate(IView previousView)
  {
    string str = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attribute = this.GetAttribute(sessionKeeper.Session, true);
      if (attribute != null)
        str = attribute.Value.ToString();
      this.AnswerBox.Text = str;
      this.AnswerBox.Modified = false;
    }
  }

  private sealed class AnswerViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Workflow.Client_1"),
        ImageIndex = Holder.AnswerImageIndex,
        OrderID = 1
      };
    }
  }
}
