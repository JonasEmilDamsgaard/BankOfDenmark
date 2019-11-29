using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Data.Annotations;

namespace Data.Models
{
    public class Account : INotifyPropertyChanged, IEntity
    {
        private decimal balance;
        private decimal amount;
        private State state = State.Active;
        private const int CreditLimit = -5000;

        public int Id { get; set; }

        public decimal Balance
        {
            get => balance;
            set
            {
                if (value == balance) return;
                balance = value;
                OnPropertyChanged();
            }
        }

        public decimal Amount
        {
            get => amount;
            set
            {
                if (value == amount) return;
                amount = value;
                OnPropertyChanged();
            }
        }

        public State State
        {
            get => state;
            set
            {
                if (value == state) return;
                state = value;
                OnPropertyChanged();
            }
        }

        private void UpdateState()
        {
            State = Balance >= 0 ? State.Active : State.Overdrawn;
            Amount = 0;
        }

        public void Deposit()
        {
            Balance += Amount;
            UpdateState();
        }

        public void Withdraw()
        {
            if (Balance - Amount < CreditLimit)
            {
                MessageBox.Show($"Credit limit of {CreditLimit} reached. Not possible to withdraw the amount of {Amount} from the account", "Error");
                return;
            }

            Balance -= Amount;
            UpdateState();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
