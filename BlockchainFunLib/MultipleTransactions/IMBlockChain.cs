using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.MultipleTransactions
{
    public interface IMBlockChain
    {
        void AcceptBlock(IMBlock block);
        void VerifyChain();
    }
}
