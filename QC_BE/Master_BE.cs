using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QC_BE
{
    public class Master_BE
    {
        //GENERAL
        public string Id { get; set; }
        public string Action { get; set; }
        public string Dept { get; set; }
        public string User { get; set; }
        public string IP { get; set; }
        public DataTable TVP { get; set; }


        //LOCATIONS
        public string statecode { get; set; }
        public string stateName { get; set; }
        public string District { get; set; }
        public string DistCode { get; set; }
        public string MandCode { get; set; }
        public string Mandal { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }

        //SAMPLE CATEGORY
        public string CatId { get; set; }
        public string CatName { get; set; }

        //SAMPLE TYPE
        public string SampleTypeId { get; set; }
        public string SampleTypeName { get; set; }


        //SAMPLE NAME
        public string SampleID { get; set; }
        public string SampleName { get; set; }

        //DOSAGES
        public string DosageId { get; set; }
        public string DosageName { get; set; }

        //GRADES
        public string Gradeid { get; set; }
        public string GradeName { get; set; }

        //SPECIFICATIONS
        public string ParamID { get; set; }
        public string ParamName { get; set; }
        public string StdValue { get; set; }
        public string PhyCond { get; set; }


        //DESIGNATION
        public string DesignationID { get; set; }
        public string DesignationName { get; set; }

        //REJECTED REASONS
        public string RejectReason { get; set; }


        //Employee
        public string EmpId { get; set; }
        public string Empcode { get; set; }
        public string EmpName { get; set; }
        public string Active { get; set; }


        //LBAS
        public string labid { get; set; }
        public string labName { get; set; }
        public string labAddress { get; set; }

        //USER REGISTRATION
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string pincode { get; set; }
        public string pslimit { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string approval { get; set; }

        public int prid { get; set; }



        //Crop -- Sampath Kumar
        public string Cropid { get; set; }
        public string CropName { get; set; }

        //CropVarity
        public string CropVrcode { get; set; }
        public string CropVrName { get; set; }

        //Test
        public string Testcode { get; set; }
        public string TestName { get; set; }

        //Equipment Master--Aparna
        public string EquipCode { get; set; }
        public string EquipName { get; set; }

        //Minimum Seed Standards
        public string CropKindId { get; set; }
        public string CropKind { get; set; }
        public string StdId { get; set; }
        public string MaxLotSize { get; set; }
        public string MinSubSamples { get; set; }

        //Target Allotment
        public string Year { get; set; }
        public string AllotId { get; set; }
        public string Target { get; set; }
        public string ActionBy { get; set; }
    }
}
