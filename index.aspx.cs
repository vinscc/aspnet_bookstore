using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Web_Book_Store_A2
{
    public partial class Index : System.Web.UI.Page
    {

        DataTable book = new DataTable();
        DataView dataView;

        DataTable cart = new DataTable();
        DataView cartView;

        List<int> bookId = new List<int>();
        List<string> bookTitle = new List<string>();
        List<string> bookRentDuration = new List<string>();
        List<double> bookPrice = new List<double>();

        //Get user selected rent duration
        int rentOption;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            set_genre();
            
            if(!IsPostBack)
            {
                BookDataList.DataSource = CreateDataSource;
                BookDataList.DataBind();
            }

            //Add column to the cart
            cart.Columns.Add("count");  //counter
            cart.Columns.Add("book"); //Book title
            cart.Columns.Add("loanDuration"); //loan duration
            cart.Columns.Add("loanPrice"); //loan price

            //Detect if there is a session
            if (Session["currBookCart"] != null)
            {
                get_Session();
            }
            else
            {
                cartDiv.Visible = false;
                checkoutDiv.Visible = false;
            }

            cartView = new DataView(cart);
        }

        //To calculate price in the cart
        protected void calculatePrice()
        {
            double sum = 0;
            for(int i=0; i<bookPrice.Count(); i++)
            {
                sum += (double)bookPrice[i];
            }
            priceTag.Text = "<b class='blue'>"+ bookPrice.Count() +" book(s)</b> for a total of <b class='red'>RM" + sum +"</b>";
        }

        //Save the list to session
        protected void set_Session()
        {
            Session["currBookCart"] = cart;
            Session["currBookId"] = bookId;
            Session["currBookTitle"] = bookTitle;
            Session["currBookDur"] = bookRentDuration;
            Session["currBookPrice"] = bookPrice;
        }

        //Retrieve session
        protected void get_Session()
        {
            bookId = (List<int>)Session["currBookId"];
            bookTitle = (List<String>)Session["currBookTitle"];
            bookRentDuration = (List<String>)Session["currBookDur"];
            bookPrice = (List<Double>)Session["currBookPrice"];
        }

        ICollection CreateDataSource
        {
            get
            {
                book.Columns.Add("id");
                book.Columns.Add("cat");
                book.Columns.Add("title");
                book.Columns.Add("img");
                book.Columns.Add("loan1");
                book.Columns.Add("loan2");
                book.Columns.Add("loan3");

                book.NewRow();

                bookRowAdd();

                dataView = new DataView(book);
                return dataView;
            }
        }

        ICollection CreateDataSourceFilter
        {
            get
            {
                book.Columns.Add("id");
                book.Columns.Add("cat");
                book.Columns.Add("title");
                book.Columns.Add("img");
                book.Columns.Add("loan1");
                book.Columns.Add("loan2");
                book.Columns.Add("loan3");

                book.NewRow();

                bookRowAdd();

                DataView dataView = new DataView(book);
                int holder;
                if (genre.SelectedValue == "Science")
                    holder = 1;
                else if (genre.SelectedValue == "Romance")
                    holder = 2;
                else if (genre.SelectedValue == "Non-fiction")
                    holder = 3;
                else
                    holder = 4;
                if(holder!= 4) 
                    dataView.RowFilter = "cat = '" + holder + "'";
                return dataView;
            }
        }

        //Add book collection
        protected void bookRowAdd()
        {
            book.Rows.Add(1, "1", "The Vaccine: Inside the Race to Conquer the COVID-19 Pandemic", @"images\sci-vaccine.jpg", "10", "15", "20");
            book.Rows.Add(2, "1", "Super Volcanoes: What They Reveal about Earth and the Worlds Beyond", @"images\sci-vol.jpg", "22", "38", "50");
            book.Rows.Add(3, "1", "Clean Code: A Handbook of Agile Software Craftsmanship", @"images\sci-cc.jpg", "13", "22", "31");
            book.Rows.Add(4, "2", "Rushed", @"images\rom-rushed.jpg", "7", "12", "17");
            book.Rows.Add(5, "2", "I Hate You More", @"images\rom-ihym.jpg", "20", "25", "35");
            book.Rows.Add(6, "2", "Not Your Average Hot Guy", @"images\rom-hg.jpg", "10", "15", "20");
            book.Rows.Add(7, "3", "Will", @"images\nf-will.jpg", "12", "22", "32");
            book.Rows.Add(8, "3", "Unbound: My Story of Liberation and the Birth of the Me Too Movement", @"images\nf-unb.jpg", "14", "19", "24");
            book.Rows.Add(9, "3", "A Promised Land", @"images\nf-ob.jpg", "70", "100", "150");
        }

        //Delete items from cart
        protected void Delete_Command(Object sender, DataListCommandEventArgs e)
        {
           if(booksViewDiv.Visible == false)
            {
                return;
            }

           //Get the book index in cart
           int index = Convert.ToInt32(((Label)e.Item.FindControl("labelCount")).Text);
           
            //Remove the book in cart based on the index
            for (int i = bookId.Count - 1; i >= 0; i--)
            {
                if(index == bookId[i])
                {
                    bookId.RemoveAt(i);
                    bookTitle.RemoveAt(i);
                    bookRentDuration.RemoveAt(i);
                    bookPrice.RemoveAt(i);
                    break;
                }
            }

            //Update book counter
            for (int i = 0; i < bookId.Count; i++)
            {
                bookId[i] = i+1;
            }

            //Save list
            set_Session();
            PrintCart();
            
            bookCart.DataSource = cartView;
            bookCart.DataBind();
        }

        //When genre is selected
        protected void genre_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_genre();
            
            DataView dataView = book.DefaultView;
            BookDataList.DataSource = CreateDataSourceFilter;
            BookDataList.DataBind();
        }

        //Display current genre view
        protected void set_genre()
        {
            genreSelected.Text = "Currently displaying: " + genre.SelectedValue;
        }

        //Datalist item function
        protected void BookDataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DropDownList list = (DropDownList)(e.Item.FindControl("rentDur"));
            
            //Function to add book to cart
            if (e.CommandName == "addBook")
            {
                //get user selected input
                if (list.SelectedValue == "none")
                    rentOption = 0;
                else if (list.SelectedValue == "1")
                    rentOption = 1;
                else if (list.SelectedValue == "2")
                    rentOption = 2;
                else
                    rentOption = 3;

                //get arguments
                if (rentOption != 0)
                {   
                    string[] arg = new string[5];
                    arg = e.CommandArgument.ToString().Split(';');
                    
                    //Add details to the lists
                    switch (rentOption)
                    {
                        case 1:
                            bookRentDuration.Add("1 week");
                            bookTitle.Add(arg[4]);
                            bookPrice.Add(Convert.ToDouble(arg[1]));
                            break;
                        case 2:
                            bookRentDuration.Add("2 weeks");
                            bookTitle.Add(arg[4]);
                            bookPrice.Add(Convert.ToDouble(arg[2]));
                            break;
                        case 3:
                            bookRentDuration.Add("1 month");
                            bookTitle.Add(arg[4]);
                            bookPrice.Add(Convert.ToDouble(arg[3]));
                            break;
                    }
                    //Increment the count in cart
                    bookId.Add(bookTitle.Count());

                    //Show cart
                    cartDiv.Visible = true;
                    
                    //Save session
                    set_Session();
                    
                    //Print cart
                    PrintCart();
                   
                    bookCart.DataSource = cartView;
                    bookCart.DataBind();
                } 
            }
        }

        protected void PrintCart()
        {
            get_Session();

            //Determine button status
            BtnStatus();

            //If cart is empty
            if(bookId.Count()<1)
            {
                cartDiv.Visible = false;
            }
            else
            {
                DataRow dr;
                //Print data to table
                for (int i = 0; i < bookId.Count(); i++)
                {
                    dr = cart.NewRow();

                    dr[0] = i + 1;
                    dr[1] = bookTitle[i];
                    dr[2] = bookRentDuration[i];
                    dr[3] = bookPrice[i];

                    cart.Rows.Add(dr);
                }
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            calculatePrice();
        }

        protected void BtnStatus()
        {
            if(bookId.Count < 3)
            {
                priceTag.Text = "";
                btnCalculate.Visible = false;
                btnCheckout.Visible = false;
                priceTag.Visible = false;
                currentIndex.Text = "<b class='blue'>" + bookId.Count() + " book(s)</b> in cart. <br /> Please select <b class='red'>" + (3 - bookId.Count) + " more books</b> to calculate price or checkout.";
            }
            else
            {
                btnCalculate.Visible = true;
                btnCheckout.Visible = true;
                priceTag.Visible = true;
                currentIndex.Text = "<b class='blue'>" + bookId.Count() + " book(s)</b> in cart.";
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            checkoutDiv.Visible = true;
            genreDiv.Visible = false;
            booksViewDiv.Visible = false;
            btnCheckout.Visible = false;
            
        }

        protected void btnSubmitCheckout_Click(object sender, EventArgs e)
        {
            String text;

            text = "<h3 class='red'>Loan Submission Success!</h3>" +
                "<br />Name: <span class='blue'>" + tbName.Text +
                "</span><br />Email: <span class='blue'>" + tbEmail.Text +
                "</span><br />Phone Number: <span class='blue'>" + tbPhone.Text +
                "</span><br />Total rented books: <span class='blue'>" + bookId.Count() +
                "</span><br />Books:";
            foreach(string title in bookTitle) {
                text += "<br /><span class='blue'>" + title +"</span>";
            }

            labelConfirmSubmit.Text = text;
            btnSubmitCheckout.Visible = false;
            checkoutFormDiv.Visible = false;
            btnCalculate.Visible = false;
        }
    }
}