using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.MultipleTransactions
{
    public interface IMBlock : IBlockHeader
    {
        string BlockSignature { get; }

        List<ITransaction> Transactions { get; }

        void AddTransaction(ITransaction transaction);

        string CalculateBlockHash(string previousBlockHash);

        void SetBlockHash(IMBlock parent);

        IMBlock NextBlock { get; set; }

        bool IsValidChain(string prevBlockHash, bool verbose);

        IKeyStore KeyStore { get; }

    }
}
