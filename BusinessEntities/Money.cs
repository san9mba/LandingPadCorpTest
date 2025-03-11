using System;

namespace BusinessEntities
{
    public readonly struct Money
    {
        public static string DefaultCurrency = "USD";

        public decimal Amount { get; }
        public string Currency { get; }

        public Money(Money amount)
        {
            if (amount.Amount < 0) throw new ArgumentException("Amount cannot be negative.");
            if (string.IsNullOrWhiteSpace(amount.Currency)) throw new ArgumentException("Currency is required.");

            Amount = amount.Amount;
            Currency = amount.Currency;
        }
        public Money(decimal amount) : this(amount, DefaultCurrency)
        {
        }
        public Money(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required.");

            Amount = amount;
            Currency = currency;
        }


        public override bool Equals(object obj)
        {
            if (obj is Money money)
            {
                return Amount == money.Amount && Currency == money.Currency;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Amount.GetHashCode();
                hash = hash * 31 + (Currency?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot add money in different currencies.");

            return new Money(a.Amount + b.Amount, a.Currency);
        }

        public static Money operator *(Money money, int quantity)
        {
            return new Money(money.Amount * quantity, money.Currency);
        }

        public override string ToString() => $"{Amount} {Currency}";
    }
}
