using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Data.Annotations;

namespace Data.Models
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public State state { get; set; }
       
        public void Deposit()
        {
            Balance += Amount;
        }

        public void Withdraw()
        {
            Balance -= Amount;
        }
    }
}
