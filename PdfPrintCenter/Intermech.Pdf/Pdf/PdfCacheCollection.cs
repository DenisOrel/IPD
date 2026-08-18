// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfCacheCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

internal class PdfCacheCollection
{
  private Dictionary<Font, byte[]> m_fontData = new Dictionary<Font, byte[]>();
  private Dictionary<Font, int> m_fontOffsets;
  private List<List<object>> m_referenceObjects = new List<List<object>>();

  public void Clear()
  {
    if (this.m_referenceObjects != null)
    {
      int index = 0;
      for (int count = this.m_referenceObjects.Count; index < count; ++index)
        this.m_referenceObjects[index].Clear();
      this.m_referenceObjects.Clear();
    }
    if (this.m_fontOffsets != null)
      this.m_fontOffsets.Clear();
    if (this.m_fontData == null)
      return;
    this.m_fontData.Clear();
  }

  public bool Contains(IPdfCache obj)
  {
    bool flag = false;
    if (obj != null)
      flag = this.GetGroup(obj) != null;
    return flag;
  }

  private List<object> CreateNewGroup()
  {
    List<object> newGroup = new List<object>();
    this.m_referenceObjects.Add(newGroup);
    return newGroup;
  }

  private List<object> GetGroup(IPdfCache result)
  {
    if (result != null)
    {
      for (int index = this.m_referenceObjects.Count - 1; index >= 0; --index)
      {
        if (this.m_referenceObjects.Count > index)
        {
          List<object> referenceObject = this.m_referenceObjects[index];
          if (referenceObject.Count > 0)
          {
            IPdfCache pdfCache = (IPdfCache) referenceObject[0];
            if (result.EqualsTo(pdfCache))
              return referenceObject;
          }
          else
            this.RemoveGroup(referenceObject);
        }
      }
    }
    return (List<object>) null;
  }

  public int GroupCount(IPdfCache obj)
  {
    int num = 0;
    if (obj != null)
    {
      List<object> group = this.GetGroup(obj);
      if (group != null)
        num = group.Count;
    }
    return num;
  }

  public void Remove(IPdfCache obj)
  {
    if (obj == null)
      return;
    List<object> group = this.GetGroup(obj);
    if (group == null)
      return;
    group.Remove((object) obj);
    if (group.Count != 0)
      return;
    this.RemoveGroup(group);
  }

  private void RemoveGroup(List<object> group)
  {
    if (group == null)
      return;
    this.m_referenceObjects.Remove(group);
  }

  public IPdfCache Search(IPdfCache obj)
  {
    IPdfCache pdfCache = (IPdfCache) null;
    List<object> objectList = this.GetGroup(obj);
    if (objectList == null)
      objectList = this.CreateNewGroup();
    else if (objectList.Count > 0)
      pdfCache = (IPdfCache) objectList[0];
    objectList.Add((object) obj);
    return pdfCache;
  }

  internal Dictionary<Font, byte[]> FontData => this.m_fontData;

  internal Dictionary<Font, int> FontOffsetTable
  {
    get
    {
      if (this.m_fontOffsets == null)
        this.m_fontOffsets = new Dictionary<Font, int>();
      return this.m_fontOffsets;
    }
  }

  private List<object> this[int index] => this.m_referenceObjects[index];
}
