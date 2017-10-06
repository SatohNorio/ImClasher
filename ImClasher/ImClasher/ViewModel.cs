using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImClasher
{
	class ViewModel : DependencyObject, INotifyPropertyChanged
	{
		public int MemorySize
		{
			get { return (int)GetValue(MemorySizeProperty); }
			set { SetValue(MemorySizeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for MemorySize.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MemorySizeProperty =
			DependencyProperty.Register("MemorySize", typeof(int), typeof(ViewModel), new PropertyMetadata(0));

		public event PropertyChangedEventHandler PropertyChanged;

		private ICommand FAllocateCommand;
		public ICommand AllocateCommand
		{
			get
			{
				return this.FAllocateCommand = this.FAllocateCommand ?? new DelegateCommand(() =>
				{
					if (0 < this.MemorySize)
					{
						this.FKeepMemory = new int[this.MemorySize * 1024 * 1024];
					}
				});
			}
		}

		private object FKeepMemory;

		private ICommand FReleaseCommand;
		public ICommand ReleaseCommand
		{
			get
			{
				return this.FReleaseCommand = this.FReleaseCommand ?? new DelegateCommand(() =>
				{
					this.FKeepMemory = null;
				});
			}
		}
	}

	class DelegateCommand : ICommand
	{
		public DelegateCommand(Action act)
		{
			this.FAction = act;
		}

		private Action FAction;
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			this.FAction();
		}
	}
}
