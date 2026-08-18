// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.RequiredSigns
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using Intermech.Signs.Interfaces;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Workflow;

public class RequiredSigns
{
  private GraphsSet gset;
  private bool _modified;

  public GraphsSet GraphsSet => this.gset;

  public bool IsEmpty => this.gset.Count == 0;

  public RequiredSigns(IDBAttribute attr)
    : this(attr.Value.ToString())
  {
  }

  public RequiredSigns(string xml)
  {
    MemoryStream source = new MemoryStream(Encoding.UTF8.GetBytes(xml));
    try
    {
      if (source.Length == 0L)
        this.gset = new GraphsSet();
      else
        this.gset = GraphsSet.Load((Stream) source);
    }
    finally
    {
      source.Close();
    }
  }

  public RequiredSigns() => this.gset = new GraphsSet();

  public void Save(IDBObject obj)
  {
    IDBAttribute attributeById = obj.GetAttributeByID(wfConsts.AttrRequiredSignsID);
    if (attributeById == null)
      return;
    this.Save(attributeById);
  }

  public void Save(IDBAttribute attr)
  {
    MemoryStream destination = new MemoryStream();
    try
    {
      this.gset.Save((Stream) destination);
      destination.Position = 0L;
      StreamReader streamReader = new StreamReader((Stream) destination);
      try
      {
        attr.Value = (object) streamReader.ReadToEnd();
      }
      finally
      {
        streamReader.Close();
      }
    }
    finally
    {
      destination.Close();
    }
  }

  public bool Modified
  {
    get => this._modified;
    set => this._modified = value;
  }

  public GraphClass Add(string graphValue, bool strongSign, int groupID)
  {
    GraphsCollection graphsCollection = this.gset[groupID.ToString()];
    GraphClass graphClass = new GraphClass(graphValue, strongSign, false);
    if (graphsCollection == null)
    {
      this.gset.Add(groupID.ToString(), new GraphsCollection()
      {
        graphClass
      });
      this._modified = true;
    }
    else if (!graphsCollection.Contains(graphClass))
    {
      graphsCollection.Add(graphClass);
      this._modified = true;
    }
    return graphClass;
  }

  /// <summary>Deletes GraphClass from collection</summary>
  /// <param name="graph"></param>
  /// <param name="groupID"></param>
  /// <returns>Number of items remained in group</returns>
  public int Delete(GraphClass graph, int groupID)
  {
    GraphsCollection graphsCollection = this.gset[groupID.ToString()];
    if (graphsCollection == null || !graphsCollection.Contains(graph))
      return -1;
    this._modified = true;
    graphsCollection.Remove(graph);
    int count = graphsCollection.Count;
    if (count != 0)
      return count;
    this.gset.Remove(groupID.ToString());
    return count;
  }
}
