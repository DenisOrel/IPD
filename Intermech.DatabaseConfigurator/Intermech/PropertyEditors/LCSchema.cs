// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCSchema
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Localization;
using Intermech.Map;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCSchema
{
  private int category;
  private int categoryId = -1;
  private byte[] drawDataArray;
  private bool defaultDraw;
  private DataSet lcDataset;
  private ArrayList lcSteps = new ArrayList();
  private ArrayList lcLinks = new ArrayList();
  private MapPalette mapPalette;
  private LCView lcView;
  private bool readOnly;
  private static PointF myNextNodePos = LCSchema.InitNodePos();

  public int Category => this.category;

  public int CategoryId => this.categoryId;

  public ArrayList LcSteps => this.lcSteps;

  private ArrayList LcLinks => this.lcLinks;

  public LCView LCView => this.lcView;

  public bool ReadOnly
  {
    get => this.readOnly;
    set
    {
      this.readOnly = value;
      this.lcView.AllowCopy = !this.readOnly;
      this.lcView.AllowDelete = !this.readOnly;
      this.lcView.AllowDragOut = !this.readOnly;
      this.lcView.AllowDrop = !this.readOnly;
      this.lcView.AllowEdit = !this.readOnly;
      this.lcView.AllowInsert = !this.readOnly;
      this.lcView.AllowKey = !this.readOnly;
      this.lcView.AllowLink = !this.readOnly;
      this.lcView.AllowMove = !this.readOnly;
    }
  }

  public LCSchema(MapPalette aMapPalette, LCView aLCView)
  {
    this.mapPalette = aMapPalette;
    this.lcView = aLCView;
  }

  public void FillPalette()
  {
    DataTable dataTable = DataHolders.LevelsHolder.LoadData(false);
    this.mapPalette.Document.BeginUpdateViews();
    this.mapPalette.Document.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      this.mapPalette.Document.Add((MapObject) new LCNode(Convert.ToInt32(row["F_LEVEL_ID"])));
    this.mapPalette.Document.EndUpdateViews();
  }

  private IDBLCSchema GetSchemaObject(IUserSession session)
  {
    int schemaID = -1;
    switch (this.category)
    {
      case 4:
        IDBObjectType objectType = session.GetObjectType(this.categoryId);
        if (objectType != null)
        {
          schemaID = objectType.SchemaID;
          break;
        }
        break;
      case 16 /*0x10*/:
        schemaID = this.categoryId;
        break;
      default:
        throw new Exception("Bad category");
    }
    return session.GetLCSchema(schemaID);
  }

  public string GetSchemaName()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBLCSchema schemaObject = this.GetSchemaObject(sessionKeeper.Session);
      return schemaObject != null ? schemaObject.Name : LocalizationHolder.rm.GetString("DatabaseConfigurator_7");
    }
  }

  private DataSet GetSchemaData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBLCSchema schemaObject = this.GetSchemaObject(sessionKeeper.Session);
      this.drawDataArray = schemaObject.DrawData;
      DataSet schema = schemaObject.GetStepsCollection().GetSchema();
      if (this.category == 4)
      {
        foreach (DataRow row in (InternalDataCollectionBase) schema.Tables["IMS_LC_STEPS"].Rows)
        {
          row["F_OBJECT_TYPE"] = (object) this.categoryId;
          row.AcceptChanges();
        }
      }
      return schema;
    }
  }

  private void SetSchemaData(DataSet aDataset)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBLCSchema schemaObject = this.GetSchemaObject(sessionKeeper.Session);
      schemaObject.GetStepsCollection().SetSchema(aDataset);
      schemaObject.DrawData = this.drawDataArray;
    }
  }

  public void LoadSchema(int aCategoryId, int aCategory)
  {
    this.category = aCategory;
    this.categoryId = aCategoryId;
    this.lcDataset = this.GetSchemaData();
    this.FillSteps();
    this.FillLinks();
  }

  private void FillSteps()
  {
    this.lcSteps.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) this.lcDataset.Tables["IMS_LC_STEPS"].Rows)
      this.lcSteps.Add((object) new LCStepObject(new DBLifecycleStepProperties(row), this));
  }

  internal int FindLCLinkPD(int aFrom, int aTo)
  {
    int lcLinkPd = -1;
    for (int index = 0; index < this.lcLinks.Count; ++index)
    {
      LCStepsLinkProperties stepLinkProperties = ((LCLinkObject) this.lcLinks[index]).LCStepLinkProperties;
      if (stepLinkProperties.FromStepID == aFrom && stepLinkProperties.ToStepID == aTo || stepLinkProperties.FromStepID == aTo && stepLinkProperties.ToStepID == aFrom)
      {
        lcLinkPd = index;
        break;
      }
    }
    return lcLinkPd;
  }

  private void FillLinks()
  {
    this.lcLinks.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) this.lcDataset.Tables["IMS_LC_LINKS"].Rows)
    {
      int lcLinkPd = this.FindLCLinkPD(Convert.ToInt32(row["F_FROM_STEP"]), Convert.ToInt32(row["F_TO_STEP"]));
      if (lcLinkPd != -1)
      {
        ((LCLinkObject) this.lcLinks[lcLinkPd]).Reversible = true;
        ((LCLinkObject) this.lcLinks[lcLinkPd]).ReversibleParams = Convert.ToInt32(row["F_PARAMS"]);
      }
      else
        this.lcLinks.Add((object) new LCLinkObject(new LCStepsLinkProperties(row), this));
    }
  }

  public void WriteSchema() => this.SaveDataSet(this.FillResultDataSet(this.SchemaToXml()));

  private DataSet FillResultDataSet(XmlDocument aXmlDocument)
  {
    DataSet dataSet = this.lcDataset.Copy();
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) this.lcDataset.Tables["IMS_LC_STEPS"].Rows)
      {
        LCNode nodeByStepId = this.lcView.LCDocument.FindNodeByStepId(Convert.ToInt32(row["F_LC_STEP"]));
        if (nodeByStepId == null)
          row["F_DELETED"] = (object) 1;
        else
          DBLifecycleStepProperties.StoreToDataRow(nodeByStepId.LCStepObject.LCStepProperties, false, row);
      }
      MapLayerCollectionObjectEnumerator enumerator = this.lcView.LCDocument.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current is LCNode current && current.LCStepObject.LCStepProperties.LCStep < 0)
        {
          DataRow dataRow = this.lcDataset.Tables["IMS_LC_STEPS"].NewRow();
          DBLifecycleStepProperties.StoreToDataRow(current.LCStepObject.LCStepProperties, false, dataRow);
          this.lcDataset.Tables["IMS_LC_STEPS"].Rows.Add(dataRow);
        }
      }
    }
    finally
    {
      this.lcDataset.Tables["IMS_LC_STEPS"].AcceptChanges();
    }
    this.lcDataset.Tables["IMS_LC_LINKS"].Rows.Clear();
    try
    {
      MapLayerCollectionObjectEnumerator enumerator = this.lcView.LCDocument.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current is LCLink current)
        {
          DataRow dataRow1 = this.lcDataset.Tables["IMS_LC_LINKS"].NewRow();
          LCStepsLinkProperties.StoreToDataRow(current.LCLinkObject.LCStepLinkProperties, dataRow1);
          this.lcDataset.Tables["IMS_LC_LINKS"].Rows.Add(dataRow1);
          if (current.LCLinkObject.Reversible)
          {
            DataRow dataRow2 = this.lcDataset.Tables["IMS_LC_LINKS"].NewRow();
            LCStepsLinkProperties.StoreToDataRow(current.LCLinkObject.LCStepLinkProperties, dataRow2);
            object obj = dataRow2["F_FROM_STEP"];
            dataRow2["F_FROM_STEP"] = dataRow2["F_TO_STEP"];
            dataRow2["F_TO_STEP"] = obj;
            dataRow2["F_PARAMS"] = (object) current.LCLinkObject.ReversibleParams;
            this.lcDataset.Tables["IMS_LC_LINKS"].Rows.Add(dataRow2);
          }
        }
      }
    }
    finally
    {
      this.lcDataset.Tables["IMS_LC_LINKS"].AcceptChanges();
    }
    using (MemoryStream outStream = new MemoryStream())
    {
      aXmlDocument.Save((Stream) outStream);
      this.drawDataArray = outStream.ToArray();
    }
    return dataSet;
  }

  private void SaveDataSet(DataSet safeDs)
  {
    if (!this.readOnly)
    {
      try
      {
        this.SetSchemaData(this.lcDataset);
      }
      catch
      {
        this.lcDataset = safeDs;
        throw;
      }
    }
    DataSet dataSet = (DataSet) null;
    if (!this.readOnly)
      dataSet = this.GetSchemaData();
    foreach (MapObject mapObject1 in (MapDocument) this.lcView.LCDocument)
    {
      if (mapObject1 is LCNode lcNode)
      {
        if (lcNode.LCStepObject.LCStepProperties.LCStep >= 0)
          lcNode.LCStepObject.Apply((object) lcNode.LCStepObject.LCStepProperties.LCStep);
        else if (!this.readOnly)
        {
          DataRow[] dataRowArray = dataSet.Tables["IMS_LC_STEPS"].Select($"F_GUID='{lcNode.LCStepObject.LCStepProperties.StepGuid.ToString()}'");
          if (dataRowArray.Length != 0)
          {
            DBLifecycleStepProperties lcStepProperties = lcNode.LCStepObject.LCStepProperties;
            int lcStep = lcStepProperties.LCStep;
            int int32 = Convert.ToInt32(dataRowArray[0]["F_LC_STEP"]);
            lcStepProperties.LCStep = int32;
            lcNode.LCStepObject.LCStepProperties = lcStepProperties;
            foreach (MapObject mapObject2 in (MapDocument) this.lcView.LCDocument)
            {
              if (mapObject2 is LCLink lcLink)
              {
                LCStepsLinkProperties stepLinkProperties = lcLink.LCLinkObject.LCStepLinkProperties;
                bool flag = false;
                if (stepLinkProperties.FromStepID == lcStep)
                {
                  stepLinkProperties.FromStepID = int32;
                  flag = true;
                }
                if (stepLinkProperties.ToStepID == lcStep)
                {
                  stepLinkProperties.ToStepID = int32;
                  flag = true;
                }
                if (flag)
                  lcLink.LCLinkObject.LCStepLinkProperties = stepLinkProperties;
              }
            }
            lcNode.LCStepObject.Apply((object) lcStep);
          }
        }
      }
    }
    if (this.readOnly)
      return;
    this.lcDataset = dataSet;
  }

  public bool FillView()
  {
    this.defaultDraw = this.drawDataArray == null || this.drawDataArray.Length == 0;
    this.lcView.Document.Clear();
    this.XmlToSchema(this.DrawDataToXml(this.drawDataArray), this.defaultDraw);
    return true;
  }

  public void ClearDrawInfoStep() => this.drawDataArray = (byte[]) null;

  private XmlDocument DrawDataToXml(byte[] drawData)
  {
    if (drawData == null || drawData.Length == 0)
      return (XmlDocument) null;
    XmlDocument xml = new XmlDocument();
    using (MemoryStream inStream = new MemoryStream(drawData))
      xml.Load((Stream) inStream);
    return xml;
  }

  private void XmlToSchema(XmlDocument xmlDocument, bool defaultDraw)
  {
    LCSchema.myNextNodePos = LCSchema.InitNodePos();
    XmlNode xmlNode1 = (XmlNode) null;
    if (!defaultDraw)
    {
      xmlNode1 = xmlDocument.SelectSingleNode("//schema//nodes");
      xmlDocument.SelectSingleNode("//schema//links");
    }
    this.lcView.StartTransaction();
    try
    {
      for (int index = 0; index < this.lcSteps.Count; ++index)
      {
        LCNode lcNode = new LCNode((LCStepObject) this.lcSteps[index]);
        if (defaultDraw)
        {
          lcNode.Position = LCSchema.NextNodePosition();
        }
        else
        {
          XmlNode xmlNode2 = xmlNode1.SelectSingleNode($"node[@guid='{XmlConvert.ToString(lcNode.LCStepObject.LCStepProperties.StepGuid)}']");
          if (xmlNode2 == null)
          {
            lcNode.Position = LCSchema.NextNodePosition();
          }
          else
          {
            lcNode.PartID = Convert.ToInt32(xmlNode2.Attributes["part"].Value);
            lcNode.Port.PartID = Convert.ToInt32(xmlNode2.Attributes["port"].Value);
            float x;
            try
            {
              x = (float) Convert.ToDouble(xmlNode2.Attributes["x"].Value, (IFormatProvider) CultureInfo.InvariantCulture);
            }
            catch
            {
              x = (float) Convert.ToDouble(xmlNode2.Attributes["x"].Value);
            }
            float y;
            try
            {
              y = (float) Convert.ToDouble(xmlNode2.Attributes["y"].Value, (IFormatProvider) CultureInfo.InvariantCulture);
            }
            catch
            {
              y = (float) Convert.ToDouble(xmlNode2.Attributes["y"].Value);
            }
            lcNode.Position = new PointF(x, y);
          }
        }
        this.lcView.Document.Add((MapObject) lcNode);
      }
      for (int index = 0; index < this.lcLinks.Count; ++index)
      {
        LCLinkObject lcLink1 = (LCLinkObject) this.lcLinks[index];
        int fromStepId = lcLink1.LCStepLinkProperties.FromStepID;
        int toStepId = lcLink1.LCStepLinkProperties.ToStepID;
        LCNode nodeByStepId1 = this.lcView.LCDocument.FindNodeByStepId(fromStepId);
        LCNode nodeByStepId2 = this.lcView.LCDocument.FindNodeByStepId(toStepId);
        LCLink lcLink2 = new LCLink();
        lcLink2.ToArrow = true;
        if (lcLink1.Reversible)
          lcLink2.FromArrow = true;
        lcLink2.FromPort = (IMapPort) nodeByStepId1.Port;
        lcLink2.ToPort = (IMapPort) nodeByStepId2.Port;
        lcLink2.LCLinkObject = lcLink1;
        this.lcView.Document.Add((MapObject) lcLink2);
      }
    }
    finally
    {
      this.lcView.FinishTransaction("");
    }
  }

  private static PointF InitNodePos() => new PointF(40f, 30f);

  public static PointF NextNodePosition()
  {
    PointF nextNodePos = LCSchema.myNextNodePos;
    LCSchema.myNextNodePos.X += 250f;
    if ((double) LCSchema.myNextNodePos.X <= 400.0)
      return nextNodePos;
    LCSchema.myNextNodePos.X = 40f;
    LCSchema.myNextNodePos.Y += 150f;
    return nextNodePos;
  }

  private XmlDocument SchemaToXml()
  {
    XmlDocument xml = new XmlDocument();
    XmlElement element1 = xml.CreateElement("schema");
    xml.AppendChild((XmlNode) element1);
    XmlElement element2 = xml.CreateElement("nodes");
    element1.AppendChild((XmlNode) element2);
    foreach (MapObject mapObject in this.lcView.Document)
    {
      if (mapObject is LCNode lcNode)
      {
        XmlElement element3 = xml.CreateElement("node");
        element3.SetAttribute("part", XmlConvert.ToString(lcNode.PartID));
        element3.SetAttribute("port", XmlConvert.ToString(lcNode.Port.PartID));
        element3.SetAttribute("guid", XmlConvert.ToString(lcNode.LCStepObject.LCStepProperties.StepGuid));
        element3.SetAttribute("x", lcNode.Position.X.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        element3.SetAttribute("y", lcNode.Position.Y.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        element2.AppendChild((XmlNode) element3);
      }
    }
    XmlElement element4 = xml.CreateElement("links");
    element1.AppendChild((XmlNode) element4);
    foreach (MapObject mapObject1 in this.lcView.Document)
    {
      if (mapObject1 is LCLink lcLink)
      {
        XmlElement element5 = xml.CreateElement("link");
        element5.SetAttribute("part", XmlConvert.ToString(lcLink.PartID));
        if (lcLink.FromPort != null)
        {
          MapPort mapObject2 = (MapPort) lcLink.FromPort.MapObject;
          element5.SetAttribute("fromport", XmlConvert.ToString(mapObject2.PartID));
          element5.SetAttribute("fromarrow", XmlConvert.ToString(lcLink.FromArrow ? 1 : 0));
        }
        if (lcLink.ToPort != null)
        {
          MapPort mapObject3 = (MapPort) lcLink.ToPort.MapObject;
          element5.SetAttribute("toport", XmlConvert.ToString(mapObject3.PartID));
          element5.SetAttribute("toarrow", XmlConvert.ToString(lcLink.ToArrow ? 1 : 0));
        }
        element4.AppendChild((XmlNode) element5);
      }
    }
    return xml;
  }
}
