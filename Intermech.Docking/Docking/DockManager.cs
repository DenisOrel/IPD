
// Type: Intermech.Docking.DockManager
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;


namespace Intermech.Docking;

[DefaultEvent("DockingStarted")]
[Designer(typeof (DockManagerDesigner))]
[ToolboxBitmap(typeof (DockManager))]
public class DockManager : Component, IDockManager
{
  private bool _updating;
  internal DocumentContainer _documentContainer;
  internal ArrayList _dockContainers;
  internal ArrayList _dockControls;
  private DockControl _activeDockControl;
  private bool _integralClose;
  private RendererBase _renderer;
  private ImageList _imageList;
  private DockingHints _dockingHints;
  private DockingManager _dockingManager;
  private Form _ownerForm;
  private DockManager.GetDockControlCallback _getDockControlCallback;
  private static Size _DefaultSize = new Size(100, 100);

  public event DockManager.DockControlActivatingHandler DockControlActivating;

  public event DockControlEventHandler DockControlActivated;

  public event DockControlEventHandler DockControlDeactivated;

  public event EventHandler DockingFinished;

  public event EventHandler DockingStarted;

  public event EventHandler RendererChanged;

  public event ShowControlContextMenuEventHandler ShowControlContextMenu;

  public DockManager()
  {
    this._dockingHints = DockingHints.TranslucentFill;
    this._dockingManager = DockingManager.Standard;
    this._renderer = (RendererBase) new WhidbeyRenderer();
    this._dockContainers = new ArrayList();
    this._dockControls = new ArrayList();
    this._imageList = (ImageList) null;
    this._integralClose = false;
  }

  private void LayoutContainers()
  {
    foreach (DockContainer dockContainer in this._dockContainers)
      dockContainer.LayoutNeeded();
  }

  private FloatingDockContainer CreateFloatingDockContainer(Font font)
  {
    FloatingDockContainer floatingDockContainer = new FloatingDockContainer();
    floatingDockContainer.Font = new Font(font, font.Style);
    floatingDockContainer.Manager = this;
    if (this.OwnerForm != null)
      this.OwnerForm.AddOwnedForm(floatingDockContainer.GetForm());
    return floatingDockContainer;
  }

  private string SizeToString(Size size)
  {
    return (string) TypeDescriptor.GetConverter(typeof (Size)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) size, typeof (string));
  }

  private Size StringToSize(string value)
  {
    value = value.Replace(';', CultureInfo.InvariantCulture.TextInfo.ListSeparator[0]);
    return (Size) TypeDescriptor.GetConverter(typeof (Size)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) value);
  }

  private Point StringToPoint(string value)
  {
    value = value.Replace(';', CultureInfo.InvariantCulture.TextInfo.ListSeparator[0]);
    return (Point) TypeDescriptor.GetConverter(typeof (Point)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) value);
  }

  private Rectangle StringToRectangle(string value)
  {
    value = value.Replace(';', CultureInfo.InvariantCulture.TextInfo.ListSeparator[0]);
    return (Rectangle) TypeDescriptor.GetConverter(typeof (Rectangle)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) value);
  }

  private DockContainer FindDockContainer(Guid guid)
  {
    foreach (DockContainer dockContainer in this._dockContainers)
    {
      if (dockContainer.Guid == guid)
        return dockContainer;
    }
    return (DockContainer) null;
  }

  public DockContainer GetDockContainer(DockStyle style)
  {
    return this.GetDockContainer(style, (Control) null);
  }

  [Browsable(false)]
  public DocumentContainer DocumentContainer
  {
    get => this.GetDocumentContainer();
    set
    {
      this._documentContainer = value;
      if (this._documentContainer == null || this._documentContainer.Manager != null)
        return;
      this._documentContainer.SetHardManager(this);
    }
  }

  private DocumentContainer GetDocumentContainer(Control parent)
  {
    if (parent == null)
      return (DocumentContainer) null;
    foreach (Control control in (ArrangedElementCollection) parent.Controls)
    {
      if (control is DocumentContainer documentContainer1)
        return documentContainer1;
      DocumentContainer documentContainer2 = this.GetDocumentContainer(control);
      if (documentContainer2 != null)
        return documentContainer2;
    }
    return (DocumentContainer) null;
  }

  internal DocumentContainer GetDocumentContainer()
  {
    if (this._documentContainer != null)
      return this._documentContainer;
    if (this._ownerForm == null)
      return (DocumentContainer) null;
    foreach (Control control in (ArrangedElementCollection) this._ownerForm.Controls)
    {
      if (control is DocumentContainer documentContainer1)
        return documentContainer1;
      DocumentContainer documentContainer2 = this.GetDocumentContainer(control);
      if (documentContainer2 != null)
        return documentContainer2;
    }
    return (DocumentContainer) null;
  }

  private SplitLayoutSystem ReadSplitLayoutSystem(XmlNode xmlNode)
  {
    Size size = this.StringToSize(xmlNode.Attributes["WorkingSize"].Value);
    Orientation splitMode = (Orientation) Enum.Parse(typeof (Orientation), xmlNode.Attributes["SplitMode"].Value);
    LayoutSystemBase[] layoutSystems = new LayoutSystemBase[int.Parse(xmlNode.Attributes["LayoutSystems"].Value)];
    int num = 0;
    foreach (XmlNode childNode in xmlNode.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "SplitLayoutSystem")
        layoutSystems[num++] = (LayoutSystemBase) this.ReadSplitLayoutSystem(childNode);
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "DocumentLayoutSystem")
        layoutSystems[num++] = (LayoutSystemBase) this.ReadLayoutSystem(childNode, true);
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "ControlLayoutSystem")
        layoutSystems[num++] = (LayoutSystemBase) this.ReadLayoutSystem(childNode, false);
    }
    return new SplitLayoutSystem(size.Width, size.Height, splitMode, layoutSystems);
  }

  internal void AddDockContainer(DockContainer value)
  {
    if (this._dockContainers.Contains((object) value))
      return;
    this._dockContainers.Add((object) value);
    if (!(value is DocumentContainer))
      return;
    (value as DocumentContainer).IntegralClose = this.IntegralClose;
  }

  internal void AddDockControl(DockControl value)
  {
    if (this._dockControls.Contains((object) value))
      return;
    this._dockControls.Add((object) value);
  }

  internal void DisposeFloatingContainer(FloatingDockContainer fdc)
  {
    fdc.HideForm();
    if (this.OwnerForm != null)
      this.OwnerForm.RemoveOwnedForm(fdc.GetForm());
    fdc.Manager = (DockManager) null;
    fdc.Controls.Clear();
    fdc.Dispose();
  }

  private void OwnerForm_Deactivate(object A_0, EventArgs A_1)
  {
    foreach (DockContainer dockContainer in this._dockContainers)
    {
      if (!dockContainer.IsFloating)
        dockContainer.Form_Deactivate(A_0, A_1);
    }
  }

  private void OwnerForm_Activated(object sender, EventArgs e)
  {
    DockContainer documentContainer = (DockContainer) this.DocumentContainer;
    if (documentContainer != null)
    {
      bool flag = false;
      if (documentContainer.Manager == null)
      {
        documentContainer.SetManager(this);
        flag = true;
      }
      try
      {
        documentContainer.Form_Activated(sender, e);
      }
      finally
      {
        if (flag)
          documentContainer.SetManager((DockManager) null);
      }
    }
    foreach (DockContainer dockContainer in this._dockContainers)
    {
      if (!dockContainer.IsFloating)
        dockContainer.Form_Activated(sender, e);
    }
  }

  internal DockContainer GetDockContainer(DockStyle style, Control control)
  {
    foreach (DockContainer dockContainer in this._dockContainers)
    {
      if (dockContainer.Dock == style && (control == null || control == dockContainer.Parent))
        return dockContainer;
    }
    return (DockContainer) null;
  }

  private ControlLayoutSystem ReadLayoutSystem(XmlNode xmlNode, bool isDocument)
  {
    Size size = this.StringToSize(xmlNode.Attributes["WorkingSize"].Value);
    bool flag = bool.Parse(xmlNode.Attributes["Collapsed"].Value);
    DockControl selectedControl = (DockControl) null;
    if (xmlNode.Attributes["SelectedControl"] != null)
      selectedControl = this.FindDockControl(int.Parse(xmlNode.Attributes["SelectedControl"].Value));
    int length = int.Parse(xmlNode.Attributes["Controls"].Value);
    int num1 = 0;
    if (xmlNode.Attributes["PopupSize"] != null)
      num1 = int.Parse(xmlNode.Attributes["PopupSize"].Value);
    DockControl[] controls = new DockControl[length];
    int num2 = 0;
    foreach (XmlNode childNode1 in xmlNode.ChildNodes)
    {
      if (childNode1.NodeType == XmlNodeType.Element && !(childNode1.Name != "Controls"))
      {
        foreach (XmlNode childNode2 in childNode1.ChildNodes)
        {
          if (childNode2.NodeType == XmlNodeType.Element && childNode2.Name == "Control")
          {
            int id = int.Parse(childNode2.Attributes["ID"].Value);
            if (this.FindDockControl(id) != null)
              controls[num2++] = this.FindDockControl(id);
          }
        }
      }
    }
    ControlLayoutSystem controlLayoutSystem = !isDocument ? new ControlLayoutSystem(size.Width, size.Height, controls, selectedControl) : (ControlLayoutSystem) new DocumentLayoutSystem(size.Width, size.Height, controls, selectedControl);
    controlLayoutSystem.Collapsed = flag;
    controlLayoutSystem.PopupSize = num1;
    return controlLayoutSystem;
  }

  private void ReadFloatingDockContainer(XmlNode xmlNode, FloatingDockContainer dockContainer)
  {
    Rectangle rectangle = this.StringToRectangle(xmlNode.Attributes["Bounds"].Value);
    bool visible = bool.Parse(xmlNode.Attributes["Visible"].Value);
    if (dockContainer == null)
      dockContainer = this.CreateFloatingDockContainer(((Control) this._dockContainers[0]).Font);
    foreach (XmlNode childNode in xmlNode.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "SplitLayoutSystem")
        dockContainer.LayoutSystem = this.ReadSplitLayoutSystem(childNode);
    }
    if (dockContainer.Empty)
      dockContainer.Dispose();
    else
      dockContainer.SetWindowPos(rectangle, visible, false);
  }

  private void WriteDockContainer(DockContainer dockContainer, XmlTextWriter writer)
  {
    if (dockContainer is FloatingDockContainer floatingDockContainer)
    {
      writer.WriteStartElement("FloatingContainer");
      writer.WriteAttributeString("Bounds", (string) TypeDescriptor.GetConverter(typeof (Rectangle)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) floatingDockContainer.GetBounds(), typeof (string)));
      writer.WriteAttributeString("Visible", floatingDockContainer.Visible.ToString());
      this.WriteLayoutSystem((LayoutSystemBase) dockContainer.LayoutSystem, writer);
      writer.WriteEndElement();
    }
    else
    {
      writer.WriteStartElement("Container");
      writer.WriteAttributeString("Guid", dockContainer.Guid.ToString());
      Size size = dockContainer.Size;
      if (dockContainer.Collapsed || dockContainer.Empty)
      {
        if (dockContainer.Dock == DockStyle.Left || dockContainer.Dock == DockStyle.Right)
          size.Width = dockContainer.ContentSize;
        else if (dockContainer.Dock == DockStyle.Top || dockContainer.Dock == DockStyle.Bottom)
          size.Height = dockContainer.ContentSize;
      }
      writer.WriteAttributeString("Size", this.SizeToString(size));
      this.WriteLayoutSystem((LayoutSystemBase) dockContainer.LayoutSystem, writer);
      writer.WriteEndElement();
    }
  }

  private void WriteDockControl(DockControl dc, XmlTextWriter writer, int persistId)
  {
    dc.PersistId = persistId;
    writer.WriteStartElement("Window");
    writer.WriteAttributeString("Guid", dc.Guid.ToString());
    writer.WriteAttributeString("PersistString", dc.PersistString);
    writer.WriteAttributeString("ID", dc.PersistId.ToString());
    writer.WriteAttributeString("Text", dc.Text);
    writer.WriteAttributeString("FloatingSize", this.SizeToString(dc.FloatingSize));
    writer.WriteAttributeString("FloatingLocation", (string) TypeDescriptor.GetConverter(typeof (Point)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) dc.FloatingLocation, typeof (string)));
    writer.WriteAttributeString("DockLocation", dc.DockLocation.ToString());
    if (dc.TabImageIndex != -1)
      writer.WriteAttributeString("TabImageIndex", dc.TabImageIndex.ToString());
    else if (dc.TabImage != null)
    {
      try
      {
        writer.WriteAttributeString("TabImageStream", this.ImageToBase64(dc.TabImage, ImageFormat.Png));
      }
      catch
      {
      }
    }
    writer.WriteEndElement();
  }

  private void WriteLayoutSystem(LayoutSystemBase system, XmlTextWriter writer)
  {
    switch (system)
    {
      case SplitLayoutSystem _:
        writer.WriteStartElement("SplitLayoutSystem");
        break;
      case DocumentLayoutSystem _:
        writer.WriteStartElement("DocumentLayoutSystem");
        break;
      case ControlLayoutSystem _:
        writer.WriteStartElement("ControlLayoutSystem");
        break;
      default:
        return;
    }
    writer.WriteAttributeString("WorkingSize", this.SizeToString(new Size((int) system._workingSize.Width, (int) system._workingSize.Height)));
    switch (system)
    {
      case SplitLayoutSystem _:
        SplitLayoutSystem splitLayoutSystem = (SplitLayoutSystem) system;
        writer.WriteAttributeString("SplitMode", splitLayoutSystem.SplitMode.ToString());
        int persistableCount1 = splitLayoutSystem.LayoutSystems.PersistableCount;
        writer.WriteAttributeString("LayoutSystems", persistableCount1.ToString());
        IEnumerator enumerator = splitLayoutSystem.LayoutSystems.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            LayoutSystemBase current = (LayoutSystemBase) enumerator.Current;
            if (current.ContainsPersistableDockControls)
              this.WriteLayoutSystem(current, writer);
          }
          break;
        }
        finally
        {
          if (enumerator is IDisposable disposable)
            disposable.Dispose();
        }
      case ControlLayoutSystem _:
      case DocumentLayoutSystem _:
        ControlLayoutSystem controlLayoutSystem = (ControlLayoutSystem) system;
        writer.WriteAttributeString("Collapsed", controlLayoutSystem.Collapsed.ToString());
        int persistableCount2 = controlLayoutSystem.Controls.PersistableCount;
        writer.WriteAttributeString("Controls", persistableCount2.ToString());
        int persistId;
        if (controlLayoutSystem.SelectedControl != null && controlLayoutSystem.SelectedControl.PersistState)
        {
          XmlTextWriter xmlTextWriter = writer;
          persistId = controlLayoutSystem.SelectedControl.PersistId;
          string str = persistId.ToString();
          xmlTextWriter.WriteAttributeString("SelectedControl", str);
        }
        int popupSize = controlLayoutSystem.PopupSize;
        writer.WriteAttributeString("PopupSize", popupSize.ToString());
        writer.WriteStartElement("Controls");
        foreach (DockControl control in (CollectionBase) controlLayoutSystem.Controls)
        {
          if (control.PersistState)
          {
            writer.WriteStartElement("Control");
            XmlTextWriter xmlTextWriter = writer;
            persistId = control.PersistId;
            string str = persistId.ToString();
            xmlTextWriter.WriteAttributeString("ID", str);
            writer.WriteEndElement();
          }
        }
        writer.WriteEndElement();
        break;
    }
    writer.WriteEndElement();
  }

  public void LoadConfiguration(
    DockManagerConfigurationStorage configStorage,
    DockManager.GetDockControlCallback getDockControlCallback)
  {
    if (configStorage == null)
      throw new ArgumentNullException(nameof (configStorage));
    this._getDockControlCallback = getDockControlCallback;
    string layout = configStorage.TryLoadLayout();
    if (string.IsNullOrEmpty(layout))
      return;
    this.SetLayout(layout);
  }

  public void SaveConfiguration(DockManagerConfigurationStorage configStorage)
  {
    if (configStorage == null)
      throw new ArgumentNullException(nameof (configStorage));
    string layout = this.GetLayout();
    configStorage.SaveLayout(layout);
  }

  internal FloatingDockContainer CreateFloatingDockContainer()
  {
    return this._dockContainers.Count != 0 ? this.CreateFloatingDockContainer(((Control) this._dockContainers[0]).Font) : this.CreateFloatingDockContainer(Control.DefaultFont);
  }

  public DockControl FindDockControl(Guid guid)
  {
    foreach (DockControl dockControl in this._dockControls)
    {
      if (dockControl.Guid == guid)
        return dockControl;
    }
    return (DockControl) null;
  }

  private DockControl FindDockControl(int id)
  {
    foreach (DockControl dockControl in this._dockControls)
    {
      if (dockControl.PersistId == id)
        return dockControl;
    }
    return (DockControl) null;
  }

  internal DockControl FindMostRecentlyUsedDocument(DockControl exceptDc)
  {
    DockControl recentlyUsedDocument = (DockControl) null;
    DateTime dateTime = DateTime.MinValue;
    foreach (DockControl document in this.GetDocuments())
    {
      if (document.DockLocation == DockLocation.Document && document != exceptDc && document.LastFocused > dateTime)
      {
        dateTime = document.LastFocused;
        recentlyUsedDocument = document;
      }
    }
    return recentlyUsedDocument;
  }

  public DockControl FindDockControl(Guid guid, string persistString)
  {
    foreach (DockControl dockControl in this._dockControls)
    {
      if (dockControl.Guid == guid && dockControl.PersistString == persistString)
        return dockControl;
    }
    return (DockControl) null;
  }

  private DockControl FindDockControl(Guid guid, int persistId)
  {
    foreach (DockControl dockControl in this._dockControls)
    {
      if (dockControl.Guid == guid && dockControl.PersistId == persistId)
        return dockControl;
    }
    return (DockControl) null;
  }

  private void ReadDockContainer(XmlNode xmlNode)
  {
    Guid guid = new Guid(xmlNode.Attributes["Guid"].Value);
    DockContainer dockContainer = this.FindDockContainer(guid);
    if (dockContainer == null)
    {
      DockContainer documentContainer = (DockContainer) this.GetDocumentContainer();
      if (documentContainer == null || documentContainer.Guid != guid)
        return;
      dockContainer = documentContainer;
    }
    Size size = this.StringToSize(xmlNode.Attributes["Size"].Value);
    foreach (XmlNode childNode in xmlNode.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && !(childNode.Name != "SplitLayoutSystem"))
      {
        dockContainer.LayoutSystem = this.ReadSplitLayoutSystem(childNode);
        if (dockContainer.Collapsed || dockContainer.Empty)
          dockContainer.UpdateContentSize(size);
        else
          dockContainer.Size = size;
      }
    }
  }

  internal void RemoveDockContainer(DockContainer container)
  {
    if (!this._dockContainers.Contains((object) container))
      return;
    this._dockContainers.Remove((object) container);
  }

  internal void RemoveDockControl(DockControl dockControl)
  {
    if (!this._dockControls.Contains((object) dockControl))
      return;
    this._dockControls.Remove((object) dockControl);
  }

  private string ImageToBase64(Image image, ImageFormat format)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      image.Save((Stream) memoryStream, format);
      return Convert.ToBase64String(memoryStream.ToArray());
    }
  }

  private Image Base64ToImage(string base64String)
  {
    byte[] buffer = Convert.FromBase64String(base64String);
    MemoryStream memoryStream = new MemoryStream(buffer, 0, buffer.Length);
    memoryStream.Write(buffer, 0, buffer.Length);
    return Image.FromStream((Stream) memoryStream, true);
  }

  private void ReadDockControl(XmlNode xmlNode)
  {
    Guid guid = new Guid(xmlNode.Attributes["Guid"].Value);
    string persistString = xmlNode.Attributes["PersistString"].Value;
    int num = int.Parse(xmlNode.Attributes["ID"].Value);
    string text = xmlNode.Attributes["Text"].Value;
    DockControl dockControl = this.FindDockControl(guid, persistString);
    if (dockControl == null && this._getDockControlCallback != null)
      dockControl = this._getDockControlCallback(guid, persistString, text);
    if (dockControl == null)
      return;
    dockControl.LastFocused = DateTime.Now;
    Size size = this.StringToSize(xmlNode.Attributes["FloatingSize"].Value);
    Point point = this.StringToPoint(xmlNode.Attributes["FloatingLocation"].Value);
    DockLocation A_2 = (DockLocation) Enum.Parse(typeof (DockLocation), xmlNode.Attributes["DockLocation"].Value);
    dockControl.SetFloatingValues(size, point, A_2);
    dockControl.PersistId = num;
    if (dockControl.Manager == null)
      dockControl.Manager = this;
    if (dockControl.TabImageIndex == -1)
    {
      XmlAttribute attribute = xmlNode.Attributes["TabImageIndex"];
      if (attribute != null)
      {
        dockControl.TabImageIndex = int.Parse(attribute.Value);
        dockControl.ShowImageInDocumentTab = dockControl.TabImageIndex != -1;
      }
    }
    if (dockControl.TabImageIndex != -1 || dockControl.TabImage != null)
      return;
    XmlAttribute attribute1 = xmlNode.Attributes["TabImageStream"];
    if (attribute1 == null)
      return;
    Image image = this.Base64ToImage(attribute1.Value);
    dockControl.TabImage = image;
    dockControl.ShowImageInDocumentTab = image != null;
  }

  internal void OnDockControlActivating(DockControl dc, CancelEventArgs args)
  {
    if (this.DockControlActivating == null)
      return;
    this.DockControlActivating(dc, args);
  }

  internal void Lock() => this._updating = true;

  internal void UnLock() => this._updating = false;

  internal void OnDockControlActivated(DockControl dc)
  {
    if (dc.IsDisposed || this._activeDockControl == dc)
      return;
    if (this._activeDockControl != null)
      this.OnDockControlDeactivated(this._activeDockControl);
    this._activeDockControl = dc;
    dc?.Activated();
    if (dc.IsDisposed || this.DockControlActivated == null || this._updating)
      return;
    this.DockControlActivated((object) this, new DockControlEventArgs(dc));
  }

  internal void OnDockControlDeactivated(DockControl dc)
  {
    if (dc != null)
    {
      dc.Deactivated();
      if (this.DockControlDeactivated != null && !this._updating)
        this.DockControlDeactivated((object) this, new DockControlEventArgs(dc));
    }
    this._activeDockControl = (DockControl) null;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      DockContainer[] dockContainerArray = new DockContainer[this._dockContainers.Count];
      this._dockContainers.CopyTo((Array) dockContainerArray);
      foreach (Component component in dockContainerArray)
        component.Dispose();
      this._activeDockControl = (DockControl) null;
    }
    base.Dispose(disposing);
  }

  public DockControl[] GetDockControls()
  {
    DockControl[] dockControls = new DockControl[this._dockControls.Count];
    this._dockControls.CopyTo((Array) dockControls);
    return dockControls;
  }

  public DockControl[] GetDocuments()
  {
    List<DockControl> dockControlList = new List<DockControl>();
    DocumentContainer documentContainer = this.GetDocumentContainer();
    if (documentContainer != null)
    {
      foreach (DockControl control in (ArrangedElementCollection) documentContainer.Controls)
      {
        if (control.DockLocation == DockLocation.Document)
          dockControlList.Add(control);
      }
    }
    return dockControlList.ToArray();
  }

  public string GetLayout()
  {
    StringWriter w = new StringWriter();
    XmlTextWriter writer = new XmlTextWriter((TextWriter) w);
    writer.Formatting = Formatting.None;
    writer.WriteStartDocument();
    writer.WriteStartElement("Layout");
    int num = 0;
    foreach (DockControl dockControl in this._dockControls)
    {
      if (dockControl.PersistState && dockControl.IsInContainer)
        this.WriteDockControl(dockControl, writer, num++);
    }
    foreach (DockContainer dockContainer in this._dockContainers)
      this.WriteDockContainer(dockContainer, writer);
    DockContainer documentContainer = (DockContainer) this.GetDocumentContainer();
    if (documentContainer != null && this._dockContainers.IndexOf((object) documentContainer) == -1)
      this.WriteDockContainer(documentContainer, writer);
    writer.WriteEndElement();
    writer.WriteEndDocument();
    writer.Flush();
    writer.Close();
    return w.ToString();
  }

  protected internal virtual void OnDockingFinished(EventArgs e)
  {
    if (this.DockingFinished == null)
      return;
    this.DockingFinished((object) this, e);
  }

  protected internal virtual void OnDockingStarted(EventArgs e)
  {
    if (this.DockingStarted == null)
      return;
    this.DockingStarted((object) this, e);
  }

  protected internal virtual void OnRendererChanged()
  {
    if (this.RendererChanged == null)
      return;
    this.RendererChanged((object) this, EventArgs.Empty);
  }

  protected internal virtual void OnShowControlContextMenu(ShowControlContextMenuEventArgs e)
  {
    if (this.ShowControlContextMenu == null)
      return;
    this.ShowControlContextMenu((object) this, e);
  }

  public void SetLayout(string layout)
  {
    if (string.IsNullOrEmpty(layout))
      return;
    List<DockContainer> dockContainerList;
    try
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(layout);
      XmlNode xmlNode = xmlDocument.GetElementsByTagName("Layout")[0];
      if (xmlNode == null || xmlNode.ChildNodes.Count == 0)
        return;
      DockContainer[] dockContainerArray = new DockContainer[this._dockContainers.Count];
      this._dockContainers.CopyTo((Array) dockContainerArray);
      dockContainerList = new List<DockContainer>();
      foreach (DockContainer dockContainer in dockContainerArray)
      {
        dockContainer.RecreateLayout();
        if (dockContainer.IsFloating)
          dockContainerList.Add(dockContainer);
      }
      foreach (XmlNode childNode in xmlNode.ChildNodes)
      {
        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Window")
          this.ReadDockControl(childNode);
        else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Container" && childNode.HasChildNodes)
          this.ReadDockContainer(childNode);
        else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "FloatingContainer" && childNode.HasChildNodes)
        {
          if (dockContainerList.Count != 0)
          {
            this.ReadFloatingDockContainer(childNode, (FloatingDockContainer) dockContainerList[0]);
            dockContainerList.RemoveAt(0);
          }
          else
            this.ReadFloatingDockContainer(childNode, (FloatingDockContainer) null);
        }
      }
    }
    catch
    {
      throw new ArgumentException("The layout information provided could not be interpreted.");
    }
    foreach (FloatingDockContainer fdc in dockContainerList)
      this.DisposeFloatingContainer(fdc);
  }

  private bool ShouldSerializeRenderer() => !(this._renderer is WhidbeyRenderer);

  private void ImageList_Disposed(object A_0, EventArgs A_1) => this.ImageList = (ImageList) null;

  private void ImageList_RecreateHandle(object A_0, EventArgs A_1) => this.LayoutContainers();

  [Category("Appearance")]
  [Description("Indicates the type of visual artifacts drawn to the screen to indicate size and position while docking.")]
  [DefaultValue(typeof (DockingHints), "TranslucentFill")]
  public DockingHints DockingHints
  {
    get => this._dockingHints;
    set => this._dockingHints = value;
  }

  [Browsable(false)]
  public DockControl ActiveDockControl => this._activeDockControl;

  [Browsable(false)]
  public DockControl ActiveDocument
  {
    get
    {
      if (this.ActiveDockControl != null)
        return this.ActiveDockControl;
      return this.GetDocumentContainer()?.ActiveDocument;
    }
  }

  [Description("Indicates whether the close button is displayed inside the active tab.")]
  [Category("Behavior")]
  [DefaultValue(false)]
  public bool IntegralClose
  {
    get => this._integralClose;
    set
    {
      if (value == this._integralClose)
        return;
      this._integralClose = value;
      if (this.DocumentContainer == null)
        return;
      this.DocumentContainer.IntegralClose = this.IntegralClose;
    }
  }

  [DefaultValue(typeof (DockingManager), "Standard")]
  [Category("Behavior")]
  [Description("Indicates the method of user interaction during a docking operation.")]
  public DockingManager DockingManager
  {
    get => this._dockingManager;
    set => this._dockingManager = value;
  }

  [Browsable(false)]
  public Form OwnerForm
  {
    get => this._ownerForm;
    set
    {
      if (this._ownerForm != null && this._ownerForm == value)
        return;
      if (this._ownerForm != null)
      {
        this._ownerForm.Activated -= new EventHandler(this.OwnerForm_Activated);
        this._ownerForm.Deactivate -= new EventHandler(this.OwnerForm_Deactivate);
      }
      this._ownerForm = value;
      if (this._ownerForm == null)
        return;
      this._ownerForm.Activated += new EventHandler(this.OwnerForm_Activated);
      this._ownerForm.Deactivate += new EventHandler(this.OwnerForm_Deactivate);
    }
  }

  [DefaultValue(null)]
  [Category("Appearance")]
  public ImageList ImageList
  {
    get => this._imageList;
    set
    {
      if (this._imageList != null)
      {
        this._imageList.RecreateHandle -= new EventHandler(this.ImageList_RecreateHandle);
        this._imageList.Disposed -= new EventHandler(this.ImageList_Disposed);
      }
      this._imageList = value;
      if (this._imageList != null)
      {
        this._imageList.RecreateHandle += new EventHandler(this.ImageList_RecreateHandle);
        this._imageList.Disposed += new EventHandler(this.ImageList_Disposed);
      }
      this.LayoutContainers();
    }
  }

  [Description("The renderer used to calculate object metrics and draw contents.")]
  [Category("Appearance")]
  public RendererBase Renderer
  {
    get => this._renderer;
    set
    {
      if (value == null)
        throw new ArgumentNullException();
      if (this._renderer != null)
        this._renderer.Dispose();
      this._renderer = value;
      this.OnRendererChanged();
      this.LayoutContainers();
    }
  }

  public override ISite Site
  {
    get => base.Site;
    set
    {
      base.Site = value;
      if (value == null)
        return;
      IDesignerHost service = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      if (service == null || !(service.RootComponent is Form))
        return;
      this._ownerForm = (Form) service.RootComponent;
    }
  }

  public delegate void DockControlActivatingHandler(DockControl control, CancelEventArgs args);

  public delegate DockControl GetDockControlCallback(Guid guid, string persistString, string text);
}
