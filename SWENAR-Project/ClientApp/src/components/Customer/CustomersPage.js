import React, { Component } from "react";
import { AgGridReact } from "ag-grid-react";
import CustomerForm from "./CustomerForm";
import { URL } from "./CustomerConstants";
import "ag-grid-community/dist/styles/ag-grid.css";
import "ag-grid-community/dist/styles/ag-theme-balham.css";

//default styling the customers react data grid
const style = {
  height: "500px",
  width: "610px",
  justifyContent: "center"
};

export class CustomersPage extends Component {
  static displayName = CustomersPage.name;
  //default state of the compoment is empty customers
  constructor(props) {
    super(props);
    this.state = {
      customers: []
    };
  }
  // used to fetch all of the customers from the end point
  fetchCustomers = () => {
    fetch(URL)
      .then(res => res.json())
      .then(data => {
        console.log(data);
        this.setState({ customers: data });
      })
      .catch(error => {
        console.log(error);
      });
  };
  // when a page loads fetch the customer information
  componentDidMount = () => {
    this.fetchCustomers();
  };
  // if changes are made to customers update the datagrid.
  componentDidUpdate = ({ customers: prevCustomers }) => {
    const { customers } = this.state;
  };

  // update helper function to update name change.
  handleNumName = (Num, Name) => {
    let customer = {
      id: this.state.customers[this.state.customers.length - 1].id + 1,
      name: Name,
      number: Num
    };
    this.setState({
      customers: [...this.state.customers, customer]
    });
  };

  // properties of each customer
  columns = [
    {
      field: "id",
      name: "ID",
      cellRenderer: params => {
        var link = document.createElement("a");
        link.href = "/customerpage/" + params.value;
        link.innerText = params.value;
        return link;
      }
    },
    { field: "name", name: "Name" },
    { field: "number", name: "Number" }
  ];

  render = () => {
    // Rendering out hhe customers React data grid.
    return (
      <div>
        <div>
          <h3>Customers</h3>
          <CustomerForm handleNumName={this.handleNumName} />
          <hr />
        </div>
        <div className='ag-theme-balham' style={style}>
          <AgGridReact
            columnDefs={this.columns}
            rowData={this.state.customers}
          ></AgGridReact>
        </div>
      </div>
    );
  };
}
