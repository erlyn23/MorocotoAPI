using System;
using System.Collections.Generic;
using System.Text;

namespace Morocoto.Infraestructure.Tools
{
    public class AccountNumberGeneration
    {
        public static string GenerateAccountNumber()
        {
            Random random = new Random();
            string accountNumber = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            for(int index = 0; index < 12; index++)
            {
                string randomNumber = random.Next(0, 9).ToString();
                accountNumber = stringBuilder.Append(randomNumber).ToString();
            }
            return accountNumber;
        }
    }
}
