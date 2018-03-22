using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.SingleTransaction
{
    public class SBlockChain : ISBlockChain
    {
        public ISBlock CurrentBlock { get; set; }

        public ISBlock HeadBlock { get; set; } // Genesis block

        public List<ISBlock> Blocks { get; set; }

        public SBlockChain()
        {
            Blocks = new List<ISBlock>();
        }


        public void AcceptBlock(ISBlock block)
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
