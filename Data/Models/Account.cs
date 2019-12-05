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
        public State State { get; set; }
    }
}
