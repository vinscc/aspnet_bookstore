<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web_Book_Store_A2.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SWE1904865 - Book Store</title>
    <link rel="stylesheet" href="index.css"/>
    <style type="text/css">
        .auto-style1 {
            width: 373px;
            height: 506px;
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style3 {
            width: 39%;
        }
        .auto-style4 {
            width: 242px;
            text-align: right;
        }
        .auto-style5 {
            width: 242px;
            height: 30px;
            text-align: right;
        }
        .auto-style6 {
            height: 30px;
            text-align: left;
        }
        .auto-style7 {
            margin-top: 70px;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 70px;
        }
        .auto-style8 {
            border-style: solid;
            border-color: inherit;
            border-width: medium;
            text-align: center;
        }
        .auto-style9 {
            text-align: left;
        }
        .auto-style10 {
            text-align: center;
            width: 1000px;
        }
        .auto-style11 {
            width: 134px;
        }

        .autostyle12 {
            width: 134px;
        }

        .auto-style13 {
            width: 50px;
        }

        .auto-style15 {
            width: 50px;
        }
        </style>
</head>
<body>
    <form id="genreForm" runat="server" class="content-pad border ct-bg">
        <div>
            <h1>Alvin's Book Store</h1>
            <br />
            <br />
            
        </div>
        
        <div id="cartDiv" runat="server" class="auto-style8 nor-content-pad non-bg">
            <asp:Label ID="currentIndex" runat="server"></asp:Label>
            <asp:DataList ID="bookCart" runat="server" GridLines="Both" OnDeleteCommand="Delete_Command" CellPadding="4" ShowFooter="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellSpacing="2" CssClass="auto-style20" ForeColor="Black">
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle Font-Bold="True" BackColor="Black" ForeColor="White" />
                <HeaderTemplate>
                    <table class="auto-style10">
                        <tr>
                            <td class="auto-style13">#</td>
                            <td class="auto-style12">Book</td>
                            <td class="auto-style11">Loan Duration</td>
                            <td class="auto-style11">Price(RM)</td>
                            <td class="auto-style15">Action</td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemStyle BackColor="White" />
                <ItemTemplate>
                    <table class="auto-style10">
                        <tr>
                            <td class="auto-style13">
                                <asp:Label ID="labelCount" runat="server" text='<%# Eval("count") %>'></asp:Label></td>
                            <td class="auto-style12"><asp:Label ID="labelBook" runat="server" text='<%# Eval("book") %>'></asp:Label></td>
                            <td class="auto-style11"><asp:Label ID="labelLoanDur" runat="server" text='<%# Eval("loanDuration") %>'></asp:Label></td>
                            <td class="auto-style11"><asp:Label ID="labelLoanPrice" runat="server" text='<%# Eval("loanPrice") %>'></asp:Label></td>
                            <td class="auto-style15">
                                <asp:LinkButton id="lbDelete" Text="Delete" CommandName="Delete" runat="server"/>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            </asp:DataList>
            <asp:Label ID="priceTag" runat="server"></asp:Label>
            <br />
            <asp:Button ID="btnCalculate" class="btn" runat="server" Text="Calculate" OnClick="btnCalculate_Click"/>
            <asp:Button ID="btnCheckout" class="btn" runat="server" Text="Checkout" OnClick="btnCheckout_Click"/>
            <br />
            
        </div>
        <div class="auto-style2">
        </div>
        <div runat="server" id="checkoutDiv" class="auto-style8 nor-content-pad non-bg">
            <h1>Book Rental Checkout</h1>
            <div runat="server" id="checkoutFormDiv" class="auto-style2" >

                <table class="auto-style3" align="center">
                    <tr>
                        <td class="auto-style4">Name</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="tbName" runat="server" placeholder="Alvin Yeoh"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style5">Email Address</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="tbEmail" runat="server" placeholder="swe1904865@xmu.edu.my"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Phone Number</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="tbPhone" runat="server" placeholder="0123456789"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnSubmitCheckout" class="btn" runat="server" Text="Confirm" OnClick="btnSubmitCheckout_Click" />
            </div>
            <asp:Label ID="labelConfirmSubmit" runat="server" Text=""></asp:Label>
        </div>
        <div runat="server" id="genreDiv" class="auto-style2 div-padding border genre-bg" style="text-align: center">
            <asp:Label ID="genrePrompt" runat="server" Text="Select a genre: "></asp:Label>
            <asp:DropDownList class="genre-ddl" ID="genre" runat="server" Width="138px" OnSelectedIndexChanged="genre_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Selected="True">All</asp:ListItem>
                <asp:ListItem>Science</asp:ListItem>
                <asp:ListItem>Romance</asp:ListItem>
                <asp:ListItem>Non-fiction</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div runat="server" id="booksViewDiv" class="auto-style8 non-bg">
            <br /><br /><asp:Label ID="genreSelected" runat="server" class="genre-txt"></asp:Label>
            <div>
                <asp:DataList ID="BookDataList" runat="server" RepeatColumns="3" OnItemCommand="BookDataList_ItemCommand" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" CssClass="auto-style7" GridLines="Both" Width="1381px">
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#FFF7E7" HorizontalAlign="Center" VerticalAlign="Top" ForeColor="#8C4510" />
                    <ItemTemplate>
                        <table class="auto-style1">
                            <tr>
                                <td style="text-align: center">
                                    <br />
                                    <asp:Label ID="lbTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Image ID="imgBook" runat="server" ImageUrl='<%# Eval("img") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">Loan Prices:<br />1 week: RM
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("loan1") %>'></asp:Label>
                                    <br />
                                    2 weeks: RM
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("loan2") %>'></asp:Label>
                                    <br />
                                    1 month: RM
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("loan3") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="Label4" runat="server" Text="Select loan duration:" />
                                       <asp:DropDownList ID="rentDur" runat="server" class="dur-ddl">
                                        <asp:ListItem>none</asp:ListItem>
                                        <asp:ListItem Value="1">1 week</asp:ListItem>
                                        <asp:ListItem Value="2">2 weeks</asp:ListItem>
                                        <asp:ListItem Value="3">1 month</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Button ID="btnAdd" runat="server" class="btn" Text="Add to cart" CommandName="addBook" CommandArgument='<%#Eval("title") + ";" +Eval("loan1")  + ";" +Eval("loan2") + ";" +Eval("loan3") + ";" +Eval("Title")%> ' />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
            </div>
        </div>
    </form>
</body>
</html>

