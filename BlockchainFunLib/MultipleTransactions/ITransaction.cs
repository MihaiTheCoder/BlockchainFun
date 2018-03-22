using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.MultipleTransactions
{
    public interface ITransaction : ITransactionDescription
    {
        string CalculateTransactionHash();
    }
}
