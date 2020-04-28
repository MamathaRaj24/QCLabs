using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using QC_BE;

namespace QC_DL
{
    public class AgriDL
    {
        public DataTable SampleRegistration(AgriBE objbe, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SampleRegistration_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Di_Id", SqlDbType.VarChar).Value = objbe.DI_AO_Code;
                    da.SelectCommand.Parameters.Add("@SampleClass", SqlDbType.VarChar).Value = objbe.SampleClass;
                    da.SelectCommand.Parameters.Add("@Usage", SqlDbType.VarChar).Value = objbe.Usage;
                    da.SelectCommand.Parameters.Add("@Priority", SqlDbType.VarChar).Value = objbe.Priority;
                    da.SelectCommand.Parameters.Add("@PaymentType", SqlDbType.VarChar).Value = objbe.Paymenttype;
                    da.SelectCommand.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = objbe.PriorityRemarks;
                    da.SelectCommand.Parameters.Add("@FirmType", SqlDbType.VarChar).Value = objbe.FirmType;
                    da.SelectCommand.Parameters.Add("@Firm_Name", SqlDbType.VarChar).Value = objbe.Firm_Name;
                    da.SelectCommand.Parameters.Add("@LicenceNo", SqlDbType.VarChar).Value = objbe.LicenceNo;
                    da.SelectCommand.Parameters.Add("@Validity", SqlDbType.Date).Value = objbe.Validity;
                    da.SelectCommand.Parameters.Add("@contactPerson", SqlDbType.VarChar).Value = objbe.ContactPerson;
                    da.SelectCommand.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = objbe.Mobile;
                    da.SelectCommand.Parameters.Add("@qtyPicked", SqlDbType.VarChar).Value = objbe.SampleQty;
                    da.SelectCommand.Parameters.Add("@StkRcvdFrom", SqlDbType.VarChar).Value = objbe.StkRcvdFrom;
                    da.SelectCommand.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = objbe.InvoiceNo;
                    da.SelectCommand.Parameters.Add("@FairPrice", SqlDbType.VarChar).Value = objbe.FairPrice;
                    da.SelectCommand.Parameters.Add("@InvoiceDate", SqlDbType.Date).Value = objbe.InvoiceDate;
                    da.SelectCommand.Parameters.Add("@AadharNo", SqlDbType.VarChar).Value = objbe.Aadharno;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = objbe.state;
                    da.SelectCommand.Parameters.Add("@District", SqlDbType.VarChar).Value = objbe.District;
                    da.SelectCommand.Parameters.Add("@Mandal", SqlDbType.VarChar).Value = objbe.Mandal;
                    da.SelectCommand.Parameters.Add("@HouseNo", SqlDbType.VarChar).Value = objbe.HouseNo;
                    da.SelectCommand.Parameters.Add("@Locality", SqlDbType.VarChar).Value = objbe.Locality;
                    da.SelectCommand.Parameters.Add("@emailid", SqlDbType.VarChar).Value = objbe.Email;
                    da.SelectCommand.Parameters.Add("@punchnama", SqlDbType.VarChar).Value = objbe.punchnama;
                    da.SelectCommand.Parameters.Add("@SampleCollDate", SqlDbType.Date).Value = objbe.SmplCollectingDt;
                    da.SelectCommand.Parameters.Add("@SampleCategory", SqlDbType.VarChar).Value = objbe.SampleCategory;
                    da.SelectCommand.Parameters.Add("@SampleType_ID", SqlDbType.VarChar).Value = objbe.SampleType;
                    da.SelectCommand.Parameters.Add("@Sample_ID", SqlDbType.VarChar).Value = objbe.SampleID;
                    da.SelectCommand.Parameters.Add("@GenericName", SqlDbType.VarChar).Value = objbe.GenericName;
                    da.SelectCommand.Parameters.Add("@TradeName", SqlDbType.VarChar).Value = objbe.TradeName;
                    da.SelectCommand.Parameters.Add("@SampleQty", SqlDbType.VarChar).Value = objbe.SampleQty;
                    da.SelectCommand.Parameters.Add("@Composition", SqlDbType.VarChar).Value = objbe.Composition;
                    da.SelectCommand.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = objbe.BatchNo;
                    da.SelectCommand.Parameters.Add("@StockPosition", SqlDbType.VarChar).Value = objbe.StockPosition;
                    da.SelectCommand.Parameters.Add("@ManufacturingDate", SqlDbType.VarChar).Value = objbe.ManufacturingDate;
                    da.SelectCommand.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = objbe.ExpiryDate;
                    da.SelectCommand.Parameters.Add("@StkRcvdDate", SqlDbType.Date).Value = objbe.StkRcvdDate;
                    da.SelectCommand.Parameters.Add("@ManfucaturerName", SqlDbType.VarChar).Value = objbe.ManfucaturerName;
                    da.SelectCommand.Parameters.Add("@ManufacutrerLicence", SqlDbType.VarChar).Value = objbe.ManufacutrerLicence;
                    da.SelectCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = objbe.Address;
                    da.SelectCommand.Parameters.Add("@ManufacturerState", SqlDbType.VarChar).Value = objbe.ManufacturerState;
                    da.SelectCommand.Parameters.Add("@SRegid", SqlDbType.VarChar).Value = objbe.SRegid;
                    da.SelectCommand.Parameters.Add("@loginState", SqlDbType.VarChar).Value = objbe.login_state;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = objbe.dept;
                    da.SelectCommand.Parameters.Add("@Memoid", SqlDbType.VarChar).Value = objbe.MemoId;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = objbe.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable MemoID(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {

                using (SqlDataAdapter da = new SqlDataAdapter("MemoIDGeneration", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@tvpmemo", SqlDbType.Structured).Value = obj.TVP;
                    da.SelectCommand.Parameters.Add("@SampleID", SqlDbType.VarChar).Value = obj.SampleID;
                    // da.SelectCommand.Parameters.Add("@SampleCategory", SqlDbType.VarChar).Value = obj.SampleCategory;
                    da.SelectCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = obj.UserId;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = obj.state;
                    da.SelectCommand.Parameters.Add("@Depatment", SqlDbType.VarChar).Value = obj.dept;
                    da.SelectCommand.Parameters.Add("@Catid", SqlDbType.VarChar).Value = obj.SampleCategory;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable CourierIUDR(AgriBE objBE, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("CourierDetails_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = objBE.TVP;
                    da.SelectCommand.Parameters.Add("@SampleCat", SqlDbType.VarChar).Value = objBE.SampleCategory;
                    da.SelectCommand.Parameters.Add("@ack", SqlDbType.Structured).Value = objBE.AckTVP;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = objBE.dept;
                    da.SelectCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = objBE.UserId;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = objBE.user;
                    da.SelectCommand.Parameters.Add("@memoid", SqlDbType.VarChar).Value = objBE.MemoId;
                    da.SelectCommand.Parameters.Add("@ACTION", SqlDbType.VarChar).Value = objBE.Action;
                    da.SelectCommand.Parameters.Add("@Freeze", SqlDbType.VarChar).Value = objBE.tvpfreeze_UO;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        //PrintForm1718 --sampath
        public DataTable PrintForm1718(AgriBE objBE, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("PrintMemoNForms", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@RegID", SqlDbType.VarChar).Value = objBE.SRegid;
                    da.SelectCommand.Parameters.Add("@MemoID", SqlDbType.VarChar).Value = objBE.MemoId;
                    da.SelectCommand.Parameters.Add("@zone", SqlDbType.VarChar).Value = objBE.ZoneCode;
                    da.SelectCommand.Parameters.Add("@labid", SqlDbType.VarChar).Value = objBE.labID;
                    da.SelectCommand.Parameters.Add("@SCategory", SqlDbType.VarChar).Value = objBE.SampleCategory;
                    da.SelectCommand.Parameters.Add("@month", SqlDbType.VarChar).Value = objBE.ManufacturingDate;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = objBE.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable FreezeSamples(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("FreezeSamples", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = obj.TVP;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        //UserRegistration --sampath
        public DataTable UserRegistration_IUDR(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("UserReg_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = obj.Name;
                    da.SelectCommand.Parameters.Add("@ShortName", SqlDbType.VarChar).Value = obj.ShortName;
                    da.SelectCommand.Parameters.Add("@Depatment", SqlDbType.VarChar).Value = obj.dept;
                    da.SelectCommand.Parameters.Add("@Role", SqlDbType.VarChar).Value = obj.UserType;
                    da.SelectCommand.Parameters.Add("@Scheme", SqlDbType.VarChar).Value = obj.Scheme;
                    da.SelectCommand.Parameters.Add("@zoneCd", SqlDbType.VarChar).Value = obj.ZoneCode;
                    da.SelectCommand.Parameters.Add("@TestLab", SqlDbType.VarChar).Value = obj.TestLab;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = obj.state;
                    da.SelectCommand.Parameters.Add("@DistCode", SqlDbType.VarChar).Value = obj.District;
                    da.SelectCommand.Parameters.Add("@MandCode", SqlDbType.VarChar).Value = obj.Mandal;
                    //da.SelectCommand.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = obj.Village;
                    da.SelectCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = obj.user;
                    da.SelectCommand.Parameters.Add("@HNO", SqlDbType.VarChar).Value = obj.HouseNo;
                    da.SelectCommand.Parameters.Add("@Locality", SqlDbType.VarChar).Value = obj.Locality;
                    da.SelectCommand.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = obj.PinCode;
                    da.SelectCommand.Parameters.Add("@Ps_Limt", SqlDbType.VarChar).Value = obj.PsLimit;
                    da.SelectCommand.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = obj.Mobile;
                    da.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = obj.Email;
                    da.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = obj.UserName;
                    da.SelectCommand.Parameters.Add("@PWD", SqlDbType.VarChar).Value = obj.Password;
                    //da.SelectCommand.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = obj.UserId;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }




        public DataTable FertlizerRegistration(AgriBE objbe, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Fertlizer_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Di_Ao_Id", SqlDbType.VarChar).Value = objbe.DI_AO_Code;
                    da.SelectCommand.Parameters.Add("@SampleClass", SqlDbType.VarChar).Value = objbe.SampleClass;
                    da.SelectCommand.Parameters.Add("@Code_sticker", SqlDbType.VarChar).Value = objbe.CodeSticker;
                    da.SelectCommand.Parameters.Add("@LicenceNo", SqlDbType.VarChar).Value = objbe.LicenceNo;
                    da.SelectCommand.Parameters.Add("@Firm_Name", SqlDbType.VarChar).Value = objbe.Firm_Name;
                    da.SelectCommand.Parameters.Add("@FOwnerName", SqlDbType.VarChar).Value = objbe.ownername;
                    da.SelectCommand.Parameters.Add("@Validity", SqlDbType.Date).Value = objbe.Validity;
                    da.SelectCommand.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = objbe.BatchNo;
                    da.SelectCommand.Parameters.Add("@StockPosition", SqlDbType.VarChar).Value = objbe.StockPosition;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = objbe.state;
                    da.SelectCommand.Parameters.Add("@District", SqlDbType.VarChar).Value = objbe.District;
                    da.SelectCommand.Parameters.Add("@HouseNo", SqlDbType.VarChar).Value = objbe.HouseNo;
                    da.SelectCommand.Parameters.Add("@Locality", SqlDbType.VarChar).Value = objbe.Locality;
                    da.SelectCommand.Parameters.Add("@StkRcvdDate", SqlDbType.Date).Value = objbe.StkRcvdDate;
                    da.SelectCommand.Parameters.Add("@SampleCollDate", SqlDbType.Date).Value = objbe.SmplCollectingDt;
                    da.SelectCommand.Parameters.Add("@SampleType_ID", SqlDbType.VarChar).Value = objbe.SampleType;
                    da.SelectCommand.Parameters.Add("@Sample_ID", SqlDbType.VarChar).Value = objbe.SampleID;
                    da.SelectCommand.Parameters.Add("@PhysicalCondition", SqlDbType.VarChar).Value = objbe.PhycicalCondition;
                    da.SelectCommand.Parameters.Add("@ManufacturingDate", SqlDbType.VarChar).Value = objbe.ManufacturingDate;
                    da.SelectCommand.Parameters.Add("@ManfucaturerName", SqlDbType.VarChar).Value = objbe.ManfucaturerName;
                    da.SelectCommand.Parameters.Add("@ManufacturerState", SqlDbType.VarChar).Value = objbe.ManufacturerState;
                    da.SelectCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = objbe.Address;
                    da.SelectCommand.Parameters.Add("@drawBag", SqlDbType.VarChar).Value = objbe.SmplDrawnBag;
                    da.SelectCommand.Parameters.Add("@punchnama", SqlDbType.VarChar).Value = objbe.punchnama;
                    da.SelectCommand.Parameters.Add("@SampleCategory", SqlDbType.VarChar).Value = objbe.SampleCategory;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = objbe.dept;
                    da.SelectCommand.Parameters.Add("@loginState", SqlDbType.VarChar).Value = objbe.login_state;

                    da.SelectCommand.Parameters.Add("@SRegid", SqlDbType.VarChar).Value = objbe.SRegid;
                    da.SelectCommand.Parameters.Add("@Memoid", SqlDbType.VarChar).Value = objbe.MemoId;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = objbe.TVP;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = objbe.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }






        public DataTable DashBoard(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("DashBoard", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.dept;
                    da.SelectCommand.Parameters.Add("@DI", SqlDbType.VarChar).Value = obj.DI_AO_Code;
                    da.SelectCommand.Parameters.Add("@SampleId", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@Status", SqlDbType.VarChar).Value = obj.status;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable PrintForm13(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("AnalystAction", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@sampleid", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        //Analyst Dashboard  sampath
        public DataTable PesticideRegistration(AgriBE objbe, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("PesticideRegistration_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Di_Ao_Id", SqlDbType.VarChar).Value = objbe.DI_AO_Code;
                    da.SelectCommand.Parameters.Add("@SampleCategory", SqlDbType.VarChar).Value = objbe.SampleCategory;
                    da.SelectCommand.Parameters.Add("@SampleClass", SqlDbType.VarChar).Value = objbe.SampleClass;
                    da.SelectCommand.Parameters.Add("@Code_sticker", SqlDbType.VarChar).Value = objbe.CodeSticker;
                    da.SelectCommand.Parameters.Add("@Firm_Name", SqlDbType.VarChar).Value = objbe.Firm_Name;
                    da.SelectCommand.Parameters.Add("@contactPerson", SqlDbType.VarChar).Value = objbe.ContactPerson;
                    da.SelectCommand.Parameters.Add("@LicenceNo", SqlDbType.VarChar).Value = objbe.LicenceNo;
                    da.SelectCommand.Parameters.Add("@Validity", SqlDbType.Date).Value = objbe.Validity;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = objbe.state;
                    da.SelectCommand.Parameters.Add("@District", SqlDbType.VarChar).Value = objbe.District;
                    da.SelectCommand.Parameters.Add("@Mandal", SqlDbType.VarChar).Value = objbe.Mandal;
                    da.SelectCommand.Parameters.Add("@HouseNo", SqlDbType.VarChar).Value = objbe.HouseNo;
                    da.SelectCommand.Parameters.Add("@Locality", SqlDbType.VarChar).Value = objbe.Locality;
                    da.SelectCommand.Parameters.Add("@SampleCollDate", SqlDbType.Date).Value = objbe.SmplCollectingDt;
                    da.SelectCommand.Parameters.Add("@SampleType_ID", SqlDbType.VarChar).Value = objbe.SampleType;
                    da.SelectCommand.Parameters.Add("@Sample_ID", SqlDbType.VarChar).Value = objbe.SampleID;
                    da.SelectCommand.Parameters.Add("@Gaurentees_AI", SqlDbType.VarChar).Value = objbe.Gaurentees_AI;
                    da.SelectCommand.Parameters.Add("@TradeName", SqlDbType.VarChar).Value = objbe.TradeName;
                    da.SelectCommand.Parameters.Add("@SampleQty", SqlDbType.VarChar).Value = objbe.SampleQty;
                    da.SelectCommand.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = objbe.BatchNo;
                    da.SelectCommand.Parameters.Add("@StockPosition", SqlDbType.VarChar).Value = objbe.StockPosition;
                    da.SelectCommand.Parameters.Add("@ManufacturingDate", SqlDbType.VarChar).Value = objbe.ManufacturingDate;
                    da.SelectCommand.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = objbe.ExpiryDate;
                    da.SelectCommand.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = objbe.InvoiceNo;
                    da.SelectCommand.Parameters.Add("@InvoiceDate", SqlDbType.Date).Value = objbe.InvoiceDate;
                    da.SelectCommand.Parameters.Add("@StkRcvdDate", SqlDbType.Date).Value = objbe.StkRcvdDate;
                    da.SelectCommand.Parameters.Add("@StkRcvdFrom", SqlDbType.VarChar).Value = objbe.StkRcvdFrom;
                    da.SelectCommand.Parameters.Add("@ManfucaturerName", SqlDbType.VarChar).Value = objbe.ManfucaturerName;
                    da.SelectCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = objbe.Address;
                    da.SelectCommand.Parameters.Add("@Isi_mark", SqlDbType.VarChar).Value = objbe.ISImark;
                    da.SelectCommand.Parameters.Add("@MareterName", SqlDbType.VarChar).Value = objbe.MarketerName;
                    da.SelectCommand.Parameters.Add("@punchnama", SqlDbType.VarChar).Value = objbe.punchnama;
                    da.SelectCommand.Parameters.Add("@SRegid", SqlDbType.VarChar).Value = objbe.SRegid;
                    da.SelectCommand.Parameters.Add("@loginState", SqlDbType.VarChar).Value = objbe.login_state;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = objbe.dept;
                    da.SelectCommand.Parameters.Add("@Memoid", SqlDbType.VarChar).Value = objbe.MemoId;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = objbe.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        //Analyst Dashboard  sampath
        public DataTable SeedRegistration(AgriBE objbe, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SeedRegistration_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Di_Ao_Id", SqlDbType.VarChar).Value = objbe.DI_AO_Code;
                    da.SelectCommand.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = objbe.Remarks;
                    da.SelectCommand.Parameters.Add("@Code_sticker", SqlDbType.VarChar).Value = objbe.CodeSticker;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = objbe.state;
                    da.SelectCommand.Parameters.Add("@District", SqlDbType.VarChar).Value = objbe.District;
                    da.SelectCommand.Parameters.Add("@Mandal", SqlDbType.VarChar).Value = objbe.Mandal;
                    da.SelectCommand.Parameters.Add("@Villcode", SqlDbType.VarChar).Value = objbe.Village;
                    da.SelectCommand.Parameters.Add("@SampleCollDate", SqlDbType.Date).Value = objbe.SmplCollectingDt;
                    da.SelectCommand.Parameters.Add("@Sample_ID", SqlDbType.VarChar).Value = objbe.SampleID;
                    da.SelectCommand.Parameters.Add("@SampleQty", SqlDbType.VarChar).Value = objbe.SampleQty;
                    da.SelectCommand.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = objbe.ExpiryDate;
                    da.SelectCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = objbe.Address;
                    da.SelectCommand.Parameters.Add("@Cropid", SqlDbType.VarChar).Value = objbe.Cropcode;
                    da.SelectCommand.Parameters.Add("@CropVarietyid", SqlDbType.VarChar).Value = objbe.CropVrcode;
                    da.SelectCommand.Parameters.Add("@Lotno", SqlDbType.VarChar).Value = objbe.Lotno;
                    da.SelectCommand.Parameters.Add("@Orgin", SqlDbType.VarChar).Value = objbe.Orgin;
                    da.SelectCommand.Parameters.Add("@Testid", SqlDbType.VarChar).Value = objbe.Testid;
                    da.SelectCommand.Parameters.Add("@SampleCategory", SqlDbType.VarChar).Value = objbe.SampleCategory;
                    da.SelectCommand.Parameters.Add("@tvpseed", SqlDbType.Structured).Value = objbe.TVP;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = objbe.dept;
                    da.SelectCommand.Parameters.Add("@SRegid", SqlDbType.VarChar).Value = objbe.SRegid;
                    da.SelectCommand.Parameters.Add("@loginState", SqlDbType.VarChar).Value = objbe.login_state;
                    da.SelectCommand.Parameters.Add("@Memoid", SqlDbType.VarChar).Value = objbe.MemoId;
                    da.SelectCommand.Parameters.Add("@mobile", SqlDbType.VarChar).Value = objbe.Mobile;
                    da.SelectCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = objbe.Email;
                    da.SelectCommand.Parameters.Add("@Kindvariety", SqlDbType.VarChar).Value = objbe.Cropkind;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = objbe.Action;
                    da.SelectCommand.Parameters.Add("@namesho", SqlDbType.VarChar).Value = objbe.Shociname;
                    da.SelectCommand.Parameters.Add("@P_filetype", SqlDbType.VarChar).Value = objbe.punchanamafileType;
                    da.SelectCommand.Parameters.Add("@P_size", SqlDbType.VarChar).Value = objbe.punchanamasize;
                    da.SelectCommand.Parameters.Add("@P_filename", SqlDbType.VarChar).Value = objbe.punchanamafilenm;
                    //da.SelectCommand.Parameters.Add("@sam", SqlDbType.VarChar).Value =id;
                    da.SelectCommand.Parameters.Add("@type", SqlDbType.VarChar).Value = objbe.type; ;
                    da.SelectCommand.Parameters.AddWithValue("@P_filecontent", objbe.punchanamafilecontent);
                    da.SelectCommand.Parameters.Add("@Fir_filetype", SqlDbType.VarChar).Value = objbe.firfileType;
                    da.SelectCommand.Parameters.Add("@Fir_size", SqlDbType.VarChar).Value = objbe.firsize;
                    da.SelectCommand.Parameters.Add("@Fir_filename", SqlDbType.VarChar).Value = objbe.firfilenm;
                    da.SelectCommand.Parameters.AddWithValue("@Fir_filecontent", objbe.firfilecontent);
                    da.SelectCommand.Parameters.Add("@Ltr_filetype", SqlDbType.VarChar).Value = objbe.shocifileType;
                    da.SelectCommand.Parameters.Add("@Ltr_size", SqlDbType.VarChar).Value = objbe.shocisize;
                    da.SelectCommand.Parameters.Add("@Ltr_filename", SqlDbType.VarChar).Value = objbe.shocifilenm;
                    da.SelectCommand.Parameters.AddWithValue("@Ltr_filecontent", objbe.shocifilecontent);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        //Analyst Dashboard  sampath
        public DataTable AnalystDashBoard(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("DCA_Dashboard", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = obj.AnalystId;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    da.SelectCommand.Parameters.Add("@SubAction", SqlDbType.VarChar).Value = obj.COAction;
                    da.SelectCommand.Parameters.Add("@FromDt", SqlDbType.Date).Value = obj.Entry_Dt;
                    da.SelectCommand.Parameters.Add("@toDt", SqlDbType.Date).Value = obj.ExpiryDate;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        //qrcode agri sampath
        public DataTable GenerateSticker_AGRI(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("GenerateSticker_Agri", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Memoid", SqlDbType.VarChar).Value = obj.MemoId;
                    da.SelectCommand.Parameters.Add("@sampleid", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@qrcode", SqlDbType.VarBinary).Value = obj.qrcode;
                    da.SelectCommand.Parameters.Add("@Labid", SqlDbType.VarChar).Value = obj.labID;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.dept;
                    da.SelectCommand.Parameters.Add("@Status", SqlDbType.VarChar).Value = obj.status;
                    da.SelectCommand.Parameters.Add("@rejectedresons", SqlDbType.VarChar).Value = obj.Ref;
                    da.SelectCommand.Parameters.Add("@Codingofcrid", SqlDbType.VarChar).Value = obj.UserId;
                    da.SelectCommand.Parameters.Add("@SampleCategory", SqlDbType.VarChar).Value = obj.SampleCategory;
                    da.SelectCommand.Parameters.Add("@Sampletype", SqlDbType.VarChar).Value = obj.SampleType;
                    da.SelectCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = obj.UserId;
                    if (obj.TVP != null)
                        da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = obj.TVP;
                    if (obj.AckTVP != null)
                        da.SelectCommand.Parameters.Add("@tvplab", SqlDbType.Structured).Value = obj.AckTVP;
                    if (obj.TVpstiker != null)
                        da.SelectCommand.Parameters.Add("@stick", SqlDbType.Structured).Value = obj.TVpstiker;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        //Equipement Details sampath
        public DataTable Equipemntdetails(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SP_Equipmentdtls_Agri", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@SampleCategory", SqlDbType.VarChar).Value = obj.Category;
                    da.SelectCommand.Parameters.Add("@Sampletypeid", SqlDbType.VarChar).Value = obj.SampleType;
                    da.SelectCommand.Parameters.Add("@Donotallot", SqlDbType.VarChar).Value = obj.type;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = obj.dept;
                    da.SelectCommand.Parameters.Add("@ip", SqlDbType.VarChar).Value = obj.ip;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.user;
                    da.SelectCommand.Parameters.Add("@labcode", SqlDbType.VarChar).Value = obj.labID;
                    if (obj.TVP != null)
                        da.SelectCommand.Parameters.Add("@Tvpequipment", SqlDbType.Structured).Value = obj.TVP;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }



    }
}
