using System;
using System.Reflection;
using System.Management;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace Acrolinx.Sdk.Sidebar.Util
{
    public class AssemblyUtil
    {
        private readonly Assembly asm;
        public AssemblyUtil(Assembly asm)
        {
            this.asm = asm;
        }
        #region Assembly Attribute Accessors

        public string AssemblyPath
        {
            get
            {
                return new Uri(this.asm.CodeBase).LocalPath;
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return this.asm.GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = this.asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = this.asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = this.asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = this.asm.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        public static Dictionary<string, string> OSInfo()
        {
            var osInfo = new Dictionary<string, string>();

            bool is64bit = Environment.Is64BitOperatingSystem;
            var name = (from t in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>() select t.GetPropertyValue("Caption")).FirstOrDefault();
            var osName = name != null ? name.ToString() : "Unknown";
            osInfo.Add("osName", osName);

            var architecture = is64bit ? " 64 bit" : " 32 bit";
            var osVersion = Environment.OSVersion.ToString() + architecture;
            osInfo.Add("version", osVersion);

            osInfo.Add("osId", osName.Trim().Replace(" ", ".").ToLower());

            return osInfo;
        }

        public static Dictionary<string, string> AppInfo()
        {
            var appInfo = new Dictionary<string, string>();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var architecture = (IntPtr.Size * 8).ToString() + " bit";
            appInfo.Add("applicationName", fvi.FileDescription.Split(' ').Last());
            appInfo.Add("productName", fvi.FileDescription);
            appInfo.Add("version", fvi.FileVersion + " " + architecture);
            appInfo.Add("appId", fvi.FileDescription.Trim().Replace(" ", ".").ToLower());

            return appInfo;
        }
    }
}
