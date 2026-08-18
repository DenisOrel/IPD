
// Type: Intermech.Interfaces.Mapi
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Interfaces
{
    public class Mapi
    {
      /// <summary>
      /// показывает диалог для идентификации,  если  это  потребуется
      /// (парметр flg в MAPILogon)
      /// </summary>
      private const int MapiLogonUI = 1;
      private const int MapiPasswordUI = 131072 /*0x020000*/;
      /// <summary>
      ///  если флаг установлен,  функция  попытается  создать  новую сессию перед тем, как использовать существующую
      /// (парметр flg в MAPILogon)
      /// </summary>
      private const int MapiNewSession = 2;
      /// <summary>
      /// если флаг установлен, то функция пытается загрузить все сообщения,  перед  ее   завершением
      /// (парметр flg в MAPILogon)
      /// </summary>
      private const int MapiForceDownload = 4096 /*0x1000*/;
      private const int MapiExtendedUI = 32 /*0x20*/;
      private IntPtr session = IntPtr.Zero;
      private IntPtr winhandle = IntPtr.Zero;
      /// <summary>адрес нашей техподдержки</summary>
      private string IPS_SUPPORT_ADRRESS = "helpdesk@intermech.ru";
      /// <summary>Основной получатель сообщения</summary>
      private const int MapiORIG = 0;
      /// <summary>Первый получатель сообщения</summary>
      private const int MapiTO = 1;
      /// <summary>Получатель копии сообщения</summary>
      private const int MapiCC = 2;
      /// <summary>Получатель копии blind copy</summary>
      private const int MapiBCC = 3;
      /// <summary>информация об отправителе</summary>
      private MapiRecipDesc origin = new MapiRecipDesc();
      /// <summary>массив получателей</summary>
      private ArrayList recpts = new ArrayList();
      /// <summary>массив прикреплённых файлов</summary>
      private ArrayList attachs = new ArrayList();
      private const int MapiUnreadOnly = 32 /*0x20*/;
      private const int MapiGuaranteeFiFo = 256 /*0x0100*/;
      private const int MapiLongMsgID = 16384 /*0x4000*/;
      private StringBuilder lastMsgID = new StringBuilder(600);
      private string findseed;
      private const int MapiPeek = 128 /*0x80*/;
      private const int MapiSuprAttach = 2048 /*0x0800*/;
      private const int MapiEnvOnly = 64 /*0x40*/;
      private const int MapiBodyAsFile = 512 /*0x0200*/;
      private const int MapiUnread = 1;
      private const int MapiReceiptReq = 2;
      private const int MapiSent = 4;
      private MapiMessage lastMsg;
      private int error;
      private readonly string[] errors = new string[27]
      {
        "OK [0]",
        "User abort [1]",
        "General MAPI failure [2]",
        "MAPI login failure [3]",
        "Disk full [4]",
        "Insufficient memory [5]",
        "Access denied [6]",
        "-unknown- [7]",
        "Too many sessions [8]",
        "Too many files were specified [9]",
        "Too many recipients were specified [10]",
        "A specified attachment was not found [11]",
        "Attachment open failure [12]",
        "Attachment write failure [13]",
        "Unknown recipient [14]",
        "Bad recipient type [15]",
        "No messages [16]",
        "Invalid message [17]",
        "Text too large [18]",
        "Invalid session [19]",
        "Type not supported [20]",
        "A recipient was specified ambiguously [21]",
        "Message in use [22]",
        "Network failure [23]",
        "Invalid edit fields [24]",
        "Invalid recipients [25]",
        "Not supported [26]"
      };

      /// <summary>начать новую сессию</summary>
      /// <param name="hwnd"></param>
      /// <returns>true - если всё прошло успешно, иначе false. описание ошибки - см. св-во  Error</returns>
      public bool Logon(IntPtr hwnd)
      {
        this.winhandle = hwnd;
        this.error = Mapi.MAPILogon(hwnd, (string) null, (string) null, 0, 0, ref this.session);
        if (this.error != 0)
          this.error = Mapi.MAPILogon(hwnd, (string) null, (string) null, 1, 0, ref this.session);
        return this.error == 0;
      }

      public void Reset()
      {
        this.findseed = (string) null;
        this.origin = new MapiRecipDesc();
        this.recpts.Clear();
        this.attachs.Clear();
        this.lastMsg = (MapiMessage) null;
      }

      public void Logoff()
      {
        if (!(this.session != IntPtr.Zero))
          return;
        this.error = Mapi.MAPILogoff(this.session, this.winhandle, 0, 0);
        this.session = IntPtr.Zero;
      }

      /// <summary>
      /// Начать новую сессию, загрузить адресную книгу и список сообщений
      /// </summary>
      /// <param name="hwnd">handle диалогового окна созданного MAPILogon или 0. Если  0 , то  этот параметр игнорируется</param>
      /// <param name="prf">указатель на строку, содержащую путь к файлу настроек</param>
      /// <param name="pw">указатель на строку, содержащую пароль для доступа</param>
      /// <param name="flg">флаги опций</param>
      /// <param name="rsv">зарезервировано</param>
      /// <param name="sess">указатель на сессию Simple MAPI</param>
      /// <returns></returns>
      [DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
      private static extern int MAPILogon(
        IntPtr hwnd,
        string prf,
        string pw,
        int flg,
        int rsv,
        ref IntPtr sess);

      /// <summary>Завершает сессию с почтовой системой</summary>
      /// <param name="sess">указатель на сессию, которую нужно завершить</param>
      /// <param name="hwnd">handle диалогового окна созданного MAPILogon или 0</param>
      /// <param name="flg">зарезервировано</param>
      /// <param name="rsv">зарезервировано</param>
      /// <returns></returns>
      [DllImport("MAPI32.DLL")]
      private static extern int MAPILogoff(IntPtr sess, IntPtr hwnd, int flg, int rsv);

      /// <summary>отправить письмо</summary>
      /// <param name="sub">тема</param>
      /// <param name="txt">текст письма</param>
      /// <returns>true - если всё прошло успешно, иначе false. описание ошибки - см. св-во  Error</returns>
      public bool Send(string sub, string txt)
      {
        this.lastMsg = new MapiMessage();
        this.lastMsg.subject = sub;
        this.lastMsg.noteText = txt;
        this.lastMsg.originator = this.AllocOrigin();
        this.lastMsg.recips = this.AllocRecips(out this.lastMsg.recipCount);
        this.lastMsg.files = this.AllocAttachs(out this.lastMsg.fileCount);
        this.error = Mapi.MAPISendMail(this.session, this.winhandle, this.lastMsg, 0, 0);
        this.Dealloc();
        this.Reset();
        return this.error == 0;
      }

      /// <summary>
      /// отправить письмо в нашу техподдрежку, приложить отчёт, располженный по пути reportPath
      /// </summary>
      /// <param name="reportPath">путь к отчёту</param>
      /// <param name="reportTopic">тема письма</param>
      /// <param name="reportText">текст письма</param>
      /// <returns></returns>
      public bool SendReport(string reportPath, string reportTopic, string reportText)
      {
        this.AddRecip(this.IPS_SUPPORT_ADRRESS, (string) null, false);
        this.Attach(reportPath);
        this.Send(reportTopic, reportText);
        return this.error == 0;
      }

      /// <summary>добавить получателя</summary>
      /// <param name="name">имя получателя</param>
      /// <param name="addr">адрес поулчателя</param>
      /// <param name="cc">копия?</param>
      public void AddRecip(string name, string addr, bool cc)
      {
        this.recpts.Add((object) new MapiRecipDesc()
        {
          recipClass = (!cc ? 1 : 2),
          name = name,
          address = addr
        });
      }

      public void SetSender(string sname, string saddr)
      {
        this.origin.name = sname;
        this.origin.address = saddr;
      }

      /// <summary>добавить файл</summary>
      /// <param name="filepath">путь к файлу</param>
      public void Attach(string filepath) => this.attachs.Add((object) filepath);

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      private IntPtr AllocOrigin()
      {
        this.origin.recipClass = 0;
        IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (MapiRecipDesc)));
        Marshal.StructureToPtr<MapiRecipDesc>(this.origin, ptr, false);
        return ptr;
      }

      private IntPtr AllocRecips(out int recipCount)
      {
        recipCount = 0;
        if (this.recpts.Count == 0)
          return IntPtr.Zero;
        int num1 = Marshal.SizeOf(typeof (MapiRecipDesc));
        IntPtr num2 = Marshal.AllocHGlobal(this.recpts.Count * num1);
        int ptr = (int) num2;
        for (int index = 0; index < this.recpts.Count; ++index)
        {
          Marshal.StructureToPtr<MapiRecipDesc>(this.recpts[index] as MapiRecipDesc, (IntPtr) ptr, false);
          ptr += num1;
        }
        recipCount = this.recpts.Count;
        return num2;
      }

      private IntPtr AllocAttachs(out int fileCount)
      {
        fileCount = 0;
        if (this.attachs == null || this.attachs.Count <= 0 || this.attachs.Count > 100)
          return IntPtr.Zero;
        int num1 = Marshal.SizeOf(typeof (MapiFileDesc));
        IntPtr num2 = Marshal.AllocHGlobal(this.attachs.Count * num1);
        MapiFileDesc structure = new MapiFileDesc();
        structure.position = -1;
        int ptr = (int) num2;
        for (int index = 0; index < this.attachs.Count; ++index)
        {
          string attach = this.attachs[index] as string;
          structure.name = Path.GetFileName(attach);
          structure.path = attach;
          Marshal.StructureToPtr<MapiFileDesc>(structure, (IntPtr) ptr, false);
          ptr += num1;
        }
        fileCount = this.attachs.Count;
        return num2;
      }

      private void Dealloc()
      {
        Type type1 = typeof (MapiRecipDesc);
        int num1 = Marshal.SizeOf(type1);
        if (this.lastMsg.originator != IntPtr.Zero)
        {
          Marshal.DestroyStructure(this.lastMsg.originator, type1);
          Marshal.FreeHGlobal(this.lastMsg.originator);
        }
        if (this.lastMsg.recips != IntPtr.Zero)
        {
          int recips = (int) this.lastMsg.recips;
          for (int index = 0; index < this.lastMsg.recipCount; ++index)
          {
            Marshal.DestroyStructure((IntPtr) recips, type1);
            recips += num1;
          }
          Marshal.FreeHGlobal(this.lastMsg.recips);
        }
        if (!(this.lastMsg.files != IntPtr.Zero))
          return;
        Type type2 = typeof (MapiFileDesc);
        int num2 = Marshal.SizeOf(type2);
        int files = (int) this.lastMsg.files;
        for (int index = 0; index < this.lastMsg.fileCount; ++index)
        {
          Marshal.DestroyStructure((IntPtr) files, type2);
          files += num2;
        }
        Marshal.FreeHGlobal(this.lastMsg.files);
      }

      /// <summary>Отправка сообщения</summary>
      /// <param name="sess">handle сессии</param>
      /// <param name="hwnd">handle диалогового окна созданного MAPILogon или 0</param>
      /// <param name="message">указатель на строку, содержащую сообщение для посылки</param>
      /// <param name="flg">флаги опций</param>
      /// <param name="rsv">зарезервировано</param>
      /// <returns></returns>
      [DllImport("MAPI32.DLL")]
      private static extern int MAPISendMail(
        IntPtr sess,
        IntPtr hwnd,
        MapiMessage message,
        int flg,
        int rsv);

      public bool Next(ref MailEnvelop env)
      {
        this.error = Mapi.MAPIFindNext(this.session, this.winhandle, (string) null, this.findseed, 16384 /*0x4000*/, 0, this.lastMsgID);
        if (this.error != 0)
          return false;
        this.findseed = this.lastMsgID.ToString();
        IntPtr zero = IntPtr.Zero;
        this.error = Mapi.MAPIReadMail(this.session, this.winhandle, this.findseed, 2240, 0, ref zero);
        if (this.error != 0 || zero == IntPtr.Zero)
          return false;
        this.lastMsg = new MapiMessage();
        Marshal.PtrToStructure<MapiMessage>(zero, this.lastMsg);
        MapiRecipDesc structure = new MapiRecipDesc();
        if (this.lastMsg.originator != IntPtr.Zero)
          Marshal.PtrToStructure<MapiRecipDesc>(this.lastMsg.originator, structure);
        env.id = this.findseed;
        env.date = DateTime.ParseExact(this.lastMsg.dateReceived, "yyyy/MM/dd HH:mm", (IFormatProvider) DateTimeFormatInfo.InvariantInfo);
        env.subject = this.lastMsg.subject;
        env.from = structure.name;
        env.unread = (this.lastMsg.flags & 1) != 0;
        env.atts = this.lastMsg.fileCount;
        this.error = Mapi.MAPIFreeBuffer(zero);
        return this.error == 0;
      }

      [DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
      private static extern int MAPIFindNext(
        IntPtr sess,
        IntPtr hwnd,
        string typ,
        string seed,
        int flg,
        int rsv,
        StringBuilder id);

      public string Read(string id, out MailAttach[] aat)
      {
        aat = (MailAttach[]) null;
        IntPtr zero = IntPtr.Zero;
        this.error = Mapi.MAPIReadMail(this.session, this.winhandle, id, 2176, 0, ref zero);
        if (this.error != 0 || zero == IntPtr.Zero)
          return (string) null;
        this.lastMsg = new MapiMessage();
        Marshal.PtrToStructure<MapiMessage>(zero, this.lastMsg);
        if (this.lastMsg.fileCount > 0 && this.lastMsg.fileCount < 100 && this.lastMsg.files != IntPtr.Zero)
          this.GetAttachNames(out aat);
        Mapi.MAPIFreeBuffer(zero);
        return this.lastMsg.noteText;
      }

      public bool Delete(string id)
      {
        this.error = Mapi.MAPIDeleteMail(this.session, this.winhandle, id, 0, 0);
        return this.error == 0;
      }

      public bool SaveAttachm(string id, string name, string savepath)
      {
        IntPtr zero = IntPtr.Zero;
        this.error = Mapi.MAPIReadMail(this.session, this.winhandle, id, 128 /*0x80*/, 0, ref zero);
        if (this.error != 0 || zero == IntPtr.Zero)
          return false;
        this.lastMsg = new MapiMessage();
        Marshal.PtrToStructure<MapiMessage>(zero, this.lastMsg);
        bool flag = false;
        if (this.lastMsg.fileCount > 0 && this.lastMsg.fileCount < 100 && this.lastMsg.files != IntPtr.Zero)
          flag = this.SaveAttachByName(name, savepath);
        Mapi.MAPIFreeBuffer(zero);
        return flag;
      }

      private void GetAttachNames(out MailAttach[] aat)
      {
        aat = new MailAttach[this.lastMsg.fileCount];
        int num = Marshal.SizeOf(typeof (MapiFileDesc));
        MapiFileDesc structure = new MapiFileDesc();
        int files = (int) this.lastMsg.files;
        for (int index = 0; index < this.lastMsg.fileCount; ++index)
        {
          Marshal.PtrToStructure<MapiFileDesc>((IntPtr) files, structure);
          files += num;
          aat[index] = new MailAttach();
          if (structure.flags == 0)
          {
            aat[index].position = structure.position;
            aat[index].name = structure.name;
            aat[index].path = structure.path;
          }
        }
      }

      private bool SaveAttachByName(string name, string savepath)
      {
        bool flag = true;
        int num = Marshal.SizeOf(typeof (MapiFileDesc));
        MapiFileDesc structure = new MapiFileDesc();
        int files = (int) this.lastMsg.files;
        for (int index = 0; index < this.lastMsg.fileCount; ++index)
        {
          Marshal.PtrToStructure<MapiFileDesc>((IntPtr) files, structure);
          files += num;
          if (structure.flags == 0)
          {
            if (structure.name != null)
            {
              try
              {
                if (name == structure.name)
                {
                  if (File.Exists(savepath))
                    File.Delete(savepath);
                  File.Move(structure.path, savepath);
                }
              }
              catch (Exception ex)
              {
                flag = false;
                this.error = 13;
              }
              try
              {
                File.Delete(structure.path);
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
        return flag;
      }

      [DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
      private static extern int MAPIReadMail(
        IntPtr sess,
        IntPtr hwnd,
        string id,
        int flg,
        int rsv,
        ref IntPtr ptrmsg);

      [DllImport("MAPI32.DLL")]
      private static extern int MAPIFreeBuffer(IntPtr ptr);

      [DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
      private static extern int MAPIDeleteMail(IntPtr sess, IntPtr hwnd, string id, int flg, int rsv);

      public bool SingleAddress(string label, out string name, out string addr)
      {
        name = (string) null;
        addr = (string) null;
        int newrec = 0;
        IntPtr zero = IntPtr.Zero;
        this.error = Mapi.MAPIAddress(this.session, this.winhandle, (string) null, 1, label, 0, IntPtr.Zero, 0, 0, ref newrec, ref zero);
        if (this.error != 0 || newrec < 1 || zero == IntPtr.Zero)
          return false;
        MapiRecipDesc structure = new MapiRecipDesc();
        Marshal.PtrToStructure<MapiRecipDesc>(zero, structure);
        name = structure.name;
        addr = structure.address;
        Mapi.MAPIFreeBuffer(zero);
        return true;
      }

      [DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
      private static extern int MAPIAddress(
        IntPtr sess,
        IntPtr hwnd,
        string caption,
        int editfld,
        string labels,
        int recipcount,
        IntPtr ptrrecips,
        int flg,
        int rsv,
        ref int newrec,
        ref IntPtr ptrnew);

      public string Error()
      {
        return this.error >= 0 && this.error <= 26 ? this.errors[this.error] : $"?unknown? [{this.error.ToString()}]";
      }
    }
}
