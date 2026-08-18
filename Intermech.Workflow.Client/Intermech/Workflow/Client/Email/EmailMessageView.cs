// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Email.EmailMessageView
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using DevExpress.IM.XtraBars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Workflow.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.Email;

[ViewDescriptionProvider(typeof (EmailMessageView.EmailMessageViewDescriptionProvider))]
public class EmailMessageView : UserControl, IView
{
  private IContainer components;
  private BarManager barManager1;
  private Bar bar1;
  private BarButtonItem barButtonItem1;
  private BarDockControl barDockControlTop;
  private BarDockControl barDockControlBottom;
  private BarDockControl barDockControlLeft;
  private BarDockControl barDockControlRight;
  private WebBrowser webBrowser1;

  public EmailMessageView() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(itemData.ObjectID).GetAttributeByID(wfConsts.AttrActivityMessageID);
      if (attributeById == null || attributeById.IsNull)
        return;
      string str = Convert.ToString(attributeById.Value);
      if (str.Length <= 0)
        return;
      this.webBrowser1.Navigate("about:blank");
      HtmlDocument document = this.webBrowser1.Document;
      if (document != (HtmlDocument) null)
        document.Write(string.Empty);
      this.webBrowser1.DocumentText = str;
      this.webBrowser1.Update();
    }
  }

  public void Activate(IView previousView)
  {
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => LocalizationHolder.rm.GetString("Workflow.Client_70");

  public int ImageIndex => Holder.MessagesImageIndex;

  public int OrderID => 1;

  private void barButtonItem1_ItemPress(object sender, ItemClickEventArgs e)
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.Filter = LocalizationHolder.rm.GetString("Workflow.Client_71");
    saveFileDialog.FilterIndex = 1;
    saveFileDialog.RestoreDirectory = true;
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    TextWriter textWriter = (TextWriter) new StreamWriter(saveFileDialog.FileName);
    try
    {
      textWriter.Write(this.webBrowser1.DocumentText);
    }
    finally
    {
      textWriter.Flush();
      textWriter.Close();
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EmailMessageView));
    this.barManager1 = new BarManager();
    this.bar1 = new Bar();
    this.barButtonItem1 = new BarButtonItem();
    this.barDockControlTop = new BarDockControl();
    this.barDockControlBottom = new BarDockControl();
    this.barDockControlLeft = new BarDockControl();
    this.barDockControlRight = new BarDockControl();
    this.webBrowser1 = new WebBrowser();
    ((ISupportInitialize) this.barManager1).BeginInit();
    this.SuspendLayout();
    this.barManager1.Bars.AddRange(new Bar[1]{ this.bar1 });
    ((ArrayList) this.barManager1.DockControls).Add((object) this.barDockControlTop);
    ((ArrayList) this.barManager1.DockControls).Add((object) this.barDockControlBottom);
    ((ArrayList) this.barManager1.DockControls).Add((object) this.barDockControlLeft);
    ((ArrayList) this.barManager1.DockControls).Add((object) this.barDockControlRight);
    this.barManager1.Form = (ContainerControl) this;
    this.barManager1.Items.AddRange(new BarItem[1]
    {
      (BarItem) this.barButtonItem1
    });
    this.barManager1.MaxItemId = 1;
    this.bar1.BarName = "Custom 1";
    this.bar1.DockCol = 0;
    this.bar1.DockRow = 0;
    this.bar1.DockStyle = (BarDockStyle) 2;
    this.bar1.LinksPersistInfo.AddRange(new LinkPersistInfo[1]
    {
      new LinkPersistInfo((BarItem) this.barButtonItem1)
    });
    this.bar1.OptionsBar.AllowQuickCustomization = false;
    this.bar1.OptionsBar.DisableClose = true;
    this.bar1.OptionsBar.DisableCustomization = true;
    this.bar1.OptionsBar.RotateWhenVertical = false;
    componentResourceManager.ApplyResources((object) this.bar1, "bar1");
    componentResourceManager.ApplyResources((object) this.barButtonItem1, "barButtonItem1");
    ((BarItem) this.barButtonItem1).Glyph = (Image) componentResourceManager.GetObject("barButtonItem1.Glyph");
    ((BarItem) this.barButtonItem1).Id = 0;
    ((BarItem) this.barButtonItem1).Name = "barButtonItem1";
    ((BarItem) this.barButtonItem1).ItemPress += new ItemClickEventHandler(this.barButtonItem1_ItemPress);
    componentResourceManager.ApplyResources((object) this.webBrowser1, "webBrowser1");
    this.webBrowser1.Name = "webBrowser1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.webBrowser1);
    this.Controls.Add((Control) this.barDockControlLeft);
    this.Controls.Add((Control) this.barDockControlRight);
    this.Controls.Add((Control) this.barDockControlBottom);
    this.Controls.Add((Control) this.barDockControlTop);
    this.Name = nameof (EmailMessageView);
    ((ISupportInitialize) this.barManager1).EndInit();
    this.ResumeLayout(false);
  }

  private sealed class EmailMessageViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Workflow.Client_70"),
        ImageIndex = Holder.MessagesImageIndex,
        OrderID = 1
      };
    }
  }
}
