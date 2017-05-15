using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyKernelEventMonitor
{
    public class CsvRow : List<string>
    {
        public string rowText { get; set; }
    }

    class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream) : base(stream) { }
        public CsvFileWriter(string filename) : base(filename) { }

        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            bool isFirstCol = true;

            foreach (string field in row)
            {
                if (!isFirstCol)
                    builder.Append(',');

                if (field.IndexOfAny(new char[] { '"', ',' }) != -1)
                    builder.AppendFormat("\"{0}\"", field.Replace("\"", "\"\""));
                else
                    builder.Append(field);
                isFirstCol = false;
            }
            row.rowText = builder.ToString();
            WriteLine(row.rowText);
        }

        public static void WriteTest()
        {
            using (CsvFileWriter writer = new CsvFileWriter("WriteTrst.csv"))
            {
                for (int i = 0; i < 100; i++)
                {
                    CsvRow row = new CsvRow();
                    for (int j = 0; j < 5; j++)
                        row.Add(string.Format("column{0}", j));
                    writer.WriteRow(row);

                }
            }
        }
    }
}
