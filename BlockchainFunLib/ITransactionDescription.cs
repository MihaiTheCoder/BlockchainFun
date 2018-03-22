using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib
{
    public interface ITransactionDescription
    {
        string ClaimNumber { get; set; }

        decimal SettlementAmount { get; set; }

        DateTime SettlementDate { get; set; }

        string CarRegistration { get; set; }

        int Mileage { get; set; }

        ClaimType ClaimType { get; set; }
    }
}
