import React from "react";
import { AgGridReact } from "ag-grid-react";
import { URL } from "./InvoiceConstants";
import "ag-grid-community/dist/styles/ag-grid.css";
import "ag-grid-community/dist/styles/ag-theme-balham.css";
//default styling of the react datagrid
const style = {
  height: "300px",
  width: "1100px",
  justifyContent: "center",
};

class InvoicePage extends React.Component {
  static displayName = InvoicePage.name;
  //defines the default state of invoices
  constructor(props) {
    super(props);
    this.state = {
      invoice: [],
    };
  }
  // fetch invoice makes a network call to the invoice end point to retrieve invoice data with a id argumenet
  fetchInvoice = () => {
    const { id } = this.props;
    fetch(`${URL}/${id}`)
      .then((res) => res.json())
      .then((data) => {
        this.setState({ invoice: data });
      })
      .catch((error) => {
        console.log(error);
      });
  };

  componentDidMount = () => {
    this.fetchInvoice();
  };
  // columns is a touple that holds the invoice data structure
  columns = [
    { field: "id", name: "ID" },
    { field: "invoiceNo", name: "Invoice Number" },
    { field: "customerNumber", name: "Customer Number" },
    { field: "customerName", name: "Customer Name" },
    { field: "amount", name: "Amount" },
    { field: "invoiceDate", name: "Invoice Date" },
    { field: "dueDate", name: "Due Date" },
    { field: "status", name: "Status" },
  ];

  render = () => {
    const { invoice } = this.state;
    return (
      <div>
        <div>
          <h3>Invoices</h3>
          <hr />
        </div>
        <div className='ag-theme-balham' style={style}>
          {/* react data grid needs two properties the column definition and the data to render */}
          <AgGridReact
            columnDefs={this.columns}
            rowData={invoice}
          ></AgGridReact>
        </div>
      </div>
    );
  };
}

export default InvoicePage;
