
// Type: Intermech.HyperlinkHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;


namespace Intermech;

public class HyperlinkHandler
{
  private static Regex _urlRegex = new Regex("(?<p>i[mp]s)://(?<mod>[^/]*)/?(?<id>[^/]*)/?(?<cmd>[^/]*)?/?", RegexOptions.Compiled);
  private static Dictionary<string, Dictionary<string, HyperlinkEventHandler>> _customModes = new Dictionary<string, Dictionary<string, HyperlinkEventHandler>>();
  private static List<string> _designationModes = new List<string>()
  {
    "designatio",
    "d",
    "adesignatio",
    "a",
    "caption",
    "designation"
  };
  private static Dictionary<string, string> _commandNames = new Dictionary<string, string>()
  {
    {
      "",
      "OpenInNewWindow"
    },
    {
      "view",
      "ViewDocument"
    },
    {
      "edit",
      "EditDocument"
    },
    {
      "card",
      "ParametersCard"
    },
    {
      "versions",
      "ListVersions"
    },
    {
      "sign",
      "ParametersCard"
    },
    {
      "visual",
      "PDM.RelationVisualizer"
    }
  };
  private static List<string> _suspendedUrls = new List<string>();
  private static int _objTypeDoc = -1;
  private static int _objTypeArt = -1;
  public static readonly Guid AttrSearchIDGuid = new Guid("cad0132b-306c-11d8-b4e9-00304f19f545");
  public static int AttrSearchID = -1;
  private static int _attrDesignationID = -1;
  private static Lazy<IMainFormUpdate> _mainFormService;
  private static IStartupService _startupService;
  private static string _sessionIdentifier = "";
  private static Mutex _mutex = (Mutex) null;
  private static bool? _isFirstInstance = new bool?();

  private static void RegisterProtocol(string name)
  {
    try
    {
      RegistryKey subKey = Registry.ClassesRoot.CreateSubKey(name);
      subKey.SetValue("", (object) "IPS URL Handler");
      subKey.SetValue("URL Protocol", (object) "");
      subKey.CreateSubKey("shell\\open\\command").SetValue("", (object) $"\"{Application.ExecutablePath}\" \"%1\"");
    }
    catch
    {
    }
  }

  public static void RegisterIMSProtocol() => HyperlinkHandler.RegisterProtocol("ims");

  public static void RegisterIPSProtocol() => HyperlinkHandler.RegisterProtocol("ips");

  /// <param name="Url"></param>
  /// <returns>True, если Url обработан/передан другому экземпляру программы</returns>
  protected static bool ProcessUrl(string Url)
  {
    if (!HyperlinkHandler.IsSupportedUrl(Url))
      return false;
    if (HyperlinkHandler.IsFirstInstance)
      return HyperlinkHandler.OpenUrl(Url);
    HyperlinkHandler.PassArgumentsToFirstInstance(new string[1]
    {
      Url
    });
    return true;
  }

  public static bool IsSupportedUrl(string Url) => HyperlinkHandler._urlRegex.Match(Url).Success;

  public static IExceptionHandlerService ExceptionService { get; set; }

  /// <summary>
  /// Возвращает true, если Url обработан, и false, если его надо обработать позже/в другом месте
  /// </summary>
  public static bool OpenUrl(string Url) => HyperlinkHandler.OpenUrl(Url, false);

  /// <summary>
  /// Возвращает true, если Url обработан, и false, если его надо обработать позже/в другом месте
  /// </summary>
  public static bool OpenUrl(string Url, bool throwExceptions)
  {
    if (Url == null)
      return true;
    try
    {
      Match match = HyperlinkHandler._urlRegex.Match(Url);
      if (match.Success)
      {
        if (!HyperlinkHandler.MainFormCreated)
        {
          HyperlinkHandler._suspendedUrls.Add(Url);
          return false;
        }
        long objectID = 0;
        string str = match.Groups["mod"].Value;
        string sid = match.Groups["id"].Value;
        sid = HttpUtility.UrlDecode(sid);
        string cmd = match.Groups["cmd"].Value;
        Dictionary<string, HyperlinkEventHandler> dictionary = (Dictionary<string, HyperlinkEventHandler>) null;
        if (HyperlinkHandler._customModes.TryGetValue(str, out dictionary))
        {
          HyperlinkEventHandler handler = (HyperlinkEventHandler) null;
          if (dictionary.TryGetValue(cmd, out handler))
          {
            if (HyperlinkHandler.MainForm.InvokeRequired)
              HyperlinkHandler.MainForm.Invoke((Delegate) (() => handler(cmd, sid)));
            else
              handler(cmd, sid);
            return true;
          }
        }
        try
        {
          if (HyperlinkHandler._designationModes.Contains(str))
          {
            int attrID = HyperlinkHandler.AttrDesignationID;
            if (str == "caption")
              attrID = -50;
            objectID = HyperlinkHandler.AttributeToObjectID(attrID, (object) sid);
            if (objectID == 0L)
              throw new SimpleMessageException(string.Format("Не удалось найти объект по обозначению \"{0}\"", (object) str, (object) sid));
          }
          else
          {
            switch (match.Groups["p"].Value)
            {
              case "ips":
                if (str == "object")
                {
                  objectID = (long) Convert.ToInt32(sid);
                  break;
                }
                break;
              case "ims":
                int int32 = Convert.ToInt32(sid);
                objectID = HyperlinkHandler.SearchIDToObjectID(str, int32);
                if (objectID == 0L)
                  throw new SimpleMessageException($"Не удалось найти объект, соответствующий объекту Search ({str},ID={match.Groups["id"].Value})");
                break;
            }
          }
        }
        catch (Exception ex)
        {
          if (ex is SimpleMessageException)
            throw;
        }
        if (objectID != 0L)
        {
          string command = "";
          if (!HyperlinkHandler._commandNames.TryGetValue(cmd, out command))
            command = HyperlinkHandler._commandNames[""];
          if (HyperlinkHandler.MainForm.InvokeRequired)
            HyperlinkHandler.MainForm.Invoke((Delegate) (() => HyperlinkHandler.PerformCommand(objectID, command)));
          else
            HyperlinkHandler.PerformCommand(objectID, command);
          return true;
        }
      }
      return false;
    }
    catch (Exception ex)
    {
      if (throwExceptions)
        throw;
      if (HyperlinkHandler.ExceptionService != null)
        HyperlinkHandler.ExceptionService.ShowException(ex);
      return false;
    }
  }

  protected static void PerformCommand(long objID, string commandName)
  {
    if (HyperlinkHandler.MainFormCreated)
    {
      HyperlinkHandler.MainForm.Restore();
      HyperlinkHandler.MainForm.Activate();
      HyperlinkHandler.MainForm.BringToFront();
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(Math.Abs(objID), false);
      if (objectActualCopy == null)
      {
        int num = (int) MessageBox.Show($"Невозможно открыть ссылку: объект с идентификатором \"{objID}\" не найден!", (string) null, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      if (objectActualCopy.ObjectID != objID)
        objID = objectActualCopy.ObjectID;
    }
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(objID);
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    ServiceContainer viewServices2 = viewServices1;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2);
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, (System.IServiceProvider) viewServices1);
  }

  protected static long SearchIDToObjectID(string kind, int id)
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    int objectType1 = -1;
    switch (kind)
    {
      case "doc":
        if (HyperlinkHandler._objTypeDoc == -1)
        {
          IDBObjectTypeInfo objectType2 = service.GetObjectType(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"), false);
          if (objectType2 != null)
            HyperlinkHandler._objTypeDoc = objectType2.ObjectType;
        }
        objectType1 = HyperlinkHandler._objTypeDoc;
        break;
      case "art":
        if (HyperlinkHandler._objTypeArt == -1)
        {
          IDBObjectTypeInfo objectType3 = service.GetObjectType(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"), false);
          if (objectType3 != null)
            HyperlinkHandler._objTypeArt = objectType3.ObjectType;
        }
        objectType1 = HyperlinkHandler._objTypeArt;
        break;
    }
    if (HyperlinkHandler.AttrSearchID == -1)
    {
      IDBAttributeTypeInfo attributeType = service.GetAttributeType(HyperlinkHandler.AttrSearchIDGuid, false);
      if (attributeType != null)
        HyperlinkHandler.AttrSearchID = attributeType.AttributeID;
    }
    if (HyperlinkHandler.AttrSearchID != -1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectType1);
        if (objectCollection != null)
        {
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(HyperlinkHandler.AttrSearchID, RelationalOperators.Equal, (object) id, (object) null, LogicalOperators.AND, 0, true, AttributeSourceTypes.Auto, ColumnContents.ID)
          }, new object[1]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID
          }, 0L, (object) null, 1);
          IEnumerator enumerator = objectCollection.Select(paramSet).Rows.GetEnumerator();
          try
          {
            if (enumerator.MoveNext())
              return Convert.ToInt64(((DataRow) enumerator.Current)[0]);
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
      }
    }
    return 0;
  }

  protected static int AttrDesignationID
  {
    get
    {
      if (HyperlinkHandler._attrDesignationID == -1)
        HyperlinkHandler._attrDesignationID = MetaDataHelper.GetAttributeID((object) new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
      return HyperlinkHandler._attrDesignationID;
    }
  }

  protected static long AttributeToObjectID(int attrID, object value)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (HyperlinkHandler.AttrDesignationID != -1)
      {
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(attrID, RelationalOperators.Equal, value, LogicalOperators.AND, 0, false)
        }, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }, 0L, (object) null, 1);
        IEnumerator enumerator = sessionKeeper.Session.ObjectsSelect(-1, dbRecordSetParams).Rows.GetEnumerator();
        try
        {
          if (enumerator.MoveNext())
            return Convert.ToInt64(((DataRow) enumerator.Current)[0]);
        }
        finally
        {
          if (enumerator is IDisposable disposable)
            disposable.Dispose();
        }
      }
    }
    return 0;
  }

  public static Lazy<IMainFormUpdate> MainFormService
  {
    get => HyperlinkHandler._mainFormService;
    set => HyperlinkHandler._mainFormService = value;
  }

  private static Form MainForm => HyperlinkHandler.MainFormService.Value.MainForm;

  private static bool MainFormCreated
  {
    get
    {
      return HyperlinkHandler.StartupService != null && HyperlinkHandler.StartupService.IsStartupCompleted;
    }
  }

  public static IStartupService StartupService
  {
    get => HyperlinkHandler._startupService;
    set
    {
      if (HyperlinkHandler._startupService == value)
        return;
      if (HyperlinkHandler._startupService != null)
        HyperlinkHandler._startupService.StartupComplete -= new EventHandler(HyperlinkHandler.OnStartupComplete);
      HyperlinkHandler._startupService = value;
      if (HyperlinkHandler._startupService == null)
        return;
      HyperlinkHandler._startupService.StartupComplete += new EventHandler(HyperlinkHandler.OnStartupComplete);
      if (!HyperlinkHandler._startupService.IsStartupCompleted)
        return;
      HyperlinkHandler.OpenSuspendedUrls();
    }
  }

  private static void OnStartupComplete(object sender, EventArgs e)
  {
    HyperlinkHandler.OpenSuspendedUrls();
  }

  private static void OpenSuspendedUrls()
  {
    while (HyperlinkHandler._suspendedUrls.Count > 0)
    {
      HyperlinkHandler.OpenUrl(HyperlinkHandler._suspendedUrls[0]);
      HyperlinkHandler._suspendedUrls.RemoveAt(0);
    }
  }

  /// <summary>
  /// </summary>
  /// <param name="args"></param>
  /// <returns>True, если старт приложения не требуется</returns>
  public static bool Process(IList<string> args)
  {
    bool flag = false;
    foreach (string Url in (IEnumerable<string>) args)
    {
      switch (Url.Trim('-', '/', '\\'))
      {
        case "regims":
          HyperlinkHandler.RegisterIMSProtocol();
          flag = true;
          continue;
        case "regips":
          HyperlinkHandler.RegisterIPSProtocol();
          flag = true;
          continue;
        case "unregips":
        case "unregims":
          flag = true;
          continue;
        default:
          if (HyperlinkHandler.ProcessUrl(Url))
          {
            flag = true;
            continue;
          }
          continue;
      }
    }
    if (!flag)
      HyperlinkHandler.InitFirstInstance();
    return flag;
  }

  private static string SessionIdentifier
  {
    get
    {
      if (HyperlinkHandler._sessionIdentifier == "")
        HyperlinkHandler._sessionIdentifier = "IPS.FirstInstance." + System.Diagnostics.Process.GetCurrentProcess().SessionId.ToString();
      return HyperlinkHandler._sessionIdentifier;
    }
  }

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  private static extern bool WaitNamedPipe(string name, int timeout);

  protected static bool NamedPipeExists(string pipeName)
  {
    try
    {
      int timeout = 1;
      if (!HyperlinkHandler.WaitNamedPipe(Path.GetFullPath($"\\\\.\\pipe\\{pipeName}"), timeout))
      {
        switch (Marshal.GetLastWin32Error())
        {
          case 0:
            return false;
          case 2:
            return false;
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  protected static bool IsFirstInstance
  {
    get
    {
      if (!HyperlinkHandler._isFirstInstance.HasValue)
        HyperlinkHandler._isFirstInstance = new bool?(!HyperlinkHandler.NamedPipeExists(HyperlinkHandler.SessionIdentifier));
      HyperlinkHandler._mutex = new Mutex(true, HyperlinkHandler.SessionIdentifier);
      return HyperlinkHandler._isFirstInstance.Value;
    }
  }

  protected static void InitFirstInstance()
  {
    if (!HyperlinkHandler.IsFirstInstance)
      return;
    HyperlinkHandler.ListenForArgumentsFromSuccessiveInstances();
  }

  /// <summary>
  /// Passes the given arguments to the first running instance of the application.
  /// </summary>
  /// <param name="arguments">The arguments to pass.</param>
  /// <returns>Return true if the operation succeded, false otherwise.</returns>
  protected static bool PassArgumentsToFirstInstance(string[] arguments)
  {
    if (HyperlinkHandler.IsFirstInstance)
      throw new InvalidOperationException("This is the first instance.");
    try
    {
      using (NamedPipeClientStream pipeClientStream = new NamedPipeClientStream(HyperlinkHandler.SessionIdentifier))
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) pipeClientStream))
        {
          pipeClientStream.Connect(200);
          foreach (string str in arguments)
            streamWriter.WriteLine(str);
        }
      }
      return true;
    }
    catch (TimeoutException ex)
    {
    }
    catch (IOException ex)
    {
    }
    return false;
  }

  /// <summary>
  /// Listens for arguments being passed from successive instances of the applicaiton.
  /// </summary>
  private static void ListenForArgumentsFromSuccessiveInstances()
  {
    if (!HyperlinkHandler.IsFirstInstance)
      throw new InvalidOperationException("This is not the first instance.");
    ThreadPool.QueueUserWorkItem(new WaitCallback(HyperlinkHandler.ListenForArguments));
  }

  /// <summary>Listens for arguments on a named pipe.</summary>
  /// <param name="state">State object required by WaitCallback delegate.</param>
  private static void ListenForArguments(object state)
  {
    while (true)
    {
      try
      {
        using (NamedPipeServerStream pipeServerStream = new NamedPipeServerStream(HyperlinkHandler.SessionIdentifier))
        {
          using (StreamReader streamReader = new StreamReader((Stream) pipeServerStream))
          {
            pipeServerStream.WaitForConnection();
            while (pipeServerStream.IsConnected)
              new HyperlinkHandler.OpenUrlDelegate(HyperlinkHandler.OpenUrl).BeginInvoke(streamReader.ReadLine(), (AsyncCallback) null, (object) null);
          }
        }
      }
      catch (IOException ex)
      {
      }
    }
  }

  public static void RegisterCommand(string mode, string command, HyperlinkEventHandler handler)
  {
    Dictionary<string, HyperlinkEventHandler> dictionary = (Dictionary<string, HyperlinkEventHandler>) null;
    if (!HyperlinkHandler._customModes.TryGetValue(mode, out dictionary))
    {
      dictionary = new Dictionary<string, HyperlinkEventHandler>();
      HyperlinkHandler._customModes.Add(mode, dictionary);
    }
    dictionary[command] = handler;
  }

  public delegate bool OpenUrlDelegate(string Url);
}
