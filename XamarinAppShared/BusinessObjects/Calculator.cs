using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinAppShared.BusinessObjects
{
    public class Calculator
    {
        
        public decimal CachedValue { get; set; }
        public String StoredOperation { get; set; }

        private string _InputBuffer;
        public event EventHandler OnInputBufferChanged;
        public String InputBuffer
        {
            get { return _InputBuffer; }
            set
            {
                _InputBuffer = value;
                OnInputBufferChanged?.Invoke(this, null);
            }
        }

        private string _CurrentInput = "0";
        public event EventHandler OnCurrentInputChanged;
        public event EventHandler OnCurrentInputOverflow;
        public String CurrentInput
        {
            get { return _CurrentInput; }
            set
            {
                if (value.Length <= 9)
                {
                    _CurrentInput = value;
                    OnCurrentInputChanged?.Invoke(this, null);
                }
                else
                {
                    OnCurrentInputOverflow?.Invoke(this, null);
                }
                
                
            }
        }

        public decimal GetCurrentValueAsDecimal()
        {
            return Convert.ToDecimal(CurrentInput);
        }
    }
}
