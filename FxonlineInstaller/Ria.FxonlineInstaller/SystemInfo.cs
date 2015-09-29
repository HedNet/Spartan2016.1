using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;   //This namespace is used to work with WMI classes. For using this namespace add reference of System.Management.dll .
using Microsoft.Win32;
using Ria.FxonlineInstaller;     //This namespace is used to work with Registry editor.

namespace Ria.FxonlineInstaller
{
    /*
    class TestProgram
    {
        static void Main(string[] args)
        {
            SystemInfo si = new SystemInfo();       //Create an object of SystemInfo class.
            si.getOperatingSystemInfo();            //Call get operating system info method which will display operating system information.
            si.getProcessorInfo();                  //Call get  processor info method which will display processor info.
            // Console.ReadLine();                     //Wait for user to accept input key.
        }
    }
     */

    public class SystemInfo
    {
        public void getOperatingSystemInfo()
        {
            // Console.WriteLine("Displaying operating system info....\n");
            //Create an object of ManagementObjectSearcher class and pass query as parameter.
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    // Console.WriteLine("Operating System Name  :  " + managementObject["Caption"].ToString());   //Display operating system caption
                    this.OSName = managementObject["Caption"].ToString();
                }
                if (managementObject["OSArchitecture"] != null)
                {
                    // Console.WriteLine("Operating System Architecture  :  " + managementObject["OSArchitecture"].ToString());   //Display operating system architecture.
                    this.OSArchitecture = managementObject["OSArchitecture"].ToString();
                }
                if (managementObject["CSDVersion"] != null)
                {
                    // Console.WriteLine("Operating System Service Pack   :  " + managementObject["CSDVersion"].ToString());     //Display operating system version.
                    this.OSSP= managementObject["CSDVersion"].ToString();
                }
                string syscpu="";
                if ((syscpu=getProcessorInfo()) != null)
                {
                    this.CPU = syscpu;
                }
            }
        }

        public string getProcessorInfo()
        {
            // Console.WriteLine("\n\nDisplaying Processor Name....");
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);   //This registry entry contains entry for processor info.

            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    // Console.WriteLine(processor_name.GetValue("ProcessorNameString"));   //Display processor ingo.
                    return processor_name.GetValue("ProcessorNameString").ToString();
                }
            }
            return null;
        }

        public SystemInfo() { getOperatingSystemInfo(); }

        public string OSName { get; internal set; }

        public string OSArchitecture { get; internal set; }

        public string OSSP { get; internal set; }

        public string CPU { get; internal set; }
    }
}
