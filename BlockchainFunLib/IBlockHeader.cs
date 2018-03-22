using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib
{
    public interface IBlockHeader
    {
        int BlockNumber { get; set; }

        DateTime CreatedDate { get; set; }

        string BlockHash { get; set; }

        string PreviousBlockHash { get; set; }
    }
}
