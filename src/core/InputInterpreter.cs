using System.Runtime.CompilerServices;

namespace csmic
{
    public class InputInterpreter
    {
        #region Members

        private decimal numericValue = 0;
        private string stringValue = string.Empty;

        #endregion

        #region Properties

        public decimal NumericValue
        {
            get
            {
                return numericValue;
            }
        }

        public string StringValue
        {
            get
            {
                return stringValue;
            }
        }

        #endregion

        #region Methods

        internal void ProduceOutput(decimal numericValue, string stringValue)
        {
            this.numericValue = numericValue;
            this.stringValue = stringValue;
        }

        internal void ProduceOutput(FunctionValue functionValue)
        {
            switch (functionValue.Type)
            {
                case ValueType.Numeric:
                    decimal numericValue = Convert.ToDecimal(functionValue.Value);
                    ProduceOutput(numericValue, string.Empty);
                    break;
                case ValueType.String:
                    if (functionValue.Value != null && functionValue.Value is string)
                    {
                        ProduceOutput(0, functionValue.Value!.ToString());
                    }
                    else
                    {
                        ProduceOutput(0, string.Empty);
                    }
                    break;
                case ValueType.None:
                default:
                    ProduceOutput(0, string.Empty);
                    break;
            }
        }

        #endregion
    }
}
