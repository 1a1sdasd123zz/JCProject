using System;

namespace Vision.Hardware
{
    public delegate void HardwareErrorEventHandler(object sender, HardwareErrorEventArgs e);
    public class HardwareErrorEventArgs : EventArgs
    {
        private byte _errorCode;

        private string _message;

        public byte ErrorCode => _errorCode;

        public string Message => _message;

        public HardwareErrorEventArgs(byte errorCode)
        {
            _errorCode = errorCode;
            _message = Bv_HardwareError.Errors[_errorCode];
        }
    }
}
