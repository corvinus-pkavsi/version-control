﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace Excell_Export
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats = new List<Flat>();
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            Flats = context.Flats.ToList();
        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();



            return ExcelCoordinate;
        }
        private void CreaTable()
        {
            string[] headers = new string[]
            {
                 "Kód",
                 "Eladó",
                 "Oldal",
                 "Kerület",
                 "Lift",
                 "Szobák száma",
                 "Alapterület (m2)",
                 "Ár (mFt)",
                 "Négyzetméter ár (Ft/m2)"
            };

            for (int length = 0; length < headers.Length; length++)
            {
                int counter = 0;
                xlSheet.Cells[1, length + 1] = headers[0];
                object[,] values = new object[Flats.Count, headers.Length];
                foreach (Flat f in Flats)
                {
                    values[counter, 0] = f.Code;
                    values[counter, 1] = f.Vendor;
                    values[counter, 2] = f.Side;
                    values[counter, 3] = f.District;
                    values[counter, 4] = f.Elevator;
                    values[counter, 5] = f.NumberOfRooms;
                    values[counter, 6] = f.FloorArea;
                    values[counter, 7] = f.Price;
                    values[counter, 8] = f.FlatSK;
                }
                xlSheet.get_Range(
                    GetCell(2, 1),
                    GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;
            }


        }
        private void CreateExcell()
        {
            try
            {
                xlApp = new Excel.Application();

                xlWB = xlApp.Workbooks.Add(Missing.Value);

                xlSheet = xlWB.ActiveSheet;

                CreaTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;


            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg);

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }
    }
}
