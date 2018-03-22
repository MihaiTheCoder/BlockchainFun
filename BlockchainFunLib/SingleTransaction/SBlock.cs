using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.SingleTransaction
{
    public class SBlock : ISBlock
    {
        public ISBlock NextBlock { get; set; }
        public string ClaimNumber { get; set; }
        public decimal SettlementAmount { get; set; }
        public DateTime SettlementDate { get; set; }
        public string CarRegistration { get; set; }
        public int Mileage { get; set; }
        public ClaimType ClaimType { get; set; }
        public int BlockNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BlockHash { get; set; }
        public string PreviousBlockHash { get; set; }

        public SBlock(int blockNumber,
                     string claimNumber,
                     decimal settlementAmount,
                     DateTime settlementDate,
                     string carRegistration,
                     int mileage,
                     ClaimType claimType,
                     ISBlock parent)
        {
            BlockNumber = blockNumber;
            ClaimNumber = claimNumber;
            SettlementAmount = settlementAmount;
            SettlementDate = settlementDate;
            CarRegistration = carRegistration;
            Mileage = mileage;
            ClaimType = claimType;
            CreatedDate = DateTime.UtcNow;
            SetBlockHash(parent);
        }

        public string CalculateBlockHash(string previousBlockHash)
        {
            string txnHash = ClaimNumber + SettlementAmount + SettlementDate + CarRegistration + Mileage + ClaimType;
            string blockHeader = BlockNumber + CreatedDate.ToString() + previousBlockHash;
            string combined = txnHash + blockHeader;

            return HashFun.GetSha256(combined);
        }

        public void SetBlockHash(ISBlock parent)
        {
            if (parent != null)
            {
                PreviousBlockHash = parent.BlockHash;
                parent.NextBlock = this;
            }
            else
            {
                // Previous block is the genesis block.
                PreviousBlockHash = null;
            }

            BlockHash = CalculateBlockHash(PreviousBlockHash);
        }

        public bool IsValidChain(string prevBlockHash, bool verbose)
        {
            bool isValid = true;

            // Is this a valid block and transaction
            string newBlockHash = CalculateBlockHash(prevBlockHash);

            if(newBlockHash != BlockHash)
            {
                isValid = false;
            }
            else
            {
                isValid |= PreviousBlockHash == prevBlockHash;
            }
            PrintVerificationMessage(verbose, isValid);

            // Check the next block by passing in our newly calculated blockhash. This will be compared to the previous
            // hash in the next block. They should match for the chain to be valid.
            if (NextBlock != null)
            {
                return NextBlock.IsValidChain(newBlockHash, verbose);
            }

            return isValid;
        }

        private void PrintVerificationMessage(bool verbose, bool isValid)
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
            }
        }


    }
}
