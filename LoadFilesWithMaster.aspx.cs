using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Web;

//using Office = Microsoft.Office.Core;
//using Excel = Microsoft.Office.Interop.Excel;

namespace PharmCASASPX
{
    public partial class LoadFilesWithMaster : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        private string SONISFile = "";
        private string ApplicantFile = "";
        private string CollegesFile = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //set the last load dates
            if (IsPostBack)
            {
            }
            else
            {
                SetLoadDates();
            }
        }

        protected void SetLoadDates()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();

                string SQLcmd = "";
                // start here
                //SqlCommand command = new SqlCommand("", cnn);
                //command.Parameters.AddWithValue("@cas_id", "09877");

                SQLcmd = "dbo.[GetLastLoadDates] ";

                using (SqlCommand cmd = new SqlCommand(SQLcmd, cnn))
                {
                    //cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lblLastPharmCASApplicantUpload.Text = reader.GetString(1);
                            lblLastCollegesAttendedUpload.Text = reader.GetString(2);
                            lblLastSONISUpload.Text = reader.GetString(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }
            }
        }

        protected void btnUploadSONIFile_Click(object sender, EventArgs e)
        {
            if (fuSONISFile.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fuSONISFile.FileName);
                    SONISFile = Server.MapPath("~/") + filename;
                    lblSONISFileName.Text = Server.MapPath("~/") + filename;
                    fuSONISFile.SaveAs(Server.MapPath("~/") + filename);

                    lblSONISResults.Text = "Upload status: File uploaded!";
                }
                catch (Exception ex)
                {
                    lblSONISResults.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

        protected void btnLoadSONISFileToSQL_Click(object sender, EventArgs e)
        {
            SONISStudentProfileLoad("");
            lblSONISResults.Text = "Done";
        }

        protected void btnUploadPharmCASApplicantFile_Click(object sender, EventArgs e)
        {
            if (fuPharmCASApplicantFile.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fuPharmCASApplicantFile.FileName);
                    lblPharmCASApplicantFileName.Text = Server.MapPath("~/") + filename;
                    fuPharmCASApplicantFile.SaveAs(Server.MapPath("~/") + filename);
                    lblPharmCASApplicantFileResults.Text = "Upload status: File uploaded!";
                }
                catch (Exception ex)
                {
                    lblPharmCASApplicantFileResults.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

        protected void btnLoadPharmCASApplicantFileToSQL_Click(object sender, EventArgs e)
        {
            ApplicantExtendedLoad("");
            lblPharmCASApplicantFileResults.Text = "Done";
        }

        protected void btnUploadPharmCASCollegesAttendedFile_Click(object sender, EventArgs e)
        {
            if (fuPharmCASCollegesAttendedFile.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fuPharmCASCollegesAttendedFile.FileName);
                    ApplicantFile = Server.MapPath("~/") + filename;
                    lblCollegesAttendedFileName.Text = Server.MapPath("~/") + filename;
                    fuPharmCASCollegesAttendedFile.SaveAs(Server.MapPath("~/") + filename);
                    lblPharmCASCollegesAttendedResults.Text = "Upload status: File uploaded!";
                }
                catch (Exception ex)
                {
                    lblPharmCASCollegesAttendedResults.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

        protected void btnLoadPharmCASCollegesAttendedFileToSQL_Click(object sender, EventArgs e)
        {
            ParseCollegesAttendedFile("");
            lblPharmCASCollegesAttendedResults.Text = "Done";
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (fuSONISFile.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fuSONISFile.FileName);
                    CollegesFile = Server.MapPath("~/") + filename;
                    fuSONISFile.SaveAs(Server.MapPath("~/") + filename);
                    lblSONISResults.Text = "Upload status: File uploaded!";
                }
                catch (Exception ex)
                {
                    lblSONISResults.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

        private void SONISStudentProfileLoad(string filePath)
        {
            //string connectionString = null;
            string SQLcmd = null;
            //string Tester = null;
            string workLine;
            int RowCount = 0;
            //connectionString = "Data Source=Azure-SQL;Initial Catalog=PharmCAS; Trusted_Connection=True;";
            //Microsoft.VisualBasic.FileIO.TextFieldParser reader = New Microsoft.VisualBasic.FileIO.TextFieldParser(@"C:\PharmCAS\DownloadedCSV\CollegesAttended.csv")
            string[] rowArray = new string[40];
            try
            {
                //IList<string> lines = await FileIO.ReadLinesAsync(gSonisFile);
                //foreach (string line in File.ReadLines(Server.MapPath("~/") + fuSONISFile.FileName))

                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    SQLcmd = "exec [dbo].[TruncateSONISStudentProfileLoadTable] ";
                    using (SqlCommand cmd = new SqlCommand(SQLcmd, cnn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    foreach (string line in File.ReadLines(lblSONISFileName.Text))
                    //foreach (string line in lines)
                    {
                        workLine = line.Replace("\"", "");
                        var values = workLine.Split(',');
                        RowCount = RowCount + 1;
                        if (values.Length != 24)
                        {
                            // File is not the right size
                            // report an error and exit
                        }
                        //string CASID = values[0];

                        // start here
                        //SqlCommand command = new SqlCommand("", cnn);
                        //command.Parameters.AddWithValue("@cas_id", "09877");

                        SQLcmd = "dbo.[SONISStudentProfileLoad_insert] ";
                        //SQLcmd += " @cas_id='" + CASID + "'";

                        SQLcmd += " @firstName='" + values[0] + "'";
                        SQLcmd += ", @lastname='" + values[1].Replace("'", "''") + "'";
                        SQLcmd += ", @MIddle='" + values[2].Replace("'", "''") + "'";
                        SQLcmd += ", @Birthdate='" + values[3] + "'";
                        SQLcmd += ", @Sonis_Inst_ID= '" + values[4] + "'";
                        SQLcmd += ", @Cohort='" + values[5] + "'";
                        SQLcmd += ", @CustomerSourceID='" + values[6] + "'";
                        using (SqlCommand cmd = new SqlCommand(SQLcmd, cnn))
                        {
                            if (RowCount > 1)
                            {
                                cmd.ExecuteNonQuery();
                            }
                            //MessageBox.Show("Row inserted !! ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var test21 = "";
                test21 = e.Message.ToString();
            }
        }

        private void ParseCollegesAttendedFile(string filePath)
        {
            //string connectionString = null;
            string SQLcmd = null;
            //string Tester = null;
            string workLine;
            int RowCount = 0;
            //connectionString = "Data Source=Azure-SQL;Initial Catalog=PharmCAS; Trusted_Connection=True;";
            //Microsoft.VisualBasic.FileIO.TextFieldParser reader = New Microsoft.VisualBasic.FileIO.TextFieldParser(@"C:\PharmCAS\DownloadedCSV\CollegesAttended.csv")
            string[] rowArray = new string[40];
            //foreach (string line in File.ReadLines(fuPharmCASCollegesAttendedFile.FileName))
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                SQLcmd = "exec [dbo].[TruncateCollegesAttendedLoadTable] ";
                using (SqlCommand cmd = new SqlCommand(SQLcmd, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
                foreach (string line in File.ReadLines(lblCollegesAttendedFileName.Text))
                //IList<string> lines = await FileIO.ReadLinesAsync(gPharmCASCollegesAttendedFile);
                //foreach (string line in lines)
                {
                    //workLine = line.Replace("\"", "");
                    workLine = CleanAString(line);
                    var values = workLine.Split(',');
                    RowCount = RowCount + 1;

                    if (values.Length != 400)
                    {
                        // File is not the right size
                        // report an error and exit
                    }
                    string CASID = values[0];

                    // start here
                    //SqlCommand command = new SqlCommand("", cnn);
                    //command.Parameters.AddWithValue("@cas_id", "09877");

                    for (int i = 0; i <= 9; i++)
                    {
                        SQLcmd = "dbo.CollegesAttendedETLLoad_insert";
                        SQLcmd += " @cas_id='" + CASID + "'";

                        SQLcmd += ", @college_code='" + values[i + 1] + "'";
                        SQLcmd += ", @college_name='" + values[i + 11].Replace("'", "''") + "'";
                        SQLcmd += ", @college_name_other='" + values[i + 21].Replace("'", "''") + "'";
                        SQLcmd += ", @college_country='" + values[i + 31] + "'";
                        SQLcmd += ", @college_country_name='" + values[i + 41] + "'";
                        SQLcmd += ", @college_state_code='" + values[i + 51] + "'";
                        SQLcmd += ", @college_state='" + values[i + 61] + "'";
                        SQLcmd += ", @college_first_degree_code='" + values[i + 71] + "'";
                        SQLcmd += ", @college_first_degree='" + values[i + 81].Replace("'", "''") + "'";
                        SQLcmd += ", @college_first_degree_status='" + values[i + 91] + "'";

                        SQLcmd += ", @college_first_degree_date='" + values[i + 101] + "'";
                        SQLcmd += ", @college_first_degree_primary_major_code='" + values[i + 111] + "'";
                        SQLcmd += ", @college_first_degree_primary_major='" + values[i + 121].Replace("'", "''") + "'";
                        SQLcmd += ", @college_first_degree_minor_code='" + values[i + 131] + "'";
                        SQLcmd += ", @college_first_degree_minor='" + values[i + 141] + "'";
                        SQLcmd += ", @college_first_degree_secondary_major_code='" + values[i + 151] + "'";
                        SQLcmd += ", @college_first_degree_secondary_major='" + values[i + 161].Replace("'", "''") + "'";
                        SQLcmd += ", @college_second_degree_code='" + values[i + 171] + "'";
                        SQLcmd += ", @college_second_degree='" + values[i + 181].Replace("'", "''") + "'";
                        SQLcmd += ", @college_second_degree_status='" + values[i + 191] + "'";

                        SQLcmd += ", @college_second_degree_date='" + values[i + 201] + "'";
                        SQLcmd += ", @college_second_degree_primary_major_code='" + values[i + 211] + "'";
                        SQLcmd += ", @college_second_degree_primary_major='" + values[i + 221] + "'";
                        SQLcmd += ", @college_attended_from ='" + values[i + 231] + "'";
                        SQLcmd += ", @college_attended_to ='" + values[i + 241] + "'";
                        SQLcmd += ", @college_primary_attended='" + values[i + 251] + "'";
                        SQLcmd += ", @accrediting_agency='" + values[i + 261] + "'";
                        SQLcmd += ", @regionally_accredited='" + values[i + 271] + "'";
                        SQLcmd += ", @ceeb_code='" + values[i + 281] + "'";
                        SQLcmd += ", @college_attended_id_internal='" + values[i + 291] + "'";

                        SQLcmd += ", @college_country_iso_2_code='" + values[i + 301] + "'";
                        SQLcmd += ", @college_country_iso_3_code='" + values[i + 311] + "'";
                        SQLcmd += ", @fice_code='" + values[i + 321] + "'";
                        SQLcmd += ", @degree_verified='" + values[i + 331] + "'";
                        SQLcmd += ", @ipeds_id='" + values[i + 341] + "'";
                        SQLcmd += ", @college_second_degree_first_minor='" + values[i + 351] + "'";
                        SQLcmd += ", @college_second_degree_first_minor_code='" + values[i + 361] + "'";
                        SQLcmd += ", @college_second_degree_second_major='" + values[i + 371] + "'";
                        SQLcmd += ", @college_second_degree_second_major_code='" + values[i + 381] + "'";
                        SQLcmd += ", @college_second_degree_verified='" + values[i + 391] + "'";
                        using (SqlCommand cmd = new SqlCommand(SQLcmd, cnn))
                        {
                            if (values[i + 1] != "")
                            {
                                if (RowCount > 1)
                                {
                                    cmd.ExecuteNonQuery();
                                    //Tester = "2";
                                }
                            }

                            //MessageBox.Show("Row inserted !! ");
                        }
                        //cnn.Close;
                    }
                }
            }
        }

        public string CleanAString(string strInput)
        {
            string lineOut = "";
            string workLine = "";
            string beginquoteFlag = "";
            string endquoteFlag = "";
            string Quote1 = ((char)34).ToString();
            string C1 = ((char)46).ToString();
            C1 = ",";
            string commaSubstitute = " ";
            workLine = strInput;
            lineOut = "";
            for (int i = 0; i < workLine.Length; i++)
            {
                if (workLine.Substring(i, 1) == Quote1)
                {
                    if (endquoteFlag == "Y")
                    {
                        endquoteFlag = "";
                        beginquoteFlag = "";
                    }
                    if (beginquoteFlag == "")
                    {
                        beginquoteFlag = "Y";
                    }
                    else if (beginquoteFlag == "Y")
                    {
                        endquoteFlag = "Y";
                        //beginquoteFlag = "";
                    }
                }
                if (beginquoteFlag == "Y" && endquoteFlag != "Y")
                {
                    if (workLine.Substring(i, 1) == Quote1)
                    {
                        //Don't put quotes into the output string
                        //lineOut = lineOut + line.Substring(i, 1);
                    }
                    else if (workLine.Substring(i, 1) == C1)
                    {
                        //If it's a comma, put in a substitute character
                        lineOut = lineOut + commaSubstitute;
                    }
                    else
                    {
                        if (workLine.Substring(i, 1) == "\\" || workLine.Substring(i, 1) == "\"")
                        {
                        }
                        else
                        {
                            lineOut = lineOut + workLine.Substring(i, 1);
                        }
                    }
                }
                else
                {
                    if (workLine.Substring(i, 1) == Quote1)
                    {
                        //Don't put quotes into the output string
                        //lineOut = lineOut + line.Substring(i, 1);
                    }
                    else
                    {
                        lineOut = lineOut + workLine.Substring(i, 1);
                    }
                }
            }
            return lineOut;
        }

        public void ApplicantExtendedLoad(string FilePath)
        {
            string SQLcmd = null;
            int RowCount = 0;
            string[] rowArray = new string[40];
            string Q1 = ((char)34).ToString();
            string C1 = ((char)46).ToString();
            C1 = ",";
            string lineOut = "";

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                SQLcmd = "exec [dbo].[TruncateApplicantExtendedLoadTable] ";
                using (SqlCommand cmd = new SqlCommand(SQLcmd, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
                foreach (string line in File.ReadLines(lblPharmCASApplicantFileName.Text))
                {
                    lineOut = CleanAString(line);
                    lineOut = lineOut.Replace("'", "''");
                    var values = lineOut.Split(',');
                    RowCount = RowCount + 1;
                    if (values.Length != 271)
                    {
                        // File is not the right size
                        // report an error and exit
                    }
                    string CASID = values[0];

                    // start here
                    //SqlCommand command = new SqlCommand("", cnn);
                    //command.Parameters.AddWithValue("@cas_id", "09877");

                    //cas_id,  salutation,  first_name,  middle_name,  last_name,
                    //10--       //suffix,  applicant_verified,  application_last_changed_on,  number_evaluations_received,  program_plans,
                    //military_status_extended,  current_street_address,  current_street_address_2,  current_city,  current_county,
                    //20 --      //current_state_code,  current_state,  current_country,  current_country_name,  current_postal_code,
                    //current_address_valid_until,  current_country_iso_2_code,  current_country_iso_3_code,  current_county_code,  freshman_total_gpa,
                    //30 --      //freshman_total_hours,  freshman_total_quality_points,  sophomore_total_gpa,  sophomore_total_hours,  sophomore_total_quality_points,
                    //junior_total_gpa,  junior_total_hours,  junior_total_quality_points,  senior_total_gpa,  senior_total_hours,
                    //40 --       //senior_total_quality_points,  graduate_total_gpa,  graduate_total_hours,  graduate_total_quality_points,  baccalaureate_total_gpa,
                    //baccalaureate_total_hours,  baccalaureate_total_quality_points,  international_total_gpa,  international_total_hours,  international_total_quality_points,
                    //50 --       //post_baccalaureate_total_gpa,  post_baccalaureate_total_hours,  post_baccalaureate_total_quality_points,  cumulative_undergraduate_total_gpa,  cumulative_undergraduate_total_hours,
                    //cumulative_undergraduate_total_quality_points,  freshman_mathematics_total_gpa,  freshman_mathematics_total_hours,  freshman_mathematics_total_quality_points,  overall_total_gpa,
                    //60 --        //overall_total_hours,  overall_total_quality_points,  sophomore_mathematics_total_gpa,  sophomore_mathematics_total_hours,  sophomore_mathematics_total_quality_points,
                    //junior_mathematics_total_gpa,  junior_mathematics_total_hours,  junior_mathematics_total_quality_points,  senior_mathematics_total_gpa,  senior_mathematics_total_hours,
                    //70--        //senior_mathematics_total_quality_points,  post_baccalaureate_mathematics_total_gpa,  post_baccalaureate_mathematics_total_hours,  post_baccalaureate_mathematics_total_quality_points,  cumulative_undergraduate_mathematics_total_gpa,
                    //cumulative_undergraduate_mathematics_total_hours,  cumulative_undergraduate_mathematics_total_quality_points,  graduate_mathematics_total_gpa,  graduate_mathematics_total_hours,  graduate_mathematics_total_quality_points,
                    //80 --        //overall_mathematics_total_gpa,  overall_mathematics_total_hours,  overall_mathematics_total_quality_points,  freshman_non_science_total_gpa,  freshman_non_science_total_hours,
                    //freshman_non_science_total_quality_points,  sophomore_non_science_total_gpa,  sophomore_non_science_total_hours,  sophomore_non_science_total_quality_points,  junior_non_science_total_gpa,
                    //90 --        //junior_non_science_total_hours,  junior_non_science_total_quality_points,  senior_non_science_total_gpa,  senior_non_science_total_hours,  senior_non_science_total_quality_points,
                    //post_baccalaureate_non_science_total_gpa,  post_baccalaureate_non_science_total_hours,  post_baccalaureate_non_science_total_quality_points,  cumulative_undergraduate_non_science_total_gpa,  cumulative_undergraduate_non_science_total_hours,
                    //100  --       //cumulative_undergraduate_non_science_total_quality_points,  graduate_non_science_total_gpa,  graduate_non_science_total_hours,  graduate_non_science_total_quality_points,  overall_non_science_total_gpa,
                    //overall_non_science_total_hours,  overall_non_science_total_quality_points,  freshman_science_total_gpa,  freshman_science_total_hours,  freshman_science_total_quality_points,
                    //110 --        //sophomore_science_total_gpa,  sophomore_science_total_hours,  sophomore_science_total_quality_points,  junior_science_total_gpa,  junior_science_total_hours,
                    //junior_science_total_quality_points,  senior_science_total_gpa,  senior_science_total_hours,  senior_science_total_quality_points,  post_baccalaureate_science_total_gpa,
                    //120 --        //post_baccalaureate_science_total_hours,  post_baccalaureate_science_total_quality_points,  cumulative_undergraduate_science_total_gpa,  cumulative_undergraduate_science_total_hours,  cumulative_undergraduate_science_total_quality_points,
                    //graduate_science_total_gpa,  graduate_science_total_hours,  graduate_science_total_quality_points,  overall_science_total_gpa,  overall_science_total_hours,
                    //130 --         //overall_science_total_quality_points,  permanent_street_address,  permanent_street_address_2,  permanent_city,  permanent_county,
                    //permanent_county_code,  permanent_state,  permanent_state_code,  permanent_postal_code,  permanent_country_name,
                    //140 --        //permanent_country,  permanent_country_iso_2_code,  permanent_country_iso_3_code,  gender_code,  gender,
                    //materials_under_another_name,  alt_first_name,  alt_middle_name,  alt_last_name,  citizenship,
                    //150 --      //citizenship_country_name,  citizenship_status,  holds_visa,  visa_type_code,  visa_type,
                    //visa_issuing_city,  country_of_visa_issue_code,  country_of_visa_issue,  date_of_birth,  age_at_submission,
                    //160 --      //city_of_birth,  county_of_birth_code,  county_of_birth,  state_of_birth_code,  state_of_birth,
                    //country_of_birth,  country_of_birth_name,  state_of_residence_code,  state_of_residence,  county_of_residence_code,
                    //170 --      //county_of_residence_name,  dual_citizenship,  second_country_citizenship_code,  second_country_citizenship,  citizenship_country_iso_2_code,
                    //citizenship_country_iso_3_code,  citizenship_status_code,  country_of_birth_iso_2_code,  country_of_birth_iso_3_code,  country_of_visa_issue_iso_2_code,
                    //180--       //country_of_visa_issue_iso_3_code,  us_residence_time_range,  issuing_authority,  second_country_citizenship_iso_2_code,  second_country_citizenship_iso_3_code,
                    //state_of_residence_time_range,  us_visa_number,  visa_issuing_date,  visa_sponsor,  visa_valid_until,
                    //190 --      //preferred_first_name,  preferred_middle_name,  preferred_phone,  alternate_phone,  email,
                    //alternate_phone_type,  email_type,  preferred_phone_number_type,  text_authorization,  gpas_by_school_gpa_0,
                    //200 --      //gpas_by_school_gpa_1,  gpas_by_school_gpa_2,  gpas_by_school_gpa_3,  gpas_by_school_gpa_4,  gpas_by_school_gpa_5,
                    //gpas_by_school_gpa_6,  gpas_by_school_gpa_7,  gpas_by_school_gpa_8,  gpas_by_school_credit_hours_0,  gpas_by_school_credit_hours_1,
                    //210       //gpas_by_school_credit_hours_2,  gpas_by_school_credit_hours_3,  gpas_by_school_credit_hours_4,  gpas_by_school_credit_hours_5,  gpas_by_school_credit_hours_6,
                    //gpas_by_school_credit_hours_7,  gpas_by_school_credit_hours_8,  gpas_by_school_quality_points_0,  gpas_by_school_quality_points_1,  gpas_by_school_quality_points_2,
                    //220 --    //gpas_by_school_quality_points_3,  gpas_by_school_quality_points_4,  gpas_by_school_quality_points_5,  gpas_by_school_quality_points_6,  gpas_by_school_quality_points_7,
                    //gpas_by_school_quality_points_8,  gpas_by_school_college_id_0,  gpas_by_school_college_id_1,  gpas_by_school_college_id_2,  gpas_by_school_college_id_3,
                    //230 --    //gpas_by_school_college_id_4,  gpas_by_school_college_id_5,  gpas_by_school_college_id_6,  gpas_by_school_college_id_7,  gpas_by_school_college_id_8,
                    //gpas_by_school_college_name_0,  gpas_by_school_college_name_1,  gpas_by_school_college_name_2,  gpas_by_school_college_name_3,  gpas_by_school_college_name_4,
                    //240 --    //gpas_by_school_college_name_5,  gpas_by_school_college_name_6,  gpas_by_school_college_name_7,  gpas_by_school_college_name_8,  race_hispanic,
                    //race_cuban,  race_mexican,  race_puerto_rican,  race_south_american,  race_other_spanish,
                    //250 --    //race_other_spanish_name,  race_american_indian,  race_indian_tribe_name,  race_asian,  race_asian_indian,
                    //race_cambodian,  race_chinese,  race_filipino,  race_japanese,  race_korean,
                    //260 --    //race_malaysian,  race_pakistani,  race_vietnamese,  race_other_asian,  race_other_asian_name,
                    //race_black,  race_pacific_islander,  race_guamanian,  race_hawaiian,  race_samoan,
                    //270 --   //race_other_pacific_islander,  race_other_pacific_islander_name,  race_white,  gpa_cumulative_gpa,  gpa_cumulative_gpa_hours,
                    //271 --  gpa_cumulative_gpa_quality_points

                    SQLcmd = "dbo.ApplicantExtendedLoad_Insert";
                    //SQLcmd += ", @PharmCASEthnicity='" + values[] + "'";
                    SQLcmd += "  @cas_id='" + values[0] + "'";
                    SQLcmd += ", @salutation='" + values[1] + "'";
                    SQLcmd += ", @first_name='" + values[2] + "'";
                    SQLcmd += ", @middle_name='" + values[3] + "'";
                    SQLcmd += ", @last_name='" + values[4] + "'";
                    // 5
                    SQLcmd += ", @suffix='" + values[5] + "'";
                    SQLcmd += ", @applicant_verified='" + values[6] + "'";
                    SQLcmd += ", @application_last_changed_on='" + values[7] + "'";
                    SQLcmd += ", @number_evaluations_received='" + values[8] + "'";
                    SQLcmd += ", @program_plans='" + values[9] + "'";
                    //10
                    SQLcmd += ", @military_status_extended='" + values[10] + "'";
                    SQLcmd += ", @current_street_address='" + values[11] + "'";
                    SQLcmd += ", @current_street_address_2='" + values[12] + "'";
                    SQLcmd += ", @current_city='" + values[13] + "'";
                    SQLcmd += ", @current_county='" + values[14] + "'";
                    //15
                    SQLcmd += ", @current_state_code='" + values[15] + "'";
                    SQLcmd += ", @current_state='" + values[16] + "'";
                    SQLcmd += ", @current_country='" + values[17] + "'";
                    SQLcmd += ", @current_country_name='" + values[18] + "'";
                    SQLcmd += ", @current_postal_code='" + values[19] + "'";
                    //20

                    SQLcmd += ", @current_address_valid_until='" + values[20] + "'";
                    SQLcmd += ", @current_country_iso_2_code='" + values[21] + "'";
                    SQLcmd += ", @current_country_iso_3_code='" + values[22] + "'";
                    SQLcmd += ", @current_county_code='" + values[23] + "'";
                    SQLcmd += ", @freshman_total_gpa='" + values[24] + "'";
                    //25
                    SQLcmd += ", @freshman_total_hours='" + values[25] + "'";
                    SQLcmd += ", @freshman_total_quality_points='" + values[26] + "'";
                    SQLcmd += ", @sophomore_total_gpa='" + values[27] + "'";
                    SQLcmd += ", @sophomore_total_hours='" + values[28] + "'";
                    SQLcmd += ", @sophomore_total_quality_points='" + values[29] + "'";
                    //30
                    SQLcmd += ", @junior_total_gpa='" + values[30] + "'";
                    SQLcmd += ", @junior_total_hours='" + values[31] + "'";
                    SQLcmd += ", @junior_total_quality_points='" + values[32] + "'";
                    SQLcmd += ", @senior_total_gpa='" + values[33] + "'";
                    SQLcmd += ", @senior_total_hours='" + values[34] + "'";
                    //35
                    SQLcmd += ", @senior_total_quality_points='" + values[35] + "'";
                    SQLcmd += ", @graduate_total_gpa='" + values[36] + "'";
                    SQLcmd += ", @graduate_total_hours='" + values[37] + "'";
                    SQLcmd += ", @graduate_total_quality_points='" + values[38] + "'";
                    SQLcmd += ", @baccalaureate_total_gpa='" + values[39] + "'";
                    //40
                    SQLcmd += ", @baccalaureate_total_hours='" + values[40] + "'";
                    SQLcmd += ", @baccalaureate_total_quality_points='" + values[41] + "'";
                    SQLcmd += ", @international_total_gpa='" + values[42] + "'";
                    SQLcmd += ", @international_total_hours='" + values[43] + "'";
                    SQLcmd += ", @international_total_quality_points='" + values[44] + "'";
                    //45
                    SQLcmd += ", @post_baccalaureate_total_gpa='" + values[45] + "'";
                    SQLcmd += ", @post_baccalaureate_total_hours='" + values[46] + "'";
                    SQLcmd += ", @post_baccalaureate_total_quality_points='" + values[47] + "'";
                    SQLcmd += ", @cumulative_undergraduate_total_gpa='" + values[48] + "'";
                    SQLcmd += ", @cumulative_undergraduate_total_hours='" + values[49] + "'";
                    //50
                    SQLcmd += ", @cumulative_undergraduate_total_quality_points='" + values[50] + "'";
                    SQLcmd += ", @freshman_mathematics_total_gpa='" + values[51] + "'";
                    SQLcmd += ", @freshman_mathematics_total_hours='" + values[52] + "'";
                    SQLcmd += ", @freshman_mathematics_total_quality_points='" + values[53] + "'";
                    SQLcmd += ", @overall_total_gpa='" + values[54] + "'";
                    //55
                    SQLcmd += ", @overall_total_hours='" + values[55] + "'";
                    SQLcmd += ", @overall_total_quality_points='" + values[56] + "'";
                    SQLcmd += ", @sophomore_mathematics_total_gpa='" + values[57] + "'";
                    SQLcmd += ", @sophomore_mathematics_total_hours='" + values[58] + "'";
                    SQLcmd += ", @sophomore_mathematics_total_quality_points='" + values[59] + "'";
                    //60
                    SQLcmd += ", @junior_mathematics_total_gpa='" + values[60] + "'";
                    SQLcmd += ", @junior_mathematics_total_hours='" + values[61] + "'";
                    SQLcmd += ", @junior_mathematics_total_quality_points='" + values[62] + "'";
                    SQLcmd += ", @senior_mathematics_total_gpa='" + values[63] + "'";
                    SQLcmd += ", @senior_mathematics_total_hours='" + values[64] + "'";
                    //65
                    SQLcmd += ", @senior_mathematics_total_quality_points='" + values[65] + "'";
                    SQLcmd += ", @post_baccalaureate_mathematics_total_gpa='" + values[66] + "'";
                    SQLcmd += ", @post_baccalaureate_mathematics_total_hours='" + values[67] + "'";
                    SQLcmd += ", @post_baccalaureate_mathematics_total_quality_points='" + values[68] + "'";
                    SQLcmd += ", @cumulative_undergraduate_mathematics_total_gpa='" + values[69] + "'";
                    //70
                    SQLcmd += ", @cumulative_undergraduate_mathematics_total_hours='" + values[70] + "'";
                    SQLcmd += ", @cumulative_undergraduate_mathematics_total_quality_points='" + values[71] + "'";
                    SQLcmd += ", @graduate_mathematics_total_gpa='" + values[72] + "'";
                    SQLcmd += ", @graduate_mathematics_total_hours='" + values[73] + "'";
                    SQLcmd += ", @graduate_mathematics_total_quality_points='" + values[74] + "'";
                    //75
                    SQLcmd += ", @overall_mathematics_total_gpa='" + values[75] + "'";
                    SQLcmd += ", @overall_mathematics_total_hours='" + values[76] + "'";
                    SQLcmd += ", @overall_mathematics_total_quality_points='" + values[77] + "'";
                    SQLcmd += ", @freshman_non_science_total_gpa='" + values[78] + "'";
                    SQLcmd += ", @freshman_non_science_total_hours='" + values[79] + "'";
                    //80
                    SQLcmd += ", @freshman_non_science_total_quality_points='" + values[80] + "'";
                    SQLcmd += ", @sophomore_non_science_total_gpa='" + values[81] + "'";
                    SQLcmd += ", @sophomore_non_science_total_hours='" + values[82] + "'";
                    SQLcmd += ", @sophomore_non_science_total_quality_points='" + values[83] + "'";
                    SQLcmd += ", @junior_non_science_total_gpa='" + values[84] + "'";
                    //85
                    SQLcmd += ", @junior_non_science_total_hours='" + values[85] + "'";
                    SQLcmd += ", @junior_non_science_total_quality_points='" + values[86] + "'";
                    SQLcmd += ", @senior_non_science_total_gpa='" + values[87] + "'";
                    SQLcmd += ", @senior_non_science_total_hours='" + values[88] + "'";
                    SQLcmd += ", @senior_non_science_total_quality_points='" + values[89] + "'";
                    //90
                    SQLcmd += ", @post_baccalaureate_non_science_total_gpa='" + values[90] + "'";
                    SQLcmd += ", @post_baccalaureate_non_science_total_hours='" + values[91] + "'";
                    SQLcmd += ", @post_baccalaureate_non_science_total_quality_points='" + values[92] + "'";
                    SQLcmd += ", @cumulative_undergraduate_non_science_total_gpa='" + values[93] + "'";
                    SQLcmd += ", @cumulative_undergraduate_non_science_total_hours='" + values[94] + "'";
                    //95
                    SQLcmd += ", @cumulative_undergraduate_non_science_total_quality_points='" + values[95] + "'";
                    SQLcmd += ", @graduate_non_science_total_gpa='" + values[96] + "'";
                    SQLcmd += ", @graduate_non_science_total_hours='" + values[97] + "'";
                    SQLcmd += ", @graduate_non_science_total_quality_points='" + values[98] + "'";
                    SQLcmd += ", @overall_non_science_total_gpa='" + values[99] + "'";
                    //100
                    SQLcmd += ", @overall_non_science_total_hours='" + values[100] + "'";
                    SQLcmd += ", @overall_non_science_total_quality_points='" + values[101] + "'";
                    SQLcmd += ", @freshman_science_total_gpa='" + values[102] + "'";
                    SQLcmd += ", @freshman_science_total_hours='" + values[103] + "'";
                    SQLcmd += ", @freshman_science_total_quality_points='" + values[104] + "'";
                    //105
                    SQLcmd += ", @sophomore_science_total_gpa='" + values[105] + "'";
                    SQLcmd += ", @sophomore_science_total_hours='" + values[106] + "'";
                    SQLcmd += ", @sophomore_science_total_quality_points='" + values[107] + "'";
                    SQLcmd += ", @junior_science_total_gpa='" + values[108] + "'";
                    SQLcmd += ", @junior_science_total_hours='" + values[109] + "'";
                    //110
                    SQLcmd += ", @junior_science_total_quality_points='" + values[110] + "'";
                    SQLcmd += ", @senior_science_total_gpa='" + values[111] + "'";
                    SQLcmd += ", @senior_science_total_hours='" + values[112] + "'";
                    SQLcmd += ", @senior_science_total_quality_points='" + values[113] + "'";
                    SQLcmd += ", @post_baccalaureate_science_total_gpa='" + values[114] + "'";
                    //115
                    SQLcmd += ", @post_baccalaureate_science_total_hours='" + values[115] + "'";
                    SQLcmd += ", @post_baccalaureate_science_total_quality_points='" + values[116] + "'";
                    SQLcmd += ", @cumulative_undergraduate_science_total_gpa='" + values[117] + "'";
                    SQLcmd += ", @cumulative_undergraduate_science_total_hours='" + values[118] + "'";
                    SQLcmd += ", @cumulative_undergraduate_science_total_quality_points='" + values[119] + "'";
                    //120
                    SQLcmd += ", @graduate_science_total_gpa='" + values[120] + "'";
                    SQLcmd += ", @graduate_science_total_hours='" + values[121] + "'";
                    SQLcmd += ", @graduate_science_total_quality_points='" + values[122] + "'";
                    SQLcmd += ", @overall_science_total_gpa='" + values[123] + "'";
                    SQLcmd += ", @overall_science_total_hours='" + values[124] + "'";
                    //125
                    SQLcmd += ", @overall_science_total_quality_points='" + values[125] + "'";
                    SQLcmd += ", @permanent_street_address='" + values[126] + "'";
                    SQLcmd += ", @permanent_street_address_2='" + values[127] + "'";
                    SQLcmd += ", @permanent_city='" + values[128] + "'";
                    SQLcmd += ", @permanent_county='" + values[129] + "'";
                    //130
                    SQLcmd += ", @permanent_county_code='" + values[130] + "'";
                    SQLcmd += ", @permanent_state='" + values[131] + "'";
                    SQLcmd += ", @permanent_state_code='" + values[132] + "'";
                    SQLcmd += ", @permanent_postal_code='" + values[133] + "'";
                    SQLcmd += ", @permanent_country_name='" + values[134] + "'";
                    //135
                    SQLcmd += ", @permanent_country='" + values[135] + "'";
                    SQLcmd += ", @permanent_country_iso_2_code='" + values[136] + "'";
                    SQLcmd += ", @permanent_country_iso_3_code='" + values[137] + "'";
                    SQLcmd += ", @gender_code='" + values[138] + "'";
                    SQLcmd += ", @gender='" + values[139] + "'";
                    //140
                    SQLcmd += ", @materials_under_another_name='" + values[140] + "'";
                    SQLcmd += ", @alt_first_name='" + values[141] + "'";
                    SQLcmd += ", @alt_middle_name='" + values[142] + "'";
                    SQLcmd += ", @alt_last_name='" + values[143] + "'";
                    SQLcmd += ", @citizenship='" + values[144] + "'";
                    //145
                    SQLcmd += ", @citizenship_country_name='" + values[145] + "'";
                    SQLcmd += ", @citizenship_status='" + values[146] + "'";
                    SQLcmd += ", @holds_visa='" + values[147] + "'";
                    SQLcmd += ", @visa_type_code='" + values[148] + "'";
                    SQLcmd += ", @visa_type='" + values[149] + "'";
                    //150
                    SQLcmd += ", @visa_issuing_city='" + values[150] + "'";
                    SQLcmd += ", @country_of_visa_issue_code='" + values[151] + "'";
                    SQLcmd += ", @country_of_visa_issue='" + values[152] + "'";
                    SQLcmd += ", @date_of_birth='" + values[153] + "'";
                    SQLcmd += ", @age_at_submission='" + values[154] + "'";
                    //155
                    SQLcmd += ", @city_of_birth='" + values[155] + "'";
                    SQLcmd += ", @county_of_birth_code='" + values[156] + "'";
                    SQLcmd += ", @county_of_birth='" + values[157] + "'";
                    SQLcmd += ", @state_of_birth_code='" + values[158] + "'";
                    SQLcmd += ", @state_of_birth='" + values[159] + "'";
                    //160
                    SQLcmd += ", @country_of_birth='" + values[160] + "'";
                    SQLcmd += ", @country_of_birth_name='" + values[161] + "'";
                    SQLcmd += ", @state_of_residence_code='" + values[162] + "'";
                    SQLcmd += ", @state_of_residence='" + values[163] + "'";
                    SQLcmd += ", @county_of_residence_code='" + values[164] + "'";
                    //165
                    SQLcmd += ", @county_of_residence_name='" + values[165] + "'";
                    SQLcmd += ", @dual_citizenship='" + values[166] + "'";
                    SQLcmd += ", @second_country_citizenship_code='" + values[167] + "'";
                    SQLcmd += ", @second_country_citizenship='" + values[168] + "'";
                    SQLcmd += ", @citizenship_country_iso_2_code='" + values[169] + "'";
                    //170
                    SQLcmd += ", @citizenship_country_iso_3_code='" + values[170] + "'";
                    SQLcmd += ", @citizenship_status_code='" + values[171] + "'";
                    SQLcmd += ", @country_of_birth_iso_2_code='" + values[172] + "'";
                    SQLcmd += ", @country_of_birth_iso_3_code='" + values[173] + "'";
                    SQLcmd += ", @country_of_visa_issue_iso_2_code='" + values[174] + "'";
                    //175
                    SQLcmd += ", @country_of_visa_issue_iso_3_code='" + values[175] + "'";
                    SQLcmd += ", @us_residence_time_range='" + values[176] + "'";
                    SQLcmd += ", @issuing_authority='" + values[177] + "'";
                    SQLcmd += ", @second_country_citizenship_iso_2_code='" + values[178] + "'";
                    SQLcmd += ", @second_country_citizenship_iso_3_code='" + values[179] + "'";
                    //180
                    SQLcmd += ", @state_of_residence_time_range='" + values[180] + "'";
                    SQLcmd += ", @us_visa_number='" + values[181] + "'";
                    SQLcmd += ", @visa_issuing_date='" + values[182] + "'";
                    SQLcmd += ", @visa_sponsor='" + values[183] + "'";
                    SQLcmd += ", @visa_valid_until='" + values[184] + "'";
                    //185
                    SQLcmd += ", @preferred_first_name='" + values[185] + "'";
                    SQLcmd += ", @preferred_middle_name='" + values[186] + "'";
                    SQLcmd += ", @preferred_phone='" + values[187] + "'";
                    SQLcmd += ", @alternate_phone='" + values[188] + "'";
                    SQLcmd += ", @email='" + values[189] + "'";
                    //190
                    SQLcmd += ", @alternate_phone_type='" + values[190] + "'";
                    SQLcmd += ", @email_type='" + values[191] + "'";
                    SQLcmd += ", @preferred_phone_number_type='" + values[192] + "'";
                    SQLcmd += ", @text_authorization='" + values[193] + "'";
                    SQLcmd += ", @gpas_by_school_gpa_0='" + values[194] + "'";
                    //195
                    SQLcmd += ", @gpas_by_school_gpa_1='" + values[195] + "'";
                    SQLcmd += ", @gpas_by_school_gpa_2='" + values[196] + "'";
                    SQLcmd += ", @gpas_by_school_gpa_3='" + values[197] + "'";
                    SQLcmd += ", @gpas_by_school_gpa_4='" + values[198] + "'";
                    SQLcmd += ", @gpas_by_school_gpa_5='" + values[199] + "'";
                    //200
                    SQLcmd += ", @gpas_by_school_gpa_6='" + values[200] + "'";
                    SQLcmd += ", @gpas_by_school_gpa_7='" + values[201] + "'";
                    SQLcmd += ", @gpas_by_school_gpa_8='" + values[202] + "'";
                    SQLcmd += ", @gpas_by_school_credit_hours_0='" + values[203] + "'";
                    SQLcmd += ", @gpas_by_school_credit_hours_1='" + values[204] + "'";
                    //205
                    SQLcmd += ", @gpas_by_school_credit_hours_2='" + values[205] + "'";
                    SQLcmd += ", @gpas_by_school_credit_hours_3='" + values[206] + "'";
                    SQLcmd += ", @gpas_by_school_credit_hours_4='" + values[207] + "'";
                    SQLcmd += ", @gpas_by_school_credit_hours_5='" + values[208] + "'";
                    SQLcmd += ", @gpas_by_school_credit_hours_6='" + values[209] + "'";
                    //210
                    SQLcmd += ", @gpas_by_school_credit_hours_7='" + values[210] + "'";
                    SQLcmd += ", @gpas_by_school_credit_hours_8='" + values[211] + "'";
                    SQLcmd += ", @gpas_by_school_quality_points_0='" + values[212] + "'";
                    SQLcmd += ", @gpas_by_school_quality_points_1='" + values[213] + "'";
                    SQLcmd += ", @gpas_by_school_quality_points_2='" + values[214] + "'";
                    //215
                    SQLcmd += ", @gpas_by_school_quality_points_3='" + values[215] + "'";
                    SQLcmd += ", @gpas_by_school_quality_points_4='" + values[216] + "'";
                    SQLcmd += ", @gpas_by_school_quality_points_5='" + values[217] + "'";
                    SQLcmd += ", @gpas_by_school_quality_points_6='" + values[218] + "'";
                    SQLcmd += ", @gpas_by_school_quality_points_7='" + values[219] + "'";
                    //220
                    SQLcmd += ", @gpas_by_school_quality_points_8='" + values[220] + "'";
                    SQLcmd += ", @gpas_by_school_college_id_0='" + values[221] + "'";
                    SQLcmd += ", @gpas_by_school_college_id_1='" + values[222] + "'";
                    SQLcmd += ", @gpas_by_school_college_id_2='" + values[223] + "'";
                    SQLcmd += ", @gpas_by_school_college_id_3='" + values[224] + "'";
                    //225
                    SQLcmd += ", @gpas_by_school_college_id_4='" + values[225] + "'";
                    SQLcmd += ", @gpas_by_school_college_id_5='" + values[226] + "'";
                    SQLcmd += ", @gpas_by_school_college_id_6='" + values[227] + "'";
                    SQLcmd += ", @gpas_by_school_college_id_7='" + values[228] + "'";
                    SQLcmd += ", @gpas_by_school_college_id_8='" + values[229] + "'";
                    //230
                    SQLcmd += ", @gpas_by_school_college_name_0='" + values[230] + "'";
                    SQLcmd += ", @gpas_by_school_college_name_1='" + values[231] + "'";
                    SQLcmd += ", @gpas_by_school_college_name_2='" + values[232] + "'";
                    SQLcmd += ", @gpas_by_school_college_name_3='" + values[233] + "'";
                    SQLcmd += ", @gpas_by_school_college_name_4='" + values[234] + "'";
                    //235
                    SQLcmd += ", @gpas_by_school_college_name_5='" + values[235] + "'";
                    SQLcmd += ", @gpas_by_school_college_name_6='" + values[236] + "'";
                    SQLcmd += ", @gpas_by_school_college_name_7='" + values[237] + "'";
                    SQLcmd += ", @gpas_by_school_college_name_8='" + values[238] + "'";
                    SQLcmd += ", @race_hispanic='" + values[239] + "'";
                    //240
                    SQLcmd += ", @race_cuban='" + values[240] + "'";
                    SQLcmd += ", @race_mexican='" + values[241] + "'";
                    SQLcmd += ", @race_puerto_rican='" + values[242] + "'";
                    SQLcmd += ", @race_south_american='" + values[243] + "'";
                    SQLcmd += ", @race_other_spanish='" + values[244] + "'";
                    //245
                    SQLcmd += ", @race_other_spanish_name='" + values[245] + "'";
                    SQLcmd += ", @race_american_indian='" + values[246] + "'";
                    SQLcmd += ", @race_indian_tribe_name='" + values[247] + "'";
                    SQLcmd += ", @race_asian='" + values[248] + "'";
                    SQLcmd += ", @race_asian_indian='" + values[249] + "'";
                    //250
                    SQLcmd += ", @race_cambodian='" + values[250] + "'";
                    SQLcmd += ", @race_chinese='" + values[251] + "'";
                    SQLcmd += ", @race_filipino='" + values[252] + "'";
                    SQLcmd += ", @race_japanese='" + values[253] + "'";
                    SQLcmd += ", @race_korean='" + values[254] + "'";
                    //255
                    SQLcmd += ", @race_malaysian='" + values[255] + "'";
                    SQLcmd += ", @race_pakistani='" + values[256] + "'";
                    SQLcmd += ", @race_vietnamese='" + values[257] + "'";
                    SQLcmd += ", @race_other_asian='" + values[258] + "'";
                    SQLcmd += ", @race_other_asian_name='" + values[259] + "'";
                    //260
                    SQLcmd += ", @race_black='" + values[260] + "'";
                    SQLcmd += ", @race_pacific_islander='" + values[261] + "'";
                    SQLcmd += ", @race_guamanian='" + values[262] + "'";
                    SQLcmd += ", @race_hawaiian='" + values[263] + "'";
                    SQLcmd += ", @race_samoan='" + values[264] + "'";
                    //265
                    SQLcmd += ", @race_other_pacific_islander='" + values[265] + "'";
                    SQLcmd += ", @race_other_pacific_islander_name='" + values[266] + "'";
                    SQLcmd += ", @race_white='" + values[267] + "'";
                    SQLcmd += ", @local_gpa_cumulative_gpa='" + values[268] + "'";
                    SQLcmd += ", @local_gpa_cumulative_gpa_hours='" + values[269] + "'";
                    //270
                    SQLcmd += ", @local_gpa_cumulative_gpa_quality_points='" + values[270] + "'";
                    using (SqlCommand cmd = new SqlCommand(SQLcmd, cnn))
                    {
                        if (RowCount > 1)
                        {
                            cmd.ExecuteNonQuery();
                        }
                        //Tester = "2";

                        //MessageBox.Show("Row inserted !! ");
                    }
                    //cnn.Close;
                }
            }
        }

        protected void btnCreateOutputFiles_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();

                string SQLcmd = "";
                // start here
                //SqlCommand command = new SqlCommand("", cnn);
                //command.Parameters.AddWithValue("@cas_id", "09877");

                SQLcmd = "dbo.CreateOutputData ";

                using (SqlCommand cmd = new SqlCommand(SQLcmd, cnn))
                {
                    //cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            btnCreateOutputFiles.Text = reader.GetString(0);
                            btnCreateOutputFiles.Enabled = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }
            }

            //CreateAllSONISOutputFiles();
        }

        //*************************************** create the text files **********************************************

        private void CreateAllSONISOutputFiles()
        {
            CreateSONISStudentsFile();
            CreateSONISAddressPermanentFile();
            CreateSONISAddressMailingFile();
            CreateSONISAddressLocalFile();
            CreateSONISEducationFile();
        }

        private void CreateSONISStudentsFile()
        {
            string constr = connectionString;
            string strFileContents = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from vwSONISNamesStudent ";
                    cmd.Connection = con;
                    con.Open();
                    strFileContents += "Source System,Source ID,Jenzabar ID,last_name,first_name,mi,prefix,suffix,birthdate,citizen,gender,ethnicity,veteran,division,department,campus,cohort,program,level,ssn,iped_status,tuition_status" + System.Environment.NewLine; ;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strFileContents += sdr[0].ToString() + ",";  //   [Source System]
                                strFileContents += sdr[1].ToString() + ",";  // ,[Source ID]
                                strFileContents += sdr[2].ToString() + ",";  // ,[Jenzabar ID]
                                strFileContents += sdr[3].ToString() + ",";  // ,[last_name]
                                strFileContents += sdr[4].ToString() + ",";  // ,[first_name]
                                strFileContents += sdr[5].ToString() + ",";  // ,[mi]
                                strFileContents += sdr[6].ToString() + ",";  // ,[prefix]
                                strFileContents += sdr[7].ToString() + ",";  // ,[suffix]
                                strFileContents += sdr[8].ToString() + ",";  // ,[birthdate]
                                strFileContents += sdr[9].ToString() + ",";  // ,[citizen]
                                strFileContents += sdr[1].ToString() + ",";  // ,[gender]
                                strFileContents += sdr[11].ToString() + ",";  // ,[ethnicity]
                                strFileContents += sdr[12].ToString() + ",";  // ,[veteran]
                                strFileContents += sdr[13].ToString() + ",";  // ,[division]
                                strFileContents += sdr[14].ToString() + ",";  // ,[department]
                                strFileContents += sdr[15].ToString() + ",";  // ,[campus]
                                strFileContents += sdr[16].ToString() + ",";  // ,[cohort]
                                strFileContents += sdr[17].ToString() + ",";  // ,[program]
                                strFileContents += sdr[18].ToString() + ",";  // ,[level]
                                strFileContents += sdr[19].ToString() + ",";  // ,[ssn]
                                strFileContents += sdr[20].ToString() + ",";  // ,[iped_status]
                                strFileContents += sdr[21].ToString();  // ,[tuition_status]
                                strFileContents += System.Environment.NewLine;
                            }
                        }
                    }

                    con.Close();
                }

                DownloadFile("4students.csv", strFileContents);               
            }
        }

        private void DownloadFile(string fileName, string strContents)
        {
            string fName = Server.MapPath("~/") + fileName;
            System.IO.File.WriteAllText(fName, strContents);
            HttpResponseMessage response = new HttpResponseMessage();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/excel";
            System.IO.FileInfo file = new System.IO.FileInfo(fName);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name); Response.AddHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(fName);
                Response.Flush();
                Response.End();
            }
        }

        private void CreateSONISAddressPermanentFile()
        {
            string constr = connectionString;
            string strFileContents = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from vwSONISAddressesPermanent ";
                    cmd.Connection = con;
                    con.Open();
                    strFileContents += "Source System,Source ID,Jenzabar ID,last_name,first_name,middle_name,preferred,type,e_mail,address1,address2,city,state,zip,county,country,phone,cell_phone" + System.Environment.NewLine; ;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strFileContents += sdr[0].ToString() + ",";  //   [Source System]
                                strFileContents += sdr[1].ToString() + ",";  // ,[Source ID]
                                strFileContents += sdr[2].ToString() + ",";  // ,[Jenzabar ID]
                                strFileContents += sdr[3].ToString() + ",";  // ,[last_name]
                                strFileContents += sdr[4].ToString() + ",";  // ,[first_name]
                                strFileContents += sdr[5].ToString() + ",";  // ,[middle_name]
                                strFileContents += sdr[6].ToString() + ",";  // ,[preferred]
                                strFileContents += sdr[7].ToString() + ",";  // ,[type]
                                strFileContents += sdr[8].ToString() + ",";  // ,[e_mail]
                                strFileContents += sdr[9].ToString() + ",";  // ,[address1]
                                strFileContents += sdr[10].ToString() + ",";  // ,[address2]
                                strFileContents += sdr[11].ToString() + ",";  // ,[city]
                                strFileContents += sdr[12].ToString() + ",";  // ,[state]
                                strFileContents += sdr[13].ToString() + ",";  // ,[zip]
                                strFileContents += sdr[14].ToString() + ",";  // ,[county]
                                strFileContents += sdr[15].ToString() + ",";  // ,[country]
                                strFileContents += sdr[16].ToString() + ",";  // ,[phone]
                                strFileContents += sdr[17].ToString();  // ,[cell_phone]
                                strFileContents += System.Environment.NewLine;
                            }
                        }
                    }
                    con.Close();
                }
                con.Close();
            }

            //5AddressesPermanent
            DownloadFile("5AddressesPermanent.csv", strFileContents);
            
        }

        private void CreateSONISAddressMailingFile()
        {
            string constr = connectionString;
            string strFileContents = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from vwSONISAddressesMailing ";
                    cmd.Connection = con;
                    con.Open();
                    strFileContents += "Source System,Source ID,Jenzabar ID,last_name,first_name,middle_name,preferred,type,e_mail,address1,address2,city,state,zip,county,country,phone,cell_phone" + System.Environment.NewLine; ;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strFileContents += sdr[0].ToString() + ",";  //   [Source System]
                                strFileContents += sdr[1].ToString() + ",";  // ,[Source ID]
                                strFileContents += sdr[2].ToString() + ",";  // ,[Jenzabar ID]
                                strFileContents += sdr[3].ToString() + ",";  // ,[last_name]
                                strFileContents += sdr[4].ToString() + ",";  // ,[first_name]
                                strFileContents += sdr[5].ToString() + ",";  // ,[middle_name]
                                strFileContents += sdr[6].ToString() + ",";  // ,[preferred]
                                strFileContents += sdr[7].ToString() + ",";  // ,[type]
                                strFileContents += sdr[8].ToString() + ",";  // ,[e_mail]
                                strFileContents += sdr[9].ToString() + ",";  // ,[address1]
                                strFileContents += sdr[10].ToString() + ",";  // ,[address2]
                                strFileContents += sdr[11].ToString() + ",";  // ,[city]
                                strFileContents += sdr[12].ToString() + ",";  // ,[state]
                                strFileContents += sdr[13].ToString() + ",";  // ,[zip]
                                strFileContents += sdr[14].ToString() + ",";  // ,[county]
                                strFileContents += sdr[15].ToString() + ",";  // ,[country]
                                strFileContents += sdr[16].ToString() + ",";  // ,[phone]
                                strFileContents += sdr[17].ToString();  // ,[cell_phone]
                                strFileContents += System.Environment.NewLine;
                            }
                        }
                    }
                }
                con.Close();
            }
            //5AddressesMailing
            DownloadFile("5AddressesMailing.csv", strFileContents);
            
        }

        private void CreateSONISAddressLocalFile()
        {
            string constr = connectionString;
            string strFileContents = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from vwSONISAddressesLocal ";
                    cmd.Connection = con;
                    con.Open();

                    //******************************************** Header record
                    strFileContents += "";

                    strFileContents += "Source System,Source ID,Jenzabar ID,last_name,first_name,middle_name,preferred,type,e_mail,address1,address2,city,state,zip,county,country,phone,cell_phone" + System.Environment.NewLine;
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strFileContents += sdr[0].ToString() + ",";  //   [Source System]
                                strFileContents += sdr[1].ToString() + ",";  // ,[Source ID]
                                strFileContents += sdr[2].ToString() + ",";  // ,[Jenzabar ID]
                                strFileContents += sdr[3].ToString() + ",";  // ,[last_name]
                                strFileContents += sdr[4].ToString() + ",";  // ,[first_name]
                                strFileContents += sdr[5].ToString() + ",";  // ,[middle_name]
                                strFileContents += sdr[6].ToString() + ",";  // ,[preferred]
                                strFileContents += sdr[7].ToString() + ",";  // ,[type]
                                strFileContents += sdr[8].ToString() + ",";  // ,[e_mail]
                                strFileContents += sdr[9].ToString() + ",";  // ,[address1]
                                strFileContents += sdr[10].ToString() + ",";  // ,[address2]
                                strFileContents += sdr[11].ToString() + ",";  // ,[city]
                                strFileContents += sdr[12].ToString() + ",";  // ,[state]
                                strFileContents += sdr[13].ToString() + ",";  // ,[zip]
                                strFileContents += sdr[14].ToString() + ",";  // ,[county]
                                strFileContents += sdr[15].ToString() + ",";  // ,[country]
                                strFileContents += sdr[16].ToString() + ",";  // ,[phone]
                                strFileContents += sdr[17].ToString();  // ,[cell_phone]
                                strFileContents += System.Environment.NewLine;
                            }
                        }
                    }
                }
                con.Close();
            }
            //5AddressesLocal
            DownloadFile("5AddressesLocal.csv", strFileContents);            
        }

        private void CreateSONISEducationFile()
        {
            string constr = connectionString;
            string strFileContents = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from vwSONISEducation ";
                    cmd.Connection = con;
                    con.Open();
                    strFileContents += "Source System,Source ID,Jenzabar ID,last_name,first_name,middle_name,graduated,institution_uniqueID,institution_txt,degree,GPA,credits,quality_points,graduation_date,cohort_code" + System.Environment.NewLine; ;
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strFileContents += sdr[0].ToString() + ",";  //   [Source System]
                                strFileContents += sdr[1].ToString() + ",";  // ,[Source ID]
                                strFileContents += sdr[2].ToString() + ",";  // ,[Jenzabar ID]
                                strFileContents += sdr[3].ToString() + ",";  // ,[last_name]
                                strFileContents += sdr[4].ToString() + ",";  // ,[first_name]
                                strFileContents += sdr[5].ToString() + ",";  // ,[middle_name]
                                strFileContents += sdr[6].ToString() + ",";  // ,[graduated]
                                strFileContents += sdr[7].ToString() + ",";  // ,[institution_uniqueID]
                                strFileContents += sdr[8].ToString() + ",";  // ,[institution_txt]
                                strFileContents += sdr[9].ToString() + ",";  // ,[degree]
                                strFileContents += sdr[10].ToString() + ",";  // ,[GPA]
                                strFileContents += sdr[11].ToString() + ",";  // ,[credits]
                                strFileContents += sdr[12].ToString() + ",";  // ,[quality_points]
                                strFileContents += sdr[13].ToString() + ",";  // ,[graduation_date]
                                strFileContents += sdr[14].ToString();  // ,[cohort_code]
                                strFileContents += System.Environment.NewLine;
                            }
                        }
                    }
                }
                con.Close();
            }

            //7Education
            DownloadFile("7Education.csv", strFileContents);
        }

        protected void btnDownloadTextFiles_Click(object sender, EventArgs e)
        {
            CreateAllSONISOutputFiles();
        }

        protected void btnDownloadSONISStudents_Click(object sender, EventArgs e)
        {
            CreateSONISStudentsFile();
        }

        protected void btnDownloadSONISAddressesLocal_Click(object sender, EventArgs e)
        {
            CreateSONISAddressLocalFile();
        }

        protected void btnDownloadSONISAddressesMailing_Click(object sender, EventArgs e)
        {
            CreateSONISAddressMailingFile();
        }

        protected void btnDownloadSONISAddressesPermanent_Click(object sender, EventArgs e)
        {
            CreateSONISAddressPermanentFile();
        }

        protected void btnDownloadSONISEducation_Click(object sender, EventArgs e)
        {
            CreateSONISEducationFile();
        }
    }
}