<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoadFilesWithMaster.aspx.cs" Inherits="PharmCASASPX.LoadFilesWithMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>

        <asp:Table ID="Table1" runat="server" Width="100%" BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
            <asp:TableHeaderRow BorderWidth="2" BorderStyle="Solid" BorderColor="Black">

                <asp:TableHeaderCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:Label ID="Label5" runat="server" Text="File Name"></asp:Label>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Width="50%" BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:Label ID="Label2" runat="server" Text="File Picker"></asp:Label>
                </asp:TableHeaderCell>

                <asp:TableHeaderCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">

                    <asp:Label ID="Label6" runat="server" Text="Load Button"></asp:Label>
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">

                    <asp:Label ID="lblFindSONIFile" runat="server" Text="Find SONIS Student Profile File"></asp:Label><br />
                    <asp:Label ID="Label3" runat="server" Text="Last Upload:  "></asp:Label>
                    <asp:Label ID="lblLastSONISUpload" runat="server" Text="Some Date"></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:FileUpload ID="fuSONISFile" runat="server" Width="500px" />
                    <asp:Button runat="server" ID="SONISUploadButton" Text="Upload" OnClick="btnUploadSONIFile_Click" />
                    <asp:Label ID="lblSONISFileName" runat="server" Text=" "></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:Button ID="btnLoadSONISFile" runat="server" Text="Load SONIS File" OnClick="btnLoadSONISFileToSQL_Click" /><br />
                    <asp:Label ID="lblSONISResults" runat="server" Text="Label"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:Label ID="btnFindPharmCASApplicantFile" runat="server" Text="Find PharmCAS ApplicantExtended File"></asp:Label><br />
                    <asp:Label ID="Label4" runat="server" Text="Last Upload:  "></asp:Label>
                    <asp:Label ID="lblLastPharmCASApplicantUpload" runat="server" Text="Some Date"></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:FileUpload ID="fuPharmCASApplicantFile" runat="server" Width="500px"></asp:FileUpload>
                    <asp:Button runat="server" ID="btnApplicantUploadButton" Text="Upload" OnClick="btnUploadPharmCASApplicantFile_Click" />
                    <asp:Label ID="lblPharmCASApplicantFileName" runat="server" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:Button ID="btnLoadPharmCASApplicantFile" runat="server" Text="Load PharmCAS Applicants" OnClick="btnLoadPharmCASApplicantFileToSQL_Click" />
                    <br />
                    <asp:Label ID="lblPharmCASApplicantFileResults" runat="server" Text="Label"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:Label ID="Label1" runat="server" Text="Find PharmCAS CollegesAttended File"></asp:Label><br />
                    <asp:Label ID="label10" runat="server" Text="Last Upload:  "></asp:Label>
                    <asp:Label ID="lblLastCollegesAttendedUpload" runat="server" Text="Some Date"></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:FileUpload ID="fuPharmCASCollegesAttendedFile" runat="server" Width="500px" />
                    <asp:Button runat="server" ID="btnCollegesAttendedUploadButton1" Text="Upload" OnClick="btnUploadPharmCASCollegesAttendedFile_Click" />
                    <asp:Label ID="lblCollegesAttendedFileName" runat="server" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                    <asp:Button ID="btnLoadPharmCASCollegesAttendedFile" runat="server" Text="Load PharmCAS Colleges Attended" OnClick="btnLoadPharmCASCollegesAttendedFileToSQL_Click" />
                    <br />
                    <asp:Label ID="lblPharmCASCollegesAttendedResults" runat="server" Text="Label"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
              </asp:Table>
         <asp:Table ID="Table2" runat="server" Width="100%" BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
            <asp:TableRow BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                     <asp:Button runat="server" ID="btnCreateOutputFiles" Text="Create Output Files" OnClick="btnCreateOutputFiles_Click" />
                    
                </asp:TableCell>
                          <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                               <asp:Button runat="server" ID="btnDownloadSONISStudents" Text="Download SONIS Student File" OnClick="btnDownloadSONISStudents_Click" />
                    
                </asp:TableCell>
                          <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">

                     <asp:Button runat="server" ID="btnDownloadSONISAddressesLocal" Text="Download SONIS Local Address File" OnClick="btnDownloadSONISAddressesLocal_Click" />
                </asp:TableCell>
                          <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">

                     <asp:Button runat="server" ID="btnDownloadSONISAddressesMailing" Text="Download SONIS Mailing Address File" OnClick="btnDownloadSONISAddressesMailing_Click" />
                </asp:TableCell>
                          <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                               <asp:Button runat="server" ID="btnDownloadSONISAddressesPermanent" Text="Download SONIS Permanent Address File" OnClick="btnDownloadSONISAddressesPermanent_Click" />
                    
                </asp:TableCell>
                          <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                               <asp:Button runat="server" ID="btnDownloadSONISEducation" Text="Download SONIS Education File" OnClick="btnDownloadSONISEducation_Click" />
                    
                </asp:TableCell>

            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>