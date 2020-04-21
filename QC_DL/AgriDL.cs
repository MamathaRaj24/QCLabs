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
        //DEO
        public DataTable SampleRegistrationDI(AgriBE objbe, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SampelReg_DI", con))
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
                    da.SelectCommand.Parameters.Add("@qtyPicked", SqlDbType.VarChar).Value = objbe.qtyPicked;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = objbe.state;
                    da.SelectCommand.Parameters.Add("@District", SqlDbType.VarChar).Value = objbe.District;
                    da.SelectCommand.Parameters.Add("@Mandal", SqlDbType.VarChar).Value = objbe.Mandal;
                    da.SelectCommand.Parameters.Add("@HouseNo", SqlDbType.VarChar).Value = objbe.HouseNo;
                    da.SelectCommand.Parameters.Add("@Locality", SqlDbType.VarChar).Value = objbe.Locality;
                    da.SelectCommand.Parameters.Add("@SampleCategory", SqlDbType.VarChar).Value = objbe.SampleCategory;
                    da.SelectCommand.Parameters.Add("@SampleCollDate", SqlDbType.Date).Value = objbe.SmplCollectingDt;
                    da.SelectCommand.Parameters.Add("@GenericName", SqlDbType.VarChar).Value = objbe.GenericName;
                    da.SelectCommand.Parameters.Add("@TradeName", SqlDbType.VarChar).Value = objbe.TradeName;
                    da.SelectCommand.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = objbe.BatchNo;
                    da.SelectCommand.Parameters.Add("@ManufacturingDate", SqlDbType.VarChar).Value = objbe.ManufacturingDate;
                    da.SelectCommand.Parameters.Add("@ExpiryDate", SqlDbType.VarChar).Value = objbe.ExpiryDate;
                    da.SelectCommand.Parameters.Add("@StkRcvdDate", SqlDbType.Date).Value = objbe.StkRcvdDate;
                    da.SelectCommand.Parameters.Add("@SampleQty", SqlDbType.VarChar).Value = objbe.SampleQty;
                    da.SelectCommand.Parameters.Add("@Composition", SqlDbType.VarChar).Value = objbe.Composition;
                    da.SelectCommand.Parameters.Add("@ManufacturerState", SqlDbType.VarChar).Value = objbe.ManufacturerState;
                    da.SelectCommand.Parameters.Add("@ManfucaturerName", SqlDbType.VarChar).Value = objbe.ManfucaturerName;
                    da.SelectCommand.Parameters.Add("@ManufacutrerLicence", SqlDbType.VarChar).Value = objbe.ManufacutrerLicence;
                    da.SelectCommand.Parameters.Add("@ManufAddress", SqlDbType.VarChar).Value = objbe.ManufAddress;
                    da.SelectCommand.Parameters.Add("@MarketerState", SqlDbType.VarChar).Value = objbe.MarketerState;
                    da.SelectCommand.Parameters.Add("@MarketerName", SqlDbType.VarChar).Value = objbe.MarketerName;

                    da.SelectCommand.Parameters.Add("@MarketerAddress", SqlDbType.VarChar).Value = objbe.MarketerAddress;
                    da.SelectCommand.Parameters.Add("@loginState", SqlDbType.VarChar).Value = objbe.login_state;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = objbe.dept;
                    da.SelectCommand.Parameters.Add("@REGID", SqlDbType.VarChar).Value = objbe.SRegid;

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


        public DataTable ApproveUsers(DataTable dtusers, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Approve_User", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = dtusers;
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


        public DataTable SampleRegister(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SampleRegister", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.dept;
                    da.SelectCommand.Parameters.Add("@Memoid", SqlDbType.VarChar).Value = obj.MemoId;
                    da.SelectCommand.Parameters.Add("@SampleId", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@rcvdt", SqlDbType.VarChar).Value = obj.ReceiptDate;
                    da.SelectCommand.Parameters.Add("@sentby", SqlDbType.VarChar).Value = obj.DI_AO_Code;
                    da.SelectCommand.Parameters.Add("@category", SqlDbType.VarChar).Value = obj.SampleCategory;
                    da.SelectCommand.Parameters.Add("@Usage", SqlDbType.VarChar).Value = obj.Usage;
                    da.SelectCommand.Parameters.Add("@priority", SqlDbType.VarChar).Value = obj.Priority;
                    da.SelectCommand.Parameters.Add("@genericNm", SqlDbType.VarChar).Value = obj.GenericName;
                    da.SelectCommand.Parameters.Add("@priorityRemarks", SqlDbType.VarChar).Value = obj.PriorityRemarks;
                    da.SelectCommand.Parameters.Add("@SampleType", SqlDbType.VarChar).Value = obj.SampleType;
                    da.SelectCommand.Parameters.Add("@DrugName", SqlDbType.VarChar).Value = obj.DrugName;
                    da.SelectCommand.Parameters.Add("@qty", SqlDbType.VarChar).Value = obj.SampleQty;
                    da.SelectCommand.Parameters.Add("@batch", SqlDbType.VarChar).Value = obj.BatchNo;
                    da.SelectCommand.Parameters.Add("@maufnNm", SqlDbType.VarChar).Value = obj.ManfucaturerName;
                    da.SelectCommand.Parameters.Add("@licenseNo", SqlDbType.VarChar).Value = obj.LicenceNo;
                    da.SelectCommand.Parameters.Add("@marketBy", SqlDbType.VarChar).Value = obj.DealerName;
                    da.SelectCommand.Parameters.Add("@mdt", SqlDbType.VarChar).Value = obj.ManufacturingDate;
                    da.SelectCommand.Parameters.Add("@composition", SqlDbType.VarChar).Value = obj.Composition;
                    da.SelectCommand.Parameters.Add("@ExpDt", SqlDbType.VarChar).Value = obj.ExpiryDate;
                    da.SelectCommand.Parameters.Add("@userid", SqlDbType.VarChar).Value = obj.UserId;
                    da.SelectCommand.Parameters.Add("@remarks", SqlDbType.VarChar).Value = obj.Remarks;
                    da.SelectCommand.Parameters.Add("@COAction", SqlDbType.VarChar).Value = obj.COAction;
                    da.SelectCommand.Parameters.Add("@status", SqlDbType.VarChar).Value = obj.status;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable GenerateSticker(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("GenerateSticker", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Memoid", SqlDbType.VarChar).Value = obj.MemoId;
                    da.SelectCommand.Parameters.Add("@sampleid", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@qrcode", SqlDbType.VarBinary).Value = obj.qrcode;
                    da.SelectCommand.Parameters.Add("@Labid", SqlDbType.VarChar).Value = obj.labID;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.dept;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable UOAction(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("UnitOfficer", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@ack", SqlDbType.VarChar).Value = obj.ack;
                    da.SelectCommand.Parameters.Add("@sampleid", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@Labid", SqlDbType.VarChar).Value = obj.labID;
                    da.SelectCommand.Parameters.Add("@AnalystID", SqlDbType.VarChar).Value = obj.AnalystId;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = obj.TVP;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable JAAction(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("AnalystAction", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@status", SqlDbType.VarChar).Value = obj.status;
                    da.SelectCommand.Parameters.Add("@sampleid", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@AnalystID", SqlDbType.VarChar).Value = obj.AnalystId;
                    da.SelectCommand.Parameters.Add("@startdt", SqlDbType.VarChar).Value = obj.startdt;
                    da.SelectCommand.Parameters.Add("@enddt", SqlDbType.VarChar).Value = obj.enddt;
                    da.SelectCommand.Parameters.Add("@remarks", SqlDbType.VarChar).Value = obj.Remarks;
                    da.SelectCommand.Parameters.Add("@desc", SqlDbType.VarChar).Value = obj.description;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = obj.AckTVP;
                    da.SelectCommand.Parameters.Add("@result", SqlDbType.Structured).Value = obj.TVP;
                    //S1
                    da.SelectCommand.Parameters.Add("@s11f", SqlDbType.VarChar).Value = obj.S1Found1;
                    da.SelectCommand.Parameters.Add("@s11c", SqlDbType.VarChar).Value = obj.S1Claim1;
                    da.SelectCommand.Parameters.Add("@s11p", SqlDbType.VarChar).Value = obj.S1Percentage1;
                    da.SelectCommand.Parameters.Add("@s11cal", SqlDbType.VarChar).Value = obj.S1Calculation1;
                    da.SelectCommand.Parameters.Add("@s12f", SqlDbType.VarChar).Value = obj.S1Found2;
                    da.SelectCommand.Parameters.Add("@s12c", SqlDbType.VarChar).Value = obj.S1Claim2;
                    da.SelectCommand.Parameters.Add("@s12p", SqlDbType.VarChar).Value = obj.S1Percentage2;
                    da.SelectCommand.Parameters.Add("@s12cal", SqlDbType.VarChar).Value = obj.S1Calculation2;
                    da.SelectCommand.Parameters.Add("@s13f", SqlDbType.VarChar).Value = obj.S1Found3;
                    da.SelectCommand.Parameters.Add("@s13c", SqlDbType.VarChar).Value = obj.S1Claim3;
                    da.SelectCommand.Parameters.Add("@s13p", SqlDbType.VarChar).Value = obj.S1Percentage3;
                    da.SelectCommand.Parameters.Add("@s13cal", SqlDbType.VarChar).Value = obj.S1Calculation3;

                    da.SelectCommand.Parameters.Add("@s14f", SqlDbType.VarChar).Value = obj.S1Found4;
                    da.SelectCommand.Parameters.Add("@s14c", SqlDbType.VarChar).Value = obj.S1Claim4;
                    da.SelectCommand.Parameters.Add("@s14p", SqlDbType.VarChar).Value = obj.S1Percentage4;
                    da.SelectCommand.Parameters.Add("@s14cal", SqlDbType.VarChar).Value = obj.S1Calculation4;

                    da.SelectCommand.Parameters.Add("@s15f", SqlDbType.VarChar).Value = obj.S1Found5;
                    da.SelectCommand.Parameters.Add("@s15c", SqlDbType.VarChar).Value = obj.S1Claim5;
                    da.SelectCommand.Parameters.Add("@s15p", SqlDbType.VarChar).Value = obj.S1Percentage5;
                    da.SelectCommand.Parameters.Add("@s15cal", SqlDbType.VarChar).Value = obj.S1Calculation5;

                    da.SelectCommand.Parameters.Add("@s16f", SqlDbType.VarChar).Value = obj.S1Found6;
                    da.SelectCommand.Parameters.Add("@s16c", SqlDbType.VarChar).Value = obj.S1Claim6;
                    da.SelectCommand.Parameters.Add("@s16p", SqlDbType.VarChar).Value = obj.S1Percentage6;
                    da.SelectCommand.Parameters.Add("@s16cal", SqlDbType.VarChar).Value = obj.S1Calculation6;
                    //S2
                    da.SelectCommand.Parameters.Add("@s21f", SqlDbType.VarChar).Value = obj.S2Found1;
                    da.SelectCommand.Parameters.Add("@s21c", SqlDbType.VarChar).Value = obj.S2Claim1;
                    da.SelectCommand.Parameters.Add("@s21p ", SqlDbType.VarChar).Value = obj.S2Percentage1;
                    da.SelectCommand.Parameters.Add("@s21cal", SqlDbType.VarChar).Value = obj.S2Calculation1;
                    da.SelectCommand.Parameters.Add("@s22f", SqlDbType.VarChar).Value = obj.S2Found2;
                    da.SelectCommand.Parameters.Add("@s22c", SqlDbType.VarChar).Value = obj.S2Claim2;
                    da.SelectCommand.Parameters.Add("@s22p", SqlDbType.VarChar).Value = obj.S2Percentage2;
                    da.SelectCommand.Parameters.Add("@s22cal", SqlDbType.VarChar).Value = obj.S2Calculation2;
                    da.SelectCommand.Parameters.Add("@s23f", SqlDbType.VarChar).Value = obj.S2Found3;
                    da.SelectCommand.Parameters.Add("@s23c", SqlDbType.VarChar).Value = obj.S2Claim3;
                    da.SelectCommand.Parameters.Add("@s23p", SqlDbType.VarChar).Value = obj.S2Percentage3;
                    da.SelectCommand.Parameters.Add("@s23cal", SqlDbType.VarChar).Value = obj.S2Calculation3;
                    da.SelectCommand.Parameters.Add("@s24f", SqlDbType.VarChar).Value = obj.S2Found4;
                    da.SelectCommand.Parameters.Add("@s24c", SqlDbType.VarChar).Value = obj.S2Claim4;
                    da.SelectCommand.Parameters.Add("@s24p", SqlDbType.VarChar).Value = obj.S2Percentage4;
                    da.SelectCommand.Parameters.Add("@s24cal", SqlDbType.VarChar).Value = obj.S2Calculation4;
                    da.SelectCommand.Parameters.Add("@s25f", SqlDbType.VarChar).Value = obj.S2Found5;
                    da.SelectCommand.Parameters.Add("@s25c", SqlDbType.VarChar).Value = obj.S2Claim5;
                    da.SelectCommand.Parameters.Add("@s25p", SqlDbType.VarChar).Value = obj.S2Percentage5;
                    da.SelectCommand.Parameters.Add("@s25cal", SqlDbType.VarChar).Value = obj.S2Calculation5;
                    da.SelectCommand.Parameters.Add("@s26f", SqlDbType.VarChar).Value = obj.S2Found6;
                    da.SelectCommand.Parameters.Add("@s26c", SqlDbType.VarChar).Value = obj.S2Claim6;
                    da.SelectCommand.Parameters.Add("@s26p", SqlDbType.VarChar).Value = obj.S2Percentage6;
                    da.SelectCommand.Parameters.Add("@s26cal", SqlDbType.VarChar).Value = obj.S2Calculation6;
                    //S3
                    da.SelectCommand.Parameters.Add("@s31f", SqlDbType.VarChar).Value = obj.S3Found1;
                    da.SelectCommand.Parameters.Add("@s31c", SqlDbType.VarChar).Value = obj.S3Claim1;
                    da.SelectCommand.Parameters.Add("@s31p ", SqlDbType.VarChar).Value = obj.S3Percentage1;
                    da.SelectCommand.Parameters.Add("@s31cal", SqlDbType.VarChar).Value = obj.S3Calculation1;
                    da.SelectCommand.Parameters.Add("@s32f", SqlDbType.VarChar).Value = obj.S3Found2;
                    da.SelectCommand.Parameters.Add("@s32c", SqlDbType.VarChar).Value = obj.S3Claim2;
                    da.SelectCommand.Parameters.Add("@s32p", SqlDbType.VarChar).Value = obj.S3Percentage2;
                    da.SelectCommand.Parameters.Add("@s32cal", SqlDbType.VarChar).Value = obj.S3Calculation2;
                    da.SelectCommand.Parameters.Add("@s33f", SqlDbType.VarChar).Value = obj.S3Found3;
                    da.SelectCommand.Parameters.Add("@s33c", SqlDbType.VarChar).Value = obj.S3Claim3;
                    da.SelectCommand.Parameters.Add("@s33p", SqlDbType.VarChar).Value = obj.S3Percentage3;
                    da.SelectCommand.Parameters.Add("@s33cal", SqlDbType.VarChar).Value = obj.S3Calculation3;
                    da.SelectCommand.Parameters.Add("@s34f", SqlDbType.VarChar).Value = obj.S3Found4;
                    da.SelectCommand.Parameters.Add("@s34c", SqlDbType.VarChar).Value = obj.S3Claim4;
                    da.SelectCommand.Parameters.Add("@s34p", SqlDbType.VarChar).Value = obj.S3Percentage4;
                    da.SelectCommand.Parameters.Add("@s34cal", SqlDbType.VarChar).Value = obj.S3Calculation4;

                    da.SelectCommand.Parameters.Add("@s35f", SqlDbType.VarChar).Value = obj.S3Found5;
                    da.SelectCommand.Parameters.Add("@s35c", SqlDbType.VarChar).Value = obj.S3Claim5;
                    da.SelectCommand.Parameters.Add("@s35p", SqlDbType.VarChar).Value = obj.S3Percentage5;
                    da.SelectCommand.Parameters.Add("@s35cal", SqlDbType.VarChar).Value = obj.S3Calculation5;
                    da.SelectCommand.Parameters.Add("@s36f", SqlDbType.VarChar).Value = obj.S3Found6;
                    da.SelectCommand.Parameters.Add("@s36c", SqlDbType.VarChar).Value = obj.S3Claim6;
                    da.SelectCommand.Parameters.Add("@s36p", SqlDbType.VarChar).Value = obj.S3Percentage6;
                    da.SelectCommand.Parameters.Add("@s36cal", SqlDbType.VarChar).Value = obj.S3Calculation6;
                    da.SelectCommand.Parameters.Add("@s37f", SqlDbType.VarChar).Value = obj.S3Found7;
                    da.SelectCommand.Parameters.Add("@s37c", SqlDbType.VarChar).Value = obj.S3Claim7;
                    da.SelectCommand.Parameters.Add("@s37p", SqlDbType.VarChar).Value = obj.S3Percentage7;
                    da.SelectCommand.Parameters.Add("@s37cal", SqlDbType.VarChar).Value = obj.S3Calculation7;
                    da.SelectCommand.Parameters.Add("@s38f", SqlDbType.VarChar).Value = obj.S3Found8;
                    da.SelectCommand.Parameters.Add("@s38c", SqlDbType.VarChar).Value = obj.S3Claim8;
                    da.SelectCommand.Parameters.Add("@s38p", SqlDbType.VarChar).Value = obj.S3Percentage8;
                    da.SelectCommand.Parameters.Add("@s38cal", SqlDbType.VarChar).Value = obj.S3Calculation8;
                    da.SelectCommand.Parameters.Add("@s39f", SqlDbType.VarChar).Value = obj.S3Found9;
                    da.SelectCommand.Parameters.Add("@s39c", SqlDbType.VarChar).Value = obj.S3Claim9;
                    da.SelectCommand.Parameters.Add("@s39p", SqlDbType.VarChar).Value = obj.S3Percentage9;
                    da.SelectCommand.Parameters.Add("@s39cal", SqlDbType.VarChar).Value = obj.S3Calculation9;
                    da.SelectCommand.Parameters.Add("@s310f", SqlDbType.VarChar).Value = obj.S3Found10;
                    da.SelectCommand.Parameters.Add("@s310c", SqlDbType.VarChar).Value = obj.S3Claim10;
                    da.SelectCommand.Parameters.Add("@s310p", SqlDbType.VarChar).Value = obj.S3Percentage10;
                    da.SelectCommand.Parameters.Add("@s310cal", SqlDbType.VarChar).Value = obj.S3Calculation10;
                    da.SelectCommand.Parameters.Add("@s311f", SqlDbType.VarChar).Value = obj.S3Found11;
                    da.SelectCommand.Parameters.Add("@s311c", SqlDbType.VarChar).Value = obj.S3Claim11;
                    da.SelectCommand.Parameters.Add("@s311p", SqlDbType.VarChar).Value = obj.S3Percentage11;
                    da.SelectCommand.Parameters.Add("@s311cal", SqlDbType.VarChar).Value = obj.S3Calculation11;
                    da.SelectCommand.Parameters.Add("@s312f", SqlDbType.VarChar).Value = obj.S3Found12;
                    da.SelectCommand.Parameters.Add("@s312c", SqlDbType.VarChar).Value = obj.S3Claim12;
                    da.SelectCommand.Parameters.Add("@s312p", SqlDbType.VarChar).Value = obj.S3Percentage12;
                    da.SelectCommand.Parameters.Add("@s312cal", SqlDbType.VarChar).Value = obj.S3Calculation12;
                    da.SelectCommand.Parameters.Add("@s1remarks", SqlDbType.VarChar).Value = obj.S1Remarks;
                    da.SelectCommand.Parameters.Add("@s2remarks", SqlDbType.VarChar).Value = obj.S2Remarks;
                    da.SelectCommand.Parameters.Add("@s3remarks", SqlDbType.VarChar).Value = obj.S3Remarks;
                    //BSU
                    da.SelectCommand.Parameters.Add("@bs1f", SqlDbType.VarChar).Value = obj.bsFound1;
                    da.SelectCommand.Parameters.Add("@bs1c", SqlDbType.VarChar).Value = obj.bsClaim1;
                    da.SelectCommand.Parameters.Add("@bs1p", SqlDbType.VarChar).Value = obj.bsPercentage1;
                    da.SelectCommand.Parameters.Add("@bs1cal", SqlDbType.VarChar).Value = obj.bsCalculation1;
                    da.SelectCommand.Parameters.Add("@bs2f", SqlDbType.VarChar).Value = obj.bsFound2;
                    da.SelectCommand.Parameters.Add("@bs2c", SqlDbType.VarChar).Value = obj.bsClaim2;
                    da.SelectCommand.Parameters.Add("@bs2p", SqlDbType.VarChar).Value = obj.bsPercentage2;
                    da.SelectCommand.Parameters.Add("@bs2cal", SqlDbType.VarChar).Value = obj.bsCalculation2;
                    da.SelectCommand.Parameters.Add("@bs3f", SqlDbType.VarChar).Value = obj.bsFound3;
                    da.SelectCommand.Parameters.Add("@bs3c", SqlDbType.VarChar).Value = obj.bsClaim3;
                    da.SelectCommand.Parameters.Add("@bs3p", SqlDbType.VarChar).Value = obj.bsPercentage3;
                    da.SelectCommand.Parameters.Add("@bs3cal", SqlDbType.VarChar).Value = obj.bsCalculation3;
                    da.SelectCommand.Parameters.Add("@bs4f", SqlDbType.VarChar).Value = obj.bsFound4;
                    da.SelectCommand.Parameters.Add("@bs4c", SqlDbType.VarChar).Value = obj.bsClaim4;
                    da.SelectCommand.Parameters.Add("@bs4p", SqlDbType.VarChar).Value = obj.bsPercentage4;
                    da.SelectCommand.Parameters.Add("@bs4cal", SqlDbType.VarChar).Value = obj.bsCalculation4;
                    da.SelectCommand.Parameters.Add("@bs5f", SqlDbType.VarChar).Value = obj.bsFound5;
                    da.SelectCommand.Parameters.Add("@bs5c", SqlDbType.VarChar).Value = obj.bsClaim5;
                    da.SelectCommand.Parameters.Add("@bs5p", SqlDbType.VarChar).Value = obj.bsPercentage5;
                    da.SelectCommand.Parameters.Add("@bs5cal", SqlDbType.VarChar).Value = obj.bsCalculation5;

                    da.SelectCommand.Parameters.Add("@bs6f", SqlDbType.VarChar).Value = obj.bsFound6;
                    da.SelectCommand.Parameters.Add("@bs6c", SqlDbType.VarChar).Value = obj.bsClaim6;
                    da.SelectCommand.Parameters.Add("@bs6p", SqlDbType.VarChar).Value = obj.bsPercentage6;
                    da.SelectCommand.Parameters.Add("@bs6cal", SqlDbType.VarChar).Value = obj.bsCalculation6;
                    da.SelectCommand.Parameters.Add("@bsremarks", SqlDbType.VarChar).Value = obj.bsRemarks;
                    da.SelectCommand.Parameters.Add("@ip", SqlDbType.VarChar).Value = obj.ip;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        //Edit TestResult sampath
        public DataTable JAActionEdit(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("EditTestResult", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@status", SqlDbType.VarChar).Value = obj.status;
                    da.SelectCommand.Parameters.Add("@sampleid", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@AnalystID", SqlDbType.VarChar).Value = obj.AnalystId;

                    da.SelectCommand.Parameters.Add("@startdt", SqlDbType.VarChar).Value = obj.startdt;
                    da.SelectCommand.Parameters.Add("@enddt", SqlDbType.VarChar).Value = obj.enddt;
                    da.SelectCommand.Parameters.Add("@remarks", SqlDbType.VarChar).Value = obj.Remarks;
                    da.SelectCommand.Parameters.Add("@desc", SqlDbType.VarChar).Value = obj.description;
                    da.SelectCommand.Parameters.Add("@TestId", SqlDbType.VarChar).Value = obj.Testid;
                    da.SelectCommand.Parameters.Add("@ip", SqlDbType.VarChar).Value = obj.ip;
                    da.SelectCommand.Parameters.Add("@TestParam", SqlDbType.VarChar).Value = obj.testparameter;
                    da.SelectCommand.Parameters.Add("@TestProtocol", SqlDbType.VarChar).Value = obj.testprotocal;
                    da.SelectCommand.Parameters.Add("@Calculation", SqlDbType.VarChar).Value = obj.testcalculation;
                    da.SelectCommand.Parameters.Add("@TestName", SqlDbType.VarChar).Value = obj.testname;
                    da.SelectCommand.Parameters.Add("@found", SqlDbType.VarChar).Value = obj.testfound;
                    da.SelectCommand.Parameters.Add("@claim", SqlDbType.VarChar).Value = obj.testclaim;
                    da.SelectCommand.Parameters.Add("@limit", SqlDbType.VarChar).Value = obj.testlimit;
                    da.SelectCommand.Parameters.Add("@testfor", SqlDbType.VarChar).Value = obj.testforanalyst;
                    da.SelectCommand.Parameters.Add("@result", SqlDbType.Structured).Value = obj.TVP;
                    /*Govt Analyst Action*/
                    da.SelectCommand.Parameters.Add("@UOStatus", SqlDbType.VarChar).Value = obj.UOStatus;
                    da.SelectCommand.Parameters.Add("@UoCode", SqlDbType.VarChar).Value = obj.UOId;
                    da.SelectCommand.Parameters.Add("@UoReamrks", SqlDbType.VarChar).Value = obj.UORemarks;
                    da.SelectCommand.Parameters.Add("@resultUO", SqlDbType.Structured).Value = obj.UOTVP;
                    da.SelectCommand.Parameters.Add("@ref", SqlDbType.VarChar).Value = obj.Ref;
                    da.SelectCommand.Parameters.Add("@Labid", SqlDbType.VarChar).Value = obj.labID;
                    da.SelectCommand.Parameters.Add("@flag", SqlDbType.VarChar).Value = obj.flag;
                    da.SelectCommand.Parameters.Add("@tvpfreeze", SqlDbType.Structured).Value = obj.tvpfreeze_UO;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
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
        //bulkdata sampath
        public DataTable bulkdata(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {

                using (SqlDataAdapter da = new SqlDataAdapter("DCA_TestResult", con))
                {

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@sampleid", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@AnalystID", SqlDbType.VarChar).Value = obj.AnalystId;
                    da.SelectCommand.Parameters.Add("@role", SqlDbType.VarChar).Value = obj.user;
                    da.SelectCommand.Parameters.Add("@startdt", SqlDbType.VarChar).Value = obj.startdt;
                    da.SelectCommand.Parameters.Add("@enddt", SqlDbType.VarChar).Value = obj.enddt;
                    da.SelectCommand.Parameters.Add("@remarks", SqlDbType.VarChar).Value = obj.Remarks;
                    da.SelectCommand.Parameters.Add("@desc", SqlDbType.VarChar).Value = obj.description;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = obj.TVP;
                    da.SelectCommand.Parameters.Add("@status", SqlDbType.VarChar).Value = obj.status;
                    da.SelectCommand.Parameters.Add("@result", SqlDbType.Structured).Value = obj.AckTVP;
                    if (obj.Ds1TVP != null)
                    {
                        da.SelectCommand.Parameters.Add("@ds1", SqlDbType.Structured).Value = obj.Ds1TVP;
                    }
                    if (obj.Ds2TVP != null)
                    {
                        da.SelectCommand.Parameters.Add("@ds2", SqlDbType.Structured).Value = obj.Ds2TVP;
                    }
                    if (obj.Ds3TVP != null)
                    {
                        da.SelectCommand.Parameters.Add("@ds3", SqlDbType.Structured).Value = obj.Ds3TVP;
                    }
                    da.SelectCommand.Parameters.Add("@ip", SqlDbType.VarChar).Value = obj.ip;

                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    da.SelectCommand.Parameters.Add("@subaction", SqlDbType.VarChar).Value = obj.flag;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable searchsample(AgriBE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Search_Sample", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@AnalysyID", SqlDbType.VarChar).Value = obj.AnalystId;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    da.SelectCommand.Parameters.Add("@Category", SqlDbType.VarChar).Value = obj.Category;
                    da.SelectCommand.Parameters.Add("@SampleName", SqlDbType.VarChar).Value = obj.SampleName;
                    da.SelectCommand.Parameters.Add("@SampleId", SqlDbType.VarChar).Value = obj.AckId;
                    // da.SelectCommand.Parameters.Add("@SampleId", SqlDbType.VarChar).Value = obj.SampleID;
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
                    da.SelectCommand.Parameters.Add("@Sampletype", SqlDbType.VarChar).Value = obj.SampleType;
                    da.SelectCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = obj.UserId;
                    if (obj.TVP != null)
                    {
                        da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = obj.TVP;
                    }
                    if (obj.AckTVP != null)
                    {
                        da.SelectCommand.Parameters.Add("@tvplab", SqlDbType.Structured).Value = obj.AckTVP;
                    }
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
                    {
                        da.SelectCommand.Parameters.Add("@Tvpequipment", SqlDbType.Structured).Value = obj.TVP;
                    }

                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

    }
}
