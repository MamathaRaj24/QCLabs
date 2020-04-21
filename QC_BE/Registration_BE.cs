using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QC_BE
{
    public class Registration_BE
    {
        //SAMPLE REGISTRATION
        public string login_state { get; set; }
        public string login_dist { get; set; }
        public string logon_mand { get; set; }
        public string dept { get; set; }
        public string DI_AO_Code { get; set; }
        public string Action { get; set; }
        public string user { get; set; }


        public string SampleClass { get; set; }
        public string Usage { get; set; }
        public string Paymenttype { get; set; }
        public string Priority { get; set; }
        public string PriorityRemarks { get; set; }



        public string FirmType { get; set; }
        public string Firm_Name { get; set; }
        public string LicenceNo { get; set; }
        public string Validity { get; set; }
        public string ContactPerson { get; set; }
        public string StkRcvdFrom { get; set; }
        public string InvoiceNo { get; set; }
        public string FairPrice { get; set; }
        public string InvoiceDate { get; set; }
        public string Aadharno { get; set; }
        public string state { get; set; }
        public string District { get; set; }
        public string Mandal { get; set; }
        public string HouseNo { get; set; }
        public string Locality { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string punchnama { get; set; }

        public string SmplCollectingDt { get; set; }
        public string SampleCategory { get; set; }
        public string SampleType { get; set; }
        public string SampleID { get; set; }
        public string GenericName { get; set; }
        public string TradeName { get; set; }
        public string BatchNo { get; set; }
        public string Composition { get; set; }
        public string StockPosition { get; set; }
        public string ManufacturingDate { get; set; }
        public string ExpiryDate { get; set; }
        public string StkRcvdDate { get; set; }
        public string SampleQty { get; set; }
        public string DealerName { get; set; }
        public string ReceiptDate { get; set; }
        public string COAction { get; set; }


        public string ManufacturerState { get; set; }
        public string ManfucaturerName { get; set; }
        public string ManufacutrerLicence { get; set; }
        public string Address { get; set; }


        public string CodeSticker { get; set; }
        public string ISImark { get; set; }
        public string MarketerName { get; set; }
        public string SmplDrawnBag { get; set; }
        public string Receiptdate { get; set; }
        public string Gaurentees_AI { get; set; }

        public string DrugName { get; set; }
        public string Remarks { get; set; }

        public string ack { get; set; }
        public string ownername { get; set; }
        public string PhycicalCondition { get; set; }
        public string qtyPicked { get; set; }
        public string MarketerState { get; set; }
        public string ManufAddress { get; set; }
        public string MarketerAddress { get; set; }



        public string SRegid { get; set; }


        public string Entry_Dt { get; set; }
        /*aparna*/
        public string UserId { get; set; }

        public string MemoId { get; set; }

        public DataTable TVP { get; set; }

        public DataTable AckTVP { get; set; }


        /*Rajesh*/

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Scheme { get; set; }

        public string UserType { get; set; }

        public string Designation { get; set; }

        public string TestLab { get; set; }

        public string PinCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Village { get; set; }

        public string PsLimit { get; set; }

        public string ZoneCode { get; set; }

        public string ZoneName { get; set; }

        public string UserApproval { get; set; }

        public string labID { get; set; }

        public string AnalystId { get; set; }

        public string status { get; set; }
        public string startdt { get; set; }
        public string enddt { get; set; }
        public string description { get; set; }

        public string AckId { get; set; }




        public byte[] qrcode { get; set; }


        public string S1Calculation1 { get; set; }
        public string S1Found1 { get; set; }
        public string S1Claim1 { get; set; }
        public string S1Percentage1 { get; set; }
        public string S1Calculation2 { get; set; }
        public string S1Found2 { get; set; }
        public string S1Claim2 { get; set; }
        public string S1Percentage2 { get; set; }
        public string S1Calculation3 { get; set; }
        public string S1Found3 { get; set; }
        public string S1Claim3 { get; set; }
        public string S1Percentage3 { get; set; }
        public string S1Calculation4 { get; set; }
        public string S1Found4 { get; set; }
        public string S1Claim4 { get; set; }
        public string S1Percentage4 { get; set; }
        public string S1Calculation5 { get; set; }
        public string S1Found5 { get; set; }
        public string S1Claim5 { get; set; }
        public string S1Percentage5 { get; set; }
        public string S1Calculation6 { get; set; }
        public string S1Found6 { get; set; }
        public string S1Claim6 { get; set; }
        public string S1Percentage6 { get; set; }
        public string S1Remarks { get; set; }
        //S2
        public string S2Calculation1 { get; set; }
        public string S2Found1 { get; set; }
        public string S2Claim1 { get; set; }
        public string S2Percentage1 { get; set; }
        public string S2Calculation2 { get; set; }
        public string S2Found2 { get; set; }
        public string S2Claim2 { get; set; }
        public string S2Percentage2 { get; set; }
        public string S2Calculation3 { get; set; }
        public string S2Found3 { get; set; }
        public string S2Claim3 { get; set; }
        public string S2Percentage3 { get; set; }
        public string S2Calculation4 { get; set; }
        public string S2Found4 { get; set; }
        public string S2Claim4 { get; set; }
        public string S2Percentage4 { get; set; }
        public string S2Calculation5 { get; set; }
        public string S2Found5 { get; set; }
        public string S2Claim5 { get; set; }
        public string S2Percentage5 { get; set; }
        public string S2Calculation6 { get; set; }
        public string S2Found6 { get; set; }
        public string S2Claim6 { get; set; }
        public string S2Percentage6 { get; set; }
        public string S2Remarks { get; set; }
        //S3
        public string S3Calculation1 { get; set; }
        public string S3Found1 { get; set; }
        public string S3Claim1 { get; set; }
        public string S3Percentage1 { get; set; }
        public string S3Calculation2 { get; set; }
        public string S3Found2 { get; set; }
        public string S3Claim2 { get; set; }
        public string S3Percentage2 { get; set; }
        public string S3Calculation3 { get; set; }
        public string S3Found3 { get; set; }
        public string S3Claim3 { get; set; }
        public string S3Percentage3 { get; set; }
        public string S3Calculation4 { get; set; }
        public string S3Found4 { get; set; }
        public string S3Claim4 { get; set; }
        public string S3Percentage4 { get; set; }
        public string S3Calculation5 { get; set; }
        public string S3Found5 { get; set; }
        public string S3Claim5 { get; set; }
        public string S3Percentage5 { get; set; }
        public string S3Calculation6 { get; set; }
        public string S3Found6 { get; set; }
        public string S3Claim6 { get; set; }
        public string S3Percentage6 { get; set; }
        public string S3Calculation7 { get; set; }
        public string S3Found7 { get; set; }
        public string S3Claim7 { get; set; }
        public string S3Percentage7 { get; set; }
        public string S3Calculation8 { get; set; }
        public string S3Found8 { get; set; }
        public string S3Claim8 { get; set; }
        public string S3Percentage8 { get; set; }
        public string S3Calculation9 { get; set; }
        public string S3Found9 { get; set; }
        public string S3Claim9 { get; set; }
        public string S3Percentage9 { get; set; }
        public string S3Calculation10 { get; set; }
        public string S3Found10 { get; set; }
        public string S3Claim10 { get; set; }
        public string S3Percentage10 { get; set; }
        public string S3Calculation11 { get; set; }
        public string S3Found11 { get; set; }
        public string S3Claim11 { get; set; }
        public string S3Percentage11 { get; set; }
        public string S3Calculation12 { get; set; }
        public string S3Found12 { get; set; }
        public string S3Claim12 { get; set; }
        public string S3Percentage12 { get; set; }

        public string S3Remarks { get; set; }
        public string Disolution { get; set; }
        public string UOStatus { get; set; }
        public string UOId { get; set; }
        public string UORemarks { get; set; }
        public string Ref { get; set; }
        public DataTable UOTVP { get; set; }


        //SEED
        public string Cropcode { get; set; }
        public string CropVrcode { get; set; }
        public string Lotno { get; set; }
        public string Orgin { get; set; }
        public string Testid { get; set; }

        //Seed Registration
        public string PunchanamaUploadid { get; set; }
        public string FirUploadId { get; set; }
        public string ShociUploadId { get; set; }
        public string Cropkind { get; set; }
        public string Lotquantity { get; set; }
        public string Quantityofsamplesubmit { get; set; }

        public string punchanamafileType { get; set; }
        public string punchanamasize { get; set; }
        public string punchanamafilenm { get; set; }
        public string type { get; set; }
        public byte[] punchanamafilecontent { get; set; }
        public string Shociname { get; set; }

        public string firfileType { get; set; }
        public string firsize { get; set; }
        public string firfilenm { get; set; }

        public byte[] firfilecontent { get; set; }

        public string shocifileType { get; set; }
        public string shocisize { get; set; }
        public string shocifilenm { get; set; }
        public string Complise { get; set; }
        public string ip { get; set; }
        public byte[] shocifilecontent { get; set; }
        //edit test Sampath
        public string testparameter { get; set; }
        public string testname { get; set; }
        public string testcalculation { get; set; }
        public string testprotocal { get; set; }
        public string testfound { get; set; }
        public string testclaim { get; set; }
        public string testlimit { get; set; }
        public string testforanalyst { get; set; }

        //Buffer Stage 
        public string bsCalculation1 { get; set; }
        public string bsFound1 { get; set; }
        public string bsClaim1 { get; set; }
        public string bsPercentage1 { get; set; }
        public string bsCalculation2 { get; set; }
        public string bsFound2 { get; set; }
        public string bsClaim2 { get; set; }
        public string bsPercentage2 { get; set; }
        public string bsCalculation3 { get; set; }
        public string bsFound3 { get; set; }
        public string bsClaim3 { get; set; }
        public string bsPercentage3 { get; set; }
        public string bsCalculation4 { get; set; }
        public string bsFound4 { get; set; }
        public string bsClaim4 { get; set; }
        public string bsPercentage4 { get; set; }
        public string bsCalculation5 { get; set; }
        public string bsFound5 { get; set; }
        public string bsClaim5 { get; set; }
        public string bsPercentage5 { get; set; }
        public string bsCalculation6 { get; set; }
        public string bsFound6 { get; set; }
        public string bsClaim6 { get; set; }
        public string bsPercentage6 { get; set; }
        public string bsRemarks { get; set; }

    
        public string Jsoid { get; set; }

        public string flag { get; set; }
        public DataTable tvpfreeze_UO { get; set; }
        public DataTable Ds1TVP { get; set; }
        public DataTable Ds2TVP { get; set; }
        public DataTable Ds3TVP { get; set; }



        public string Category { get; set; }
        public string SampleName { get; set; }
    }
}
