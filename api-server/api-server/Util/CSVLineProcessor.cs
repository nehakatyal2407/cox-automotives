using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_server.Util
{
    public class CSVLineProcessor
    {
        public static string[] parseLine(string csvLine)
        {
            using (TextFieldParser TFP = new TextFieldParser(new MemoryStream(Encoding.UTF8.GetBytes(csvLine))))
            {
                TFP.HasFieldsEnclosedInQuotes = true;
                TFP.SetDelimiters(",");

                try
                {
                    return TFP.ReadFields();
                }
                catch (MalformedLineException)
                {
                    StringBuilder m_sbLine = new StringBuilder();

                    for (int i = 0; i < TFP.ErrorLine.Length; i++)
                    {
                        if (i > 0 && TFP.ErrorLine[i] == '"' && (TFP.ErrorLine[i + 1] != ',' && TFP.ErrorLine[i - 1] != ','))
                            m_sbLine.Append("\"\"");
                        else
                            m_sbLine.Append(TFP.ErrorLine[i]);
                    }

                    return parseLine(m_sbLine.ToString());
                }
            }
        }
    }
}
