using BlockchainFunLib;
using BlockchainFunLib.MultipleTransactions;
using BlockchainFunLib.SingleTransaction;
using System;

namespace BlockchainFun
{
    class Program
    {
        static void Main(string[] args)
        {
            RunWithTransactionPoolAndProofOfWork();
            //PlayWithProofOfWork();
            //RunWithTransactionPool();
            //RunMultuipleTransactionsBlockExample();
            //RunSingleTransactionBlockExample();
        }

        private static void RunWithTransactionPoolAndProofOfWork()
        {
            var txn5 = SetupTransactions();
            IKeyStore keyStore = new KeyStore(HashFun.Generate256BitKey());

            IMBlock block1 = new MBlock(0, keyStore, 3);
            IMBlock block2 = new MBlock(1, keyStore, 3);
            IMBlock block3 = new MBlock(2, keyStore, 3);
            IMBlock block4 = new MBlock(3, keyStore, 3);

            AddTransactionsToBlocksAndCalculateHashes(block1, block2, block3, block4);

            MBlockChain chain = new MBlockChain();
            chain.AcceptBlock(block1);
            chain.AcceptBlock(block2);
            chain.AcceptBlock(block3);
            chain.AcceptBlock(block4);

            chain.VerifyChain();

            Console.WriteLine("");
            Console.WriteLine("");

            txn5.ClaimNumber = "weqwewe";
            chain.VerifyChain();

            Console.WriteLine();
        }

        static void PlayWithProofOfWork()
        {
            string text = "Marry had a little lamb";
            ProofOfWork.CalculateProofOfWork(text, 0);
            ProofOfWork.CalculateProofOfWork(text, 1);
            ProofOfWork.CalculateProofOfWork(text, 2);
            ProofOfWork.CalculateProofOfWork(text, 3);
            ProofOfWork.CalculateProofOfWork(text, 4);
            ProofOfWork.CalculateProofOfWork(text, 5);
        }

        static void RunWithTransactionPool()
        {
            var txn5 = SetupTransactions();
            IKeyStore keyStore = new KeyStore(HashFun.Generate256BitKey());

            IMBlock block1 = new MBlock(0, keyStore);
            IMBlock block2 = new MBlock(1, keyStore);
            IMBlock block3 = new MBlock(2, keyStore);
            IMBlock block4 = new MBlock(3, keyStore);

            AddTransactionsToBlocksAndCalculateHashes(block1, block2, block3, block4);

            MBlockChain chain = new MBlockChain();
            chain.AcceptBlock(block1);
            chain.AcceptBlock(block2);
            chain.AcceptBlock(block3);
            chain.AcceptBlock(block4);

            chain.VerifyChain();

            Console.WriteLine("");
            Console.WriteLine("");

            txn5.ClaimNumber = "weqwewe";
            chain.VerifyChain();

            Console.WriteLine();
        }

        private static void AddTransactionsToBlocksAndCalculateHashes(IMBlock block1, IMBlock block2, IMBlock block3, IMBlock block4)
        {
            block1.AddTransaction(txnPool.GetTransaction());
            block1.AddTransaction(txnPool.GetTransaction());
            block1.AddTransaction(txnPool.GetTransaction());
            block1.AddTransaction(txnPool.GetTransaction());

            block2.AddTransaction(txnPool.GetTransaction());
            block2.AddTransaction(txnPool.GetTransaction());
            block2.AddTransaction(txnPool.GetTransaction());
            block2.AddTransaction(txnPool.GetTransaction());

            block3.AddTransaction(txnPool.GetTransaction());
            block3.AddTransaction(txnPool.GetTransaction());
            block3.AddTransaction(txnPool.GetTransaction());
            block3.AddTransaction(txnPool.GetTransaction());

            block4.AddTransaction(txnPool.GetTransaction());
            block4.AddTransaction(txnPool.GetTransaction());
            block4.AddTransaction(txnPool.GetTransaction());
            block4.AddTransaction(txnPool.GetTransaction());

            block1.SetBlockHash(null);
            block2.SetBlockHash(block1);
            block3.SetBlockHash(block2);
            block4.SetBlockHash(block3);
        }

        private static ITransaction SetupTransactions()
        {
            txnPool.AddTransaction(txn1);
            txnPool.AddTransaction(txn2);
            txnPool.AddTransaction(txn3);
            txnPool.AddTransaction(txn4);
            txnPool.AddTransaction(txn5);
            txnPool.AddTransaction(txn6);
            txnPool.AddTransaction(txn7);
            txnPool.AddTransaction(txn8);
            txnPool.AddTransaction(txn9);
            txnPool.AddTransaction(txn10);
            txnPool.AddTransaction(txn11);
            txnPool.AddTransaction(txn12);
            txnPool.AddTransaction(txn13);
            txnPool.AddTransaction(txn14);
            txnPool.AddTransaction(txn15);
            txnPool.AddTransaction(txn16);

            return txn5;
        }

        private static void RunMultuipleTransactionsBlockExample()
        {
            IMBlock block1 = new MBlock(0);
            IMBlock block2 = new MBlock(1);
            IMBlock block3 = new MBlock(2);
            IMBlock block4 = new MBlock(3);

            block1.AddTransaction(txn1);
            block1.AddTransaction(txn2);
            block1.AddTransaction(txn3);
            block1.AddTransaction(txn4);

            block2.AddTransaction(txn5);
            block2.AddTransaction(txn6);
            block2.AddTransaction(txn7);
            block2.AddTransaction(txn8);

            block3.AddTransaction(txn9);
            block3.AddTransaction(txn10);
            block3.AddTransaction(txn11);
            block3.AddTransaction(txn12);

            block4.AddTransaction(txn13);
            block4.AddTransaction(txn14);
            block4.AddTransaction(txn15);
            block4.AddTransaction(txn16);

            block1.SetBlockHash(null);
            block2.SetBlockHash(block1);
            block3.SetBlockHash(block2);
            block4.SetBlockHash(block4);

            MBlockChain chain = new MBlockChain();

            chain.AcceptBlock(block1);
            chain.AcceptBlock(block2);
            chain.AcceptBlock(block3);
            chain.AcceptBlock(block4);

            chain.VerifyChain();

            Console.WriteLine("");
            Console.WriteLine("");

            txn4.ClaimNumber = "El ciupos";
            chain.VerifyChain();
        }

        private static void RunSingleTransactionBlockExample()
        {
            SBlockChain sBlockChain = new SBlockChain();

            ISBlock block1 = new SBlock(0, "ABC123", 1000.00m, DateTime.Now, "QWE123", 10000, ClaimType.TotalLoss, null);
            ISBlock block2 = new SBlock(1, "VBG345", 2000.00m, DateTime.Now, "JKH567", 20000, ClaimType.TotalLoss, block1);
            ISBlock block3 = new SBlock(2, "XCF234", 3000.00m, DateTime.Now, "DH23ED", 30000, ClaimType.TotalLoss, block2);
            ISBlock block4 = new SBlock(3, "CBHD45", 4000.00m, DateTime.Now, "DH34K6", 40000, ClaimType.TotalLoss, block3);
            ISBlock block5 = new SBlock(4, "AJD345", 5000.00m, DateTime.Now, "28FNF4", 50000, ClaimType.TotalLoss, block4);
            ISBlock block6 = new SBlock(5, "QAX367", 6000.00m, DateTime.Now, "FJK676", 60000, ClaimType.TotalLoss, block5);
            ISBlock block7 = new SBlock(6, "CGO444", 7000.00m, DateTime.Now, "LKU234", 70000, ClaimType.TotalLoss, block6);
            ISBlock block8 = new SBlock(7, "PLO254", 8000.00m, DateTime.Now, "VBN456", 80000, ClaimType.TotalLoss, block7);

            sBlockChain.AcceptBlock(block1);
            sBlockChain.AcceptBlock(block2);
            sBlockChain.AcceptBlock(block3);
            sBlockChain.AcceptBlock(block4);
            sBlockChain.AcceptBlock(block5);
            sBlockChain.AcceptBlock(block6);
            sBlockChain.AcceptBlock(block7);
            sBlockChain.AcceptBlock(block8);

            sBlockChain.VerifyChain();

            Console.WriteLine("");
            Console.WriteLine("");

            block4.CreatedDate = new DateTime(2017, 09, 20);

            sBlockChain.VerifyChain();

            Console.WriteLine();
        }

        static ITransaction txn1 = new Transaction("ABC123", 1000.00m, DateTime.Now, "QWE123", 10000, ClaimType.TotalLoss);
        static ITransaction txn2 = new Transaction("VBG345", 2000.00m, DateTime.Now, "JKH567", 20000, ClaimType.TotalLoss);
        static ITransaction txn3 = new Transaction("XCF234", 3000.00m, DateTime.Now, "DH23ED", 30000, ClaimType.TotalLoss);
        static ITransaction txn4 = new Transaction("CBHD45", 4000.00m, DateTime.Now, "DH34K6", 40000, ClaimType.TotalLoss);
        static ITransaction txn5 = new Transaction("AJD345", 5000.00m, DateTime.Now, "28FNF4", 50000, ClaimType.TotalLoss);
        static ITransaction txn6 = new Transaction("QAX367", 6000.00m, DateTime.Now, "FJK676", 60000, ClaimType.TotalLoss);
        static ITransaction txn7 = new Transaction("CGO444", 7000.00m, DateTime.Now, "LKU234", 70000, ClaimType.TotalLoss);
        static ITransaction txn8 = new Transaction("PLO254", 8000.00m, DateTime.Now, "VBN456", 80000, ClaimType.TotalLoss);
        static ITransaction txn9 = new Transaction("ABC123", 1000.00m, DateTime.Now, "QWE123", 10000, ClaimType.TotalLoss);
        static ITransaction txn10 = new Transaction("VBG345", 2000.00m, DateTime.Now, "JKH567", 20000, ClaimType.TotalLoss);
        static ITransaction txn11 = new Transaction("XCF234", 3000.00m, DateTime.Now, "DH23ED", 30000, ClaimType.TotalLoss);
        static ITransaction txn12 = new Transaction("CBHD45", 4000.00m, DateTime.Now, "DH34K6", 40000, ClaimType.TotalLoss);
        static ITransaction txn13 = new Transaction("AJD345", 5000.00m, DateTime.Now, "28FNF4", 50000, ClaimType.TotalLoss);
        static ITransaction txn14 = new Transaction("QAX367", 6000.00m, DateTime.Now, "FJK676", 60000, ClaimType.TotalLoss);
        static ITransaction txn15 = new Transaction("CGO444", 7000.00m, DateTime.Now, "LKU234", 70000, ClaimType.TotalLoss);
        static ITransaction txn16 = new Transaction("PLO254", 8000.00m, DateTime.Now, "VBN456", 80000, ClaimType.TotalLoss);
        static readonly TransactionPool txnPool = new TransactionPool();
    }
}
