using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExtension.Utilties
{
 public   class ExcelReader
    {
        private Dictionary<string, string> props = new Dictionary<string, string>();
        private String FileName = "";
        private string Range = "";
        public ExcelReader(string FileName)
        {
            this.FileName = FileName;
        }

        public ExcelReader(string filename, string range)
        {
            // TODO: Complete member initialization
            this.FileName = filename;
            this.Range = range;
        }


        public DataSet ReadExcelFile()
        {
            var filename = this.FileName;
            var connString = string.Format(
                @"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""",
                Path.GetDirectoryName(filename)
            );
            //var connString = string.Format(
           //     @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=Excel 8.0;",
           //     Path.GetDirectoryName(filename)
           // );
            
            using (var conn = new OleDbConnection(connString))
            {
                string query;
                conn.Open();
                if (this.Range != "")
                {
                    query = "SELECT * FROM [" + Path.GetFileName(filename) + "$" + Range + "]";

                }
                else
                {
                    query = "SELECT * FROM [" + Path.GetFileName(filename) + "]";

                }
                using (var adapter = new OleDbDataAdapter(query, conn))
                {
                    var ds = new DataSet("CSV File");
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }

        public DataSet ReadExcelFile(string fn)
        {
            var filename =fn;
            string sql = "SELECT * FROM [Sheet1$]";
            string excelConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fn + ";Extended Properties=Excel 12.0;";
            excelConnection=string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0 Xml;HDR=YES;IMEX=1;", fn);
            using (OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, excelConnection))
            {
                DataSet ds = new DataSet();
                adaptor.Fill(ds);
                return ds;
            }
        }

    }
}
