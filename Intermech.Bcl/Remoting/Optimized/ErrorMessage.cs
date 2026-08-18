
// Type: Intermech.Remoting.Optimized.ErrorMessage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Messaging;


namespace Intermech.Remoting.Optimized
{
    internal class ErrorMessage : IMethodCallMessage, IMethodMessage, IMessage
    {
      private string m_URI = "Exception";
      private string m_MethodName = "Unknown";
      private string m_TypeName = "Unknown";
      private object m_MethodSignature;
      private int m_ArgCount;
      private string m_ArgName = "Unknown";

      public IDictionary Properties => (IDictionary) null;

      public string Uri => this.m_URI;

      public string MethodName => this.m_MethodName;

      public string TypeName => this.m_TypeName;

      public object MethodSignature => this.m_MethodSignature;

      public MethodBase MethodBase => (MethodBase) null;

      public int ArgCount => this.m_ArgCount;

      public object[] Args => (object[]) null;

      public bool HasVarArgs => false;

      public LogicalCallContext LogicalCallContext => (LogicalCallContext) null;

      public int InArgCount => this.m_ArgCount;

      public object[] InArgs => (object[]) null;

      public string GetArgName(int index) => this.m_ArgName;

      public object GetArg(int argNum) => (object) null;

      public string GetInArgName(int index) => (string) null;

      public object GetInArg(int argNum) => (object) null;
    }
}
