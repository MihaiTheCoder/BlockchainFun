using Clifton.Blockchain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.MultipleTransactions
{
    public class MBlock : IMBlock
    {
        public List<ITransaction> Transactions { get; private set; }

        public IMBlock NextBlock { get; set; }

        public int BlockNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BlockHash { get; set; }
        public string PreviousBlockHash { get; set; }

        public IKeyStore KeyStore { get; private set; }

        public string BlockSignature { get; set; }

        private MerkleTree merkleTree = new MerkleTree();

        public int Difficulty { get; set; }

        public int Nonce { get; set; }

        public MBlock(int blockNumber)
        {
            BlockNumber = blockNumber;

            CreatedDate = DateTime.Now;

            Transactions = new List<ITransaction>();
        }

        public MBlock(int blockNumber, IKeyStore keyStore) : this(blockNumber)
        {
            KeyStore = keyStore;
        }

        public MBlock(int blockNumber, IKeyStore keyStore, int difficulty) :this(blockNumber, keyStore)
        {
            Difficulty = difficulty;
        }

        public void AddTransaction(ITransaction transaction)
        {
            Transactions.Add(transaction);
        }

        public string CalculateBlockHash(string previousBlockHash)
        {
            string blockheader = BlockNumber + CreatedDate.ToString() + previousBlockHash;
            string combined = merkleTree.RootNode + blockheader;

            if (KeyStore == null)
                return HashFun.GetSha256(combined);
            else
                return HashFun.GetHMACHash(combined, KeyStore.AuthenticatedHashKey);
        }        

        public void SetBlockHash(IMBlock parent)
        {
            if (parent != null)
            {
                PreviousBlockHash = parent.BlockHash;
                parent.NextBlock = this;
            }
            else
            {
                PreviousBlockHash = null;
            }

            BuildMerkleTree();
            (Nonce, BlockHash) = ProofOfWork.CalculateProofOfWork(CalculateBlockHash(PreviousBlockHash), Difficulty);            

            if (KeyStore != null)
                BlockSignature = KeyStore.SignBlock(BlockHash);

        }

        public bool IsValidChain(string prevBlockHash, bool verbose)
        {
            bool isValid = true;

            BuildMerkleTree();

            string newBlockHash = HashFun.GetSha256(Nonce + CalculateBlockHash(prevBlockHash));

            bool validSignature = KeyStore.VerifyBlock(newBlockHash, BlockSignature);

            if (newBlockHash != BlockHash)
                isValid = false;
            else
                isValid |= PreviousBlockHash == prevBlockHash;

            PrintVerificationMessage(verbose, isValid, validSignature);

            if (NextBlock != null)
            {
                return NextBlock.IsValidChain(newBlockHash, true);
            }

            return isValid;
        }

        private void BuildMerkleTree()
        {
            merkleTree = new MerkleTree();
            foreach (var txn in Transactions)
            {
                merkleTree.AppendLeaf(MerkleHash.Create(txn.CalculateTransactionHash()));
            }
            merkleTree.BuildTree();
        }

        private void PrintVerificationMessage(bool verbose, bool isValid, bool validSignature)
        {
            if (verbose)
            {
                if (!isValid)
                {
                    Console.WriteLine("Block Number " + BlockNumber + " : FAILED VERIFICATION");
                }
                else
                {
                    Console.WriteLine("Block Number " + BlockNumber + " : PASS VERIFICATION");
                }

                if (!validSignature)
                {
                    Console.WriteLine("Block Number " + BlockNumber + " : Invalid Digital Signature");
                }
            }
        }
    }
}
