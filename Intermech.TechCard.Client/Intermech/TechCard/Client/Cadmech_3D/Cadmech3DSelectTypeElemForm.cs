// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.Cadmech3DSelectTypeElemForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

internal class Cadmech3DSelectTypeElemForm : Form
{
  /// <summary>
  /// 
  /// </summary>
  private readonly ObjInfoItem _modelObjInfo;
  /// <summary>
  /// 
  /// </summary>
  private IMTextAttributeManagerProxy _imCadAttrMgr;
  /// <summary>
  /// 
  /// </summary>
  private ICategoryTypeIconService _categoryImages;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel pnButtons;
  protected Button btCancel;
  protected Button btApply;
  private TreeView tvTypeElements;
  private ImageList mainImageList;
  private Panel pnlTop;
  public PictureBox pbObject;
  public Label lblDescription;
  private ImageList stateImageList;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    this.tvTypeElements.CheckBoxes = false;
    this.stateImageList.Images.Clear();
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      this.stateImageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgUnchecked")]);
      this.stateImageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgChecked")]);
      this.stateImageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgGrayed")]);
    }
    this._categoryImages = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    if (this._categoryImages == null)
      return;
    this.tvTypeElements.ImageList = this._categoryImages.ImageList;
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateControls() => this.btApply.Enabled = this.SelectedElems.Length != 0;

  /// <summary>Конструктор</summary>
  public Cadmech3DSelectTypeElemForm(ObjInfoItem modelObjInfo)
  {
    this._modelObjInfo = modelObjInfo;
    this.InitializeComponent();
    this.InitializeCustomControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imCadAttrMgr"></param>
  public void LoadTypeElemInfo(IMTextAttributeManagerProxy imCadAttrMgr)
  {
    if (imCadAttrMgr == null)
      throw new ArgumentNullException(nameof (imCadAttrMgr));
    int num1 = this._categoryImages != null ? this._categoryImages.IndexOf(4, this._modelObjInfo.ObjTypeID) : -1;
    int num2 = this._categoryImages != null ? this._categoryImages.IndexOf(4, TechCardConsts.ObjectTypes.SurfaceMasterID) : -1;
    int num3 = this._categoryImages != null ? this._categoryImages.IndexOf(4, TechCardConsts.ObjectTypes.SurfaceSlaveID) : -1;
    this._imCadAttrMgr = imCadAttrMgr;
    this.tvTypeElements.BeginUpdate();
    try
    {
      this.tvTypeElements.Nodes.Clear();
      if (imCadAttrMgr.GetAllFaces() == null)
        return;
      TreeNode treeNode1;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        treeNode1 = this.tvTypeElements.Nodes.Add(sessionKeeper.Session.GetObjectInfo(this._modelObjInfo.ObjectID).Caption);
        treeNode1.Tag = (object) this._modelObjInfo;
        treeNode1.ImageIndex = num1;
        treeNode1.StateImageIndex = 0;
      }
      IMTextFaceAttributeProxy[] allFaceAttrsByType = imCadAttrMgr.GetAllFaceAttrsByType(IMTextFaceAttributeType.Parameter);
      if (allFaceAttrsByType == null)
        return;
      foreach (IMTextFaceAttributeProxy faceAttributeProxy in allFaceAttrsByType)
      {
        string text = Convert.ToString(faceAttributeProxy.GetProperty("FCN_TEMPLATE"));
        TreeNode treeNode2 = treeNode1 != null ? treeNode1.Nodes.Add(text) : this.tvTypeElements.Nodes.Add(text);
        treeNode2.Tag = (object) faceAttributeProxy;
        treeNode2.ImageIndex = num2;
        treeNode2.StateImageIndex = 0;
        IMTextFaceProxy[] faces = faceAttributeProxy.Faces;
        if (faces != null)
        {
          foreach (IMTextFaceProxy imTextFaceProxy in faces)
          {
            TreeNode treeNode3 = treeNode2.Nodes.Add(imTextFaceProxy.Description);
            treeNode3.Tag = (object) imTextFaceProxy;
            treeNode3.ImageIndex = num3;
            treeNode3.StateImageIndex = -1;
          }
        }
      }
    }
    finally
    {
      this.tvTypeElements.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public IMTextFaceAttributeProxy[] SelectedElems
  {
    get
    {
      List<IMTextFaceAttributeProxy> selectedElems = new List<IMTextFaceAttributeProxy>();
      Action<TreeNodeCollection> collectElement = (Action<TreeNodeCollection>) null;
      collectElement = (Action<TreeNodeCollection>) (nodes =>
      {
        if (nodes == null)
          return;
        foreach (TreeNode node in nodes)
        {
          if (node.StateImageIndex == 1 && node.Tag is IMTextFaceAttributeProxy)
            selectedElems.Add(node.Tag as IMTextFaceAttributeProxy);
          collectElement(node.Nodes);
        }
      });
      collectElement(this.tvTypeElements.Nodes);
      return selectedElems.ToArray();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tvTypeElements_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    if (!(sender is TreeView treeView) || treeView.HitTest(e.X, e.Y).Location != TreeViewHitTestLocations.StateImage)
      return;
    TreeNode node1 = e.Node;
    object tag = node1.Tag;
    if (node1.StateImageIndex != 0 && node1.StateImageIndex != 1 || tag == null)
      return;
    node1.StateImageIndex = (node1.StateImageIndex + 1) % 2;
    if (node1.Level == 0)
    {
      foreach (TreeNode node2 in node1.Nodes)
        node2.StateImageIndex = node1.StateImageIndex;
    }
    else if (node1.StateImageIndex == 0)
      node1.Parent.StateImageIndex = node1.StateImageIndex;
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tvTypeElements_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeNode node = e.Node;
    if (node == null)
      return;
    node.SelectedImageIndex = node.ImageIndex;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    TreeNode treeNode1 = new TreeNode("nodeSlave0_1");
    TreeNode treeNode2 = new TreeNode("nodeMaster0", new TreeNode[1]
    {
      treeNode1
    });
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Cadmech3DSelectTypeElemForm));
    this.pnButtons = new Panel();
    this.btCancel = new Button();
    this.btApply = new Button();
    this.tvTypeElements = new TreeView();
    this.mainImageList = new ImageList(this.components);
    this.stateImageList = new ImageList(this.components);
    this.pnlTop = new Panel();
    this.pbObject = new PictureBox();
    this.lblDescription = new Label();
    this.pnButtons.SuspendLayout();
    this.pnlTop.SuspendLayout();
    ((ISupportInitialize) this.pbObject).BeginInit();
    this.SuspendLayout();
    this.pnButtons.Controls.Add((Control) this.btCancel);
    this.pnButtons.Controls.Add((Control) this.btApply);
    this.pnButtons.Dock = DockStyle.Bottom;
    this.pnButtons.Location = new Point(0, 388);
    this.pnButtons.Name = "pnButtons";
    this.pnButtons.Size = new Size(523, 40);
    this.pnButtons.TabIndex = 2;
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.FlatStyle = FlatStyle.System;
    this.btCancel.ImeMode = ImeMode.NoControl;
    this.btCancel.Location = new Point(397, 7);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(121, 27);
    this.btCancel.TabIndex = 1;
    this.btCancel.Text = "Отмена";
    this.btApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btApply.DialogResult = DialogResult.OK;
    this.btApply.Enabled = false;
    this.btApply.FlatStyle = FlatStyle.System;
    this.btApply.ImeMode = ImeMode.NoControl;
    this.btApply.Location = new Point(270, 7);
    this.btApply.Name = "btApply";
    this.btApply.Size = new Size(121, 27);
    this.btApply.TabIndex = 0;
    this.btApply.Text = "Применить";
    this.tvTypeElements.Dock = DockStyle.Fill;
    this.tvTypeElements.FullRowSelect = true;
    this.tvTypeElements.ImageIndex = 0;
    this.tvTypeElements.ImageList = this.mainImageList;
    this.tvTypeElements.Location = new Point(0, 30);
    this.tvTypeElements.Name = "tvTypeElements";
    treeNode1.Name = "Node1";
    treeNode1.Text = "nodeSlave0_1";
    treeNode2.Name = "Node0";
    treeNode2.Text = "nodeMaster0";
    this.tvTypeElements.Nodes.AddRange(new TreeNode[1]
    {
      treeNode2
    });
    this.tvTypeElements.SelectedImageIndex = 0;
    this.tvTypeElements.Size = new Size(523, 358);
    this.tvTypeElements.StateImageList = this.stateImageList;
    this.tvTypeElements.TabIndex = 3;
    this.tvTypeElements.AfterSelect += new TreeViewEventHandler(this.tvTypeElements_AfterSelect);
    this.tvTypeElements.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.tvTypeElements_NodeMouseClick);
    this.mainImageList.ColorDepth = ColorDepth.Depth8Bit;
    this.mainImageList.ImageSize = new Size(32 /*0x20*/, 32 /*0x20*/);
    this.mainImageList.TransparentColor = Color.Transparent;
    this.stateImageList.ColorDepth = ColorDepth.Depth8Bit;
    this.stateImageList.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.stateImageList.TransparentColor = Color.Transparent;
    this.pnlTop.Controls.Add((Control) this.pbObject);
    this.pnlTop.Controls.Add((Control) this.lblDescription);
    this.pnlTop.Dock = DockStyle.Top;
    this.pnlTop.Location = new Point(0, 0);
    this.pnlTop.Name = "pnlTop";
    this.pnlTop.Size = new Size(523, 30);
    this.pnlTop.TabIndex = 23;
    this.pbObject.ErrorImage = (Image) null;
    this.pbObject.Image = (Image) componentResourceManager.GetObject("pbObject.Image");
    this.pbObject.ImeMode = ImeMode.NoControl;
    this.pbObject.InitialImage = (Image) null;
    this.pbObject.Location = new Point(4, 4);
    this.pbObject.Name = "pbObject";
    this.pbObject.Size = new Size(20, 20);
    this.pbObject.SizeMode = PictureBoxSizeMode.Zoom;
    this.pbObject.TabIndex = 17;
    this.pbObject.TabStop = false;
    this.lblDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lblDescription.ImeMode = ImeMode.NoControl;
    this.lblDescription.Location = new Point(30, 4);
    this.lblDescription.Name = "lblDescription";
    this.lblDescription.Size = new Size(462, 20);
    this.lblDescription.TabIndex = 0;
    this.lblDescription.TextAlign = ContentAlignment.MiddleLeft;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(523, 428);
    this.Controls.Add((Control) this.tvTypeElements);
    this.Controls.Add((Control) this.pnlTop);
    this.Controls.Add((Control) this.pnButtons);
    this.Name = nameof (Cadmech3DSelectTypeElemForm);
    this.Text = "Выбор типовых элементов модели";
    this.pnButtons.ResumeLayout(false);
    this.pnlTop.ResumeLayout(false);
    ((ISupportInitialize) this.pbObject).EndInit();
    this.ResumeLayout(false);
  }
}
