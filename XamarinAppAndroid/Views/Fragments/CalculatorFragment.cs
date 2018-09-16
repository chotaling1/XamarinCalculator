using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using XamarinAppShared;
using XamarinAppShared.BusinessObjects;
namespace XamarinApp.Views.Fragments.CalculatorFragment
{
    public class CalculatorFragment : Fragment
    {
        private View view = null;
        private CalculatorService calculatorService = new CalculatorService();
        private Calculator calculator = new Calculator();

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.calculator_fragment, container, false);
            AssignEventHandlers();
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        private void AssignEventHandlers()
        {
            Button button0 = view.FindViewById(Resource.Id.button0) as Button;
            button0.Click += OnNumberButtonPressed;
            Button button1 = view.FindViewById(Resource.Id.button1) as Button;
            button1.Click += OnNumberButtonPressed;
            Button button2 = view.FindViewById(Resource.Id.button2) as Button;
            button2.Click += OnNumberButtonPressed;
            Button button3 = view.FindViewById(Resource.Id.button3) as Button;
            button3.Click += OnNumberButtonPressed;
            Button button4 = view.FindViewById(Resource.Id.button4) as Button;
            button4.Click += OnNumberButtonPressed;
            Button button5 = view.FindViewById(Resource.Id.button5) as Button;
            button5.Click += OnNumberButtonPressed;
            Button button6 = view.FindViewById(Resource.Id.button6) as Button;
            button6.Click += OnNumberButtonPressed;
            Button button7 = view.FindViewById(Resource.Id.button7) as Button;
            button7.Click += OnNumberButtonPressed;
            Button button8 = view.FindViewById(Resource.Id.button8) as Button;
            button8.Click += OnNumberButtonPressed;
            Button button9 = view.FindViewById(Resource.Id.button9) as Button;
            button9.Click += OnNumberButtonPressed;

            Button buttonAdd = view.FindViewById(Resource.Id.buttonAdd) as Button;
            buttonAdd.Click += OnAddButtonPressed;
            Button buttonSubtract = view.FindViewById(Resource.Id.buttonSubtract) as Button;
            buttonSubtract.Click += OnSubtractButtonPressed;
            Button buttonMultiply = view.FindViewById(Resource.Id.buttonMultiply) as Button;
            buttonMultiply.Click += OnMultiplyButtonPressed;
            Button buttonDivide = view.FindViewById(Resource.Id.buttonDivide) as Button;
            buttonDivide.Click += OnDivideButtonPressed;

            Button buttonEnter = view.FindViewById(Resource.Id.buttonEnter) as Button;
            buttonEnter.Click += OnEnterButtonPressed;
            Button buttonClear = view.FindViewById(Resource.Id.buttonClear) as Button;
            buttonClear.Click += OnClearButtonPressed;
            Button buttonBack = view.FindViewById(Resource.Id.buttonBack) as Button;
            buttonBack.Click += OnBackButtonPressed;
            Button buttonRNG = view.FindViewById(Resource.Id.buttonRNG) as Button;
            buttonRNG.Click += OnRNGButtonPressed;
            Button buttonPalindrome = view.FindViewById(Resource.Id.buttonPalindrome) as Button;
            buttonPalindrome.Click += OnPalindromeButtonPressed;
            Button buttonQuickSort = view.FindViewById(Resource.Id.buttonQuickSort) as Button;
            buttonQuickSort.Click += OnQuickSortButtonPressed;

            calculator.OnInputBufferChanged += UpdateInputBufferEvent;
            calculator.OnCurrentInputChanged += UpdateCurrentInputEvent;
            calculator.OnCurrentInputOverflow += CurrentInputOverflowEvent;
        }

        #region Number Buttons
        private void OnNumberButtonPressed(object sender, EventArgs e)
        {
            Button numberButton = sender as Button;
            char input = numberButton.Text.ToCharArray()[0];
            calculatorService.NumberAction(input, calculator);
        }
        #endregion

        #region Function Buttons
        private void OnAddButtonPressed(object sender, EventArgs e)
        {
            calculatorService.AddAction(calculator);
        }

        private void OnSubtractButtonPressed(object sender, EventArgs e)
        {
            calculatorService.SubtractAction(calculator);
        }

        private void OnMultiplyButtonPressed(object sender, EventArgs e)
        {
            calculatorService.MultiplyAction(calculator);
        }

        private void OnDivideButtonPressed(object sender, EventArgs e)
        {
            calculatorService.DivideAction(calculator);
        }

        private void OnClearButtonPressed(object sender, EventArgs e)
        {
            calculatorService.ClearAction(calculator);
        }

        private void OnEnterButtonPressed(object sender, EventArgs e)
        {
            calculatorService.EnterAction(calculator);
        }

        private void OnBackButtonPressed(object sender, EventArgs e)
        {
            calculatorService.BackAction(calculator);
        }

        private void OnQuickSortButtonPressed(object sender, EventArgs e)
        {
            calculatorService.QuickSortAction(calculator);
        }

        private void OnRNGButtonPressed(object sender, EventArgs e)
        {
            calculatorService.RNGAction(calculator);
        }

        private void OnPalindromeButtonPressed(object sender, EventArgs e)
        {
            ShowPalindromeToast(calculatorService.PalindromeAction(calculator));
        }
        #endregion

        #region Events
        private void CurrentInputOverflowEvent(object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this.Activity);
            alert.SetTitle("Error");
            alert.SetMessage("Input Overflow");
            alert.SetPositiveButton("Ok", (IDialogInterfaceOnClickListener)null);
            alert.Show();
            calculator.CachedValue = 0;
            calculator.CurrentInput = "0";
            calculator.InputBuffer = null;
        }
        private void UpdateInputBufferEvent(object sender, EventArgs e)
        {
            TextView InputBuffer = view.FindViewById(Resource.Id.InputBufferText) as TextView;
            InputBuffer.Text = calculator.InputBuffer;
        }

        private void UpdateCurrentInputEvent(object sender, EventArgs e)
        {
            TextView currentInputText = view.FindViewById(Resource.Id.CurrentInputText) as TextView;
            currentInputText.Text = calculator.CurrentInput;
        }
        #endregion

        private void ShowPalindromeToast(bool isPalindrome)
        {
            String message = "";
            if (isPalindrome)
            {
                message = String.Format("{0} is a palindrome.", calculator.CurrentInput);
            }
            else
            {
                message = String.Format("{0} is not a palindrome.", calculator.CurrentInput);
            }

            Toast.MakeText(this.Activity, message, ToastLength.Long).Show();
        }
    }
}