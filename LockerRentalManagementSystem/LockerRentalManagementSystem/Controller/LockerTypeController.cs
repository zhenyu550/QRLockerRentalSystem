using ClosedXML.Excel;
using LockerRentalManagementSystem.Core;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.Controller
{
    public class LockerTypeController
    {
        // Private Attributes
        private LockerType _lockerType = new LockerType();

        // Constructor
        public LockerTypeController() { }

        // Methods
        public void SetLockerTypeData(string name, string code, decimal rate)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(code))
                throw new InvalidUserInputException("Empty Field");
            else
            {
                if (!Database.CheckUnique("locker_type", "name", name))
                    throw new InvalidUserInputException("Duplicate Detected", "IC / Passport No.", name, "Locker Type");
                if (!Database.CheckUnique("locker_type", "code", code))
                    throw new InvalidUserInputException("Duplicate Detected", "IC / Passport No.", code, "Locker Type");

                _lockerType.Name = name;
                _lockerType.Code = code;
                _lockerType.Rate = rate;
            }
        }

        public void SetLockerTypeData(LockerType lockerType, decimal rate)
        {
            _lockerType = lockerType;
            _lockerType.Rate = rate;
        }

        public void SaveLockerTypeData()
        {
            _lockerType.Save();
        }

        public void DeleteLockerTypeData(int id)
        {
            // Check if any cabinet exists for this locker type. If yes, show error.
            if (Cabinet.Count(String.Format("locker_type_id = {0} AND status <> 'Disabled'", id)) > 0)
                throw new InvalidUserInputException("Delete Error - Locker Type Cabinet");
            
            // Delete the locker type
            LockerType deletedLockerType = LockerType.Get(id);
            deletedLockerType.TempDelete();
        }

        public void RestoreLockerTypeData(int id)
        {
            LockerType lockerType = LockerType.Get(id);
            lockerType.Restore();
        }

        public void ExportLockerTypeData(int id)
        {
            var delType = LockerType.Where(String.Format("id = {0}", id), 0, 1);
            string defaultFileName = String.Format("EXPORT_LOCKER_TYPE_{0}_{1}", id, DateTime.Now.ToString("ddMMyyyy_HHmmss"));

            int noOfCabinet = Cabinet.Count(String.Format("locker_type_id = {0}", id));
            if (noOfCabinet > 0)
                throw new InvalidUserInputException("Export Fail - Cabinet");

            var workbook = new XLWorkbook();
            var ws = workbook.AddWorksheet("DeletedCabinet");
            ws.Cell(1, 1).Value = "Locker Type";
            ws.Cell(2, 1).InsertTable(delType);

            SaveFileDialog sf = new SaveFileDialog
            {
                FileName = defaultFileName,
                Filter = "Excel Workbook (.xlsx) |*.xlsx",
                Title = "Export Locker Type as",
                FilterIndex = 1
            };

            if (sf.ShowDialog() == DialogResult.OK)
            {
                string savePath = Path.GetDirectoryName(sf.FileName);
                string fileName = Path.GetFileName(sf.FileName);
                string saveFile = Path.Combine(savePath, fileName);
                try
                {
                    workbook.SaveAs(saveFile); //Save the file

                    delType[0].Delete();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    throw new InvalidUserInputException("Export Fail", "", "", "locker type");
                }
            }
            else
                throw new InvalidUserInputException("Export Fail", "", "", "locker type");
        }
    }
}