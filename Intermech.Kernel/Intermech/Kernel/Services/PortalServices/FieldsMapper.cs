// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.FieldsMapper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class FieldsMapper
{
  public int idxPrjlinkID { get; private set; }

  public int idxRelationType { get; private set; }

  public int idxGuid { get; private set; }

  public int idxProjID { get; private set; }

  public int idxObjectID { get; private set; }

  public int idxID { get; private set; }

  public int idxObjectType { get; private set; }

  public int idxSiteID { get; private set; }

  public int idxAccessLevel { get; private set; }

  public int idxPublicationNeccesary { get; private set; }

  public int idxCaption { get; private set; }

  public int idxPublishOptions { get; private set; }

  public int idxOwnerID { get; private set; }

  public int idxCheckOutBy { get; private set; }

  public Dictionary<int, int> CustomIndexes { get; private set; }

  public FieldsMapper() => this.Initialize();

  private void Initialize()
  {
    this.Columns = new List<ColumnDescriptor>();
    this.CustomIndexes = new Dictionary<int, int>();
    this.idxPrjlinkID = this.CreateColumn(new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-20, this.idxPrjlinkID);
    this.idxRelationType = this.CreateColumn(new ColumnDescriptor((object) -23, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-23, this.idxRelationType);
    this.idxGuid = this.CreateColumn(new ColumnDescriptor((object) -12, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-12, this.idxGuid);
    this.idxProjID = this.CreateColumn(new ColumnDescriptor((object) -21, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-21, this.idxProjID);
    this.idxObjectID = this.CreateColumn(new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-2, this.idxObjectID);
    this.idxID = this.CreateColumn(new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-3, this.idxID);
    this.idxObjectType = this.CreateColumn(new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-7, this.idxObjectType);
    this.idxSiteID = this.CreateColumn(new ColumnDescriptor((object) -17, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-17, this.idxSiteID);
    this.idxCaption = this.CreateColumn(new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-50, this.idxCaption);
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(PortalConsts.attributePublicationNecessary);
    this.idxPublicationNeccesary = this.CreateColumn(new ColumnDescriptor((object) attributeTypeId1, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(attributeTypeId1, this.idxPublicationNeccesary);
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(PortalConsts.attributePublishOptions);
    this.idxPublishOptions = this.CreateColumn(new ColumnDescriptor((object) attributeTypeId2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(attributeTypeId2, this.idxPublishOptions);
    this.idxOwnerID = this.CreateColumn(new ColumnDescriptor((object) -8, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-8, this.idxOwnerID);
    this.idxCheckOutBy = this.CreateColumn(new ColumnDescriptor((object) -6, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-6, this.idxCheckOutBy);
    this.idxAccessLevel = this.CreateColumn(new ColumnDescriptor((object) -80, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    this.CustomIndexes.Add(-80, this.idxAccessLevel);
  }

  private int CreateColumn(ColumnDescriptor descriptor)
  {
    this.Columns.Add(descriptor);
    return this.Columns.Count - 1;
  }

  public List<ColumnDescriptor> Columns { get; private set; }

  public void AddCustomColumns(List<ColumnDescriptor> columns)
  {
    foreach (ColumnDescriptor column in columns)
      this.AddCustomColumn(column);
  }

  public void AddCustomColumn(ColumnDescriptor column)
  {
    int column1 = this.CreateColumn(column);
    this.CustomIndexes.Add((int) column.AttributeID, column1);
  }
}
