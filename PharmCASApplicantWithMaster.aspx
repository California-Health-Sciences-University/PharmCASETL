<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PharmCASApplicantWithMaster.aspx.cs" Inherits="PharmCASASPX.PharmCASApplicantWithMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div s>
        <asp:Table Width="100%" BorderColor="Black" BorderStyle="Solid" Font-Bold="true" runat="server">
            <asp:TableRow>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                    <asp:Label ID="Label5" runat="server" Text="PharmCAS Data" Font-Bold="true" Font-Size="Large"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <asp:Table Width="100%" BorderColor="Black" BorderStyle="Solid" Font-Bold="true" runat="server" CellPadding="10" BackColor="LightCyan">
            <asp:TableRow>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                    <asp:Label ID="Label6" runat="server" Text="PharmCAS ID" Font-Bold="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center" BackColor="White">
                    <asp:TextBox ID="txtCASID" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                    <asp:Label ID="Label8" runat="server" Text="Jenzabar ID" Font-Bold="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center" BackColor="White">
                    <asp:TextBox ID="txtJenzabarID" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                    <asp:Label ID="Label10" runat="server" Text="Last Name" Font-Bold="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center" BackColor="White">
                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                    <asp:Label ID="Label12" runat="server" Text="First Name" Font-Bold="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center" BackColor="White">
                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell BorderColor="Black" BorderStyle="Solid" Font-Bold="true" HorizontalAlign="Center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:GridView AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" AutoGenerateSelectButton="true" BorderWidth="2"
            BorderStyle="Solid" BorderColor="Black" CellPadding="3" GridLines="Both" runat="server"
            ID="gvPharmCASApplicants" CssClass="" DataKeyNames="cas_id"
            OnRowDataBound="OnRowDataBound" 
            OnPageIndexChanging="grdData_PageIndexChanging"
            OnSelectedIndexChanged="gvPharmCASApplicants_SelectedIndexChanged"
            OnDataBound="ApplicantsGridView_DataBound"
            OnSorting="ApplicantsGridView_Sorting"
            Width="100%">
            <HeaderStyle BackColor="Blue" ForeColor="White" BorderColor="Black" Font-Bold="true" Font-Size="Larger" BorderWidth="2" HorizontalAlign="Center" />
            <RowStyle BackColor="White"  ForeColor="Black" BorderColor="Black" BorderWidth="1" HorizontalAlign="Left" Font-Size="Medium" />
            <AlternatingRowStyle BackColor="LightBlue" ForeColor="Black" BorderColor="Black" BorderWidth="1" HorizontalAlign="Left" Font-Size="Medium"></AlternatingRowStyle>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton CommandName="" ID="ClickImage" OnClick="ClickImage_Click" ImageUrl="" runat="server"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-Width="150px" DataField="LastUpdate" HeaderText="Last Update"  />
                <asp:BoundField ItemStyle-Width="50px" DataField="cas_id" HeaderText="PharmCAS ID" />
                <asp:BoundField ItemStyle-Width="50px" DataField="JenzabarID" HeaderText="Jenzabar ID" />
                <asp:BoundField ItemStyle-Width="100px" DataField="last_name" HeaderText="Last Name" />
                <asp:BoundField ItemStyle-Width="100px" DataField="first_name" HeaderText="First Name" />
                <asp:BoundField ItemStyle-Width="50px" DataField="middle_name" HeaderText="Middle" />

                <asp:BoundField ItemStyle-Width="50px" DataField="Suffix" HeaderText="Suffix" />
                <asp:BoundField ItemStyle-Width="50px" DataField="date_of_birth" HeaderText="Birthdate" />
                <asp:BoundField ItemStyle-Width="50px" DataField="citizenship_status" HeaderText="Citizen Status" />
                <asp:BoundField ItemStyle-Width="50px" DataField="gender" HeaderText="Gender" />
                <asp:BoundField ItemStyle-Width="50px" DataField="PharmCASethnicity" HeaderText="PharmCAS Ethnicity" />
                <asp:BoundField ItemStyle-Width="50px" DataField="ethnicity" HeaderText="SONIS Ethnicity" />
                <asp:BoundField ItemStyle-Width="50px" DataField="military_status_extended" HeaderText="Veteran" />

                <asp:BoundField ItemStyle-Width="50px" DataField="Cohort" HeaderText="Cohort" />
                <asp:BoundField ItemStyle-Width="50px" DataField="level" HeaderText="Level" />
            </Columns>
        </asp:GridView>
        <asp:GridView AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" BorderWidth="2"
            BorderStyle="Solid" BorderColor="Black" CellPadding="3" GridLines="Both" runat="server" CssClass="ChildGrid"
            ID="grdEducation" DataKeyNames="cas_id">
            <Columns>
                <asp:BoundField ItemStyle-Width="50px" DataField="cas_id" HeaderText="PharmCAS ID" />
                <asp:BoundField ItemStyle-Width="50px" DataField="JenzabarID" HeaderText="Jenzabar ID" Visible="false" />
                <asp:BoundField ItemStyle-Width="100px" DataField="last_name" HeaderText="Last Name" Visible="false" />
                <asp:BoundField ItemStyle-Width="100px" DataField="first_name" HeaderText="First Name" Visible="false" />
                <asp:BoundField ItemStyle-Width="50px" DataField="middle_name" HeaderText="Middle Name" Visible="false" />

                <asp:BoundField ItemStyle-Width="40px" DataField="college_code" HeaderText="Institution Code" Visible="true" />
                <asp:BoundField ItemStyle-Width="200px" DataField="college_name" HeaderText="Institution" Visible="true" />
                <asp:BoundField ItemStyle-Width="100px" DataField="college_first_degree" HeaderText="First Degree" Visible="true" />
                <asp:BoundField ItemStyle-Width="50px" DataField="college_first_degree_status" HeaderText="First Degree Status" Visible="true" />
                <asp:BoundField ItemStyle-Width="50px" DataField="college_first_degree_date" HeaderText="First Degree Date" Visible="true" />
                <asp:BoundField ItemStyle-Width="50px" DataField="college_first_degree_primary_major" HeaderText="First Degree Major" Visible="true" />
                <asp:BoundField ItemStyle-Width="100px" DataField="college_second_degree" HeaderText="Second Degree" Visible="true" />
                <asp:BoundField ItemStyle-Width="50px" DataField="college_second_degree_status" HeaderText="Second Degree Status" Visible="true" />
                <asp:BoundField ItemStyle-Width="50px" DataField="college_second_degree_date" HeaderText="Second Degree Date" Visible="true" />
                <asp:BoundField ItemStyle-Width="50px" DataField="college_second_degree_primary_major" HeaderText="Second Degree Major" Visible="true" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="grdAddress" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
            <Columns>
                <asp:BoundField ItemStyle-Width="50px" DataField="cas_id" HeaderText="PharmCAS ID" />
                <asp:BoundField ItemStyle-Width="50px" DataField="JenzabarID" HeaderText="Jenzabar ID" Visible="false" />
                <asp:BoundField ItemStyle-Width="150px" DataField="last_name" HeaderText="Last Name" Visible="false" />
                <asp:BoundField ItemStyle-Width="150px" DataField="first_name" HeaderText="First Name" Visible="false" />
                <asp:BoundField ItemStyle-Width="50px" DataField="middle_name" HeaderText="Middle Name" Visible="false" />

                <asp:BoundField ItemStyle-Width="60px" DataField="Type" HeaderText="Type" />
                <asp:BoundField ItemStyle-Width="200px" DataField="email" HeaderText="Email" />
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