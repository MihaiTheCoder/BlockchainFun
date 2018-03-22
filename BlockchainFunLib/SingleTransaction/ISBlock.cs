using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.SingleTransaction
{
    public interface ISBlock : ITransactionDescription, IBlockHeader
    {

        string CalculateBlockHash(string previousBlockHash);

        void SetBlockHash(ISBlock parent);

        ISBlock NextBlock { get; set; }

        bool IsValidChain(string prevBlockHash, bool verbose);
    }
}
