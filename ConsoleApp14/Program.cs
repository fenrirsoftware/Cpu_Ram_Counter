using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ConsoleApp14
{
    internal class Program
    {

        public static void Rc()
        {
            //valla burda neden sql sorgusu attığımıza dair en ufak bir fikrim yok. 
            //bulduğum tüm yöntemler bu şekildeydi
            var wmiObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            var memoryValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new
            {
                FreePhysicalMemory = Double.Parse(mo["FreePhysicalMemory"].ToString()),
                TotalVisibleMemorySize = Double.Parse(mo["TotalVisibleMemorySize"].ToString())
            }).FirstOrDefault();


            if (true)  //döngünün kontrolünü sağlamak için if true attım
            {
                var percent = ((memoryValues.TotalVisibleMemorySize - memoryValues.FreePhysicalMemory) / memoryValues.TotalVisibleMemorySize) * 100;
                //hesap kitap
                Convert.ToInt16(percent);
                percent = Math.Round(percent, 0); //çevirme işlemleri round da virgülden sonrasını silme

                Console.Write("RAM Kullanımı:");
                Console.WriteLine(percent.ToString());



            }

            //sql sorgusu ama CPU için 
            ObjectQuery objQuery = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor WHERE Name=\"_Total\"");
            ManagementObjectSearcher mngObjSearch = new ManagementObjectSearcher(objQuery);
            ManagementObjectCollection mngObjColl = mngObjSearch.Get();

            if (mngObjColl.Count > 0)
            {
                foreach (ManagementObject mngObject in mngObjColl)
                {

                    uint cpu_usage = 100 - Convert.ToUInt32(mngObject["PercentIdleTime"]);

                    Console.Write("CPU Kullanımı:");
                    Console.Write(cpu_usage);


                }
            }

         



        }
        static void Main(string[] args)
        {
            //sonsuz döngüye alıyorum 
            //thread sleepi 2700 verme sebebim
            //bi yazıda görev yöneticisinin 2700 ms de bir güncelleme attığıydı 
            //yanlışmış :D
            while (true)
            {

                Rc();  //fonksiyon çağırma
                Thread.Sleep(2700);
                Console.Clear();  //console temizliyo
            }
        }
      

           
        
    }

}
