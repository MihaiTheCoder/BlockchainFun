using BlockchainFunLib.SingleTransaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib
{
    public interface ISBlockChain
    {
        void AcceptBlock(ISBlock block);
        void VerifyChain();
    }
}
