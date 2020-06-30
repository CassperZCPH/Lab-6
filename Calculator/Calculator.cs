using System;

namespace Calculator
{
    enum Operation { Add, Div, Sub, Mul };

    delegate void CalculatorDidUpdateOutput(Calculator sender, double value, int precision);

    class Calculator
    {
        double? left = null;
        double? right = null;
        Operation? currentOp = null;
        bool decimalPoint = false;
        int precision = 0;

        public event CalculatorDidUpdateOutput DidUpdateValue;
        public event EventHandler<string> InputError;
        public event EventHandler<string> CalculationError;

        public void AddDigit(int digit)
        {
            if (left.HasValue && Math.Log10(left.Value) > 10)
            {
                InputError?.Invoke(this, "Input overflow");
                return;
            }

            if (!currentOp.HasValue)
            {
                right = null;
            }

            if (!decimalPoint)
            {
                left = (left ?? 0) * 10 + digit;
            }
            else
            {
                precision++;
            }

            DidUpdateValue?.Invoke(this, left.Value, precision);
        }

        public void AddDecimalPoint()
        {
            decimalPoint = true;
        }

        public void AddOperation(Operation op)
        {
            if (left.HasValue && right.HasValue && currentOp.HasValue)
            {
                Compute();
            }
            if (!left.HasValue)
            {
                right = 0;
                precision = 0;
            }
            if (!right.HasValue)
            {
                right = left;
                left = 0;
                precision = 0;
            }
            currentOp = op;
        }

        public void Compute()
        {
            if (!(left.HasValue && currentOp.HasValue))
            {
                return;
            }

            switch (currentOp)
            {
                case Operation.Add:
                    right = right + left;
                    break;
                case Operation.Sub:
                    right = right - left;
                    break;
                case Operation.Mul:
                    right = right * left;
                    break;
                case Operation.Div:
                    if (left == 0)
                    {
                        CalculationError?.Invoke(this, "Division by 0!");
                        return;
                    }
                    right = right / left;
                    break;
            }

            DidUpdateValue?.Invoke(this, right.Value, precision);
            left = 0;
            currentOp = null;
        }

        public void Reverce()
        {
            if (left.HasValue && left != 0)
            {
                left *= -1;
            }
            DidUpdateValue?.Invoke(this, left.Value, precision);
        }

        public void SQRT()
        {
            if (left.HasValue && left != 0)
            {
                left = Math.Sqrt((double)left);
            }
            DidUpdateValue?.Invoke(this, left.Value, precision);
        }

        public void Reset() 
        {
            left = 0;
            right = null;
            currentOp = null;
            precision = 0;
            DidUpdateValue?.Invoke(this, left.Value, precision);
        }

        public void Clear()
        {
            left = 0;
            DidUpdateValue?.Invoke(this, left.Value, precision);
        }
    }
}
