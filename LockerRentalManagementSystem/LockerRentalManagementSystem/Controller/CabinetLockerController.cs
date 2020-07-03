using ClosedXML.Excel;
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
    public class CabinetLockerController
    {
        // Private Attributes
        private Cabinet _cabinet = new Cabinet();

        // Constructor
        public CabinetLockerController() { }

        // Methods
        public void SetCabinetData(string code, int lockerTypeid, int row, int column)
        {
            _cabinet.Code = code;
            _cabinet.LockerTypeId = lockerTypeid;
            _cabinet.Row = row;
            _cabinet.Column = column;
        }

        public void SaveCabinetData()
        {
            // Create the cabinet
            _cabinet.Save();

            // Get the Id of the Saved cabinet above
            int insertedCabinetId = _cabinet.Id;

            // Generate Lockers for the Cabinet
            for (int i = 1; i <= (_cabinet.Row * _cabinet.Column); i++)
            {
                //Auto increment for locker codes
                var locker = new Locker
                {
                    Code = String.Format("{0}-{1}", _cabinet.Code, i.ToString("D3")),
                    CabinetId = insertedCabinetId
                };
                locker.Save();
            }
        }

        public void DeleteCabinetData(int id)
        {
            // Get the cabinet data
            var deletedCabinet = Cabinet.Get(id);

            // Calculate how many lockers in the cabinet
            int numberOfLockers = deletedCabinet.Row * deletedCabinet.Column;

            // Get all data of lockers for this cabinet
            List<Locker> lockers = Locker.Where(String.Format("cabinet_id = {0}", id), 0, numberOfLockers);

            // Check any locker was booked for rental. If yes, show error.
            foreach (Locker locker in lockers)
            {
                if (Rental.Count(String.Format("locker_id = {0} AND status <> 'Ended'", locker.Id)) > 0)
                    throw new InvalidUserInputException("Delete Error - Cabinet Locker Booked");
            }

            // Disable the cabinet
            deletedCabinet.TempDelete();

            // Disable Lockers belong to that cabinet
            foreach (Locker deletedLocker in lockers)
                deletedLocker.TempDelete();
        }

        public void RestoreCabinetData(int id)
        {
            var cabItem = Cabinet.Get(id);

            //Check if the Locker Type available in the list (Not deleted). If no, show error.
            var lockerType = new LockerType();
            var typeItem = LockerType.Get(cabItem.LockerTypeId);

            if (typeItem.IsDisabled())
                throw new InvalidUserInputException("Restore Error - Cabinet Locker Type", "", typeItem.Name, "");

            cabItem.Restore(); //Restore the cabinet

            //Calculate how many lockers in the cabinet
            int noOfLockers = cabItem.Row * cabItem.Column;

            //Restore the lockers assoicated to this cabinet
            List<Locker> lockerList = Locker.Where(String.Format("cabinet_id = {0}", id), 0, noOfLockers);
            for (int i = 0; i < noOfLockers; i++)
            {
                var restoreLocker = new Locker
                {
                    Id = lockerList[i].Id
                };
                restoreLocker.Reset();
            }
        }

        public void ExportCabinetData(int id)
        {
            var delCab = Cabinet.Where(String.Format("id = {0}", id), 0, 1);
            var delLockers = Locker.Where(String.Format("cabinet_id = {0}", id), 0, 2147483467);

            string defaultFileName = String.Format("EXPORT_CABINET_{0}_{1}", id, DateTime.Now.ToString("ddMMyyyy_HHmmss"));

            var workbook = new XLWorkbook();
            var ws = workbook.AddWorksheet("DeletedCabinet");
            ws.Cell(1, 1).Value = "Cabinet";
            ws.Cell(2, 1).InsertTable(delCab);
            ws.Cell(5, 1).Value = "Locker";
            ws.Cell(6, 1).InsertTable(delLockers);

            SaveFileDialog sf = new SaveFileDialog
            {
                FileName = defaultFileName,
                Filter = "Excel Workbook (.xlsx) |*.xlsx",
                Title = "Export Cabinet as",
                FilterIndex = 0
            };

            if (sf.ShowDialog() == DialogResult.OK)
            {
                string savePath = Path.GetDirectoryName(sf.FileName);
                string fileName = Path.GetFileName(sf.FileName);
                string saveFile = Path.Combine(savePath, fileName);
                try
                {
                    workbook.SaveAs(saveFile); //Save the file

                    foreach (Locker dLocker in delLockers)
                        dLocker.Delete();

                    delCab[0].Delete();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    throw new InvalidUserInputException("Export Fail", "", "", "cabinet");
                }
            }
            else
                throw new InvalidUserInputException("Export Fail", "", "", "cabinet");
        }

        public void CheckLockerAvailability(Locker locker, Rental rental)
        {
            // Check if the new selected locker is the original locker, if yes ignore operation
            if (locker.Id == rental.LockerId)
                return;

            bool isOverlap = false;
            DateTime startDate = rental.StartDate;
            DateTime endDate = rental.EndDate;

            // Get all existing rental data for the selected locker
            string bookingTimeCheckingString = String.Format("locker_id = {0} AND status <> 'Ended'", locker.Id);     
            List<Rental> existingRentals = Rental.Where(bookingTimeCheckingString, 0, 2147483467);

            foreach (Rental existingRental in existingRentals)
            {
                int newStartCompareExistEnd = startDate.Date.CompareTo(existingRental.EndDate.Date);
                int newEndCompareExistStart = endDate.Date.CompareTo(existingRental.StartDate.Date);

                /* 
                 * New Rental Start Date > Existing Rental End Date OR
                 * New Rental End Date < Existing Rental Start Date 
                 * are Valid time periods
                 */
                if (!(newStartCompareExistEnd > 0 || newEndCompareExistStart < 0))
                {
                    isOverlap = true;
                    break;
                }
            }

            if (isOverlap)
                throw new InvalidUserInputException("Reserved Timeslot");
        }

        public List<Locker> GetAvailableLockers(int cabinetId, DateTime startDate, DateTime endDate)
        {
            // Select all lockers for that particular cabinet
            List <Locker> lockers = Locker.Where(String.Format("cabinet_id = {0}", cabinetId), 0, 2147483467);

            // foreach locker in the lockers list, check for time peroid overlaps
            // lockers.ToList() duplicates the value of lockers to a seperate list that will be used 
            // at the foreach loop to prevent modifications on the list used for looping
            foreach (Locker locker in lockers.ToList())
            {
                bool isOverlap = false;

                if (locker.IsNotAvailable() || locker.IsDisabled())
                {
                    isOverlap = true;
                }
                else
                {
                    string bookingTimeCheckingString = String.Format("locker_id = {0} AND status <> 'Ended'", locker.Id);
                    List<Rental> existingRentals = Rental.Where(bookingTimeCheckingString, 0, 2147483467);

                    foreach (Rental existingRental in existingRentals)
                    {
                        int newStartCompareExistEnd = startDate.Date.CompareTo(existingRental.EndDate.Date);
                        int newEndCompareExistStart = endDate.Date.CompareTo(existingRental.StartDate.Date);

                        /* 
                         * New Rental Start Date > Existing Rental End Date OR
                         * New Rental End Date < Existing Rental Start Date 
                         * are Valid time periods
                         */
                        if (!(newStartCompareExistEnd > 0 || newEndCompareExistStart < 0))
                        {
                            isOverlap = true;
                            break;
                        }
                    }
                }
                if (isOverlap)
                    lockers.Remove(locker);
            }

            List<Locker> availableLockers = new List<Locker>();
            availableLockers = lockers;

            return availableLockers;
        }

        public void DisableLocker(string lockerCode)
        {
            string lockerCodeCondition = String.Format("code = '{0}'", lockerCode);
            List<Locker> lockers = Locker.Where(lockerCodeCondition, 0, 1);
            var locker = lockers[0];

            if (locker.IsNotAvailable())
                throw new InvalidUserInputException("Disable Error - Locker Disabled");
            else if (Rental.Count(String.Format("locker_id = {0} AND status <> 'Ended'", locker.Id)) > 0)
                throw new InvalidUserInputException("Disable Error - Locker Booked");
            else
            {
                locker.NotAvailable();

                //Check is the cabinet full. If yes, update and insert log.
                int noOfEmptyLocker = Locker.Count(String.Format("cabinet_id = {0} AND status = 'Available'",
                    lockers[0].CabinetId));
                if (noOfEmptyLocker <= 0)
                {
                    var cabinet = Cabinet.Get(lockers[0].CabinetId);
                    cabinet.Full();
                }
            }
        }

        public void ResetLocker(string lockerCode)
        {
            string lockerCodeCondition = String.Format("code = '{0}'", lockerCode);
            List<Locker> lockers = Locker.Where(lockerCodeCondition, 0, 1);
            var locker = lockers[0];

            // Check if locker is already available, if yes, ignore.
            if (locker.IsAvailable())
                return;

            //Check if locker occupied / overdued, if yes, show error message.
            if (locker.IsOccupied() || locker.IsOverdued())
                throw new InvalidUserInputException("Reset Error - Locker Occupied");

            locker.Reset();

            //Check was the cabinet full. If yes, set to available.
            var selectedCabinet = Cabinet.Get(locker.CabinetId);
            if (selectedCabinet.IsFull())
                selectedCabinet.Restore();
        }
    }
}
