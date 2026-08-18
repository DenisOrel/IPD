// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.DesignerSerializationService
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// 
/// </summary>
internal class DesignerSerializationService : IDesignerSerializationService
{
  private IDesignerHost _host;

  /// <summary>Конструктор.</summary>
  /// <param name="host"></param>
  public DesignerSerializationService(IDesignerHost host) => this._host = host;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ctrl"></param>
  /// <param name="L"></param>
  private void ScanChildCoontrols(Control ctrl, List<string> L)
  {
    if (ctrl == null || L.Contains(ctrl.Name))
      return;
    L.Add(ctrl.Name);
    if (!ctrl.HasChildren)
      return;
    foreach (Control control in (ArrangedElementCollection) ctrl.Controls)
      this.ScanChildCoontrols(control, L);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objects"></param>
  /// <returns></returns>
  public object Serialize(ICollection objects)
  {
    XmlDocument doc = new XmlDocument();
    XmlNode element = (XmlNode) doc.CreateElement("Root");
    List<string> L = new List<string>();
    object[] collection = new object[objects.Count];
    objects.CopyTo((Array) collection, 0);
    List<object> objectList = new List<object>((IEnumerable<object>) collection);
    foreach (object obj in (IEnumerable) objects)
    {
      if (obj is Control ctrl)
      {
        if (!L.Contains(ctrl.Name))
        {
          Control parent = ctrl.Parent;
          bool flag = false;
          for (; parent != null && parent.GetType() != typeof (DesForm); parent = parent.Parent)
          {
            if (objectList.Contains((object) parent))
            {
              flag = true;
              break;
            }
          }
          if (flag)
            continue;
        }
        else
          continue;
      }
      this.ScanChildCoontrols(ctrl, L);
      element.AppendChild(ImXmlWriter.WriteObject(doc, obj, this._host));
    }
    doc.AppendChild(element);
    MemoryStream outStream = new MemoryStream();
    doc.Save((Stream) outStream);
    return (object) outStream;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="serializationData"></param>
  /// <returns></returns>
  public ICollection Deserialize(object serializationData)
  {
    if (serializationData is MemoryStream)
    {
      MemoryStream inStream = serializationData as MemoryStream;
      inStream.Position = 0L;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load((Stream) inStream);
      XmlNode firstChild = xmlDocument.FirstChild;
      if (firstChild.Name == "Root")
      {
        ArrayList arrayList = new ArrayList();
        foreach (XmlNode childNode in firstChild.ChildNodes)
        {
          object obj = ImXmlReader.ReadObject(childNode, this._host);
          if (obj != null)
            arrayList.Add(obj);
        }
        return (ICollection) arrayList;
      }
    }
    return (ICollection) null;
  }
}
