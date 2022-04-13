using Hotwire_Transient_GUI.Code.Events;
using Hotwire_Transient_GUI.Modals;
using System;
using System.IO.Ports;
using System.Windows;

namespace Hotwire_Transient_GUI.Code
{
    public class HotWireSerialCom
    {
        private SerialPort _port;
        public SerialPort Port
        {
            get { return _port; }
            set 
            {
                if (_port != null)
                {
                    _port.Close();
                }
                

                _port = value;
                _port.BaudRate = 115200;
                _port.DataBits = 8;
                _port.StopBits = StopBits.One;
                _port.Parity = Parity.None;
                _port.Open();
                _port.DataReceived += readData;
            }
        }
        private HotWireTest hotWireTest;
        private SingleHotWireTest CalibrationData;
        private bool receivingTest = false;
        private bool receivingCalibration = true;
        private int currentTest;
        private int sampleNum;
        private string line;
        private string[] splitLine;
        //private double expectedXValue;
        private double xValue;
        private double yValue;
        private double thermocoupleValue;
        public event EventHandler<NewTestEventArgs> NewTest;
        public event EventHandler<CalibrationDataEventArgs> CalibrationDataReceived;
        public event EventHandler<TestProgressEventArgs> TestProgress;
        public event EventHandler<ShowDialogEventArgs> ShowWindow;

        private ReceivingTestModal receivingTestModal;

        public const double wireResistance = 1;
        public const double alpha = 0.00393;
        public const double refTemp = 20;
        public const double gain = 51;
        public const double current = 0.015;

        public void readData(object sender, SerialDataReceivedEventArgs e)
        {
            line = Port.ReadLine();
            line = line.ToLower();
            //Filter out random junk
            line = line.Replace(" ", string.Empty);
            line = line.Replace("\r", string.Empty);
            line = line.Replace("\0", string.Empty);
            if (!receivingTest || !receivingCalibration)
            {
                //Line sent from UART to make a new test profile
                if (line == "start")
                {
                    initReceiveTest();
                }
                //ReceiveCalibrationData
                else if (line == "calibration")
                {
                    initReceiveCalibration();
                }
            }
            else if(receivingTest)
            {
                //Marks end of transmission of a test iteration
                if (line == "endsingle")
                {
                    receiveNextTest();
                }
                //Marks end of all test iterations
                else if (line == "endtest")
                {
                    receiveTestEnd();
                }
                else if (line == "cancel")
                {
                    receivingTest = false;
                    //Raise Cancel Event
                }
                //Otherwise it's a data point, coming in the form of "time, wire temperature, thermocouple value"
                else
                {
                    receiveData();
                }
            }
            else if (receivingCalibration)
            {
                if(line == "endcalibrationdata")
                {
                    endReceiveCalibration();
                }
                //else if(line == "endcalibration")
                //{

                //}
                else
                {
                    receiveCalibrationData();
                }
            }

        }

        #region Receive Calibration

        private void receiveCalibrationData()
        {
            splitLine = line.Split(',');
            if (splitLine.Length == 2)
            {
                if (!double.TryParse(splitLine[0], out xValue))
                {
                    xValue = double.NaN;
                }
                if (!double.TryParse(splitLine[1], out yValue))
                {
                    yValue = double.NaN;
                }
                //Vibe check
                if (!(xValue == double.NaN || xValue == double.PositiveInfinity || xValue == double.NegativeInfinity || xValue <= 0))
                {
                    if (!(yValue == double.NaN || yValue == double.PositiveInfinity || yValue == double.NegativeInfinity))
                    {
                        yValue = ((yValue / gain) / current / wireResistance - 1) / alpha + refTemp;
                        CalibrationData.Data.Add(new Point(xValue, yValue, sampleNum));
                    }
                }
            }
            else if (splitLine.Length == 3)
            {
                if (double.TryParse(splitLine[0], out xValue))
                {
                    xValue = double.NaN;
                }
                if (!double.TryParse(splitLine[1], out yValue))
                {
                    yValue = double.NaN;
                }
                if (!double.TryParse(splitLine[1], out thermocoupleValue))
                {
                    thermocoupleValue = double.NaN;
                }

                if (!(xValue == double.NaN || xValue == double.PositiveInfinity || xValue == double.NegativeInfinity))
                {
                    if (!(yValue == double.NaN || yValue == double.PositiveInfinity || yValue == double.NegativeInfinity))
                    {
                        yValue = ((yValue / gain) / current / wireResistance - 1) / alpha + refTemp;
                        CalibrationData.Data.Add(new Point(xValue, yValue, sampleNum));
                    }
                }
            }
            sampleNum++;
        }

        private void initReceiveCalibration()
        {
            receivingCalibration = true;
            CalibrationData = new SingleHotWireTest();

            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                receivingTestModal = new ReceivingTestModal();
                receivingTestModal.TotalTests = 1;
                receivingTestModal.CompletedTests = 0;
                this.TestProgress += receivingTestModal.TestUpdated;
                TestProgress(this, new TestProgressEventArgs(1, 1));
                OnShowWindow(this, new ShowDialogEventArgs(receivingTestModal, false));
            });

        }

        private void endReceiveCalibration()
        {
            TestProgress(this, new TestProgressEventArgs(1, 1));
            Application.Current.Dispatcher.Invoke((Action)delegate {
                TestProgress(this, new TestProgressEventArgs(-1, -1));
                OnCalibrationDataReceived(this, new CalibrationDataEventArgs(CalibrationData));
            });
            receivingCalibration = false;
        }

        #endregion

        #region Receive Test
        private void receiveData()
        {
            splitLine = line.Split(',');
            if (splitLine.Length == 2)
            {
                if (!double.TryParse(splitLine[0], out xValue))
                {
                    xValue = double.NaN;
                }
                if (!double.TryParse(splitLine[1], out yValue))
                {
                    yValue = double.NaN;
                }
                //Vibe check
                if (!(xValue == double.NaN || xValue == double.PositiveInfinity || xValue == double.NegativeInfinity || xValue <= 0))
                {
                    if (!(yValue == double.NaN || yValue == double.PositiveInfinity || yValue == double.NegativeInfinity))
                    {
                        yValue = ((yValue / gain) / current / wireResistance - 1) / alpha + refTemp;
                        hotWireTest.Tests[currentTest].Data.Add(new Point(xValue, yValue, sampleNum));
                    }
                }
            }
            else if (splitLine.Length == 3)
            {
                if (double.TryParse(splitLine[0], out xValue))
                {
                    xValue = double.NaN;
                }
                if (!double.TryParse(splitLine[1], out yValue))
                {
                    yValue = double.NaN;
                }
                if (!double.TryParse(splitLine[1], out thermocoupleValue))
                {
                    thermocoupleValue = double.NaN;
                }

                if (!(xValue == double.NaN || xValue == double.PositiveInfinity || xValue == double.NegativeInfinity))
                {
                    if (!(yValue == double.NaN || yValue == double.PositiveInfinity || yValue == double.NegativeInfinity))
                    {
                        yValue = ((yValue / gain) / current / wireResistance - 1) / alpha + refTemp;
                        hotWireTest.Tests[currentTest].Data.Add(new Point(xValue, yValue, sampleNum));
                    }
                }
            }
            sampleNum++;
        }

        private void receiveNextTest()
        {
            //Illegal wpf threading hack
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                OnTestProgress(this, new TestProgressEventArgs(currentTest + 1, hotWireTest.Tests.Count));
            });
            currentTest++;
            sampleNum = 0;
        }

        private void receiveTestEnd()
        {
            receivingTest = false;
            currentTest = 0;
            sampleNum = 0;

            //Close the window
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                OnTestProgress(this, new TestProgressEventArgs(-1, -1));
            });

            //Do regression analysis and calculate error for all tests
            for (int i = 0; i < hotWireTest.Tests.Count; i++)
            {
                hotWireTest.Tests[i].LinearRegression();
                hotWireTest.Tests[i].CalculateError();
            }
            hotWireTest.CreateAverageofTests();
            Application.Current.Dispatcher.Invoke((Action)delegate {
                OnNewTest(new NewTestEventArgs(hotWireTest));
            });
            //Raise Event
        }

        private void initReceiveTest()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                receivingTestModal = new ReceivingTestModal();
                this.TestProgress += receivingTestModal.TestUpdated;
                OnShowWindow(this, new ShowDialogEventArgs(receivingTestModal, false));
            });
            hotWireTest = new HotWireTest();
            hotWireTest.Date = DateTime.Now;
            hotWireTest.Material = "Unspecified";
            currentTest = 0;
            sampleNum = 0;
            receivingTest = true;
        }

        #endregion

        #region Commands
        public bool StartTest()
        {
            if(!Port.IsOpen) { return false; }

            Port.WriteLine("start");

            return true;
        }


        public bool StartCalibration(double tolerance)
        {
            if (!Port.IsOpen) { return false; }

            Port.WriteLine("calibrate");
            Port.WriteLine(tolerance.ToString());// get tolerance to here somehow
            return true;
        }

        #endregion
        protected virtual void OnNewTest(NewTestEventArgs e)
        {
            EventHandler<NewTestEventArgs> handler = NewTest;
            handler?.Invoke(this, e);
        }

        protected virtual void OnShowWindow(object sender, ShowDialogEventArgs e)
        {
            EventHandler<ShowDialogEventArgs> handler = ShowWindow;
            handler?.Invoke(this, e);
        }

        protected virtual void OnTestProgress(object sender, TestProgressEventArgs e)
        {
            EventHandler <TestProgressEventArgs> handler = TestProgress;
            handler?.Invoke(this, e);
        }

        protected virtual void OnCalibrationDataReceived(object sender, CalibrationDataEventArgs e)
        {
            EventHandler<CalibrationDataEventArgs> handler = CalibrationDataReceived;
            handler?.Invoke(this, e);
        }
    }
}
