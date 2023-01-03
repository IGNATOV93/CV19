using CV19.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Infrastructure.Commands
{
    internal class LambdaCommand:Command
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;
        public LambdaCommand(Action<object> Execute,Func<object,bool> CanExecute=null)
        {
            _Execute = Execute ?? throw new ArgumentException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;
        public override void Execute(object parameter) => _Execute(parameter);
       


    }
}
