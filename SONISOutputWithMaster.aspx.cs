using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace PharmCASASPX
{
    public partial class SONISOutputWithMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadGridData();
            }
        }

        private void LoadGridData()
        {
            string sQuery = "SONISStudents_select ";
            sQuery += "@pharmcasid ='" + this.txtCASID.Text + "' ";
            sQuery += ",@lastname='" + this.txtLastName.Text + "' ";
            sQuery += ",@firstname='" + this.txtFirstName.Text + "' ";
            sQuery += ", @jenzabarid ='" + this.txtJenzabarID.Text + "'";
            gvSONISStudents.DataSource = GetData(sQuery);
            gvSONISStudents.DataBind();
        }
        private static DataTable GetData(string query)
        {
            //string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string strConnString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string customerId = gvSONISStudents.DataKeys[e.Row.RowIndex].Value.ToString();
                //GridView gvEducation = e.Row.FindControl("gvEducation") as GridView;
                //gvEducation.DataSource = GetData(string.Format("select * from vwSonisEducation where [Source Id]='{0}'", customerId));
                //gvEducation.DataBind();

                //GridView gvMAddress = e.Row.FindControl("gvMailingAddress") as GridView;
                //gvMAddress.DataSource = GetData(string.Format("select * from vwSonisAddressesMailing where [Source Id]='{0}'", customerId));
                //gvMAddress.DataBind();

                //GridView gvPAddress = e.Row.FindControl("gvPermanentAddress") as GridView;
                //gvPAddress.DataSource = GetData(string.Format("select * from vwSonisAddressesPermanent where [Source Id]='{0}'", customerId));
                //gvPAddress.DataBind();

                //GridView gvLAddress = e.Row.FindControl("gvLocalAddress") as GridView;
                //gvLAddress.DataSource = GetData(string.Format("select * from vwSonisAddressesLocal where [Source Id]='{0}'", customerId));
                //gvLAddress.DataBind();

            }
        }

        protected void StudentsGridView_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < gvSONISStudents.Rows.Count; i++)
            {
                // Ignore values that cannot be cast as integer.
                try
                {
                    if ((int)gvSONISStudents.DataKeys[i].Value == (int)ViewState["SelectedKey"])
                        gvSONISStudents.SelectedIndex = i;
                }
                catch { }
            }
        }

        protected void StudentsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            gvSONISStudents.SelectedIndex = -1;
        }

        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSONISStudents.PageIndex = e.NewPageIndex;
            LoadGridData();
        }

        protected void gvSONISStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (gvSONISStudents.SelectedIndex >= 0)
                ViewState["SelectedKey"] = gvSONISStudents.SelectedValue;
            else
                ViewState["SelectedKey"] = null;
            string test = e.ToString();
            //string customerId = gvSONISStudents.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvEducation = this.Master.FindControl("MainContent").FindControl("grdEducation") as GridView;
            gvEducation.DataSource = GetData(string.Format("select * from vwSonisEducation where [Source Id]='{0}'", ViewState["SelectedKey"]));
            gvEducation.DataBind();

            GridView gAddress = this.Master.FindControl("MainContent").FindControl("grdAddress") as GridView;
            gAddress.DataSource = GetData(string.Format("select * from vwSonisAddresses where [Source Id]='{0}'", ViewState["SelectedKey"]));
            gAddress.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void btnCreateOutput_Click(object sender, EventArgs e)
        {

        }
    }
}