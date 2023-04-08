using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Commands
{
    public class CloseWindowCommand : CommandBase
    {
        private readonly Window _window;

        public CloseWindowCommand(Window window)
        {
            _window = window;
        }

        public override void Execute(object? parameter)
        {
            _window.Close();
        }
    }
}
