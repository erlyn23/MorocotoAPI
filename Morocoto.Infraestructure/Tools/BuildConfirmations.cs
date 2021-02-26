using System;
using System.Collections.Generic;
using System.Text;

namespace Morocoto.Infraestructure.Tools
{
    public class BuildConfirmations
    {
        public string BuildConfirmationCode()
        {
            Random random = new Random();
            string randomCode = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            for (int index = 0; index < 6; index++)
            {
                string randomNumber = random.Next(1, 9).ToString();
                randomCode = stringBuilder.Append(randomNumber).ToString();
            }
            return randomCode;
        }
    }
}
