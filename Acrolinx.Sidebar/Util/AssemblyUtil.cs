using System;
using System.Reflection;
using System.Management;
using System.Linq;

namespace Acrolinx.Sdk.Sidebar.Util
{
    public class AssemblyUtil
    {
        private Assembly asm;
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

        public static string OSName()
        {
            bool is64bit = Environment.Is64BitOperatingSystem;
            var name = (from t in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>() select t.GetPropertyValue("Caption")).FirstOrDefault();
            var osName = name != null ? name.ToString() : "Unknown";
            var version = is64bit ? " 64 bit" : " 32 bit";

            return osName + version;
        }
    }
}
