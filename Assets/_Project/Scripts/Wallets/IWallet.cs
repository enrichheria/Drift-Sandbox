namespace Wallets
{
    public interface IWallet
    {
        public float CurrentValue { get; }

        public void Add(int value);
        public void Spend(int value);
    }
}
