// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadSketchObject
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Xml;

#nullable disable
namespace Intermech.TechAcad.Connector;

public class TechAcadSketchObject : SingleThreadedObject, ISketchObject
{
  protected internal string _sketchID;
  protected internal string _name;
  protected internal long _orderID;
  private readonly IDraftObject _draftObject;
  protected internal ITPObject _tpObject;

  private void Initialize()
  {
    this._sketchID = "";
    this._name = "";
    this._orderID = 0L;
  }

  public TechAcadSketchObject(IDraftObject draftObject, ITPObject tpObject)
  {
    this._draftObject = draftObject;
    this._tpObject = tpObject;
    this.Initialize();
  }

  public TechAcadSketchObject(ISketchObject sketchObject)
  {
    this.Initialize();
    if (sketchObject == null)
      return;
    this._draftObject = sketchObject.DraftObject;
    if (sketchObject is TechAcadSketchObject acadSketchObject)
      this._tpObject = acadSketchObject.TPObject;
    this._sketchID = sketchObject.SketchID;
    this._name = sketchObject.Name;
    this._orderID = sketchObject.OrderID;
  }

  public XmlNode Save(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return (XmlNode) null;
    XmlElement element1 = xmlDoc.CreateElement(nameof (TechAcadSketchObject));
    XmlNode element2 = (XmlNode) xmlDoc.CreateElement("SketchID");
    XmlNode element3 = (XmlNode) xmlDoc.CreateElement("SketchName");
    XmlNode element4 = (XmlNode) xmlDoc.CreateElement("OrderID");
    element1.AppendChild(element2);
    element1.AppendChild(element3);
    element1.AppendChild(element4);
    element2.InnerText = this.SketchID;
    element3.InnerText = this.Name;
    element4.InnerText = this.OrderID.ToString();
    return (XmlNode) element1;
  }

  public void Load(XmlNode xmlNode)
  {
    if (xmlNode == null || !xmlNode.Name.Equals(nameof (TechAcadSketchObject)))
      return;
    XmlElement xmlElement1 = xmlNode["SketchID"];
    XmlElement xmlElement2 = xmlNode["SketchName"];
    XmlElement xmlElement3 = xmlNode["OrderID"];
    if (xmlElement1 != null)
      this._sketchID = xmlElement1.InnerText;
    if (xmlElement2 != null)
      this._name = xmlElement2.InnerText;
    if (xmlElement3 == null)
      return;
    this._orderID = Convert.ToInt64(xmlElement3.InnerText);
  }

  public IDraftObject DraftObject
  {
    get => this._draftObject;
    set
    {
    }
  }

  public string Name
  {
    get => this._name;
    set => this._name = value;
  }

  public int ReadOnly => 0;

  public string SketchID
  {
    get => this._sketchID;
    set => this._sketchID = value;
  }

  public long OrderID
  {
    get => this._orderID;
    set
    {
      if (this._orderID == value)
        return;
      this._orderID = value;
      this.Status |= ChangeStatus.Modified;
      if (this.TPObject != null)
      {
        if (!(this.TPObject is TechAcadTPObject tpObject))
          return;
        tpObject.SaveDraftInfo();
      }
      else
      {
        if (!(this.DraftObject is TechAcadDraftObject draftObject))
          return;
        draftObject.SaveStucture();
      }
    }
  }

  public ITPObject TPObject => this._tpObject;

  public ChangeStatus Status { get; set; } = ChangeStatus.Added;
}
