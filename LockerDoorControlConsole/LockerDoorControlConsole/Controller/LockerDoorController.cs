/// <summary>
/// The class that manages and controls all functions for Locker Door.
/// The summary of the functions are: 
/// 1. Identify QR Code
/// 2. Identify Locker
/// 3. Open Locker Door
/// 4. Close Locker Door
/// </summary>

using LockerDoorControlConsole.Exceptions;
using LockerDoorControlConsole.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerDoorControlConsole.Controller
{
    class LockerDoorController
    {
        //  Private attributes
        private string _qrString;
        private string _cabinetCode;
        private Employee _employee;
        private Rental _rental;
        private Cabinet _cabinet;
        private Locker _locker;
        private SerialPort _arduinoPort;

        // Private booleans
        private bool _isPortSet = false;
        private bool _isMasterKey = false;

        public string CabinetCode { get { return _cabinetCode; } set { _cabinetCode = value; } }
        public Locker Locker { get { return _locker; } set { _locker = value; } }
        public Rental Rental { get { return _rental; } set { _rental = value; } }

        public bool IsMasterKey()
        {
            return _isMasterKey;
        }

        //  Getter and Setters
        /*public string QrString { get { return _qrString; } set { _qrString = value; } }
        */

        // Constructor
        public LockerDoorController() { }

        //  Methods
        public void ValidateQr(string qrString)
        {
            _qrString = qrString;
            _cabinet = Cabinet.Where(String.Format("code = '{0}'", _cabinetCode), 0, 1)[0];

            string rentalCondition = String.Format("rental_key ='{0}'", _qrString);
            string employeeCondition = String.Format("master_key = '{0}'", _qrString);

            // Check if the QR Code belong to any rental or employee
            List<Rental> rentals = Rental.Where(rentalCondition, 0, 1);
            List<Employee> employees = Employee.Where(employeeCondition, 0, 1);

            // If the QR Code does not belong to any rental / employee, show error
            if (!rentals.Any() && !employees.Any())
                throw new InvalidQRCodeException("Invalid QR");
            else
            {
                if (rentals.Any())
                {
                    _rental = rentals[0];
                    _isMasterKey = false;

                    if (_rental.IsEnded())
                        throw new InvalidQRCodeException("Ended QR");
                    else if (_rental.IsOverdue())
                    {
                        // Calculate the overdue days
                        TimeSpan overdueTime = DateTime.Now.Date.Subtract(_rental.EndDate.Date);
                        int overdueDays = overdueTime.Days;

                        // Get the overdue locker data
                        _locker = Locker.Get(_rental.LockerId);

                        throw new InvalidQRCodeException("Overdue QR", overdueDays.ToString(), _locker.Code);
                    }
                    else if (_rental.IsNotStarted())
                        throw new InvalidQRCodeException("Not Started QR");
                    else
                    {
                        _locker = Locker.Get(_rental.LockerId);
                        _cabinet = Cabinet.Get(_locker.CabinetId);

                        if (!_cabinet.Code.Equals(_cabinetCode))
                            throw new InvalidQRCodeException("Incorrect Cabinet");
                    }
                }
                else
                {
                    _employee = employees[0];
                    if(_employee.IsDisabled())
                        throw new InvalidQRCodeException("Deactivated Master QR");
                    else if (_employee.IsDefault())
                        throw new InvalidQRCodeException("Inactive Master QR");
                    else
                        _isMasterKey = true;
                }
            }
        }

        public void SetArduinoPort()
        {
            try
            {
                _arduinoPort = new SerialPort
                {
                    BaudRate = 9600,
                    PortName = "COM3"
                };
                _arduinoPort.Open();

                _isPortSet = true;
            }
            catch (Exception)
            {
                throw new InvalidArduinoConnectionException("Arduino Connect Fail");
            }
        }

        public void CloseArduinoPort()
        {
            _arduinoPort.Close();
        }

        public void OpenLockerDoor()
        {
            try
            {
                if (_isPortSet && _locker.Code.Equals("S-01-001"))
                {
                    // Send Open signal to Arduino
                    _arduinoPort.Write("ON");
                }
                // Set Door Status in Database to "Opened"
                _locker.DoorStatus = "Opened";
                _locker.Open();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        public void CloseLockerDoor()
        {
            try
            {
                if (_isPortSet && _locker.Code.Equals("S-01-001"))
                {
                    // Send lock signal to Arduino
                    _arduinoPort.Write("OFF");
                }

                // Set Door Status in database to "Locked"
                _locker.DoorStatus = "Locked";
                _locker.Lock();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

        }
        
        public List<Locker> GetLockers(string cabinetCode)
        {
            List<Locker> lockers = new List<Locker>();

            // Get the cabinet data with the cabinet code
            Cabinet cabinet = Cabinet.Where(String.Format("code = '{0}'", cabinetCode), 0, 1)[0];

            // Get lockers associate to that cabinet using cabinet Id
            lockers = Locker.Where(String.Format("cabinet_id = {0}", cabinet.Id), 0, 2147483467);

            return lockers;
        }

        public int GetRemainingDays()
        {
            int remainingDay = 0;

            TimeSpan timeSpan = _rental.EndDate.Date.Subtract(DateTime.Now.Date);
            remainingDay = Convert.ToInt32(timeSpan.Days);

            if (remainingDay < 0)
                remainingDay = 0;

            return remainingDay;
        }
    }
}
