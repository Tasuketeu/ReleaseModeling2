using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Modelirovanie_lab1_Console
{
    class Excel
    {
        private string _path = "";
        private _Application _excel = new _Excel.Application();
        private Workbook _wb;
        private Worksheet _ws;

        public Excel(string path)
        {
            _wb = _excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            CreateNewSheet();
            _ws = _wb.Worksheets[1];
            _path = path;
            SaveAs(_path);
        }

        ~Excel()
        {
            Close();
        }

        public Excel(string path, int sheet)
        {
            _path = path;
            _wb = _excel.Workbooks.Open(path);
            _ws = _wb.Worksheets[sheet];
        }

        public void CreateNewSheet()
        {
            _wb.Worksheets.Add();
        }

        public string ReadCell(int i, int j)
        {
            i++;
            j++;
            if (_ws.Cells[i, j].value2 != null)
                return _ws.Cells[i, j].value2;
            else
                return "";
        }

        public void WriteToCell(int i, int j, double s)
        {
            i++;
            j++;
            _ws.Cells[i, j].value2 = s;
        }

        public void Save()
        {
            _wb.Save();
        }

        public void SaveAs(string path)
        {
            _wb.SaveAs(path);
        }

        public void Close()
        {
            if (_excel != null)
            {
                _excel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_excel);
                _excel = null;
                _wb = null;
                _ws = null;
                GC.Collect();
            }
        }
    }
}
