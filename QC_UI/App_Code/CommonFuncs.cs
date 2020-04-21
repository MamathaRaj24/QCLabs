using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CommonFuncs
/// </summary>
public class CommonFuncs
{
    public void ShowAlertMessage(string error)
    {
        Page page = HttpContext.Current.Handler as Page;

        if (page != null)
        {
            error = error.Replace("'", " ");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
        }
    }
    //*******************  **********************************************
    // Description       : This method is used to bind drop down list 
    // Input Parameters  : Dropdownlist id, Dataset name, Textfield , value field , default value that 
    //                     should be shown in the drop down list
    //OutPut parameters  : None 
    //**************************************************************************
    public void BindDropDownLists(DropDownList ddl, DataTable ddt, string textfield, string valuefield, string strDefaultValue)
    {


        // if (ds.Tables.Count > 0)
        //{
        // if (ds.Tables[0].Rows.Count > 0)
        // {
        ddl.Items.Clear();
       ddl.DataSource = ddt;
        ddl.DataTextField = textfield;
        ddl.DataValueField = valuefield;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select--", ""));
        ////ddl.SelectedIndex = 0;
        // }
    }



    public void BindRadioLists(RadioButtonList rbl, DataTable ddt, string textfield, string valuefield, string strDefaultValue)
    {
        rbl.Items.Clear();
        rbl.DataSource = ddt;
        rbl.DataTextField = textfield;
        rbl.DataValueField = valuefield;
        rbl.DataBind();
        ////ddl.SelectedIndex = 0;
        // }
    }






    //*******************  **********************************************
    // Description       : This method is used to bind drop down list 
    // Input Parameters  : Dropdownlist id, Dataset name, Textfield , value field , default value that 
    //                     should be shown in the drop down list
    //OutPut parameters  : None 
    //**************************************************************************
    public void BindDropDownLists_WithAllOption(DropDownList ddl, DataTable ddt, string textfield, string valuefield, string strDefaultValue)
    {


        // if (ds.Tables.Count > 0)
        //{
        // if (ds.Tables[0].Rows.Count > 0)
        // {
        ddl.Items.Clear();
        ddl.DataSource = ddt;
        ddl.DataTextField = textfield;
        ddl.DataValueField = valuefield;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("ALL", "ALL"));
        ////ddl.SelectedIndex = 0;
        // }
    }
    public void BindCheckLists(CheckBoxList chk, DataTable ddt, string textfield, string valuefield, string strDefaultValue)
    {


        // if (ds.Tables.Count > 0)
        //{
        // if (ds.Tables[0].Rows.Count > 0)
        // {
        chk.Items.Clear();
        chk.DataSource = ddt;
        chk.DataTextField = textfield;
        chk.DataValueField = valuefield;
        chk.DataBind();
        // chk.Items.Insert(0, new ListItem("Select", "0"));
        ////ddl.SelectedIndex = 0;
        // }
    }
    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }

    public string[] CalculateFinYearandMonth()
    {
        int Year = DateTime.Now.Year;
        int Month = DateTime.Now.Month;
        string[] FinYear = new string[2];
        /*FINANCIAL YEAR - FROM APRIL TO MARCH   
         SO IF MONTH IS APRIL , THEN FINYEAR = CURRENT YEAR - CURRENT YEAR + 1
         ELSE CURRENT YEAR -1 - CURRENT YEAR*/

        if (Month >= 4)
        {
            FinYear[0] = (Year - 1).ToString() + "-" + (Year).ToString();
            FinYear[1] = (Year).ToString() + "-" + (Year + 1).ToString();
        }
        else
        {
            FinYear[0] = (Year ).ToString() + "-" + (Year+1).ToString();
            FinYear[1] = (Year-1).ToString() + "-" + (Year ).ToString();
        }
        return FinYear;

    }

    public void BindFinancialYears(DropDownList ddl)
    {
        string[] years = CalculateFinYearandMonth();
        ddl.Items.Clear();
        ddl.DataSource = years;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select--", ""));
    }

    public string getCurrentFinancialYear()
    {
        int Year = DateTime.Now.Year;
        int Month = DateTime.Now.Month;
        if (Month >= 4)
            return Year.ToString() + "-" + (Year + 1).ToString();
        else
            return (Year - 1).ToString() + "-" + Year.ToString();
    }
    public string getPrviousFinancialYear()
    {
        int Year = DateTime.Now.Year;
        int Month = DateTime.Now.Month;
        if (Month >= 4)
            return (Year - 1).ToString() + "-" + Year.ToString();
        else
            return Year.ToString() + "-" + (Year + 1).ToString();
    }


    IFormatProvider provider = new System.Globalization.CultureInfo("fr-FR", true);
    public string Texttodateconverter(string textdate)
    {
        string date = (DateTime.Parse(textdate.Trim(), provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault).Date).ToString("yyyy-MM-dd"); //objCommon.Texttodateconverter
        return date;
    }


    public string MonthToDate(string textdate)
    {
        string date = (DateTime.Parse(textdate.Trim()+"30", provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault).Date).ToString("yyyy-MM"); //objCommon.Texttodateconverter
        return date;
    }

    public decimal IsNumeric(string value)
    {
       decimal result;
       if(decimal.TryParse(value,out result))
            return result;
        else
            return 0;
    }

    public int IsInteger(string value)
    {
        int result;
        if (int.TryParse(value, out result))
            return result;
        else
            return 0;
    }


    
    public string ConvertNumbertoWords(int number)
    {
        if (number == 0)
            return "Zero";
        if (number < 0)
            return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";

        if ((number / 1000000000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000000000) + " Billion ";
            number %= 1000000000;
        }

        if ((number / 10000000) > 0)
        {
            words += ConvertNumbertoWords(number / 10000000) + " Crore ";
            number %= 10000000;
        }

        //if ((number / 1000000) > 0)
        //{
        //    words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
        //    number %= 1000000;
        //}

        if ((number / 100000) > 0)
        {
            words += ConvertNumbertoWords(number / 100000) + " Lakhs ";
            number %= 100000;
        }


        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " Thousand ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " Hundred ";
            number %= 100;
        }
        if (number > 0)
        {
            if (words != "")
                words += "And ";
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


}