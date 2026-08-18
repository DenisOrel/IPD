// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.RequestProcessTaskHelper
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.ExternalSystemIntegration.Server.Helpers;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

public class RequestProcessTaskHelper
{
  private IUserSession _Session;

  private RequestProcessTaskHelper()
  {
  }

  public RequestProcessTaskHelper(IUserSession ASession)
    : this()
  {
    this._Session = ASession;
  }

  public bool ProcessTask()
  {
    this.ProcessRequestObjs();
    return true;
  }

  private void ProcessRequestObjs()
  {
    foreach (long requestObjId in this.GetRequestObjIDs())
    {
      try
      {
        this.RequestProcessing(requestObjId);
      }
      catch (Exception ex)
      {
        HelperMethods.WriteErrorMsg(this._Session.SessionGUID, string.Format(LocalizationHolder.rm.GetString("ExtInt_5"), (object) requestObjId, (object) ex.Message));
      }
    }
  }

  private void RequestProcessing(long requestID)
  {
    IRequestObject requestObject = this._Session.GetObject(requestID, true) as IRequestObject;
    try
    {
      requestObject = requestObject.CheckOut() as IRequestObject;
      HelperMethods.SetObjectStatus(this._Session.SessionGUID, requestObject.ObjectID, StatusEnum.Work);
      string requestSchemeData = this.GetRequestSchemeData(requestObject);
      string outputFiles = (ServerServices.GetService(typeof (ICommonSettingsHolder)) as ICommonSettingsHolder).OutputFiles;
      if (requestSchemeData.Length > 0 && Directory.Exists(outputFiles))
      {
        XmlDocument requestXmlDocument = new RequestXmlDocCreator(this._Session, requestObject.ObjectID, requestSchemeData).GetRequestXmlDocument();
        string requestId = requestObject.RequestID;
        string empty = string.Empty;
        string str = RequestXmlDocCreator.ReplaceBrackets(this._Session.SessionGUID, (this._Session.GetObject(requestObject.ConfigElementLink, true) as IRequestConfigObject).FileName, requestObject.ObjectID);
        using (TextWriter writer = (TextWriter) new StreamWriter(str.Length <= 0 ? Path.Combine(outputFiles, requestId + ".xml") : Path.Combine(outputFiles, str + ".xml"), false, (Encoding) new UTF8Encoding(false)))
          requestXmlDocument.Save(writer);
      }
      else
      {
        if (requestSchemeData.Length == 0)
          throw new Exception(LocalizationHolder.rm.GetString("ExtInt_6"));
        if (!Directory.Exists(outputFiles))
          throw new Exception(LocalizationHolder.rm.GetString("ExtInt_7"));
      }
      HelperMethods.SetObjectStatus(this._Session.SessionGUID, requestObject.ObjectID, StatusEnum.RequestCreate);
    }
    catch (Exception ex)
    {
      HelperMethods.SetObjectStatus(this._Session.SessionGUID, requestObject.ObjectID, StatusEnum.Error);
      HelperMethods.AddErrorText(this._Session.SessionGUID, requestObject.ObjectID, ex.Message + Environment.NewLine + ex.StackTrace);
    }
    finally
    {
      requestObject.CheckIn();
    }
  }

  private string GetRequestSchemeData(IRequestObject requestObject)
  {
    string requestSchemeData = string.Empty;
    if (requestObject != null)
    {
      long configElementLink = requestObject.ConfigElementLink;
      if (configElementLink != 0L && this._Session.GetObject(configElementLink, false) is IRequestConfigObject requestConfigObject)
      {
        long schemeTransfLink = requestConfigObject.SchemeTransfLink;
        if (schemeTransfLink != 0L && this._Session.GetObject(schemeTransfLink, false) is IRequestSchemeObject requestSchemeObject)
          requestSchemeData = requestSchemeObject.SchemeData;
      }
    }
    return requestSchemeData;
  }

  private long[] GetRequestObjIDs()
  {
    long[] numArray = new long[0];
    return this._Session.GetObjectCollection(Const.RequestObjTypeID).Select(new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(Const.StatusAttrTypeID, RelationalOperators.Equal, (object) 0L, LogicalOperators.AND, 0, false),
      new ConditionStructure(-6, RelationalOperators.Equal, (object) 0, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }, (object[]) null, (SortOrders[]) null, 0L, (object) null, -1, true, "MyObjects")).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => row.Field<long>(0))).ToArray<long>();
  }
}
