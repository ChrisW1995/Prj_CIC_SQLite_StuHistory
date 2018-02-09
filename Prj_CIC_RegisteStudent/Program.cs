using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Prj_CIC_RegisteStudent.Model;
using System.IO;

namespace Prj_CIC_RegisteStudent
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 105; i < 107; i++)
            {
                bool exists = Directory.Exists(i.ToString());
                if (!exists)
                    Directory.CreateDirectory(i.ToString());
            }
            if (File.Exists("All_Student_105.csv"))
            {
                File.Delete("All_Student_105.csv");
            }
            if (File.Exists("All_Student_106.csv"))
            {
                File.Delete("All_Student_106.csv");
            }
            Console.WriteLine("按下Enter開始匯出...");
            Console.ReadKey();
            Console.WriteLine("請稍候...");
            StudentsRepository repo = new StudentsRepository(new StudentContext());
            var testno_list = repo.GetTestNoList();
            foreach (var item in testno_list)
            {
               
                try
                {
                    string connectionString = @"Data Source=C:\Users\user\Documents\Visual Studio 2017\Projects\Prj_CIC_RegisteStudent\Prj_CIC_RegisteStudent\sch_category.sqlite;Version=3;";
                    using (SQLiteConnection conn = new SQLiteConnection(Configuration.ConfigurationManager.ConnectionStrings["StudentContext"].ConnectionString))
                    {
                        conn.Open();
                        string sql = $"SELECT * FROM student WHERE testno = {item.testno} AND year = {item.year} And sch = '中國文化大學' Group By schname" ;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {

                            //IWorkbook workbook = new XSSFWorkbook();
                            //IWorkbook workbook_file = new XSSFWorkbook();
                            //XSSFSheet sheet1 = (XSSFSheet) workbook.CreateSheet($"{item.testno}");
                            //XSSFSheet sheet_file = (XSSFSheet)workbook.CreateSheet($"result");
                            var csv = new StringBuilder();
                            string filePath = $"{item.year}\\{item.testno}.csv";
                            bool resultY1=false, resultY2= false;
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                
                                using (var sw = new StreamWriter(filePath, false, Encoding.UTF8))
                                {
                                    Console.WriteLine($"Creating file {item.testno}.csv...");
                                    while (reader.Read())
                                    {
                                        var year = reader["year"].ToString().Trim();
                                        var name = reader["name"].ToString().Trim();
                                        var sch = reader["sch"].ToString().Trim().Replace("\r", "");
                                        var dept = reader["dept"].ToString().Trim().Replace("\r", "");
                                        var schname = reader["schname"].ToString().Trim().Replace("\r","");
                                        var check = reader.GetString(8).Trim();
                                        var newLine = $"{year},{sch},{dept},{name},{schname},{check}";
                                        if (check.ToLower().Equals("true"))
                                        {
                                            resultY1 = schname.Contains("文化大學") ? true : false;
                                            resultY2 = schname.Contains(sch+dept) ? true : false;
                                        }
                                        sw.WriteLine(newLine);

                                    }
                                }
                            }
                            using (var sw = new StreamWriter($"All_Student_{item.year}.csv", true, Encoding.UTF8))
                            {
                                
                                sw.WriteLine($".\\{filePath},{resultY1},{resultY2}");
                            }
                        }
                        conn.Close();
                    }
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("Error Occour!"+e.ToString());
                    Console.ReadKey();
                }
              
            }


        }
    }
}
