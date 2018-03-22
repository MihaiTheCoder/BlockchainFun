using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.MultipleTransactions
{
    public class MBlockChain : IMBlockChain
    {
        public IMBlock CurrentBlock { get; set; }

        public IMBlock HeadBlock { get; set; } // Genesis block

        public List<IMBlock> Blocks { get; set; }

        public MBlockChain()
        {
            Blocks = new List<IMBlock>();
        }


        public void AcceptBlock(IMBlock block)
        {
            if(HeadBlock == null)
            {
                HeadBlock = block;
                HeadBlock.PreviousBlockHash = null;
            }
            CurrentBlock = block;
            Blocks.Add(block);
        }

        public void VerifyChain()
        {
            if (HeadBlock == null)
                throw new InvalidOperationException("Genesis block not set");

            bool isValid = HeadBlock.IsValidChain(null, true);

            if (isValid)
                Console.WriteLine("Blockchain integrity intact.");
            else
                Console.WriteLine("Blockchainintegrity NOT intact");
        }
    }
}
