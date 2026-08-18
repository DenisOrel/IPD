// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.RequestXmlDocCreator
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Xml;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

internal class RequestXmlDocCreator
{
  private IUserSession _Session;
  private long _RequestObjectID;
  private XmlDocument _XmlDoc = new XmlDocument();

  private RequestXmlDocCreator()
  {
  }

  public RequestXmlDocCreator(IUserSession Asession, long ArequestID, string AschemeData)
    : this()
  {
    this._Session = Asession;
    this._RequestObjectID = ArequestID;
    this._XmlDoc.LoadXml(AschemeData);
  }

  public XmlDocument GetRequestXmlDocument()
  {
    this.ProcessGetChildNode((XmlNode) this._XmlDoc, this._RequestObjectID);
    return this._XmlDoc;
  }

  private void ProcessGetChildNode(XmlNode Node, long requestID)
  {
    if (Node.Attributes != null)
    {
      foreach (XmlNode attribute in (XmlNamedNodeMap) Node.Attributes)
        this.ProcessGetChildNode(attribute, requestID);
    }
    if (!Node.HasChildNodes)
      return;
    foreach (XmlNode childNode in Node.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element)
        this.ProcessGetChildNode(childNode, requestID);
      else if (childNode.NodeType == XmlNodeType.Text)
      {
        string str = RequestXmlDocCreator.ReplaceBrackets(this._Session.SessionGUID, childNode.Value, requestID);
        childNode.Value = str;
      }
    }
  }

  public static string ReplaceBrackets(Guid ASessionGuid, string AValue, long AObjectID)
  {
    string str = AValue;
    if (str != string.Empty)
    {
      IUserSession sessionById = UserSession.GetSessionByID(ASessionGuid);
      if (sessionById != null)
      {
        while (str.IndexOf('[') > -1 && str.IndexOf(']') > str.IndexOf('['))
        {
          string oldValue = str.Substring(str.IndexOf('['), str.IndexOf(']') - (str.IndexOf('[') - 1));
          if (oldValue.Length > 2)
          {
            string AttributeName = oldValue.Substring(1, oldValue.Length - 2);
            string newValue = "";
            IDBObject dbObject = sessionById.GetObject(AObjectID, false);
            if (dbObject != null)
            {
              IDBAttribute byName = dbObject.Attributes.FindByName(AttributeName);
              if (byName != null)
                newValue = byName.AsString;
              str = str.Replace(oldValue, newValue);
            }
          }
          else if (oldValue.Length == 2)
            str = str.Replace(oldValue, "");
        }
      }
    }
    return str;
  }
}
