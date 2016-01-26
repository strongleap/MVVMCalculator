using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalculator.ViewModels
{
    public class CalculationViewModel : Prism.Mvvm.BindableBase
    {
        bool hasResult = false;

        /// <summary>
        /// Backing field for the <see cref="Operator"/> property
        /// </summary>
        private IOperator @operator = default(IOperator);

        /// <summary>
        /// Backing field for the <see cref="CalculationText"/> property
        /// </summary>
        private string calculationText = default(string);

        /// <summary>
        /// Backing field for the <see cref="LeftOperand"/> property
        /// </summary>
        private int leftOperand = default(int);

        /// <summary>
        /// Backing field for the <see cref="RightOperand"/> property
        /// </summary>
        private int? rightOperand = default(int?);

        public CalculationViewModel()
        {
            this.AppendDigitCommand = new Prism.Commands.DelegateCommand<int?>(AppendDigit, value => !hasResult);
            this.SetOperatorCommand = new Prism.Commands.DelegateCommand<IOperator>(SetOperator);
            this.EvaluateCommand = new Prism.Commands.DelegateCommand(Evaluate);
            this.ClearCommand = new Prism.Commands.DelegateCommand(this.Clear);

            UpdateCalculationText();
        }

        public Prism.Commands.DelegateCommand<int?> AppendDigitCommand { get; set; }

        /// <summary>
        /// Get or sets a value that displays the calculation text for the current operation.
        /// </summary>
        public string CalculationText
        {
            get
            {
                return this.calculationText;
            }

            set
            {
                this.calculationText = value;
                this.OnPropertyChanged(() => this.CalculationText);
            }
        }

        public Prism.Commands.DelegateCommand ClearCommand { get; set; }

        public Prism.Commands.DelegateCommand EvaluateCommand { get; set; }

        /// <summary>
        /// Get or sets a value that contains the left operand for this operation.
        /// </summary>
        public int LeftOperand
        {
            get
            {
                return this.leftOperand;
            }

            set
            {
                this.leftOperand = value;
                this.OnPropertyChanged(() => LeftOperand);
            }
        }

        /// <summary>
        /// Get or sets a value that indicates something
        /// </summary>
        public IOperator Operator
        {
            get
            {
                return this.@operator;
            }

            set
            {
                this.@operator = value;
                this.OnPropertyChanged(() => this.Operator);
            }
        }

        /// <summary>
        /// Get or sets a value that contains the right hand operand for this operation.
        /// </summary>
        public int? RightOperand
        {
            get
            {
                return this.rightOperand;
            }

            set
            {
                this.rightOperand = value;
                this.OnPropertyChanged(() => this.RightOperand);
            }
        }
        public Prism.Commands.DelegateCommand<IOperator> SetOperatorCommand { get; set; }

        public void AppendDigit(int? digit)
        {
            System.Diagnostics.Debug.Assert(digit != null);

            if (this.Operator == null)
            {
                // update left
                this.LeftOperand = (this.leftOperand * 10) + digit.Value;
            }
            else
            {
                // update right
                if (this.rightOperand == null)
                {
                    this.RightOperand =  digit.Value;
                }
                else
                {
                    this.RightOperand = (this.rightOperand * 10) + digit.Value;
                }
                
            }

            UpdateCalculationText();
        }

        private void Clear()
        {
            this.leftOperand = 0;
            this.rightOperand = null;
            this.Operator = null;

            this.UpdateCalculationText();

            this.hasResult = false;
            AppendDigitCommand.RaiseCanExecuteChanged();
        }

        private void Evaluate()
        {
            this.leftOperand = this.Operator.Evaluate(this.leftOperand, this.rightOperand.Value);

            this.rightOperand = null;
            this.Operator = null;

            this.UpdateCalculationText();

            this.hasResult = true;
            AppendDigitCommand.RaiseCanExecuteChanged();
        }

        private void SetOperator(IOperator @operator)
        {
            this.Operator = @operator;

            UpdateCalculationText();

            this.hasResult = false;
            AppendDigitCommand.RaiseCanExecuteChanged();
        }
        private void UpdateCalculationText()
        {
            this.CalculationText = string.Format("{0} {1} {2}", this.leftOperand, this.Operator, this.rightOperand);
            //this.CalculationText = string.Format($"{this.leftOperand} {this.Operator} {this.rightOperand}");
        }
    }
}
