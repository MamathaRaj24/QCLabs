using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using QC_BE;
using QC_DL;
using System.Text;

public partial class Agri_AO_SeedRegistrationAO : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    AgriDL Regobjdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    AgriBE objR = new AgriBE();
    DataTable dt;
    string con, user, state, Department, Aocode;
    SampleSqlInjectionScreeningModule obj = new SampleSqlInjectionScreeningModule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.ServerVariables["HTTP_REFERER"] == null) || (Request.ServerVariables["HTTP_REFERER"] == ""))
            Response.Redirect("~/Error.aspx");
        else
        {
            string http_ref = Request.ServerVariables["HTTP_REFERER"].Trim();
            string http_hos = Request.ServerVariables["HTTP_HOST"].Trim();
            int len = http_hos.Length;
            if (http_ref.IndexOf(http_hos, 0) < 0)
                Response.Redirect("../Error.aspx");
        }
        PrevBrowCache.enforceNoCache();
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "8" || Session["RoleID"].ToString() == "9" || Session["RoleID"].ToString() == "10"))
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();

            Aocode = Session["AO_Code"].ToString();
            lblUser.Text = Session["Role"].ToString() + "  " + Session["DistName"].ToString() + "  " + Session["MandName"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

            if (!IsPostBack)
            {
                try
                {
                    random();
                
                    BindClass();
                    BindDistrict();
                    BindCrop();
                    Bindtestparamertes();
                    //lblcodesample.Visible = false;
                    //txtcodesample.Visible = false;

                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    cf.ShowAlertMessage(ex.ToString());
                }
            }
        }
        else
            Response.Redirect("../Error.aspx");
    }
    protected void BindClass()
    {
        objBE.Action = "CLS";
        objBE.Id = Department;
        objBE.CatId = Session["SampleCategory"].ToString();
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlClass, dt, "class_name", "class_id", "0");
    }

    protected void BindDistrict()
    {
        objBE.Action = "D";
        //objBE.statecode = ddlstate.SelectedValue;
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddldist, dt, "DistName", "DistCode", "0");
    }
    protected void BindMandal()
    {
        objBE.Action = "M";

        objBE.DistCode = ddldist.SelectedValue;
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddlmandal, dt, "MandName", "MandCode", "0");
    }

    protected void BindVillage()
    {
        objBE.Action = "V";
        objBE.DistCode = ddldist.SelectedValue;
        objBE.MandCode = ddlmandal.SelectedValue;
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddlvlg, dt, "VillName", "VillCode", "0");
    }
    protected void BindCrop()
    {
        objBE.Action = "R";
        objBE.Dept = Department;
        objBE.CatId = null;
        dt = ObjDL.CropIUDR(objBE, con);
        cf.BindDropDownLists(ddlcrop, dt, "CropName", "CropCode", "0");
    }
    protected void BindCropVariety()
    {
        objBE.Action = "R1";
        objBE.Dept = Department;
        objBE.Cropid = ddlcrop.SelectedValue;
        dt = ObjDL.CropVarietyIUDR(objBE, con);
        cf.BindDropDownLists(ddlcropvariety, dt, "CropVName", "CropVCode", "0");
    }
    protected void Bindtestparamertes()
    {
        objBE.Action = "R";
        objBE.Dept = Department;
        dt = ObjDL.TestIUDR(objBE, con);
        cf.BindCheckLists(chktestparametrs, dt, "TestName", "Testid", "0");
    }

    protected void ddlcrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCropVariety();
        if (ddlcrop.SelectedItem.Text == "Cotton")
        {


        }
    }
    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMandal();
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClass.SelectedItem.Text == "Vigilance")
            pnlvigilence.Visible = true;
        else
            pnlvigilence.Visible = false;

        if (ddlClass.SelectedItem.Text == "Act")
            txtcodesample.Enabled = true;
        else
            txtcodesample.Enabled = false;
    }
    protected void ddlmandal_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVillage();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (Validatesave())
            {

                DataTable dtest = new DataTable();
                dtest.Columns.Add("Testid", typeof(string));

                int j = 0;
                foreach (ListItem c in chktestparametrs.Items)
                {

                    if (c.Selected == true)
                    {
                        dtest.Rows.Add();
                        dtest.Rows[j]["Testid"] = c.Value;
                        j++;
                    }
                    //else
                    //{
                    //    cf.ShowAlertMessage("Pls Chek Test required(if any)");
                    //    return;
                    //}
                }


                //file punchanama

                if (Filepanchanama.HasFile)
                {
                    Stream fs = Filepanchanama.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string fileext = Path.GetExtension(Filepanchanama.PostedFile.FileName);
                    string mime = MimeType.GetMimeType(bytes, Filepanchanama.PostedFile.FileName);
                    if (fileext == ".jpeg" || fileext == ".JPG" || fileext == ".JPEG" || fileext == ".jpg" || fileext == ".PNG" || fileext == ".png" || fileext == ".pdf")
                    {
                        int len = Filepanchanama.PostedFile.ContentLength;
                        if ((len / 1024) > 5120)
                        {
                            cf.ShowAlertMessage("File size is exceeded");
                            Filepanchanama.Focus();
                        }
                        string filname = Path.GetFileName(Filepanchanama.PostedFile.FileName);

                        objR.punchanamafileType = fileext.ToString();
                        objR.punchanamafilenm = filname.ToString();
                        objR.punchanamasize = len.ToString();
                        objR.punchanamafilecontent = bytes;

                    }

                    else
                        cf.ShowAlertMessage("Invalid file");
                }
                else
                {
                    cf.ShowAlertMessage("Pls upload punchanama File");
                    return;
                }
                //file FIR
                if (FileFricopy.HasFile)
                {
                    Stream fs = FileFricopy.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string fileext = Path.GetExtension(FileFricopy.PostedFile.FileName);
                    string mime = MimeType.GetMimeType(bytes, FileFricopy.PostedFile.FileName);
                    if (fileext == ".jpeg" || fileext == ".JPG" || fileext == ".JPEG" || fileext == ".jpg" || fileext == ".PNG" || fileext == ".png" || fileext == ".pdf")
                    {
                        int len = FileFricopy.PostedFile.ContentLength;
                        if ((len / 1024) > 5120)
                        {
                            cf.ShowAlertMessage("File size is exceeded");
                            FileFricopy.Focus();
                        }
                        string filname = Path.GetFileName(FileFricopy.PostedFile.FileName);

                        objR.firfileType = fileext.ToString();
                        objR.firfilenm = filname.ToString();
                        objR.firsize = len.ToString();
                        objR.firfilecontent = bytes;

                    }

                    else
                        cf.ShowAlertMessage("Invalid file");
                }
                else
                {
                    cf.ShowAlertMessage("Pls upload FirCopy File");
                    return;
                }


                //file SHOCi
                if (FileSHOCI.HasFile)
                {
                    Stream fs = FileSHOCI.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string fileext = Path.GetExtension(FileSHOCI.PostedFile.FileName);
                    string mime = MimeType.GetMimeType(bytes, FileSHOCI.PostedFile.FileName);
                    if (fileext == ".jpeg" || fileext == ".JPG" || fileext == ".JPEG" || fileext == ".jpg" || fileext == ".PNG" || fileext == ".png" || fileext == ".pdf")
                    {
                        int len = FileSHOCI.PostedFile.ContentLength;
                        if ((len / 1024) > 5120)
                        {
                            cf.ShowAlertMessage("File size is exceeded");
                            FileSHOCI.Focus();
                        }
                        string filname = Path.GetFileName(FileSHOCI.PostedFile.FileName);

                        objR.shocifileType = fileext.ToString();
                        objR.shocifilenm = filname.ToString();
                        objR.shocisize = len.ToString();
                        objR.shocifilecontent = bytes;

                    }

                    else
                        cf.ShowAlertMessage("Invalid file");
                }
                else
                {
                    cf.ShowAlertMessage("Upload letter from SHO/CI of Police Department Copy ");
                    return;
                }

                objR.SampleClass = ddlClass.SelectedValue;
                objR.CodeSticker = txtcodesample.Text;
                objR.SmplCollectingDt = cf.Texttodateconverter(txtdateofcollection.Text.Trim());
                objR.District = ddldist.SelectedValue;
                objR.Mandal = ddlmandal.SelectedValue;
                objR.Village = ddlvlg.SelectedValue;
                objR.qtyPicked = txtqntysamplesubmit.Text;
                objR.Shociname = txtshorci.Text.Trim();
                objR.Address = txtposteladdress.Text.Trim();
                objR.Email = txtemail.Text.Trim();
                objR.Mobile = txtmobileno.Text.Trim();
                int crckind = 0;
                if (ddlkindVrtyofseed.SelectedValue == "HB")
                {
                    objR.Cropkind = "1";

                }
                else
                    objR.Cropkind = "0";
                // objR.Cropkind = ddlkindVrtyofseed.SelectedValue;
                objR.Cropcode = ddlcrop.SelectedValue;
                objR.CropVrcode = ddlcropvariety.SelectedValue;
                objR.Lotno = txtlotno.Text.Trim();
                objR.Lotquantity = txtquantitylot.Text.Trim();
                objR.Quantityofsamplesubmit = txtqntysamplesubmit.Text.Trim();
                objR.Orgin = ddlorginclass.SelectedValue;

                objR.ExpiryDate = cf.Texttodateconverter(txtdateexpiry.Text);
                objR.Remarks = txtremarks.Text.Trim();
                objR.SampleCategory = Session["SampleCategory"].ToString();
                objR.state = state;
                objR.login_state = state;
                objR.DI_AO_Code = Aocode;
                objR.dept = Department;
                objR.Action = "I";
                objR.TVP = dtest;
                dt = Regobjdl.SeedRegistration(objR, con);
                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage("Insert Failed");

                }
                else
                {
                    cf.ShowAlertMessage("Insert Succussfully");
                    Clear();

                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }

    }

    protected string SaveFile(FileUpload file)
    {
        string id = "";
        if (file.HasFile)
        {
            Stream fs = file.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            string fileext = Path.GetExtension(file.PostedFile.FileName);
            string mime = MimeType.GetMimeType(bytes, file.PostedFile.FileName);
            if (fileext == ".jpeg" || fileext == ".JPG" || fileext == ".JPEG" || fileext == ".jpg" || fileext == ".PNG" || fileext == ".png" || fileext == ".pdf")
            {
                int len = file.PostedFile.ContentLength;
                if ((len / 1024) > 5120)
                {
                    cf.ShowAlertMessage("File size is exceeded");
                    file.Focus();
                }
                else
                {
                    string filname = Path.GetFileName(file.PostedFile.FileName);
                    // dt = Regobjdl.SaveFile(fileext, filname, len.ToString(), bytes, type, con);
                    //objR.fileType = fileext.ToString();
                    //objR.filenm = filname.ToString();
                    //objR.size = len.ToString();
                    //objR.file = bytes;
                    //objR.Action = "I";
                    dt = Regobjdl.SeedRegistration(objR, con);


                    if (dt.Rows.Count > 0)
                        id = dt.Rows[0][0].ToString();
                }
            }

            else
                cf.ShowAlertMessage("Invalid file");
        }
        else
            cf.ShowAlertMessage("upload File");
        return id;
    }

    protected void Clear()
    {
        txtcodesample.Text = "";
        txtdateofcollection.Text = "";
        ddlClass.ClearSelection();
        ddldist.ClearSelection();
        ddlmandal.ClearSelection();
        ddlvlg.ClearSelection();
        ddlcrop.ClearSelection();
        ddlcropvariety.ClearSelection();
        txtlotno.Text = "";
        txtquantitylot.Text = "";
        ddlorginclass.ClearSelection();
        txtdateexpiry.Text = "";
        chktestparametrs.ClearSelection();
        txtremarks.Text = "";

        ddlkindVrtyofseed.ClearSelection();
        txtemail.Text = "";
        txtmobileno.Text = "";
        txtposteladdress.Text = "";
        txtshorci.Text = "";
        txtqntysamplesubmit.Text = "";

    }
    public bool Validatesave()
    {
        if (ddlClass.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Class Name");
            ddlClass.Focus();
            return false;
        }
        if (ddlClass.SelectedItem.Text == "Act")
        {
            if (txtcodesample.Text == "")
            {
                cf.ShowAlertMessage("Enter Code No. of Sample");
                txtcodesample.Focus();
                return false;
            }
            if (txtcodesample.Text != "")
            {
                bool val;
                val = obj.CheckInput_new(txtcodesample.Text);
                if (val == true)
                    Response.Redirect("~/Error.aspx");
            }
        }
        if (ddlClass.SelectedItem.Text == "Vigilance")
        {
            if (txtshorci.Text == "")
            {
                cf.ShowAlertMessage("Enter Name of SHO/CI");
                txtshorci.Focus();
                return false;
            }
            if (txtshorci.Text != "")
            {
                bool val;
                val = obj.CheckInput_new(txtshorci.Text);
                if (val == true)
                    Response.Redirect("~/Error.aspx");
            }
            if (txtposteladdress.Text == "")
            {
                cf.ShowAlertMessage("Enter Postel Address ");
                txtposteladdress.Focus();
                return false;
            }
            if (txtposteladdress.Text != "")
            {
                bool val;
                val = obj.CheckInput_new(txtposteladdress.Text);
                if (val == true)
                    Response.Redirect("~/Error.aspx");
            }
            if (txtmobileno.Text == "")
            {
                cf.ShowAlertMessage("Enter Mobile Number");
                txtmobileno.Focus();
                return false;
            }
            if (txtmobileno.Text != "")
            {
                bool val;
                val = obj.CheckInput_new(txtmobileno.Text);
                if (val == true)
                    Response.Redirect("~/Error.aspx");
            }
            if (txtemail.Text == "")
            {
                cf.ShowAlertMessage("Enter Email id");
                txtemail.Focus();
                return false;
            }
            if (txtemail.Text != "")
            {
                bool val;
                val = obj.CheckInput_new(txtemail.Text);
                if (val == true)
                    Response.Redirect("~/Error.aspx");
            }

        }

        if (txtdateofcollection.Text == "")
        {
            cf.ShowAlertMessage("Select Date");
            txtdateofcollection.Focus();
            return false;
        }
        if (txtdateofcollection.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtdateofcollection.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (ddldist.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select District Name");
            ddldist.Focus();
            return false;
        }
        if (ddlmandal.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Mandal Name");
            ddlmandal.Focus();
            return false;
        }


        if (ddlkindVrtyofseed.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Kind and Variety of seed");
            ddlkindVrtyofseed.Focus();
            return false;
        }
        if (ddlcrop.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Crop Name");
            ddlcrop.Focus();
            return false;
        }
        if (ddlcropvariety.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Crop Variety Name");
            ddlcropvariety.Focus();
            return false;
        }
        if (txtlotno.Text == "")
        {
            cf.ShowAlertMessage("Enter Quantity of seed in the lott");
            txtlotno.Focus();
            return false;
        }
        if (txtlotno.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtlotno.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtquantitylot.Text == "")
        {
            cf.ShowAlertMessage("Enter Quantity");
            txtquantitylot.Focus();
            return false;
        }
        if (ddlorginclass.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Orgin or Class of seed *");
            ddlorginclass.Focus();
            return false;
        }

        //if (txtvldty.Text.Trim() != "")
        //{

        //    bool val;
        //    val = obj.CheckInput_new(txtvldty.Text);
        //    if (val == true)
        //        Response.Redirect("~/Error.aspx");

        //    if (!objValidate.IsDate(txtvldty.Text.Trim()))
        //    {
        //        cf.ShowAlertMessage("Enter Valid  Date");
        //        txtvldty.Focus();
        //        return false;
        //    }
        //}


        if (txtdateexpiry.Text == "")
        {
            cf.ShowAlertMessage("Select Expiry Date");
            txtdateexpiry.Focus();
            return false;
        }


        //if (chktestparametrs.SelectedIndex == 0)
        //{
        //    cf.ShowAlertMessage("Select Test required(if any)");
        //    chktestparametrs.Focus();
        //    return false;
        //}
        if (txtremarks.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Remarks");
            txtremarks.Focus();
            return false;
        }

        if (txtremarks.Text != "")
        {


            bool val;
            val = obj.CheckInput_new(txtremarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        //if (txtdtsam.Text.Trim() != "")
        //{

        //    bool val;
        //    val = obj.CheckInput_new(txtdtsam.Text);
        //    if (val == true)
        //        Response.Redirect("~/Error.aspx");

        //    if (!objValidate.IsDate(txtdtsam.Text.Trim()))
        //    {
        //        cf.ShowAlertMessage("Enter Valid  Date");
        //        txtdtsam.Focus();
        //        return false;
        //    }
        //}




        return true;
    }

    public void random()
    {
        try
        {
            string strString = "abcdefghijklmnpqrstuvwxyzABCDQEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string num = "";
            Random rm = new Random();
            for (int i = 0; i < 16; i++)
            {
                int randomcharindex = rm.Next(0, strString.Length);
                char randomchar = strString[randomcharindex];
                num += Convert.ToString(randomchar);
            }

            Response.Cookies.Add(new HttpCookie("ASPFIXATION2", num));
            Session["hf"] = num;
            Session["ASPFIXATION2"] = num;
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Error.aspx");
        }
    }

    public void check()
    {
        try
        {
            string cookie_value = null;
            string session_value = null;
            //cookie_value = System.Web.HttpContext.Current.Request.Cookies["ASPFIXATION2"].Value;
            cookie_value = Session["hf"].ToString();
            session_value = System.Web.HttpContext.Current.Session["ASPFIXATION2"].ToString();
            if (cookie_value != session_value)
            {
                System.Web.HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.Redirect("~/Error.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Error.aspx");
        }
    }
}