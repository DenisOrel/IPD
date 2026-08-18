// Decompiled with JetBrains decompiler
// Type: Intermech.PdmConfigurator.Server.ServerPDMConfiguratorPlugin
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Search.Pdm.CompositionsConfigurator;
using Intermech.Search.Pdm.Instances;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.PdmConfigurator.Server;

internal class ServerPDMConfiguratorPlugin : IPackage
{
  private static Guid _pluginGuid = new Guid("cad005f6-306c-11d8-b4e9-00304f19f545");
  private ServerPDMConfiguratorPlugin.ServerPDMConfiguratorPluginClass _serverPDMConfiguratorPluginClass = new ServerPDMConfiguratorPlugin.ServerPDMConfiguratorPluginClass();
  internal EventLogHelper _eventLogHelper;
  internal IElementStatusesService _elementStatusesService;
  internal IPdmConfiguratorService _pdmConfiguratorService;
  internal IPluginStatusesTable _pluginStatusesTable;
  private ElementStatusesPluginDescription _pluginDescription = new ElementStatusesPluginDescription(4, "cad005f6-306c-11d8-b4e9-00304f19f545", "cad005fb-306c-11d8-b4e9-00304f19f545", LocalizationHolder.rm.GetString("PdmConfigurator.Server_10"), LocalizationHolder.rm.GetString("PdmConfigurator.Server_11"));
  private CompositionsConfiguratorModule _compositionsConfiguratorModule = new CompositionsConfiguratorModule();

  public void Load(IServiceProvider serviceProvider)
  {
    this._compositionsConfiguratorModule.Load();
    if (!(ServerServices.GetService(typeof (IPdmServerPlugin)) is IPdmServerPlugin))
      throw new PdmConfiguratorExeption(LocalizationHolder.rm.GetString("PdmConfigurator.Server_12"));
    this._eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    this._pluginStatusesTable = serviceProvider.GetService(typeof (IPluginStatusesTable)) as IPluginStatusesTable;
    this.LoadPluginResources(serviceProvider);
    ICustomServices service = ServerServices.GetService(typeof (ICustomServices)) as ICustomServices;
    ServerServices.AddService(typeof (IPdmConfiguratorServerPlugin), (object) this._serverPDMConfiguratorPluginClass);
    service.AddService(typeof (IPdmConfiguratorServerPlugin), (object) this._serverPDMConfiguratorPluginClass);
    PdmConfiguratorService serviceInstance = new PdmConfiguratorService();
    service.AddService(typeof (IPdmConfiguratorService), (object) serviceInstance);
    ServerServices.AddService(typeof (IPdmConfiguratorService), (object) serviceInstance);
    serviceInstance.RegisterAnalyzer((IPdmOptionsAnalyzer) new PdmComplexOptionsAnalyzer());
    serviceInstance.RegisterBrowser((IPdmCompositionBrowser) new PdmCompositionsBrowser());
    this._elementStatusesService = serviceProvider.GetService(typeof (IElementStatusesService)) as IElementStatusesService;
    if (this._elementStatusesService != null)
      this._elementStatusesService.RegisterServerPlugin(this._pluginDescription);
    this._pdmConfiguratorService = ServerServices.GetService(typeof (IPdmConfiguratorService)) as IPdmConfiguratorService;
    IDBObjectCreator creatorInstance = (IDBObjectCreator) new DBConfiguratorObjectsCreator();
    (serviceProvider.GetService(typeof (IDBObjectService)) as ICreatorContainer).AddCreator((object) new Guid("cad015b0-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance, true);
    if (this._eventLogHelper != null)
    {
      this._eventLogHelper.BeforeRecordsSelectEvent += new BeforeRecordsSelectHandler(this.BeforeRecordsSelect);
      this._eventLogHelper.GetRecordsListEvent += new GetRecordsListHandler(this.GetRecordsList);
      this._eventLogHelper.AddAttributeValuesWriteHandler((object) CompositionsConfiguratorConstants.ApplicationConditionsAttributeTypeID, new WriteAttributeValuesHandler(this.WriteAttributeValues));
    }
    service.AddService(typeof (ICompositionConfiguratorServerService), (object) new CompositionConfiguratorServerService((IInstancesServerService) service.GetService(typeof (IInstancesServerService))));
  }

  public void Unload()
  {
  }

  public string Name => Intermech.Interfaces.PdmConfigurator.Consts.PDMConfiguratorPluginName;

  private void BeforeRecordsSelect(object sender, BeforeRecordsSelectEventArgs args)
  {
    bool result1 = true;
    bool result2 = false;
    if (args == null || args.Session == null || !args.Session.EnabledPdmConfigurator || !(sender is DBRelationCollection relationCollection) || relationCollection.RelationTypeID == -1 || !MetaDataHelper.IsPdmPartiallyConfigurableRelationType(relationCollection.RelationTypeID) || relationCollection.FunctionID == SelectFunction.EntersIn || relationCollection.FunctionID == SelectFunction.EntersInVersion)
      return;
    MetaDataHelper.IsPdmConfigurableRelationType(relationCollection.RelationTypeID);
    if (args.OldParameters.Tags != null)
    {
      object tag = args.OldParameters.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"];
    }
    if (args.OldParameters.Tags != null)
      args.OldParameters.Tags[(object) "{32C584B7-5063-4101-890D-E30C5F7BE12B}"] = (object) null;
    if (args.OldParameters.Tags != null && args.OldParameters.Tags[(object) "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}"] != null && !bool.TryParse(args.OldParameters.Tags[(object) "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}"].ToString(), out result1))
      ;
    if (args.OldParameters.Tags != null && args.OldParameters.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] != null && !bool.TryParse(args.OldParameters.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"].ToString(), out result2))
      result2 = false;
    if (result2)
      return;
    if (args.OldParameters.Tags == null)
      args.OldParameters.Tags = new HybridDictionary();
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(1);
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionsLinkID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_RELATION_TYPE, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionsIncompatibilityID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) Intermech.Interfaces.PdmConfigurator.Consts.attributeObjectApplicabilityCondID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    columnDescriptorList.Add(new ColumnDescriptor((object) Intermech.Interfaces.PdmConfigurator.Consts.attributeConfiguratorContextID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
    ColumnDescriptor[] array = columnDescriptorList.ToArray();
    List<int> AddedColumnsPos = new List<int>(0);
    if (columnDescriptorList.Count <= 0)
      return;
    int length = args.OldParameters.Columns != null ? args.OldParameters.Columns.Length : 0;
    args.OldParameters.AddColumnDescriptors(array, AddedColumnsPos);
    args.OldParameters.Tags[(object) "{32C584B7-5063-4101-890D-E30C5F7BE12B}"] = (object) AddedColumnsPos;
    args.OldParameters.Tags[(object) "{32C584B7-5063-4101-890D-E30C5F7BE12B}.ofs"] = (object) length;
    args.NewParameters = new DBRecordSetParams?(args.OldParameters);
  }

  private void GetRecordsList(
    DataTable table,
    object sender,
    DBRecordSetParams parameters,
    IUserSession session)
  {
    if (table == null || session == null || !session.EnabledPdmConfigurator)
      return;
    bool result1 = true;
    bool result2 = false;
    if (!(sender is DBRelationCollection rels) || rels.RelationTypeID == -1 || !MetaDataHelper.IsPdmPartiallyConfigurableRelationType(rels.RelationTypeID))
      return;
    MetaDataHelper.IsPdmConfigurableRelationType(rels.RelationTypeID);
    if (parameters.Tags != null)
    {
      object tag1 = parameters.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"];
    }
    if (parameters.Tags != null && parameters.Tags[(object) "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}"] != null && !bool.TryParse(parameters.Tags[(object) "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}"].ToString(), out result1))
      ;
    if (parameters.Tags != null && parameters.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] != null && !bool.TryParse(parameters.Tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"].ToString(), out result2))
      result2 = false;
    if (result2)
      return;
    try
    {
      this.ConfigureComposition(session, parameters, rels, table);
    }
    catch
    {
    }
    finally
    {
      List<int> tag2 = parameters.Tags != null ? parameters.Tags[(object) "{32C584B7-5063-4101-890D-E30C5F7BE12B}"] as List<int> : (List<int>) null;
      if (tag2 != null && tag2.Count > 0)
      {
        int tag3 = (int) parameters.Tags[(object) "{32C584B7-5063-4101-890D-E30C5F7BE12B}.ofs"];
        int num = Math.Min(table.Columns.Count - tag3 - tag2.Count, 0);
        for (int index = tag2.Count - 1; index >= 0; --index)
          table.Columns.RemoveAt(tag2[index] + num);
      }
      if (parameters.Tags != null)
      {
        parameters.Tags.Remove((object) "{32C584B7-5063-4101-890D-E30C5F7BE12B}");
        parameters.Tags.Remove((object) "{32C584B7-5063-4101-890D-E30C5F7BE12B}.ofs");
      }
    }
  }

  private void WriteAttributeValues(IDBAttribute attribute, AttributeValuesEventArgs args)
  {
    using (UserSessionContext.CaptureSession(attribute.Session))
    {
      try
      {
        if (!((attribute as DBAttribute).ParentObject is IDBRelation dbRelation))
          dbRelation = attribute.Session.GetRelation(attribute.DBObjectID, false);
        if (dbRelation == null || MetaDataHelper.GetAttribute4RelationType(dbRelation.TypeID, CompositionsConfiguratorConstants.ApplicationConditionsAsStringAttributeTypeID) == null)
          return;
        if (PdmConfiguratorCache.OptionsCache != null && PdmConfiguratorCache.OptionsCache.Count == 0)
          PdmConfiguratorCache.CacheLoadOptions(attribute.Session);
        string initValue = new ObjectsApplicabilitiesCriterionsCollection((object) this.CreateStringFromAttributeValues(args.Values)).GenerateStringComments(true, true);
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(CompositionsConfiguratorConstants.ApplicationConditionsAsStringAttributeTypeID);
        if (initValue != null && (long) initValue.Length > attributeType.SizeType)
          initValue = initValue.Substring(0, (int) attributeType.SizeType);
        dbRelation.SetAttributesValues(new AttributeValues[1]
        {
          new AttributeValues(CompositionsConfiguratorConstants.ApplicationConditionsAsStringAttributeTypeID, (object) initValue)
        });
      }
      catch (Exception ex)
      {
        Trace.Write((object) ex);
      }
    }
  }

  private void LoadPluginResources(IServiceProvider serviceProvider)
  {
    Intermech.Interfaces.PdmConfigurator.Consts.Initialize();
    string str = "Intermech.PdmConfigurator.Server.Resources.";
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 0, string.Empty, (byte[]) null);
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 1, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.Configured), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsConfigured.ico"));
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 2, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.ContextNotFound), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsContextNotFound.ico"));
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 3, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.OptionNotFound), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsOptionNotFound.ico"));
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 4, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.OptionValueNotFound), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsOptionValueNotFound.ico"));
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 5, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.ConflictOptionNotFound), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsOptionNotFound.ico"));
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 6, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.ConflictOptionValueNotFound), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsOptionValueNotFound.ico"));
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 7, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.ApplOptionNotFound), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsOptionNotFound.ico"));
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 8, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.ApplOptionValueNotFound), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsOptionValueNotFound.ico"));
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 9, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.Incompatibles), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsIncompatibilities.ico"));
    this._pluginStatusesTable.AddStatus("cad005f6-306c-11d8-b4e9-00304f19f545", 10, EnumDescConverter.GetEnumDescription((Enum) PdmConfiguratorStates.Exception), ServerPDMConfiguratorPlugin.LoadResource(str + "pcsException.ico"));
  }

  private static byte[] LoadResource(string ResourceName)
  {
    Stream stream = (Stream) null;
    try
    {
      stream = typeof (ServerPDMConfiguratorPlugin).Assembly.GetManifestResourceStream(ResourceName);
      if (stream == null)
        return new byte[0];
      byte[] buffer = new byte[stream.Length];
      stream.Read(buffer, 0, buffer.Length);
      return buffer;
    }
    finally
    {
      stream?.Close();
    }
  }

  private void ConfigureComposition(
    IUserSession session,
    DBRecordSetParams parameters,
    DBRelationCollection rels,
    DataTable table)
  {
    if (!(session is IServerSession serverSession))
      return;
    Dictionary<int, short> dictionary = new Dictionary<int, short>();
    if (session == null || rels == null || table == null || table.Rows.Count == 0)
      return;
    int columnIndex1 = DBRecordSet.AttributeColumnIndex(parameters, (object) -2, AttributeSourceTypes.Object, table);
    int columnIndex2 = DBRecordSet.AttributeColumnIndex(parameters, (object) -7, AttributeSourceTypes.Object, table);
    int columnIndex3 = DBRecordSet.AttributeColumnIndex(parameters, (object) Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionsLinkID, AttributeSourceTypes.Object, table);
    int columnIndex4 = DBRecordSet.AttributeColumnIndex(parameters, (object) -21, AttributeSourceTypes.Relation, table);
    int columnIndex5 = DBRecordSet.AttributeColumnIndex(parameters, (object) -20, AttributeSourceTypes.Relation, table);
    int columnIndex6 = DBRecordSet.AttributeColumnIndex(parameters, (object) -23, AttributeSourceTypes.Relation, table);
    int columnIndex7 = DBRecordSet.AttributeColumnIndex(parameters, (object) Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionsIncompatibilityID, AttributeSourceTypes.Object, table);
    int columnIndex8 = DBRecordSet.AttributeColumnIndex(parameters, (object) Intermech.Interfaces.PdmConfigurator.Consts.attributeObjectApplicabilityCondID, AttributeSourceTypes.Relation, table);
    int columnIndex9 = DBRecordSet.AttributeColumnIndex(parameters, (object) Intermech.Interfaces.PdmConfigurator.Consts.attributeConfiguratorContextID, AttributeSourceTypes.Relation, table);
    if (columnIndex1 < 0 || columnIndex2 < 0 || columnIndex3 < 0 || columnIndex4 < 0 || columnIndex5 < 0 || columnIndex6 < 0 || columnIndex7 < 0 || columnIndex8 < 0 || columnIndex9 < 0)
      return;
    long userId = session.UserID;
    List<DataRow> dataRowList = new List<DataRow>(table.Rows.Count);
    ObjectIncompatibilitiesCollection incompatibilitiesCollection = new ObjectIncompatibilitiesCollection();
    ObjectsApplicabilitiesCriterionsCollection criterionsCollection = new ObjectsApplicabilitiesCriterionsCollection();
    this._pdmConfiguratorService = this._pdmConfiguratorService ?? session.GetCustomService(typeof (IPdmConfiguratorService)) as IPdmConfiguratorService;
    RelationPair key1 = parameters.Tags != null ? parameters.Tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] as RelationPair : (RelationPair) null;
    TraceLog traceLog;
    try
    {
      traceLog = parameters.Tags != null ? parameters.Tags[(object) TraceLog.TraceLogGuid] as TraceLog : (TraceLog) null;
    }
    catch
    {
      traceLog = (TraceLog) null;
    }
    RelationPath source1;
    try
    {
      source1 = parameters.Tags != null ? parameters.Tags[(object) RelationPath.RelationPathGuid] as RelationPath : (RelationPath) null;
    }
    catch
    {
      source1 = (RelationPath) null;
    }
    long clientConnectionId = session.ClientConnectionID;
    if (key1 != null && key1.Handle == 0L)
      key1 = new RelationPair(clientConnectionId, key1.TOP_OBJECT_ID, key1.TOP_OBJECT_TYPE, key1.F_PRJLINK_ID, key1.USER_ID, key1.F_PROJ_ID, key1.F_RELATION_TYPE, key1.F_OBJECT_TYPE);
    long num1 = key1 != null ? key1.TOP_OBJECT_ID : 0L;
    int num2 = key1 != null ? key1.TOP_OBJECT_TYPE : -1;
    if (key1 == null)
      key1 = new RelationPair(clientConnectionId, num1, num2, 0L, userId, num1, -1, num2);
    RelationPair key2 = new RelationPair(clientConnectionId, key1.TOP_OBJECT_ID, key1.TOP_OBJECT_TYPE, 0L, key1.USER_ID, key1.TOP_OBJECT_ID, -1, key1.TOP_OBJECT_TYPE);
    if (key1.TOP_OBJECT_ID != 0L && MetaDataHelper.IsPdmRootObjectType(key1.TOP_OBJECT_TYPE) && MetaDataHelper.IsPdmConfigurableObjectType(key1.F_OBJECT_TYPE))
    {
      ObjectOptionsHolder loadObjectOptions = PdmConfiguratorObjectOptionsCache.GetOrLoadObjectOptions(session, key1.TOP_OBJECT_ID);
      if (loadObjectOptions != null && loadObjectOptions.Options != null && loadObjectOptions.Options.Count > 0)
      {
        PdmConfiguratorCache.CacheLoadOptions(session, (IList<long>) loadObjectOptions.Options);
        PdmConfiguratorContext configuratorContext1 = new PdmConfiguratorContext((object) serverSession);
        IDBObject dbObject = session.GetObject(key1.TOP_OBJECT_ID, false);
        configuratorContext1.Key = key2;
        configuratorContext1.LoadFromObject((IDBAttributable) dbObject);
        configuratorContext1.Key = key2;
        this._pdmConfiguratorService[(object) serverSession, key2] = configuratorContext1;
        PdmConfiguratorContext configuratorContext2 = this._pdmConfiguratorService[(object) serverSession, key2];
      }
    }
    PdmConfiguratorContext context = this._pdmConfiguratorService[(object) serverSession, key1];
    if (context != null && key1.F_PRJLINK_ID != 0L && key1.F_PROJ_ID == key1.TOP_OBJECT_ID)
    {
      context.ParentKey = key2;
      this._pdmConfiguratorService[(object) serverSession, key1] = context;
      context = this._pdmConfiguratorService[(object) serverSession, key1];
    }
    ObjectOptionsHolder objectOptionsHolder = (ObjectOptionsHolder) null;
    if (key1 != null && key1.F_PROJ_ID != 0L && MetaDataHelper.IsPdmConfigurableObjectType(key1.F_OBJECT_TYPE))
      objectOptionsHolder = PdmConfiguratorObjectOptionsCache.GetOrLoadObjectOptions(session, key1.F_PROJ_ID);
    if (objectOptionsHolder != null && objectOptionsHolder.Options != null && objectOptionsHolder.Options.Count > 0)
      PdmConfiguratorCache.CacheLoadOptions(session, (IList<long>) objectOptionsHolder.Options);
    Dictionary<long, int> collection = new Dictionary<long, int>();
    Dictionary<long, int> tag = parameters.Tags != null ? parameters.Tags[(object) "{004511C2-5AA8-4831-B60A-7CD17C1A2D88}"] as Dictionary<long, int> : (Dictionary<long, int>) null;
    if (tag != null)
      collection.AddRange<KeyValuePair<long, int>>((IEnumerable<KeyValuePair<long, int>>) tag);
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      DataRow row = table.Rows[index];
      long int64Value1 = DataSetProcessor.GetInt64Value(row, columnIndex1, 0L);
      int int32Value1 = DataSetProcessor.GetInt32Value(row, columnIndex2, -1);
      long int64Value2 = DataSetProcessor.GetInt64Value(row, columnIndex4, 0L);
      long int64Value3 = DataSetProcessor.GetInt64Value(row, columnIndex5, 0L);
      int int32Value2 = DataSetProcessor.GetInt32Value(row, columnIndex6, -1);
      if (int64Value1 != 0L && int32Value1 != -1 && int64Value2 != 0L && int64Value3 != 0L && int32Value2 != -1)
      {
        if (!collection.ContainsKey(int64Value2))
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(int64Value2);
          collection[int64Value2] = objectInfo.ObjectTypeID;
        }
        if (num2 == -1)
        {
          num1 = int64Value2;
          num2 = collection[int64Value2];
        }
        IDBObject dbObject1 = (IDBObject) null;
        IDBRelation source2 = (IDBRelation) null;
        if (!key1.Empty && context == null && key1.F_PRJLINK_ID != 0L && key1.F_PRJLINK_ID != -1L)
        {
          PdmConfiguratorContext configuratorContext = new PdmConfiguratorContext((object) serverSession);
          IDBRelation relation = session.GetRelation(key1.F_PRJLINK_ID, false);
          configuratorContext.LoadFromObject((IDBAttributable) relation);
          configuratorContext.Key = key1;
          if (key1.F_PRJLINK_ID != 0L && key1.F_PROJ_ID == key1.TOP_OBJECT_ID)
            configuratorContext.ParentKey = key2;
          this._pdmConfiguratorService[(object) serverSession, key1] = configuratorContext;
          context = this._pdmConfiguratorService[(object) serverSession, key1];
        }
        RelationPair key3 = Helper.CreateKey(clientConnectionId, num1, num2, userId, int64Value3, int32Value2, int64Value1, int32Value1);
        RelationPair key4 = Helper.CreateKey(clientConnectionId, num1, num2, userId, 0L, -1, int64Value1, int32Value1);
        PdmConfiguratorContext configuratorContext3 = (PdmConfiguratorContext) null;
        string stringValue1 = DataSetProcessor.GetStringValue(row, columnIndex9, string.Empty);
        PdmConfiguratorContext configuratorContext4;
        if (stringValue1.IndexOf("1|") == 0)
        {
          source2 = source2 ?? session.GetRelation(int64Value3, false);
          configuratorContext4 = new PdmConfiguratorContext((object) source2);
        }
        else
          configuratorContext4 = new PdmConfiguratorContext((object) stringValue1);
        configuratorContext4.Key = key3;
        configuratorContext4.ParentKey = key3 == null || !key3.Equals((object) key1) ? key1 : (RelationPair) null;
        if (configuratorContext4 == null)
        {
          configuratorContext3 = this._pdmConfiguratorService[(object) serverSession, key4];
        }
        else
        {
          configuratorContext4.ObjectsOptions.Clear();
          this._pdmConfiguratorService[(object) serverSession, key3] = configuratorContext4;
          configuratorContext3 = this._pdmConfiguratorService[(object) serverSession, key3];
        }
        if (DataSetProcessor.GetInt64Value(row, columnIndex3, 0L) != 0L)
          PdmConfiguratorObjectOptionsCache.GetOrLoadObjectOptions(session, int64Value1);
        string stringValue2 = DataSetProcessor.GetStringValue(row, columnIndex7, string.Empty);
        IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(Intermech.Interfaces.PdmConfigurator.Consts.attributeOptionsIncompatibilityID);
        if (!string.IsNullOrEmpty(stringValue2) && (long) stringValue2.Length == attributeType1.SizeType)
        {
          IDBObject dbObject2 = dbObject1 ?? session.GetObject(int64Value1, false);
          incompatibilitiesCollection.LoadFromObject((IDBAttributable) dbObject2);
        }
        else
          incompatibilitiesCollection.Assign((object) stringValue2);
        if (!incompatibilitiesCollection.Empty)
        {
          PdmConfiguratorResult configuratorResult = incompatibilitiesCollection.Evalute(context);
          if (traceLog != null && source1 != null && !incompatibilitiesCollection.EvaluateTrace.Empty)
          {
            RelationPath key5 = new RelationPath((object) source1);
            if (key5.Items.Count <= 0 || MetaDataHelper.IsPdmConfigurableRelationType(key5.Items[key5.Items.Count - 1].F_RELATION_TYPE) || incompatibilitiesCollection.EvaluateTrace.Flags != PdmConfiguratorResult.ContextNotFound)
            {
              SimpleRelationPair simpleRelationPair = new SimpleRelationPair(int64Value3, int32Value2, int64Value1, int32Value1);
              key5.Items.Add(simpleRelationPair);
              traceLog.Items[key5] = incompatibilitiesCollection.EvaluateTrace.Clone() as TraceEntry;
            }
          }
          if (configuratorResult == PdmConfiguratorResult.True || configuratorResult == PdmConfiguratorResult.Incompatibles)
          {
            dictionary[index] = (short) configuratorResult;
            continue;
          }
        }
        string stringValue3 = DataSetProcessor.GetStringValue(row, columnIndex8, string.Empty);
        IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(Intermech.Interfaces.PdmConfigurator.Consts.attributeObjectApplicabilityCondID);
        if (!string.IsNullOrEmpty(stringValue3) && (long) stringValue3.Length == attributeType2.SizeType)
        {
          IDBRelation dbRelation = source2 ?? session.GetRelation(int64Value3, false);
          criterionsCollection.LoadFromObject((IDBAttributable) dbRelation);
        }
        else
          criterionsCollection.Assign((object) stringValue3);
        if (!criterionsCollection.Empty)
        {
          PdmConfiguratorResult configuratorResult = criterionsCollection.Evalute(context);
          if (traceLog != null && source1 != null && !criterionsCollection.EvaluateTrace.Empty)
          {
            RelationPath key6 = new RelationPath((object) source1);
            SimpleRelationPair simpleRelationPair = new SimpleRelationPair(int64Value3, int32Value2, int64Value1, int32Value1);
            key6.Items.Add(simpleRelationPair);
            if (key6.Items.Count <= 0 || MetaDataHelper.IsPdmConfigurableRelationType(key6.Items[key6.Items.Count - 1].F_RELATION_TYPE) || criterionsCollection.EvaluateTrace.Flags != PdmConfiguratorResult.ContextNotFound)
              traceLog.Items[key6] = criterionsCollection.EvaluateTrace.Clone() as TraceEntry;
          }
          if (configuratorResult == PdmConfiguratorResult.False)
            dataRowList.Add(row);
          else if (configuratorResult >= PdmConfiguratorResult.True)
            dictionary[index] = (short) configuratorResult;
        }
      }
    }
    bool result = true;
    if (parameters.Tags[(object) "cad005fb-306c-11d8-b4e9-00304f19f545"] != null && !bool.TryParse(parameters.Tags[(object) "cad005fb-306c-11d8-b4e9-00304f19f545"].ToString(), out result))
      result = true;
    if (result)
    {
      int statusesColumnIndex = ElementStatusesPluginDescription.GetStatusesColumnIndex(ref table);
      if (statusesColumnIndex >= 0 && ServerServices.GetService(typeof (IElementStatusesService)) is IElementStatusesService service)
      {
        for (int index = 0; index < table.Rows.Count; ++index)
        {
          if (dictionary.ContainsKey(index))
          {
            DataRow row = table.Rows[index];
            short num3 = dictionary[index];
            service.SetElementStatuses16("cad005f6-306c-11d8-b4e9-00304f19f545", row[statusesColumnIndex] as byte[], num3);
          }
        }
      }
    }
    if (table.Rows.Count <= 0 || dataRowList.Count <= 0)
      return;
    for (int index = 0; index < dataRowList.Count; ++index)
      table.Rows.Remove(dataRowList[index]);
    table.AcceptChanges();
  }

  private string CreateStringFromAttributeValues(object values)
  {
    return string.Join(string.Empty, (object[]) values);
  }

  internal class ServerPDMConfiguratorPluginClass : LongLifeObject, IPdmConfiguratorServerPlugin
  {
    public Guid PluginGuid => ServerPDMConfiguratorPlugin._pluginGuid;
  }
}
