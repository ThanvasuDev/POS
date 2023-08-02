using System.Windows.Forms;
using System.Configuration;

namespace AppRest
{
    public static class ScreenService
    {
        public static SecondMonitor SecondMonitor { get; }
        public static FormCheckBill2Screen FormCheckBill2Screen { get; }
        

        static ScreenService()
        {
            SecondMonitor = new SecondMonitor();
            FormCheckBill2Screen = new FormCheckBill2Screen();
        }

        // Show any dialog on the main screen if showOnMonitor equal 1
        public static DialogResult ShowDialogOnMonitor(int showOnMonitor, Form form)
        {
            if (Screen.AllScreens.Length == 1)
                showOnMonitor = 0;

            var sc = Screen.AllScreens;
            form.Left = (sc[showOnMonitor].Bounds.Width - form.Width) / 2;
            form.Top = (sc[showOnMonitor].Bounds.Height - form.Height) / 2;

            if (ConfigurationSettings.AppSettings["MonitorDisplay"] == "Y")
            {
                form.StartPosition = FormStartPosition.Manual;
                form.WindowState = FormWindowState.Maximized; 
            }

            return form.ShowDialog();
        }

        // Show second monitor
        public static void ShowOnMonitor(int showOnMonitor)
        {
            if (Screen.AllScreens.Length == 1)
                showOnMonitor = 0;

            SecondMonitor.FormBorderStyle = FormBorderStyle.None;
            SecondMonitor.Show();
            var workingArea = Screen.AllScreens[showOnMonitor].WorkingArea;
            SecondMonitor.Location = workingArea.Location;
            SecondMonitor.WindowState = FormWindowState.Maximized;
        }

        public static void ShowOnMonitorCashResult(int showOnMonitor)
        {
            if (Screen.AllScreens.Length == 1)
                showOnMonitor = 0;

            FormCheckBill2Screen.FormBorderStyle = FormBorderStyle.None;
            FormCheckBill2Screen.Show();
            var workingArea = Screen.AllScreens[showOnMonitor].WorkingArea;
            FormCheckBill2Screen.Location = workingArea.Location;
             FormCheckBill2Screen.WindowState = FormWindowState.Maximized;
           // FormCheckBill2Screen.StartPosition = FormStartPosition.CenterScreen;
           
        }

        public static void ShowOnMonitorCashResultHide()
        {
            FormCheckBill2Screen.Visible = false;
        }


            public static void SendDataToSecondMonitor(DisplayData displayData)
        {
            SecondMonitor.DisplayData = displayData;
        }
    }
}
