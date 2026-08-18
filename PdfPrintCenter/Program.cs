using Intermech.Diagnostics;
using Intermech.PdfPrintCenter.Connector;
using Intermech.PdfPrintCenter.Utils;
using Intermech.UI.Winforms;
using Ninject;
using Ninject.Modules;
using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            HighDPIServices.EnableHighDPIMode();
            if (Array.IndexOf<string>(args, "/WaitForPDMSystem") == -1)
            {
                Program.ShowUnlaunchedMessageBox();
            }
            else
            {
                using (StandardKernel iocContainer = new StandardKernel(Array.Empty<INinjectModule>()))
                {
                    iocContainer.Load((INinjectModule)new PrintCenterNinjectModule());
                    RemotingConfiguration.Configure((string)null, false);
                    ChannelServices.RegisterChannel((IChannel)new IpcChannel(Process.GetCurrentProcess().Id.ToString()), false);
                    PrintCenterSystem printCenterSystem = iocContainer.Get<PrintCenterSystem>();
                    RemotingServices.Marshal((MarshalByRefObject)printCenterSystem, "PrintCenterSystem", typeof(IPrintCenterSystem));
                    if (!printCenterSystem.StartedEvent.Wait(30000))
                    {
                        Program.ShowUnlaunchedMessageBox();
                    }
                    else
                    {
                        new FatalExceptionLogger(iocContainer.Get<IEventLogWriter>()).Activate();
                        new UIExceptionHandler((Action<Exception>)(uiException => Program.HandleUiException(uiException, iocContainer))).Activate();
                        PrintCenterForm mainForm;
                        try
                        {
                            mainForm = iocContainer.Get<PrintCenterForm>();
                        }
                        catch (Exception ex)
                        {
                            int num = (int)MessageBox.Show("Не удалось инициализировать центр печати. " + ex.Message, PrintCenterConsts.PrintCenterTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        Application.Run((Form)mainForm);
                    }
                }
            }
        }

        private static void HandleUiException(Exception e, StandardKernel iocContainer)
        {
            if (iocContainer.Get<ExceptionForm>().ShowException(e) != DialogResult.Abort)
                return;
            Application.Exit();
        }

        private static void ShowUnlaunchedMessageBox()
        {
            int num = (int)MessageBox.Show("Не удалось дождаться подключения PDM-системы к центру печати. Работа приложения без PDM-системы не поддерживается.", PrintCenterConsts.PrintCenterTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}
