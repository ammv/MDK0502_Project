using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Excel =  Microsoft.Office.Interop.Excel;

namespace Momatov.ClassFolder
{
    class ExportClass
    {
        public static void ToExcelFile(DataGrid listDataGrid, string nameList)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            var process = Process.GetProcessesByName("EXCEL");

            SaveFileDialog openDlg = new SaveFileDialog();
            openDlg.FileName = nameList;
            openDlg.Filter = "Excel (.xls)|*.xls|Excel (.xlsx)|*.xlsx|All files(*.*)|*.*";
            openDlg.FilterIndex = 2;
            openDlg.RestoreDirectory = true;
            string path = openDlg.FileName;

            if(openDlg.ShowDialog() == true)
            {
                excelApp = new Excel.Application();
                excelApp.Visible = true;
                workbook = excelApp.Workbooks.Add(System.Reflection.Missing.Value);
                worksheet = (Excel.Worksheet)workbook.Sheets[1];

                for (int i = 0; i < listDataGrid.Columns.Count; i++)
                {
                    Excel.Range myRange = (Excel.Range)worksheet.Cells[1, i+1];
                    worksheet.Cells[1, i+1].Font.Bold = true;
                    worksheet.Cells[1, i+1].Style.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    myRange.Value2 = listDataGrid.Columns[i].Header;

                    for (int j = 0; j < listDataGrid.Items.Count; j++)
                    {
                        TextBlock textBlock = listDataGrid.Columns[i].GetCellContent(listDataGrid.Items[j]) as TextBlock;
                        myRange = (Excel.Range)worksheet.Cells[j+2, i+1];
                    }

                }

                worksheet.Columns.AutoFit();
            }
        }
    }
}

