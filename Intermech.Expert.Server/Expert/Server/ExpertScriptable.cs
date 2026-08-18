// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertScriptable
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Server;

public abstract class ExpertScriptable : 
  ExpertObject,
  IExpertScriptable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  private bool setting;
  public XmlDocument xDoc;
  private byte[] zippedScr;
  protected ExpertScriptType objType = ExpertScriptType.CommonCalc;
  public long[] scriptLinks;
  internal AttributeRoles[] attrRoles = new AttributeRoles[0];

  public ExpertScriptable(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._objType = ExpertObjType.Script;
  }

  protected override void LoadBLOBData(byte[] data) => this.zippedScr = data;

  protected override byte[] SaveBLOBData() => this.zippedScr;

  private void Unpack(byte[] zipScr) => this.xDoc = ZlibHelper.UnpackXmlBuffer(zipScr);

  public void UnpackXML() => this.Unpack(this.zippedScr);

  private void UpdateScript(byte[] data)
  {
    try
    {
      this.Unpack(data);
      this.CollectInfo();
      this.UpdateScriptInCache();
      this.zippedScr = new byte[data.Length];
      Array.Copy((Array) data, (Array) this.zippedScr, data.Length);
    }
    catch
    {
      this.xDoc = (XmlDocument) null;
      throw;
    }
  }

  internal virtual void UpdateScriptInCache()
  {
  }

  private void CollectInfo()
  {
    ArrayList attribs = new ArrayList();
    ArrayList attrGUIDs = new ArrayList();
    ArrayList otGUIDs = new ArrayList();
    ArrayList objLinks = new ArrayList();
    ArrayList aRoles = new ArrayList();
    if (this.xDoc != null && this.xDoc.DocumentElement != null && this.xDoc.DocumentElement.HasChildNodes)
    {
      foreach (XmlNode childNode in this.xDoc.DocumentElement.ChildNodes)
        this.ProcessNode(childNode, attribs, attrGUIDs, otGUIDs, objLinks, aRoles);
    }
    this.attribs = (AttribPair[]) Array.CreateInstance(typeof (AttribPair), attribs.Count);
    this.attrGUIDs = (string[]) Array.CreateInstance(typeof (string), attrGUIDs.Count);
    this.objTypeGUIDs = (string[]) Array.CreateInstance(typeof (string), otGUIDs.Count);
    this.objectLinks = (long[]) Array.CreateInstance(typeof (long), objLinks.Count);
    this.attrRoles = (AttributeRoles[]) Array.CreateInstance(typeof (AttributeRoles), aRoles.Count);
    attribs.CopyTo((Array) this.attribs);
    attrGUIDs.CopyTo((Array) this.attrGUIDs);
    otGUIDs.CopyTo((Array) this.objTypeGUIDs);
    if (this.scriptLinks == null || this.scriptLinks.Length != objLinks.Count)
      this.scriptLinks = new long[objLinks.Count];
    objLinks.CopyTo((Array) this.scriptLinks);
    aRoles.CopyTo((Array) this.attrRoles);
  }

  private bool AddAttr(
    ArrayList attribs,
    ArrayList attrGUIDs,
    ArrayList otGUIDs,
    int attrID,
    int otID,
    string attrGUID,
    string otGUID)
  {
    for (int index = 0; index < attrGUIDs.Count; ++index)
    {
      if (string.Compare((string) attrGUIDs[index], attrGUID) == 0 && string.Compare((string) otGUIDs[index], otGUID) == 0)
        return false;
    }
    attribs.Add((object) new AttribPair(attrID, otID));
    attrGUIDs.Add((object) attrGUID);
    otGUIDs.Add((object) otGUID);
    if (!ExpertServer.es.idents.ContainsKey(new Guid(attrGUID)))
      ExpertServer.es.idents.GetOrAdd(new Guid(attrGUID), (long) attrID);
    if (otGUID != "" && !ExpertServer.es.idents.ContainsKey(new Guid(otGUID)))
      ExpertServer.es.idents.GetOrAdd(new Guid(otGUID), (long) otID);
    return true;
  }

  private void ProcessNode(
    XmlNode node,
    ArrayList attribs,
    ArrayList attrGUIDs,
    ArrayList otGUIDs,
    ArrayList objLinks,
    ArrayList aRoles)
  {
    int num = 0;
    if (node.NodeType == XmlNodeType.Element && node.Name == "set-kind")
      this.setting = true;
    else if (node.NodeType == XmlNodeType.Element && node.Name == "Attr-Info")
    {
      int attrID = 0;
      int otID = 0;
      string attrGUID = "";
      string otGUID = "";
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (childNode.Name == "attrID")
          attrID = Convert.ToInt32(childNode.InnerText);
        else if (childNode.Name == "objTypeID")
          otID = Convert.ToInt32(childNode.InnerText);
        else if (childNode.Name == "Full-Attr")
        {
          attrGUID = childNode.ChildNodes[0].InnerText;
          otGUID = childNode.ChildNodes[1].InnerText;
        }
      }
      if (!(attrGUID != "") || !this.AddAttr(attribs, attrGUIDs, otGUIDs, attrID, otID, attrGUID, otGUID))
        return;
      aRoles.Add((object) AttributeRoles.argHorz);
    }
    else if (node.NodeType == XmlNodeType.Element && node.Name == "Object-Links")
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (childNode.Name == "ID")
        {
          try
          {
            long int64 = Convert.ToInt64(childNode.InnerText);
            if (objLinks.IndexOf((object) int64) < 0)
              objLinks.Add((object) int64);
          }
          catch
          {
          }
        }
      }
    }
    else if (node.NodeType == XmlNodeType.Element && node.Name == "Attr-ObjType")
    {
      int attrID = 0;
      int otID = 0;
      string str1 = "";
      string str2 = "";
      if (node.HasChildNodes)
      {
        if (node.ChildNodes[0].Name == "Attr-Link")
        {
          str1 = node.ChildNodes[0].ChildNodes[0].InnerText;
          if (str1 != "")
          {
            IDBAttributeType attributeType = this.UserSession.GetAttributeType(new Guid(str1), false);
            if (attributeType == null)
              return;
            attrID = attributeType.AttributeID;
          }
        }
        if (node.ChildNodes.Count > 1 && node.ChildNodes[1].Name == "Attr-Link")
        {
          str2 = node.ChildNodes[1].ChildNodes[0].InnerText;
          if (str2 != "")
          {
            IDBObjectType objectType = this.UserSession.GetObjectType(new Guid(str2), false);
            if (objectType == null)
              return;
            otID = objectType.ObjectType;
          }
        }
      }
      if (!(str1 != "") || !this.AddAttr(attribs, attrGUIDs, otGUIDs, attrID, otID, str1, str2))
        return;
      aRoles.Add((object) (AttributeRoles) (this.setting ? 2 : 1));
    }
    else if (node.NodeType == XmlNodeType.Element && node.Name == "Attr-Link")
    {
      num = 0;
      int otID = 0;
      string str = "";
      string otGUID = "";
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (childNode.Name == "GUID")
          str = childNode.InnerText;
      }
      if (!(str != ""))
        return;
      IDBAttributeType attributeType = this.UserSession.GetAttributeType(new Guid(str), false);
      if (attributeType == null)
        return;
      int attributeId = attributeType.AttributeID;
      if (!this.AddAttr(attribs, attrGUIDs, otGUIDs, attributeId, otID, str, otGUID))
        return;
      aRoles.Add((object) AttributeRoles.argHorz);
    }
    else
    {
      if (!node.HasChildNodes)
        return;
      foreach (XmlNode childNode in node.ChildNodes)
        this.ProcessNode(childNode, attribs, attrGUIDs, otGUIDs, objLinks, aRoles);
    }
  }

  protected override List<long> CollectObjectLinks()
  {
    List<long> longList = base.CollectObjectLinks();
    if (this.scriptLinks != null)
    {
      foreach (long scriptLink in this.scriptLinks)
      {
        if (longList.IndexOf(scriptLink) < 0)
          longList.Add(scriptLink);
      }
    }
    return longList;
  }

  protected override void LoadField(UserSession uSession, AttributeValues av)
  {
    if (av.AttributeID != ExpertConsts.Consts.attrAttrRoles)
      return;
    this.attrRoles = (AttributeRoles[]) Array.CreateInstance(typeof (AttributeRoles), av.Values.Length);
    for (int index = 0; index < av.Values.Length; ++index)
      this.attrRoles[index] = ExpertConsts.Str2AttrRole(Convert.ToString(av.Values[index]));
  }

  protected override int GetAttribCount() => base.GetAttribCount() + 1;

  protected override AttributeValues[] CreateAttribs()
  {
    AttributeValues[] attribs = base.CreateAttribs();
    attribs[base.GetAttribCount()] = new AttributeValues(ExpertConsts.Consts.attrAttrRoles, FieldTypes.ftString, MultiValueModes.MultiValues);
    return attribs;
  }

  protected override AttributeValues[] SaveData()
  {
    AttributeValues[] attributeValuesArray = base.SaveData();
    for (int index1 = 0; index1 < attributeValuesArray.Length; ++index1)
    {
      if (attributeValuesArray[index1].AttributeID == ExpertConsts.Consts.attrAttrRoles)
      {
        if (this.attrRoles.Length != 0 && (attributeValuesArray[index1].Values == null || attributeValuesArray[index1].Values[0] == DBNull.Value))
          attributeValuesArray[index1].Values = (object[]) Array.CreateInstance(typeof (string), this.attrRoles.Length);
        for (int index2 = 0; index2 < this.attrRoles.Length; ++index2)
          attributeValuesArray[index1].Values[index2] = (object) ExpertConsts.AttrRole2Str(this.attrRoles[index2]);
      }
    }
    return attributeValuesArray;
  }

  public ScriptTreeNode LoadScript(out ExpertScriptParms parms)
  {
    parms = (ExpertScriptParms) null;
    this.UnpackXML();
    ScriptTreeNode scriptTreeNode = ExpertServer.LoadScriptTree(this.xDoc);
    XmlElement documentElement = this.xDoc.DocumentElement;
    if (documentElement.HasChildNodes)
    {
      foreach (XmlNode childNode in documentElement.ChildNodes)
      {
        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "DocParms")
          parms = new ExpertScriptParms(childNode);
      }
    }
    return scriptTreeNode;
  }

  public virtual void SaveScript(ScriptTreeNode root, ExpertScriptParms parms = null)
  {
    XmlTextWriter writer = (XmlTextWriter) null;
    try
    {
      MemoryStream w = new MemoryStream();
      writer = new XmlTextWriter((Stream) w, Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();
      writer.WriteStartElement("WholeScript");
      parms?.WriteToXml(writer);
      writer.WriteStartElement("ExpScript");
      writer.WriteAttributeString("xmlns", (string) null, "http://www.intermech.ru/Expert-System");
      for (int index = 0; index < root.Items.Count; ++index)
        ScriptTreeNode.WriteNodeToXML(ref writer, (ScriptTreeNode) root.Items[index]);
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Flush();
      w.Position = 0L;
      MemoryStream baseOutputStream = new MemoryStream();
      Deflater deflater = new Deflater(3);
      DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream((Stream) baseOutputStream, deflater);
      deflaterOutputStream.Write(w.GetBuffer(), 0, Convert.ToInt32(w.Length));
      deflaterOutputStream.Flush();
      deflaterOutputStream.Finish();
      this.zippedScr = baseOutputStream.ToArray();
    }
    finally
    {
      writer?.Close();
    }
  }

  protected bool FixOneNode(ScriptTreeNode node, IUserSession ius)
  {
    bool flag1 = false;
    if (node.mod != null)
      flag1 = node.mod.FixIdentsComplete(ius);
    bool flag2 = flag1 || node.op.FixIdentsComplete(ius);
    if (node.Items.Count > 0)
    {
      for (int index = 0; index < node.Items.Count; ++index)
      {
        ScriptTreeNode node1 = (ScriptTreeNode) node.Items[index];
        flag2 = flag2 || this.FixOneNode(node1, ius);
      }
    }
    return flag2;
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    ExpertScriptParms parms = (ExpertScriptParms) null;
    ScriptTreeNode root = this.LoadScript(out parms);
    bool flag = false;
    for (int index = 0; index < root.Items.Count; ++index)
    {
      ScriptTreeNode node = (ScriptTreeNode) root.Items[index];
      flag = flag || this.FixOneNode(node, ius);
    }
    if (this.cond != null)
      flag = flag || this.cond.FixIdentsComplete(ius);
    if (flag)
    {
      this.SaveScript(root, parms);
      this.WriteBLOB();
    }
    return flag;
  }

  public byte[] Script
  {
    get => this.zippedScr;
    set => this.UpdateScript(value);
  }

  public ExpertScriptType ScriptType => this.objType;

  public void UpdateObject(byte[] buffer, string Name)
  {
    this.UpdateScript(buffer);
    this._Name = Name;
    AttributeValues[] valuesList1 = this.SaveData();
    bool flag = true;
    foreach (AttributeValues attributeValues in valuesList1)
    {
      if (attributeValues.AttributeID == ExpertConsts.Consts._attrObjName)
      {
        flag = false;
        break;
      }
    }
    if (flag)
    {
      AttributeValues[] valuesList2 = new AttributeValues[valuesList1.Length + 1];
      valuesList1.CopyTo((Array) valuesList2, 0);
      valuesList2[valuesList2.Length - 1] = new AttributeValues(ExpertConsts.Consts._attrObjName, (object) this._Name);
      this.SetAttributesValues(valuesList2, false, false);
    }
    else
      this.SetAttributesValues(valuesList1, false, false);
  }

  public AttributeRoles[] AttrRoles => this.attrRoles;

  public override bool ReplaceAttr(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode)
  {
    ExpertScriptParms parms = (ExpertScriptParms) null;
    ScriptTreeNode root = this.LoadScript(out parms);
    bool flag = false;
    for (int index = 0; index < root.Items.Count; ++index)
    {
      ScriptTreeNode node = (ScriptTreeNode) root.Items[index];
      flag = flag || this.ReplaceAttrForOneNode(node, session, fromAttribute, toAttribute);
    }
    if (this.cond != null)
      flag = flag || this.cond.PerformAttrChange(fromAttribute, toAttribute);
    if (flag)
    {
      this.SaveScript(root, parms);
      this.WriteBLOB();
    }
    return flag;
  }

  protected bool ReplaceAttrForOneNode(
    ScriptTreeNode node,
    IUserSession ius,
    IDBAttributeType fromAttr,
    IDBAttributeType toAttr)
  {
    bool flag1 = false;
    if (node.mod != null)
      flag1 = node.mod.PerformAttrCombine(fromAttr, toAttr, ius);
    bool flag2 = flag1 || node.op.PerformAttrCombine(fromAttr, toAttr, ius);
    if (node.Items.Count > 0)
    {
      for (int index = 0; index < node.Items.Count; ++index)
      {
        ScriptTreeNode node1 = (ScriptTreeNode) node.Items[index];
        flag2 = flag2 || this.ReplaceAttrForOneNode(node1, ius, fromAttr, toAttr);
      }
    }
    return flag2;
  }
}
