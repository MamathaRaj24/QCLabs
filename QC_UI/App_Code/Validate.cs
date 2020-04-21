using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Validate
/// </summary>
public class Validate
{
    public Validate()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool CheckRange(int value, int minValue, int MaxValue)
    {

        if (!(value >= minValue & value <= MaxValue))
        {
            return false;
        }
        return true;
    }

    //public bool validateUserId(string userId,string password, string cUserId,string cHashedPassword,string crnum)
    //    {
    //        if(userId!=cUserId)
    //        {
    //            return false;  
    //        }
    //        if (cUserId.Trim() == "")
    //        {
    //            return false;
    //        }
    //        if (cHashedPassword.Trim() == "")
    //        {
    //            return false;
    //        }
    //        if (crnum.Trim() == "")
    //        {
    //            return false;
    //        }
    //        if (!IsNumberOk(crnum))
    //        {
    //            return false;
    //        }
    //        int icrnum = int.Parse(crnum);
    //        if (!CheckRange(icrnum, 1, 1000))
    //        {
    //            return false;
    //        }

    //        hasing getHash = new hasing();


    //        string Hashedsalted = getHash.GetMD5Hash(password).ToString().Trim();
    //        string Hash = Hashedsalted.ToUpper() + crnum;
    //        string HashedsaltedDbPwd = getHash.GetMD5Hash(Hash).ToString().Trim();
    //        if (HashedsaltedDbPwd != cHashedPassword)
    //        {
    //            return false;
    //        }
    //        return true;   
    //    }

    public bool IsAlphaNumTDH(string input_val)
    {

        foreach (char c in input_val)
        {
            int f1 = c;
            // Allows for chars /, -, blank
            if (c.Equals('/') || c.Equals('-') || c.Equals(' ') || c.Equals('(') || c.Equals('.') || c.Equals(')')) continue;

            // Allows  numbers 0-9   A-Z and a-z

            if ((f1 > 47 && f1 < 58) || (f1 > 64 && f1 < 91) || (f1 > 96 && f1 < 123))
            {
                continue;
            }

            //// checks for regular telugu chars
            //if (f1 < 3073 || f1 > 3169)
            //{

            //    return false;
            //}
            //else
            //    continue;
        }

        return true;
    }



    public bool IsUnicodeString(string input_val)
    {

        foreach (char c in input_val)
        {
            int f1 = c;
            // Allows for chars /, -, blank
            if (c.Equals('/') || c.Equals('-') || c.Equals(' ') || c.Equals('.') || c.Equals('&')) continue;

            // Allows  numbers 0-9   A-Z and a-z

            if ((f1 > 64 && f1 < 91) || (f1 > 96 && f1 < 123))
            {
                continue;
            }

            // checks for regular telugu chars
            if (f1 < 3073 || f1 > 3169)
            {

                return false;
            }
            else
                continue;
        }
        return true;
    }



    private bool IsDecimalOk(string input_val)
    {

        foreach (char c in input_val)
        {
            int f1 = c;
            // Allows for chars /, -, blank
            if (c.Equals('.')) continue;

            // Allows  numbers 0-9   A-Z and a-z

            if ((f1 > 47 && f1 < 58))
            {
                continue;
            }
            else
                return false;
        }
        return true;
    }

    public bool IsNumberOk(string input_val)
    {

        foreach (char c in input_val)
        {
            int f1 = c;


            // Allows  numbers 0-9   A-Z and a-z

            if ((f1 > 47 && f1 < 58))
            {
                continue;
            }
            else
                return false;
        }
        return true;
    }

    public bool IsDecimal(string strValue, int minval, int maxval)
    {

        Regex objRankPattern = new Regex(@"^(-)?\d+(\.\d{" + minval + "," + maxval + "})?$");
        //Regex objRankPattern = new Regex(@"^(-)?\d+(\.\d\d)?$");
        // new Regex(@"^\d{" + min + "," + max + "}$");  // between 1 to 6 digits
        if (objRankPattern.IsMatch(strValue))
        {
            return IsDecimalOk(strValue);

        }
        else
        {
            return false;
        }

        //return objRankPattern.IsMatch(strValue);
    }

    //Date validation  ----------------------------------------
    IFormatProvider provider = new System.Globalization.CultureInfo("fr-FR", true);
    public bool IsDate(string strDoB)
    {
        Regex objDoBPattern = new Regex(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");

        if (objDoBPattern.IsMatch(strDoB))
        {

            DateTime billdate = DateTime.Parse(strDoB, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault).Date;
            if ((billdate > DateTime.Now))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }



        /*
         This expression validates dates in the ITALIAN d/m/y format from 1/1/1600 - 31/12/9999. 
         * The days are validated for the given month and year. Leap years are validated for all 
         * 4 digits years from 1600-9999, and all 2 digits years except 00 since it could be any
         * century (1900, 2000, 2100). Days and months must be 1 or 2 digits and may have leading 
         * zeros. Years must be 2 or 4 digit years. 4 digit years must be between 1600 and 9999. 
         * Date separator may be a slash (/), dash (-), or period (.) 
         * Thanks to Michael Ash for US Version 
         */
    }

    //IFSC Code Validation   ---------------------------------------- 
    public bool IsIfsc(string IFSC)
    {
        if ((IFSC.ToString().Trim() != "") && (IFSC != null))
        {
            Regex objMobilePattern = new Regex("^[A-Za-z]{4}[0]{1}[A-Za-z0-9]{6}$");
            return objMobilePattern.IsMatch(IFSC);
        }
        else
        {
            return true;
        }
    }


    //ISNUMBER   Min-10, Max-10, 0 to 9 is validated string ---------------------------------------- 
    public bool IsNumber(string strMobNo, int minval, int maxval)
    {
        if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
        {
            Regex objMobilePattern = new Regex("^[0-9]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(strMobNo);
        }
        else
        {
            return true;
        }

        //if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
        //{
        //    Regex objMobilePattern = new Regex("^[0-9]{" + minval + "," + maxval + "}$");
        //    if (objMobilePattern.IsMatch(strMobNo))
        //    {
        //        return IsNumberOk(strMobNo.Trim());
        //    }
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return true;
        //}

    }

    //IsValidCourseCode   Min-3, Max-3, A to Z,0 to 9 is validated string ---------------------------------------- 
    public bool IsValidCourseCode(string coursecode, int minval, int maxval)
    {
        if ((coursecode.ToString().Trim() != "") && (coursecode != null))
        {
            Regex objMobilePattern = new Regex("^[A-Z]{1}[0-9]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(coursecode);
        }
        else
        {
            return true;
        }

        //if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
        //{
        //    Regex objMobilePattern = new Regex("^[0-9]{" + minval + "," + maxval + "}$");
        //    if (objMobilePattern.IsMatch(strMobNo))
        //    {
        //        return IsNumberOk(strMobNo.Trim());
        //    }
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return true;
        //}

    }

    //IsValidCollegeCode   Min-4, Max-6, A to Z,a-z,0 to 9 is validated string ---------------------------------------- 
    public bool IsValidCollegeCode(string collegecode, int minval, int maxval)
    {
        if ((collegecode.ToString().Trim() != "") && (collegecode != null))
        {
            Regex objMobilePattern = new Regex("^[A-Za-z0-9]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(collegecode);
        }
        else
        {
            return true;
        }

        //if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
        //{
        //    Regex objMobilePattern = new Regex("^[0-9]{" + minval + "," + maxval + "}$");
        //    if (objMobilePattern.IsMatch(strMobNo))
        //    {
        //        return IsNumberOk(strMobNo.Trim());
        //    }
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return true;
        //}

    }

    //IsValidSocietyCode   Min-3, Max-3, A to Z,a-z,0 to 9 is validated string ---------------------------------------- 
    public bool IsValidSocietyCode(string societycode, int minval, int maxval)
    {
        if ((societycode.ToString().Trim() != "") && (societycode != null))
        {
            Regex objMobilePattern = new Regex("^[A-Za-z]{3}[0-9]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(societycode);
        }
        else
        {
            return true;
        }


    }

    //IsValidAppendixCode   Min-1, Max-1, 0 to 9 is validated string ---------------------------------------- 
    public bool IsValidAppendixCode(string appendixcode, int minval, int maxval)
    {
        if ((appendixcode.ToString().Trim() != "") && (appendixcode != null))
        {
            Regex objMobilePattern = new Regex("^[0]{1}[1-8]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(appendixcode);
        }
        else
        {
            return true;
        }


    }

    //IsValidCetCode   Min-4, Max-6, A to Z is validated string ---------------------------------------- 
    public bool IsValidCetCode(string cetcode, int minval, int maxval)
    {
        if ((cetcode.ToString().Trim() != "") && (cetcode != null))
        {
            Regex objMobilePattern = new Regex("^[A-Z]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(cetcode);
        }
        else
        {
            return true;
        }


    }

    //IsValidYear   Min-4, Max-4, A to Z,0 to 9 is validated string ---------------------------------------- 
    public bool IsValidYear(string year, int minval, int maxval)
    {
        if ((year.ToString().Trim() != "") && (year != null))
        {
            Regex objMobilePattern = new Regex("^[1-6]{1}[A-Z]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(year);
        }
        else
        {
            return true;
        }


    }

    //IsValidCategory   Min-1, Max-4, A to Z is validated string ---------------------------------------- 
    public bool IsValidCategory(string category, int minval, int maxval)
    {
        if ((category.ToString().Trim() != "") && (category != null))
        {
            Regex objMobilePattern = new Regex("^[A-Z]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(category);
        }
        else
        {
            return true;
        }


    }

    //IsValidCollegeName   Min-1, Max-255, A to Z,.-()/ is validated string ---------------------------------------- 
    public bool IsValidCollegeName(string collegename, int minval, int maxval)
    {
        if ((collegename.ToString().Trim() != "") && (collegename != null))
        {
            Regex objMobilePattern = new Regex("^[-/.,()~A-Za-z\\s]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(collegename);
        }
        else
        {
            return true;
        }


    }

    //IsValidCourseName   Min-1, Max-25, A to Z.()/ is validated string ---------------------------------------- 
    public bool IsValidCourseName(string coursename, int minval, int maxval)
    {
        if ((coursename.ToString().Trim() != "") && (coursename != null))
        {
            Regex objMobilePattern = new Regex("^[.()/A-Za-z\\s]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(coursename);
        }
        else
        {
            return true;
        }


    }

    //IsValidPanNo   Min-10, Max-10, A to Z,0 to 9 is validated string ---------------------------------------- 
    public bool IsValidPanNo(string panno, int minval, int maxval)
    {
        if ((panno.ToString().Trim() != "") && (panno != null))
        {
            Regex objMobilePattern = new Regex("^[A-Z0-9]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(panno);
        }
        else
        {
            return true;
        }


    }

    //All names
    //IsValidName   Min-1, Max-255, A-Za-z space is validated string ---------------------------------------- 
    public bool IsValidName(string name, int minval, int maxval)
    {
        if ((name.ToString().Trim() != "") && (name != null))
        {
            Regex objMobilePattern = new Regex("^[A-Za-z\\s]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(name);
        }
        else
        {
            return true;
        }

    }

    //IsValidAddress   Min-1, Max-100, -/.,A-Za-z0-9 space is validated string ---------------------------------------- 
    public bool IsValidAddress(string address, int minval, int maxval)
    {
        if ((address.ToString().Trim() != "") && (address != null))
        {
            Regex objMobilePattern = new Regex("^[-/.,A-Za-z0-9\\s]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(address);
        }
        else
        {
            return true;
        }


    }


    //Password    ----------------------------------------
    public bool IsPassword(string strPWD)
    {
        Regex objPWDPattern = new Regex("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$");
        return objPWDPattern.IsMatch(strPWD);
    }

    //EMail   ---------------------------------------- 
    public bool IsValidEmail(string strIn)
    {
        // Return true if strIn is in valid e-mail format.
        if (strIn != null)
        {
            return Regex.IsMatch(strIn,
               @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
        else
        {
            return true;
        }
    }

    //Password    ----------------------------------------
    public bool IsIPAddress(string strIPAdd)
    {
        Regex objIPAddPattern = new Regex(@"[(\d{1,3}\.){3}\d{1,3}\]");
        return objIPAddPattern.IsMatch(strIPAdd);
    }

    public string properMsg(string errMsg)
    {
        string result = errMsg.ToString().Trim();
        return result;
        //if (result != "")
        //{
        //    int start_val = errMsg.IndexOf('-');
        //    int end_val = errMsg.IndexOf('-', start_val + 1);
        //    if (end_val < 0 || start_val < 0) return result;
        //    int err_len = end_val - start_val;
        //    String err_message = result.Substring(start_val + 1, err_len - 1).ToString();
        //    return err_message;

        //}
        //else
        //{
        //    result = "<font color='red' face='Arial' size='3'>Uknown error. Not a valid Message.</font>";
        //    return result.ToString();
        //}
    }
    public string traperror(string errmsg)
    {
        string myStr = null;
        myStr = errmsg;
        //return myStr;
        if (errmsg.IndexOf("PRIMARY KEY".ToUpper()) >= 0)
            myStr = "Data already Exists!! Duplicates not allowed!!";
        else if (errmsg.IndexOf("UNIQUE KEY".ToUpper()) >= 0)
            myStr = "One or more Key Values Already Present!! Duplicate Key Values are not allowed!!";
        else if (errmsg.IndexOf("REFERENCE constraint") >= 0)
            myStr = "Related Data is available in another dependent Forms. Please Rectify and Try again!!";
        else if (errmsg.IndexOf("CHECK constraint".ToUpper(), 0, StringComparison.OrdinalIgnoreCase) >= 0)
        {
            int start_val = errmsg.IndexOf('-');
            int end_val = errmsg.IndexOf('-', start_val + 1);
            if (end_val < 0 || start_val < 0) return errmsg;
            int err_len = end_val - start_val;
            myStr = errmsg.Substring(start_val + 1, err_len - 1).ToString();

        }
        else if (errmsg.IndexOf("Login failed".ToUpper()) >= 0)
            myStr = "Could not Login. Please give proper Login Details and Try again!!";
        else if (errmsg.IndexOf("constraint".ToUpper()) >= 0)
            myStr = "Data in One or more colums is invalid. Please Rectify and Try again!!";
        else if (errmsg.IndexOf("deadlocked".ToUpper()) >= 0)
            myStr = "Data Could not be Saved. Please Submit/Save again!!";
        else if (errmsg.IndexOf("unique index".ToUpper()) >= 0)
            myStr = "Duplicates present in Unique Columns. Check relevant columns. Data Could not be Saved. Please rectify and Submit/Save again!!";
        else if (errmsg.IndexOf("Arithmetic overflow".ToUpper()) >= 0)
            myStr = "Arithmetic overflow in one more colums. Please enter data within limits and Submit/Save again!!";
        else if ((errmsg.Length) > 0)
            myStr = "Error encountered in Performing the operation. Please Rectify and Try again!!";

        if ((myStr.Length) > 0)
            myStr = "<font color='red' size='3'><bold>" + myStr + "</bold></font>";

        return myStr;
    }
    public bool IsValidExtent(string value)
    {
        decimal extent = decimal.Parse(value);
        string[] ext = extent.ToString().Split('.');

        if ((ext.Length >= 1))
        {
            decimal extentd = decimal.Parse(ext[0]);


            if ((extent - extentd) > .3999M)
            {
                return false;
            }
        }
        return true;


    }
    // To Logout 

    public bool checkLevel(int pageLevel, string ulevel)
    {
        int usr_level = Convert.ToInt16(ulevel);
        if (usr_level > pageLevel)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public decimal getGuntas(decimal values)
    {
        string[] strValues = values.ToString("00.0000").Split('.');

        int s1 = int.Parse(strValues[0]);
        int s2 = int.Parse(strValues[1].Substring(0, 2));
        int s3 = int.Parse(strValues[1].Substring(2, 2));
        int s = s1 * 40 * 100 + s2 * 100 + s3;


        return decimal.Parse(s.ToString());
    }

    public decimal getDecimal(decimal values1)
    {
        int values = int.Parse(values1.ToString());
        int subguntas = values % 100;
        int blance = values / 100;
        int guntas = blance % 40;
        blance = blance / 40;
        int Acres = blance;
        //txtResuly.Text=Acres +(guntas*0.)
        string sSubguntas = (subguntas > 9) ? subguntas.ToString() : "0" + subguntas.ToString();
        string sGuntas = (guntas > 9) ? guntas.ToString() : "0" + guntas.ToString();
        return decimal.Parse(Acres.ToString() + "." + sGuntas + sSubguntas);
    }

    public bool IsAlphaNumbers(string input_val)
    {

        foreach (char c in input_val)
        {
            int f1 = c;


            // Allows  numbers 0-9   A-Z and a-z

            if ((f1 > 47 && f1 < 58) || (f1 > 64 && f1 < 91) || (f1 > 96 && f1 < 123))
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }


    public string NumberToWords(int number)
    {
        if (number == 0)
            return "Zero";

        if (number < 0)
            return "Minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }

        return words;
    }

    public string NumberToWords(decimal number)
    {

        if (number == 0)
            return "Zero";

        if (number < 0)
            return "Minus " + NumberToWords(Math.Abs(number));

        string words = "";


        int intNumber = int.Parse(number.ToString("0.0000").Split('.')[0]);
        string decimalNumber = number.ToString("0.0000").Split('.')[1];
        string intNumberString = NumberToWords(intNumber);

        string[] unitsMap = new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        string decimalNumberString = "";
        for (int i = 0; i < decimalNumber.Length; i++)
        {
            decimalNumberString += unitsMap[int.Parse(decimalNumber[i].ToString())] + " ";
        }

        words = intNumberString + " Acre(s) " + decimalNumberString;


        return words;

    }



    //prasad new 





    //IsValidRegNo   Min-1, Max-100, -/A-Za-z0-9 is validated string ---------------------------------------- 
    public bool IsValidRegNo(string regno, int minval, int maxval)
    {
        if ((regno.ToString().Trim() != "") && (regno != null))
        {
            Regex objMobilePattern = new Regex("^[-/A-Za-z0-9]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(regno);
        }
        else
        {
            return true;
        }


    }

    //IsValidDistCode   Min-4, Max-6, A to Z is validated string ---------------------------------------- 
    public bool IsValidDistCode(string distcode, int minval, int maxval)
    {
        if ((distcode.ToString().Trim() != "") && (distcode != null))
        {
            Regex objMobilePattern = new Regex("^[A-Z]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(distcode);
        }
        else
        {
            return true;
        }


    }

    //ISMobileNo   Min-9, Max-9, 0 to 9 is validated string ---------------------------------------- 
    public bool ISMobileNo(string strMobNo, int minval, int maxval)
    {
        if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
        {
            Regex objMobilePattern = new Regex("^[5-9][0-9]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(strMobNo);
        }
        else
        {
            return true;
        }

        //if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
        //{
        //    Regex objMobilePattern = new Regex("^[0-9]{" + minval + "," + maxval + "}$");
        //    if (objMobilePattern.IsMatch(strMobNo))
        //    {
        //        return IsNumberOk(strMobNo.Trim());
        //    }
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return true;
        //}

    }

    //ISPhoneNo   Min-10, Max-10, 0 to 9 is validated string ---------------------------------------- 
    public bool ISPhoneNo(string strphoneNo, int minval, int maxval)
    {
        if ((strphoneNo.ToString().Trim() != "") && (strphoneNo != null))
        {
            Regex objMobilePattern = new Regex("^[0][0-9]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(strphoneNo);
        }
        else
        {
            return true;
        }

        //if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
        //{
        //    Regex objMobilePattern = new Regex("^[0-9]{" + minval + "," + maxval + "}$");
        //    if (objMobilePattern.IsMatch(strMobNo))
        //    {
        //        return IsNumberOk(strMobNo.Trim());
        //    }
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return true;
        //}

    }

    //ISValidUserName   Min-8, Max-15, a-zA-Z0-9_. is validated string ---------------------------------------- 
    public bool ISValidUserName(string username, int minval, int maxval)
    {
        if ((username.ToString().Trim() != "") && (username != null))
        {
            Regex objMobilePattern = new Regex("^[a-zA-Z0-9_.]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(username);
        }
        else
        {
            return true;
        }

        //if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
        //{
        //    Regex objMobilePattern = new Regex("^[0-9]{" + minval + "," + maxval + "}$");
        //    if (objMobilePattern.IsMatch(strMobNo))
        //    {
        //        return IsNumberOk(strMobNo.Trim());
        //    }
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return true;
        //}

    }

    //ISValidPassword   Min-8, Max-15, 0 to 9 is validated string ---------------------------------------- 
    public bool ISValidPassword(string password, int minval, int maxval)
    {
        if ((password.ToString().Trim() != "") && (password != null))
        {
            Regex objMobilePattern = new Regex("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{" + minval + "," + maxval + "})$");
            return objMobilePattern.IsMatch(password);
        }
        else
        {
            return true;
        }

        //if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
        //{
        //    Regex objMobilePattern = new Regex("^[0-9]{" + minval + "," + maxval + "}$");
        //    if (objMobilePattern.IsMatch(strMobNo))
        //    {
        //        return IsNumberOk(strMobNo.Trim());
        //    }
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return true;
        //}

    }

    //IsValidParticularName   Min-1, Max-25, A to Z.()/ is validated string ---------------------------------------- 
    public bool IsValidParticularName(string particular_name, int minval, int maxval)
    {
        if ((particular_name.ToString().Trim() != "") && (particular_name != null))
        {
            Regex objMobilePattern = new Regex("^[-,&()A-Za-z\\s]{" + minval + "," + maxval + "}$");
            return objMobilePattern.IsMatch(particular_name);
        }
        else
        {
            return true;
        }


    }


    ////ISValidFloat   Min-8, Max-15, 0 to 9 is validated string ---------------------------------------- 
    //public bool ISValidFloatNo(string floatno)
    //{
    //    if ((floatno.ToString().Trim() != "") && (floatno != null))
    //    {
    //        Regex objMobilePattern = new Regex("?[0-9]{0,3}\.?[0-9]{1,2}$");
    //        return objMobilePattern.IsMatch(floatno);
    //    }
    //    else
    //    {
    //        return true;
    //    }

    //    //if ((strMobNo.ToString().Trim() != "") && (strMobNo != null))
    //    //{
    //    //    Regex objMobilePattern = new Regex("^[0-9]{" + minval + "," + maxval + "}$");
    //    //    if (objMobilePattern.IsMatch(strMobNo))
    //    //    {
    //    //        return IsNumberOk(strMobNo.Trim());
    //    //    }
    //    //    {
    //    //        return false;
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    return true;
    //    //}

    //}



}