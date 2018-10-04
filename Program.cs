using SimpleListener.Properties;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace SimpleListener
{
    internal class Program
    {
        private static void Main()
        {

            ////GetTransCodeByRefID: [55000000001120180905152812], (3[1 - 46 - 9] | 41)0{ 6}\d{ 4}\d{ 8}\d{ 6} ,IgnoreCase,
            //// 55 0000 000011 20180905 152812
            //// 55 000000 0011 20180905 152812
            //var msgRefID = new List<string>();
            ////msgRefID.Add("31000000001220180911154904");
            ////msgRefID.Add("36000000000320180525162603");
            ////msgRefID.Add("38000000000120180526122639");
            ////msgRefID.Add("55000000001120180905152812");
            ////msgRefID.Add("56000000001120180905152812");
            ////msgRefID.Add("58000000001120180905152812");
            ////msgRefID.Add("57000000001120180905152812");
            //foreach (var item in msgRefID)
            //{
            //    var xx = Regex.Match(item, "(3[1-46-9]|5[568])0{6}\\d{4}\\d{8}\\d{6}", RegexOptions.IgnoreCase).Value;
            //    Console.WriteLine($"{item} = {xx}");
            //}

            ////msgRefID.Add("ANCRMBK2017050800002               M3100700068124       Miss                Siriporn Tangjairakkandee     ศิริพร ตั้งใจรักการดี         0421974102022134/262                                                                                   แขวงบางลำภูล่าง     เขตคลองสาน          กรุงเทพมหานคร       10600002437326100000811703213     1                                   20000021                     บมจ.อิออนธนสินทรัพย์(ไทยแลนด์)                    MKT BR.MANAGEMENT             388                                                                                            คลองเตย             คลองเตย             กรุงเทพมหานคร       1011000268970760000010120001 1                     0900                                            00005000000000000000000                    1                                          2015100920231019134/262                                                                                        กรุงเทพมหานคร       05209129933435012499                                                                                                                                    03919324                      1813CPMIB146RR20010Rathapongp                                                                                1002017050800000000000000000000000000000000134/262                                                                                   แขวงบางลำภูล่าง     เขตคลองสาน          กรุงเทพมหานคร       10600                         0.00         0.00       เนาวรัตน์                     BROTHER/SISTER 0817522310      0817522310 1 1CR1234567890123456                                                                                                                                                                                                                                                             19000101                                                                                                                                                                                                                                                                                                                                                                                           #");
            //msgRefID.Add("NWJDM0000000000000100310000000005201809121316431470600005211       BK                              #");
            ////string RxRegion = "(?&lt;=^nwjdm0{12}\\d{4}((3[346-9]|41)[a-z\\d]{24}a[nv]crm))(?:bk)|(?&lt;=^nwjdm0{12}\\d{4}((3[12]).+))(?:bk)(?=[ ]{30}#$)";
            //string RxRegion = Settings.Default.RxRegion;
            //foreach (var item in msgRefID)
            //{
            //    var xx = Regex.IsMatch(item, RxRegion, RegexOptions.IgnoreCase);
            //    Console.WriteLine($"{item} = {xx}");
            //}


            try
            {
                SocketRecv sc = new SocketRecv();
                Thread tr = new Thread(() => sc.StartListener()) { IsBackground = true };
                tr.Start();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}