using ClosedXML.Excel;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.Controller
{
    public class RentalController
    {
        // Private Attributes
        private Rental _rental = new Rental();

        // Constructor
        public RentalController() { }

        // Methods
        // Method to calculate change
        public decimal CalculateChange(decimal totalPrice, decimal payment)
        {
            decimal change = payment - totalPrice;

            if (change < 0)
                throw new InvalidUserInputException("Insufficient Payment");

            return change;
        }

        // Mrthod to check rental duration
        public void CheckRentalDuration(int duration)
        {
            if (duration <= 0)
                throw new InvalidUserInputException("Invalid Duration");
        }

        // Method to set the rental
        public void SetAddRentalData(Customer customer, Employee employee, DateTime startDate,
            DateTime endDate, int duration)
        {
            if (customer.Id == 0)
                throw new InvalidUserInputException("Empty Customer");

            _rental.StartDate = startDate;
            _rental.EndDate = endDate;
            _rental.Duration = duration;
            _rental.CustomerId = customer.Id;
            _rental.EmployeeId = employee.Id;
        }

        public void SetAddRentalLockerData(Locker locker)
        {
            if (locker.Id == 0)
                throw new InvalidUserInputException("Empty Locker");

            _rental.LockerId = locker.Id;
        }

        // Method to set the Rental Payment
        public void SetPayRentalData(string code, DateTime bookingDateTime, string rentalKey)
        {
            _rental.Code = code;
            _rental.BookingDateTime = bookingDateTime;
            _rental.Key = rentalKey;
            _rental.Status = "Not Started";
        }

        // Method to set end rental data
        public void SetEndRentalData(Rental rental)
        {
            _rental = rental;
        }

        // Method to generate QR Code using the Rental Key
        public Bitmap GenerateQR(string input)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return qrCodeImage;
        }

        // Method to Save the Rental Data
        public void SaveRentalData()
        {
            _rental.Save();
        }

        // Method to End the Rental;
        public void EndRental()
        {
            // End the rental
            _rental.End();

            // Set the Locker to Available
            Locker locker = Locker.Get(_rental.LockerId);
            locker.Reset();

            // Check is the cabinet full, if yes, set it as available
            Cabinet cabinet = Cabinet.Get(locker.CabinetId);

            if (cabinet.IsFull())
            {
                cabinet.Restore();
            }

        }

        // Method to validate the booking date range of rentals to be exported
        public void CheckExportDate(DateTime fromDate, DateTime untilDate)
        {
            if (fromDate.Date.CompareTo(untilDate.Date) > 0)
                throw new InvalidUserInputException("Invalid From Until Date");

            if (fromDate.Date.CompareTo(DateTime.Now.Date) == 0 || untilDate.Date.CompareTo(DateTime.Now.Date) == 0)
                throw new InvalidUserInputException("Export Today Date");
        }

        // Method to export the ended rental data
        public void ExportRentalData(DateTime fromDate, DateTime untilDate)
        {
            string startDate = fromDate.ToString("dd-MM-yyyy");
            string endDate = untilDate.ToString("dd-MM-yyyy");

            string defaultFileName = String.Format("EXPORT_RENTAL_{0}_{1}",
                String.Format("{0}~{1}", startDate, endDate), DateTime.Now.ToString("ddMMyyyy_HHmmss"));

            string dbStartDate = fromDate.ToString("yyyy-MM-dd");
            string dbEndDate = untilDate.ToString("yyyy-MM-dd");
            string dateCond = String.Format("status = 'Ended' AND DATE(booking_date_time) BETWEEN '{0}' AND '{1}'", dbStartDate, dbEndDate);

            List<Rental> rentals = Rental.Where(dateCond, 0, 2147483647);

            var workbook = new XLWorkbook();
            var ws = workbook.AddWorksheet("Ended Rental");
            ws.Cell(1, 1).Value = "Rental";
            ws.Cell(2, 1).InsertTable(rentals);

            SaveFileDialog sf = new SaveFileDialog
            {
                FileName = defaultFileName,
                Filter = "Excel Workbook (.xlsx) |*.xlsx",
                Title = "Export Rental as",
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

                    foreach (var item in rentals)
                        item.Delete();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    throw new InvalidUserInputException("Export Fail", "", "", "rental");
                }
            }
            else
                throw new InvalidUserInputException("Export Fail", "", "", "rental");
        }

        // Method to update the locker of the rental if changed
        public void ChangeBookedLocker(Rental rental, Locker previousLocker)
        {
            Locker locker = Locker.Get(rental.LockerId);

            // Change the locker of the rental
            rental.ChangeLocker();

            // Check if the rental is a started rental.
            if (rental.IsStarted())
            {
                // If yes, release the previous locker
                previousLocker.Reset();

                // Occupy the new locker
                locker.Occupied();

                // Check if the new cabinet is full, if yes set to full
                string lockerSearchCondition = String.Format("cabinet_id = {0} AND status = 'Available'", locker.CabinetId);
                int noOfEmptyLocker = Locker.Count(lockerSearchCondition);
                if (noOfEmptyLocker <= 0)
                {
                    Cabinet cabinet = Cabinet.Get(locker.CabinetId);
                    cabinet.Full();
                }

                // Check is the old cabinet full. If yes, set it as available
                Cabinet previousCabinet = Cabinet.Get(previousLocker.CabinetId);
                if (previousCabinet.IsFull())
                    previousCabinet.Restore();
            }
        }
    }
}
