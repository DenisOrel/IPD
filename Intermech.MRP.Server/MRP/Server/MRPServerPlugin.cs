// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPServerPlugin
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.MRP;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.MRP.Server.Compositions.Filtration;
using Intermech.MRP2;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

#nullable disable
namespace Intermech.MRP.Server;

internal class MRPServerPlugin : MarshalByRefObject, IPackage, IMRPServerPlugin
{
  private static Guid _pluginGuid = new Guid("{722B3498-1EEB-4272-9654-FA6BD98C287C}");
  internal IEventLogHelper _eventLogHelper;
  private LazyService<IElementStatusesService> _elementStatusesService = new LazyService<IElementStatusesService>();
  private LazyService<IPluginStatusesTable> _pluginStatusesTable = new LazyService<IPluginStatusesTable>();

  public Guid PluginGuid => MRPServerPlugin._pluginGuid;

  public string Name => LocalizationHolder.rm.GetString("MRP.Server_3");

  public void Load(IServiceProvider serviceProvider)
  {
    this._eventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    this.LoadPluginResources(serviceProvider);
    IDBObjectCreator creatorInstance = (IDBObjectCreator) new DBMRPObjectsCreator();
    ICreatorContainer service1 = serviceProvider.GetService(typeof (IDBObjectService)) as ICreatorContainer;
    service1.AddCreator((object) new Guid("cadd92e9-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance, true);
    service1.AddCreator((object) new Guid("cadd9a5d-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance, true);
    ICustomServices service2 = serviceProvider.GetService(typeof (ICustomServices)) as ICustomServices;
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("MRPServer.Load");
    try
    {
      ServerServices.AddService(typeof (IMRPServerPlugin), (object) this);
      service2.AddService(typeof (IMRPServerPlugin), (object) this);
      IMRPSettings serviceInstance1 = (IMRPSettings) new ServerMRPSettings(sessionTemporaryClone);
      ServerServices.AddService(typeof (IMRPSettings), (object) serviceInstance1);
      service2.AddService(typeof (IMRPSettings), (object) serviceInstance1);
      MRPCompositionsBrowser serviceInstance2 = new MRPCompositionsBrowser();
      ServerServices.AddService(typeof (IMRPCompositionsBrowser), (object) serviceInstance2);
      ServerServices.AddService(typeof (IMRPCompositionsServerBrowser), (object) serviceInstance2);
      service2.AddService(typeof (IMRPCompositionsBrowser), (object) serviceInstance2);
      CompositionsAutosortRule.OnGetVisibleRelations += new CompositionsGetVisibleRelationsEventHandler(ServerAutosortRuleEvents.CompositionsGetVisibleRelationsEventHandler);
      CompositionsAutosortRule.OnGetVisibleRelationsGuids += new CompositionsGetVisibleRelationsGuidEventHandler(ServerAutosortRuleEvents.CompositionsGetVisibleRelationsGuidEventHandler);
      MRP2ServerService serviceInstance3 = new MRP2ServerService();
      ServerServices.AddService(typeof (IMRP2ServerService), (object) serviceInstance3);
      service2.AddService(typeof (IMRP2ServerService), (object) serviceInstance3);
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(MRP2Consts.attrIdChangeBase);
      if (attributeType != null)
      {
        if (attributeType.MultiValueMode == MultiValueModes.SingleValue)
          this._eventLogHelper.AddAttributeWriteHandler((object) MRP2Consts.attrIdChangeBase, new WriteAttributeValueHandler(serviceInstance3.WriteAttributeValueHandler));
        if (attributeType.MultiValueMode == MultiValueModes.MultiValues)
          (this._eventLogHelper as EventLogHelper).AddAttributeValuesWriteHandler((object) MRP2Consts.attrIdChangeBase, new WriteAttributeValuesHandler(serviceInstance3.WriteAttributeValuesHandler));
        this._eventLogHelper.AfterCreateRelationExEvent += new CreateRelationExHandler(serviceInstance3.AfterCreateRelationEvent);
      }
      this.LoadCustomStatuses();
    }
    finally
    {
      sessionTemporaryClone?.Logout("MRPServer.Load");
    }
  }

  public void Unload() => this.UnLoadCustomStatuses();

  private void LoadCustomStatuses()
  {
    this._elementStatusesService.Value.RegisterServerPlugin(new ElementStatusesPluginDescription(16 /*0x10*/, "cad8491c-5d67-476f-b87a-f2c6dcd807a2", (string) null, "Производственные ведомости", "Статусы копий в составе ПВ")
    {
      IsFlags = true
    });
    ImageConverter imageConverter = new ImageConverter();
    this._pluginStatusesTable.Value.AddStatus("cad8491c-5d67-476f-b87a-f2c6dcd807a2", 1, MRP2Status.Copied.GetDescription<MRP2Status>(), (byte[]) imageConverter.ConvertTo((object) MRP2Resources.MRP2Copied, typeof (byte[])));
    this._pluginStatusesTable.Value.AddStatus("cad8491c-5d67-476f-b87a-f2c6dcd807a2", 2, MRP2Status.Added.GetDescription<MRP2Status>(), (byte[]) imageConverter.ConvertTo((object) MRP2Resources.MRP2Added, typeof (byte[])));
    this._pluginStatusesTable.Value.AddStatus("cad8491c-5d67-476f-b87a-f2c6dcd807a2", 4, MRP2Status.Deleted.GetDescription<MRP2Status>(), (byte[]) imageConverter.ConvertTo((object) MRP2Resources.MRP2Deleted, typeof (byte[])));
    this._eventLogHelper.GetRecordsListEvent += new GetRecordsListHandler(this.EventLogHelper_GetRecordsListEvent);
    this._eventLogHelper.BeforeRecordsSelectEvent += new BeforeRecordsSelectHandler(this.EventLogHelper_BeforeRecordsSelect);
  }

  private void UnLoadCustomStatuses()
  {
    this._pluginStatusesTable.Value.RemoveStatuses("cad8491c-5d67-476f-b87a-f2c6dcd807a2");
    this._eventLogHelper.GetRecordsListEvent -= new GetRecordsListHandler(this.EventLogHelper_GetRecordsListEvent);
    this._eventLogHelper.BeforeRecordsSelectEvent -= new BeforeRecordsSelectHandler(this.EventLogHelper_BeforeRecordsSelect);
  }

  private void EventLogHelper_BeforeRecordsSelect(object sender, BeforeRecordsSelectEventArgs args)
  {
    if (args == null || args.Session == null)
      return;
    FiltrationCompositionService.PrepareFilterByDate(sender, args);
    DBRelationCollection relationCollection = sender as DBRelationCollection;
    if (MRP2Consts.reltypeIdProductComposition <= 0 || MRP2Consts.reltypeIdDocumentComposition <= 0 || relationCollection == null || relationCollection.RelationTypeID != MRP2Consts.reltypeIdProductComposition && relationCollection.RelationTypeID != MRP2Consts.reltypeIdDocumentComposition || relationCollection.FunctionID == SelectFunction.EntersIn || relationCollection.FunctionID == SelectFunction.EntersInVersion)
      return;
    ColumnDescriptor[] array = new List<ColumnDescriptor>(1)
    {
      new ColumnDescriptor((object) MRP2Consts.attrIdDeleteTag, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) MRP2Consts.attrIdChangeTag, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) MRP2Consts.attrIdChangeCode, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
    }.ToArray();
    List<int> AddedColumnsPos = new List<int>(0);
    args.OldParameters.AddColumnDescriptors(array, AddedColumnsPos);
    if (args.OldParameters.Tags[(object) "9854400D-D3EB-4A82-ADD3-00163FB748FC"] != null)
    {
      bool result;
      if (!bool.TryParse(args.OldParameters.Tags[(object) "9854400D-D3EB-4A82-ADD3-00163FB748FC"].ToString(), out result))
        result = false;
      if (result)
      {
        int newSize = args.OldParameters.Conditions == null ? 1 : args.OldParameters.Conditions.Length + 1;
        int index = newSize - 1;
        Array.Resize<ConditionStructure>(ref args.OldParameters.Conditions, newSize);
        ConditionStructure conditionStructure = new ConditionStructure(MRP2Consts.attrIdDeleteTag, RelationalOperators.Equal, (object) 0, LogicalOperators.NONE, 0, true);
        args.OldParameters.Conditions[index] = conditionStructure;
      }
    }
    args.NewParameters = new DBRecordSetParams?(args.OldParameters);
  }

  private void EventLogHelper_GetRecordsListEvent(
    DataTable table,
    object sender,
    DBRecordSetParams parameters,
    IUserSession session)
  {
    if (table == null || parameters.ColumnsInfo == null || session == null || table.Rows.Count == 0)
      return;
    FiltrationCompositionService.FilterByDate(table, parameters);
    DBRelationCollection relationCollection = sender as DBRelationCollection;
    if (MRP2Consts.reltypeIdProductComposition <= 0 || MRP2Consts.reltypeIdDocumentComposition <= 0 || relationCollection == null || relationCollection.RelationTypeID != MRP2Consts.reltypeIdProductComposition && relationCollection.RelationTypeID != MRP2Consts.reltypeIdDocumentComposition)
      return;
    int statusesColumnIndex = ElementStatusesPluginDescription.GetStatusesColumnIndex(ref table);
    if (statusesColumnIndex < 0)
      return;
    int columnIndex = DBRecordSet.AttributeColumnIndex(parameters, (object) MRP2Consts.attrIdDeleteTag, AttributeSourceTypes.Relation, table);
    if (columnIndex < 0 || !(ServerServices.GetService(typeof (IElementStatusesService)) is IElementStatusesService service))
      return;
    table.BeginLoadData();
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      if (DataSetProcessor.GetBooleanValue(row[columnIndex], false))
        service.SetElementStatuses16("cad8491c-5d67-476f-b87a-f2c6dcd807a2", row[statusesColumnIndex] as byte[], Convert.ToInt16((object) MRP2Status.Deleted));
    }
    table.EndLoadData();
    table.AcceptChanges();
  }

  internal static byte[] LoadResource(string ResourceName)
  {
    Stream stream = (Stream) null;
    try
    {
      stream = typeof (MRPServerPlugin).Assembly.GetManifestResourceStream(ResourceName);
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

  private void LoadPluginResources(IServiceProvider serviceProvider)
  {
  }
}
