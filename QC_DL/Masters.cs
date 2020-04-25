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
    public class Masters
    {

        public DataTable GetLocations(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("GetLocations", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@dist", SqlDbType.VarChar).Value = obj.DistCode;
                    da.SelectCommand.Parameters.Add("@mand", SqlDbType.VarChar).Value = obj.MandCode;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable Statemaster_IUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("State_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //if (obj.StateCode !="0") 
                    da.SelectCommand.Parameters.Add("@statecode", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@stateName", SqlDbType.VarChar).Value = obj.stateName;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable District_IUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("District_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //if (obj.StateCode !="0") 
                    da.SelectCommand.Parameters.Add("@statecode", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@distcode", SqlDbType.VarChar).Value = obj.DistCode;
                    da.SelectCommand.Parameters.Add("@DistName", SqlDbType.VarChar).Value = obj.District;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable Zone_IUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Zone_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@Zonecode", SqlDbType.VarChar).Value = obj.ZoneCode;
                    da.SelectCommand.Parameters.Add("@zonename", SqlDbType.VarChar).Value = obj.ZoneName;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }



        public DataTable CategoryIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SampleCategory_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@cat_id", SqlDbType.VarChar).Value = obj.CatId;
                    da.SelectCommand.Parameters.Add("@cat_name", SqlDbType.VarChar).Value = obj.CatName;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.UserID;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }



        public DataTable SampleTypeIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SampleTypes_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@category", SqlDbType.VarChar).Value = obj.CatId;
                    da.SelectCommand.Parameters.Add("@typeid", SqlDbType.VarChar).Value = obj.SampleTypeId;
                    da.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = obj.SampleTypeName;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public DataTable SampleCategoryIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SampleCategory_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@cat_id", SqlDbType.VarChar).Value = obj.CatId;
                    da.SelectCommand.Parameters.Add("@cat_name", SqlDbType.VarChar).Value = obj.CatName;
                    // da.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = obj.SampleTypeName;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public DataTable TestParameterIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("TestParams_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    // da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@category", SqlDbType.VarChar).Value = obj.CatId;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = obj.TVP;
                    da.SelectCommand.Parameters.Add("@Param_Id", SqlDbType.VarChar).Value = obj.ParamID;
                    da.SelectCommand.Parameters.Add("@Param", SqlDbType.VarChar).Value = obj.ParamName;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable SampleIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SampleMst_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@SampleTypeCode", SqlDbType.VarChar).Value = obj.SampleTypeId;
                    da.SelectCommand.Parameters.Add("@SampleCode", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@SampleName", SqlDbType.VarChar).Value = obj.SampleName;
                    da.SelectCommand.Parameters.Add("@User_ID", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }





        public DataTable DosageDtls(Master_BE objBE, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Dosage_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.Int).Value = objBE.Dept;
                    da.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = objBE.Id;
                    da.SelectCommand.Parameters.Add("@DosageName", SqlDbType.VarChar).Value = objBE.DosageName;
                    da.SelectCommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = objBE.User;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = objBE.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable Specifications_IUDR(Master_BE objBE, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Specifications_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@sampleID", SqlDbType.VarChar).Value = objBE.SampleID;
                    da.SelectCommand.Parameters.Add("@phyCond", SqlDbType.VarChar).Value = objBE.PhyCond;
                    da.SelectCommand.Parameters.Add("@Param_ID", SqlDbType.VarChar).Value = objBE.ParamID;
                    da.SelectCommand.Parameters.Add("@param_Name", SqlDbType.VarChar).Value = objBE.ParamName;
                    da.SelectCommand.Parameters.Add("@std", SqlDbType.VarChar).Value = objBE.StdValue;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = objBE.User;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = objBE.TVP;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = objBE.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }





        public DataTable RejectedReasons(Master_BE objBE, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("RejectReasons_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = objBE.Id;
                    da.SelectCommand.Parameters.Add("@RejectedReason", SqlDbType.VarChar).Value = objBE.RejectReason;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.BigInt).Value = objBE.Dept;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.BigInt).Value = objBE.statecode;
                    da.SelectCommand.Parameters.Add("@User", SqlDbType.BigInt).Value = objBE.User;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = objBE.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable UserRegistration_IUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("UserReg_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = obj.Id;
                    da.SelectCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = obj.Name;
                    da.SelectCommand.Parameters.Add("@Depatment", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@Role", SqlDbType.VarChar).Value = obj.Role;
                    da.SelectCommand.Parameters.Add("@Desig", SqlDbType.VarChar).Value = obj.DesignationID;
                    da.SelectCommand.Parameters.Add("@Empcode", SqlDbType.VarChar).Value = obj.EmpId;
                    da.SelectCommand.Parameters.Add("@Lab", SqlDbType.VarChar).Value = obj.labid;
                    da.SelectCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = obj.UserID;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@zoneCd", SqlDbType.VarChar).Value = obj.ZoneCode;
                    da.SelectCommand.Parameters.Add("@DistCode", SqlDbType.VarChar).Value = obj.DistCode;
                    da.SelectCommand.Parameters.Add("@MandCode", SqlDbType.VarChar).Value = obj.MandCode;
                    da.SelectCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = obj.Address;
                    da.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = obj.UserName;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable ApproveUsers(DataTable dtusers, string dept, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Approve_User", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = dept;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = dtusers;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable Getdetails(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("GetDetails", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = obj.Id;
                    da.SelectCommand.Parameters.Add("@Catid", SqlDbType.VarChar).Value = obj.CatId;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable Lab_IUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Labs_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@sampleCategory", SqlDbType.VarChar).Value = obj.CatId;
                    da.SelectCommand.Parameters.Add("@labcode", SqlDbType.VarChar).Value = obj.labid;
                    da.SelectCommand.Parameters.Add("@labname", SqlDbType.VarChar).Value = obj.labName;
                    da.SelectCommand.Parameters.Add("@labAddress", SqlDbType.VarChar).Value = obj.labAddress;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        //Equipment Master
        public DataTable Equipment_IUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Equipment_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@sampleCategory", SqlDbType.VarChar).Value = obj.CatId;
                    da.SelectCommand.Parameters.Add("@SampleType", SqlDbType.VarChar).Value = obj.SampleTypeId;
                    da.SelectCommand.Parameters.Add("@EquipCode", SqlDbType.VarChar).Value = obj.EquipCode;
                    da.SelectCommand.Parameters.Add("@EquipName", SqlDbType.VarChar).Value = obj.EquipName;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        //Grade Master --Sampath
        public DataTable GradeIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Grade_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Gradeid", SqlDbType.VarChar).Value = obj.Gradeid;
                    da.SelectCommand.Parameters.Add("@Categiryid", SqlDbType.VarChar).Value = obj.CatId;
                    da.SelectCommand.Parameters.Add("@SampleTypeCode", SqlDbType.VarChar).Value = obj.SampleTypeId;
                    da.SelectCommand.Parameters.Add("@SampleCode", SqlDbType.VarChar).Value = obj.SampleID;
                    da.SelectCommand.Parameters.Add("@GradeName", SqlDbType.VarChar).Value = obj.GradeName;
                    da.SelectCommand.Parameters.Add("@User_ID", SqlDbType.VarChar).Value = obj.UserID;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }



        //Designation

        public DataTable DesignationIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Designation_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@DesignationID", SqlDbType.VarChar).Value = obj.DesignationID;
                    da.SelectCommand.Parameters.Add("@Desig", SqlDbType.VarChar).Value = obj.DesignationName;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@User", SqlDbType.VarChar).Value = obj.UserID;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        //Zone

        public DataTable ZoneIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Zone_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Zonecode", SqlDbType.VarChar).Value = obj.ZoneCode;
                    da.SelectCommand.Parameters.Add("@zonename", SqlDbType.VarChar).Value = obj.ZoneName;
                    da.SelectCommand.Parameters.Add("@state", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@User", SqlDbType.VarChar).Value = obj.UserID;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        //Employee

        public DataTable EmployeeIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("EMPLOYEE_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = obj.EmpId;
                    da.SelectCommand.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = obj.Empcode;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = obj.EmpName;
                    da.SelectCommand.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = obj.Mobile;
                    da.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = obj.Email;
                    da.SelectCommand.Parameters.Add("@Active", SqlDbType.VarChar).Value = obj.Active;
                    da.SelectCommand.Parameters.Add("@User", SqlDbType.VarChar).Value = obj.UserID;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }




        public DataTable GenerateMemoID(Master_BE obj, string Conn)
        {
            using (SqlConnection con = new SqlConnection(Conn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("MemoIDGeneration", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@tvpmemo", SqlDbType.Structured).Value = obj.TVP;
                    da.SelectCommand.Parameters.Add("@User", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@statecode", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@Depatment", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        //GetCourier Details
        public DataTable GetCourierDtls(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("MemoIDGeneration", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        //Courier Details IUDR
        public DataTable CourierIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("CourierDetails_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = obj.TVP;
                    da.SelectCommand.Parameters.Add("@di_id", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable FreezeSamples(Registration_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("FreezeSamples", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = obj.TVP;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }




        //Crop Master
        public DataTable CropIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Agri_CropIUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@CropCode", SqlDbType.VarChar).Value = obj.Cropid;
                    da.SelectCommand.Parameters.Add("@CropName", SqlDbType.VarChar).Value = obj.CropName;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.UserID;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        //Crop Variety Master
        public DataTable CropVarietyIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Agri_CropVarietyIUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@CropCode", SqlDbType.VarChar).Value = obj.Cropid;
                    da.SelectCommand.Parameters.Add("@CropVRCode", SqlDbType.VarChar).Value = obj.CropVrcode;
                    da.SelectCommand.Parameters.Add("@CropVRName", SqlDbType.VarChar).Value = obj.CropVrName;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.UserID;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        //Test Master
        public DataTable TestIUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Agri_TestIUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@TestCode", SqlDbType.VarChar).Value = obj.Testcode;
                    da.SelectCommand.Parameters.Add("@TestaName", SqlDbType.VarChar).Value = obj.TestName;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.UserID;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable AgriTestParameters_IUDR(Master_BE objBE, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("TestParameters_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Testid", SqlDbType.VarChar).Value = objBE.Testcode;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = objBE.Dept;
                    da.SelectCommand.Parameters.Add("@Param_ID", SqlDbType.VarChar).Value = objBE.ParamID;
                    da.SelectCommand.Parameters.Add("@param_Name", SqlDbType.VarChar).Value = objBE.ParamName;
                    da.SelectCommand.Parameters.Add("@std", SqlDbType.VarChar).Value = objBE.StdValue;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = objBE.User;
                    da.SelectCommand.Parameters.Add("@tvp", SqlDbType.Structured).Value = objBE.TVP;
                    da.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar).Value = objBE.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        //Minimum Seed Standards--aparna
        public DataTable MinimumSeedStandard_IUDR(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Standard_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = obj.StdId;
                    da.SelectCommand.Parameters.Add("@Crop", SqlDbType.VarChar).Value = obj.Cropid;
                    da.SelectCommand.Parameters.Add("@Kind", SqlDbType.VarChar).Value = obj.CropKindId;
                    da.SelectCommand.Parameters.Add("@MaxLot", SqlDbType.VarChar).Value = obj.MaxLotSize;
                    da.SelectCommand.Parameters.Add("@MinSmpl", SqlDbType.VarChar).Value = obj.MinSubSamples;
                    da.SelectCommand.Parameters.Add("@user", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        //Target Allotment--aparna
        public DataTable TargetAllotment_IUDR(Master_BE obj, string Conn)
        {
            using (SqlConnection con = new SqlConnection(Conn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("Target_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@TVP", SqlDbType.Structured).Value = obj.TVP;
                    da.SelectCommand.Parameters.Add("@Allotid", SqlDbType.Int).Value = obj.AllotId;
                    da.SelectCommand.Parameters.Add("@Year", SqlDbType.VarChar).Value = obj.Year;
                    da.SelectCommand.Parameters.Add("@Catgory", SqlDbType.Int).Value = obj.CatId;
                    da.SelectCommand.Parameters.Add("@Dist", SqlDbType.VarChar).Value = obj.DistCode;
                    da.SelectCommand.Parameters.Add("@Target", SqlDbType.VarChar).Value = obj.Target;
                    da.SelectCommand.Parameters.Add("@ActionBy", SqlDbType.VarChar).Value = obj.ActionBy;
                    da.SelectCommand.Parameters.Add("@User", SqlDbType.VarChar).Value = obj.User;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.Char).Value = obj.Action;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public DataTable FreezeTargetDetails(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("FreezeTargets", con))
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


        public DataTable EmployeeIUDR_Agri(Master_BE obj, string ConnKey)
        {
            using (SqlConnection con = new SqlConnection(ConnKey))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("EMPLOYEE_IUDR", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = obj.EmpId;
                    da.SelectCommand.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = obj.Empcode;
                    da.SelectCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.Dept;
                    da.SelectCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = obj.EmpName;
                    da.SelectCommand.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = obj.Mobile;
                    da.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = obj.Email;
                    da.SelectCommand.Parameters.Add("@Active", SqlDbType.VarChar).Value = obj.Active;
                    da.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = obj.statecode;
                    da.SelectCommand.Parameters.Add("@Role", SqlDbType.VarChar).Value = obj.Role;
                    da.SelectCommand.Parameters.Add("@Lab", SqlDbType.VarChar).Value = obj.labid;
                    da.SelectCommand.Parameters.Add("@DistCode", SqlDbType.VarChar).Value = obj.DistCode;
                    da.SelectCommand.Parameters.Add("@zoneCd", SqlDbType.VarChar).Value = obj.ZoneCode;
                    da.SelectCommand.Parameters.Add("@MandCode", SqlDbType.VarChar).Value = obj.MandCode;
                    da.SelectCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = obj.Address;
                    da.SelectCommand.Parameters.Add("@Desig", SqlDbType.VarChar).Value = obj.DesignationID;
                    da.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = obj.UserName;
                    da.SelectCommand.Parameters.Add("@Action", SqlDbType.VarChar).Value = obj.Action;

                    da.SelectCommand.Parameters.Add("@User", SqlDbType.VarChar).Value = obj.UserID;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
