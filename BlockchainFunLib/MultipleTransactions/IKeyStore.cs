﻿namespace BlockchainFunLib.MultipleTransactions
{
    public interface IKeyStore
    {
        byte[] AuthenticatedHashKey { get; }

        string SignBlock(string blockHash);

        bool VerifyBlock(string blockHash, string signature);
    }
}