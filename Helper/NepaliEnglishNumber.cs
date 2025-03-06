using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace McavesExtension
{
    public class NepaliEnglishNumber
    {
        public static string English_Nepali(string EnglishNumericValue)
        {
            if (EnglishNumericValue == null)
            {
                EnglishNumericValue = " ";
            }
            string Eng_Value = EnglishNumericValue; // unicode  numeric chars
            string Nep_value = "";
            string[] Text_Nepali = { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९", ".", "/", "-" };
            string[] Text_English = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "/", "-" };
            char[] Inputtext = Eng_Value.ToString().ToCharArray();
            for (int j = 0; j < Eng_Value.Length; j++)
            {
                for (int i = 0; i < 13; i++)
                {
                    string value = Text_English[i].ToString();
                    string value1 = Inputtext[j].ToString();
                    if (value == value1)
                    {
                        Nep_value += Text_Nepali[i].ToString();
                    }
                }

            }
            return Nep_value;
        }

        public static string Nepali_English(string NepaliNumericValue)
        {
            int k = 0;
            string Nepali_Value = NepaliNumericValue;
            string Eng_Value = "";
            string[] Text_English = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "/", "-" };
            string[] Text_Nepali = { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९", ".", "/", "-" };
            char[] InputText = NepaliNumericValue.ToString().ToCharArray();
            for (int j = 0; j < Nepali_Value.Length; j++)
            {
                for (int i = 0; i < 13; i++)
                {
                    string value = Text_Nepali[i].ToString();
                    string value1 = InputText[j].ToString();
                    if (value == value1)
                    {
                        Eng_Value += Text_English[i].ToString();
                        k++;
                    }
                }
                if (k == 0)
                {
                    return Eng_Value = Nepali_Value;
                }
            }
            return Eng_Value;
        }
        public static Int32 GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
        }
    }
}
