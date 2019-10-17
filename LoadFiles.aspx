<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadFiles.aspx.cs" Inherits="PharmCASASPX.LoadFiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
                        <asp:FileUpload ID="fuSONISFile" runat="server" Width="500px"  />
                         <asp:Button runat="server" id="SONISUploadButton" text="Upload" onclick="btnUploadSONIFile_Click" />
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
                        <asp:FileUpload ID="fuPharmCASApplicantFile" runat="server" Width="500px"  > </asp:FileUpload>
                        <asp:Button runat="server" id="btnApplicantUploadButton" text="Upload" onclick="btnUploadPharmCASApplicantFile_Click" />
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
                        <asp:Button runat="server" id="btnCollegesAttendedUploadButton1" text="Upload" onclick="btnUploadPharmCASCollegesAttendedFile_Click" />
                        <asp:Label ID="lblCollegesAttendedFileName" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell BorderWidth="2" BorderStyle="Solid" BorderColor="Black">
                        <asp:Button ID="btnLoadPharmCASCollegesAttendedFile" runat="server" Text="Load PharmCAS Colleges Attended" OnClick="btnLoadPharmCASCollegesAttendedFileToSQL_Click" />
                        <br />
                        <asp:Label ID="lblPharmCASCollegesAttendedResults" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>