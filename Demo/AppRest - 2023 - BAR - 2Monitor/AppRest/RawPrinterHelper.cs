

using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
 
namespace AppRest
    /*    For a standard cash drawer, there's a physical cable that runs from the cash drawer and plugs into the receipt printer. 
•    To open the drawer, you issue a command to the printer. The printer will send a signal to the cash drawer that kicks open the drawer. 
•    For a standard epson receipt printer such as the Epson TM-T88III receipt printer, the command is:
fwrite($handle, chr(27). chr(112). chr(0). chr(100). chr(250)); 
•    For other receipt printers, you should be able to find the command in their user's manual. (Try the above code first. Most probably it will work. Receipt printers have been in existence for a long time. They have more or less adopted the same standard.) 
•    A standard receipt printer such as the Epson TM-T88III receipt printer uses the parallel port. 
•    To print to the parallel-port receipt printer, you print through port PRN (exactly the same as printing from DOS prompt). 
•    From within PHP-GTK2, you need to first establish the connection with the printer by using 
$handle = fopen("PRN", "w"); 
•    Thereafter, to print anything to the printer, you just "write" to it like the file handle: fwrite($handle, 'text to printer'); 
•    There are newer receipt printer that uses USB. I believe you should be able to print to such printers through PRN too. 
*/
{
    public class RawPrinterHelper
    {
    // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]

       

        public class DOCINFOA
            {
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
            }
        [DllImport("winspool.Drv", EntryPoint="OpenPrinterA", SetLastError=true,CharSet=CharSet.Ansi, ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] 
        string szPrinter, out IntPtr hPrinter, long pd);
        [DllImport("winspool.Drv", EntryPoint="ClosePrinter", SetLastError=true,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", EntryPoint="StartDocPrinterA", SetLastError=true,CharSet=CharSet.Ansi, ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
        public static extern bool StartDocPrinter( IntPtr hPrinter, Int32 level,[In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);
        [DllImport("winspool.Drv", EntryPoint="EndDocPrinter", SetLastError=true,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", EntryPoint="StartPagePrinter", SetLastError=true,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", EntryPoint="EndPagePrinter", SetLastError=true,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", EntryPoint="WritePrinter", SetLastError=true,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten );
        [DllImport("kernel32.dll", EntryPoint="GetLastError", SetLastError=false, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
        public static extern Int32 GetLastError();
        public static bool SendBytesToPrinter( string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.
            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";
            if( OpenPrinter( szPrinterName, out hPrinter, 0 ) )
            {
                if( StartDocPrinter(hPrinter, 1, di) )
                    {
                    if( StartPagePrinter(hPrinter) )
                        {
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);}
                    EndDocPrinter(hPrinter);
                    }
                ClosePrinter(hPrinter);
                }
            if( bSuccess == false )
                {
                dwError = GetLastError();
                }
            return bSuccess;
            }
        public static bool SendFileToPrinter( string szPrinterName, string szFileName )
            {
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            Byte []bytes = new Byte[fs.Length];
            bool bSuccess = false;
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;
            nLength = Convert.ToInt32(fs.Length);
            bytes = br.ReadBytes( nLength );
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
            }
        public static bool SendStringToPrinter( string szPrinterName, string szString )
            {
            IntPtr pBytes;
            Int32 dwCount;
            dwCount = szString.Length;
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
            }
        public static bool OpenCashDrawer1( string szPrinterName)
            {
                //27,112,48,55,121 Epson + Exprinter
                // 27,7,11,55,7 
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();            
            bool bSuccess = false;
            di.pDocName = "OpenDrawer";
            di.pDataType = "RAW";

            string printerType = ConfigurationSettings.AppSettings["PrinterType"].ToString().ToUpper();

            if( OpenPrinter( szPrinterName, out hPrinter, 0 ) )
                {
                if( StartDocPrinter(hPrinter, 1, di) )
                    {
                    if( StartPagePrinter(hPrinter) )
                        {
                        int nLength;
                        byte[] DrawerOpen ;
                        
                        if( printerType == "X") 
                            DrawerOpen  = new byte[] { 27, 112, 48, 55, 121 };
                        else
                            DrawerOpen = new byte[] { 27, 7, 11, 55, 7 };

                        nLength = DrawerOpen.Length;
                        IntPtr p = Marshal.AllocCoTaskMem(nLength);
                        Marshal.Copy(DrawerOpen, 0, p, nLength);
                        bSuccess = WritePrinter(hPrinter, p, DrawerOpen.Length, out dwWritten);
                        EndPagePrinter(hPrinter);
                        Marshal.FreeCoTaskMem(p);
                        }
                    EndDocPrinter(hPrinter);
                    }
                ClosePrinter(hPrinter);
                }
            if( bSuccess == false )
                {
                dwError = GetLastError();
                }
            return bSuccess;
            }
        public static bool FullCut( string szPrinterName)
            {
                //27, 109
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false;
            di.pDocName = "FullCut";
            di.pDataType = "RAW";
            if( OpenPrinter( szPrinterName, out hPrinter, 0 ) )
                {
                if( StartDocPrinter(hPrinter, 1, di) )
                    {
                    if( StartPagePrinter(hPrinter) )
                        {
                        int nLength;
                        byte[] DrawerOpen = new byte[] { 27, 100, 51 };
                        nLength = DrawerOpen.Length;
                        IntPtr p = Marshal.AllocCoTaskMem(nLength);
                        Marshal.Copy(DrawerOpen, 0, p, nLength);
                        bSuccess = WritePrinter(hPrinter, p, DrawerOpen.Length, out dwWritten);
                        EndPagePrinter(hPrinter);
                        Marshal.FreeCoTaskMem(p);
                        }
                    EndDocPrinter(hPrinter);
                    }
                ClosePrinter(hPrinter);
                }
            if( bSuccess == false )
                {
                dwError = GetLastError();
                }
            return bSuccess;
            }
        }
    }
 
