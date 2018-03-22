using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BlockchainFunLib
{
    public class ProofOfWork
    {
        public static (int nonce, string hash) CalculateProofOfWork(string data, int difficulty)
        {
            string difficultyString = DifficultyString(difficulty);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string hashedData = "";
            int nonce = -1;
            do
            {
                nonce++;
                hashedData = HashFun.GetSha256(nonce + data);                

            } while (!hashedData.StartsWith(difficultyString));

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            Console.WriteLine("Difficulty Level " + difficultyString + " - Nonce = " + nonce + " - Elapsed = " + elapsedTime + " - " + hashedData);
            return (nonce, hashedData);

        }

        private static string DifficultyString(int difficulty)
        {
            string difficultyString = string.Empty;

            for (int i = 0; i < difficulty; i++)
            {
                difficultyString += "0";
            }

            return difficultyString;
        }
    }
}
