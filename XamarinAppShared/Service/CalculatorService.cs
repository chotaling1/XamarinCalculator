using System;
using System.Text;
using XamarinAppShared.BusinessObjects;

namespace XamarinAppShared
{
    public class CalculatorService
    {
        public String DoAdd(Calculator calculator)
        {
            return (calculator.CachedValue + calculator.GetCurrentValueAsDecimal()).ToString();
        }

        public String DoSubtract(Calculator calculator)
        {
            return (calculator.CachedValue - calculator.GetCurrentValueAsDecimal()).ToString();
        }

        public String DoMultiply(Calculator calculator)
        {
            return (calculator.CachedValue * calculator.GetCurrentValueAsDecimal()).ToString();
        }

        public String DoDivide(Calculator calculator)
        {
            return (calculator.CachedValue / calculator.GetCurrentValueAsDecimal()).ToString();
        }

        public string DoStoredOperation(Calculator calculator)
        {
            string currentValue = "0";
            switch (calculator.StoredOperation)
            {
                case "Add":
                    currentValue = DoAdd(calculator);
                    break;
                case "Subtract":
                    currentValue = DoSubtract(calculator);
                    break;
                case "Multiply":
                    currentValue = DoMultiply(calculator);
                    break;
                case "Divide":
                    currentValue = DoDivide(calculator);
                    break;
                default:
                    currentValue = calculator.CurrentInput;
                    break;
            }

            calculator.CachedValue = Convert.ToDecimal(currentValue);
            calculator.StoredOperation = null;
            return currentValue;
        }

        public void AppendCurrentValue(char input, Calculator calculator)
        {
            if (calculator.CurrentInput.Equals("0"))
            {
                calculator.CurrentInput = input.ToString();
            }
            else
            {
                StringBuilder builder = new StringBuilder(calculator.CurrentInput);
                builder.Append(input);
                calculator.CurrentInput = builder.ToString();
            }
        }

        #region Input Actions
        public void NumberAction(char input, Calculator calculator)
        {
            calculator.InputBuffer += input;
            AppendCurrentValue(input, calculator);

        }

        public void AddAction(Calculator calculator)
        {
            DoStoredOperation(calculator);
            calculator.StoredOperation = "Add";
            calculator.InputBuffer += " + ";
            calculator.CurrentInput = "0";
        }

        public void SubtractAction(Calculator calculator)
        {
            DoStoredOperation(calculator);
            calculator.StoredOperation = "Subtract";
            calculator.InputBuffer += " - ";
            calculator.CurrentInput = "0";
        }

        public void MultiplyAction(Calculator calculator)
        {
            DoStoredOperation(calculator);
            calculator.StoredOperation = "Multiply";
            calculator.InputBuffer += " * ";
            calculator.CurrentInput = "0";
        }

        public void DivideAction(Calculator calculator)
        {
            DoStoredOperation(calculator);
            calculator.StoredOperation = "Divide";
            calculator.InputBuffer += " / ";
            calculator.CurrentInput = "0";
        }

        public void ClearAction(Calculator calculator)
        {
            calculator.CachedValue = 0;
            calculator.StoredOperation = null;
            calculator.InputBuffer = null;
            calculator.CurrentInput = "0";
        }

        public void EnterAction(Calculator calculator)
        {
            string currentValue = DoStoredOperation(calculator);
            calculator.CurrentInput = currentValue;
            calculator.InputBuffer += String.Format(" = {0} ", currentValue);
        }
        
        public void BackAction(Calculator calculator)
        {
            string currentValue = calculator.CurrentInput;
            if (!currentValue.Equals("0"))
            {
                if (currentValue.Length > 1)
                {
                    currentValue = currentValue.Remove(currentValue.Length - 1);
                    calculator.CurrentInput = currentValue;
                }
                else if (currentValue.Length == 1)
                {
                    calculator.CurrentInput = "0";
                }

                string inputBuffer = calculator.InputBuffer;
                inputBuffer = inputBuffer.Remove(inputBuffer.Length - 1);
                calculator.InputBuffer = inputBuffer;
            }
        }

        public void RNGAction(Calculator calculator)
        {
            Random rnd = new Random();
            int rng = rnd.Next(0, 999999999);
            calculator.CurrentInput = rng.ToString();
            calculator.InputBuffer = rng.ToString() ;
        }

        public bool PalindromeAction(Calculator calculator)
        {
            int length = calculator.CurrentInput.Length;
            if (length == 1)
                return true;

            bool isEven = length % 2 == 0;
            string firstHalfOfString = String.Empty;
            string secondHalfOfString = String.Empty;
            int substringLength = 0;
            substringLength = length / 2;
            firstHalfOfString = calculator.CurrentInput.Substring(0, substringLength);
            if (isEven)
            {
                firstHalfOfString = calculator.CurrentInput.Substring(0, substringLength);
                for (int i = length - 1; i >= substringLength; i--)
                {
                    secondHalfOfString += calculator.CurrentInput[i];
                }
            }
            else
            {
                for (int i = length - 1; i > substringLength; i--)
                {
                    secondHalfOfString += calculator.CurrentInput[i];
                }
            }

            return firstHalfOfString.Equals(secondHalfOfString);
        }

        public void QuickSortAction(Calculator calculator)
        {
            string currentInput = calculator.CurrentInput;

            int[] arr = new int[currentInput.Length];
           
            for (int i = 0; i < currentInput.Length; i++)
            {
                arr[i] = currentInput[i] - '0';
            }

            QuickSort(arr, 0, arr.Length - 1);

            calculator.CurrentInput = String.Join("", arr);
        }

        private void QuickSort(int[] arr, int left, int right)
        {
            if (left >= right)
                return;

            int pivot = arr[(left + right) / 2];
            int index = Partition(arr, left, right, pivot);
            QuickSort(arr, left, index - 1);
            QuickSort(arr, index, right);
        }

        private int Partition(int [] arr, int left, int right, int pivot)
        {
            while (left <= right)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left <= right)
                {
                    Swap(arr, left, right);
                    left++;
                    right--;
                }
            }

            return left;
        }

        private void Swap(int [] arr, int left, int right)
        {
            int temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;
        }

        #endregion
    }
}
