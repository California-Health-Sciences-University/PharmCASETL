<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewSONISStudentWithMaster.aspx.cs" Inherits="PharmCASASPX.ViewSONISStudentWithMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
          <div>
           <asp:Table Width="100%" BorderColor="Black" BorderStyle="Solid" Font-Bold="true" runat="server">
                <asp:TableRow >
                    <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                         <asp:Label ID="Label5" runat="server" Text="SONIS Output Data" Font-Bold="true" Font-Size="Large"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
                    <asp:Table Width="100%" BorderColor="Black" BorderStyle="Solid" Font-Bold="true" runat="server" CellPadding="10">
                <asp:TableRow >
                    <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                         <asp:Label ID="Label6" runat="server" Text="Cohort" Font-Bold="true" ></asp:Label>
                    </asp:TableCell>
                         <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                         <asp:TextBox ID="txtCohort" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                         <asp:Label ID="Label8" runat="server" Text="Jenzabar ID" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                         <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                           <asp:TextBox ID="txtJenzabarID" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                         <asp:Label ID="Label10" runat="server" Text="Last Name" Font-Bold="true" ></asp:Label>
                    </asp:TableCell>
                         <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                         <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                         <asp:Label ID="Label12" runat="server" Text="First Name" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                         <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                         <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </asp:TableCell>
                                       <asp:TableCell  BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                                           
                        <asp:Button ID="btnCreateOutput" runat="server" Text="Create Output Files" OnClick="btnCreateOutput_Click" />
                    </asp:TableCell>

                </asp:TableRow>

            </asp:Table>
            
            <asp:GridView AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" AutoGenerateSelectButton="true" BorderWidth="2"
                BorderStyle="Solid" BorderColor="Black" CellPadding="3" GridLines="Both" runat="server" Width="100%"
                ID="gvSONISStudents" CssClass="Grid" DataKeyNames="Source ID"
                OnRowDataBound="OnRowDataBound"
                OnPageIndexChanging="grdData_PageIndexChanging"
                OnSelectedIndexChanged="gvSONISStudents_SelectedIndexChanged"
                OnDataBound="StudentsGridView_DataBound"
                OnSorting="StudentsGridView_Sorting">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                <Columns>

                    <asp:BoundField ItemStyle-Width="50px" DataField="Source System" HeaderText="Source System" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Source ID" HeaderText="Source ID" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Jenzabar ID" HeaderText="Jenzabar ID" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="last_name" HeaderText="Last Name" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="first_name" HeaderText="First Name" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="mi" HeaderText="MI" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="prefix" HeaderText="Prefix" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Suffix" HeaderText="Suffix" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="birthdate" HeaderText="Birthdate" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="citizen" HeaderText="Citizen" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="gender" HeaderText="Gender" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="ethnicity" HeaderText="Ethnicity" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="veteran" HeaderText="Veteran" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="division" HeaderText="Division" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Department" HeaderText="Department" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="campus" HeaderText="Campus" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Cohort" HeaderText="Cohort" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Program" HeaderText="Program" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="level" HeaderText="Level" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="SSN" HeaderText="SSN" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="iped_status" HeaderText="iped status" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="tuition_status" HeaderText="Tuition Status" />
                </Columns>
            </asp:GridView>
            <asp:GridView AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" BorderWidth="2"
                BorderStyle="Solid" BorderColor="Black" CellPadding="3" GridLines="Both" runat="server" Width="100%" CssClass="ChildGrid"
                ID="grdEducation" DataKeyNames="Source ID">
                <Columns>
                    <asp:BoundField ItemStyle-Width="50px" DataField="Source System" HeaderText="Source System" Visible="false" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Source ID" HeaderText="Source ID" Visible="true" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Jenzabar ID" HeaderText="Jenzabar ID" Visible="false" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="last_name" HeaderText="Last Name" Visible="false" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="first_name" HeaderText="First Name" Visible="false" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="middle_name" HeaderText="Middle Name" Visible="false" />

                    <asp:BoundField ItemStyle-Width="40px" DataField="graduated" HeaderText="Graduated" Visible="true" />
                    <asp:BoundField ItemStyle-Width="40px" DataField="Institution_uniqueID" HeaderText="Institution Code" Visible="true" />
                    <asp:BoundField ItemStyle-Width="200px" DataField="Institution_txt" HeaderText="Institution" Visible="true" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="degree" HeaderText="Degree" Visible="true" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="GPA" HeaderText="GPA" Visible="true" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="credits" HeaderText="Credits" Visible="true" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="quality_points" HeaderText="Quality Points" Visible="true" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="graduation_date" HeaderText="Graduation Date" Visible="true" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="cohort_code" HeaderText="Cohort" Visible="true" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="grdAddress" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid" Width="100%">
                <Columns>
                    <asp:BoundField ItemStyle-Width="50px" DataField="Source System" HeaderText="Source System" Visible="false" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Source ID" HeaderText="Source ID" Visible="true" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Jenzabar ID" HeaderText="Jenzabar ID" Visible="false" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="last_name" HeaderText="Last Name" Visible="false" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="first_name" HeaderText="First Name" Visible="false" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="middle_name" HeaderText="Middle Name" Visible="false" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="preferred" HeaderText="Preferred" />
                    <asp:BoundField ItemStyle-Width="60px" DataField="Type" HeaderText="Type" />
                    <asp:BoundField ItemStyle-Width="200px" DataField="e_mail" HeaderText="Email" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="Address1" HeaderText="Address 1" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="Address2" HeaderText="Address 2" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="City" HeaderText="City" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="State" HeaderText="State" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="Zip" HeaderText="Zip" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="County" HeaderText="County" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="Country" HeaderText="Country" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="cell_phone" HeaderText="Cell Phone" />
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>
